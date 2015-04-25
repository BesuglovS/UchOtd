using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Analyse
{
    public class TeacherWishesRepository:BaseRepository<TeacherWish>
    {
        public List<TeacherWish> GetAllTeacherWishes()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherWishes.Include(w => w.Teacher).Include(w => w.Calendar).Include(w => w.Ring).ToList();
            }
        }

        public List<TeacherWish> GetFiltredTeacherWishes(Func<TeacherWish, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherWishes.Include(w => w.Teacher).Include(w => w.Calendar).Include(w => w.Ring).ToList().Where(condition).ToList();
            }
        }

        public TeacherWish GetFirstFiltredTeacherWish(Func<TeacherWish, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherWishes.Include(w => w.Teacher).Include(w => w.Calendar).Include(w => w.Ring).ToList().FirstOrDefault(condition);
            }
        }

        public TeacherWish GetTeacherWish(int teacherWishId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherWishes.Include(w => w.Teacher).Include(w => w.Calendar).Include(w => w.Ring).FirstOrDefault(w => w.TeacherWishId == teacherWishId);
            }
        }

        public void AddTeacherWish(TeacherWish wish)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                wish.TeacherWishId = 0;

                wish.Teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == wish.Teacher.TeacherId);
                wish.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == wish.Calendar.CalendarId);
                wish.Ring = context.Rings.FirstOrDefault(r => r.RingId == wish.Ring.RingId);

                context.TeacherWishes.Add(wish);
                context.SaveChanges();
            }
        }

        public void UpdateTeacherWish(TeacherWish wish)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curWish = context.TeacherWishes.FirstOrDefault(w => w.TeacherWishId == wish.TeacherWishId);

                if (curWish != null)
                {
                    curWish.Teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == wish.Teacher.TeacherId);
                    curWish.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == wish.Calendar.CalendarId);
                    curWish.Ring = context.Rings.FirstOrDefault(r => r.RingId == wish.Ring.RingId);

                    curWish.Wish = wish.Wish;
                }

                context.SaveChanges();
            }
        }

        public void RemoveTeacherWish(int teacherWishId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var wish = context.TeacherWishes.FirstOrDefault(w => w.TeacherWishId == teacherWishId);

                context.TeacherWishes.Remove(wish);
                context.SaveChanges();
            }
        }

        public void AddTeacherWishRange(IEnumerable<TeacherWish> teacherWishList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var wish in teacherWishList)
                {
                    wish.TeacherWishId = 0;

                    wish.Teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == wish.Teacher.TeacherId);
                    wish.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == wish.Calendar.CalendarId);
                    wish.Ring = context.Rings.FirstOrDefault(r => r.RingId == wish.Ring.RingId);

                    context.TeacherWishes.Add(wish);
                }

                context.SaveChanges();
            }
        }

        public TeacherWish FindTeacherWish(Teacher teacher, Calendar calendar, Ring ring)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.TeacherWishes
                    .Include(w => w.Teacher).Include(w => w.Calendar).Include(w => w.Ring)
                    .FirstOrDefault(w =>
                    w.Teacher.TeacherId == teacher.TeacherId &&
                    w.Calendar.CalendarId == calendar.CalendarId &&
                    w.Ring.RingId == ring.RingId);
            }
        }

        public void AddOrUpdateTeacherWish(TeacherWish wish)
        {
            TeacherWish targetWish = FindTeacherWish(wish.Teacher, wish.Calendar, wish.Ring);

            if (targetWish == null)
            {
                AddTeacherWish(wish);
            }
            else
            {
                if (targetWish.Wish != wish.Wish)
                {
                    targetWish.Wish = wish.Wish;

                    UpdateTeacherWish(targetWish);
                }
            }
        }
    }
}
