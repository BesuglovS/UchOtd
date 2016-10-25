using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Views
{
    public class LessonViewAtLessonListByTfd
    {
        public int LessonId { get; set; }
        public string CalendarDate { get; set; }
        public string RingTime { get; set; }
        public string AuditoriumName { get; set; }
        public string Type { get; set; }

        public LessonViewAtLessonListByTfd()
        {

        }

        public LessonViewAtLessonListByTfd(Lesson l)
        {
            LessonId = l.LessonId;
            CalendarDate = l.Calendar.Date.ToString("dd.MM.yyyy");
            RingTime = l.Ring.Time.ToString("H:mm");
            AuditoriumName = l.Auditorium.Name;
        }

        public static List<LessonViewAtLessonListByTfd> FromLessonList(List<Lesson> list)
        {
            return list
                .Select(l => new LessonViewAtLessonListByTfd(l))
                .ToList();
        }
    }
}
