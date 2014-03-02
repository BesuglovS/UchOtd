using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace Schedule.wnu.MySQLViews
{
    class MySQLStudentsInGroups
    {
        public int StudentsInGroupsId { get; set; }
        public int StudentId { get; set; }
        public int StudentGroupId { get; set; }

        public MySQLStudentsInGroups(StudentsInGroups sig)
        {
            StudentsInGroupsId = sig.StudentsInGroupsId;
            StudentId = sig.Student.StudentId;
            StudentGroupId = sig.StudentGroup.StudentGroupId;
        }

        public static List<MySQLStudentsInGroups> FromStudentsInGroupsList(IEnumerable<StudentsInGroups> list)
        {
            return list.Select(sig => new MySQLStudentsInGroups(sig)).ToList();
        }
    }
}
