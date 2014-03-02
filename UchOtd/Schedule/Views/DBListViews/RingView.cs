using Schedule.DomainClasses.Main;
using System.Collections.Generic;

namespace Schedule.Views.DBListViews
{
    public class RingView
    {
        public int RingId { get; set; }
        public string Time { get; set; }

        public RingView()
        {
        }

        public RingView(Ring ring)
        {
            RingId = ring.RingId;
            Time = ring.Time.ToString("H:mm");
        }

        public static List<RingView> RingsToView(List<Ring> list)
        {
            var result = new List<RingView>();

            foreach (var ring in list)
            {
                result.Add(new RingView(ring));
            }

            return result;
        }       
    }
}
