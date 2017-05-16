using System;

namespace UchOtd.Schedule.MainImport
{
    public class LogEvent
    {
        public int LogEventId { get; set; }
        public virtual Exam OldExam { get; set; }
        public virtual Exam NewExam { get; set; }
        public DateTime DateTime { get; set; }
    }
}
