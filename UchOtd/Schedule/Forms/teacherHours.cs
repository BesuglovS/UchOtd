using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Schedule.Repositories;
using UchOtd.Schedule.Forms.DBLists.Lessons;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms
{
    public partial class teacherHours : Form
    {
        private readonly ScheduleRepository _repo;
        
        public teacherHours(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void teacherHours_Load(object sender, EventArgs e)
        {
            var teachers = _repo
                .GetAllTeachers()
                .OrderBy(t => t.FIO)
                .ToList();

            teachersList.DisplayMember = "FIO";
            teachersList.ValueMember = "TeacherId";
            teachersList.DataSource = teachers;
        }

        private void teachersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            var teacherId = (int)teachersList.SelectedValue;

            var tfds = _repo.GetFiltredTeacherForDiscipline(tfd => tfd.Teacher.TeacherId == teacherId);

            var tfdInfo = TeacherForDisciplineView.FromTFDList(tfds, _repo);

            view.DataSource = tfdInfo;

            FormatView();
        }

        private void FormatView()
        {
            view.Columns["tfdId"].Visible = false;

            view.Columns["DisciplineId"].Visible = false;

            view.Columns["DisciplineName"].HeaderText = "Дисциплина";
            view.Columns["DisciplineName"].Width = 200;

            view.Columns["GroupName"].HeaderText = "Группа";
            view.Columns["GroupName"].Width = 80;

            view.Columns["PlanHours"].HeaderText = "Часы по плану";
            view.Columns["PlanHours"].Width = 80;

            view.Columns["ScheduleHours"].HeaderText = "Часы в расписании";
            view.Columns["ScheduleHours"].Width = 80;

            view.Columns["HoursDone"].HeaderText = "Выполнено на данный момент";
            view.Columns["HoursDone"].Width = 80;

            view.Columns["PlannedHours"].HeaderText = "Предполагаемые часы";
            view.Columns["PlannedHours"].Width = 80;

            view.Columns["Attestation"].HeaderText = "Форма отчётности";
            view.Columns["Attestation"].Width = 80;
        }
        
        private Color PickPercentColor(int PlanHours, int ScheduleHours)
        {
            if (ScheduleHours > PlanHours + 1)
            {
                return Color.FromArgb(255, 0, 255);
            }

            if (ScheduleHours == PlanHours + 1)
            {
                return Color.FromArgb(200, 255, 0);
            }

            if (ScheduleHours == PlanHours)
            {
                return Color.FromArgb(0, 255, 0);
            }

            if (ScheduleHours >= PlanHours * 0.9)
            {
                return Color.FromArgb(255, 255, 0);
            }

            if (ScheduleHours >= PlanHours * 0.5)
            {
                return Color.FromArgb(255, 128, 0);
            }

            return Color.FromArgb(255, 0, 0);
        }

        private void view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                var discView = ((List<TeacherForDisciplineView>)view.DataSource)[e.RowIndex];

                e.CellStyle.BackColor = PickPercentColor(discView.PlanHours, discView.ScheduleHours);
            }
        }

        private void view_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var discId = ((List<TeacherForDisciplineView>)view.DataSource)[e.RowIndex].DisciplineId;
            var tefd = _repo.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discId);
            if (tefd != null)
            {
                var addLessonForm = new AddLesson(_repo, tefd.TeacherForDisciplineId);
                addLessonForm.Show();
            }
            else
            {
                var addLessonForm = new AddLesson(_repo);
                addLessonForm.Show();
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            RefreshView();
        }
    }
}
