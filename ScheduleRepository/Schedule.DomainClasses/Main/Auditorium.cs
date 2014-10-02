namespace Schedule.DomainClasses.Main
{
    public class Auditorium
    {
        public Auditorium()
        {
        }

        public Auditorium(string name)
        {
            Name = name;
        }

        public int AuditoriumId { get; set; }
        public string Name { get; set; }
    }
}
