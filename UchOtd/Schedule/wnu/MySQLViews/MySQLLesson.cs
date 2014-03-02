using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace Schedule.wnu.MySQLViews
{
    class MySQLLesson
    {
        public int LessonId { get; set; }
        public int IsActive { get; set; }
        public int TeacherForDisciplineId { get; set; }
        public int CalendarId { get; set; }
        public int RingId { get; set; }
        public int AuditoriumId { get; set; }

        public MySQLLesson(Lesson lesson)
        {
            LessonId = lesson.LessonId;
            IsActive = lesson.IsActive ? 1 : 0;
            TeacherForDisciplineId = lesson.TeacherForDiscipline.TeacherForDisciplineId;
            CalendarId = lesson.Calendar.CalendarId;
            RingId = lesson.Ring.RingId;
            AuditoriumId = lesson.Auditorium.AuditoriumId;
        }

        public static List<MySQLLesson> FromLessonList(IEnumerable<Lesson> list)
        {
            return list.Select(lesson => new MySQLLesson(lesson)).ToList();
        }
    }
}
