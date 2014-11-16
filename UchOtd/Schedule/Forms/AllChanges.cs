using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms
{
    public partial class AllChanges : Form
    {
        readonly ScheduleRepository _repo;

        public AllChanges(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void AllChanges_Load(object sender, EventArgs e)
        {
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
            
            //RefreshView();
        }

        private void RefreshView()
        {
            var changes = _repo
                .LessonLogEvents
                .GetAllLessonLogEvents()
                .OrderByDescending(lle => lle.DateTime)
                .ToList();

            if (tfdFiltering.Checked)
            {
                changes = changes.Where(evt => 
                    ((evt.OldLesson != null) && 
                     (evt.OldLesson.TeacherForDiscipline.TeacherForDisciplineId == (int)tfdFilter.SelectedValue)) ||
                    ((evt.NewLesson != null) &&
                     (evt.NewLesson.TeacherForDiscipline.TeacherForDisciplineId == (int)tfdFilter.SelectedValue))).ToList();
            }

            if (teacherFiltering.Checked)
            {
                changes = changes.Where(evt =>
                    ((evt.OldLesson != null) &&
                     (evt.OldLesson.TeacherForDiscipline.Teacher.TeacherId == (int)teacherFilter.SelectedValue)) ||
                    ((evt.NewLesson != null) &&
                     (evt.NewLesson.TeacherForDiscipline.Teacher.TeacherId == (int)teacherFilter.SelectedValue))).ToList();
            }

            if (lessonDateFiltering.Checked)
            {
                changes = changes.Where(evt =>
                    ((evt.OldLesson != null) &&
                     (evt.OldLesson.Calendar.Date.Date == lessonDateFilter.Value.Date)) ||
                    ((evt.NewLesson != null) &&
                     (evt.NewLesson.Calendar.Date.Date == lessonDateFilter.Value.Date))).ToList();
            }

            if (eventDateFiltering.Checked)
            {
                changes = changes.Where(evt => evt.DateTime.Date == eventDateFilter.Value.Date).ToList();
            }

            var changesView = LessonLogEventView.FromEventList(changes);

            view.DataSource = changesView;

            FormatChangesView();
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

        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshView();
        }
    }
}
