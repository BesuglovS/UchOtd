using System.Collections.Generic;
using Schedule.DomainClasses.Main;

namespace UchOtd.Core
{
    public class DailyLessonsData
    {
        public Dictionary<int, Dictionary<int, List<Lesson>>> LessonsData;
        public List<Ring> Rings;
        public List<StudentGroup> Groups;
    }
}
