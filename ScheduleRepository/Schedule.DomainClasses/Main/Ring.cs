using System;

namespace Schedule.DomainClasses.Main
{
    public class Ring
    {
        public Ring()
        {
        }

        public Ring(DateTime time)
        {
            Time = time;
        }

        public int RingId { get; set; }
        public DateTime Time { get; set; }
    }
}
