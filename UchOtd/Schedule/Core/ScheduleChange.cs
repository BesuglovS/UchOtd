using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Core
{
    public class ScheduleChange
    {
        public int Type { get; set; }
        public Lesson OldLesson { get; set; }
        public Lesson NewLesson { get; set; }
    }
}
