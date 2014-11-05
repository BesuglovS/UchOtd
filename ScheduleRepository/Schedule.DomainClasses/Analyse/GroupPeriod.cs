using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schedule.DomainClasses.Main;

namespace Schedule.DomainClasses.Analyse
{
    public class GroupPeriod
    {
        public int GroupPeriodId { get; set; } 
        public string Name { get; set; }
        public StudentGroup StudentGroup { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
