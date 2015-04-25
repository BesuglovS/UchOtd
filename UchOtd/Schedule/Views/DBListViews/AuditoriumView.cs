using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Views.DBListViews
{
    public class AuditoriumView
    {
        public int AuditoriumId { get; set; }
        public string Name { get; set; }
        public string Buildingname { get; set; }

        public AuditoriumView(Auditorium auditorium)
        {
            AuditoriumId = auditorium.AuditoriumId;
            Name = auditorium.Name;
            if (auditorium.Building != null)
            {
                Buildingname = auditorium.Building.Name;
            }
        }

        public static List<AuditoriumView> AuditoriumsToView(List<Auditorium> list)
        {
            return list.Select(aud => new AuditoriumView(aud)).ToList();
        }
    }
}
