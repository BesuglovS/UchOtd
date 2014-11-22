using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Schedule.Repositories;
using UchOtd.Schedule.Views;
using Task = System.Threading.Tasks.Task;

namespace UchOtd.Schedule.Forms
{
    public partial class LessonListByTeacher : Form
    {
        private readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public LessonListByTeacher(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void LessonListByTeacher_Load(object sender, EventArgs e)
        {
            var teacherList = _repo
                .Teachers
                .GetAllTeachers()
                .OrderBy(t => t.FIO)
                .ToList();

            teacherBox.DisplayMember = "FIO";
            teacherBox.ValueMember = "TeacherId";
            teacherBox.DataSource = teacherList;
        }

        private async void teacherBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }

            _tokenSource = new CancellationTokenSource();
            _cToken = _tokenSource.Token;

            List<LessonViewAtLessonListByTeacher> lessonsView = null;

            try
            {
                var teacherId = (int) teacherBox.SelectedValue;
                var isShowProposed = showProposed.Checked;

                lessonsView = await Task.Run(() =>
                {
                    var lessons = _repo
                        .Lessons
                        .GetFiltredLessons(l =>
                            ((l.State == 1) || ((l.State == 2) && isShowProposed)) &&
                            l.TeacherForDiscipline.Teacher.TeacherId == teacherId)
                        .OrderBy(l => l.Calendar.Date)
                        .ThenBy(l => l.Ring.Time.TimeOfDay)
                        .ToList();

                    _cToken.ThrowIfCancellationRequested();

                    return LessonViewAtLessonListByTeacher.FromLessonList(lessons);
                }, _cToken);
            }
            catch (OperationCanceledException)
            {
                
            }


            if (lessonsView != null)
            {
                view.DataSource = lessonsView;

                FormatView();
            }
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
            if (WindowState == FormWindowState.Maximized)
            {
                FormatView();
            }
        }
    }
}
