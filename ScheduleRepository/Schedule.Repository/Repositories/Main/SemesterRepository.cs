using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class SemesterRepository: BaseRepository<Semester>
    {
        public List<Semester> GetAllSemesters()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Semesters.ToList();
            }
        }

        public List<Semester> GetFiltredSemesters(Func<Semester, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Semesters.ToList().Where(condition).ToList();
            }
        }

        public Semester GetFirstFiltredSemester(Func<Semester, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Semesters.ToList().FirstOrDefault(condition);
            }
        }

        public Semester GetSemester(int semesterId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Semesters.FirstOrDefault(s => s.SemesterId == semesterId);
            }
        }
        
        public void AddSemester(Semester semester)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                semester.SemesterId = 0;

                context.Semesters.Add(semester);
                context.SaveChanges();
            }
        }

        public void UpdateSemester(Semester semester)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curSemester = context.Semesters.FirstOrDefault(s => s.SemesterId == semester.SemesterId);

                if (curSemester != null)
                {
                    curSemester.StartingYear = semester.StartingYear;
                    curSemester.SemesterInYear = semester.SemesterInYear;
                    curSemester.DisplayName = semester.DisplayName;
                }

                context.SaveChanges();
            }
        }

        public void RemoveSemester(int semesterId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var semester = context.Semesters.FirstOrDefault(s => s.SemesterId == semesterId);

                context.Semesters.Remove(semester);
                context.SaveChanges();
            }
        }

        public void AddSemesterRange(IEnumerable<Semester> semesterList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var semester in semesterList)
                {
                    semester.SemesterId = 0;
                    context.Semesters.Add(semester);
                }

                context.SaveChanges();
            }
        }
    }
}
