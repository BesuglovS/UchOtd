using Schedule.DomainClasses.Main;
using System.Collections.Generic;

namespace UchOtd.Core
{
    public class DailyLessonsData
    {
        public Dictionary<int, Dictionary<int, List<Lesson>>> lessonsData;
        public List<Ring> rings;
        public List<StudentGroup> groups;
    }
}
