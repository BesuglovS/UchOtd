namespace Schedule.DomainClasses.Main
{
    public class Auditorium
    {
        public Auditorium()
        {
        }

        public Auditorium(string name, Building building)
        {
            Name = name;
            Building = building;
        }

        public int AuditoriumId { get; set; }
        public string Name { get; set; }
        public virtual Building Building { get; set; }
    }
}
