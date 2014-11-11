using System;

namespace Schedule.DomainClasses.Session
{
    public class LogEvent
    {
        public int LogEventId { get; set; }
        public virtual Exam OldExam { get; set; }
        public virtual Exam NewExam { get; set; }
        public DateTime DateTime { get; set; }
    }
}
