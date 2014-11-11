using System.Collections.Generic;
using Schedule.DomainClasses.Main;

namespace UchOtd.Core
{
    public class BuildingLessonsData
    {
        public List<Auditorium> SortedAuditoriums;
        public List<Ring> Rings;
        public Dictionary<int, Dictionary<int, string>> BuildingAuditoriums;
    }
}
