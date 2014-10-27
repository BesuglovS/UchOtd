using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schedule.DomainClasses.Main;

namespace Schedule.DomainClasses.Analyse
{
    public class TeacherRing
    {
        public int TeacherRingId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Ring Ring { get; set; }

        public TeacherRing()
        {
        }

        public TeacherRing(Teacher teacher, Ring ring)
        {
            Teacher = teacher;
            Ring = ring;
        }
    }
}
