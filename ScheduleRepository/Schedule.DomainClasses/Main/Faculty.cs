namespace Schedule.DomainClasses.Main
{
    public class Faculty
    {
        public Faculty()
        {
            
        }

        public Faculty(string name, string letter, int sortingOrder,
            string scheduleSigningTitle, string deanSigningSchedule,
            string sessionSigningTitle, string deanSigningSessionSchedule)
        {
            Name = name;
            Letter = letter;
            SortingOrder = sortingOrder;

            ScheduleSigningTitle = scheduleSigningTitle;
            DeanSigningSchedule = deanSigningSchedule;

            SessionSigningTitle = sessionSigningTitle;
            DeanSigningSessionSchedule = deanSigningSessionSchedule;
        }

        public int FacultyId { get; set; }
        public string Name { get; set; }
        public string Letter { get; set; }
        public int SortingOrder { get; set; }

        public string ScheduleSigningTitle { get; set; }
        public string DeanSigningSchedule { get; set; }

        public string SessionSigningTitle { get; set; }
        public string DeanSigningSessionSchedule { get; set; }
    }
}
