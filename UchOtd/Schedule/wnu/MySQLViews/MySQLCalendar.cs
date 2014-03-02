using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace Schedule.wnu.MySQLViews
{
    public class MySQLCalendar
    {
        public int CalendarId { get; set; }
        public string Date { get; set; }

        public MySQLCalendar(Calendar calendar)
        {
            CalendarId = calendar.CalendarId;
            Date = calendar.Date.ToString("yyyy-MM-dd");
        }

        public static List<MySQLCalendar> FromCalendarList(IEnumerable<Calendar> list)
        {
            return list.Select(calendar => new MySQLCalendar(calendar)).ToList();
        }
    }
}
