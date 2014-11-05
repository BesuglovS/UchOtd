using Schedule.Repositories;
using Schedule.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule.Forms
{
    public partial class LessonListByTFD : Form
    {
        private readonly ScheduleRepository _repo;

        public LessonListByTFD(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void LessonListByTFD_Load(object sender, EventArgs e)
        {
            // TFD load
            var tfdList = _repo.GetAllTeacherForDiscipline();
            var tfdViewList = tfdView.tfdsToView(tfdList);
            tfdViewList = tfdViewList.OrderBy(tfdv => tfdv.tfdSummary).ToList();

            tfdBox.DisplayMember = "tfdSummary";
            tfdBox.ValueMember = "TeacherForDisciplineId";
            tfdBox.DataSource = tfdViewList;
        }

        private void tfdBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lessons = _repo
                .GetFiltredRealLessons(l =>
                    l.IsActive &&
                    l.TeacherForDiscipline.TeacherForDisciplineId == (int)tfdBox.SelectedValue)
                .OrderBy(l => l.Calendar.Date)
                .ThenBy(l => l.Ring.Time.TimeOfDay)
                .ToList();

            var lessonsView = LessonViewAtLessonListByTFD.FromLessonList(lessons);

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
