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

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class ScheduleNoteList : Form
    {
        private readonly ScheduleRepository _repo;

        public ScheduleNoteList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void ScheduleNoteList_Load(object sender, EventArgs e)
        {
            SetLessonsList();

            RefreshView();
        }

        private void RefreshView()
        {
            var notes = _repo.ScheduleNotes.GetAllScheduleNotes();

            var notesView = ScheduleNoteView.NotesToView(notes);

            NotesView.DataSource = notesView;

            NotesView.Columns["ScheduleNoteId"].Visible = false;
            NotesView.Columns["Text"].Width = 240;
            NotesView.Columns["LessonString"].Width = 600;
            NotesView.Columns["LateAmount"].Width = 80;
        }

        private void SetLessonsList()
        {
            var lessons = _repo.Lessons.GetAllLessons()
                .OrderBy(l => l.Calendar.Date)
                .ThenBy(l => l.Ring.Time.TimeOfDay)
                .ThenBy(l => l.TeacherForDiscipline.Teacher.FIO)
                .ThenBy(l => l.State)
                .ToList();

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

            var newNote = new ScheduleNote { Lesson = lesson, Text = NoteText.Text, LateAmount = (int)LateCount.Value };

            _repo.ScheduleNotes.Add(newNote);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (NotesView.SelectedCells.Count > 0)
            {
                var noteView = ((List<ScheduleNoteView>)NotesView.DataSource)[NotesView.SelectedCells[0].RowIndex];

                var note = _repo.ScheduleNotes.GetScheduleNote(noteView.ScheduleNoteId);

                note.Lesson = (Lesson)LessonsList.SelectedValue;

                note.Text = NoteText.Text;
                note.LateAmount = (int) LateCount.Value;

                _repo.ScheduleNotes.Update(note, note.ScheduleNoteId);
                
                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (NotesView.SelectedCells.Count > 0)
            {
                var noteView = ((List<ScheduleNoteView>)NotesView.DataSource)[NotesView.SelectedCells[0].RowIndex];
                
                _repo.ScheduleNotes.RemoveScheduleNote(noteView.ScheduleNoteId);

                RefreshView();
            }
        }

        private void NotesView_Click(object sender, EventArgs e)
        {
            var noteView = ((List<ScheduleNoteView>)NotesView.DataSource)[NotesView.SelectedCells[0].RowIndex];

            var note = _repo.ScheduleNotes.GetScheduleNote(noteView.ScheduleNoteId);

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
    }
}
