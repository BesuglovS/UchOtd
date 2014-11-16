using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class CalendarsRepository : BaseRepository<Calendar>
    {
        public List<Calendar> GetAllCalendars()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Calendars.ToList();
            }
        }

        public List<Calendar> GetFiltredCalendars(Func<Calendar, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Calendars.ToList().Where(condition).ToList();
            }
        }

        public Calendar GetFirstFiltredCalendar(Func<Calendar, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Calendars.ToList().FirstOrDefault(condition);
            }
        }

        public Calendar GetCalendar(int calendarId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Calendars.FirstOrDefault(c => c.CalendarId == calendarId);
            }
        }

        public Calendar FindCalendar(DateTime date)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Calendars.FirstOrDefault(c => c.Date == date);
            }
        }

        public void AddCalendar(Calendar calendar)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                calendar.CalendarId = 0;

                context.Calendars.Add(calendar);
                context.SaveChanges();
            }
        }

        public void UpdateCalendar(Calendar calendar)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curCalendar = context.Calendars.FirstOrDefault(c => c.CalendarId == calendar.CalendarId);

                if (curCalendar != null)
                {
                    curCalendar.Date = calendar.Date;
                    curCalendar.State = calendar.State;
                }

                context.SaveChanges();
            }
        }

        public void RemoveCalendar(int calendarId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == calendarId);

                context.Calendars.Remove(calendar);
                context.SaveChanges();
            }
        }

        public void AddCalendarRange(IEnumerable<Calendar> calendarList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var calendar in calendarList)
                {
                    calendar.CalendarId = 0;
                    context.Calendars.Add(calendar);
                }

                context.SaveChanges();
            }
        }

        public List<Calendar> GetDowCalendars(int dow)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Calendars.ToList()
                    .Where(c => Constants.Constants.DowRemap[(int)c.Date.Date.DayOfWeek] == dow)
                    .ToList();
            }
        }
    }
}
