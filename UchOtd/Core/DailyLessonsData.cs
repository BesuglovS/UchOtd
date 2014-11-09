using Schedule.DomainClasses.Main;
using System.Collections.Generic;

namespace UchOtd.Core
{
    public class DailyLessonsData
    {
        public Dictionary<int, Dictionary<int, List<Lesson>>> LessonsData;
        public List<Ring> Rings;
        public List<StudentGroup> Groups;
    }
}
