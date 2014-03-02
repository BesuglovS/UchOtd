using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Views
{
    public class LessonViewAtLessonListByTeacher
    {
        public int LessonId { get; set; }
        public string DisciplineName { get; set; }
        public string GroupName { get; set; }
        public string CalendarDate { get; set; }
        public string RingTime { get; set; }
        public string AuditoriumName { get; set; }        

        public LessonViewAtLessonListByTeacher()
        {

        }

        public LessonViewAtLessonListByTeacher(Lesson l)
        {
            LessonId = l.LessonId;
            DisciplineName = l.TeacherForDiscipline.Discipline.Name;
            GroupName = l.TeacherForDiscipline.Discipline.StudentGroup.Name;
            CalendarDate = l.Calendar.Date.ToString("dd.MM.yyyy");
            RingTime = l.Ring.Time.ToString("H:mm");
            AuditoriumName = l.Auditorium.Name;            
        }

        public static List<LessonViewAtLessonListByTeacher> FromLessonList(List<Lesson> list)
        {
            return list
                .Select(l => new LessonViewAtLessonListByTeacher(l))
                .ToList();
        }
    }
}
