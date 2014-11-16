using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DataLayer;
using Schedule.DomainClasses.Analyse;

namespace Schedule.Repositories.Repositories.Analyse
{
    public class ShiftsRepository:BaseRepository<Shift>
    {
        public List<Shift> GetAllShifts()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Shifts.ToList();
            }
        }

        public List<Shift> GetFiltredShifts(Func<Shift, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Shifts.ToList().Where(condition).ToList();
            }
        }

        public Shift GetFirstFiltredShift(Func<Shift, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Shifts.ToList().FirstOrDefault(condition);
            }
        }

        public Shift GetShift(int shiftId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Shifts.FirstOrDefault(s => s.ShiftId == shiftId);
            }
        }

        public Shift FindShift(string name)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Shifts.FirstOrDefault(s => s.Name == name);
            }
        }

        public void AddShift(Shift shift)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                shift.ShiftId = 0;

                context.Shifts.Add(shift);
                context.SaveChanges();
            }
        }

        public void UpdateShift(Shift shift)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curShift = context.Shifts.FirstOrDefault(s => s.ShiftId == shift.ShiftId);

                if (curShift != null)
                {
                    curShift.Name = shift.Name;
                }

                context.SaveChanges();
            }
        }

        public void RemoveShift(int shiftId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var shift = context.Shifts.FirstOrDefault(s => s.ShiftId == shiftId);

                context.Shifts.Remove(shift);
                context.SaveChanges();
            }
        }

        public void AddShiftRange(IEnumerable<Shift> shiftList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var shift in shiftList)
                {
                    shift.ShiftId = 0;
                    context.Shifts.Add(shift);
                }

                context.SaveChanges();
            }
        }
    }
}
