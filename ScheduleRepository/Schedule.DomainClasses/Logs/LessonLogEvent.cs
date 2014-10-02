using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.DomainClasses.Logs
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
