using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Logs;

namespace Schedule.Repositories.Repositories.Logs
{
    public class LessonLogEventsRepository:BaseRepository<LessonLogEvent>
    {
        public List<LessonLogEvent> GetAllLessonLogEvents()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.LessonLog
                    .Include(e => e.OldLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.OldLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.OldLesson.Calendar)
                    .Include(e => e.OldLesson.Ring)
                    .Include(e => e.OldLesson.Auditorium)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.NewLesson.Calendar)
                    .Include(e => e.NewLesson.Ring)
                    .Include(e => e.NewLesson.Auditorium)
                    .ToList();
            }
        }

        public List<LessonLogEvent> GetFiltredLessonLogEvents(Func<LessonLogEvent, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.LessonLog
                    .Include(e => e.OldLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.OldLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.OldLesson.Calendar)
                    .Include(e => e.OldLesson.Ring)
                    .Include(e => e.OldLesson.Auditorium)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.NewLesson.Calendar)
                    .Include(e => e.NewLesson.Ring)
                    .Include(e => e.NewLesson.Auditorium)
                    .ToList().Where(condition).ToList();
            }
        }

        public LessonLogEvent GetFirstFiltredLessonLogEvents(Func<LessonLogEvent, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.LessonLog
                    .Include(e => e.OldLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.OldLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.OldLesson.Calendar)
                    .Include(e => e.OldLesson.Ring)
                    .Include(e => e.OldLesson.Auditorium)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.NewLesson.Calendar)
                    .Include(e => e.NewLesson.Ring)
                    .Include(e => e.NewLesson.Auditorium)
                    .ToList().FirstOrDefault(condition);
            }
        }

        public LessonLogEvent GetLessonLogEvent(int lessonLogEventId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.LessonLog
                    .Include(e => e.OldLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.OldLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.OldLesson.Calendar)
                    .Include(e => e.OldLesson.Ring)
                    .Include(e => e.OldLesson.Auditorium)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.NewLesson.Calendar)
                    .Include(e => e.NewLesson.Ring)
                    .Include(e => e.NewLesson.Auditorium)
                    .FirstOrDefault(lle => lle.LessonLogEventId == lessonLogEventId);
            }
        }

        public void AddLessonLogEvent(LessonLogEvent lessonLogEvent)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                lessonLogEvent.LessonLogEventId = 0;
                if (lessonLogEvent.OldLesson != null)
                {
                    lessonLogEvent.OldLesson =
                        context.Lessons.FirstOrDefault(l => l.LessonId == lessonLogEvent.OldLesson.LessonId);
                }
                if (lessonLogEvent.NewLesson != null)
                {
                    lessonLogEvent.NewLesson =
                        context.Lessons.FirstOrDefault(l => l.LessonId == lessonLogEvent.NewLesson.LessonId);
                }

                context.LessonLog.Add(lessonLogEvent);
                context.SaveChanges();
            }
        }

        public void UpdateLessonLogEvent(LessonLogEvent lessonLogEvent)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curLessonLogEvent = context.LessonLog.FirstOrDefault(lle => lle.LessonLogEventId == lessonLogEvent.LessonLogEventId);

                if (curLessonLogEvent != null)
                {
                    curLessonLogEvent.DateTime = lessonLogEvent.DateTime;
                    curLessonLogEvent.HiddenComment = lessonLogEvent.HiddenComment;
                    curLessonLogEvent.NewLesson = lessonLogEvent.NewLesson;
                    curLessonLogEvent.OldLesson = lessonLogEvent.OldLesson;
                    curLessonLogEvent.PublicComment = lessonLogEvent.PublicComment;
                }

                context.SaveChanges();
            }
        }

        public void RemoveLessonLogEvent(int lessonLogEventId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var lessonLogEvent = context.LessonLog.FirstOrDefault(lle => lle.LessonLogEventId == lessonLogEventId);

                context.LessonLog.Remove(lessonLogEvent);
                context.SaveChanges();
            }
        }

        public void AddLessonLogEventRange(IEnumerable<LessonLogEvent> lessonLogEventList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var lessonLogEvent in lessonLogEventList)
                {
                    lessonLogEvent.LessonLogEventId = 0;

                    lessonLogEvent.OldLesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonLogEvent.OldLesson.LessonId);
                    lessonLogEvent.NewLesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonLogEvent.NewLesson.LessonId);

                    context.LessonLog.Add(lessonLogEvent);
                }

                context.SaveChanges();
            }
        }
    }
}
