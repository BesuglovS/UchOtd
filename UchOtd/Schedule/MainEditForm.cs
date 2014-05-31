using Schedule.DomainClasses.Main;
using Schedule.Forms.DBLists;
using Schedule.Forms.DBLists.Lessons;
using Schedule.Repositories;
using Schedule.Views.DBListViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.wnu;
using Schedule.wnu.MySQLViews;
using System.Globalization;
using Schedule.Core;
using System.IO;
using Schedule.Forms;
using Tuple = System.Tuple;
using UchOtd.Schedule.Core;
using UchOtd.Schedule.wnu.MySQLViews;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace Schedule
{
    public partial class MainEditForm : Form
    {
        public ScheduleRepository _repo;

        public MainEditForm(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            LoadLists();

            if (_repo != null)
            {
                this.Text = "Расписание (" + Utilities.ExtractDBOrConnectionName(_repo.ConnectionString) + ")";
            }
        }

        private void LoadLists()
        {
            var groups = _repo
                .GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            groupList.ValueMember = "StudentGroupId";
            groupList.DisplayMember = "Name";
            groupList.DataSource = groups;            

            var faculties = _repo
                .GetAllFaculties()
                .OrderBy(f => f.SortingOrder)
                .ToList();

            FacultyList.DisplayMember = "Letter";
            FacultyList.ValueMember = "FacultyId";
            FacultyList.DataSource = faculties;

            DOWList.Items.Clear();
            foreach (var dow in Constants.Constants.DOWLocal.Values)
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
            var sStarts = _repo.GetSemesterStarts();

            Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>> groupLessons;
            if (weekFiltered.Checked)
            {
                int weekNum = -1;
                int.TryParse(WeekFilter.Text, out weekNum);

                groupLessons = _repo.GetGroupedGroupLessons((int)groupList.SelectedValue, sStarts, weekNum);
            }
            else
            {
                groupLessons = _repo.GetGroupedGroupLessons((int)groupList.SelectedValue, sStarts);
            }
                        
            List<GroupTableView> groupEvents = CreateGroupTableView(groupLessons);
            
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
                ScheduleView.Columns[i].HeaderText = Constants.Constants.DOWLocal[i];
                if (i < 7)
                {
                    ScheduleView.Columns[i].Width = (ScheduleView.Width - ScheduleView.Columns[0].Width - 20) / 6;
                }

            }
        }

        private List<GroupTableView> CreateGroupTableView(Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>> groupLessons)
        {
            var result = new List<GroupTableView>();

            var groupView = CreateGroupView(groupLessons);
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

        private IEnumerable<GroupView> CreateGroupView(Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>> data)
        {
            var result = new List<GroupView>();

            foreach (var dt in data)
            {
                var eventString = "";

                for (int i = 0; i < dt.Value.Count; i++)
                {
                    var item = dt.Value.ElementAt(i);
                    var tfd = item.Value.Item2[0].TeacherForDiscipline;

                    eventString += tfd.Discipline.Name + Environment.NewLine;
                    eventString += tfd.Teacher.FIO + Environment.NewLine;
                    eventString += "(" + item.Value.Item1 + ")" + Environment.NewLine;

                    var audWeekList = item.Value.Item2.ToDictionary(l => _repo.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
                    var grouped = audWeekList.GroupBy(a => a.Value);

                    var enumerable = grouped as List<IGrouping<string, KeyValuePair<int, string>>> ?? grouped.ToList();
                    var gcount = enumerable.Count();
                    if (gcount == 1)
                    {
                        eventString += enumerable.ElementAt(0).Key;
                    }
                    else
                    {
                        for (int j = 0; j < gcount; j++)
                        {
                            var jItem = enumerable.ElementAt(j);
                            eventString += ScheduleRepository.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " + jItem.Key;

                            if (j != gcount - 1)
                            {
                                eventString += Environment.NewLine;
                            }
                        }
                    }

                    if (i != dt.Value.Count - 1)
                    {
                        eventString += Environment.NewLine;
                    }

                }

                result.Add(new GroupView { Datetime = dt.Key, Events = eventString });
            }

            return result;
        }
        
        private void BigRedButtonClick(object sender, EventArgs e)
        {            
            // Oops
            // ExportStudentsData("StudentsExport-1sem.txt");
            // ImportStudentData("StudentsExport-1sem.txt");
            // CopyINOGroupLessonsFromRealSchedule();
            ExportScheduleDates();
            // ExportFacultyGroups();
        }

        private void ExportFacultyGroups()
        {
            var faculty = _repo.GetFirstFiltredFaculty(f => f.Letter == "У");

            foreach (var group in _repo.GetFacultyGroups(faculty.FacultyId))
            {
                AppendToFile("Oops\\groups.txt", group.Name);

                var studentIds = _repo
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);

                var groupList = _repo
                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .ToList()
                    .Select(stig => stig.StudentGroup)
                    .Distinct()
                    .ToList();

                foreach (var g in groupList)
                {
                    AppendToFile("Oops\\groups.txt", g.Name);

                    var studentsInGroup = _repo
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

        private void ExportScheduleDates()
        {
            String semesterString = (_repo.GetSemesterStarts().Month > 6) ? " (1 семестр)" : " (2 семестр)";

            
            foreach (var faculty in _repo.GetAllFaculties().OrderBy(f => f.SortingOrder))
            {
            //var faculty = _repo.GetFirstFiltredFaculty(f => f.Letter == "Г");

                foreach (var group in _repo.GetFacultyGroups(faculty.FacultyId))
                {
                    //var group = _repo.GetFirstFiltredStudentGroups(sg => sg.Name == "14 В");
                    
                    AppendToFile("Oops\\stat.txt", "*" + group.Name + semesterString);

                    var studentIds = _repo
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);

                    var groupsListIds = _repo
                        .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                        .ToList()
                        .Select(stig => stig.StudentGroup.StudentGroupId);

                    var tfds = _repo.GetFiltredTeacherForDiscipline(tfd => groupsListIds.Contains(tfd.Discipline.StudentGroup.StudentGroupId));

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


                        var lessons = _repo.GetFiltredLessons(l => l.IsActive && l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId);

                        foreach (var lesson in lessons.OrderBy(l => l.Calendar.Date.Date))
                        {
                            sb.Append('\t' + lesson.Calendar.Date.Date.ToString("dd.MM.yyyy"));
                        }

                        AppendToFile("Oops\\stat.txt", sb.ToString());
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

            _repo.ConnectionString = "data source=tcp:127.0.0.1,1433; Database=ScheduleDB;User ID = "+ 
                    ";User ID = " + UchOtd.Properties.Settings.Default.DBUserName +
                    ";Password = " + UchOtd.Properties.Settings.Default.DBPassword;

            var discNames = _repo
                .GetFiltredTeacherForDiscipline(tfd => tfd.Discipline.StudentGroup.Name.Contains("-") && tfd.Discipline.AuditoriumHours != 0)
                .Select(tfd => tfd.Discipline.Name)
                .OrderBy(a => a)
                .ToList();

            var result = new Dictionary<string, List<Lesson>>();

            foreach (var tfd in _repo.GetAllTeacherForDiscipline())
            {
                if (tfd.Discipline.StudentGroup.Name.Contains("-") && tfd.Discipline.AuditoriumHours != 0)
                {
                    var tfdLessons = _repo.GetFiltredLessons(l =>
                        l.IsActive &&
                        l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId);

                    if (!result.ContainsKey(tfd.Discipline.StudentGroup.Name))
                    {
                        result.Add(tfd.Discipline.StudentGroup.Name, tfdLessons);
                    }
                }

            }

            _repo.ConnectionString = "data source=tcp:127.0.0.1,1433; Database=S-13-14-2;User ID = " +
                    ";User ID = " + UchOtd.Properties.Settings.Default.DBUserName +
                    ";Password = " + UchOtd.Properties.Settings.Default.DBPassword;

            var newLessonsList = new List<Lesson>();

            foreach (var kvp in result)
            {
                var tefd = _repo.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.StudentGroup.Name == kvp.Key);
                if (tefd != null)
                {
                    foreach (var lesson in kvp.Value)
                    {
                        var calendar = _repo.GetFirstFiltredCalendar(c => c.Date.Date == lesson.Calendar.Date.Date);
                        var ring = _repo.GetFirstFiltredRing(r => r.Time.TimeOfDay == lesson.Ring.Time.TimeOfDay);
                        var auditorium = _repo.GetFirstFiltredAuditoriums(a => a.Name == lesson.Auditorium.Name);

                        if ((calendar == null) || (ring == null) || (auditorium == null))
                        {
                            throw new Exception();
                        }

                        var newLesson = new Lesson() { Auditorium = auditorium, Ring = ring, Calendar = calendar, IsActive = true, TeacherForDiscipline = tefd };

                        newLessonsList.Add(newLesson);
                    }
                }
            }

            foreach (var l in newLessonsList)
            {
                _repo.AddLesson(l);
            }
        }

        private void ImportStudentData(string filename)
        {
            var studentList = new List<Student>();
            var studentGroups = new List<StudentGroup>();
            var studentsInGroups = new List<StudentsInGroups>();

            var sr = new StreamReader(filename);

            string line;

            var maxStudentId = _repo
                .GetAllStudents()
                .Select(s => s.StudentId)
                .Max();
            maxStudentId++;

            var StudentIdRemap = new Dictionary<int, int>();

            sr.ReadLine();
            while ((line = sr.ReadLine()) != "StudentGroups")
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

                StudentIdRemap.Add(int.Parse(studentParts[0]), maxStudentId);

                studentList.Add(student);

                _repo.AddStudent(student);

                maxStudentId++;
            }

            StudentGroup group = null;
            var maxGroupId = _repo
                .GetAllStudentGroups()
                .Select(s => s.StudentGroupId)
                .Max();
            maxGroupId++;

            var groupIdRemap = new Dictionary<int, int>();
            while ((line = sr.ReadLine()) != "StudentsInGroups")
            {                
                var groupParts = line.Split('@');

                if (!_repo.GetFiltredStudentGroups(sg => sg.Name == groupParts[1]).Any())
                {
                    group = new StudentGroup()
                    {
                        StudentGroupId = maxGroupId,
                        Name = groupParts[1]
                    };

                    groupIdRemap.Add(int.Parse(groupParts[0]), maxGroupId);

                    studentGroups.Add(group);

                    _repo.AddStudentGroup(group);

                    maxGroupId++;
                }                
                else
                {
                    var gr = _repo.GetFirstFiltredStudentGroups(sg => sg.Name == groupParts[1]);
                    studentGroups.Add(gr);

                    groupIdRemap.Add(int.Parse(groupParts[0]), gr.StudentGroupId);
                }
            }

            while ((line = sr.ReadLine()) != null)
            {
                var sigParts = line.Split('@');

                var studentId = int.Parse(sigParts[0]);
                studentId = StudentIdRemap[studentId];

                var studentGroupId = int.Parse(sigParts[1]);
                if (groupIdRemap.ContainsKey(studentGroupId))
                {
                    studentGroupId = groupIdRemap[studentGroupId];
                }
                else
                {
                    studentGroupId = _repo.GetFirstFiltredStudentGroups( sg => sg.Name ==
                        studentGroups.FirstOrDefault(stg => stg.StudentGroupId == studentGroupId).Name).StudentGroupId;
                }

                var sig = new StudentsInGroups()
                {
                    Student = _repo.GetStudent(studentId),
                    StudentGroup = _repo.GetStudentGroup(studentGroupId)
                };

                studentsInGroups.Add(sig);

                _repo.AddStudentsInGroups(sig);
            }

            sr.Close();
        }

        private void ExportStudentsData(string filename)
        {
            var sw = new StreamWriter(filename);

            sw.WriteLine("Students");
            foreach (var student in _repo.GetFiltredStudents(s => !s.Expelled))
            {
                sw.WriteLine(
                    student.StudentId + "@" +
                    student.F + "@" +
                    student.I + "@" +
                    student.O
                );
            }

            sw.WriteLine("StudentGroups");
            foreach (var sg in _repo.GetAllStudentGroups())
            {
                sw.WriteLine(sg.StudentGroupId + "@" + sg.Name);
            }

            sw.WriteLine("StudentsInGroups");
            foreach (var sig in _repo.GetFiltredStudentsInGroups(sig => !sig.Student.Expelled))
            {
                sw.WriteLine(sig.Student.StudentId + "@" + sig.StudentGroup.StudentGroupId);
            }

            sw.Close();
        }

        private List<Lesson> SchoolAudLessons()
        {
            var aSchool = _repo.GetFiltredAuditoriums(a => a.Name.Contains("ШКОЛА"))[0];
            var ll = _repo.GetFiltredLessons(l => l.Auditorium.AuditoriumId == aSchool.AuditoriumId && l.Calendar.Date > DateTime.Now && l.IsActive);
            return ll;
        }

        private void LogDoubledLessons(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.Close();
            var students = _repo.GetFiltredStudents(s => !s.Expelled);
            foreach (var student in students)
            {
                var studentGroupIds = _repo
                    .GetFiltredStudentsInGroups(sig => sig.Student.StudentId == student.StudentId)
                    .Select(sig => sig.StudentGroup.StudentGroupId);

                var studentLessons = _repo.GetFiltredLessons(l => l.IsActive && studentGroupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId));

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
            var ll = _repo.GetFiltredLessons(l => l.IsActive && l.Calendar.Date == date);
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

            var activeLessons = _repo.GetAllActiveLessons();

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
            var audForm = new AuditoriumList(_repo);
            audForm.Show();
        }
       
        private void ДниСеместраToolStripMenuItemClick(object sender, EventArgs e)
        {
            var calendarForm = new CalendarList(_repo);
            calendarForm.Show();
        }

        private void ЗвонкиToolStripMenuItemClick(object sender, EventArgs e)
        {
            var ringForm = new RingList(_repo);
            ringForm.Show();
        }

        private void СтудентыToolStripMenuItemClick(object sender, EventArgs e)
        {
            var studentForm = new StudentList(_repo);
            studentForm.Show();
        }

        private void ГруппыToolStripMenuItemClick(object sender, EventArgs e)
        {
            var studentGroupForm = new StudentGroupList(_repo);
            studentGroupForm.Show();
        }

        private void ПреподавателиToolStripMenuItemClick(object sender, EventArgs e)
        {
            var teacherForm = new TeacherList(_repo);
            teacherForm.Show();
        }

        private void ДисциплиныToolStripMenuItemClick(object sender, EventArgs e)
        {
            var disciplineForm = new DisciplineList(_repo);
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
            var addLessonForm = new AddLesson(_repo);
            addLessonForm.Show();
        }

        private void LoadToSiteClick(object sender, EventArgs e)
        {
            var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            
            var auditoriums = _repo.GetAllAuditoriums();
            var wud = new WnuUploadData { tableSelector = "auditoriums", data = jsonSerializer.Serialize(auditoriums) };
            string json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);
            

            var calendars = _repo.GetAllCalendars();
            var mySqlCalendars = MySQLCalendar.FromCalendarList(calendars);
            wud = new WnuUploadData { tableSelector = "calendars", data = jsonSerializer.Serialize(mySqlCalendars) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var rings = _repo.GetAllRings();
            var mySqlRings = MySQLRing.FromRingList(rings);
            wud = new WnuUploadData { tableSelector = "rings", data = jsonSerializer.Serialize(mySqlRings) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var students = _repo.GetAllStudents();
            var mySqlStudents = MySQLStudent.FromStudentList(students);
            wud = new WnuUploadData { tableSelector = "students", data = jsonSerializer.Serialize(mySqlStudents) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var studentGroups = _repo.GetAllStudentGroups();
            wud = new WnuUploadData { tableSelector = "studentGroups", data = jsonSerializer.Serialize(studentGroups) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var teachers = _repo.GetAllTeachers();
            wud = new WnuUploadData { tableSelector = "teachers", data = jsonSerializer.Serialize(teachers) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var disciplines = _repo.GetAllDisciplines();
            var mySqlDisciplines = MySQLDiscipline.FromDisciplineList(disciplines);
            wud = new WnuUploadData { tableSelector = "disciplines", data = jsonSerializer.Serialize(mySqlDisciplines) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var studentsInGroups = _repo.GetAllStudentsInGroups();
            var mySqlStudentsInGroups = MySQLStudentsInGroups.FromStudentsInGroupsList(studentsInGroups);
            wud = new WnuUploadData { tableSelector = "studentsInGroups", data = jsonSerializer.Serialize(mySqlStudentsInGroups) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var teacherForDisciplines = _repo.GetAllTeacherForDiscipline();
            var mySqlTeacherForDisciplines = MySQLTeacherForDiscipline.FromTeacherForDisciplineList(teacherForDisciplines);
            wud = new WnuUploadData { tableSelector = "teacherForDisciplines", data = jsonSerializer.Serialize(mySqlTeacherForDisciplines) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var lessons = _repo.GetAllLessons();
            var mySqlLessons = MySQLLesson.FromLessonList(lessons);
            wud = new WnuUploadData { tableSelector = "lessons", data = jsonSerializer.Serialize(mySqlLessons) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var configs = _repo.GetAllConfigOptions();
            wud = new WnuUploadData { tableSelector = "configs", data = jsonSerializer.Serialize(configs) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var lessonsLog = _repo.GetAllLessonLogEvents();
            var mySqlLogEvent = MySQLLessonLogEvent.FromLessonLogList(lessonsLog);
            wud = new WnuUploadData { tableSelector = "lessonLogEvents", data = jsonSerializer.Serialize(mySqlLogEvent) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);
            
            var auditoriumEvents = _repo.GetAllAuditoriumEvents();
            var mySqlauditoriumEvents = MySQLAuditoriumEvent.FromAuditoriumEventList(auditoriumEvents);
            wud = new WnuUploadData { tableSelector = "auditoriumEvents", data = jsonSerializer.Serialize(mySqlauditoriumEvents) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);
            
            var faculties = _repo.GetAllFaculties();
            wud = new WnuUploadData { tableSelector = "faculties", data = jsonSerializer.Serialize(faculties) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var gifs = _repo.GetAllGroupsInFaculty();
            var mySqlgifs = MySQLGroupsInFaculty.FromGroupsInFacultyList(gifs);
            wud = new WnuUploadData { tableSelector = "GroupsInFaculties", data = jsonSerializer.Serialize(mySqlgifs) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);
        }

        private void RemovelessonClick(object sender, EventArgs e)
        {
            var removeLessonForm = new RemoveLesson(_repo);
            removeLessonForm.Show();
        }

        private void MainViewCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var source = (List<GroupTableView>)ScheduleView.DataSource;
            var time = source[e.RowIndex].Time;

            var editLessonForm = new EditLesson(_repo, (int)groupList.SelectedValue, e.ColumnIndex, time);
            editLessonForm.ShowDialog();
        }

        private void ОпцииToolStripMenuItemClick(object sender, EventArgs e)
        {
            var configOptionsForm = new ConfigOptionsList(_repo);
            configOptionsForm.Show();
        }

        private void NotEnoughClick(object sender, EventArgs e)
        {
            var stat = new Dictionary<int, int>();
            var result = new Dictionary<string, int>();
            var resByTeacher = new Dictionary<string, int>();

            foreach (var disc in _repo.GetAllDisciplines())
            {
                var disc1 = disc;
                var disctfd = _repo.GetFiltredTeacherForDiscipline(tefd => tefd.Discipline.DisciplineId == disc1.DisciplineId).FirstOrDefault();
                if (disctfd == null)
                {
                    continue;
                }
                var tfd = disctfd;

                var tfdLessons = _repo.GetFiltredLessons(l => l.IsActive && l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId).ToList();

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
            var facultyListForm = new FacultyList(_repo);
            facultyListForm.Show();
        }

        private void ActiveLessonsCount_Click(object sender, EventArgs e)
        {
            var allDiscLessonCount = _repo.GetAllDisciplines().Select(d => d.AuditoriumHours).Sum() / 2;
            var activeLessonsCount = _repo.GetAllActiveLessons().Count();
            var diff = allDiscLessonCount - activeLessonsCount;
            MessageBox.Show(activeLessonsCount + " / " + allDiscLessonCount + " => " + diff, "Пар в расписании/плане");
        }

        private void ManyGroups_Click(object sender, EventArgs e)
        {
            var manyGroupsForm = new MultipleView(_repo);
            manyGroupsForm.Show();
        }

        private void занятостьАудиторийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var audEventsForm = new AuditoriumEventsList(_repo);
            audEventsForm.Show();
        }

        private void teachersHours_Click(object sender, EventArgs e)
        {
            var teacherHoursForm = new teacherHours(_repo);
            teacherHoursForm.Show();
        }

        private void oneAuditorium_Click(object sender, EventArgs e)
        {
            var oneAudForm = new OneAuditorium(_repo);
            oneAudForm.Show();
        }

        private void auditoriums_Click(object sender, EventArgs e)
        {
            var audsForm = new Auditoriums(_repo);
            audsForm.Show();
        }

        private void allChanges_Click(object sender, EventArgs e)
        {
            var allChangesForm = new AllChanges(_repo);
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
                var evts = _repo.GetFiltredLessonLogEvents(evt => evt.DateTime.Date == curDate.Date);
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

            var evts = _repo.GetFiltredLessonLogEvents(lle => lle.DateTime.Date == DateTime.Now.Date);

            var fg = _repo.GetAllGroupsInFaculty()
                .GroupBy(gif => gif.Faculty.FacultyId, 
                         gif => gif.StudentGroup.StudentGroupId)
                .ToList();

            foreach (var ev in evts)
            {
                int studentGroupId = -1;
                if (ev.OldLesson != null)
                {
                    studentGroupId = ev.OldLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;

                    var studentIds = _repo
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroupId)
                    .Select(sig => sig.Student.StudentId)
                    .ToList();

                    var facultyScheduleChanged = new List<int>();

                    foreach (var faculty in fg)
                    {
                        if (_repo.GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId) && faculty.Contains(sig.StudentGroup.StudentGroupId)).Any())
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

                    var studentIds = _repo
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroupId)
                    .Select(sig => sig.Student.StudentId)
                    .ToList();

                    var facultyScheduleChanged = new List<int>();

                    foreach (var faculty in fg)
                    {
                        if (_repo.GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId) && faculty.Contains(sig.StudentGroup.StudentGroupId)).Any())
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
                messageString += _repo.GetFaculty(dowFac.Item1).Letter + " - " + Constants.Constants.DOWLocal[Constants.Constants.DOWRemap[(int)dowFac.Item2]] + Environment.NewLine;
            }

            MessageBox.Show(messageString, "Изменения на сегодня");
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            UpdateViewWidth();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
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

            var lessons = _repo
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

            System.Diagnostics.Process.Start("ExcelData.txt");            
        }

        private void LessonListByTFD_Click(object sender, EventArgs e)
        {
            var lessonListByTFDForm = new LessonListByTFD(_repo);
            lessonListByTFDForm.Show();
        }

        private void LessonListByTeacher_Click(object sender, EventArgs e)
        {
            var lessonListByTeacherForm = new LessonListByTeacher(_repo);
            lessonListByTeacherForm.Show();
        }
        
        private void setLayout_Click(object sender, EventArgs e)
        {
            var width = Screen.PrimaryScreen.WorkingArea.Width;
            var height = Screen.PrimaryScreen.WorkingArea.Height;

            this.Top = 0;
            this.Left = 0;
            this.Width = width / 2;
            this.Height = height / 2;
            
            var audsForm = new Auditoriums(_repo);
            audsForm.Show();
            audsForm.Top = height / 2;
            audsForm.Left = 0;
            audsForm.Width = width / 2;
            audsForm.Height = height / 2;            

        }

        private void setLayout2_Click(object sender, EventArgs e)
        {
            var width = Screen.PrimaryScreen.WorkingArea.Width;
            var height = Screen.PrimaryScreen.WorkingArea.Height;

            var disciplineForm = new DisciplineList(_repo);
            disciplineForm.Show();
            disciplineForm.Top = 0;
            disciplineForm.Left = width / 2;
            disciplineForm.Width = width / 2;
            disciplineForm.Height = (int) Math.Round(height * 0.42);
        }

        private void CreatePDF_Click(object sender, EventArgs e)
        {
            var facultyId = (int)FacultyList.SelectedValue;
            var facultyName = _repo.GetFaculty(facultyId).Name;
            var ruDOW = DOWList.SelectedIndex + 1;

            var facultyDOWLessons = _repo.GetFacultyDOWSchedule(facultyId, ruDOW);
            PDFExport.ExportSchedulePage(facultyDOWLessons, facultyName, "Export.pdf", DOWList.Text, _repo, true, false, false);

            Process.Start("Export.pdf");
            var eprst = 999;
        }

        private void AllInPDF_Click(object sender, EventArgs e)
        {            
            //PDFExport.ExportWholeSchedule("Export.pdf", _repo, false, false, false);

            PDFExport.PrintWholeSchedule(_repo);

            var eprst = 999;
        }

        private void BackupAndUpload_Click(object sender, EventArgs e)
        {
            var DBName = _repo.ExtractDBName(_repo.ConnectionString);

            _repo.BackupDB(Application.StartupPath + "\\" + DBName + ".bak");
            WnuUpload.UploadFile(Application.StartupPath + "\\" + DBName + ".bak", "httpdocs/upload/DB-Backup/" + DBName + ".bak");
        }

        private void DownloadAndRestore_Click(object sender, EventArgs e)
        {
            var wc = new WebClient();
            wc.DownloadFile("http://wiki.nayanova.edu/upload/DB-Backup/" + DBRestoreName.Text + ".bak", Application.StartupPath + "\\" + DBRestoreName.Text + ".bak");
            _repo.RestoreDB(DBRestoreName.Text, Application.StartupPath + "\\" + DBRestoreName.Text + ".bak");
        }

        private void WholeScheduleDatesExport_Click(object sender, EventArgs e)
        {
            ExportWholeScheduleDates("ScheduleDates.txt");
        }

        private void ExportWholeScheduleDates(string filename)
        {
            var groups = _repo
                .GetFiltredStudentGroups(sg => !(sg.Name.Contains("-") || sg.Name.Contains("+") || sg.Name.Contains("I") || sg.Name.Length == 1 || sg.Name.Contains("(Н)") || sg.Name.Contains(".")))
                .ToList();

            foreach (var group in groups)
            {
                var sw = new StreamWriter(filename, true);
                sw.WriteLine(group.Name);
                sw.Close();


                var studentIds = _repo
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                    .Select(sig => sig.Student.StudentId)
                    .ToList();

                var groupIds = _repo
                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .Select(sig => sig.StudentGroup.StudentGroupId)
                    .Distinct()
                    .ToList();

                var tfds = _repo
                    .GetFiltredTeacherForDiscipline(tfd => groupIds.Contains(tfd.Discipline.StudentGroup.StudentGroupId))
                    .ToList();

                foreach (var tfd in tfds)
                {
                    var lessons = _repo.GetFiltredLessons(l =>
                        l.IsActive &&
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
    }
}
