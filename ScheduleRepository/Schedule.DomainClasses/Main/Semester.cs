using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DomainClasses.Main
{
    public class Semester
    {
        public Semester()
        {
        }

        public Semester(int startingYear, int semesterInYear, string displayName)
        {
            StartingYear = startingYear;
            SemesterInYear = semesterInYear;
            DisplayName = displayName;
        }

        public int SemesterId { get; set; }
        public int StartingYear { get; set; }
        public int SemesterInYear { get; set; }
        public string DisplayName { get; set; }
    }
}
