using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class ScheduleNotesRepository:BaseRepository<ScheduleNote>
    {

        #region ScheduleNoteRepository
        public List<ScheduleNote> GetAllScheduleNotes()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context
                    .ScheduleNotes
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Teacher)
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(sn => sn.Lesson.Calendar)
                    .Include(sn => sn.Lesson.Ring)
                    .Include(sn => sn.Lesson.Auditorium)
                    .ToList();
            }
        }

        public List<ScheduleNote> GetFiltredScheduleNotes(Func<ScheduleNote, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context
                    .ScheduleNotes
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Teacher)
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(sn => sn.Lesson.Calendar)
                    .Include(sn => sn.Lesson.Ring)
                    .Include(sn => sn.Lesson.Auditorium)
                    .ToList()
                    .Where(condition)
                    .ToList();
            }
        }

        public ScheduleNote GetFirstFiltredScheduleNotes(Func<ScheduleNote, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context
                    .ScheduleNotes
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Teacher)
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(sn => sn.Lesson.Calendar)
                    .Include(sn => sn.Lesson.Ring)
                    .Include(sn => sn.Lesson.Auditorium)
                    .ToList()
                    .FirstOrDefault(condition);
            }
        }

        public ScheduleNote GetScheduleNote(int scheduleNoteId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context
                    .ScheduleNotes
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Teacher)
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(sn => sn.Lesson.Calendar)
                    .Include(sn => sn.Lesson.Ring)
                    .Include(sn => sn.Lesson.Auditorium)
                    .FirstOrDefault(sn => sn.ScheduleNoteId == scheduleNoteId);
            }
        }

        public void AddScheduleNote(ScheduleNote sNote)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                sNote.ScheduleNoteId = 0;

                sNote.Lesson = context.Lessons.FirstOrDefault(l => l.LessonId == sNote.Lesson.LessonId);

                context.ScheduleNotes.Add(sNote);
                context.SaveChanges();
            }
        }

        public void UpdateScheduleNote(ScheduleNote sNote)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curNote = context.ScheduleNotes.FirstOrDefault(sn => sn.ScheduleNoteId == sNote.ScheduleNoteId);

                if (curNote != null)
                {
                    curNote.IsLesson = sNote.IsLesson;
                    curNote.Lesson = context.Lessons.FirstOrDefault(l => l.LessonId == sNote.Lesson.LessonId);
                    curNote.Text = sNote.Text;
                    curNote.LateAmount = sNote.LateAmount;
                }

                context.SaveChanges();
            }
        }

        public void RemoveScheduleNote(int scheduleNoteId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var sNote = context.ScheduleNotes.FirstOrDefault(sn => sn.ScheduleNoteId == scheduleNoteId);

                context.ScheduleNotes.Remove(sNote);
                context.SaveChanges();
            }
        }

        public void AddScheduleNoteRange(IEnumerable<ScheduleNote> scheduleNoteList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var sNote in scheduleNoteList)
                {
                    sNote.ScheduleNoteId = 0;

                    context.ScheduleNotes.Add(sNote);
                }

                context.SaveChanges();
            }
        }
        #endregion

    }
}
