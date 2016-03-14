using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DomainClasses.Main;

namespace UchOtd.Views
{
    public class TeacherWithLocation
    {
        public string Teacher { get; set; }
        public string Auditorium { get; set; }
        public string Calendar { get; set; }
        public string Ring { get; set; }

        public TeacherWithLocation(string teacher, string auditorium, string calendar, string ring)
        {
            Teacher = teacher;
            Auditorium = auditorium;
            Calendar = calendar;
            Ring = ring;
        }
    }
}
