using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    class MySqlStudentsInGroups
    {
        public int StudentsInGroupsId { get; set; }
        public int StudentId { get; set; }
        public int StudentGroupId { get; set; }

        public MySqlStudentsInGroups(StudentsInGroups sig)
        {
            StudentsInGroupsId = sig.StudentsInGroupsId;
            StudentId = sig.Student.StudentId;
            StudentGroupId = sig.StudentGroup.StudentGroupId;
        }

        public static List<MySqlStudentsInGroups> FromStudentsInGroupsList(IEnumerable<StudentsInGroups> list)
        {
            return list.Select(sig => new MySqlStudentsInGroups(sig)).ToList();
        }
    }
}
