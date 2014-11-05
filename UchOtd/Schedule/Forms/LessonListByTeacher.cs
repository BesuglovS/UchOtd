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
    public partial class LessonListByTeacher : Form
    {
        private readonly ScheduleRepository _repo;

        public LessonListByTeacher(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void LessonListByTeacher_Load(object sender, EventArgs e)
        {
            var teacherList = _repo
                .GetAllTeachers()
                .OrderBy(t => t.FIO)
                .ToList();

            teacherBox.DisplayMember = "FIO";
            teacherBox.ValueMember = "TeacherId";
            teacherBox.DataSource = teacherList;
        }

        private void teacherBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lessons = _repo
                .GetFiltredRealLessons(l =>
                    l.IsActive &&
                    l.TeacherForDiscipline.Teacher.TeacherId == (int)teacherBox.SelectedValue)
                .OrderBy(l => l.Calendar.Date)
                .ThenBy(l => l.Ring.Time.TimeOfDay)
                .ToList();

            var lessonsView = LessonViewAtLessonListByTeacher.FromLessonList(lessons);

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

            view.Columns["DisciplineName"].HeaderText = "Наименование дисциплины";
            view.Columns["DisciplineName"].Width = (int)Math.Round(view.Width * 0.3);
            view.Columns["DisciplineName"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            view.Columns["GroupName"].HeaderText = "Группа";
            view.Columns["GroupName"].Width = (int)Math.Round(view.Width * 0.16);

            view.Columns["CalendarDate"].HeaderText = "Дата";
            view.Columns["CalendarDate"].Width = (int)Math.Round(view.Width * 0.16);

            view.Columns["RingTime"].HeaderText = "Время";
            view.Columns["RingTime"].Width = (int)Math.Round(view.Width * 0.16);

            view.Columns["AuditoriumName"].HeaderText = "Аудитория";
            view.Columns["AuditoriumName"].Width = (int)Math.Round(view.Width * 0.16);

            view.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            view.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private void LessonListByTeacher_ResizeEnd(object sender, EventArgs e)
        {
            FormatView();
        }

        private void LessonListByTeacher_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                FormatView();
            }
        }
    }
}
