using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class StudentGroupsRepository: BaseRepository<StudentGroup>
    {

        public List<StudentGroup> GetAllStudentGroups()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.StudentGroups.Include(sg => sg.Semester).ToList();
            }
        }

        public List<StudentGroup> GetFiltredStudentGroups(Func<StudentGroup, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.StudentGroups.Include(sg => sg.Semester).ToList().Where(condition).ToList();
            }
        }

        public StudentGroup GetFirstFiltredStudentGroups(Func<StudentGroup, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.StudentGroups.Include(sg => sg.Semester).ToList().FirstOrDefault(condition);
            }
        }

        public StudentGroup GetStudentGroup(int studentGroupId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.StudentGroups.Include(sg => sg.Semester).FirstOrDefault(sg => sg.StudentGroupId == studentGroupId);
            }
        }

        public StudentGroup FindStudentGroup(string name, Semester semester)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.StudentGroups.Include(sg => sg.Semester).FirstOrDefault(sg => sg.Name == name && sg.Semester.SemesterId == semester.SemesterId);
            }
        }

        public void AddStudentGroup(StudentGroup studentGroup)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                studentGroup.StudentGroupId = 0;
                studentGroup.Semester = context.Semesters.FirstOrDefault(s => s.SemesterId == studentGroup.Semester.SemesterId);

                context.StudentGroups.Add(studentGroup);
                context.SaveChanges();
            }
        }

        public void UpdateStudentGroup(StudentGroup studentGroup)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curStudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == studentGroup.StudentGroupId);

                if (curStudentGroup != null)
                {
                    curStudentGroup.Name = studentGroup.Name;
                    curStudentGroup.Semester = context.Semesters.FirstOrDefault(s => s.SemesterId == studentGroup.Semester.SemesterId);
                }

                context.SaveChanges();
            }
        }

        public void RemoveStudentGroup(int studentGroupId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var studentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == studentGroupId);

                context.StudentGroups.Remove(studentGroup);
                context.SaveChanges();
            }
        }

        public void AddStudentGroupRange(IEnumerable<StudentGroup> studentGroupList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var studentGroup in studentGroupList)
                {
                    studentGroup.StudentGroupId = 0;
                    studentGroup.Semester = context.Semesters.FirstOrDefault(s => s.SemesterId == studentGroup.Semester.SemesterId);
                    context.StudentGroups.Add(studentGroup);
                }

                context.SaveChanges();
            }
        }
    }
}
