using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Analyse
{
    public class CustomDisciplineAttributesRepository:BaseRepository<CustomDisciplineAttribute>
    {
        public List<CustomDisciplineAttribute> GetAllCustomDisciplineAttributes()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomDisciplineAttributes.Include(cda => cda.Discipline.StudentGroup).ToList();
            }
        }

        public List<CustomDisciplineAttribute> GetFiltredCustomDisciplineAttributes(Func<CustomDisciplineAttribute, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomDisciplineAttributes.Include(cda => cda.Discipline.StudentGroup).ToList().Where(condition).ToList();
            }
        }

        public CustomDisciplineAttribute GetFirstFiltredCustomDisciplineAttribute(Func<CustomDisciplineAttribute, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomDisciplineAttributes.Include(cda => cda.Discipline.StudentGroup).ToList().FirstOrDefault(condition);
            }
        }

        public CustomDisciplineAttribute GetCustomDisciplineAttribute(int customDisciplineAttributeId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomDisciplineAttributes.Include(cda => cda.Discipline.StudentGroup)
                    .FirstOrDefault(cda => cda.CustomDisciplineAttributeId == customDisciplineAttributeId);
            }
        }

        public CustomDisciplineAttribute GetCustomDisciplineAttribute(Discipline d, string name)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var attr = context.CustomDisciplineAttributes.Include(cda => cda.Discipline.StudentGroup)
                    .FirstOrDefault(cda => cda.Discipline.DisciplineId == d.DisciplineId && cda.Key == name);

                return attr;
            }
        }

        public void AddCustomDisciplineAttribute(CustomDisciplineAttribute customDisciplineAttribute)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                customDisciplineAttribute.CustomDisciplineAttributeId = 0;

                customDisciplineAttribute.Discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == customDisciplineAttribute.Discipline.DisciplineId);

                context.CustomDisciplineAttributes.Add(customDisciplineAttribute);
                context.SaveChanges();
            }
        }

        public void UpdateCustomDisciplineAttribute(CustomDisciplineAttribute customDisciplineAttribute)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curCustomDisciplineAttribute = context.CustomDisciplineAttributes
                    .FirstOrDefault(cda => cda.CustomDisciplineAttributeId == customDisciplineAttribute.CustomDisciplineAttributeId);

                if (curCustomDisciplineAttribute != null)
                {
                    curCustomDisciplineAttribute.Discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == curCustomDisciplineAttribute.Discipline.DisciplineId);

                    curCustomDisciplineAttribute.Key = customDisciplineAttribute.Key;
                    curCustomDisciplineAttribute.Value = customDisciplineAttribute.Value;
                }

                context.SaveChanges();
            }
        }

        public void RemoveCustomDisciplineAttribute(int customDisciplineAttributeId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var customDisciplineAttribute = context.CustomDisciplineAttributes.FirstOrDefault(da => da.CustomDisciplineAttributeId == customDisciplineAttributeId);

                context.CustomDisciplineAttributes.Remove(customDisciplineAttribute);
                context.SaveChanges();
            }
        }

        public void AddCustomDisciplineAttributeRange(IEnumerable<CustomDisciplineAttribute> customDisciplineAttributeList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var сustomDisciplineAttribute in customDisciplineAttributeList)
                {
                    сustomDisciplineAttribute.CustomDisciplineAttributeId = 0;
                    context.CustomDisciplineAttributes.Add(сustomDisciplineAttribute);
                }

                context.SaveChanges();
            }
        }

        public void AddOrUpdateCustomDisciplineAttribute(CustomDisciplineAttribute customDisciplineAttribute)
        {
            CustomDisciplineAttribute targetAttr = GetCustomDisciplineAttribute(customDisciplineAttribute.Discipline, customDisciplineAttribute.Key);

            if (targetAttr == null)
            {
                AddCustomDisciplineAttribute(customDisciplineAttribute);
            }
            else
            {
                if (targetAttr.Value != customDisciplineAttribute.Value)
                {
                    targetAttr.Value = customDisciplineAttribute.Value;

                    UpdateCustomDisciplineAttribute(targetAttr);
                }
            }
        }
    }
}
