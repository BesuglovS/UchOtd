using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.DomainClasses.Analyse
{
    public class ProposedLesson
    {
        public ProposedLesson()
        {
        }

        public ProposedLesson(TeacherForDiscipline teacherForDiscipline, Calendar calendar,
                      Ring ring, Auditorium auditorium)
        {
            TeacherForDiscipline = teacherForDiscipline;
            Calendar = calendar;
            Ring = ring;
            Auditorium = auditorium;
        }

        public int ProposedLessonId { get; set; }
        public bool IsActive { get; set; }
        public virtual TeacherForDiscipline TeacherForDiscipline { get; set; }
        public virtual Calendar Calendar { get; set; }
        public virtual Ring Ring { get; set; }
        public virtual Auditorium Auditorium { get; set; }
    }
}
