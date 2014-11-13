using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    class MySqlLesson
    {
        public int LessonId { get; set; }
        public int IsActive { get; set; }
        public int TeacherForDisciplineId { get; set; }
        public int CalendarId { get; set; }
        public int RingId { get; set; }
        public int AuditoriumId { get; set; }

        public MySqlLesson(Lesson lesson)
        {
            LessonId = lesson.LessonId;
            IsActive = lesson.State;
            TeacherForDisciplineId = lesson.TeacherForDiscipline.TeacherForDisciplineId;
            CalendarId = lesson.Calendar.CalendarId;
            RingId = lesson.Ring.RingId;
            AuditoriumId = lesson.Auditorium.AuditoriumId;
        }

        public static List<MySqlLesson> FromLessonList(IEnumerable<Lesson> list)
        {
            return list.Select(lesson => new MySqlLesson(lesson)).ToList();
        }
    }
}
