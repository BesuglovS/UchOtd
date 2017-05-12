using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Views.DBListViews
{
    public class StudentsInGroupsView
    {
        public int StudentsInGroupsId { get; set; }
        public int StudentId { get; set; }
        public int StudentGroupId { get; set; }
        public string StudentFioZachNum { get; set; }
        public string StudentGroup { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }

        public StudentsInGroupsView()
        {
        }

        public StudentsInGroupsView(StudentsInGroups sig)
        {
            StudentsInGroupsId = sig.StudentsInGroupsId;
            StudentId = sig.Student.StudentId;
            StudentGroupId = sig.StudentGroup.StudentGroupId;
            StudentFioZachNum = sig.Student.F + " " + sig.Student.I + " " + sig.Student.O + " " + sig.Student.ZachNumber;
            StudentGroup = sig.StudentGroup.Name + " " + sig.StudentGroup.Semester.DisplayName;
            PeriodFrom = sig.PeriodFrom;
            PeriodTo = sig.PeriodTo;
        }

        public static List<StudentsInGroupsView> SigToView(List<StudentsInGroups> list)
        {
            return list.Select(sig => new StudentsInGroupsView(sig)).ToList();
        }
    }
}
