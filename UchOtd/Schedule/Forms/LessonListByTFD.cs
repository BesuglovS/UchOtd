using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms
{
    public partial class LessonListByTfd : Form
    {
        private readonly ScheduleRepository _repo;

        public LessonListByTfd(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void LessonListByTFD_Load(object sender, EventArgs e)
        {
            // TFD load
            var tfdList = _repo.TeacherForDisciplines.GetAllTeacherForDiscipline();
            var tfdViewList = TfdView.TfdsToView(tfdList);
            tfdViewList = tfdViewList.OrderBy(tfdv => tfdv.TfdSummary).ToList();

            tfdBox.DisplayMember = "TfdSummary";
            tfdBox.ValueMember = "TeacherForDisciplineId";
            tfdBox.DataSource = tfdViewList;
        }

        private void tfdBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lessons = _repo
                .Lessons
                .GetFiltredLessons(l =>
                    ((l.State == 1) || ((l.State == 2) && showProposed.Checked)) &&
                    l.TeacherForDiscipline.TeacherForDisciplineId == (int)tfdBox.SelectedValue)
                .OrderBy(l => l.Calendar.Date)
                .ThenBy(l => l.Ring.Time.TimeOfDay)
                .ToList();

            var lessonsView = LessonViewAtLessonListByTfd.FromLessonList(lessons);

            view.DataSource = lessonsView;

            FormatView();
        }

        private void FormatView()
        {
            foreach (DataGridViewColumn col in view.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            view.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            view.Columns["LessonId"].Visible = false;
            view.Columns["CalendarDate"].HeaderText = "Дата";            
            view.Columns["RingTime"].HeaderText = "Время";            
            view.Columns["AuditoriumName"].HeaderText = "Аудитория";

            DivideViewInThreeColumns();
        }

        private void DivideViewInThreeColumns()
        {
            view.Columns["CalendarDate"].Width = (int)Math.Round(view.Width * 0.32);
            view.Columns["RingTime"].Width = (int)Math.Round(view.Width * 0.32);
            view.Columns["AuditoriumName"].Width = (int)Math.Round(view.Width * 0.32);
        }

        private void LessonListByTFD_ResizeEnd(object sender, EventArgs e)
        {
            DivideViewInThreeColumns();
        }
    }
}
