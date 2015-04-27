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
using UchOtd.Schedule.Views.DBListViews;
using UchOtd.Schedule.wnu;
using System.Threading;
using UchOtd.Properties;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class ScheduleNoteList : Form
    {
        private readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public ScheduleNoteList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void ScheduleNoteList_Load(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();

            var teachers = _repo.Teachers.GetAllTeachers();
            TeacherFilter.DisplayMember = "FIO";
            TeacherFilter.ValueMember = "TeacherId";
            TeacherFilter.DataSource = teachers;

            SetLessonsList();

            var notes = _repo.ScheduleNotes.GetAllScheduleNotes();
            RefreshView(notes);
        }

        private void RefreshView(List<ScheduleNote> notes)
        {
            if (isNotesDateFiltered.Checked)
            {
                notes = notes
                    .Where(n => n.Lesson != null && 
                        n.Lesson.Calendar.Date.Year == notesDateFilter.Value.Year &&
                        n.Lesson.Calendar.Date.Month == notesDateFilter.Value.Month && 
                        n.Lesson.Calendar.Date.Day == notesDateFilter.Value.Day)
                    .OrderBy(n => (n.Lesson != null) ? n.Lesson.Calendar.Date.Date : new DateTime(2100, 1, 1))
                    .ThenBy(n => (n.Lesson != null) ? n.Lesson.Ring.Time.TimeOfDay : new TimeSpan(0))
                    .ThenBy(n => (n.Lesson != null) ? (n.Lesson.TeacherForDiscipline.Teacher.FIO) : String.Empty)
                    .ToList();
            }
            else
            {
                notes = notes
                    .OrderBy(n => (n.Lesson != null) ? n.Lesson.Calendar.Date.Date : new DateTime(2100, 1, 1))
                    .ThenBy(n => (n.Lesson != null) ? n.Lesson.Ring.Time.TimeOfDay : new TimeSpan(0))
                    .ThenBy(n => (n.Lesson != null) ? (n.Lesson.TeacherForDiscipline.Teacher.FIO) : String.Empty)
                    .ToList();
            }

            var notesView = ScheduleNoteView.NotesToView(notes);

            NotesView.DataSource = notesView;

            NotesView.Columns["ScheduleNoteId"].Visible = false;
            NotesView.Columns["Text"].Width = 240;
            NotesView.Columns["LessonString"].Width = 600;
            NotesView.Columns["LateAmount"].Width = 80;

            totalLate.Text = notes.Count + " / Σ = " + 
                notes.Where(n => n.Lesson != null && n.Lesson.State == 1)
                     .Aggregate(0, (sum, note) => sum + note.LateAmount);
        }

        private void SetLessonsList()
        {
            List<Lesson> lessons = null;

            if (!(isDateFilter.Checked) && !(IsTeacherFilter.Checked))
            {
                lessons = _repo.Lessons.GetAllLessons()
                    .OrderBy(l => l.Calendar.Date)
                    .ThenBy(l => l.Ring.Time.TimeOfDay)
                    .ThenBy(l => l.TeacherForDiscipline.Teacher.FIO)
                    .ThenBy(l => l.State)
                    .ToList();
            }

            if ((isDateFilter.Checked) && (IsTeacherFilter.Checked))
            {
                lessons = _repo.Lessons.GetFiltredLessons(l =>
                        l.Calendar.Date.Year == DateFilter.Value.Year &&
                        l.Calendar.Date.Month == DateFilter.Value.Month &&
                        l.Calendar.Date.Day == DateFilter.Value.Day &&
                        l.TeacherForDiscipline.Teacher.TeacherId == (int) TeacherFilter.SelectedValue)
                        .OrderBy(l => l.Calendar.Date)
                        .ThenBy(l => l.Ring.Time.TimeOfDay)
                        .ThenBy(l => l.TeacherForDiscipline.Teacher.FIO)
                        .ThenBy(l => l.State)
                        .ToList();
            }
            
            if ((isDateFilter.Checked) && !(IsTeacherFilter.Checked))
            {
                lessons = _repo.Lessons.GetAllLessons()
                    .Where(l => 
                        l.Calendar.Date.Year == DateFilter.Value.Year &&
                        l.Calendar.Date.Month == DateFilter.Value.Month &&
                        l.Calendar.Date.Day == DateFilter.Value.Day)
                    .OrderBy(l => l.Calendar.Date)
                    .ThenBy(l => l.Ring.Time.TimeOfDay)
                    .ThenBy(l => l.TeacherForDiscipline.Teacher.FIO)
                    .ThenBy(l => l.State)
                    .ToList();
            }

            if (!(isDateFilter.Checked) && (IsTeacherFilter.Checked))
            {
                lessons = _repo.Lessons.GetAllLessons()
                    .Where(l => l.TeacherForDiscipline.Teacher.TeacherId == (int)TeacherFilter.SelectedValue)
                    .OrderBy(l => l.Calendar.Date)
                    .ThenBy(l => l.Ring.Time.TimeOfDay)
                    .ThenBy(l => l.TeacherForDiscipline.Teacher.FIO)
                    .ThenBy(l => l.State)
                    .ToList();
            }

            LessonsList.DataSource = lessons;
        }

        private void LessonsList_Format(object sender, ListControlConvertEventArgs e)
        {
            var lesson = ((Lesson)e.ListItem);

            e.Value = lesson.State + " " + 
                      lesson.Calendar.Date.ToString("dd.MM.yyyy") + " " +
                      lesson.Ring.Time.ToString("H:mm") + " " +
                      lesson.TeacherForDiscipline.Teacher.FIO + " " +
                      lesson.TeacherForDiscipline.Discipline.Name + " " +
                      lesson.TeacherForDiscipline.Discipline.StudentGroup.Name;
        }

        private void add_Click(object sender, EventArgs e)
        {
            var lesson = (Lesson) LessonsList.SelectedValue;

            var newNote = new ScheduleNote { IsLesson = IsLesson.Checked, Lesson = lesson, Text = NoteText.Text, LateAmount = (int)LateCount.Value };

            _repo.ScheduleNotes.AddScheduleNote(newNote);

            var notes = _repo.ScheduleNotes.GetAllScheduleNotes();
            RefreshView(notes);
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (NotesView.SelectedCells.Count > 0)
            {
                var noteView = ((List<ScheduleNoteView>)NotesView.DataSource)[NotesView.SelectedCells[0].RowIndex];

                var note = _repo.ScheduleNotes.GetScheduleNote(noteView.ScheduleNoteId);

                note.Lesson = (Lesson)LessonsList.SelectedValue;
                note.IsLesson = IsLesson.Checked;

                note.Text = NoteText.Text;
                note.LateAmount = (int) LateCount.Value;

                _repo.ScheduleNotes.UpdateScheduleNote(note);

                var notes = _repo.ScheduleNotes.GetAllScheduleNotes();
                RefreshView(notes);
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (NotesView.SelectedCells.Count > 0)
            {
                var noteView = ((List<ScheduleNoteView>)NotesView.DataSource)[NotesView.SelectedCells[0].RowIndex];
                
                _repo.ScheduleNotes.RemoveScheduleNote(noteView.ScheduleNoteId);

                var notes = _repo.ScheduleNotes.GetAllScheduleNotes();
                RefreshView(notes);
            }
        }

        private void NotesView_Click(object sender, EventArgs e)
        {
            var noteView = ((List<ScheduleNoteView>)NotesView.DataSource)[NotesView.SelectedCells[0].RowIndex];

            var note = _repo.ScheduleNotes.GetScheduleNote(noteView.ScheduleNoteId);

            IsLesson.Checked = note.IsLesson;
            if (note.Lesson != null)
            {
                var lesson = ((List<Lesson>) LessonsList.DataSource).FirstOrDefault(l => l.LessonId == note.Lesson.LessonId);
                if (lesson != null)
                {
                    LessonsList.SelectedItem = lesson;
                }
            }

            NoteText.Text = note.Text;
            LateCount.Value = note.LateAmount;
        }

        private void ShowAll_Click(object sender, EventArgs e)
        {
            var notes = _repo.ScheduleNotes.GetAllScheduleNotes();
            RefreshView(notes);
        }

        private void FilterNotes_Click(object sender, EventArgs e)
        {
            var filter = FilterText.Text;
            var notes = _repo.ScheduleNotes.GetAllScheduleNotes()
                .Where(n => n.Text.Contains(filter) ||
                n.Lesson.TeacherForDiscipline.Teacher.FIO.Contains(filter) ||
                n.Lesson.TeacherForDiscipline.Discipline.Name.Contains(filter) ||
                n.Lesson.TeacherForDiscipline.Discipline.StudentGroup.Name.Contains(filter))
                .ToList();
            RefreshView(notes);
        }

        private async void upload_Click(object sender, EventArgs e)
        {
            if (upload.Text == "Загрузить на сайт")
            {
                _cToken = _tokenSource.Token;

                upload.Text = "";
                upload.Image = Resources.Loading;

                var repo = _repo;
                
                try
                {
                    await Task.Run(() => WnuUpload.UploadNotes(repo, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            upload.Image = null;
            upload.Text = "Загрузить на сайт";
        }

        private void filterLessons_Click(object sender, EventArgs e)
        {
            SetLessonsList();
        }

        private void LateText_Click(object sender, EventArgs e)
        {
            NoteText.Text = Resources.ScheduleNoteList_LateText;
        }

        private void LongGoneText_Click(object sender, EventArgs e)
        {
            NoteText.Text = Resources.ScheduleNoteList_LongGoneText;
        }

        private void DateFilter_ValueChanged(object sender, EventArgs e)
        {
            if (dateSync.Checked)
            {
                notesDateFilter.Value = DateFilter.Value;
            }
        }
    }
}
