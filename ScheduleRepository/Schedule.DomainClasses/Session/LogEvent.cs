using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
