using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace Schedule.wnu.MySQLViews
{
    public class MySQLRing
    {
        public int RingId { get; set; }
        public string Time { get; set; }

        public MySQLRing(Ring ring)
        {
            RingId = ring.RingId;
            Time = ring.Time.ToString("H:mm:00");
        }

        public static List<MySQLRing> FromRingList(IEnumerable<Ring> list)
        {
            return list.Select(ring => new MySQLRing(ring)).ToList();
        }
    }
}
