using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.MainImport;
using Auditorium = UchOtd.Schedule.MainImport.Auditorium;
using AuditoriumEvent = UchOtd.Schedule.MainImport.AuditoriumEvent;
using Building = UchOtd.Schedule.MainImport.Building;
using Calendar = UchOtd.Schedule.MainImport.Calendar;
using Discipline = UchOtd.Schedule.MainImport.Discipline;
using Faculty = UchOtd.Schedule.MainImport.Faculty;
using GroupsInFaculty = UchOtd.Schedule.MainImport.GroupsInFaculty;
using Lesson = UchOtd.Schedule.MainImport.Lesson;
using LessonLogEvent = UchOtd.Schedule.MainImport.LessonLogEvent;
using Ring = UchOtd.Schedule.MainImport.Ring;
using Student = UchOtd.Schedule.MainImport.Student;
using StudentGroup = UchOtd.Schedule.MainImport.StudentGroup;
using StudentsInGroups = UchOtd.Schedule.MainImport.StudentsInGroups;
using Teacher = UchOtd.Schedule.MainImport.Teacher;
using TeacherForDiscipline = UchOtd.Schedule.MainImport.TeacherForDiscipline;

namespace UchOtd.Schedule.Core
{
    public static class MainImport
    {
        public static void Execute(ScheduleRepository repo, ToolStripStatusLabel status, MainEditForm form)
        {
            var txtPath = "D:\\DbUnited\\";
            var filesToImport = new List<string>
            {
                "Schedule13141.txt",
                "Schedule13142.txt",
                "Schedule14151.txt",
                "Schedule14152.txt",
                "Schedule15161.txt",
                "Schedule15162.txt",
                "Schedule16171.txt",
                "Schedule16172.txt"
            };

            // 3  Calendars
            var startingDate = new DateTime(2013, 9, 1);
            var endDate = new DateTime(2017, 8, 31);

            var date = startingDate;

            var calendarIds = new Dictionary<DateTime, int>();
            do
            {
                var calendar = new global::Schedule.DomainClasses.Main.Calendar { Date = date, State = 0 };
                repo.Calendars.AddCalendar(calendar);

                calendarIds.Add(date.Date, calendar.CalendarId);

                date = date.AddDays(1);
            } while (date <= endDate);

            for (int i = 0; i < filesToImport.Count; i++)
            {
                var filename = txtPath + filesToImport[i];
                var semesterString = filesToImport[i].Replace(".txt", "");
                semesterString = semesterString.Substring(Math.Max(0, semesterString.Length - 5));
                semesterString = semesterString.Insert(2, "-").Insert(5, "-");

                var semester = repo.Semesters.GetFirstFiltredSemester(s => s.DisplayName == semesterString);
                if (semester == null)
                {
                    var startingYear = int.Parse(semesterString.Substring(0, 2)) + 2000;
                    var semesterInYear = int.Parse(semesterString.Substring(semesterString.Length - 1));

                    semester = new Semester()
                    {
                        StartingYear = startingYear,
                        SemesterInYear = semesterInYear,
                        DisplayName = semesterString
                    };

                    repo.Semesters.AddSemester(semester);
                }

                var sr = new StreamReader(filename);
                string line;

                line = sr.ReadLine(); // 1  Auditoriums
                List<Auditorium> auditoriums = JsonConvert.DeserializeObject<List<Auditorium>>(line);

                line = sr.ReadLine(); // 2  Buildings
                List<Building> buildings = JsonConvert.DeserializeObject<List<Building>>(line);

                line = sr.ReadLine(); // 3  Calendars
                List<Calendar> calendars = JsonConvert.DeserializeObject<List<Calendar>>(line);

                line = sr.ReadLine(); // 4  Rings
                List<Ring> rings = JsonConvert.DeserializeObject<List<Ring>>(line);

                line = sr.ReadLine(); // 5  Students
                List<Student> students = JsonConvert.DeserializeObject<List<Student>>(line);

                line = sr.ReadLine(); // 6  StudentGroups
                List<StudentGroup> studentGroups = JsonConvert.DeserializeObject<List<StudentGroup>>(line);

                line = sr.ReadLine(); // 7  Teachers
                List<Teacher> teachers = JsonConvert.DeserializeObject<List<Teacher>>(line);

                line = sr.ReadLine(); // 8  Disciplines
                List<Discipline> disciplines = JsonConvert.DeserializeObject<List<Discipline>>(line);

                line = sr.ReadLine(); // 9  TeacherForDisciplines
                List<TeacherForDiscipline> teacherForDiscviplines = JsonConvert.DeserializeObject<List<TeacherForDiscipline>>(line);

                line = sr.ReadLine(); // 10 Lessons
                List<Lesson> lessons = JsonConvert.DeserializeObject<List<Lesson>>(line);

                line = sr.ReadLine(); // 11 ConfigOptions
                List<ConfigOption> configOptions = JsonConvert.DeserializeObject<List<ConfigOption>>(line);

                line = sr.ReadLine(); // 12 LessonLogEvents
                List<LessonLogEvent> lessonLogEvents = JsonConvert.DeserializeObject<List<LessonLogEvent>>(line);

                line = sr.ReadLine(); // 13 AuditoriumEvents
                List<AuditoriumEvent> auditoriumsEvents = JsonConvert.DeserializeObject<List<AuditoriumEvent>>(line);

                line = sr.ReadLine(); // 14 Faculties
                List<Faculty> faculties = JsonConvert.DeserializeObject<List<Faculty>>(line);

                line = sr.ReadLine(); // 15 GroupsInFaculties
                List<GroupsInFaculty> groupsInFaculties = JsonConvert.DeserializeObject<List<GroupsInFaculty>>(line);

                line = sr.ReadLine(); // 16 Exams
                List<Exam> exams = JsonConvert.DeserializeObject<List<Exam>>(line);

                line = sr.ReadLine(); // 17 ExamLogEvents
                List<LogEvent> logEvents = JsonConvert.DeserializeObject<List<LogEvent>>(line);

                line = sr.ReadLine(); // 18 StudentsInGroups
                List<StudentsInGroups> studentsInGroups = JsonConvert.DeserializeObject<List<StudentsInGroups>>(line);

                // 3  Calendars
                for (int j = 0; j < calendars.Count; j++)
                {
                    var calendar = calendars[j];

                    var dbCalendar = repo.Calendars.GetFirstFiltredCalendar(c => c.Date.Date == calendar.Date.Date);

                    if (dbCalendar != null)
                    {
                        if (calendar.State != 0)
                        {
                            dbCalendar.State = calendar.State;
                            repo.Calendars.UpdateCalendar(dbCalendar);
                        }
                    }
                    else
                    {
                        var newCalendar = new global::Schedule.DomainClasses.Main.Calendar { Date = calendar.Date, State = calendar.State };
                        repo.Calendars.AddCalendar(newCalendar);

                        calendarIds.Add(newCalendar.Date.Date, newCalendar.CalendarId);
                    }
                }

                // 2  Buildings
                var BuildingIds = new Dictionary<int, int>();
                for (int j = 0; j < buildings.Count; j++)
                {
                    var building = buildings[j];

                    var searchBuilding = repo.Buildings.GetFirstFiltredBuilding(b => b.Name == building.Name);
                    if (searchBuilding == null)
                    {

                        var newBuilding = new global::Schedule.DomainClasses.Main.Building()
                        {
                            Name = building.Name
                        };
                        
                        repo.Buildings.AddBuilding(newBuilding);

                        BuildingIds.Add(building.BuildingId, newBuilding.BuildingId);
                    }
                    else
                    {
                        BuildingIds.Add(building.BuildingId, searchBuilding.BuildingId);
                    }
                }
                
                // 1  Auditoriums
                var auditoriumIds = new Dictionary<int, int>();
                for (int j = 0; j < auditoriums.Count; j++)
                {
                    var auditorium = auditoriums[j];

                    var searchAuditorium = repo.Auditoriums.Find(a => a.Name == auditorium.Name);
                    if (searchAuditorium == null)
                    {
                        var newAuditorium = new global::Schedule.DomainClasses.Main.Auditorium()
                        {
                            Name = auditorium.Name,
                            Building = repo.Buildings.GetBuilding(BuildingIds[auditorium.Building.BuildingId])
                        };

                        repo.Auditoriums.Add(newAuditorium);

                        auditoriumIds.Add(auditorium.AuditoriumId, newAuditorium.AuditoriumId);
                    }
                    else
                    {
                        auditoriumIds.Add(auditorium.AuditoriumId, searchAuditorium.AuditoriumId);
                    }
                }

                // 4  Rings
                var ringIds = new Dictionary<int, int>();
                for (int j = 0; j < rings.Count; j++)
                {
                    var ring = rings[j];

                    var searchRing = repo.Rings.GetFirstFiltredRing(r => r.Time.TimeOfDay == ring.Time.TimeOfDay);
                    if (searchRing == null)
                    {
                        var newRing = new global::Schedule.DomainClasses.Main.Ring()
                        {
                            Time = ring.Time
                        };

                        repo.Rings.AddRing(newRing);

                        ringIds.Add(ring.RingId, newRing.RingId);
                    }
                    else
                    {
                        ringIds.Add(ring.RingId, searchRing.RingId);
                    }
                }

                // 5  Students
                var studentIds = new Dictionary<int, int>();
                for (int j = 0; j < students.Count; j++)
                {
                    var student = students[j];

                    var searchStudent = repo.Students.GetFirstFiltredStudents(st => st.F.Trim() == student.F.Trim() && st.I.Trim() == student.I.Trim() && st.O.Trim() == student.O.Trim());
                    if (searchStudent == null)
                    {
                        var newStudent = new global::Schedule.DomainClasses.Main.Student()
                        {
                            Address = student.Address,
                            BirthDate = student.BirthDate,
                            F = student.F,
                            I = student.I,
                            NFactor = student.NFactor,
                            O = student.O,
                            Orders = student.Orders,
                            PaidEdu = student.PaidEdu,
                            Phone = student.Phone,
                            Starosta = student.Starosta,
                            ZachNumber = student.ZachNumber
                        };

                        repo.Students.AddStudent(newStudent);

                        studentIds.Add(student.StudentId, newStudent.StudentId);
                    }
                    else
                    {
                        if (student.Orders.Length > searchStudent.Orders.Length)
                        {
                            searchStudent.Orders = student.Orders;
                            repo.Students.UpdateStudent(searchStudent);
                        }

                        studentIds.Add(student.StudentId, searchStudent.StudentId);
                    }
                }

                sr.Close();

                form.Invoke(new Action(() =>
                {
                    status.Text = filesToImport[i] + " done";
                }));
            }

            form.Invoke(new Action(() =>
            {
                status.Text = "Done";
            }));

            var eprst = 999;
        }
    }
}
