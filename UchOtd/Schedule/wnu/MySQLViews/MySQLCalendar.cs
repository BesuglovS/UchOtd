using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    public class MySqlCalendar
    {
        public int CalendarId { get; set; }
        public string Date { get; set; }

        public MySqlCalendar(Calendar calendar)
        {
            CalendarId = calendar.CalendarId;
            Date = calendar.Date.ToString("yyyy-MM-dd");
        }

        public static List<MySqlCalendar> FromCalendarList(IEnumerable<Calendar> list)
        {
            return list.Select(calendar => new MySqlCalendar(calendar)).ToList();
        }
    }
}
