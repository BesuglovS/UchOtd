using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    class MySqlAuditorium
    {
        public int AuditoriumId { get; set; }
        public string Name { get; set; }
        public int BuildingId { get; set; }

        public MySqlAuditorium(Auditorium auditorium)
        {
            AuditoriumId = auditorium.AuditoriumId;
            Name = auditorium.Name;
            BuildingId = auditorium.Building.BuildingId;
        }

        public static List<MySqlAuditorium> FromAuditoriumList(IEnumerable<Auditorium> list)
        {
            return list.Select(auditorium => new MySqlAuditorium(auditorium)).ToList();
        }
    }
}
