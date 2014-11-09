using System;

namespace Schedule.DomainClasses.Main
{
    public class Calendar
    {
        public static int Normal = 0;
        public static int Holyday = 1;

        public Calendar()
        {
        }

        public Calendar(DateTime date, int state = 0)
        {
            Date = date;
            State = state;
        }

        public int CalendarId { get; set; }
        public DateTime Date { get; set; }
        public int State { get; set; }
    }
}
