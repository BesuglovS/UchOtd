using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.DomainClasses.Analyse
{
    public class DisciplineAuditorium
    {
        public DisciplineAuditorium()
        {
        }

        public DisciplineAuditorium(Discipline discipline, Auditorium auditorium)
        {
            Discipline = discipline;
            Auditorium = auditorium;
        }

        public int DisciplineAuditoriumId { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual Auditorium Auditorium { get; set; }        
    }
}
