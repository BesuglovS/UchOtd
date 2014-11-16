using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DataLayer;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Analyse
{
    public class CustomStudentGroupAttributesRepository:BaseRepository<CustomStudentGroupAttribute>
    {
        public List<CustomStudentGroupAttribute> GetAllCustomStudentGroupAttributes()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomStudentGroupAttributes.Include(w => w.StudentGroup).ToList();
            }
        }

        public List<CustomStudentGroupAttribute> GetFiltredCustomStudentGroupAttributes(Func<CustomStudentGroupAttribute, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomStudentGroupAttributes.Include(w => w.StudentGroup).ToList().Where(condition).ToList();
            }
        }

        public CustomStudentGroupAttribute GetFirstFiltredCustomStudentGroupAttribute(Func<CustomStudentGroupAttribute, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomStudentGroupAttributes.Include(w => w.StudentGroup).ToList().FirstOrDefault(condition);
            }
        }

        public CustomStudentGroupAttribute GetCustomStudentGroupAttribute(int customStudentGroupAttributeId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomStudentGroupAttributes.Include(w => w.StudentGroup).FirstOrDefault(w => w.CustomStudentGroupAttributeId == customStudentGroupAttributeId);
            }
        }

        public CustomStudentGroupAttribute GetCustomStudentGroupAttribute(StudentGroup studentGroup, string key)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomStudentGroupAttributes.Include(w => w.StudentGroup)
                    .FirstOrDefault(w => w.StudentGroup.StudentGroupId == studentGroup.StudentGroupId && w.Key == key);
            }
        }

        public void AddCustomStudentGroupAttribute(CustomStudentGroupAttribute attr)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                attr.CustomStudentGroupAttributeId = 0;

                attr.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == attr.StudentGroup.StudentGroupId);

                context.CustomStudentGroupAttributes.Add(attr);
                context.SaveChanges();
            }
        }

        public void UpdateCustomStudentGroupAttribute(CustomStudentGroupAttribute attr)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curAttr = context.CustomStudentGroupAttributes.FirstOrDefault(w => w.CustomStudentGroupAttributeId == attr.CustomStudentGroupAttributeId);

                if (curAttr != null)
                {
                    curAttr.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == attr.StudentGroup.StudentGroupId);

                    curAttr.Key = attr.Key;
                    curAttr.Value = attr.Value;
                }

                context.SaveChanges();
            }
        }

        public void RemoveCustomStudentGroupAttribute(int customStudentGroupAttributeId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var attr = context.CustomStudentGroupAttributes.FirstOrDefault(w => w.CustomStudentGroupAttributeId == customStudentGroupAttributeId);

                context.CustomStudentGroupAttributes.Remove(attr);
                context.SaveChanges();
            }
        }

        public void AddCustomStudentGroupAttributeRange(IEnumerable<CustomStudentGroupAttribute> customStudentGroupAttributeList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var attr in customStudentGroupAttributeList)
                {
                    attr.CustomStudentGroupAttributeId = 0;

                    attr.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == attr.StudentGroup.StudentGroupId);

                    context.CustomStudentGroupAttributes.Add(attr);
                }

                context.SaveChanges();
            }
        }

        public void AddOrUpdateCustomStudentGroupAttribute(CustomStudentGroupAttribute attr)
        {
            CustomStudentGroupAttribute targetAttr = GetCustomStudentGroupAttribute(attr.StudentGroup, attr.Key);

            if (targetAttr == null)
            {
                AddCustomStudentGroupAttribute(attr);
            }
            else
            {
                if (targetAttr.Value != attr.Value)
                {
                    targetAttr.Value = attr.Value;

                    UpdateCustomStudentGroupAttribute(targetAttr);
                }
            }
        }
    }
}
