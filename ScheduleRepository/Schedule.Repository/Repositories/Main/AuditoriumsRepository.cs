using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class AuditoriumsRepository : BaseRepository<Auditorium>
    {
        public override ICollection<Auditorium> GetAll()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Auditoriums.Include(a => a.Building).ToList();
            }
        }

        public override ICollection<Auditorium> FindAll(Expression<Func<Auditorium, bool>> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Auditoriums.Include(a => a.Building).Where(condition).ToList();
            }
        }

        public override Auditorium Find(Expression<Func<Auditorium, bool>> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Auditoriums.Include(a => a.Building).FirstOrDefault(condition);
            }
        }

        public override Auditorium Get(int id)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Auditoriums.Include(a => a.Building).FirstOrDefault(a => a.AuditoriumId == id);
            }
        }

        public Auditorium Find(string name)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Auditoriums.Include(a => a.Building).FirstOrDefault(a => a.Name == name);
            }
        }

        public override Auditorium Add(Auditorium aud)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                aud.AuditoriumId = 0;

                aud.Building = context.Buildings.FirstOrDefault(b => b.BuildingId == aud.Building.BuildingId);

                context.Auditoriums.Add(aud);
                context.SaveChanges();
            }

            return aud;
        }

        public override Auditorium Update(Auditorium aud, int id)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                if (aud == null) return null;

                var curAud = context.Set<Auditorium>().Find(id);

                if (curAud != null)
                {
                    context.Entry(curAud).CurrentValues.SetValues(aud);

                    curAud.Building = context.Buildings.FirstOrDefault(b => b.BuildingId == aud.Building.BuildingId);
                }

                context.SaveChanges();

                return curAud;
            }
        }

        public override void Delete(Auditorium aud)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                /*var aud = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == auditoriumId);

                context.Auditoriums.Remove(aud);*/
                context.Set<Auditorium>().Remove(aud);
                context.SaveChanges();
            }
        }

        public void AddAuditoriumRange(IEnumerable<Auditorium> audList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var aud in audList)
                {
                    aud.AuditoriumId = 0;
                    aud.Building = context.Buildings.FirstOrDefault(b => b.BuildingId == aud.Building.BuildingId);

                    context.Auditoriums.Add(aud);
                }

                context.SaveChanges();
            }
        }
    }
}
