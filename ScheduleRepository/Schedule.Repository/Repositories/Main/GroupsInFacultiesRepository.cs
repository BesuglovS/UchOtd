using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class GroupsInFacultiesRepository:BaseRepository<GroupsInFaculty>
    {
        public List<GroupsInFaculty> GetAllGroupsInFaculty()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .ToList();
            }
        }

        public List<GroupsInFaculty> GetFiltredGroupsInFaculty(Func<GroupsInFaculty, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .ToList().Where(condition).ToList();
            }
        }

        public GroupsInFaculty GetFirstFiltredGroupsInFaculty(Func<GroupsInFaculty, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .ToList().FirstOrDefault(condition);
            }
        }

        public GroupsInFaculty GetGroupsInFaculty(int groupsInFaculty)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .FirstOrDefault(gif => gif.GroupsInFacultyId == groupsInFaculty);
            }
        }

        public GroupsInFaculty FindGroupsInFaculty(StudentGroup sg, Faculty f)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .FirstOrDefault(gif => gif.StudentGroup.StudentGroupId == sg.StudentGroupId &&
                                           gif.Faculty.FacultyId == f.FacultyId);
            }
        }

        public GroupsInFaculty FindGroupsInFaculty(string studentGroupName, string facultyName)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .FirstOrDefault(gif => gif.StudentGroup.Name == studentGroupName &&
                                           gif.Faculty.Name == facultyName);
            }
        }

        public void AddGroupsInFaculty(GroupsInFaculty groupsInFaculty)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                groupsInFaculty.GroupsInFacultyId = 0;

                groupsInFaculty.StudentGroup = context.StudentGroups.FirstOrDefault(gif => gif.StudentGroupId == groupsInFaculty.StudentGroup.StudentGroupId);
                groupsInFaculty.Faculty = context.Faculties.FirstOrDefault(gif => gif.FacultyId == groupsInFaculty.Faculty.FacultyId);

                context.GroupsInFaculties.Add(groupsInFaculty);
                context.SaveChanges();
            }
        }

        public void UpdateGroupsInFaculty(GroupsInFaculty groupsInFaculty)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curGroupsInFaculty = context.GroupsInFaculties.FirstOrDefault(gif => gif.GroupsInFacultyId == groupsInFaculty.GroupsInFacultyId);

                if (curGroupsInFaculty != null)
                {
                    curGroupsInFaculty.StudentGroup = groupsInFaculty.StudentGroup;
                    curGroupsInFaculty.Faculty = groupsInFaculty.Faculty;
                }

                context.SaveChanges();
            }
        }

        public void RemoveGroupsInFaculty(int groupsInFacultyId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var groupsInFaculty = context.GroupsInFaculties.FirstOrDefault(gif => gif.GroupsInFacultyId == groupsInFacultyId);

                context.GroupsInFaculties.Remove(groupsInFaculty);
                context.SaveChanges();
            }
        }

        public void AddGroupsInFacultyRange(IEnumerable<GroupsInFaculty> groupsInFacultiesList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var groupsInFaculty in groupsInFacultiesList)
                {
                    groupsInFaculty.GroupsInFacultyId = 0;

                    groupsInFaculty.StudentGroup = context.StudentGroups.FirstOrDefault(gif => gif.StudentGroupId == groupsInFaculty.StudentGroup.StudentGroupId);
                    groupsInFaculty.Faculty = context.Faculties.FirstOrDefault(gif => gif.FacultyId == groupsInFaculty.Faculty.FacultyId);

                    context.GroupsInFaculties.Add(groupsInFaculty);
                }

                context.SaveChanges();
            }
        }
    }
}
