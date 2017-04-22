using System;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class DisciplinesRepository: BaseRepository<Discipline>
    {
        public List<Discipline> GetAllDisciplines()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Disciplines.Include(d => d.StudentGroup).Include(d => d.Semester).ToList();
            }
        }

        public List<Discipline> GetFiltredDisciplines(Func<Discipline, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Disciplines.Include(d => d.StudentGroup).Include(d => d.Semester).ToList().Where(condition).ToList();
            }
        }

        public List<Discipline> GetTeacherDisciplines(Teacher teacher)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherForDiscipline.Where(tfd => tfd.Teacher.TeacherId == teacher.TeacherId).Select(tefd => tefd.Discipline).Include(d => d.StudentGroup).Include(d => d.Semester).ToList();
            }
        }

        public Discipline GetFirstFiltredDisciplines(Func<Discipline, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Disciplines.Include(d => d.StudentGroup).Include(d => d.Semester).ToList().FirstOrDefault(condition);
            }
        }

        public Discipline GetDiscipline(int disciplineId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Disciplines.Include(d => d.StudentGroup).Include(d => d.Semester).FirstOrDefault(d => d.DisciplineId == disciplineId);
            }
        }

        public Discipline FindDiscipline(string name, int attestation, int auditoriumHours, int lectureHours, int practicalHours, string groupName)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Disciplines.Include(d => d.StudentGroup).Include(d => d.Semester).FirstOrDefault(
                    d => d.Name == name &&
                         d.Attestation == attestation &&
                         d.AuditoriumHours == auditoriumHours &&
                         d.LectureHours == lectureHours &&
                         d.PracticalHours == practicalHours &&
                         d.StudentGroup.Name == groupName);
            }
        }

        public void AddDiscipline(Discipline discipline)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                discipline.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == discipline.StudentGroup.StudentGroupId);
                discipline.Semester = context.Semesters.FirstOrDefault(s => s.SemesterId == discipline.Semester.SemesterId);
                context.Disciplines.Add(discipline);

                context.SaveChanges();
            }
        }

        public void UpdateDiscipline(Discipline discipline)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curDiscipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == discipline.DisciplineId);

                if (curDiscipline != null)
                {
                    curDiscipline.Attestation = discipline.Attestation;
                    curDiscipline.AuditoriumHours = discipline.AuditoriumHours;
                    curDiscipline.AuditoriumHoursPerWeek = discipline.AuditoriumHoursPerWeek;
                    curDiscipline.LectureHours = discipline.LectureHours;
                    curDiscipline.Name = discipline.Name;
                    curDiscipline.PracticalHours = discipline.PracticalHours;
                    curDiscipline.TypeSequence = discipline.TypeSequence;
                    curDiscipline.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == discipline.StudentGroup.StudentGroupId);
                    curDiscipline.Semester = context.Semesters.FirstOrDefault(s => s.SemesterId == discipline.Semester.SemesterId);
                }

                context.SaveChanges();
            }
        }

        public void RemoveDiscipline(int disciplineId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == disciplineId);

                context.Disciplines.Remove(discipline);
                context.SaveChanges();
            }
        }

        public void AddDisciplineRange(IEnumerable<Discipline> disciplineList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var discipline in disciplineList)
                {
                    discipline.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == discipline.StudentGroup.StudentGroupId);
                    discipline.Semester = context.Semesters.FirstOrDefault(s => s.SemesterId == discipline.Semester.SemesterId);
                    context.Disciplines.Add(discipline);
                }

                context.SaveChanges();
            }
        }
    }
}
