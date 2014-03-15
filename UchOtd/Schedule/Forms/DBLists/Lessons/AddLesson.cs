using System.Linq;
using Schedule.Repositories;
using System.Windows.Forms;
using Schedule.Views;
using Schedule.Views.DBListViews;
using System;
using System.Collections.Generic;
using System.Drawing;
using Schedule.DomainClasses.Main;
using Schedule.Core;

namespace Schedule.Forms.DBLists.Lessons
{
    public partial class AddLesson : Form
    {
        private readonly ScheduleRepository _repo;
        private readonly int tfdId = -1;

        public AddLesson(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        public AddLesson(ScheduleRepository repo, int tfdId)
        {
            InitializeComponent();

            _repo = repo;

            this.tfdId = tfdId;
        }

        private void AddLesson_Load(object sender, System.EventArgs e)
        {
            // TFD load
            var tfdList = _repo.GetAllTeacherForDiscipline();
            var tfdViewList = tfdView.tfdsToView(tfdList);
            tfdViewList = tfdViewList.OrderBy(tfdv => tfdv.tfdSummary).ToList();

            teacherForDisciplineBox.DataSource = tfdViewList;
            teacherForDisciplineBox.DisplayMember = "tfdSummary";
            teacherForDisciplineBox.ValueMember = "TeacherForDisciplineId";

            if (tfdId != -1)
            {
                teacherForDisciplineBox.SelectedValue = tfdId;
                ringsBox.Focus();
            }

            // Rings load
            var ringsList = _repo.GetAllRings()                
                .OrderBy(r => r.Time.TimeOfDay)
                .ToList();
            var ringsView = RingView.RingsToView(ringsList);

            ringsBox.DataSource = ringsView;
            ringsBox.DisplayMember = "Time";
            ringsBox.ValueMember = "RingId";

            // DOW Local
            var dowList = new List<object>();
            foreach (var dow in Constants.Constants.DOWLocal)
            {
                dowList.Add(new { Value = dow.Key, Text = dow.Value });
            }

            dayOfWeekBox.DataSource = dowList;
            dayOfWeekBox.ValueMember = "Value";
            dayOfWeekBox.DisplayMember = "Text";

            // Public comment
            publicComment.Items.AddRange(Constants.Constants.LessonAddPublicComment.ToArray());
            publicComment.SelectedIndex = 0;

            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Execute_Click(object sender, EventArgs e)
        {            
            if (teacherForDisciplineBox.SelectedValue == null)
            {
                MessageBox.Show("Неправильно выбран TFD.");
                return;
            }

            var weekList = ScheduleRepository.WeeksStringToList(lessonWeeks.Text);

            var tfd = _repo.GetTeacherForDiscipline((int)teacherForDisciplineBox.SelectedValue);
            var disc = tfd.Discipline;
            var studentGroup = disc.StudentGroup;
            var sigFromGroup = _repo.GetFiltredStudentsInGroups(sing => sing.StudentGroup.StudentGroupId == studentGroup.StudentGroupId);
            var studentIdsInGroup = sigFromGroup.Select(studentsInGroupse => studentsInGroupse.Student.StudentId).ToList();
            var studentGroupsIds = _repo.GetFiltredStudentsInGroups(sig => studentIdsInGroup.Contains(sig.Student.StudentId)).Select(sing => sing.StudentGroup.StudentGroupId).Distinct();
                                    
            var ring = _repo.GetRing((int)ringsBox.SelectedValue);

            var calendarIdsList = new List<int>();
            for (int i = 0; i < weekList.Count; i++)
            {
                var date = _repo.GetDateFromDowAndWeek((int)dayOfWeekBox.SelectedValue, weekList[i]);
                var calendar = _repo.FindCalendar(date);
                if (calendar != null)
                {
                    calendarIdsList.Add(calendar.CalendarId);
                }
            }
            var groupsLessons = _repo
                .GetFiltredLessons(
                    l => studentGroupsIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId) && 
                         calendarIdsList.Contains(l.Calendar.CalendarId) && 
                         l.Ring.RingId == ring.RingId &&
                         l.IsActive);


            if (groupsLessons.Count != 0)
            {                
                DialogResult OutOfMind = MessageBox.Show("У студентов группы есть занятия. Всё равно добавить?", "ЕГГОГ", MessageBoxButtons.YesNo);
                if (OutOfMind == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            
                        
            Dictionary<int, string> audWeekList = new Dictionary<int,string>();
            audWeekList = Utilities.GetAudWeeksList(auditoriums.Text);

            for (int i = 0; i < weekList.Count; i++)
            {
                var lesson = new Lesson();

                lesson.IsActive = isActive.Checked;
                lesson.TeacherForDiscipline = tfd;
                lesson.Ring = _repo.GetRing((int)ringsBox.SelectedValue);

                var date = _repo.GetDateFromDowAndWeek((int)dayOfWeekBox.SelectedValue, weekList[i]);
                var calendar = _repo.FindCalendar(date);
                if (calendar == null)
                {
                    calendar = new Calendar(date);
                }
                lesson.Calendar = calendar;

                // Auditorium
                Auditorium aud;
                if (audList.SelectedIndex != -1)
                {
                    aud = _repo.GetAuditorium((int)audList.SelectedValue);
                }
                else
                {
                    if (audWeekList.Keys.Count == 1)
                    {
                        var lessonAud = audWeekList[0];
                        audWeekList.Clear();
                        foreach (var week in weekList)
                        {
                            audWeekList.Add(week, lessonAud);
                        }
                    }

                    aud = _repo.FindAuditorium(audWeekList[weekList[i]]);
                    if (aud == null)
                    {
                        _repo.AddAuditorium(new Auditorium(audWeekList[weekList[i]]));
                        aud = _repo.FindAuditorium(audWeekList[weekList[i]]);
                    }
                }
                lesson.Auditorium = aud;

                _repo.AddLesson(lesson, publicComment.Text, hiddenComment.Text);
            }

            this.Close();
        }

        private void showAuds_Click(object sender, EventArgs e)
        {
            var audsForm = new Auditoriums(_repo);
            audsForm.Show();
        }

        private void ringsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFreeAuds();
        }

        private void dayOfWeekBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFreeAuds();
        }


        private void lessonWeeks_TextChanged(object sender, EventArgs e)
        {
            UpdateFreeAuds();
        }

        private void UpdateFreeAuds()
        {
            List<int> weekList;

            try
            {
                weekList = Utilities.ConvertWeeksToList(lessonWeeks.Text);
            }
            catch
            {
                return;
            }

            if ((dayOfWeekBox.SelectedIndex == -1) || (ringsBox.SelectedIndex == -1) || (lessonWeeks.Text == ""))
            {
                return;
            }           
            
            var calendarIds = new List<int>();
            foreach (var cal in _repo.GetAllCalendars())
            {
                if (Constants.Constants.DOWRemap[(int)cal.Date.DayOfWeek]-1 == dayOfWeekBox.SelectedIndex)
                {
                    var week = _repo.CalculateWeekNumber(cal.Date);
                    if (weekList.Contains(week))
                    {
                        calendarIds.Add(cal.CalendarId);
                    }
                }
            }

            var res = _repo.GetFreeAuditoriumAtDOWTime(calendarIds, _repo.GetRing((int)ringsBox.SelectedValue));

            var c = new Utilities.AudComparer();
            res = res
                .OrderBy(aud => aud, c)
                .ToList();

            audList.DataSource = res;
            audList.ValueMember = "AuditoriumId";
            audList.DisplayMember = "Name";

            audList.SelectedIndex = -1;
        }

        private void reset_Click(object sender, EventArgs e)
        {
            audList.SelectedIndex = -1;
        }
    }
}
