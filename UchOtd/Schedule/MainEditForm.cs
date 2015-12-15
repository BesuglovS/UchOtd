using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Constants;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Repositories.Common;
using UchOtd.Core;
using UchOtd.Forms;
using UchOtd.Properties;
using UchOtd.Schedule.Core;
using UchOtd.Schedule.Forms;
using UchOtd.Schedule.Forms.Analysis;
using UchOtd.Schedule.Forms.DBLists;
using UchOtd.Schedule.Forms.DBLists.Lessons;
using UchOtd.Schedule.Views.DBListViews;
using UchOtd.Schedule.wnu;
using StudentList = UchOtd.Schedule.Forms.DBLists.StudentList;
using Utilities = UchOtd.Core.Utilities;

namespace UchOtd.Schedule
{
    public partial class MainEditForm : Form
    {
        public ScheduleRepository Repo;

        public static bool SchoolHeader = false;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public MainEditForm(ScheduleRepository repo)
        {
            InitializeComponent();

            Repo = repo;
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();

            LoadLists();

            if (Repo != null)
            {
                Text = "Расписание (" + Utilities.ExtractDbOrConnectionName(Repo.GetConnectionString()) + ")";
            }

            if (StartupForm.School)
            {
                uploadPrefix.Text = "s_";
            }
        }

        private void LoadLists()
        {
            var groups = Repo
                .StudentGroups
                .GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            groupList.ValueMember = "StudentGroupId";
            groupList.DisplayMember = "Name";
            groupList.DataSource = groups;            

            var faculties = Repo
                .Faculties
                .GetAllFaculties()
                .OrderBy(f => f.SortingOrder)
                .ToList();

            FacultyList.DisplayMember = "Letter";
            FacultyList.ValueMember = "FacultyId";
            FacultyList.DataSource = faculties;

            var faculties2 = Repo
                .Faculties
                .GetAllFaculties()
                .OrderBy(f => f.SortingOrder)
                .ToList();

            WordFacultyFilter.DisplayMember = "Letter";
            WordFacultyFilter.ValueMember = "FacultyId";
            WordFacultyFilter.DataSource = faculties2;

            WordExportWeekFilter.Items.Clear();
            for (int i = 1; i <= 18; i++)
            {
                WordExportWeekFilter.Items.Add(i);
            }
            

            DOWList.Items.Clear();
            foreach (var dow in Constants.DowLocal.Values)
            {
                DOWList.Items.Add(dow);
            }
            DOWList.SelectedIndex = 0;
            /*
            var weeks = _repo
                .GetAllActiveLessons()
                .Select(l => _repo.CalculateWeekNumber(l.Calendar.Date))
                .Distinct()
                .OrderBy(w => w)
                .ToList();
            
            WeekFilter.Items.Clear();
            foreach (var week in weeks)
            {
                WeekFilter.Items.Add(week);
            }*/

            WeekFilter.Items.Clear();
            for (int i = 1; i <= 18; i++)            
            {
                WeekFilter.Items.Add(i);
            }

            siteToUpload.Items.Clear();
            foreach (var siteEndPoint in Constants.SitesUploadEndPoints)
            {
                siteToUpload.Items.Add(siteEndPoint);
            }
            if (Constants.SitesUploadEndPoints.Count > 0)
            {
                siteToUpload.SelectedIndex = 0;
            }
        }

        public List<GroupTableView> GetGroupSchedule(int groupId, bool showProposed, CancellationToken cToken, bool isWeekFilered, int weekFilterNum, bool onlyFutureDates)
        {
            var sStarts = Repo.CommonFunctions.GetSemesterStarts();

            int weekNum = -1;
            if (isWeekFilered)
            {
                weekNum = weekFilterNum;
            }

            cToken.ThrowIfCancellationRequested();

            var groupLessons = Repo.Lessons.GetGroupedGroupLessons(groupId, sStarts, weekNum, showProposed, onlyFutureDates);

            cToken.ThrowIfCancellationRequested();

            return CreateGroupTableView(groupId, groupLessons, showProposed);
        }

        private async void ShowGroupLessonsClick(object sender, EventArgs e)
        {
            if (showGroupLessons.Text == "Go")
            {
                _cToken = _tokenSource.Token;

                var cancelled = false;

                showGroupLessons.Text = "";
                showGroupLessons.Image = Resources.Loading;

                var groupId = (int) groupList.SelectedValue;
                var showProposed = showProposedLessons.Checked;
                var isWeekFilered = weekFiltered.Checked;
                int weekFilterNum = -1;
                int.TryParse(WeekFilter.Text, out weekFilterNum);
                var onlyFutureDates = OnlyFutureDatesExportInWord.Checked;

                try
                {
                    ScheduleView.DataSource = await Task.Run(() => GetGroupSchedule(groupId, showProposed, _cToken, isWeekFilered, weekFilterNum, onlyFutureDates), _cToken);
                }
                catch (OperationCanceledException)
                {
                    cancelled = true;
                }

                if (!cancelled)
                {
                    ScheduleView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    UpdateViewWidth();
                }

                showGroupLessons.Image = null;
                showGroupLessons.Text = "Go";
            }
            else
            {
                _tokenSource.Cancel();
            }
        }

        private void UpdateViewWidth()
        {
            if (ScheduleView.DataSource == null)
            {
                return;
            }

            for (int i = 1; i <= 7; i++)
            {
                ScheduleView.Columns[i].HeaderText = Constants.DowLocal[i];
                if (i < 7)
                {
                    ScheduleView.Columns[i].Width = (ScheduleView.Width - ScheduleView.Columns[0].Width - 20) / 6;
                }

            }
        }
        
        public List<GroupTableView> CreateGroupTableView(
            int groupId, Dictionary<string, Dictionary<string, Tuple<string, List<Lesson>>>> groupLessons,
            bool putProposedLessons)
        {
            var result = new List<GroupTableView>();

            var groupView = CreateGroupView(groupId, groupLessons);
            foreach (var gv in groupView)
            {
                var time = gv.Datetime.Substring(2, gv.Datetime.Length - 2);

                switch (gv.Datetime.Substring(0,1))
                {
                    case "1":
                        var gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, MonEvents = gv.Events });
                        }                            
                        else
                        {
                            result[gtv].MonEvents = gv.Events;
                        }
                        break;
                    case "2":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, TueEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].TueEvents = gv.Events;
                        }
                        break;
                    case "3":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, WenEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].WenEvents = gv.Events;
                        }
                        break;
                    case "4":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, ThuEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].ThuEvents = gv.Events;
                        }
                        break;
                    case "5":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, FriEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].FriEvents = gv.Events;
                        }
                        break;
                    case "6":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, SatEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].SatEvents = gv.Events;
                        }
                        break;
                    case "7":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, SunEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].SunEvents = gv.Events;
                        }
                        break;
                }
            }

            result = result.OrderBy(ge => DateTime.ParseExact(ge.Time, "H:mm", CultureInfo.InvariantCulture)).ToList();

            return result;
        }

        private IEnumerable<GroupView> CreateGroupView(
            int groupId, Dictionary<string, Dictionary<string, Tuple<string, List<Lesson>>>> data)
        {
            const string proposedLessonStartToken = "[";
            const string proposedLessonEndToken = "]";

            var result = new List<GroupView>();

            var group = Repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.StudentGroupId == groupId);

            var plainGroupName = "";
            var nGroupName = "";

            if (group.Name.Contains(" (+Н)"))
            {
                plainGroupName = group.Name.Replace(" (+Н)", "");
                nGroupName = group.Name.Replace(" (+", "(");
            }


            foreach (var dt in data)
            {
                var eventString = "";

                for (int i = 0; i < dt.Value.Count; i++)
                {
                    var item = dt.Value.ElementAt(i);
                    var tfd = item.Value.Item2[0].TeacherForDiscipline;
                    var state = item.Value.Item2[0].State;

                    var discName = tfd.Discipline.Name;
                    if (tfd.Discipline.StudentGroup.StudentGroupId != groupId &&
                        ((plainGroupName == "") || (tfd.Discipline.StudentGroup.Name != plainGroupName)) &&
                        ((nGroupName == "") || (tfd.Discipline.StudentGroup.Name != nGroupName)))
                    {
                        discName += " (" + tfd.Discipline.StudentGroup.Name + ")";
                    }

                    eventString += IfProposed(discName, state, proposedLessonStartToken, proposedLessonEndToken);
                    eventString += Environment.NewLine;

                    eventString += IfProposed(tfd.Teacher.FIO, state, proposedLessonStartToken, proposedLessonEndToken);
                    eventString += Environment.NewLine;

                    eventString += IfProposed("(" + item.Value.Item1 + ")", state, proposedLessonStartToken, proposedLessonEndToken); 
                    eventString += Environment.NewLine;

                    var audStrings = "";
                    var audWeekList = item.Value.Item2.ToDictionary(l => Repo.CommonFunctions.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
                    var grouped = audWeekList.GroupBy(a => a.Value);

                    var enumerable = grouped as List<IGrouping<string, KeyValuePair<int, string>>> ?? grouped.ToList();
                    var gcount = enumerable.Count();
                    if (gcount == 1)
                    {
                        audStrings += enumerable.ElementAt(0).Key;
                    }
                    else
                    {
                        for (int j = 0; j < gcount; j++)
                        {
                            var jItem = enumerable.ElementAt(j);
                            audStrings += CommonFunctions.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " + jItem.Key;

                            if (j != gcount - 1)
                            {
                                audStrings += Environment.NewLine;
                            }
                        }
                    }

                    eventString += IfProposed(audStrings, state, proposedLessonStartToken, proposedLessonEndToken);

                    if (i != dt.Value.Count - 1)
                    {
                        eventString += Environment.NewLine;
                    }

                }

                result.Add(new GroupView { Datetime = dt.Key, Events = eventString });
            }

            return result;
        }

        private string IfProposed(string text, int state, string startToken, string endToken)
        {
            return (state == 2) ? startToken + text + endToken : text;
        }
        
        private void ExportGroupDisciplines(string filename)
        {
            String semesterString = (Repo.CommonFunctions.GetSemesterStarts().Month > 6) ? " (1 семестр)" : " (2 семестр)";

            foreach (var faculty in Repo.Faculties.GetAllFaculties())
            {
                foreach (var group in Repo.Faculties.GetFacultyGroups(faculty.FacultyId))
                {
                    AppendToFile(filename, "*" + group.Name + semesterString);

                    var studentIds = Repo
                        .StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);

                    var groupsListIds = Repo
                        .StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                        .ToList()
                        .Select(stig => stig.StudentGroup.StudentGroupId);

                    var tfds = Repo.TeacherForDisciplines.GetFiltredTeacherForDiscipline(tfd => groupsListIds.Contains(tfd.Discipline.StudentGroup.StudentGroupId));

                    foreach (var tfd in tfds)
                    {
                        AppendToFile(filename, 
                            tfd.Discipline.Name + '\t' + 
                            tfd.Discipline.StudentGroup.Name + '\t' +
                            tfd.Teacher.FIO + '\t' +
                            Repo.CommonFunctions.GetTfdHours(tfd.TeacherForDisciplineId)
                            /*tfd.Discipline.AuditoriumHours + '\t' +
                            Constants.Constants.Attestation[tfd.Discipline.Attestation]*/
                        );
                    }
                }
            }
        }

        private void ExportDiscAuds(string filename)
        {
            //var sw = new StreamWriter(filename);
            var groupsId = Repo.StudentGroups.GetFiltredStudentGroups(sg => sg.Name == "12 Д (+Н)" || sg.Name == "13 Д (+Н)" || sg.Name == "14 Д")
                    .Select(sg => sg.StudentGroupId)
                    .ToList();

            var econstudentIds = Repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => groupsId.Contains(sig.StudentGroup.StudentGroupId))
                .Select(stig => stig.Student.StudentId)
                .ToList();

            foreach (var tfd in Repo.TeacherForDisciplines.GetAllTeacherForDiscipline().OrderBy(tefd => tefd.Discipline.Name))
            {
                if (tfd.Discipline.StudentGroup.Name == "12 И")
                {
                    continue;
                }

                var localTfd = tfd;
                var studentIds = Repo
                        .StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => 
                        sig.StudentGroup.StudentGroupId == localTfd.Discipline.StudentGroup.StudentGroupId)
                    .Select(stig => stig.Student.StudentId)
                    .ToList();

                

                var econ = studentIds.Any(econstudentIds.Contains);

                if (!econ)
                {
                    continue;
                }

                var sb = new StringBuilder();
                sb.Append(tfd.Discipline.Name + '\t' + tfd.Discipline.StudentGroup.Name + '\t');

                var auds = Repo.Lessons.GetFiltredLessons(l =>
                    (l.State == 1) &&
                    l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId)
                    .ToList()
                    .Select(l => l.Auditorium.Name)                    
                    .Distinct()
                    .OrderBy(n => n)
                    .ToList();

                foreach (var aud in auds)
                {
                    sb.Append(aud + '\t');
                }

                //sw.WriteLine(sb.ToString());
                AppendToFile(filename, sb.ToString());
            }

            //sw.Close();
        }

        private void ExportFacultyGroups()
        {
            var faculty = Repo.Faculties.GetFirstFiltredFaculty(f => f.Letter == "У");

            foreach (var group in Repo.Faculties.GetFacultyGroups(faculty.FacultyId))
            {
                AppendToFile("Oops\\groups.txt", group.Name);

                var studentIds = Repo
                        .StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);

                var groupsList = Repo
                    .StudentsInGroups
                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .ToList()
                    .Select(stig => stig.StudentGroup)
                    .Distinct()
                    .ToList();

                foreach (var g in groupsList)
                {
                    AppendToFile("Oops\\groups.txt", g.Name);

                    var studentsInGroup = Repo
                        .StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == g.StudentGroupId)
                        .Select(sig => sig.Student)
                        .ToList()
                        .OrderBy(s => s.F)
                        .ThenBy(s => s.I)
                        .ThenBy(s => s.O)
                        .ThenBy(s => s.BirthDate)
                        .ToList();

                    foreach (var student in studentsInGroup)
                    {
                        AppendToFile("Oops\\groups.txt", student.F + " " + student.I + " " + student.O);
                    }

                    AppendToFile("Oops\\groups.txt", "");
                }
            }
        }

        private void ExportScheduleDates(string filename)
        {
            String semesterString = (Repo.CommonFunctions.GetSemesterStarts().Month > 6) ? " (1 семестр)" : " (2 семестр)";

            /*
            foreach (var faculty in Repo.Faculties.GetAllFaculties().OrderBy(f => f.SortingOrder))
            {*/
            //var faculty = _repo.GetFirstFiltredFaculty(f => f.Letter == "Г");
            /*
                foreach (var group in Repo.Faculties.GetFacultyGroups(faculty.FacultyId))
                {*/
            var group = Repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.Name == "12 Т");

                    AppendToFile(filename, "*" + group.Name + semesterString);

                    var studentIds = Repo
                        .StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);

                    var groupsListIds = Repo
                        .StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                        .ToList()
                        .Select(stig => stig.StudentGroup.StudentGroupId);

                    var tfds = Repo.TeacherForDisciplines.GetFiltredTeacherForDiscipline(tfd => groupsListIds.Contains(tfd.Discipline.StudentGroup.StudentGroupId));

                    foreach (var tfd in tfds)
                    {                        

                        var sb = new StringBuilder();
                        sb.Append(
                            tfd.Discipline.StudentGroup.Name + '\t' +
                            tfd.Discipline.Name + '\t' +
                            tfd.Discipline.AuditoriumHours + '\t' +
                            tfd.Teacher.FIO + '\n'
                        );


                        var lessons = Repo.Lessons.GetFiltredLessons(l => (l.State == 1) && l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId);

                        foreach (var lesson in lessons.OrderBy(l => l.Calendar.Date.Date))
                        {
                            sb.Append('\t' + lesson.Calendar.Date.Date.ToString("dd.MM.yyyy"));
                        }

                        AppendToFile(filename, sb.ToString());
                    }            
              //  }
            //}
        }

        private static void AppendToFile(String filename, String line)
        {
            var sw = new StreamWriter(filename, true);
            sw.WriteLine(line);
            sw.Close();
        }
        

        private void ImportStudentData(string filename)
        {
            var studentList = new List<Student>();
            var studentGroups = new List<StudentGroup>();
            var studentsInGroups = new List<StudentsInGroups>();

            var sr = new StreamReader(filename);

            string line;

            var maxStudentId = Repo
                .Students
                .GetAllStudents()
                .Select(s => s.StudentId)
                .Max();
            maxStudentId++;

            var studentIdRemap = new Dictionary<int, int>();

            sr.ReadLine();
            while ((line = sr.ReadLine()) != "StudentGroups")
            {
                if (line != null)
                {
                    var studentParts = line.Split('@');
                    var student = new Student
                    { 
                        StudentId = maxStudentId, 
                        F = studentParts[1], 
                        I = studentParts[2], 
                        O = studentParts[3],
                        Address = "",
                        BirthDate = new DateTime(2000,1,1),
                        Expelled = false,
                        NFactor = false,
                        Orders = "",
                        PaidEdu = false,
                        Phone = "",
                        Starosta = false,
                        ZachNumber = ""
                    };

                    studentIdRemap.Add(int.Parse(studentParts[0]), maxStudentId);

                    studentList.Add(student);

                    Repo.Students.AddStudent(student);
                }

                maxStudentId++;
            }

            var maxGroupId = Repo
                .StudentGroups
                .GetAllStudentGroups()
                .Select(s => s.StudentGroupId)
                .Max();
            maxGroupId++;

            var groupIdRemap = new Dictionary<int, int>();
            while ((line = sr.ReadLine()) != "StudentsInGroups")
            {
                if (line != null)
                {
                    var groupParts = line.Split('@');

                    if (!Repo.StudentGroups.GetFiltredStudentGroups(sg => sg.Name == groupParts[1]).Any())
                    {
                        var group = new StudentGroup
                        {
                            StudentGroupId = maxGroupId,
                            Name = groupParts[1]
                        };

                        groupIdRemap.Add(int.Parse(groupParts[0]), maxGroupId);

                        studentGroups.Add(@group);

                        Repo.StudentGroups.AddStudentGroup(@group);

                        maxGroupId++;
                    }                
                    else
                    {
                        var gr = Repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.Name == groupParts[1]);
                        studentGroups.Add(gr);

                        groupIdRemap.Add(int.Parse(groupParts[0]), gr.StudentGroupId);
                    }
                }
            }

            while ((line = sr.ReadLine()) != null)
            {
                var sigParts = line.Split('@');

                var studentId = int.Parse(sigParts[0]);
                studentId = studentIdRemap[studentId];

                var studentGroupId = int.Parse(sigParts[1]);
                if (groupIdRemap.ContainsKey(studentGroupId))
                {
                    studentGroupId = groupIdRemap[studentGroupId];
                }
                else
                {
                    studentGroupId = Repo.StudentGroups.GetFirstFiltredStudentGroups(sg =>
                    {
                        var studentGroup = studentGroups.FirstOrDefault(stg => stg.StudentGroupId == studentGroupId);
                        return studentGroup != null && sg.Name == studentGroup.Name;
                    }).StudentGroupId;
                }

                var sig = new StudentsInGroups
                {
                    Student = Repo.Students.GetStudent(studentId),
                    StudentGroup = Repo.StudentGroups.GetStudentGroup(studentGroupId)
                };

                studentsInGroups.Add(sig);

                Repo.StudentsInGroups.AddStudentsInGroups(sig);
            }

            sr.Close();
        }

        private void ExportStudentsData(string filename)
        {
            var sw = new StreamWriter(filename);

            sw.WriteLine("Students");
            foreach (var student in Repo.Students.GetFiltredStudents(s => !s.Expelled))
            {
                sw.WriteLine(
                    student.StudentId + "@" +
                    student.F + "@" +
                    student.I + "@" +
                    student.O
                );
            }

            sw.WriteLine("StudentGroups");
            foreach (var sg in Repo.StudentGroups.GetAllStudentGroups())
            {
                sw.WriteLine(sg.StudentGroupId + "@" + sg.Name);
            }

            sw.WriteLine("StudentsInGroups");
            foreach (var sig in Repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => !sig.Student.Expelled))
            {
                sw.WriteLine(sig.Student.StudentId + "@" + sig.StudentGroup.StudentGroupId);
            }

            sw.Close();
        }

        private List<Lesson> SchoolAudLessons()
        {
            var aSchool = Repo.Auditoriums.Find(a => a.Name.Contains("ШКОЛА"));
            var ll = Repo.Lessons.GetFiltredLessons(l => l.Auditorium.AuditoriumId == aSchool.AuditoriumId && l.Calendar.Date > DateTime.Now && (l.State == 1));
            return ll;
        }

        private void LogDoubledLessons(string filename)
        {
            var sw = new StreamWriter(filename);
            sw.Close();
            var students = Repo.Students.GetFiltredStudents(s => !s.Expelled);
            foreach (var student in students)
            {
                var studentGroupIds = Repo
                    .StudentsInGroups
                    .GetFiltredStudentsInGroups(sig => sig.Student.StudentId == student.StudentId)
                    .Select(sig => sig.StudentGroup.StudentGroupId);

                var studentLessons = Repo.Lessons.GetFiltredLessons(l => (l.State == 1) && studentGroupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId));

                var grouped = studentLessons
                    .GroupBy(l => l.Calendar.CalendarId + " " + l.Ring.RingId)
                    .Where(g => g.Count() > 1)
                    .ToList();

                foreach (var group in grouped)
                {
                    var gg = group.ToList();
                    foreach (var lesson in gg)
                    {
                        sw = new StreamWriter(filename, true);
                        sw.Write(lesson.Calendar.Date.ToShortDateString() + "\t" + lesson.Ring.Time.ToString("H:mm") + "\t");
                        sw.Write(student.F + " " + student.I + " " + student.O + "\t");
                        sw.Write(lesson.TeacherForDiscipline.Discipline.StudentGroup.Name + "\t");
                        sw.Write(lesson.TeacherForDiscipline.Discipline.Name + "\t");
                        sw.Write(lesson.TeacherForDiscipline.Teacher.FIO + "\t");
                        sw.Write(lesson.Auditorium.Name);
                        sw.WriteLine();                        
                        sw.Close();
                    }
                }
            }            
        }

        private void LogDayLessons(string filename)
        {
            var sw = new StreamWriter(filename);
            var date = new DateTime(2013, 11, 18);
            var ll = Repo.Lessons.GetFiltredLessons(l => (l.State == 1) && l.Calendar.Date == date);
            foreach (var l in ll)
            {
                sw.WriteLine(l.Ring.Time.ToString("H:mm") + "\t" +
                    l.TeacherForDiscipline.Discipline.Name + "\t" +
                    l.TeacherForDiscipline.Discipline.StudentGroup.Name + "\t" +
                    l.TeacherForDiscipline.Teacher.FIO + "\t" +
                    l.Auditorium.Name);
            }
            sw.Close();
        }

        private void AuditoriumCollisions(CancellationToken cToken)
        {
            var activeLessons = Repo.Lessons.GetAllActiveLessons();

            var sw = new StreamWriter("kaput.txt");
            foreach (var i in activeLessons)
            {
                foreach (var j in activeLessons)
                {
                    if ((i.Calendar.CalendarId == j.Calendar.CalendarId) && (i.Ring.RingId == j.Ring.RingId) && (i.Auditorium.AuditoriumId == j.Auditorium.AuditoriumId) &&
                        (i.LessonId != j.LessonId) && 
                        ((i.Auditorium.Name != "Ауд. 120") && (j.Auditorium.Name != "Ауд. 120")) &&
                        ((i.Auditorium.Name != "Ауд. САТД") && (j.Auditorium.Name != "Ауд. САТД")))
                    {
                        sw.WriteLine(
                            i.Calendar.Date.Date + "\t" + i.Ring.Time.TimeOfDay + "\t" +
                            i.Auditorium.Name + "\t" +
                            i.LessonId + "\t" +
                            i.TeacherForDiscipline.Discipline.Name + "\t" + i.TeacherForDiscipline.Discipline.StudentGroup.Name + "\t" + i.TeacherForDiscipline.Teacher.FIO + "\t" +
                            j.LessonId + "\t" +
                            j.TeacherForDiscipline.Discipline.Name + "\t" + j.TeacherForDiscipline.Discipline.StudentGroup.Name + "\t" + j.TeacherForDiscipline.Teacher.FIO);
                    }
                }

                cToken.ThrowIfCancellationRequested();
            }
            sw.Close();
        }

        private void АудиторииToolStripMenuItemClick(object sender, EventArgs e)
        {
            var audForm = new AuditoriumList(Repo);
            audForm.Show();
        }
       
        private void ДниСеместраToolStripMenuItemClick(object sender, EventArgs e)
        {
            var calendarForm = new CalendarList(Repo);
            calendarForm.Show();
        }

        private void ЗвонкиToolStripMenuItemClick(object sender, EventArgs e)
        {
            var ringForm = new RingList(Repo);
            ringForm.Show();
        }

        private void СтудентыToolStripMenuItemClick(object sender, EventArgs e)
        {
            var studentForm = new StudentList(Repo);
            studentForm.Show();
        }

        private void ГруппыToolStripMenuItemClick(object sender, EventArgs e)
        {
            var studentGroupForm = new StudentGroupList(Repo);
            studentGroupForm.Show();
        }

        private void ПреподавателиToolStripMenuItemClick(object sender, EventArgs e)
        {
            var teacherForm = new TeacherList(Repo);
            teacherForm.Show();
        }

        private void ДисциплиныToolStripMenuItemClick(object sender, EventArgs e)
        {
            var disciplineForm = new DisciplineList(Repo);
            disciplineForm.Show();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            var addLessonForm = new AddLesson(Repo);
            addLessonForm.Show();
        }

        private async void LoadToSiteClick(object sender, EventArgs e)
        {
            if (LoadToSite.Text == "Загрузить на сайт")
            {
                _cToken = _tokenSource.Token;

                LoadToSite.Text = "";
                LoadToSite.Image = Resources.Loading;

                var repo = Repo;
                var uploadDbPrefix = uploadPrefix.Text;
                var siteToUploadName = siteToUpload.Text;

                try
                {
                    await Task.Run(() => WnuUpload.UploadSchedule(repo, uploadDbPrefix, siteToUploadName, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            LoadToSite.Image = null;
            LoadToSite.Text = "Загрузить на сайт";
        }

        private void MainViewCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var source = (List<GroupTableView>)ScheduleView.DataSource;
            var time = source[e.RowIndex].Time;

            var editLessonForm = new EditLesson(Repo, (int)groupList.SelectedValue, e.ColumnIndex, time, showProposedLessons.Checked);
            editLessonForm.ShowDialog();
        }

        private void ОпцииToolStripMenuItemClick(object sender, EventArgs e)
        {
            var configOptionsForm = new ConfigOptionsList(Repo);
            configOptionsForm.Show();
        }

        private void NotEnoughClick(object sender, EventArgs e)
        {
            var stat = new Dictionary<int, int>();
            var result = new Dictionary<string, int>();
            var resByTeacher = new Dictionary<string, int>();

            foreach (var disc in Repo.Disciplines.GetAllDisciplines())
            {
                var disc1 = disc;
                var disctfd = Repo.TeacherForDisciplines.GetFiltredTeacherForDiscipline(tefd => tefd.Discipline.DisciplineId == disc1.DisciplineId).FirstOrDefault();
                if (disctfd == null)
                {
                    continue;
                }
                var tfd = disctfd;

                var tfdLessons = Repo.Lessons.GetFiltredLessons(l => (l.State == 1) && l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId).ToList();

                var hoursDiff = disc.AuditoriumHours - tfdLessons.Count * 2;

                result.Add(tfd.TeacherForDisciplineId + "\t" + tfd.Discipline.StudentGroup.Name +"\t" + disc.Name + "\t" + tfd.Teacher.FIO, hoursDiff);

                if (!resByTeacher.ContainsKey(tfd.Teacher.FIO))
                {
                    resByTeacher.Add(tfd.Teacher.FIO, 0);
                }

                resByTeacher[tfd.Teacher.FIO] += hoursDiff;

                if (!stat.ContainsKey(hoursDiff))
                {
                    stat.Add(hoursDiff, 0);
                }
                stat[hoursDiff]++;
            }

            var sr = new StreamWriter("stat.txt");
            foreach (var kvp in stat.OrderByDescending(kvp => kvp.Key))
            {
                sr.WriteLine(kvp.Key + " - " + kvp.Value);
            }
            sr.Close();

            var sr2 = new StreamWriter("stat2.txt");
            foreach (var kvp in result.Where(r => r.Value != 0).OrderByDescending(r => r.Value))
            {
                sr2.WriteLine(kvp.Key + "\t" + kvp.Value);
            }
            sr2.Close();

            var sr3 = new StreamWriter("statByTeacher.txt");
            foreach (var kvp in resByTeacher.Where(r => r.Value != 0).OrderByDescending(r => r.Value))
            {
                sr3.WriteLine(kvp.Key + "\t" + kvp.Value);
            }
            sr3.Close();
        }

        private async void AuditoriumKaputClick(object sender, EventArgs e)
        {
            if (auditoriumKaput.Text == "Коллизии аудиторий")
            {
                _cToken = _tokenSource.Token;

                auditoriumKaput.Text = "";
                auditoriumKaput.Image = Resources.Loading;
                
                try
                {
                    await Task.Run(() => AuditoriumCollisions(_cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            auditoriumKaput.Image = null;
            auditoriumKaput.Text = "Коллизии аудиторий";
        }

        private void ФакультетыгруппыToolStripMenuItemClick(object sender, EventArgs e)
        {
            var facultyListForm = new FacultyList(Repo);
            facultyListForm.Show();
        }

        private async void ActiveLessonsCount_Click(object sender, EventArgs e)
        {
            if (ActiveLessonsCount.Text == "%")
            {
                _cToken = _tokenSource.Token;

                ActiveLessonsCount.Text = "X";

                var repo = Repo;
                var uploadDbPrefix = uploadPrefix.Text;

                try
                {
                    await Task.Run(() => PlanCompletionPercentage(_cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            ActiveLessonsCount.Text = "%";
        }

        private void PlanCompletionPercentage(CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            var allDiscLessonCount = (int) Math.Ceiling((double)Repo.Disciplines.GetAllDisciplines().Where(d => !d.Name.Contains("я куль")).Select(d => d.AuditoriumHours).Sum() / 2);
            var activeLessonsCount = Repo.Lessons.GetAllActiveLessons().Count(l => !l.TeacherForDiscipline.Discipline.Name.Contains("я куль"));

            var diff = allDiscLessonCount - activeLessonsCount;
            String message = activeLessonsCount + " (" +
                             $"{(double) activeLessonsCount*100/allDiscLessonCount:0.00}%" + ") / " +
                             allDiscLessonCount
                             + " =>  " + diff + " (" +
                             $"{(double) diff*100/allDiscLessonCount:0.00}%" + ")";

            cToken.ThrowIfCancellationRequested();

            var discCount = Repo.Disciplines.GetAllDisciplines().Count;
            var touchedDiscs = Repo
                .Disciplines
                .GetFiltredDisciplines(d =>
                    (d.AuditoriumHours == 0) ||
                    (Repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(
                        tfd => tfd.Discipline.DisciplineId == d.DisciplineId) == null) ||
                    (Repo.CommonFunctions.GetTfdHours(
                        Repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(
                            tfd => tfd.Discipline.DisciplineId == d.DisciplineId).TeacherForDisciplineId) != 0))
                
                .Count;
            var diff2 = discCount - touchedDiscs;

            cToken.ThrowIfCancellationRequested();

            message += Environment.NewLine + touchedDiscs + " (" +
                       $"{(double) touchedDiscs*100/discCount:0.00}%" + ") / " + discCount
                       + " =>  " + diff2 + " (" + $"{(double) diff2*100/discCount:0.00}%" + ")";

            MessageBox.Show(message, "В парах / В дисциплинах");
        }

        private void ManyGroups_Click(object sender, EventArgs e)
        {
            var manyGroupsForm = new MultipleView(Repo);
            manyGroupsForm.Show();
        }

        private void занятостьАудиторийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var audEventsForm = new AuditoriumEventsList(Repo);
            audEventsForm.Show();
        }

        private void teachersHours_Click(object sender, EventArgs e)
        {
            var teacherHoursForm = new TeacherHours(Repo);
            teacherHoursForm.Show();
        }

        private void oneAuditorium_Click(object sender, EventArgs e)
        {
            var oneAudForm = new OneAuditorium(Repo);
            oneAudForm.Show();
        }

        private void auditoriums_Click(object sender, EventArgs e)
        {
            var audsForm = new Auditoriums(Repo);
            audsForm.Show();
        }

        private void allChanges_Click(object sender, EventArgs e)
        {
            var allChangesForm = new AllChanges(Repo);
            allChangesForm.Show();
        }

        private void scheduleHours_Click(object sender, EventArgs e)
        {
            var result = new Dictionary<DateTime, int>();

            var curDate = new DateTime(2014, 1, 13);

            int lessonsInSchedule = 0;

            do
            {
                int delta = 0;
                var evts = Repo.LessonLogEvents.GetFiltredLessonLogEvents(evt => evt.DateTime.Date == curDate.Date);
                foreach (var ev in evts)
                {
                    if ((ev.OldLesson == null) && (ev.NewLesson != null))
                    {
                        delta++;
                    }
                    if ((ev.OldLesson != null) && (ev.NewLesson == null))
                    {
                        delta--;
                    }
                }
                lessonsInSchedule += delta;

                result.Add(curDate.Date, lessonsInSchedule);

                curDate = curDate.AddDays(1);
            } while (curDate <= DateTime.Now.Date.Date);

            var sw = new StreamWriter("LessonsByDay.txt");
            foreach (var r in result)
            {
                sw.WriteLine(r.Key.ToString("dd.MM.yyyy") + "\t" + r.Value);
            }
            sw.Close();
        }

        private void dayDelta_Click(object sender, EventArgs e)
        {
            Task.Run(() => { 
                        
                               // facultyId + DOW
                               var result = new List<Tuple<int, DayOfWeek>>();

                               var evts = Repo.LessonLogEvents.GetFiltredLessonLogEvents(lle => lle.DateTime.Date == DateTime.Now.Date);

                               var fg = Repo.GroupsInFaculties.GetAllGroupsInFaculty()
                                   .GroupBy(gif => gif.Faculty.FacultyId, 
                                       gif => gif.StudentGroup.StudentGroupId)
                                   .ToList();

            
                               foreach (var ev in evts)
                               {   
                                   int studentGroupId;
                                   if (ev.OldLesson != null)
                                   {
                                       studentGroupId = ev.OldLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;

                                       var studentIds = Repo
                                           .StudentsInGroups
                                           .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroupId)
                                           .Select(sig => sig.Student.StudentId)
                                           .ToList();

                                       var facultyScheduleChanged = (
                                           from faculty in fg
                                           where Repo
                                               .StudentsInGroups
                                               .GetFiltredStudentsInGroups(sig => 
                                                   studentIds.Contains(sig.Student.StudentId) &&
                                                   faculty.Contains(sig.StudentGroup.StudentGroupId)).Any()
                                           select faculty.Key)
                                           .ToList();

                                       var localEvent = ev;
                                       foreach (var dowFacTuple in facultyScheduleChanged
                                           .Select(faculty => Tuple.Create(faculty, localEvent.OldLesson.Calendar.Date.DayOfWeek))
                                           .Where(dowFacTuple => !result.Contains(dowFacTuple)))
                                       {
                                           result.Add(dowFacTuple);
                                       }
                                   }

                

                                   if (ev.NewLesson != null)
                                   {
                                       studentGroupId = ev.NewLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;

                                       var studentIds = Repo
                                           .StudentsInGroups
                                           .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroupId)
                                           .Select(sig => sig.Student.StudentId)
                                           .ToList();

                                       var facultyScheduleChanged = (
                                           from faculty in fg
                                           where Repo
                                               .StudentsInGroups
                                               .GetFiltredStudentsInGroups(sig => 
                                                   studentIds.Contains(sig.Student.StudentId) &&
                                                   faculty.Contains(sig.StudentGroup.StudentGroupId)).Any()
                                           select faculty.Key)
                                           .ToList();

                                       var localEvent = ev;
                                       foreach (var dowFacTuple in facultyScheduleChanged
                                           .Select(faculty => Tuple.Create(faculty, localEvent.NewLesson.Calendar.Date.DayOfWeek))
                                           .Where(dowFacTuple => !result.Contains(dowFacTuple)))
                                       {
                                           result.Add(dowFacTuple);
                                       }
                                   }                
                               }

                               var messageString = result
                                   .OrderBy(df => df.Item1)
                                   .ThenBy(df => df.Item2)
                                   .Aggregate("", (current, dowFac) => 
                                       current + (Repo.Faculties.GetFaculty(dowFac.Item1).Letter + " - " + 
                                                  Constants.DowLocal[Constants.DowRemap[(int) dowFac.Item2]] + Environment.NewLine));

                               MessageBox.Show(messageString, "Изменения на сегодня");
            });
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            UpdateViewWidth();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                UpdateViewWidth();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void excelExport_Click(object sender, EventArgs e)
        {
            var sr = new StreamWriter("ExcelData.txt");

            var lessons = Repo
                .Lessons
                .GetAllActiveLessons()
                .OrderBy(l => l.Calendar.Date.Date)
                .ThenBy(l => l.Ring.Time.TimeOfDay)
                .ThenBy(l => l.Auditorium.Name)                
                .ToList();

            foreach (var lesson in lessons)
            {
                sr.WriteLine(
                    lesson.Calendar.Date.Date.ToString("dd.MM.yyyy") + '\t' + 
                    lesson.Ring.Time + '\t' + 
                    lesson.TeacherForDiscipline.Discipline.Name + '\t' + 
                    lesson.TeacherForDiscipline.Teacher.FIO + '\t' +
                    lesson.TeacherForDiscipline.Discipline.StudentGroup.Name + '\t' + 
                    lesson.Auditorium.Name);
            }

            sr.Close();

            Process.Start("ExcelData.txt");            
        }

        private void LessonListByTFD_Click(object sender, EventArgs e)
        {
            var lessonListByTfdForm = new LessonListByTfd(Repo);
            lessonListByTfdForm.Show();
        }

        private void LessonListByTeacher_Click(object sender, EventArgs e)
        {
            var lessonListByTeacherForm = new LessonListByTeacher(Repo);
            lessonListByTeacherForm.Show();
        }
        
        private void setLayout_Click(object sender, EventArgs e)
        {
            var width = Screen.PrimaryScreen.WorkingArea.Width;
            var height = Screen.PrimaryScreen.WorkingArea.Height;

            Top = 0;
            Left = 0;
            Width = width / 2;
            Height = height / 2;

            var allAudsForm = new Auditoriums(Repo);
            allAudsForm.Show();
            allAudsForm.Left = 0;
            allAudsForm.Top = height / 2;
            allAudsForm.Width = width / 2;
            allAudsForm.Height = height / 2;

            var discListForm = new DisciplineList(Repo);
            discListForm.Show();
            discListForm.Left = width / 2;
            discListForm.Top = 0;
            discListForm.Width = width / 2;
            discListForm.Height = height / 2;

            var teachersSchedule = new TeacherSchedule(Repo);
            teachersSchedule.Show();
            teachersSchedule.Left = width / 2;
            teachersSchedule.Top = height / 2;
            teachersSchedule.Width = width / 2;
            teachersSchedule.Height = height / 2;
        }

        private void setLayout2_Click(object sender, EventArgs e)
        {
            var width = Screen.PrimaryScreen.WorkingArea.Width;
            var height = Screen.PrimaryScreen.WorkingArea.Height;

            var disciplineForm = new DisciplineList(Repo);
            disciplineForm.Show();
            disciplineForm.Top = 0;
            disciplineForm.Left = width / 2;
            disciplineForm.Width = width / 2;
            disciplineForm.Height = (int) Math.Round(height * 0.42);
        }

        private async void CreatePDF_Click(object sender, EventArgs e)
        {
            if (CreatePDF.Text == "PDF")
            {
                _cToken = _tokenSource.Token;

                CreatePDF.Text = "";
                CreatePDF.Image = Resources.Loading;

                var facultyId = (int)FacultyList.SelectedValue;
                var facultyName = Repo.Faculties.GetFaculty(facultyId).Name;
                var ruDow = DOWList.SelectedIndex + 1;

                try
                {
                    await Task.Run(() => PdfPageExport(facultyId, ruDow, facultyName, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            CreatePDF.Image = null;
            CreatePDF.Text = "PDF";
        }

        private void PdfPageExport(int facultyId, int ruDow, string facultyName, CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            var facultyDowLessons = Repo.Lessons
                .GetFacultyDowSchedule(facultyId, ruDow, false, -1, false, false);

            cToken.ThrowIfCancellationRequested();

            PdfExport.ExportSchedulePage(facultyDowLessons, facultyName, "Export.pdf",
                DOWList.Text, Repo, true, false, false);

            cToken.ThrowIfCancellationRequested();

            Process.Start("Export.pdf");
        }

        private void AllInPDF_Click(object sender, EventArgs e)
        {            
            //PDFExport.ExportWholeSchedule("Export.pdf", _repo, false, false, false);

            PdfExport.PrintWholeSchedule(Repo);
        }

        private void BackupAndUpload_Click(object sender, EventArgs e)
        {
            var dbName = Repo.ExtractDbName(Repo.GetConnectionString());

            Repo.BackupDb(Application.StartupPath + "\\" + dbName + ".bak");
            WnuUpload.UploadFile(Application.StartupPath + "\\" + dbName + ".bak", "httpdocs/upload/DB-Backup/" + dbName + ".bak");
        }

        private void DownloadAndRestore_Click(object sender, EventArgs e)
        {
            var wc = new WebClient();
            wc.DownloadFile("http://wiki.nayanova.edu/upload/DB-Backup/" + FromDBName.Text + ".bak", Application.StartupPath + "\\" + ToDBName.Text + ".bak");
            Repo.RestoreDb(ToDBName.Text, Application.StartupPath + "\\" + ToDBName.Text + ".bak");
        }

        private void WholeScheduleDatesExport_Click(object sender, EventArgs e)
        {
            ExportWholeScheduleDates("ScheduleDates.txt");
        }

        private void ExportWholeScheduleDates(string filename)
        {
            var groups = Repo
                .StudentGroups
                .GetFiltredStudentGroups(sg => !(sg.Name.Contains("-") || sg.Name.Contains("+") || sg.Name.Contains("I") || sg.Name.Length == 1 || sg.Name.Contains("(Н)") || sg.Name.Contains(".")))
                .ToList();

            foreach (var group in groups)
            {
                var sw = new StreamWriter(filename, true);
                sw.WriteLine(group.Name);
                sw.Close();


                var studentIds = Repo
                    .StudentsInGroups
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                    .Select(sig => sig.Student.StudentId)
                    .ToList();
                var groupIds = Repo
                    .StudentsInGroups
                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .Select(sig => sig.StudentGroup.StudentGroupId)
                    .Distinct()
                    .ToList();

                var tfds = Repo
                    .TeacherForDisciplines
                    .GetFiltredTeacherForDiscipline(tfd => groupIds.Contains(tfd.Discipline.StudentGroup.StudentGroupId))
                    .ToList();

                foreach (var tfd in tfds)
                {
                    var lessons = Repo
                        .Lessons
                        .GetFiltredLessons(l =>
                        (l.State == 1) &&
                        l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId)
                        .OrderBy(l => l.Calendar.Date.Date)
                        .ToList();

                    if (lessons.Count == 0)
                    {
                        continue;
                    }

                    var discipline = new StringBuilder();

                    discipline.Append(tfd.Discipline.StudentGroup.Name + '\t');
                    discipline.Append(tfd.Discipline.Name + '\t');
                    discipline.Append(tfd.Teacher.FIO + '\t');

                    foreach (var lesson in lessons)
                    {
                        discipline.Append(lesson.Calendar.Date.ToString("dd.MM.yyyy") + '\t');
                    }

                    sw = new StreamWriter(filename, true);
                    sw.WriteLine(discipline);
                    sw.Close();
                }

                sw = new StreamWriter(filename, true);
                sw.WriteLine();
                sw.Close();
                
            }
        }

        private void ExportInWord_Click(object sender, EventArgs e)
        {
            if (WordOneFaculty.Checked)
            {
                WordExport.ExportWholeSchedule(Repo, "Расписание.docx", false, false, 90, (int)WordFacultyFilter.SelectedValue, 6, SchoolHeader, OnlyFutureDatesExportInWord.Checked);
            }
            else
            {
                WordExport.ExportWholeSchedule(Repo, "Расписание.docx", false, false, 90, -1, 6, SchoolHeader, OnlyFutureDatesExportInWord.Checked);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (WordOneFaculty.Checked)
            {
                WordExport.ExportWholeSchedule(Repo, "Расписание.docx", false, false, cb90.Checked ? 90 : 80, (int)WordFacultyFilter.SelectedValue, 6, SchoolHeader, OnlyFutureDatesExportInWord.Checked);
            }
            else
            {
                WordExport.ExportWholeSchedule(Repo, "Расписание.docx", false, false, cb90.Checked ? 90 : 80, -1, 6, SchoolHeader, OnlyFutureDatesExportInWord.Checked);
            }
        }
                

        private void AuditoriumPercentage_Click(object sender, EventArgs e)
        {
            var sw = new StreamWriter("AuditoriumPercentage.txt");
            sw.Close();

            var activeLessons = Repo.Lessons.GetFiltredLessons(l => (l.State == 1) && l.Ring.RingId <= 8);

            WriteAuditoriumPercentageToFile(activeLessons, "AuditoriumPercentage.txt");

            activeLessons = Repo.Lessons.GetFiltredLessons(l => (l.State == 1) && l.Ring.RingId <= 8 && (l.Auditorium.Building.BuildingId == 2));

            WriteAuditoriumPercentageToFile(activeLessons, "AuditoriumPercentage.txt");

            activeLessons = Repo.Lessons.GetFiltredLessons(l => (l.State == 1) && l.Ring.RingId <= 8 && (l.Auditorium.Building.BuildingId == 3));

            WriteAuditoriumPercentageToFile(activeLessons, "AuditoriumPercentage.txt");
        }

        private void WriteAuditoriumPercentageToFile(IEnumerable<Lesson> activeLessons, string filename)
        {
            //                          dow(1-7)        time    lessonCount
            var result = new Dictionary<int, Dictionary<string, int>>();

            var rings = Repo.Rings.GetAllRings().Where(r => r.RingId <= 8).OrderBy(r => r.Time).ToList();
            var ringsCount = rings.Count();

            for (int i = 1; i <= 7; i++)
            {
                result.Add(i, new Dictionary<string, int>());

                for (int j = 0; j < ringsCount; j++)
                {
                    result[i].Add(rings[j].Time.ToString("H:mm"), 0);
                }
            }


            foreach (var lesson in activeLessons)
            {
                var lessonDow = ((int)lesson.Calendar.Date.DayOfWeek == 0) ? 7 : (int)lesson.Calendar.Date.DayOfWeek;

                var lessonTime = lesson.Ring.Time.ToString("H:mm");

                result[lessonDow][lessonTime]++;
            }

            var sw = new StreamWriter(filename, true);

            sw.Write("День недели\t");
            foreach (var ring in rings)
            {
                sw.Write(ring.Time.ToString("H:mm") + "\t");
            }
            sw.WriteLine();

            for (int i = 1; i <= 7; i++)
            {
                sw.Write(Constants.DowLocal[i] + "\t");
                foreach (var ring in rings)
                {
                    sw.Write(result[i][ring.Time.ToString("H:mm")] + "\t");
                }
                sw.WriteLine();
            }

            sw.WriteLine();

            sw.Close();
        }

        private async void WordExport_Click(object sender, EventArgs e)
        {
            if (WordExportButton.Text == "Word")
            {
                _cToken = _tokenSource.Token;

                WordExportButton.Text = "";
                WordExportButton.Image = Resources.Loading;

                var repo = Repo;
                var length80Or90 = cb90.Checked ? 90 : 80;
                var facultyId = (int)FacultyList.SelectedValue;
                var ruDow = DOWList.SelectedIndex + 1;
                var wordWeekFiltered = wordExportWeekFiltered.Checked;
                int weekFilter;
                int.TryParse(WordExportWeekFilter.Text, out weekFilter);
                var onlyFutureDates = OnlyFutureDatesExportInWord.Checked;

                try
                {
                    await Task.Run(() => WordExport.ExportSchedulePage(
                    repo, "Расписание.docx", false, false, length80Or90, facultyId, ruDow, 6,
                    wordWeekFiltered, weekFilter, !wordWeekFiltered, onlyFutureDates, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            WordExportButton.Image = null;
            WordExportButton.Text = "Word";
        }

        private void WordCustom_Click(object sender, EventArgs e)
        {
            var wordCustomForm = new WordExportForm(Repo);
            wordCustomForm.Show();
        }

        private void TwoDaysWord_Click(object sender, EventArgs e)
        {
            var facultyId = (int)FacultyList.SelectedValue;
            var ruDow = DOWList.SelectedIndex + 1;
            int weekFilter;
            int.TryParse(WordExportWeekFilter.Text, out weekFilter);

            WordExport.ExportTwoSchedulePages(
                Repo, "Расписание.docx", false, false, 80, facultyId, ruDow, 6,
                wordExportWeekFiltered.Checked, weekFilter, !wordExportWeekFiltered.Checked);
        }

        private void FacultyTwoDaysInList_Click(object sender, EventArgs e)
        {
            var facultyId = (int)WordFacultyFilter.SelectedValue;
            int weekFilter;
            int.TryParse(WordExportWeekFilter.Text, out weekFilter);

            WordExport.ExportTwoDaysInPageFacultySchedule(
                Repo, "Расписание.docx", false, false, 80, facultyId, 6,
                wordExportWeekFiltered.Checked, weekFilter, !wordExportWeekFiltered.Checked);
        }

        private void BIGREDBUTTON_Click(object sender, EventArgs e)
        {
            /*
            Task.Run(() =>
            {


                var facIds = new List<int> {1, 2, 3, 4, 5, 9, 10, 11};

                var faculties = Repo.Faculties.GetFiltredFaculties(f => facIds.Contains(f.SortingOrder));

                for (int i = 0; i < faculties.Count; i++)
                {
                    var faculty = faculties[i];

                    

                    var facultyGroups =
                        Repo.GroupsInFaculties.GetFiltredGroupsInFaculty(
                            gif => gif.Faculty.FacultyId == faculty.FacultyId)
                            .Select(gif => gif.StudentGroup);

                    foreach (var facultyGroup in facultyGroups)
                    {

                        var studentIds = Repo
                                .StudentsInGroups
                                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == facultyGroup.StudentGroupId)
                                .Select(stig => stig.Student.StudentId)
                                .ToList();
                        var groupsListIds = Repo
                            .StudentsInGroups
                            .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                            .Select(stig => stig.StudentGroup.StudentGroupId)
                            .Distinct()
                            .ToList();

                        var discList = Repo.Disciplines.GetFiltredDisciplines(
                            d => groupsListIds.Contains(d.StudentGroup.StudentGroupId) && 
                            ((d.Attestation == 1) || (d.Attestation == 3) || (d.Attestation == 4)))
                            .ToList();

                        foreach (var discipline in discList)
                        {
                            var lastLesson =
                                Repo.Lessons.GetFiltredLessons(
                                    l =>
                                        (l.State == 1) &&
                                        (l.TeacherForDiscipline.Discipline.DisciplineId == discipline.DisciplineId))
                                    .OrderBy(l => l.Calendar.Date).LastOrDefault();
                            if (lastLesson != null)
                            {
                                var sw = new StreamWriter("ZachDates.txt", true);
                                sw.WriteLine(facultyGroup.Name + "\t" + 
                                    discipline.Name + "\t" +
                                    lastLesson.Calendar.Date.Date.ToString("dd.MM.yyyy"));
                                sw.Close();
                            }
                        }
                    }
                }

                var sw2 = new StreamWriter("ZachDates.txt", true);
                sw2.WriteLine("done");
                sw2.Close();
                var eprst = 999;
            });*/

            /*
            Task.Run(() =>
            {
                var lessons =
                    Repo.Lessons.GetFiltredLessons(
                        l => l.State == 1 && (l.Calendar.Date.Date == new DateTime(2015, 12, 03)));
                var sw = new StreamWriter("3dec.txt");
                foreach (var lesson in lessons)
                {
                    sw.WriteLine(lesson.TeacherForDiscipline.Teacher.FIO + "\t" + 
                        lesson.TeacherForDiscipline.Discipline.StudentGroup.Name + "\t" + 
                        lesson.TeacherForDiscipline.Discipline.Name + "\t" + 
                        lesson.Ring.Time.ToString("HH:mm") + "\t" + 
                        lesson.Auditorium.Name);
                }
                sw.Close();
            });*/
            

            Task.Run(() =>
            {
                var lastTwoDaysLessons =
                    Repo.Lessons.GetFiltredLessons(
                        l =>
                            l.State == 1 &&
                            ((l.Calendar.Date.Date == new DateTime(2015, 12, 29)) ||
                             (l.Calendar.Date.Date == new DateTime(2015, 12, 30))));

                StreamWriter sw;

                foreach (Lesson lesson in lastTwoDaysLessons)
                {
                    var tfd = lesson.TeacherForDiscipline;
                    if ((tfd.Discipline.Attestation == 1) || (tfd.Discipline.Attestation == 3) ||
                        (tfd.Discipline.Attestation == 4))
                    {
                        sw = new StreamWriter("zach.txt", true);
                        sw.WriteLine(
                            tfd.Teacher.FIO + "\t" + 
                            tfd.Discipline.StudentGroup.Name + "\t" + 
                            tfd.Discipline.Name + "\t" + 
                            lesson.Calendar.Date.ToString("dd.MM.yyyy"));
                        sw.Close();
                    }
                }

                sw = new StreamWriter("zach.txt", true);
                sw.WriteLine("done");
                sw.Close();
            });

            /*
            var lle = Repo.LessonLogEvents.GetAllLessonLogEvents().ToList();

            var sw = new StreamWriter("AudPercentage.txt");

            sw.WriteLine("buildingId = 2");
            WriteBuildingAuditoriumsBusyPercentage(sw, 2);
            sw.WriteLine();

            sw.WriteLine("buildingId = 3");
            WriteBuildingAuditoriumsBusyPercentage(sw, 3);
            sw.Close();
            var eprst = 999;
            */

            // var hours = Repo.Disciplines.GetAllDisciplines().Select(d => d.AuditoriumHours).Sum();
            // MessageBox.Show(hours.ToString(), "Всего часов", MessageBoxButtons.OK);



            //dayDelta_Click(sender, e);

            // Oops
            // ExportStudentsData("StudentsExport-1sem.txt");
            // ImportStudentData("StudentsExport-1sem.txt");
            // CopyINOGroupLessonsFromRealSchedule();
            // ExportScheduleDates("Oops\\stat.txt");
            // ExportFacultyGroups();
            // ExportDiscAuds("Auds.txt");
            // ExportGroupDisciplines("Oops\\Discs.txt");
            // dayDelta_Click(sender, e);            
            // setLayout_Click(sender, e);
            // ExportGroupDisciplines("Oops\\Discs.txt");

            /*
            foreach (var disc in Repo.GetAllDisciplines())
            {
                if (disc.Name.EndsWith("\t"))
                {
                    disc.Name = disc.Name.Substring(0, disc.Name.Length - 1);
                    Repo.UpdateDiscipline(disc);
                }
            }*/

            //Task.Run(new Action(() => DetectWindows("windows.txt")));            
        }

        private void WriteBuildingAuditoriumsBusyPercentage(StreamWriter sw, int buidingId)
        {
            var lessonsBuilding2 = Repo.Lessons.GetFiltredLessons(l => l.Auditorium.Building.BuildingId == buidingId && l.State == 1);
            var times = lessonsBuilding2.Select(l => l.Ring.Time).OrderBy(t => t.TimeOfDay).Distinct().ToList();
            var group = lessonsBuilding2
                .GroupBy(l => (int) l.Calendar.Date.DayOfWeek)
                .ToDictionary(
                    l => l.Key,
                    l2 => l2.GroupBy(l3 => l3.Ring.Time)
                        .ToDictionary(l4 => l4.Key,
                            l5 => l5))
                .OrderBy(tl => tl.Key)
                .OrderBy(ll => ll.Key)
                .ToList();
            sw.Write("Время\t");

            foreach (var time in times)
            {
                sw.Write(time.ToString("HH:mm") + "\t");
            }
            sw.WriteLine();

            foreach (var dow in @group)
            {
                sw.Write(dow.Key + "\t");

                foreach (var time in times)
                {
                    if (dow.Value.ContainsKey(time))
                    {
                        sw.Write(dow.Value[time].Count() + "\t");
                    }
                    else
                    {
                        sw.Write("0\t");
                    }
                }
                sw.WriteLine();
            }
        }

        private void DetectWindows(string filename)
        {
            foreach (var student in Repo.Students.GetFiltredStudents(s => !s.Expelled).OrderBy(s => s.F).ThenBy(s => s.I))
            {
                var calendars = Repo.Calendars.GetFiltredCalendars(c => c.Date.Date >= DateTime.Now.Date);

                var studentGroupIds = Repo
                    .StudentsInGroups
                    .GetFiltredStudentsInGroups(sig => sig.Student.StudentId == student.StudentId)
                    .Select(sig => sig.StudentGroup.StudentGroupId)
                    .ToList();

                if (studentGroupIds.Count != 0)
                {

                    foreach (var calendar in calendars)
                    {
                        var studentLessons = Repo
                            .Lessons
                            .GetFiltredLessons(l => 
                                studentGroupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId) &&
                                l.Calendar.CalendarId == calendar.CalendarId &&
                                l.State == 1)
                            .OrderBy(l => l.Ring.Time.TimeOfDay)
                            .ToList();

                        for (int i = 1; i < studentLessons.Count(); i++)
                        {
                            if ((studentLessons[i].Ring.Time.TimeOfDay - studentLessons[i - 1].Ring.Time.TimeOfDay) > TimeSpan.FromMinutes(120))
                            {
                                LogInFile(filename, "Студент " + student.F + " " + student.I + " " + student.O + " - " +
                                    calendar.Date.ToString("dd.MM.yyyy") + " " + 
                                    studentLessons[i - 1].Ring.Time.ToString("H:mm") + " (" + studentLessons[i - 1].TeacherForDiscipline.Discipline.Name + ") " +
                                    studentLessons[i].Ring.Time.ToString("H:mm") + " (" + studentLessons[i].TeacherForDiscipline.Discipline.Name + ") "
                                );
                            }
                        }
                    }
                }
            }
        }

        public void LogInFile(string filename, string message)
        {
            var sw = new StreamWriter(filename, true);
            sw.WriteLine(message);
            sw.Close();
        }

        private async void WordSchool_Click_1(object sender, EventArgs e)
        {
            if (WordSchool.Text == "Word (ШКОЛА)")
            {
                _cToken = _tokenSource.Token;

                WordSchool.Text = "";
                WordSchool.Image = Resources.Loading;

                var facultyId = (int)FacultyList.SelectedValue;
                var ruDow = DOWList.SelectedIndex + 1;
                int weekFilter;
                int.TryParse(WordExportWeekFilter.Text, out weekFilter);
                var wordWeekFiltered = wordExportWeekFiltered.Checked;

                try
                {
                    await Task.Run(() => WordExport.WordSchool(
                            Repo, "Расписание.docx", false, false, 80, facultyId, ruDow, 6,
                            wordWeekFiltered, weekFilter, !wordWeekFiltered, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            WordSchool.Image = null;
            WordSchool.Text = "Word (ШКОЛА)";
        }

        private async void WordSchool2_Click(object sender, EventArgs e)
        {
            if (WordSchool2.Text == "Word (ШКОЛА) 2 дн.")
            {
                _cToken = _tokenSource.Token;

                WordSchool2.Text = "";
                WordSchool2.Image = Resources.Loading;

                var facultyId = (int)FacultyList.SelectedValue;
                var ruDow = DOWList.SelectedIndex + 1;
                int weekFilter;
                int.TryParse(WordExportWeekFilter.Text, out weekFilter);
                var wordWeekFiltered = wordExportWeekFiltered.Checked;

                try
                {
                    await Task.Run(() => WordExport.WordSchoolTwoDays(
                                Repo, "Расписание.docx", false, false, 80, facultyId, ruDow, 6,
                                wordWeekFiltered, weekFilter, !wordWeekFiltered, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            WordSchool2.Image = null;
            WordSchool2.Text = "Word (ШКОЛА) 2 дн.";
        }

        private void happyBirthday_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var message = Repo
                    .Students
                    .GetFiltredStudents(s => !s.Expelled)
                    .Where(student => DateTime.Now.Date == student.BirthDate.Date)
                    .Aggregate("", (current, student) =>
                        current + (student.F + " " + student.I + " " + student.O +
                                   " ( " + (DateTime.Now.Year - student.BirthDate.Year) + " / " +
                                   student.BirthDate.Year + " )" + Environment.NewLine));

                if (message == "")
                {
                    message = "Нету дома никого.";
                }

                MessageBox.Show(message, "Happy");
            });
        }

        private async void OnePageGroupScheduleWordExport_Click(object sender, EventArgs e)
        {
            var onlyFutureDatesF = OnlyFutureDatesExportInWord.Checked;
            var weekFilteredF = weekFiltered.Checked;
            var weekFilterNum = 0;
            int.TryParse(WeekFilter.Text, out weekFilterNum);

            if (OnePageGroupScheduleWordExport.Text == "Экспорт в Word - одна группа")
            {
                _cToken = _tokenSource.Token;

                OnePageGroupScheduleWordExport.Text = "";
                OnePageGroupScheduleWordExport.Image = Resources.Loading;

                var groupId = (int)groupList.SelectedValue;

                try
                {
                    await
                        Task.Run(
                            () =>
                                WordExport.ExportGroupSchedulePage(Repo, this, groupId, weekFilteredF, weekFilterNum,
                                    onlyFutureDatesF, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception exc)
                {
                    var eprst = 999;
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            OnePageGroupScheduleWordExport.Image = null;
            OnePageGroupScheduleWordExport.Text = "Экспорт в Word - одна группа";
        }

        private async void WordWholeScheduleOneGroupOnePage_Click(object sender, EventArgs e)
        {
            if (WordWholeScheduleOneGroupOnePage.Text == "Всё расписание в Word 1 группа на 1 стр.")
            {
                _cToken = _tokenSource.Token;

                WordWholeScheduleOneGroupOnePage.Text = "";
                WordWholeScheduleOneGroupOnePage.Image = Resources.Loading;
                
                try
                {
                    await Task.Run(() => WordExport.ExportWholeScheduleOneGroupPerPage(Repo, this, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            WordWholeScheduleOneGroupOnePage.Image = null;
            WordWholeScheduleOneGroupOnePage.Text = "Всё расписание в Word 1 группа на 1 стр.";
        }

        private void BackupUpload_Click(object sender, EventArgs e)
        {
            string dbName = (FromDBName.Text == "") ? Repo.ExtractDbName(Repo.GetConnectionString()) : FromDBName.Text;
            dbName = StartupForm.School ? "s_" : "" + dbName;

            string toDbName = (ToDBName.Text == "") ? Repo.ExtractDbName(Repo.GetConnectionString()) : ToDBName.Text;
            toDbName = StartupForm.School ? "s_" : "" + toDbName;
            
            Repo.BackupDb(Application.StartupPath + "\\" + dbName + ".bak");
            WnuUpload.UploadFile(Application.StartupPath + "\\" + dbName + ".bak", "httpdocs/upload/DB-Backup/" + toDbName + ".bak");
        }

        private async void startSchoolWordExport_Click(object sender, EventArgs e)
        {
            if (startSchoolWordExport.Text == "Word (1-7)")
            {
                _cToken = _tokenSource.Token;

                startSchoolWordExport.Text = "";
                startSchoolWordExport.Image = Resources.Loading;

                var facultyId = (int)FacultyList.SelectedValue;
                var ruDow = DOWList.SelectedIndex + 1;
                int weekFilter;
                int.TryParse(WordExportWeekFilter.Text, out weekFilter);
                var wordWeekFiltered = wordExportWeekFiltered.Checked;

                try
                {
                    await Task.Run(() => WordExport.WordStartSchool(
                            Repo, "Расписание.docx", false, false, 40, facultyId, ruDow, 6,
                            wordWeekFiltered, weekFilter, !wordWeekFiltered, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            startSchoolWordExport.Image = null;
            startSchoolWordExport.Text = "Word (1-7)";
        }

        private void корпусаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var buildingsForm = new BuildingList(Repo);
            buildingsForm.Show();
        }

        private void пожеланияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wishesForm = new Wishes(Repo);
            wishesForm.Show();

        }

        private void аудиторииДисциплинToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var discsAudsForm = new DisciplinesAuditoriums(Repo);
            discsAudsForm.Show();
        }

        private void нельзяСтавитьПоследнимУрокомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nottheLastLessonForm = new LastLesson(Repo);
            nottheLastLessonForm.Show();
        }

        private void парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var incompatiblePairsForm = new IncompatiblePairs(Repo);
            incompatiblePairsForm.Show();
        }

        private void дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var doubledLessonsForm = new DoubledLessons(Repo);
            doubledLessonsForm.Show();
        }

        private void порядокПостановкиДисциплинВРасписаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var disciplineByOrderForm = new DisciplineByOrder(Repo);
            disciplineByOrderForm.Show();
        }

        private void analyse_Click(object sender, EventArgs e)
        {
            var analysisForm = new Forms.Analysis.Analysis(Repo);
            analysisForm.Show();
        }

        private void analyseSchool_Click(object sender, EventArgs e)
        {
            var analysisSchoolForm = new AnalysisSchool(Repo);
            analysisSchoolForm.Show();
        }

        private void removeAllProposedLessons_Click(object sender, EventArgs e)
        {
            if (removeAllProposedLessons.Text == "Удалить все преполагаемые уроки")
            {
                _cToken = _tokenSource.Token;

                removeAllProposedLessons.Text = "";
                removeAllProposedLessons.Image = Resources.Loading;
                
                try
                {
                    Task.Run(() =>
                    {
                        var proposedLessonsIds = Repo
                            .Lessons
                            .GetFiltredLessons(l => l.State == 2)
                            .Select(l => l.LessonId)
                            .ToList();

                        foreach (var lessonId in proposedLessonsIds)
                        {
                            Repo.Lessons.RemoveLesson(lessonId);
                            _cToken.ThrowIfCancellationRequested();
                        }
                    }, _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            removeAllProposedLessons.Image = null;
            removeAllProposedLessons.Text = "Удалить все преполагаемые уроки";
        }

        private void периодыГруппToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var groupPeriodsForm = new GroupPeriods(Repo);
            groupPeriodsForm.Show();
        }

        private void корпусИПреимущественнаяАудиторияГруппыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var groupBuildingAuditoriumForm = new StudentGroupAttributes(Repo);
            groupBuildingAuditoriumForm.Show();
        }

        private void дисциплиныСГарантированнойНаружнейАудиториейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var disciplinesWithAudForm = new DisciplinesWithAud(Repo);
            disciplinesWithAudForm.Show();
        }

        private void сменыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var shiftsForm = new Shifts(Repo);
            shiftsForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Repo.TxtBackup("backup.txt");
        }

        private void заметкиКРасписаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var scheduleNotesForm = new ScheduleNoteList(Repo);
            scheduleNotesForm.Show();
        }

        private void названияДисциплинToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var disciplineNameForm = new DisciplineNameList(Repo);
            disciplineNameForm.Show();
        }

        private async void датыПоФизическойКультуреToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                await Task.Run(() => WordExport.ExportCultureDates(Repo), _cToken);
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}
