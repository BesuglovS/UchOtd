using Schedule.DomainClasses.Main;
using System.Collections.Generic;
using System.Linq;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    class MySqlGroupsInFaculty
    {
        public int GroupsInFacultyId { get; set; }
        public int StudentGroupId { get; set; }
        public int FacultyId { get; set; }

        public MySqlGroupsInFaculty(GroupsInFaculty gif)
        {
            GroupsInFacultyId = gif.GroupsInFacultyId;
            StudentGroupId = gif.StudentGroup.StudentGroupId;
            FacultyId = gif.Faculty.FacultyId;
        }

        public static List<MySqlGroupsInFaculty> FromGroupsInFacultyList(IEnumerable<GroupsInFaculty> list)
        {
            return list.Select(gif => new MySqlGroupsInFaculty(gif)).ToList();
        }
    }
}
