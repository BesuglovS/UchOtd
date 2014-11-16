using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class TeachersRepository: BaseRepository<Teacher>
    {
        public List<Teacher> GetAllTeachers()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Teachers.ToList();
            }
        }

        public List<Teacher> GetFiltredTeachers(Func<Teacher, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Teachers.ToList().Where(condition).ToList();
            }
        }

        public Teacher GetFirstFiltredTeachers(Func<Teacher, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Teachers.ToList().FirstOrDefault(condition);
            }
        }

        public Teacher GetTeacher(int teacherId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
            }
        }

        public Teacher FindTeacher(string fio, string phone)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Teachers.FirstOrDefault(t => t.FIO == fio && t.Phone == phone);
            }
        }

        public void AddTeacher(Teacher teacher)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                teacher.TeacherId = 0;

                context.Teachers.Add(teacher);
                context.SaveChanges();
            }
        }

        public void UpdateTeacher(Teacher teacher)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curTeacher = context.Teachers.FirstOrDefault(t => t.TeacherId == teacher.TeacherId);

                if (curTeacher != null)
                {
                    curTeacher.FIO = teacher.FIO;
                    curTeacher.Phone = teacher.Phone;
                }

                context.SaveChanges();
            }
        }

        public void RemoveTeacher(int teacherId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);

                context.Teachers.Remove(teacher);
                context.SaveChanges();
            }
        }

        public void AddTeacherRange(IEnumerable<Teacher> teacherList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var teacher in teacherList)
                {
                    teacher.TeacherId = 0;
                    context.Teachers.Add(teacher);
                }

                context.SaveChanges();
            }
        }
    }
}
