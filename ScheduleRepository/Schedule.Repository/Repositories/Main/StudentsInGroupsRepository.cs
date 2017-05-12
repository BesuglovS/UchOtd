using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class StudentsInGroupsRepository:BaseRepository<StudentsInGroups>
    {
        public List<StudentsInGroups> GetAllStudentsInGroups()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.StudentsInGroups
                    .Include(sig => sig.Student)
                    .Include(sig => sig.StudentGroup.Semester)
                    .ToList();
            }
        }

        public List<StudentsInGroups> GetFiltredStudentsInGroups(Func<StudentsInGroups, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.StudentsInGroups
                    .Include(sig => sig.Student)
                    .Include(sig => sig.StudentGroup.Semester)
                    .ToList().Where(condition).ToList();
            }
        }

        public StudentsInGroups GetFirstFiltredStudentsInGroups(Func<StudentsInGroups, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.StudentsInGroups
                    .Include(sig => sig.Student)
                    .Include(sig => sig.StudentGroup.Semester)
                    .ToList().FirstOrDefault(condition);
            }
        }

        public StudentsInGroups GetStudentsInGroups(int studentsInGroupsId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.StudentsInGroups
                    .Include(sig => sig.Student)
                    .Include(sig => sig.StudentGroup.Semester)
                    .FirstOrDefault(sig => sig.StudentsInGroupsId == studentsInGroupsId);
            }
        }

        public StudentsInGroups FindStudentsInGroups(Student s, StudentGroup sg)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.StudentsInGroups
                    .Include(sig => sig.Student)
                    .Include(sig => sig.StudentGroup.Semester)
                    .FirstOrDefault(sig => sig.Student.StudentId == s.StudentId && sig.StudentGroup.StudentGroupId == sg.StudentGroupId);
            }
        }

        public StudentsInGroups FindStudentsInGroups(string studentF, string studentI, string studentO, string groupName)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.StudentsInGroups
                    .Include(sig => sig.Student)
                    .Include(sig => sig.StudentGroup.Semester)
                    .FirstOrDefault(sig =>
                    sig.Student.F == studentF &&
                    sig.Student.I == studentI &&
                    sig.Student.O == studentO &&
                    sig.StudentGroup.Name == groupName);
            }
        }

        public void AddStudentsInGroups(StudentsInGroups studentsInGroups)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                studentsInGroups.StudentsInGroupsId = 0;

                studentsInGroups.Student = context.Students.FirstOrDefault(s => s.StudentId == studentsInGroups.Student.StudentId);
                studentsInGroups.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == studentsInGroups.StudentGroup.StudentGroupId);

                context.StudentsInGroups.Add(studentsInGroups);
                context.SaveChanges();
            }
        }

        public void UpdateStudentsInGroups(StudentsInGroups studentsInGroups)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curStudentsInGroups = context.StudentsInGroups.FirstOrDefault(sig => sig.StudentsInGroupsId == studentsInGroups.StudentsInGroupsId);

                if (curStudentsInGroups != null)
                {
                    curStudentsInGroups.Student = context.Students.FirstOrDefault(s => s.StudentId == studentsInGroups.Student.StudentId);
                    curStudentsInGroups.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == studentsInGroups.StudentGroup.StudentGroupId);
                }

                context.SaveChanges();
            }
        }

        public void RemoveStudentsInGroups(int studentsInGroupsId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var studentsInGroups = context.StudentsInGroups.FirstOrDefault(sig => sig.StudentsInGroupsId == studentsInGroupsId);

                if (studentsInGroups != null)
                {
                    context.StudentsInGroups.Remove(studentsInGroups);
                    context.SaveChanges();
                }
            }
        }

        public void AddStudentsInGroupsRange(IEnumerable<StudentsInGroups> studentsInGroupsList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var studentsInGroups in studentsInGroupsList)
                {
                    studentsInGroups.StudentsInGroupsId = 0;

                    studentsInGroups.Student = context.Students.FirstOrDefault(s => s.StudentId == studentsInGroups.Student.StudentId);
                    studentsInGroups.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == studentsInGroups.StudentGroup.StudentGroupId);

                    context.StudentsInGroups.Add(studentsInGroups);
                }

                context.SaveChanges();
            }
        }
    }
}
