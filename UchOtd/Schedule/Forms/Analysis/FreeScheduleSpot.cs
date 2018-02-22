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
using UchOtd.Schedule.Core;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class FreeScheduleSpot : Form
    {
        private readonly ScheduleRepository _repo;

        public FreeScheduleSpot(ScheduleRepository repo)
        {
            _repo = repo;
            InitializeComponent();
        }

        private void FreeScheduleSpot_Load(object sender, EventArgs e)
        {
            var groups = _repo.StudentGroups.GetAllStudentGroups();
            groupList.DisplayMember = "Name";
            groupList.DataSource = groups;

            var rings = _repo.Rings.GetAllRings();
            var ringViews = RingView.RingsToView(rings);
            ringsList.DisplayMember = "Time";
            ringsList.DataSource = ringViews;

            spotDatePicker.Value = DateTime.Now;
        }

        private void Go_Click(object sender, EventArgs e)
        {
            var group = (StudentGroup) groupList.SelectedValue;

            var spotDate = spotDatePicker.Value;
            var calendar = _repo.Calendars.GetFirstFiltredCalendar(c =>
                c.Date.Day == spotDate.Day &&
                c.Date.Month == spotDate.Month &&
                c.Date.Year == spotDate.Year);
            if (calendar == null) return;

            var ringId = ((RingView) ringsList.SelectedValue).RingId;

            var changes = FreeThisScheduleSpot(@group, calendar, ringId);
        }

        private List<ScheduleChange> FreeThisScheduleSpot(StudentGroup @group, Calendar calendar, int ringId)
        {
            var result = new List<ScheduleChange>();

            var groupIds = _repo.StudentGroups.StudentGroupIdsFromGroupId(@group.StudentGroupId);

            var spotLessons = _repo.Lessons.GetFiltredLessons(l =>
                l.State == 1 &&
                groupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId) &&
                l.Calendar.CalendarId == calendar.CalendarId &&
                l.Ring.RingId == ringId);

            for (int i = 0; i < spotLessons.Count; i++)
            {
                var spotLesson = spotLessons[i];
                var week = _repo.Calendars.GetWeek(spotLesson);

                var teacher = spotLesson.TeacherForDiscipline.Teacher;

                var teacherWeekLessons = _repo.Lessons.GetTeacherWeekLessons(teacher.TeacherId, week);

                for (int j = 0; j < teacherWeekLessons.Count; j++)
                {
                    var weekLesson = teacherWeekLessons[j];
                    if (groupIds.Contains(weekLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId))
                    {
                        continue;
                    }

                    var changes = TrySwapTeacherLessonsInWeek(spotLesson, weekLesson);

                    if (changes != null)
                    {
                        output.AppendText("Solution was found!");
                    }
                    else
                    {
                        output.AppendText("Attempt failed");
                    }
                }
                

                //var changes = FreeThisScheduleSpot()
            }

            return result;
        }

        private List<ScheduleChange> TrySwapTeacherLessonsInWeek(Lesson lesson1, Lesson lesson2)
        {
            var result = new List<ScheduleChange>();

            var isEmpty1 = DetectIfPlaceIsEmptyForLesson(lesson1, lesson2.Calendar.CalendarId, lesson2.Ring.RingId);
            var isEmpty2 = DetectIfPlaceIsEmptyForLesson(lesson2, lesson1.Calendar.CalendarId, lesson1.Ring.RingId);

            // подумать про смены и про "не более 4-х пар в день"
            // о разной продолжительности уроков и пар и разных корпусах
            if (isEmpty1 && isEmpty2)
            {
                // move first in place of the second
                var change1 = new ScheduleChange()
                {
                    OldLesson = lesson1,
                    NewLesson = null,
                    Type = ScheduleChangeType.RemovedLesson
                };
                result.Add(change1);
                var change2 = new ScheduleChange()
                {
                    OldLesson = null,
                    NewLesson = new Lesson()
                    {
                        TeacherForDiscipline = lesson1.TeacherForDiscipline,
                        Calendar = lesson2.Calendar,
                        Ring = lesson2.Ring,
                        Auditorium = lesson2.Auditorium,
                        State = 1
                    },
                    Type = ScheduleChangeType.AddLesson
                };
                result.Add(change2);

                // move second in place of the first
                var change3 = new ScheduleChange()
                {
                    OldLesson = lesson2,
                    NewLesson = null,
                    Type = ScheduleChangeType.RemovedLesson
                };
                result.Add(change3);
                var change4 = new ScheduleChange()
                {
                    OldLesson = null,
                    NewLesson = new Lesson()
                    {
                        TeacherForDiscipline = lesson2.TeacherForDiscipline,
                        Calendar = lesson1.Calendar,
                        Ring = lesson1.Ring,
                        Auditorium = lesson1.Auditorium,
                        State = 1
                    },
                    Type = ScheduleChangeType.AddLesson
                };
                result.Add(change4);

                return result;
            }

            return null;
        }

        private bool DetectIfPlaceIsEmptyForLesson(Lesson lesson, int calendarId, int ringId)
        {
            var groupIds = _repo.StudentGroups.StudentGroupIdsFromGroupId(lesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId);

            var lessons = _repo.Lessons.GetFiltredLessons(l =>
                groupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId) &&
                l.State == 1 &&
                l.Calendar.CalendarId == calendarId &&
                l.Ring.RingId == ringId);

            return lessons.Count == 0;
        }
    }
}
