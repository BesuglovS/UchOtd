using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schedule.DomainClasses.Main;

namespace UchOtd.Core
{
    public class BuildingLessonsData
    {
        public List<Auditorium> sortedAuditoriums;
        public List<Ring> rings;
        public Dictionary<int, Dictionary<int, string>> buildingAuditoriums;
    }
}
