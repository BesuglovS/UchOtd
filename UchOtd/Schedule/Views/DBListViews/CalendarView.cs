using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.Constants;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Views.DBListViews
{
    public class CalendarView
    {
        public int CalendarId { get; set; }
        public DateTime Date { get; set; }
        public int State { get; set; }
        public string StateString { get; set; }
        public CalendarView()
        {
        }

        public CalendarView(Calendar calendar)
        {
            CalendarId = calendar.CalendarId;
            Date = calendar.Date;
            State = calendar.State;
            StateString = Constants.CalendarStateDescription[calendar.State];
        }

        public static List<CalendarView> CalendarsToView(List<Calendar> list)
        {
            return list.Select(calendar => new CalendarView(calendar)).ToList();
        }
    }
}
