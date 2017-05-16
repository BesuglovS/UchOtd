using System;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.MainImport
{
    public class LessonLogEvent
    {
        public int LessonLogEventId { get; set; }
        public virtual Lesson OldLesson { get; set; }
        public virtual Lesson NewLesson { get; set; }
        public DateTime DateTime { get; set; }
        public string PublicComment { get; set; }
        public string HiddenComment { get; set; }
    }
}
