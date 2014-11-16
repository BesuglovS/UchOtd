using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class AuditoriumsRepository : BaseRepository<Auditorium>
    {
        public List<Auditorium> GetAllAuditoriums()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Auditoriums.Include(a => a.Building).ToList();
            }
        }

        public List<Auditorium> GetFiltredAuditoriums(Func<Auditorium, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Auditoriums.Include(a => a.Building).ToList().Where(condition).ToList();
            }
        }

        public Auditorium GetFirstFiltredAuditoriums(Func<Auditorium, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Auditoriums.Include(a => a.Building).ToList().FirstOrDefault(condition);
            }
        }

        public Auditorium GetAuditorium(int auditoriumId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Auditoriums.Include(a => a.Building).FirstOrDefault(a => a.AuditoriumId == auditoriumId);
            }
        }

        public Auditorium FindAuditorium(string name)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Auditoriums.Include(a => a.Building).FirstOrDefault(a => a.Name == name);
            }
        }

        public void AddAuditorium(Auditorium aud)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                aud.AuditoriumId = 0;

                aud.Building = context.Buildings.FirstOrDefault(b => b.BuildingId == aud.Building.BuildingId);

                context.Auditoriums.Add(aud);
                context.SaveChanges();
            }
        }

        public void UpdateAuditorium(Auditorium aud)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curAud = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == aud.AuditoriumId);

                if (curAud != null)
                {
                    curAud.Name = aud.Name;
                    curAud.Building = context.Buildings.FirstOrDefault(b => b.BuildingId == aud.Building.BuildingId);
                }

                context.SaveChanges();
            }
        }

        public void RemoveAuditorium(int auditoriumId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var aud = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == auditoriumId);

                context.Auditoriums.Remove(aud);
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
