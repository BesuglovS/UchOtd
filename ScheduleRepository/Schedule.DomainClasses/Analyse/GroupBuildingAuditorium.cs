using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.DomainClasses.Analyse
{
    public class GroupBuildingAuditorium
    {
        public int GroupBuildingAuditoriumId { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
        public virtual Building Building { get; set; }
        public virtual Auditorium Auditorium { get; set; }
    }
}
