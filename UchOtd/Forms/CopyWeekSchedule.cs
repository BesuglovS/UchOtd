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

        private void Go_Click(object sender, EventArgs e)
        {
            if (copyGroup.Checked)
            {
                copyGroupSchedule();
            }

            if (copyFaculty.Checked)
            {
                copyFacultySchedule();
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

        private async void copyGroupSchedule()
        {
            var weekFrom = -1;
            var weekTo = -1;
            try
            {
                weekFrom = int.Parse(fromWeek.Text);
                weekTo = int.Parse(toWeek.Text);
            }
            catch
            {
                return;
            }

            var group = (StudentGroup) groupList.SelectedItem;

            await Task.Run(() =>
            {
                copyGroupWeekSchedule(@group, weekFrom, weekTo);
            });
        }

        private void copyGroupWeekSchedule(StudentGroup @group, int weekFrom, int weekTo)
        {
            if (weekFrom == weekTo)
            {
                return;
            }

            var groupIds = StudentGroupIdsFromGroupId(@group.StudentGroupId);

            var cf = new CommonFunctions(_repo);
            cf.ConnectionString = _repo.GetConnectionString();

            var calendarPairs = new Dictionary<Calendar, Calendar>();

            for (int i = 1; i <= 7; i++)
            {
                Calendar c1, c2;
                c1 = cf.GetCalendarFromDowAndWeek(i, weekFrom);
                c2 = cf.GetCalendarFromDowAndWeek(i, weekTo);
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

                    _repo.Lessons.AddLessonWoLog(newLesson);
                }

                Invoke((MethodInvoker)delegate
                {
                    status.Text = @group.Name + ": " + calendarPair.Key.Date.ToString("dd.MM.yyyy") + " - " + calendarPair.Value.Date.ToString("dd.MM.yyyy");
                    // runs on UI thread
                });
            }
        }

        private async void copyFacultySchedule()
        {
            var weekFrom = -1;
            var weekTo = -1;
            try
            {
                weekFrom = int.Parse(fromWeek.Text);
                weekTo = int.Parse(toWeek.Text);
            }
            catch
            {
                return;
            }

            var faculty = (Faculty) facultyList.SelectedItem;

            var facultyGroups =
                _repo.GroupsInFaculties.GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                .Select(gif => gif.StudentGroup);

            foreach (var studentGroup in facultyGroups)
            {
                await Task.Run(() =>
                {
                    copyGroupWeekSchedule(studentGroup, weekFrom, weekTo);
                });
            }
        }

        private async void deleteWeekSchedule_Click(object sender, EventArgs e)
        {
            var weekFrom = -1;
            
            try
            {
                weekFrom = int.Parse(fromWeek.Text);
            }
            catch
            {
                return;
            }

            var group = (StudentGroup)groupList.SelectedItem;

            if (copyGroup.Checked)
            {
                await Task.Run(() =>
                {
                    deleteGroupSchedule(group, weekFrom);
                });
            }

            if (copyFaculty.Checked)
            {
                deleteFacultySchedule();
            }
        }

        private async void deleteGroupSchedule(StudentGroup group, int weekFrom)
        {
            Invoke((MethodInvoker)delegate
            {
                status.Text = @group.Name + " удаляем";
                // runs on UI thread
            });

            var groupIds = StudentGroupIdsFromGroupId(@group.StudentGroupId);

            var cf = new CommonFunctions(_repo) {ConnectionString = _repo.GetConnectionString()};

            var calendarList = new List<Calendar>();

            for (int i = 1; i <= 7; i++)
            {
                Calendar c1, c2;
                c1 = cf.GetCalendarFromDowAndWeek(i, weekFrom);
                
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
                _repo.Lessons.RemoveLessonWoLog(lesson.LessonId);
            }
            
            Invoke((MethodInvoker)delegate
            {
                status.Text = @group.Name + " готово";
                // runs on UI thread
            });
        }

        private async void deleteFacultySchedule()
        {
            var weekFrom = -1;
            
            try
            {
                weekFrom = int.Parse(fromWeek.Text);
            }
            catch
            {
                return;
            }

            var faculty = (Faculty)facultyList.SelectedItem;

            var facultyGroups =
                _repo.GroupsInFaculties.GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                    .Select(gif => gif.StudentGroup);

            foreach (var studentGroup in facultyGroups)
            {
                await Task.Run(() =>
                {
                    deleteGroupSchedule(studentGroup, weekFrom);
                });
            }

            status.Text = "Готово";
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
    }
}
