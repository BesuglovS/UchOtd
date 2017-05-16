namespace UchOtd.Schedule.MainImport
{
    public class Building
    {
        public Building()
        {
        }

        public Building(string name)
        {
            Name = name;
        }

        public int BuildingId { get; set; }
        public string Name { get; set; }
    }
}
