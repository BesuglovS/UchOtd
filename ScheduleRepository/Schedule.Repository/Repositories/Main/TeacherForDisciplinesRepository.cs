using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class TeacherForDisciplinesRepository:BaseRepository<TeacherForDiscipline>
    {
        public List<TeacherForDiscipline> GetAllTeacherForDiscipline()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).ToList();
            }
        }

        public List<TeacherForDiscipline> GetFiltredTeacherForDiscipline(Func<TeacherForDiscipline, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).ToList().Where(condition).ToList();
            }
        }

        public TeacherForDiscipline GetFirstFiltredTeacherForDiscipline(Func<TeacherForDiscipline, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).ToList().FirstOrDefault(condition);
            }
        }

        public TeacherForDiscipline GetTeacherForDiscipline(int teacherForDisciplineId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).FirstOrDefault(tfd => tfd.TeacherForDisciplineId == teacherForDisciplineId);
            }
        }

        public TeacherForDiscipline FindTeacherForDiscipline(Teacher t, Discipline d)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).FirstOrDefault(tfd => tfd.Teacher.TeacherId == t.TeacherId && tfd.Discipline.DisciplineId == d.DisciplineId);
            }
        }

        public TeacherForDiscipline FindTeacherForDiscipline(string teacherFio, string disciplineName, int disciplineAttestation,
            int disciplineAuditoriumHours, int disciplineLectureHours, int disciplinePracticalHours, string disciplineGroupName)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).FirstOrDefault(tfd =>
                    tfd.Teacher.FIO == teacherFio &&
                    tfd.Discipline.Name == disciplineName &&
                    tfd.Discipline.Attestation == disciplineAttestation &&
                    tfd.Discipline.AuditoriumHours == disciplineAuditoriumHours &&
                    tfd.Discipline.LectureHours == disciplineLectureHours &&
                    tfd.Discipline.PracticalHours == disciplinePracticalHours &&
                    tfd.Discipline.StudentGroup.Name == disciplineGroupName);
            }
        }

        public void AddTeacherForDiscipline(TeacherForDiscipline teacherForDiscipline)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                teacherForDiscipline.TeacherForDisciplineId = 0;

                teacherForDiscipline.Teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == teacherForDiscipline.Teacher.TeacherId);
                teacherForDiscipline.Discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == teacherForDiscipline.Discipline.DisciplineId);

                context.TeacherForDiscipline.Add(teacherForDiscipline);
                context.SaveChanges();
            }
        }

        public void UpdateTeacherForDiscipline(TeacherForDiscipline teacherForDiscipline)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curTeacherForDiscipline = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == teacherForDiscipline.TeacherForDisciplineId);

                if (curTeacherForDiscipline != null)
                {
                    curTeacherForDiscipline.Teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == teacherForDiscipline.Teacher.TeacherId);
                    curTeacherForDiscipline.Discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == teacherForDiscipline.Discipline.DisciplineId);
                }

                context.SaveChanges();
            }
        }

        public void RemoveTeacherForDiscipline(int teacherForDisciplineId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var teacherForDiscipline = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == teacherForDisciplineId);

                context.TeacherForDiscipline.Remove(teacherForDiscipline);
                context.SaveChanges();
            }
        }

        public void AddTeacherForDisciplineRange(IEnumerable<TeacherForDiscipline> teacherForDisciplineList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var teacherForDiscipline in teacherForDisciplineList)
                {
                    teacherForDiscipline.TeacherForDisciplineId = 0;
                    context.TeacherForDiscipline.Add(teacherForDiscipline);
                }

                context.SaveChanges();
            }
        }
    }
}
