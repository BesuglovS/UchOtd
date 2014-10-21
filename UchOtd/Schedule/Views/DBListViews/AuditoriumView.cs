using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var result = new List<AuditoriumView>();

            foreach (var aud in list)
            {
                result.Add(new AuditoriumView(aud));
            }

            return result;
        }
    }
}
