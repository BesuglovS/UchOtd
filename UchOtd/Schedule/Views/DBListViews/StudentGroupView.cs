using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Views.DBListViews
{
    public class StudentGroupView
    {
        public int StudentGroupId { get; set; }
        public string Name { get; set; }
        public string SemesterDisplayName { get; set; }
        public string NameWithSemester { get; set; }

        public StudentGroupView()
        {
        }

        public StudentGroupView(StudentGroup studentGroup)
        {
            StudentGroupId = studentGroup.StudentGroupId;
            Name = studentGroup.Name;
            SemesterDisplayName = studentGroup.Semester.DisplayName;
            NameWithSemester = Name + " " + SemesterDisplayName;
        }

        public static List<StudentGroupView> ViewFromList(List<StudentGroup> list)
        {
            return list.Select(sg => new StudentGroupView(sg)).ToList();
        }
    }
}
