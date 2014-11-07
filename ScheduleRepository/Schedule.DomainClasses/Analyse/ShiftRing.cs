using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schedule.DomainClasses.Main;

namespace Schedule.DomainClasses.Analyse
{
    public class ShiftRing
    {
        public ShiftRing()
        {
        }

        public ShiftRing(Shift shift, Ring ring)
        {
            Shift = shift;
            Ring = ring;
        }

        public int ShiftRingId { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual Ring Ring { get; set; }
        
    }
}
