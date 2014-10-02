using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.DomainClasses.Main
{
    public class AuditoriumEvent
    {
        public AuditoriumEvent()
        {
        }

        public AuditoriumEvent(string name, Calendar calendar, Ring ring, Auditorium auditorium)
        {
            Name = name;
            Calendar = calendar;
            Ring = ring;
            Auditorium = auditorium;
        }

        public int AuditoriumEventId { get; set; }
        public string Name { get; set; }
        public virtual Calendar Calendar { get; set; }
        public virtual Ring Ring { get; set; }
        public virtual Auditorium Auditorium { get; set; }
    }
}
