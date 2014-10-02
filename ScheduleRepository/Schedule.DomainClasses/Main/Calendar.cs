using System;

namespace Schedule.DomainClasses.Main
{
    public class Calendar
    {
        public Calendar()
        {
        }

        public Calendar(DateTime date)
        {
            Date = date;
        }

        public int CalendarId { get; set; }
        public DateTime Date { get; set; } 
    }
}
