using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;
using Calendar = Schedule.DomainClasses.Main.Calendar;

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
                return context.Calendars.ToList().FirstOrDefault(cal => cal.Date.Date == date.Date);
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

        public int GetWeek(Lesson lesson)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var semesterStartsOption = context
                    .Config
                    .FirstOrDefault(co => co.Key == "Semester Starts");
                if (semesterStartsOption == null)
                {
                    return -1;
                }

                var semesterStarts =  DateTime.ParseExact(semesterStartsOption.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var ssDow = (semesterStarts.DayOfWeek != DayOfWeek.Sunday) ? (int)semesterStarts.DayOfWeek : 7;

                var ssWeeksMonday = semesterStarts.AddDays((-1) * (ssDow - 1));

                return (lesson.Calendar.Date - ssWeeksMonday).Days / 7 + 1;
            }
        }

        public List<Calendar> GetWeekCalendars(int week)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var semesterStartsOption = context
                    .Config
                    .FirstOrDefault(co => co.Key == "Semester Starts");
                if (semesterStartsOption == null)
                {
                    return null;
                }

                var semesterStarts = DateTime.ParseExact(semesterStartsOption.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var ssDow = (semesterStarts.DayOfWeek != DayOfWeek.Sunday) ? (int)semesterStarts.DayOfWeek : 7;
                var ssWeeksMonday = semesterStarts.AddDays((-1) * (ssDow - 1));

                var result = new List<Calendar>();
                var calendars = context.Calendars.ToList();

                for (int i = 0; i < calendars.Count; i++)
                {
                    var c = calendars[i];
                    if (((c.Date - ssWeeksMonday).Days / 7 + 1) == week)
                    {
                        result.Add(c);
                    }
                }

                return result;
            }
        }
    }
}
