using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchOtd.Schedule.Views
{
    public class ZachDate
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string DisciplineName { get; set; }
        public DateTime dtDate { get; set; }
        public string Date { get; set; }
        public bool ScheduleCompleted { get; set; }
    }
}
