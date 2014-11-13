using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Views.DBListViews
{
    public class AuditoriumEventView
    {
        public int AuditoriumEventId { get; set; }
        public string Name { get; set; }
        public string Calendar { get; set; }
        public string Ring { get; set; }
        public string Auditorium { get; set; }

        public AuditoriumEventView()
        {
        }

        public AuditoriumEventView(AuditoriumEvent evt)
        {
            AuditoriumEventId = evt.AuditoriumEventId;
            Name = evt.Name;
            Calendar = evt.Calendar.Date.ToString("d.M.yyyy");
            Ring = evt.Ring.Time.ToString("H:mm");
            Auditorium = evt.Auditorium.Name;
        }

        public static List<AuditoriumEventView> AuditoriumEventsToView(List<AuditoriumEvent> list)
        {
            return list.Select(evt => new AuditoriumEventView(evt)).ToList();
        }
    }
}
