using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    public class MySqlRing
    {
        public int RingId { get; set; }
        public string Time { get; set; }

        public MySqlRing(Ring ring)
        {
            RingId = ring.RingId;
            Time = ring.Time.ToString("H:mm:00");
        }

        public static List<MySqlRing> FromRingList(IEnumerable<Ring> list)
        {
            return list.Select(ring => new MySqlRing(ring)).ToList();
        }
    }
}
