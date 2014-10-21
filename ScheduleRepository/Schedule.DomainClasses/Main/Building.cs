using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.DomainClasses.Main
{
    public class Building
    {
        public Building()
        {
        }

        public Building(string name)
        {
            Name = name;
        }

        public int BuildingId { get; set; }
        public string Name { get; set; }
    }
}
