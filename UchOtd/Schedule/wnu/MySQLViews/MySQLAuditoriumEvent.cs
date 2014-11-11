using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    public class MySQLAuditoriumEvent
    {
        public int AuditoriumEventId { get; set; }
        public string Name { get; set; }
        public int CalendarId { get; set; }
        public int RingId { get; set; }
        public int AuditoriumId { get; set; }

        public MySQLAuditoriumEvent(AuditoriumEvent evt)
        {
            AuditoriumEventId = evt.AuditoriumEventId;
            Name = evt.Name;
            CalendarId = evt.Calendar.CalendarId;
            RingId = evt.Ring.RingId;
            AuditoriumId = evt.Auditorium.AuditoriumId;
        }

        public static List<MySQLAuditoriumEvent> FromAuditoriumEventList(IEnumerable<AuditoriumEvent> list)
        {
            return list.Select(aEvt => new MySQLAuditoriumEvent(aEvt)).ToList();
        }
    }
}
