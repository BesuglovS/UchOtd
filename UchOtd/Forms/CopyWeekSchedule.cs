using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Repositories.Common;
using UchOtd.NUDS.Core;

namespace UchOtd.Forms
{
    public partial class CopyWeekSchedule : Form
    {
        private readonly ScheduleRepository _repo;

        public CopyWeekSchedule(ScheduleRepository repo)
        {
            _repo = repo;
            InitializeComponent();
        }

        private void CopyWeekSchedule_Load(object sender, EventArgs e)
        {
            var groups = _repo
                .StudentGroups
                .GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            groupList.ValueMember = "StudentGroupId";
            groupList.DisplayMember = "Name";
            groupList.DataSource = groups;

            var faculties = _repo
                .Faculties
                .GetAllFaculties()
                .OrderBy(f => f.SortingOrder)
                .ToList();

            facultyList.DisplayMember = "Letter";
            facultyList.ValueMember = "FacultyId";
            facultyList.DataSource = faculties;

        }

        private async void Go_Click(object sender, EventArgs e)
        {
            var group = (StudentGroup)groupList.SelectedItem;
            
            List<int> dow = GetDow();

            var weekFrom = -1;
            int.TryParse(fromWeek.Text, out weekFrom);

            var weekToList = new List<int>();
            var result = Utilities.getWeeksFromString(out weekToList, toWeek.Text);

            var faculty = (Faculty)facultyList.SelectedItem;

            if (copyGroup.Checked)
            {
                for (int i = 0; i < weekToList.Count; i++)
                {
                    var weekToItem1 = weekToList[i];
                    await Task.Run(() => { CopyGroupSchedule(group, weekFrom, weekToItem1, dow); });
                }
            }

            if (copyFaculty.Checked)
            {
                for (int i = 0; i < weekToList.Count; i++)
                {
                    var weekToItem2 = weekToList[i];
                    await Task.Run(() => { CopyFacultySchedule(faculty, weekFrom, weekToItem2, dow); });                    
                }
            }
        }

        private List<int> StudentGroupIdsFromGroupId(int groupId)
        {
            var studentIds = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId && !sig.Student.Expelled)
                .Select(stig => stig.Student.StudentId)
                .ToList();

            var groupsListIds = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                .Select(stig => stig.StudentGroup.StudentGroupId)
                .Distinct()
                .ToList();
            return groupsListIds;
        }

        private async void CopyGroupSchedule(StudentGroup group, int weekFrom, int weekTo, List<int> dow)
        {
            await Task.Run(() =>
            {
                CopyGroupWeekSchedule(@group, weekFrom, weekTo, dow);
            });

            status.Text = "Готово";
        }

        private List<int> GetDow()
        {
            var dow = new List<int>();
            if (Mon.Checked) dow.Add(1);
            if (Tue.Checked) dow.Add(2);
            if (Wed.Checked) dow.Add(3);
            if (Thu.Checked) dow.Add(4);
            if (Fri.Checked) dow.Add(5);
            if (Sat.Checked) dow.Add(6);
            return dow;
        }

        private void CopyGroupWeekSchedule(StudentGroup @group, int weekFrom, int weekTo, List<int> dow)
        {
            if (weekFrom == weekTo)
            {
                return;
            }

            var groupIds = StudentGroupIdsFromGroupId(@group.StudentGroupId);

            var cf = new CommonFunctions(_repo);
            cf.ConnectionString = _repo.GetConnectionString();

            var calendarPairs = new Dictionary<Calendar, Calendar>();

            for (int i = 0; i < dow.Count; i++)
            {
                var dayOfWeek = dow[i];
                Calendar c1, c2;
                c1 = cf.GetCalendarFromDowAndWeek(dayOfWeek, weekFrom);
                c2 = cf.GetCalendarFromDowAndWeek(dayOfWeek, weekTo);
                if (c1 != null && c2 != null)
                {
                    calendarPairs.Add(c1, c2);
                }
            }

            foreach (var calendarPair in calendarPairs)
            {
                var dayLessons = _repo
                    .Lessons
                    .GetFiltredLessons(l => l.State == 1 &&
                                            l.Calendar.CalendarId == calendarPair.Key.CalendarId &&
                                            groupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId));

                foreach (var lesson in dayLessons)
                {
                    var newLesson = new Lesson(lesson.TeacherForDiscipline, calendarPair.Value, lesson.Ring, lesson.Auditorium);
                    newLesson.State = 1;

                    _repo.Lessons.AddLesson(newLesson);
                }

                Invoke((MethodInvoker)delegate
                {
                    status.Text = @group.Name + ": " + calendarPair.Key.Date.ToString("dd.MM.yyyy") + " - " + calendarPair.Value.Date.ToString("dd.MM.yyyy");
                    // runs on UI thread
                });
            }
        }

        private async void CopyFacultySchedule(Faculty faculty, int weekFrom, int weekTo, List<int> dow)
        {
            var facultyGroups =
                _repo.GroupsInFaculties.GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                .Select(gif => gif.StudentGroup);

            foreach (var studentGroup in facultyGroups)
            {
                await Task.Run(() =>
                {
                    CopyGroupWeekSchedule(studentGroup, weekFrom, weekTo, dow);
                });
            }

            Invoke((MethodInvoker)delegate
            {
                status.Text = "Готово"; 
            });
        }

        private async void deleteWeekSchedule_Click(object sender, EventArgs e)
        {
            List<int> dow = GetDow();

            var weekList = new List<int>();
            var result = Utilities.getWeeksFromString(out weekList, fromWeek.Text);
            
            var group = (StudentGroup)groupList.SelectedItem;

            if (copyGroup.Checked)
            {
                await Task.Run(async () =>
                {
                    for (int i = 0; i < weekList.Count; i++)
                    {
                        var weekItem = weekList[i];
                        await DeleteGroupSchedule(group, weekItem, dow);
                    }
                    
                });
            }

            if (copyFaculty.Checked)
            {
                var faculty = (Faculty)facultyList.SelectedItem;

                for (int i = 0; i < weekList.Count; i++)
                {
                    var weekItem = weekList[i];
                    await DeleteFacultySchedule(faculty, weekItem, dow);
                }                
            }
        }

        private async Task<int> DeleteGroupSchedule(StudentGroup group, int weekFrom, List<int> dow)
        {
            Invoke((MethodInvoker)delegate
            {
                status.Text = @group.Name + " удаляем (" + weekFrom.ToString() + ")";
                // runs on UI thread
            });

            var groupIds = StudentGroupIdsFromGroupId(@group.StudentGroupId);

            var cf = new CommonFunctions(_repo) {ConnectionString = _repo.GetConnectionString()};

            var calendarList = new List<Calendar>();



            for (int i = 0; i < dow.Count; i++)
            {
                var doyOfWeek = dow[i];
                Calendar c1;
                c1 = cf.GetCalendarFromDowAndWeek(doyOfWeek, weekFrom);
                
                if (c1 != null)
                {
                    calendarList.Add(c1);
                }
            }

            var calendarIds = calendarList.Select(c => c.CalendarId).ToList();

            
            var weekLessons = _repo
                .Lessons
                .GetFiltredLessons(l => l.State == 1 &&
                                        calendarIds.Contains(l.Calendar.CalendarId) &&
                                        groupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId));

            var lessonIds = weekLessons.Select(l => l.LessonId).ToList();
            var lessonEvents = _repo.LessonLogEvents.GetFiltredLessonLogEvents(le =>
                (le.OldLesson != null && lessonIds.Contains(le.OldLesson.LessonId)) ||
                (le.NewLesson != null && lessonIds.Contains(le.NewLesson.LessonId)));

            foreach (var lessonLogEvent in lessonEvents)
            {
                _repo.LessonLogEvents.RemoveLessonLogEvent(lessonLogEvent.LessonLogEventId);
            }

            foreach (var lesson in weekLessons)
            {
                _repo.Lessons.RemoveLesson(lesson.LessonId);
            }
            
            Invoke((MethodInvoker)delegate
            {
                status.Text = @group.Name + " готово (" + weekFrom.ToString() + ")";
                // runs on UI thread
            });

            return 0;
        }

        private async Task<int> DeleteFacultySchedule(Faculty faculty, int weekFrom, List<int> dow)
        {
            var facultyGroups =
                _repo.GroupsInFaculties.GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                    .Select(gif => gif.StudentGroup);

            foreach (var studentGroup in facultyGroups)
            {
                await Task.Run(async () =>
                {
                    await DeleteGroupSchedule(studentGroup, weekFrom, dow);
                });
            }

            status.Text = "Готово";

            return 0;
        }

        private async void FacultyScheduleFromGroup_Click(object sender, EventArgs e)
        {
            var faculty = (Faculty)facultyList.SelectedItem;
            var group = (StudentGroup)groupList.SelectedItem;

            var weekFrom = -1;
            try
            {
                weekFrom = int.Parse(fromWeek.Text);
            }
            catch
            {
                return;
            }

            await Task.Run(() =>
            {
                CreateFacultyScheduleFromGroup(faculty, @group, weekFrom);
            });
        }

        private void CreateFacultyScheduleFromGroup(Faculty faculty, StudentGroup @group, int weekFrom)
        {
            var facultyGroups =
                _repo.GroupsInFaculties
                    .GetFiltredGroupsInFaculty(
                        gif => gif.Faculty.FacultyId == faculty.FacultyId &&
                        gif.StudentGroup.StudentGroupId != group.StudentGroupId)
                    .Select(gif => gif.StudentGroup);

            foreach (var studentGroup in facultyGroups)
            {
                var groupIds = StudentGroupIdsFromGroupId(@group.StudentGroupId);

                var cf = new CommonFunctions(_repo);
                cf.ConnectionString = _repo.GetConnectionString();

                var calendarList = new List<Calendar>();

                for (int i = 1; i <= 7; i++)
                {
                    Calendar c1;
                    c1 = cf.GetCalendarFromDowAndWeek(i, weekFrom);
                    if (c1 != null )
                    {
                        calendarList.Add(c1);
                    }
                }

                foreach (var calendar in calendarList)
                {
                    var dayLessons = _repo
                        .Lessons
                        .GetFiltredLessons(l => l.State == 1 &&
                                                l.Calendar.CalendarId == calendar.CalendarId &&
                                                groupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId));
                    var lessonRings = dayLessons.Select(l => l.Ring).ToList();

                    if (dayLessons.Count == 0) continue;

                    var seq = RandomizeSeq(dayLessons.Count);

                    for (int i = 0; i < dayLessons.Count; i++)
                    {
                        // random order
                        var lesson = dayLessons[seq[i]];

                        var discName = lesson.TeacherForDiscipline.Discipline.Name;

                        var disc = _repo.Disciplines.GetFirstFiltredDisciplines(d =>
                            d.Name == discName &&
                            d.StudentGroup.StudentGroupId == studentGroup.StudentGroupId);

                        if (disc == null) continue;

                        var tefd = _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(
                                tfd => tfd.Discipline.DisciplineId == disc.DisciplineId);

                        if (tefd == null) continue;

                        var newLesson = new Lesson(tefd, calendar, lessonRings[i], _repo.Auditoriums.getFreeAud(calendar.CalendarId, lessonRings[i].RingId, lesson.Auditorium.Building.BuildingId));
                        newLesson.State = 1;

                        _repo.Lessons.AddLessonWoLog(newLesson);
                    }

                    Invoke((MethodInvoker)delegate
                    {
                        status.Text = @group.Name + ": " + calendar.Date.ToString("dd.MM.yyyy");
                        // runs on UI thread
                    });
                }
            }

            Invoke((MethodInvoker)delegate
            {
                status.Text = "Готово";
                // runs on UI thread
            });
        }

        

        private List<int> RandomizeSeq(int count)
        {
            var result = Enumerable.Range(0, count).ToList();
            var r = new Random();

            for (int i = 0; i < 100; i++)
            {
                var src = r.Next(0, count);
                var dest = r.Next(0, count);

                // swap
                var swap = result[dest];
                result[dest] = result[src];
                result[src] = swap;

            }

            return result;
        }

        private async Task MoveScheduleByRings(Dictionary<TimeSpan, TimeSpan> rings)
        {
            List<int> dow = GetDow();

            var week = -1;
            try
            {
                week = int.Parse(fromWeek.Text);
            }
            catch
            {
                return;
            }

            var faculty = (Faculty) facultyList.SelectedItem;
            var group = (StudentGroup) groupList.SelectedItem;

            if (copyGroup.Checked)
            {
                await Task.Run(() => { MoveGroupSchedule(@group, week, dow, rings); });
            }

            if (copyFaculty.Checked)
            {
                await Task.Run(() => { MoveFacultySchedule(faculty, week, dow, rings); });
            }

            Invoke((MethodInvoker)delegate
            {
                status.Text = "Готово";
            });
        }

        private async void MoveFacultySchedule(Faculty faculty, int week, List<int> dow, Dictionary<TimeSpan, TimeSpan> rings)
        {
            var facultyGroups =
                _repo.GroupsInFaculties.GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                    .Select(gif => gif.StudentGroup);

            foreach (var studentGroup in facultyGroups)
            {
                await Task.Run(() =>
                {
                    MoveGroupSchedule(studentGroup, week, dow, rings);
                });
            }
        }

        private void MoveGroupSchedule(StudentGroup @group, int week, List<int> dow, Dictionary<TimeSpan, TimeSpan> rings)
        {
            var ringsTransformDict = RingsFromTimeSpans(rings);
            
            var groupIds = StudentGroupIdsFromGroupId(@group.StudentGroupId);

            var cf = new CommonFunctions(_repo) { ConnectionString = _repo.GetConnectionString() };

            var calendarList = new List<Calendar>();

            for (int i = 0; i < dow.Count; i++)
            {
                var dayOfWeek = dow[i];
                var c1 = cf.GetCalendarFromDowAndWeek(dayOfWeek, week);
                if (c1 != null)
                {
                    calendarList.Add(c1);
                }
            }

            foreach (var calendar in calendarList)
            {
                var dayLessons = _repo
                    .Lessons
                    .GetFiltredLessons(l => l.State == 1 &&
                                            l.Calendar.CalendarId == calendar.CalendarId &&
                                            groupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId));

                

                foreach (var lesson in dayLessons)
                {
                    if (!ringsTransformDict.ContainsKey(lesson.Ring.RingId))
                    {
                        continue;
                    }

                    lesson.Ring = ringsTransformDict[lesson.Ring.RingId];
                    _repo.Lessons.UpdateLesson(lesson);
                }

                var dayOfWeek = GetRuDow(calendar.Date);

                Invoke((MethodInvoker)delegate
                {
                    status.Text = @group.Name + ": " + calendar.Date.ToString("dd.MM.yyyy") + " - " + Constants.DowRu[dayOfWeek];
                    // runs on UI thread
                });
            }

        }

        private static int GetRuDow(DateTime date)
        {
            int dow = -1;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dow = 1;
                    break;
                case DayOfWeek.Tuesday:
                    dow = 2;
                    break;
                case DayOfWeek.Wednesday:
                    dow = 3;
                    break;
                case DayOfWeek.Thursday:
                    dow = 4;
                    break;
                case DayOfWeek.Friday:
                    dow = 5;
                    break;
                case DayOfWeek.Saturday:
                    dow = 6;
                    break;
                case DayOfWeek.Sunday:
                    dow = 7;
                    break;
            }

            return dow;
        }

        private Dictionary<int, Ring> RingsFromTimeSpans(Dictionary<TimeSpan, TimeSpan> rings)
        {
            var ringIdDict = new Dictionary<int, Ring>();

            var allRings = _repo.Rings.GetAllRings();

            foreach (var ringPair in rings)
            {
                var r1 = allRings.FirstOrDefault(r => r.Time.Hour == ringPair.Key.Hours &&
                                                      r.Time.Minute == ringPair.Key.Minutes);
                var r2 = allRings.FirstOrDefault(r => r.Time.Hour == ringPair.Value.Hours &&
                                                      r.Time.Minute == ringPair.Value.Minutes);
                if (r1 != null && r2 != null)
                {
                    ringIdDict.Add(r1.RingId, r2);
                }
            }
            return ringIdDict;
        }

        private async void ringsOldToNew_Click(object sender, EventArgs e)
        {
            var rings = new Dictionary<TimeSpan, TimeSpan>
            {
                { new TimeSpan(08,00,00), new TimeSpan(08,00,00) },
                { new TimeSpan(08,50,00), new TimeSpan(08,45,00) },
                { new TimeSpan(09,50,00), new TimeSpan(09,40,00) },
                { new TimeSpan(10,50,00), new TimeSpan(10,35,00) },
                { new TimeSpan(11,40,00), new TimeSpan(11,25,00) },
                { new TimeSpan(12,30,00), new TimeSpan(12,15,00) },
                { new TimeSpan(13,15,00), new TimeSpan(13,15,00) },
                { new TimeSpan(14,00,00), new TimeSpan(14,00,00) },
                { new TimeSpan(14,50,00), new TimeSpan(14,50,00) },
                { new TimeSpan(15,50,00), new TimeSpan(15,45,00) },
                { new TimeSpan(16,50,00), new TimeSpan(16,35,00) },
                { new TimeSpan(17,40,00), new TimeSpan(17,25,00) },
                { new TimeSpan(18,30,00), new TimeSpan(18,15,00) },
                { new TimeSpan(19,20,00), new TimeSpan(19,05,00) }
            };

            await MoveScheduleByRings(rings);
        }

        private async void ringsNewToOld_Click(object sender, EventArgs e)
        {
            var rings = new Dictionary<TimeSpan, TimeSpan>
            {
                { new TimeSpan(08,00,00), new TimeSpan(08,00,00) },
                { new TimeSpan(08,45,00), new TimeSpan(08,50,00) },
                { new TimeSpan(09,40,00), new TimeSpan(09,50,00) },
                { new TimeSpan(10,35,00), new TimeSpan(10,50,00) },
                { new TimeSpan(11,25,00), new TimeSpan(11,40,00) },
                { new TimeSpan(12,15,00), new TimeSpan(12,30,00) },
                { new TimeSpan(13,15,00), new TimeSpan(13,15,00) },
                { new TimeSpan(14,00,00), new TimeSpan(14,00,00) },
                { new TimeSpan(14,50,00), new TimeSpan(14,50,00) },
                { new TimeSpan(15,45,00), new TimeSpan(15,50,00) },
                { new TimeSpan(16,35,00), new TimeSpan(16,50,00) },
                { new TimeSpan(17,25,00), new TimeSpan(17,40,00) },
                { new TimeSpan(18,15,00), new TimeSpan(18,30,00) },
                { new TimeSpan(19,05,00), new TimeSpan(19,20,00) }
            };

            await MoveScheduleByRings(rings);
        }

        private async void ShortenLessons_Click(object sender, EventArgs e)
        {
            var rings = new Dictionary<TimeSpan, TimeSpan>
            {
                { new TimeSpan(08,00,00), new TimeSpan(08,00,00) },
                { new TimeSpan(08,45,00), new TimeSpan(08,40,00) },
                { new TimeSpan(09,40,00), new TimeSpan(09,25,00) },
                { new TimeSpan(10,35,00), new TimeSpan(10,10,00) },
                { new TimeSpan(11,25,00), new TimeSpan(10,50,00) },
                { new TimeSpan(12,15,00), new TimeSpan(11,30,00) },
                { new TimeSpan(13,15,00), new TimeSpan(12,10,00) },
                { new TimeSpan(14,00,00), new TimeSpan(12,55,00) },
                { new TimeSpan(14,50,00), new TimeSpan(13,40,00) },
                { new TimeSpan(15,45,00), new TimeSpan(14,25,00) },
                { new TimeSpan(16,35,00), new TimeSpan(15,05,00) },
                { new TimeSpan(17,25,00), new TimeSpan(15,45,00) },
                { new TimeSpan(18,15,00), new TimeSpan(16,25,00) },
                { new TimeSpan(19,05,00), new TimeSpan(17,05,00) }
            };

            await MoveScheduleByRings(rings);
        }

        private async void LongerLessons_Click(object sender, EventArgs e)
        {
            var rings = new Dictionary<TimeSpan, TimeSpan>
            {
                { new TimeSpan(08,00,00), new TimeSpan(08,00,00) },
                { new TimeSpan(08,40,00), new TimeSpan(08,45,00) },
                { new TimeSpan(09,25,00), new TimeSpan(09,40,00) },
                { new TimeSpan(10,10,00), new TimeSpan(10,35,00) },
                { new TimeSpan(10,50,00), new TimeSpan(11,25,00) },
                { new TimeSpan(11,30,00), new TimeSpan(12,15,00) },
                { new TimeSpan(12,10,00), new TimeSpan(13,15,00) },
                { new TimeSpan(12,55,00), new TimeSpan(14,00,00) },
                { new TimeSpan(13,40,00), new TimeSpan(14,50,00) },
                { new TimeSpan(14,25,00), new TimeSpan(15,45,00) },
                { new TimeSpan(15,05,00), new TimeSpan(16,35,00) },
                { new TimeSpan(15,45,00), new TimeSpan(17,25,00) },
                { new TimeSpan(16,25,00), new TimeSpan(18,15,00) },
                { new TimeSpan(17,05,00), new TimeSpan(19,05,00) }
            };

            await MoveScheduleByRings(rings);
        }
    }
}
