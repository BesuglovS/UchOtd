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

                    var searchStudent = repo.Students.GetFirstFiltredStudents(st => st.ZachNumber == student.ZachNumber);
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

                // 6  StudentGroups
                var studentGroupIds = new Dictionary<int, int>();
                for (int j = 0; j < studentGroups.Count; j++)
                {
                    var studentGroup = studentGroups[j];
                    
                    var newStudentGroup = new global::Schedule.DomainClasses.Main.StudentGroup()
                    {
                        Name = studentGroup.Name,
                        Semester = semester
                    };

                    repo.StudentGroups.AddStudentGroup(newStudentGroup);

                    studentGroupIds.Add(studentGroup.StudentGroupId, newStudentGroup.StudentGroupId);
                }

                // 18 StudentsInGroups
                var studentInGroupIds = new Dictionary<int, int>();
                for (int j = 0; j < studentsInGroups.Count; j++)
                {
                    var studentInGroup = studentsInGroups[j];
                    
                    var newStudentInGroups = new global::Schedule.DomainClasses.Main.StudentsInGroups()
                    {
                        PeriodFrom = (semester.SemesterInYear == 1) ? (new DateTime(semester.StartingYear,  9,  1)) : (new DateTime(semester.StartingYear + 1, 2,  1)),
                        PeriodTo   = (semester.SemesterInYear == 1) ? (new DateTime(semester.StartingYear, 12, 31)) : (new DateTime(semester.StartingYear + 1, 8, 31)),
                        Student = repo.Students.GetStudent(studentIds[studentInGroup.Student.StudentId]),
                        StudentGroup = repo.StudentGroups.GetStudentGroup(studentGroupIds[studentInGroup.StudentGroup.StudentGroupId]) 
                    };

                    repo.StudentsInGroups.AddStudentsInGroups(newStudentInGroups);

                    studentInGroupIds.Add(studentInGroup.StudentsInGroupsId, newStudentInGroups.StudentsInGroupsId);
                }

                // 7  Teachers
                var teacherIds = new Dictionary<int, int>();
                for (int j = 0; j < teachers.Count; j++)
                {
                    var teacher = teachers[j];

                    var searchTeacher = repo.Teachers.GetFirstFiltredTeachers(t => t.FIO == teacher.FIO);
                    if (searchTeacher == null)
                    {
                        var newTeacher = new global::Schedule.DomainClasses.Main.Teacher()
                        {
                            FIO = teacher.FIO,
                            Phone = teacher.Phone
                        };

                        repo.Teachers.AddTeacher(newTeacher);

                        teacherIds.Add(teacher.TeacherId, newTeacher.TeacherId);
                    }
                    else
                    {
                        teacherIds.Add(teacher.TeacherId, searchTeacher.TeacherId);
                    }
                }

                // 8  Disciplines
                var disciplineIds = new Dictionary<int, int>();
                for (int j = 0; j < disciplines.Count; j++)
                {
                    var discipline = disciplines[j];

                    var newDiscipline = new global::Schedule.DomainClasses.Main.Discipline()
                    {
                        Semester = semester,
                        Name = discipline.Name,
                        Attestation = discipline.Attestation,
                        AuditoriumHours = discipline.AuditoriumHours,
                        AuditoriumHoursPerWeek = discipline.AuditoriumHoursPerWeek,
                        ControlTask = discipline.ControlTask,
                        CourseProject = discipline.CourseProject,
                        CourseTask = discipline.CourseTask,
                        Essay = discipline.Essay,
                        LectureHours = discipline.LectureHours,
                        PracticalHours = discipline.PracticalHours,
                        Referat = discipline.Referat,
                        TypeSequence = discipline.TypeSequence,
                        StudentGroup = repo.StudentGroups.GetStudentGroup(studentGroupIds[discipline.StudentGroup.StudentGroupId])
                    };

                    repo.Disciplines.AddDiscipline(newDiscipline);
                    
                    disciplineIds.Add(discipline.DisciplineId, newDiscipline.DisciplineId);
                }

                // 9  TeacherForDisciplines
                var teacherForDisciplineIds = new Dictionary<int, int>();
                for (int j = 0; j < teacherForDiscviplines.Count; j++)
                {
                    var teacherForDiscipline = teacherForDiscviplines[j];

                    var newtfd = new global::Schedule.DomainClasses.Main.TeacherForDiscipline()
                    {
                        Teacher = repo.Teachers.GetTeacher(teacherIds[teacherForDiscipline.Teacher.TeacherId]),
                        Discipline = repo.Disciplines.GetDiscipline(disciplineIds[teacherForDiscipline.Discipline.DisciplineId])
                    };

                    repo.TeacherForDisciplines.AddTeacherForDiscipline(newtfd);

                    teacherForDisciplineIds.Add(teacherForDiscipline.TeacherForDisciplineId, newtfd.TeacherForDisciplineId);
                }

                // 10 Lessons
                var lessonIds = new Dictionary<int, int>();
                for (int j = 0; j < lessons.Count; j++)
                {
                    var lesson = lessons[j];

                    var newLesson = new global::Schedule.DomainClasses.Main.Lesson()
                    {
                        State = lesson.State,
                        Auditorium = repo.Auditoriums.Get(auditoriumIds[lesson.Auditorium.AuditoriumId]),
                        Calendar = repo.Calendars.GetCalendar(calendarIds[lesson.Calendar.Date.Date]),
                        Ring = repo.Rings.GetRing(ringIds[lesson.Ring.RingId]),
                        TeacherForDiscipline = repo.TeacherForDisciplines.GetTeacherForDiscipline(teacherForDisciplineIds[lesson.TeacherForDiscipline.TeacherForDisciplineId])
                    };

                    repo.Lessons.AddLesson(newLesson);

                    lessonIds.Add(lesson.LessonId, newLesson.LessonId);
                }

                // 11 ConfigOptions
                for (int j = 0; j < configOptions.Count; j++)
                {
                    var configoption = configOptions[j];

                    var newConfigOption = new global::Schedule.DomainClasses.Config.ConfigOption()
                    {
                        Semester = semester,
                        Key = configoption.Key,
                        Value = configoption.Value
                    };

                    repo.ConfigOptions.AddConfigOption(newConfigOption);
                }

                // 12 LessonLogEvents
                var lessonLogEventIds = new Dictionary<int, int>();
                for (int j = 0; j < lessonLogEvents.Count; j++)
                {
                    var lessonLogEvent = lessonLogEvents[j];

                    var newLessonLogEvent = new global::Schedule.DomainClasses.Main.LessonLogEvent()
                    {
                        DateTime = lessonLogEvent.DateTime,
                        HiddenComment = lessonLogEvent.HiddenComment,
                        PublicComment = lessonLogEvent.PublicComment,
                        OldLesson = (lessonLogEvent.OldLesson != null) ? repo.Lessons.GetLesson(lessonIds[lessonLogEvent.OldLesson.LessonId]) : null,
                        NewLesson = (lessonLogEvent.NewLesson != null) ? repo.Lessons.GetLesson(lessonIds[lessonLogEvent.NewLesson.LessonId]) : null
                    };

                    repo.LessonLogEvents.AddLessonLogEvent(newLessonLogEvent);

                    lessonLogEventIds.Add(lessonLogEvent.LessonLogEventId, newLessonLogEvent.LessonLogEventId);
                }

                // 13 AuditoriumEvents
                for (int j = 0; j < auditoriumsEvents.Count; j++)
                {
                    var auditoriumEvent = auditoriumsEvents[j];

                    var newAuditoriumEvents = new global::Schedule.DomainClasses.Main.AuditoriumEvent()
                    {
                        Name = auditoriumEvent.Name,
                        Calendar = repo.Calendars.GetCalendar(calendarIds[auditoriumEvent.Calendar.Date.Date]),
                        Ring = repo.Rings.GetRing(ringIds[auditoriumEvent.Ring.RingId]),
                        Auditorium = repo.Auditoriums.Get(auditoriumIds[auditoriumEvent.Auditorium.AuditoriumId])
                    };

                    repo.AuditoriumEvents.AddAuditoriumEvent(newAuditoriumEvents);
                }


                // 14 Faculties
                var facultyIds = new Dictionary<int, int>();
                for (int j = 0; j < faculties.Count; j++)
                {
                    var faculty = faculties[j];
                    
                    var searchFaculty = repo.Faculties.GetFirstFiltredFaculty(f => f.Name == faculty.Name);
                    if (searchFaculty == null)
                    {
                        var newFaculty = new global::Schedule.DomainClasses.Main.Faculty()
                        {
                            Name = faculty.Name,
                            DeanSigningSchedule = faculty.DeanSigningSchedule,
                            DeanSigningSessionSchedule = faculty.DeanSigningSessionSchedule,
                            Letter = faculty.Letter,
                            ScheduleSigningTitle = faculty.ScheduleSigningTitle,
                            SessionSigningTitle = faculty.SessionSigningTitle,
                            SortingOrder = faculty.SortingOrder
                        };

                        repo.Faculties.AddFaculty(newFaculty);

                        facultyIds.Add(faculty.FacultyId, newFaculty.FacultyId);
                    }
                    else
                    {
                        facultyIds.Add(faculty.FacultyId, searchFaculty.FacultyId);
                    }

                }

                // 15 GroupsInFaculties
                for (int j = 0; j < groupsInFaculties.Count; j++)
                {
                    var groupInFaculty = groupsInFaculties[j];

                    var newGroupInFaculty = new global::Schedule.DomainClasses.Main.GroupsInFaculty()
                    {
                        StudentGroup = repo.StudentGroups.GetStudentGroup(studentGroupIds[groupInFaculty.StudentGroup.StudentGroupId]),
                        Faculty = repo.Faculties.GetFaculty(facultyIds[groupInFaculty.Faculty.FacultyId])
                    };

                    repo.GroupsInFaculties.AddGroupsInFaculty(newGroupInFaculty);
                }

                // 16 Exams
                var examIds = new Dictionary<int, int>();
                for (int j = 0; j < exams.Count; j++)
                {
                    var exam = exams[j];

                    global::Schedule.DomainClasses.Session.Exam newExam;

                    if (disciplineIds.ContainsKey(exam.DisciplineId))
                    {
                        newExam = new global::Schedule.DomainClasses.Session.Exam()
                        {
                            DisciplineId = disciplineIds[exam.DisciplineId],
                            ConsultationAuditoriumId = exam.ConsultationAuditoriumId,
                            ConsultationDateTime = exam.ConsultationDateTime,
                            ExamAuditoriumId = exam.ExamAuditoriumId,
                            ExamDateTime = exam.ExamDateTime,
                            IsActive = exam.IsActive
                        };

                        repo.Exams.AddExam(newExam);

                        examIds.Add(exam.ExamId, newExam.ExamId);
                    }
                }

                // 17 ExamLogEvents
                for (int j = 0; j < logEvents.Count; j++)
                {
                    var logEvent = logEvents[j];

                    if ((logEvent.OldExam != null && examIds.ContainsKey(logEvent.OldExam.ExamId)) &&
                        (logEvent.NewExam != null && examIds.ContainsKey(logEvent.NewExam.ExamId)))
                    {
                        var newLogEvent = new global::Schedule.DomainClasses.Session.LogEvent()
                        {
                            DateTime = logEvent.DateTime,
                            OldExam = (logEvent.OldExam != null)
                                ? (repo.Exams.GetExam(examIds[logEvent.OldExam.ExamId]))
                                : null,
                            NewExam = (logEvent.NewExam != null)
                                ? (repo.Exams.GetExam(examIds[logEvent.NewExam.ExamId]))
                                : null
                        };

                        repo.LogEvents.Add(newLogEvent);
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
