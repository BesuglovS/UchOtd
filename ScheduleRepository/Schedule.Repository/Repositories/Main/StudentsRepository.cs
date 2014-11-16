using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class StudentsRepository: BaseRepository<Student>
    {
        public List<Student> GetAllStudents()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Students.ToList();
            }
        }

        public List<Student> GetGroupStudents(string groupName)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var group = context.StudentGroups.FirstOrDefault(g => g.Name == groupName);
                if (group == null)
                {
                    return null;
                }

                return context.StudentsInGroups.Where(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId).Select(stig => stig.Student).ToList();
            }
        }

        public List<Student> GetFiltredStudents(Func<Student, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Students.ToList().Where(condition).ToList();
            }
        }

        public Student GetFirstFiltredStudents(Func<Student, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Students.ToList().FirstOrDefault(condition);
            }
        }

        public Student GetStudent(int studentId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Students.FirstOrDefault(s => s.StudentId == studentId);
            }
        }

        public Student FindStudent(string f, string i, string o)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Students.FirstOrDefault(s => s.F == f && s.I == i && s.O == o);
            }
        }

        public Student FindStudent(string zachNumber)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Students.FirstOrDefault(s => s.ZachNumber == zachNumber);
            }
        }

        public void AddStudent(Student student)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                student.StudentId = 0;

                context.Students.Add(student);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curStudent = context.Students.FirstOrDefault(s => s.StudentId == student.StudentId);

                if (curStudent != null)
                {
                    curStudent.F = student.F;
                    curStudent.I = student.I;
                    curStudent.O = student.O;

                    curStudent.ZachNumber = student.ZachNumber;
                    curStudent.BirthDate = student.BirthDate;
                    curStudent.Address = student.Address;
                    curStudent.Phone = student.Phone;
                    curStudent.Starosta = student.Starosta;
                    curStudent.NFactor = student.NFactor;
                    curStudent.PaidEdu = student.PaidEdu;
                    curStudent.Expelled = student.Expelled;
                    curStudent.Orders = student.Orders;
                }

                context.SaveChanges();
            }
        }

        public void RemoveStudent(int studentId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var student = context.Students.FirstOrDefault(s => s.StudentId == studentId);

                context.Students.Remove(student);
                context.SaveChanges();
            }
        }

        public void AddStudentRange(IEnumerable<Student> studentList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var student in studentList)
                {
                    student.StudentId = 0;
                    context.Students.Add(student);
                }

                context.SaveChanges();
            }
        }
    }
}
