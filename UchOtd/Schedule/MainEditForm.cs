using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Forms;
using Schedule.Forms.DBLists;
using Schedule.Forms.DBLists.Lessons;
using Schedule.Repositories;
using Schedule.Views.DBListViews;
using Schedule.wnu;
using Schedule.wnu.MySQLViews;
using UchOtd.Core;
using UchOtd.Schedule.Core;
using UchOtd.Schedule.Forms;
using UchOtd.Schedule.Forms.DBLists.Lessons;
using UchOtd.Schedule.wnu.MySQLViews;
using UchOtd.Schedule.Forms.DBLists;
using UchOtd.Schedule.Forms.Analysis;
using Schedule.DomainClasses.Analyse;

namespace UchOtd.Schedule
{
    public partial class MainEditForm : Form
    {
        public ScheduleRepository Repo;

        public static bool SchoolHeader = false;

        public MainEditForm(ScheduleRepository repo)
        {
            InitializeComponent();

            Repo = repo;
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            LoadLists();

            if (Repo != null)
            {
                Text = "Расписание (" + Utilities.ExtractDBOrConnectionName(Repo.ConnectionString) + ")";
            }

            if (StartupForm.school)
            {
                uploadPrefix.Text = "s_";
            }
        }

        private void LoadLists()
        {
            var groups = Repo
                .GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            groupList.ValueMember = "StudentGroupId";
            groupList.DisplayMember = "Name";
            groupList.DataSource = groups;            

            var faculties = Repo
                .GetAllFaculties()
                .OrderBy(f => f.SortingOrder)
                .ToList();

            FacultyList.DisplayMember = "Letter";
            FacultyList.ValueMember = "FacultyId";
            FacultyList.DataSource = faculties;

            var faculties2 = Repo
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
            foreach (var dow in global::Schedule.Constants.Constants.DOWLocal.Values)
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
        }

        private void ShowGroupLessonsClick(object sender, EventArgs e)
        {
            var sStarts = Repo.GetSemesterStarts();
            
            int weekNum = -1;
            if (weekFiltered.Checked)
            {                
                int.TryParse(WeekFilter.Text, out weekNum);                
            }

            var groupLessons = Repo.GetGroupedGroupLessons((int)groupList.SelectedValue, sStarts, weekNum, showProposedLessons.Checked);
            
            List<GroupTableView> groupEvents = CreateGroupTableView((int)groupList.SelectedValue, groupLessons, showProposedLessons.Checked);

            ScheduleView.DataSource = groupEvents;

            ScheduleView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            UpdateViewWidth();
        }

        private void UpdateViewWidth()
        {
            if (ScheduleView.DataSource == null)
            {
                return;
            }

            for (int i = 1; i <= 7; i++)
            {
                ScheduleView.Columns[i].HeaderText = global::Schedule.Constants.Constants.DOWLocal[i];
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

            var groupView = CreateGroupView(groupId, groupLessons, putProposedLessons);
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
            int groupId, Dictionary<string, Dictionary<string, Tuple<string, List<Lesson>>>> data,
            bool putProposedLessons)
        {
            var proposedLessonStartToken = "[";
            var proposedLessonEndToken = "]";

            var result = new List<GroupView>();

            var group = Repo.GetFirstFiltredStudentGroups(sg => sg.StudentGroupId == groupId);

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
                    var audWeekList = item.Value.Item2.ToDictionary(l => Repo.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
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
                            audStrings += ScheduleRepository.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " + jItem.Key;

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

        private void BigRedButtonClick(object sender, EventArgs e)
        {            
            // Oops
            // ExportStudentsData("StudentsExport-1sem.txt");
            // ImportStudentData("StudentsExport-1sem.txt");
            // CopyINOGroupLessonsFromRealSchedule();
            //ExportScheduleDates("Oops\\stat.txt");
            // ExportFacultyGroups();
            // ExportDiscAuds("Auds.txt");
            //ExportGroupDisciplines("Oops\\Discs.txt");

            
        }

        private void ExportGroupDisciplines(string filename)
        {
            String semesterString = (Repo.GetSemesterStarts().Month > 6) ? " (1 семестр)" : " (2 семестр)";

            foreach (var faculty in Repo.GetAllFaculties())
            {
                foreach (var group in Repo.GetFacultyGroups(faculty.FacultyId))
                {
                    AppendToFile(filename, "*" + group.Name + semesterString);

                    var studentIds = Repo
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);

                    var groupsListIds = Repo
                        .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                        .ToList()
                        .Select(stig => stig.StudentGroup.StudentGroupId);

                    var tfds = Repo.GetFiltredTeacherForDiscipline(tfd => groupsListIds.Contains(tfd.Discipline.StudentGroup.StudentGroupId));

                    foreach (var tfd in tfds)
                    {
                        AppendToFile(filename, 
                            tfd.Discipline.Name + '\t' + 
                            tfd.Discipline.StudentGroup.Name + '\t' +
                            tfd.Teacher.FIO + '\t' +
                            Repo.getTFDHours(tfd.TeacherForDisciplineId)
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
            var groupsId = Repo.GetFiltredStudentGroups(sg => sg.Name == "12 Д (+Н)" || sg.Name == "13 Д (+Н)" || sg.Name == "14 Д")
                    .Select(sg => sg.StudentGroupId)
                    .ToList();

            var econstudentIds = Repo
                .GetFiltredStudentsInGroups(sig => groupsId.Contains(sig.StudentGroup.StudentGroupId))
                .Select(stig => stig.Student.StudentId)
                .ToList();

            foreach (var tfd in Repo.GetAllTeacherForDiscipline().OrderBy(tefd => tefd.Discipline.Name))
            {
                if (tfd.Discipline.StudentGroup.Name == "12 И")
                {
                    continue;
                }

                var studentIds = Repo.GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == tfd.Discipline.StudentGroup.StudentGroupId)
                    .ToList()
                    .Select(stig => stig.Student.StudentId);

                

                Boolean econ = false;

                foreach (var studentId in studentIds)
                {
                    if (econstudentIds.Contains(studentId))
                    {
                        econ = true;
                        break;
                    }
                }

                if (!econ)
                {
                    continue;
                }

                var sb = new StringBuilder();
                sb.Append(tfd.Discipline.Name + '\t' + tfd.Discipline.StudentGroup.Name + '\t');

                var auds = Repo.GetFiltredLessons(l =>
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
            var faculty = Repo.GetFirstFiltredFaculty(f => f.Letter == "У");

            foreach (var group in Repo.GetFacultyGroups(faculty.FacultyId))
            {
                AppendToFile("Oops\\groups.txt", group.Name);

                var studentIds = Repo
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);

                var groupsList = Repo
                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .ToList()
                    .Select(stig => stig.StudentGroup)
                    .Distinct()
                    .ToList();

                foreach (var g in groupsList)
                {
                    AppendToFile("Oops\\groups.txt", g.Name);

                    var studentsInGroup = Repo
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
            String semesterString = (Repo.GetSemesterStarts().Month > 6) ? " (1 семестр)" : " (2 семестр)";

            
            foreach (var faculty in Repo.GetAllFaculties().OrderBy(f => f.SortingOrder))
            {
            //var faculty = _repo.GetFirstFiltredFaculty(f => f.Letter == "Г");

                foreach (var group in Repo.GetFacultyGroups(faculty.FacultyId))
                {
                    //var group = _repo.GetFirstFiltredStudentGroups(sg => sg.Name == "12 Т");

                    AppendToFile(filename, "*" + group.Name + semesterString);

                    var studentIds = Repo
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);

                    var groupsListIds = Repo
                        .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                        .ToList()
                        .Select(stig => stig.StudentGroup.StudentGroupId);

                    var tfds = Repo.GetFiltredTeacherForDiscipline(tfd => groupsListIds.Contains(tfd.Discipline.StudentGroup.StudentGroupId));

                    foreach (var tfd in tfds)
                    {
                        if (tfd.Discipline.AuditoriumHours == 0)
                        {
                            continue;
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.Append(
                            tfd.Discipline.StudentGroup.Name + '\t' +
                            tfd.Discipline.Name + '\t' +
                            tfd.Discipline.AuditoriumHours + '\t' +
                            tfd.Teacher.FIO + '\n'
                        );


                        var lessons = Repo.GetFiltredLessons(l => (l.State == 1) && l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId);

                        foreach (var lesson in lessons.OrderBy(l => l.Calendar.Date.Date))
                        {
                            sb.Append('\t' + lesson.Calendar.Date.Date.ToString("dd.MM.yyyy"));
                        }

                        AppendToFile(filename, sb.ToString());
                    }            
                }
            }
        }

        private static void AppendToFile(String filename, String line)
        {
            StreamWriter sw = new StreamWriter(filename, true);
            sw.WriteLine(line);
            sw.Close();
        }



        private void CopyINOGroupLessonsFromRealSchedule()
        {

            Repo.ConnectionString = "data source=tcp:127.0.0.1,1433; Database=ScheduleDB;User ID = "+ 
                    ";User ID = " + Properties.Settings.Default.DBUserName +
                    ";Password = " + Properties.Settings.Default.DBPassword;

            /*var discNames = Repo
                .GetFiltredTeacherForDiscipline(tfd => tfd.Discipline.StudentGroup.Name.Contains("-") && tfd.Discipline.AuditoriumHours != 0)
                .Select(tfd => tfd.Discipline.Name)
                .OrderBy(a => a)
                .ToList();*/

            var result = new Dictionary<string, List<Lesson>>();

            foreach (var tfd in Repo.GetAllTeacherForDiscipline())
            {
                if (tfd.Discipline.StudentGroup.Name.Contains("-") && tfd.Discipline.AuditoriumHours != 0)
                {
                    var tfdLessons = Repo.GetFiltredLessons(l =>
                        (l.State == 1) &&
                        l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId);

                    if (!result.ContainsKey(tfd.Discipline.StudentGroup.Name))
                    {
                        result.Add(tfd.Discipline.StudentGroup.Name, tfdLessons);
                    }
                }

            }

            Repo.ConnectionString = "data source=tcp:127.0.0.1,1433; Database=S-13-14-2;User ID = " +
                    ";User ID = " + Properties.Settings.Default.DBUserName +
                    ";Password = " + Properties.Settings.Default.DBPassword;

            var newLessonsList = new List<Lesson>();

            foreach (var kvp in result)
            {
                var tefd = Repo.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.StudentGroup.Name == kvp.Key);
                if (tefd != null)
                {
                    foreach (var lesson in kvp.Value)
                    {
                        var calendar = Repo.GetFirstFiltredCalendar(c => c.Date.Date == lesson.Calendar.Date.Date);
                        var ring = Repo.GetFirstFiltredRing(r => r.Time.TimeOfDay == lesson.Ring.Time.TimeOfDay);
                        var auditorium = Repo.GetFirstFiltredAuditoriums(a => a.Name == lesson.Auditorium.Name);

                        if ((calendar == null) || (ring == null) || (auditorium == null))
                        {
                            throw new Exception();
                        }

                        var newLesson = new Lesson { Auditorium = auditorium, Ring = ring, Calendar = calendar, State = 1, TeacherForDiscipline = tefd };

                        newLessonsList.Add(newLesson);
                    }
                }
            }

            foreach (var l in newLessonsList)
            {
                Repo.AddLesson(l);
            }
        }

        private void ImportStudentData(string filename)
        {
            var studentList = new List<Student>();
            var studentGroups = new List<StudentGroup>();
            var studentsInGroups = new List<StudentsInGroups>();

            var sr = new StreamReader(filename);

            string line;

            var maxStudentId = Repo
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
                    var student = new Student() { 
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

                    Repo.AddStudent(student);
                }

                maxStudentId++;
            }

            var maxGroupId = Repo
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

                    if (!Repo.GetFiltredStudentGroups(sg => sg.Name == groupParts[1]).Any())
                    {
                        var group = new StudentGroup
                        {
                            StudentGroupId = maxGroupId,
                            Name = groupParts[1]
                        };

                        groupIdRemap.Add(int.Parse(groupParts[0]), maxGroupId);

                        studentGroups.Add(@group);

                        Repo.AddStudentGroup(@group);

                        maxGroupId++;
                    }                
                    else
                    {
                        var gr = Repo.GetFirstFiltredStudentGroups(sg => sg.Name == groupParts[1]);
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
                    studentGroupId = Repo.GetFirstFiltredStudentGroups( sg =>
                    {
                        var studentGroup = studentGroups.FirstOrDefault(stg => stg.StudentGroupId == studentGroupId);
                        return studentGroup != null && sg.Name == studentGroup.Name;
                    }).StudentGroupId;
                }

                var sig = new StudentsInGroups
                {
                    Student = Repo.GetStudent(studentId),
                    StudentGroup = Repo.GetStudentGroup(studentGroupId)
                };

                studentsInGroups.Add(sig);

                Repo.AddStudentsInGroups(sig);
            }

            sr.Close();
        }

        private void ExportStudentsData(string filename)
        {
            var sw = new StreamWriter(filename);

            sw.WriteLine("Students");
            foreach (var student in Repo.GetFiltredStudents(s => !s.Expelled))
            {
                sw.WriteLine(
                    student.StudentId + "@" +
                    student.F + "@" +
                    student.I + "@" +
                    student.O
                );
            }

            sw.WriteLine("StudentGroups");
            foreach (var sg in Repo.GetAllStudentGroups())
            {
                sw.WriteLine(sg.StudentGroupId + "@" + sg.Name);
            }

            sw.WriteLine("StudentsInGroups");
            foreach (var sig in Repo.GetFiltredStudentsInGroups(sig => !sig.Student.Expelled))
            {
                sw.WriteLine(sig.Student.StudentId + "@" + sig.StudentGroup.StudentGroupId);
            }

            sw.Close();
        }

        private List<Lesson> SchoolAudLessons()
        {
            var aSchool = Repo.GetFiltredAuditoriums(a => a.Name.Contains("ШКОЛА"))[0];
            var ll = Repo.GetFiltredLessons(l => l.Auditorium.AuditoriumId == aSchool.AuditoriumId && l.Calendar.Date > DateTime.Now && (l.State == 1));
            return ll;
        }

        private void LogDoubledLessons(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.Close();
            var students = Repo.GetFiltredStudents(s => !s.Expelled);
            foreach (var student in students)
            {
                var studentGroupIds = Repo
                    .GetFiltredStudentsInGroups(sig => sig.Student.StudentId == student.StudentId)
                    .Select(sig => sig.StudentGroup.StudentGroupId);

                var studentLessons = Repo.GetFiltredLessons(l => (l.State == 1) && studentGroupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId));

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
            var ll = Repo.GetFiltredLessons(l => (l.State == 1) && l.Calendar.Date == date);
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

        private void AuditoriumCollisions()
        {

            var activeLessons = Repo.GetAllActiveLessons();

            var sw = new StreamWriter("kaput.txt");
            foreach (var i in activeLessons)
            {
                foreach (var j in activeLessons)
                {
                    if ((i.Calendar.CalendarId == j.Calendar.CalendarId) && (i.Ring.RingId == j.Ring.RingId) && (i.Auditorium.AuditoriumId == j.Auditorium.AuditoriumId) &&
                        (i.LessonId != j.LessonId))
                    {
                        if ((i.TeacherForDiscipline.Teacher.FIO == "Хенкин Валерий Анатольевич") && ((j.TeacherForDiscipline.Teacher.FIO == "Хенкин Валерий Анатольевич")))
                        {
                            break;
                        }
                        sw.WriteLine(
                            i.Calendar.Date.Date + "\t" + i.Ring.Time.TimeOfDay + "\t" +
                            i.Auditorium.Name + "\t" +
                            i.LessonId + "\t" +
                            i.TeacherForDiscipline.Discipline.Name + "\t" + i.TeacherForDiscipline.Discipline.StudentGroup.Name + "\t" + i.TeacherForDiscipline.Teacher.FIO + "\t" +
                            j.LessonId + "\t" +
                            j.TeacherForDiscipline.Discipline.Name + "\t" + j.TeacherForDiscipline.Discipline.StudentGroup.Name + "\t" + j.TeacherForDiscipline.Teacher.FIO);
                    }
                }
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

        private void ImportFromTextClick(object sender, EventArgs e)
        {
            /*
            _repo.RecreateDB();
            _repo.Dispose();

            _repo = new ScheduleRepository();

            const string basePath = @"D:\BS\csprogs\Schedule\Schedule.TxtImport\bin\Debug\Import\old\";
            //const string basePath = @"E:\csprogs\Schedule\Schedule.TxtImport\bin\Debug\Import\old\";

            var auds = ScheduleTxtImport.ImportAuditoriums(basePath);
            _repo.AddAuditoriumRange(auds);

            var studentGroups = ScheduleTxtImport.ImportStudentsWithBaseGroups(basePath);
            foreach (var group in studentGroups)
            {
                var groupToAdd = _repo.FindStudentGroup(group.Key);
                if (groupToAdd == null)
                {
                    groupToAdd = new StudentGroup { Name = group.Key };
                    _repo.AddStudentGroup(groupToAdd);
                }

                foreach (var student in group.Value)
                {
                    _repo.AddStudent(student);

                    _repo.AddStudentsInGroups(new StudentsInGroups { Student = student, StudentGroup = groupToAdd });
                }
            }

            var disciplines = ScheduleTxtImport.ImportDisciplines(basePath);
            foreach (var disc in disciplines)
            {
                var group = _repo.FindStudentGroup(disc.StudentGroup.Name);

                if (group == null)
                {
                    group = new StudentGroup { Name = disc.StudentGroup.Name };
                    _repo.AddStudentGroup(group);
                }

                disc.StudentGroup = group;
            }
            _repo.AddDisciplineRange(disciplines);

            var rings = ScheduleTxtImport.ImportRings(basePath);
            _repo.AddRingRange(rings);
            */
            /*
            var teachers = ScheduleTxtImport.ImportTeacherList();
            _repo.AddTeacherRange(teachers);
             */
            
        }

        private void Button1Click(object sender, EventArgs e)
        {
            var addLessonForm = new AddLesson(Repo);
            addLessonForm.Show();
        }

        private void LoadToSiteClick(object sender, EventArgs e)
        {
            WnuUpload.UploadSchedule(Repo, "");
        }

        private void RemovelessonClick(object sender, EventArgs e)
        {
            var removeLessonForm = new RemoveLesson(Repo);
            removeLessonForm.Show();
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

            foreach (var disc in Repo.GetAllDisciplines())
            {
                var disc1 = disc;
                var disctfd = Repo.GetFiltredTeacherForDiscipline(tefd => tefd.Discipline.DisciplineId == disc1.DisciplineId).FirstOrDefault();
                if (disctfd == null)
                {
                    continue;
                }
                var tfd = disctfd;

                var tfdLessons = Repo.GetFiltredLessons(l => (l.State == 1) && l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId).ToList();

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

        private void AuditoriumKaputClick(object sender, EventArgs e)
        {
            AuditoriumCollisions();
        }

        private void ФакультетыгруппыToolStripMenuItemClick(object sender, EventArgs e)
        {
            var facultyListForm = new FacultyList(Repo);
            facultyListForm.Show();
        }

        private void ActiveLessonsCount_Click(object sender, EventArgs e)
        {
            var allDiscLessonCount = Repo.GetAllDisciplines().Select(d => d.AuditoriumHours).Sum() / 2;
            var activeLessonsCount = Repo.GetAllActiveLessons().Count();
            var diff = allDiscLessonCount - activeLessonsCount;            
            String message = activeLessonsCount + " (" + String.Format("{0:0.00}%", (double)activeLessonsCount * 100 / allDiscLessonCount) + ") / " + allDiscLessonCount
                + " =>  " + diff + " (" + String.Format("{0:0.00}%", (double)diff * 100 / allDiscLessonCount) + ")";

            var discCount = Repo.GetAllDisciplines().Count;
            var touchedDiscs = Repo
                .GetFiltredDisciplines(d => 
                    (d.AuditoriumHours == 0) ||
                    (Repo.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == d.DisciplineId) == null) ||
                    (Repo.getTFDHours(Repo.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == d.DisciplineId).TeacherForDisciplineId) != 0))
                .Count;
            var diff2 = discCount - touchedDiscs;

            message += Environment.NewLine + touchedDiscs + " (" + String.Format("{0:0.00}%", (double)touchedDiscs * 100 / discCount) + ") / " + discCount
                + " =>  " + diff2 + " (" + String.Format("{0:0.00}%", (double)diff2 * 100 / discCount) + ")";

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
            var teacherHoursForm = new teacherHours(Repo);
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
                var evts = Repo.GetFiltredLessonLogEvents(evt => evt.DateTime.Date == curDate.Date);
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
            // facultyId + DOW
            var result = new List<Tuple<int, DayOfWeek>>();

            var evts = Repo.GetFiltredLessonLogEvents(lle => lle.DateTime.Date == DateTime.Now.Date);

            var fg = Repo.GetAllGroupsInFaculty()
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
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroupId)
                    .Select(sig => sig.Student.StudentId)
                    .ToList();

                    var facultyScheduleChanged = new List<int>();

                    foreach (var faculty in fg)
                    {
                        if (Repo.GetFiltredStudentsInGroups(sig => 
                            studentIds.Contains(sig.Student.StudentId) && 
                            faculty.Contains(sig.StudentGroup.StudentGroupId)).Any())
                        {
                            facultyScheduleChanged.Add(faculty.Key);
                        }
                    }

                    foreach (var faculty in facultyScheduleChanged)
                    {
                        var dowFacTuple = Tuple.Create(faculty, ev.OldLesson.Calendar.Date.DayOfWeek);
                        if (!result.Contains(dowFacTuple))
                        {
                            result.Add(dowFacTuple);
                        }
                    }
                }

                

                if (ev.NewLesson != null)
                {
                    studentGroupId = ev.NewLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;

                    var studentIds = Repo
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroupId)
                    .Select(sig => sig.Student.StudentId)
                    .ToList();

                    var facultyScheduleChanged = new List<int>();

                    foreach (var faculty in fg)
                    {
                        if (Repo.GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId) && faculty.Contains(sig.StudentGroup.StudentGroupId)).Any())
                        {
                            facultyScheduleChanged.Add(faculty.Key);
                        }
                    }

                    foreach (var faculty in facultyScheduleChanged)
                    {
                        var dowFacTuple = Tuple.Create(faculty, ev.NewLesson.Calendar.Date.DayOfWeek);
                        if (!result.Contains(dowFacTuple))
                        {
                            result.Add(dowFacTuple);
                        }
                    }
                }                
            }

            var messageString = "";

            foreach (var dowFac in result.OrderBy(df => df.Item1).ThenBy(df => df.Item2))
            {
                messageString += Repo.GetFaculty(dowFac.Item1).Letter + " - " + global::Schedule.Constants.Constants.DOWLocal[global::Schedule.Constants.Constants.DOWRemap[(int)dowFac.Item2]] + Environment.NewLine;
            }

            MessageBox.Show(messageString, "Изменения на сегодня");
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
                .GetAllActiveLessons()
                .OrderBy(l => l.Calendar.Date.Date)
                .ThenBy(l => l.Ring.Time.TimeOfDay)
                .ThenBy(l => l.Auditorium.Name)                
                .ToList();

            for (int i = 0; i < lessons.Count; i++)
            {
                sr.WriteLine(
                    lessons[i].Calendar.Date.Date.ToString("dd.MM.yyyy") + '\t' + 
                    lessons[i].Ring.Time + '\t' + 
                    lessons[i].TeacherForDiscipline.Discipline.Name + '\t' + 
                    lessons[i].TeacherForDiscipline.Teacher.FIO + '\t' +
                    lessons[i].TeacherForDiscipline.Discipline.StudentGroup.Name + '\t' + 
                    lessons[i].Auditorium.Name);
            }

            sr.Close();

            Process.Start("ExcelData.txt");            
        }

        private void LessonListByTFD_Click(object sender, EventArgs e)
        {
            var lessonListByTfdForm = new LessonListByTFD(Repo);
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

            var teachersSchedule = new UchOtd.Forms.TeacherSchedule(Repo);
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

        private void CreatePDF_Click(object sender, EventArgs e)
        {
            var facultyId = (int)FacultyList.SelectedValue;
            var facultyName = Repo.GetFaculty(facultyId).Name;
            var ruDow = DOWList.SelectedIndex + 1;

            var facultyDowLessons = Repo.GetFacultyDOWSchedule(facultyId, ruDow, false, -1);
            PDFExport.ExportSchedulePage(facultyDowLessons, facultyName, "Export.pdf", DOWList.Text, Repo, true, false, false);

            Process.Start("Export.pdf");
        }

        private void AllInPDF_Click(object sender, EventArgs e)
        {            
            //PDFExport.ExportWholeSchedule("Export.pdf", _repo, false, false, false);

            PDFExport.PrintWholeSchedule(Repo);
        }

        private void BackupAndUpload_Click(object sender, EventArgs e)
        {
            var dbName = Repo.ExtractDBName(Repo.ConnectionString);

            Repo.BackupDB(Application.StartupPath + "\\" + dbName + ".bak");
            WnuUpload.UploadFile(Application.StartupPath + "\\" + dbName + ".bak", "httpdocs/upload/DB-Backup/" + dbName + ".bak");
        }

        private void DownloadAndRestore_Click(object sender, EventArgs e)
        {
            var wc = new WebClient();
            //wc.DownloadFile("http://wiki.nayanova.edu/upload/DB-Backup/" + DBRestoreName.Text + ".bak", Application.StartupPath + "\\" + DBRestoreName.Text + ".bak");
            //Repo.RestoreDB(DBRestoreName.Text, Application.StartupPath + "\\" + DBRestoreName.Text + ".bak");
        }

        private void WholeScheduleDatesExport_Click(object sender, EventArgs e)
        {
            ExportWholeScheduleDates("ScheduleDates.txt");
        }

        private void ExportWholeScheduleDates(string filename)
        {
            var groups = Repo
                .GetFiltredStudentGroups(sg => !(sg.Name.Contains("-") || sg.Name.Contains("+") || sg.Name.Contains("I") || sg.Name.Length == 1 || sg.Name.Contains("(Н)") || sg.Name.Contains(".")))
                .ToList();

            foreach (var group in groups)
            {
                var sw = new StreamWriter(filename, true);
                sw.WriteLine(group.Name);
                sw.Close();


                var studentIds = Repo
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                    .Select(sig => sig.Student.StudentId)
                    .ToList();

                var groupIds = Repo
                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .Select(sig => sig.StudentGroup.StudentGroupId)
                    .Distinct()
                    .ToList();

                var tfds = Repo
                    .GetFiltredTeacherForDiscipline(tfd => groupIds.Contains(tfd.Discipline.StudentGroup.StudentGroupId))
                    .ToList();

                foreach (var tfd in tfds)
                {
                    var lessons = Repo.GetFiltredLessons(l =>
                        (l.State == 1) &&
                        l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId)
                        .OrderBy(l => l.Calendar.Date.Date)
                        .ToList();

                    if (lessons.Count == 0)
                    {
                        continue;
                    }

                    StringBuilder discipline = new StringBuilder();

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
                WordExport.ExportWholeSchedule(Repo, "Расписание.docx", false, false, 90, (int)WordFacultyFilter.SelectedValue, 6, SchoolHeader);
            }
            else
            {
                WordExport.ExportWholeSchedule(Repo, "Расписание.docx", false, false, 90, -1, 6, SchoolHeader);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (WordOneFaculty.Checked)
            {
                WordExport.ExportWholeSchedule(Repo, "Расписание.docx", false, false, cb90.Checked ? 90 : 80, (int)WordFacultyFilter.SelectedValue, 6, SchoolHeader);
            }
            else
            {
                WordExport.ExportWholeSchedule(Repo, "Расписание.docx", false, false, cb90.Checked ? 90 : 80, -1, 6, SchoolHeader);
            }
        }
                

        private void AuditoriumPercentage_Click(object sender, EventArgs e)
        {
            var sw = new StreamWriter("AuditoriumPercentage.txt");
            sw.Close();

            var activeLessons = Repo.GetFiltredLessons(l => (l.State == 1) && l.Ring.RingId <= 8);

            WriteAuditoriumPercentageToFile(activeLessons, "AuditoriumPercentage.txt");

            activeLessons = Repo.GetFiltredLessons(l => (l.State == 1) && l.Ring.RingId <= 8 && (l.Auditorium.Building.BuildingId == 2));

            WriteAuditoriumPercentageToFile(activeLessons, "AuditoriumPercentage.txt");

            activeLessons = Repo.GetFiltredLessons(l => (l.State == 1) && l.Ring.RingId <= 8 && (l.Auditorium.Building.BuildingId == 3));

            WriteAuditoriumPercentageToFile(activeLessons, "AuditoriumPercentage.txt");
        }

        private void WriteAuditoriumPercentageToFile(List<Lesson> activeLessons, string filename)
        {
            //                          dow(1-7)        time    lessonCount
            var result = new Dictionary<int, Dictionary<string, int>>();

            var rings = Repo.GetAllRings().Where(r => r.RingId <= 8).OrderBy(r => r.Time).ToList();
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
                sw.Write(global::Schedule.Constants.Constants.DOWLocal[i] + "\t");
                foreach (var ring in rings)
                {
                    sw.Write(result[i][ring.Time.ToString("H:mm")] + "\t");
                }
                sw.WriteLine();
            }

            sw.WriteLine();

            sw.Close();
        }

        private void WordExport_Click(object sender, EventArgs e)
        {
            var facultyId = (int)FacultyList.SelectedValue;            
            var ruDow = DOWList.SelectedIndex + 1;
            int weekFilter;
            int.TryParse(WordExportWeekFilter.Text, out weekFilter);

            WordExport.ExportSchedulePage(
                Repo, "Расписание.docx", false, false, cb90.Checked ? 90 : 80, facultyId, ruDow, 6,
                wordExportWeekFiltered.Checked, weekFilter, !wordExportWeekFiltered.Checked);
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
            //dayDelta_Click(sender, e);            
            //setLayout_Click(sender, e);
            ExportGroupDisciplines("Oops\\Discs.txt");

            /*
            foreach (var disc in Repo.GetAllDisciplines())
            {
                if (disc.Name.EndsWith("\t"))
                {
                    disc.Name = disc.Name.Substring(0, disc.Name.Length - 1);
                    Repo.UpdateDiscipline(disc);
                }
            }*/
        }

        private void WordSchool_Click_1(object sender, EventArgs e)
        {
            var facultyId = (int)FacultyList.SelectedValue;
            var ruDow = DOWList.SelectedIndex + 1;
            int weekFilter;
            int.TryParse(WordExportWeekFilter.Text, out weekFilter);

            WordExport.WordSchool(
                Repo, "Расписание.docx", false, false, 80, facultyId, ruDow, 6,
                wordExportWeekFiltered.Checked, weekFilter, !wordExportWeekFiltered.Checked);
        }

        private void WordSchool2_Click(object sender, EventArgs e)
        {
            var facultyId = (int)FacultyList.SelectedValue;
            var ruDow = DOWList.SelectedIndex + 1;
            int weekFilter;
            int.TryParse(WordExportWeekFilter.Text, out weekFilter);

            WordExport.WordSchoolTwoDays(
                Repo, "Расписание.docx", false, false, 80, facultyId, ruDow, 6,
                wordExportWeekFiltered.Checked, weekFilter, !wordExportWeekFiltered.Checked);
        }

        private void happyBirthday_Click(object sender, EventArgs e)
        {
            var message = "";

            foreach (var student in Repo.GetFiltredStudents(s => !s.Expelled))
            {
                if (DateTime.Now.Date == student.BirthDate.Date)
                {
                    message += student.F + " " + student.I + " " + student.O  + " ( " + (student.BirthDate.Year - DateTime.Now.Year) + " / " + student.BirthDate.Year + " )" + Environment.NewLine;
                }
            }

            if (message == "")
            {
                message = "Нету дома никого.";
            }

            MessageBox.Show(message, "Happy");
        }

        private void OnePageGroupScheduleWordExport_Click(object sender, EventArgs e)
        {
            WordExport.ExportGroupSchedulePage(Repo, this, (int)groupList.SelectedValue);
        }

        private void WordWholeScheduleOneGroupOnePage_Click(object sender, EventArgs e)
        {
            WordExport.ExportSchedulePage(Repo, this);            
        }

        private void BackupUpload_Click(object sender, EventArgs e)
        {
            string dbName = (FromDBName.Text == "") ? Repo.ExtractDBName(Repo.ConnectionString) : FromDBName.Text;
            
            Repo.BackupDB(Application.StartupPath + "\\" + dbName + ".bak");
            WnuUpload.UploadFile(Application.StartupPath + "\\" + dbName + ".bak", "httpdocs/upload/DB-Backup/" + ToDBName.Text + ".bak");
        }

        private void startSchoolWordExport_Click(object sender, EventArgs e)
        {
            var facultyId = (int)FacultyList.SelectedValue;
            var ruDow = DOWList.SelectedIndex + 1;
            int weekFilter;
            int.TryParse(WordExportWeekFilter.Text, out weekFilter);

            WordExport.WordStartSchool(
                Repo, "Расписание.docx", false, false, 40, facultyId, ruDow, 6,
                wordExportWeekFiltered.Checked, weekFilter, !wordExportWeekFiltered.Checked);
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
            var NottheLastLessonForm = new LastLesson(Repo);
            NottheLastLessonForm.Show();
        }

        private void парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var IncompatiblePairsForm = new IncompatiblePairs(Repo);
            IncompatiblePairsForm.Show();
        }

        private void дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var DoubledLessonsForm = new DoubledLessons(Repo);
            DoubledLessonsForm.Show();
        }

        private void порядокПостановкиДисциплинВРасписаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var DisciplineByOrderForm = new DisciplineByOrder(Repo);
            DisciplineByOrderForm.Show();
        }

        private void analyse_Click(object sender, EventArgs e)
        {
            var analysisForm = new Analysis(Repo);
            analysisForm.Show();
        }

        private void analyseSchool_Click(object sender, EventArgs e)
        {
            var analysisSchoolForm = new AnalysisSchool(Repo);
            analysisSchoolForm.Show();
        }

        private void removeAllProposedLessons_Click(object sender, EventArgs e)
        {
            var proposedLessonsIds = Repo
                .GetFiltredLessons(l => l.State == 2)
                .Select(l => l.LessonId)
                .ToList();

            foreach (var lessonId in proposedLessonsIds)
            {
                Repo.RemoveLesson(lessonId);
            }
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
            var DisciplinesWithAudForm = new DisciplinesWithAud(Repo);
            DisciplinesWithAudForm.Show();
        }
    }
}
