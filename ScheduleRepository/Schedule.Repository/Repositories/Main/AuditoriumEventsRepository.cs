using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;
using System.Data.Entity;

namespace Schedule.Repositories.Repositories.Main
{
    public class AuditoriumEventsRepository: BaseRepository<AuditoriumEvent>
    {
        public List<AuditoriumEvent> GetAllAuditoriumEvents()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.AuditoriumEvents
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium.Building)
                    .ToList();
            }
        }

        public List<AuditoriumEvent> GetFiltredAuditoriumEvents(Func<AuditoriumEvent, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.AuditoriumEvents
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium.Building)
                    .ToList().Where(condition).ToList();
            }
        }

        public AuditoriumEvent GetFirstFiltredAuditoriumEvent(Func<AuditoriumEvent, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.AuditoriumEvents
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium.Building)
                    .ToList().FirstOrDefault(condition);
            }
        }

        public AuditoriumEvent GetAuditoriumEvent(int auditoriumEventId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.AuditoriumEvents
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium.Building)
                    .FirstOrDefault(ae => ae.AuditoriumEventId == auditoriumEventId);
            }
        }

        public void AddAuditoriumEvent(AuditoriumEvent ae)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                ae.AuditoriumEventId = 0;

                ae.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == ae.Calendar.CalendarId);
                ae.Ring = context.Rings.FirstOrDefault(r => r.RingId == ae.Ring.RingId);
                ae.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == ae.Auditorium.AuditoriumId);

                context.AuditoriumEvents.Add(ae);
                context.SaveChanges();
            }
        }

        public void UpdateAuditoriumEvent(AuditoriumEvent ae)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curAe = context.AuditoriumEvents.FirstOrDefault(evt => evt.AuditoriumEventId == ae.AuditoriumEventId);

                if (curAe != null)
                {
                    curAe.Name = ae.Name;
                    curAe.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == ae.Calendar.CalendarId);
                    curAe.Ring = context.Rings.FirstOrDefault(r => r.RingId == ae.Ring.RingId);
                    curAe.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == ae.Auditorium.AuditoriumId);
                }

                context.SaveChanges();
            }
        }

        public void RemoveAuditoriumEvent(int auditoriumEventId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var aud = context.AuditoriumEvents.FirstOrDefault(evt => evt.AuditoriumEventId == auditoriumEventId);

                context.AuditoriumEvents.Remove(aud);
                context.SaveChanges();
            }
        }

        public void AddAuditoriumEventRange(IEnumerable<AuditoriumEvent> aeList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var ae in aeList)
                {
                    ae.AuditoriumEventId = 0;

                    ae.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == ae.Calendar.CalendarId);
                    ae.Ring = context.Rings.FirstOrDefault(r => r.RingId == ae.Ring.RingId);
                    ae.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == ae.Auditorium.AuditoriumId);

                    context.AuditoriumEvents.Add(ae);
                }

                context.SaveChanges();
            }
        }
    }
}
