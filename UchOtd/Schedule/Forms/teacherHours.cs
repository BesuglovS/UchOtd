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
            var teacherId = (int)teachersList.SelectedValue;

            var tfds = _repo.GetFiltredTeacherForDiscipline(tfd => tfd.Teacher.TeacherId == teacherId);

            var tfdInfo = TeacherForDisciplineView.FromTFDList(tfds, _repo);

            view.DataSource = tfdInfo;

            FormatView();
        }

        private void FormatView()
        {
            view.Columns["tfdId"].Visible = false;

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

            view.Columns["Attestation"].HeaderText = "Форма отчётности";
            view.Columns["Attestation"].Width = 80;
        }
    }
}
