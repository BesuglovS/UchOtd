using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Views
{
    public class LessonViewAtLessonListByTFD
    {
        public int LessonId { get; set; }
        public string CalendarDate { get; set; }
        public string RingTime { get; set; }
        public string AuditoriumName { get; set; }

        public LessonViewAtLessonListByTFD()
        {

        }

        public LessonViewAtLessonListByTFD(Lesson l)
        {
            LessonId = l.LessonId;
            CalendarDate = l.Calendar.Date.ToString("dd.MM.yyyy");
            RingTime = l.Ring.Time.ToString("H:mm");
            AuditoriumName = l.Auditorium.Name;
        }

        public static List<LessonViewAtLessonListByTFD> FromLessonList(List<Lesson> list)
        {
            return list
                .Select(l => new LessonViewAtLessonListByTFD(l))
                .ToList();
        }
    }
}
