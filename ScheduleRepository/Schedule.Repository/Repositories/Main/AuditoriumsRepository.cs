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
                var audFound = context.Set<Auditorium>().FirstOrDefault(a => a.AuditoriumId == aud.AuditoriumId);
                if (audFound != null)
                {
                    context.Set<Auditorium>().Remove(audFound);
                    context.SaveChanges();
                }
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

        public Auditorium getFreeAud(int calendarId, int ringId, int buildingId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                List<Auditorium> auds;
                if (buildingId == -1)
                {
                    auds = context.Auditoriums.ToList();
                }
                else
                {
                    auds = context.Auditoriums.Where(a => a.Building.BuildingId == buildingId).ToList();
                }

                var lessonAudIds = context.Lessons.Where(l =>
                        l.State == 1 &&
                        l.Calendar.CalendarId == calendarId &&
                        l.Ring.RingId == ringId)
                    .Select(l => l.Auditorium.AuditoriumId)
                    .Distinct();

                auds = auds.Where(a => !lessonAudIds.Contains(a.AuditoriumId)).ToList();

                var audEventsAudIds = context.AuditoriumEvents.Where(evt =>
                        evt.Calendar.CalendarId == calendarId &&
                        evt.Ring.RingId == ringId)
                    .Select(l => l.Auditorium.AuditoriumId)
                    .Distinct();

                auds = auds.Where(a => !audEventsAudIds.Contains(a.AuditoriumId)).ToList();

                if (auds.Count == 0)
                {
                    return null;
                }

                Random rnd = new Random();

                return auds[rnd.Next(0, auds.Count)];
            }
        }

        public bool CheckIfEmpty(Calendar calendar, Ring ring, Auditorium aud)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var lessonCount = context.Lessons.Count(l =>
                    l.State == 1 &&
                    l.Calendar.CalendarId == calendar.CalendarId &&
                    l.Ring.RingId == ring.RingId &&
                    l.Auditorium.AuditoriumId == aud.AuditoriumId);
                var eventCount = context.AuditoriumEvents.Count(ae =>
                    ae.Calendar.CalendarId == calendar.CalendarId &&
                    ae.Ring.RingId == ring.RingId &&
                    ae.Auditorium.AuditoriumId == aud.AuditoriumId);

                if (lessonCount > 0 || eventCount > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
