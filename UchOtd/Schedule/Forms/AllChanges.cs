using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Repositories;
using UchOtd.Properties;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms
{
    public partial class AllChanges : Form
    {
        readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public AllChanges(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void AllChanges_Load(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();

            var tfds = _repo.TeacherForDisciplines.GetAllTeacherForDiscipline()
                .OrderBy(tfd => tfd.Teacher.FIO)
                .ThenBy(tfd => tfd.Discipline.Name)
                .ToList();
            var tfdsView = TfdView.TfdsToView(tfds);

            tfdFilter.DisplayMember = "tfdSummary";
            tfdFilter.ValueMember = "TeacherForDisciplineId";
            tfdFilter.DataSource = tfdsView;

            var teachers = _repo
                .Teachers
                .GetAllTeachers()
                .OrderBy(t => t.FIO)
                .ToList();

            teacherFilter.DisplayMember = "FIO";
            teacherFilter.ValueMember = "TeacherId";
            teacherFilter.DataSource = teachers;
        }

        private void FormatChangesView()
        {
            view.Columns["LessonLogEventId"].HeaderText = "Id";
            view.Columns["LessonLogEventId"].Width = 50;

            view.Columns["OldLesson"].HeaderText = "Старый урок";
            view.Columns["OldLesson"].Width = 150;

            view.Columns["NewLesson"].HeaderText = "Новый урок";
            view.Columns["NewLesson"].Width = 150;

            view.Columns["DateTime"].HeaderText = "Дата + время";
            view.Columns["DateTime"].Width = 100;

            view.Columns["PublicComment"].HeaderText = "PublicComment";
            view.Columns["PublicComment"].Width = 100;

            view.Columns["HiddenComment"].HeaderText = "HiddenComment";
            view.Columns["HiddenComment"].Width = 100;

            view.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            view.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private async void Refresh_Click(object sender, EventArgs e)
        {
            if (UpdateView.Text == "Обновить")
            {
                _cToken = _tokenSource.Token;

                UpdateView.Text = "";
                UpdateView.Image = Resources.Loading;

                var tfd = tfdFiltering.Checked;
                var tfdId = (int) tfdFilter.SelectedValue;

                var teacher = teacherFiltering.Checked;
                var teacherId = (int) teacherFilter.SelectedValue;

                var lessonDate = lessonDateFiltering.Checked;
                var lessonDateValue = lessonDateFilter.Value.Date;

                var eventDate = eventDateFiltering.Checked;
                var eventDateValue = eventDateFilter.Value.Date;

                List<LessonLogEventView> changesView = null;

                try
                {
                    changesView = await Task.Run(() =>
                    {
                        var changes = _repo
                                .LessonLogEvents
                                .GetAllLessonLogEvents()
                                .OrderByDescending(lle => lle.DateTime)
                                .ToList();

                        if (tfd)
                        {
                            changes = changes.Where(evt =>
                                ((evt.OldLesson != null) &&
                                 (evt.OldLesson.TeacherForDiscipline.TeacherForDisciplineId == tfdId)) ||
                                ((evt.NewLesson != null) &&
                                 (evt.NewLesson.TeacherForDiscipline.TeacherForDisciplineId == tfdId))).ToList();
                        }

                        if (teacher)
                        {
                            changes = changes.Where(evt =>
                                ((evt.OldLesson != null) &&
                                 (evt.OldLesson.TeacherForDiscipline.Teacher.TeacherId == teacherId)) ||
                                ((evt.NewLesson != null) &&
                                 (evt.NewLesson.TeacherForDiscipline.Teacher.TeacherId == teacherId))).ToList();
                        }

                        if (lessonDate)
                        {
                            changes = changes.Where(evt =>
                                ((evt.OldLesson != null) &&
                                 (evt.OldLesson.Calendar.Date.Date == lessonDateValue)) ||
                                ((evt.NewLesson != null) &&
                                 (evt.NewLesson.Calendar.Date.Date == lessonDateValue))).ToList();
                        }

                        if (eventDate)
                        {
                            changes = changes.Where(evt => evt.DateTime.Date == eventDateValue).ToList();
                        }

                        return LessonLogEventView.FromEventList(changes);
                    }, _cToken);
                }
                catch (OperationCanceledException)
                {
                }

                view.DataSource = changesView;

                FormatChangesView();
            }
            else
            {
                _tokenSource.Cancel();
            }

            UpdateView.Image = null;
            UpdateView.Text = "Обновить";
        }
    }
}
