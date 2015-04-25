using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Analyse
{
    public class CustomTeacherAttributesRepository:BaseRepository<CustomTeacherAttribute>
    {
        public List<CustomTeacherAttribute> GetAllCustomTeacherAttributes()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomTeacherAttributes.Include(w => w.Teacher).ToList();
            }
        }

        public List<CustomTeacherAttribute> GetFiltredCustomTeacherAttributes(Func<CustomTeacherAttribute, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomTeacherAttributes.Include(w => w.Teacher).ToList().Where(condition).ToList();
            }
        }

        public CustomTeacherAttribute GetFirstFiltredCustomTeacherAttribute(Func<CustomTeacherAttribute, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomTeacherAttributes.Include(w => w.Teacher).ToList().FirstOrDefault(condition);
            }
        }

        public CustomTeacherAttribute GetCustomTeacherAttribute(int teacherWishId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomTeacherAttributes.Include(w => w.Teacher).FirstOrDefault(w => w.CustomTeacherAttributeId == teacherWishId);
            }
        }

        public CustomTeacherAttribute GetCustomTeacherAttribute(Teacher teacher, string key)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.CustomTeacherAttributes.Include(w => w.Teacher).FirstOrDefault(w => w.Teacher.TeacherId == teacher.TeacherId && w.Key == key);
            }
        }

        public void AddCustomTeacherAttribute(CustomTeacherAttribute wish)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                wish.CustomTeacherAttributeId = 0;

                wish.Teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == wish.Teacher.TeacherId);

                context.CustomTeacherAttributes.Add(wish);
                context.SaveChanges();
            }
        }

        public void UpdateCustomTeacherAttribute(CustomTeacherAttribute wish)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curWish = context.CustomTeacherAttributes.FirstOrDefault(w => w.CustomTeacherAttributeId == wish.CustomTeacherAttributeId);

                if (curWish != null)
                {
                    curWish.Teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == wish.Teacher.TeacherId);

                    curWish.Key = wish.Key;
                    curWish.Value = wish.Value;
                }

                context.SaveChanges();
            }
        }

        public void RemoveCustomTeacherAttribute(int customTeacherWishId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var wish = context.CustomTeacherAttributes.FirstOrDefault(w => w.CustomTeacherAttributeId == customTeacherWishId);

                context.CustomTeacherAttributes.Remove(wish);
                context.SaveChanges();
            }
        }

        public void AddCustomTeacherAttributeRange(IEnumerable<CustomTeacherAttribute> teacherWishList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var wish in teacherWishList)
                {
                    wish.CustomTeacherAttributeId = 0;

                    wish.Teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == wish.Teacher.TeacherId);

                    context.CustomTeacherAttributes.Add(wish);
                }

                context.SaveChanges();
            }
        }

        public void AddOrUpdateCustomTeacherAttribute(CustomTeacherAttribute wish)
        {
            CustomTeacherAttribute targetWish = GetCustomTeacherAttribute(wish.Teacher, wish.Key);

            if (targetWish == null)
            {
                AddCustomTeacherAttribute(wish);
            }
            else
            {
                if (targetWish.Value != wish.Value)
                {
                    targetWish.Value = wish.Value;

                    UpdateCustomTeacherAttribute(targetWish);
                }
            }
        }
    }
}
