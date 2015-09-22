using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class DisciplineNameRepository : BaseRepository<Discipline>
    {
        public List<DisciplineName> GetAllDisciplineNames()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.DisciplineNames
                    .Include(dn => dn.Discipline.StudentGroup)
                    .Include(dn => dn.StudentGroup)
                    .ToList();
            }
        }

        public List<DisciplineName> GetFiltredDisciplineNames(Func<DisciplineName, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.DisciplineNames
                    .Include(dn => dn.Discipline.StudentGroup)
                    .Include(dn => dn.StudentGroup)
                    .ToList().Where(condition).ToList();
            }
        }

        public List<DisciplineName> GetDisciplineNames(Discipline discipline)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.DisciplineNames
                    .Include(dn => dn.Discipline.StudentGroup)
                    .Include(dn => dn.StudentGroup)
                    .ToList()
                    .Where(dn => dn.Discipline.DisciplineId == discipline.DisciplineId)
                    .ToList();
            }
        }

        // StudentGroupId, DisciplineName
        public Dictionary<int, string> GetDisciplineNamesDictionary(Discipline discipline)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.DisciplineNames
                    .Include(dn => dn.Discipline.StudentGroup)
                    .Include(dn => dn.StudentGroup)
                    .ToList()
                    .Where(dn => dn.Discipline.DisciplineId == discipline.DisciplineId)
                    .ToDictionary(dn => dn.StudentGroup.StudentGroupId, dn => dn.Name);
            }
        }

        public DisciplineName GetFirstFiltredDisciplineName(Func<DisciplineName, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.DisciplineNames
                    .Include(dn => dn.Discipline.StudentGroup)
                    .Include(dn => dn.StudentGroup)
                    .ToList().FirstOrDefault(condition);
            }
        }

        public DisciplineName GetDisciplineName(int disciplineNameId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.DisciplineNames
                    .Include(dn => dn.Discipline.StudentGroup)
                    .Include(dn => dn.StudentGroup)
                    .FirstOrDefault(dn => dn.DisciplineNameId == disciplineNameId);
            }
        }
        
        public void AddDisciplineName(DisciplineName disciplineName)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                disciplineName.Discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == disciplineName.Discipline.DisciplineId);
                disciplineName.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == disciplineName.StudentGroup.StudentGroupId);
                
                context.DisciplineNames.Add(disciplineName);

                context.SaveChanges();
            }
        }

        public void UpdateDisciplineName(DisciplineName disciplineName)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curDisciplineName = context.DisciplineNames.FirstOrDefault(dn => dn.DisciplineNameId == disciplineName.DisciplineNameId);

                if (curDisciplineName != null)
                {
                    curDisciplineName.Name = disciplineName.Name;
                    
                    var disciplineNameDiscipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == disciplineName.Discipline.DisciplineId);
                    curDisciplineName.Discipline = disciplineNameDiscipline;

                    var disciplineNameGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == disciplineName.StudentGroup.StudentGroupId);
                    curDisciplineName.StudentGroup = disciplineNameGroup;
                }

                context.SaveChanges();
            }
        }

        public void RemoveDisciplineName(int disciplineNameId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var disciplineName = context.DisciplineNames.FirstOrDefault(dn => dn.DisciplineNameId == disciplineNameId);

                context.DisciplineNames.Remove(disciplineName);
                context.SaveChanges();
            }
        }

        public void AddDisciplineNameRange(IEnumerable<DisciplineName> disciplineNameList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var disciplineName in disciplineNameList)
                {
                    var disciplineNameDiscipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == disciplineName.Discipline.DisciplineId);
                    disciplineName.Discipline = disciplineNameDiscipline;

                    var disciplineNameGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == disciplineName.StudentGroup.StudentGroupId);
                    disciplineName.StudentGroup = disciplineNameGroup;

                    context.DisciplineNames.Add(disciplineName);
                }

                context.SaveChanges();
            }
        }
    }
}
