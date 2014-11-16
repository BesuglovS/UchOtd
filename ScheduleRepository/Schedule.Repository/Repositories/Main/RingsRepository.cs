using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class RingsRepository: BaseRepository<Ring>
    {
        public List<Ring> GetAllRings()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Rings.ToList();
            }
        }

        public List<Ring> GetFiltredRings(Func<Ring, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Rings.ToList().Where(condition).ToList();
            }
        }

        public Ring GetFirstFiltredRing(Func<Ring, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Rings.ToList().FirstOrDefault(condition);
            }
        }

        public Ring GetRing(int ringId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Rings.FirstOrDefault(r => r.RingId == ringId);
            }
        }

        public Ring FindRing(DateTime time)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Rings.FirstOrDefault(r => ((r.Time.Hour == time.Hour) && (r.Time.Minute == time.Minute)));
            }
        }

        public void AddRing(Ring ring)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                ring.RingId = 0;

                context.Rings.Add(ring);
                context.SaveChanges();
            }
        }

        public void UpdateRing(Ring ring)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curRing = context.Rings.FirstOrDefault(r => r.RingId == ring.RingId);

                if (curRing != null)
                {
                    curRing.Time = ring.Time;
                }

                context.SaveChanges();
            }
        }

        public void RemoveRing(int ringId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var ring = context.Rings.FirstOrDefault(r => r.RingId == ringId);

                context.Rings.Remove(ring);
                context.SaveChanges();
            }
        }

        public void AddRingRange(IEnumerable<Ring> ringList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var ring in ringList)
                {
                    ring.RingId = 0;
                    context.Rings.Add(ring);
                }

                context.SaveChanges();
            }
        }
    }
}
