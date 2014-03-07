using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    class MySQLGroupsInFaculty
    {
        public int GroupsInFacultyId { get; set; }
        public int StudentGroupId { get; set; }
        public int FacultyId { get; set; }

        public MySQLGroupsInFaculty(GroupsInFaculty gif)
        {
            GroupsInFacultyId = gif.GroupsInFacultyId;
            StudentGroupId = gif.StudentGroup.StudentGroupId;
            FacultyId = gif.Faculty.FacultyId;
        }

        public static List<MySQLGroupsInFaculty> FromGroupsInFacultyList(IEnumerable<GroupsInFaculty> list)
        {
            return list.Select(gif => new MySQLGroupsInFaculty(gif)).ToList();
        }
    }
}
