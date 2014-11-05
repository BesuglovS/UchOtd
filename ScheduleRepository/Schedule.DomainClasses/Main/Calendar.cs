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

        public Calendar(DateTime date, int State = 0)
        {
            Date = date;
        }

        public int CalendarId { get; set; }
        public DateTime Date { get; set; }
        public int State { get; set; }
    }
}
