using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Analyse
{
    public class ShiftRingsRepository:BaseRepository<ShiftRing>
    {
        public List<ShiftRing> GetAllShiftRings()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.ShiftRings
                    .Include(rs => rs.Ring)
                    .Include(rs => rs.Shift)
                    .ToList();
            }
        }

        public List<ShiftRing> GetFiltredShiftRings(Func<ShiftRing, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.ShiftRings
                    .Include(rs => rs.Ring)
                    .Include(rs => rs.Shift)
                    .ToList().Where(condition).ToList();
            }
        }

        public ShiftRing GetFirstFiltredShiftRing(Func<ShiftRing, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.ShiftRings
                    .Include(rs => rs.Ring)
                    .Include(rs => rs.Shift)
                    .ToList().FirstOrDefault(condition);
            }
        }

        public ShiftRing GetShiftRing(int shiftRingId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.ShiftRings
                    .Include(rs => rs.Ring)
                    .Include(rs => rs.Shift)
                    .FirstOrDefault(rs => rs.ShiftRingId == shiftRingId);
            }
        }

        public List<Ring> GetShiftRings(Shift shift)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.ShiftRings
                    .Include(rs => rs.Ring)
                    .Include(rs => rs.Shift)
                    .Where(rs => rs.Shift.ShiftId == shift.ShiftId)
                    .Select(rs => rs.Ring)
                    .ToList();
            }
        }

        public List<Ring> GetShiftRings(int shiftId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.ShiftRings
                    .Include(rs => rs.Ring)
                    .Include(rs => rs.Shift)
                    .Where(rs => rs.Shift.ShiftId == shiftId)
                    .Select(rs => rs.Ring)
                    .ToList();
            }
        }

        public void AddShiftRing(ShiftRing shiftRing)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                shiftRing.ShiftRingId = 0;

                shiftRing.Ring = context.Rings.FirstOrDefault(r => r.RingId == shiftRing.Ring.RingId);
                shiftRing.Shift = context.Shifts.FirstOrDefault(s => s.ShiftId == shiftRing.Shift.ShiftId);

                context.ShiftRings.Add(shiftRing);
                context.SaveChanges();
            }
        }

        public void UpdateShiftRing(ShiftRing shiftRing)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curShiftRing = context.ShiftRings.FirstOrDefault(rs => rs.ShiftRingId == shiftRing.ShiftRingId);

                if (curShiftRing != null)
                {
                    curShiftRing.Ring = context.Rings.FirstOrDefault(r => r.RingId == shiftRing.Ring.RingId);
                    curShiftRing.Shift = context.Shifts.FirstOrDefault(s => s.ShiftId == shiftRing.Shift.ShiftId);
                }

                context.SaveChanges();
            }
        }

        public void RemoveShiftRing(int shiftRingId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var shiftRing = context.ShiftRings.FirstOrDefault(rs => rs.ShiftRingId == shiftRingId);

                context.ShiftRings.Remove(shiftRing);
                context.SaveChanges();
            }
        }

        public void AddShiftRingRange(IEnumerable<ShiftRing> shiftRingList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var shiftRing in shiftRingList)
                {
                    shiftRing.ShiftRingId = 0;
                    context.ShiftRings.Add(shiftRing);
                }

                context.SaveChanges();
            }
        }
    }
}
