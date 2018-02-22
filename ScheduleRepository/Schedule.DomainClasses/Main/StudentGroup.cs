namespace Schedule.DomainClasses.Main
{
    public class StudentGroup
    {
        public StudentGroup()
        {
        }

        public StudentGroup(string name)
        {
            Name = name;
        }

        public int StudentGroupId { get; set; }
        public string Name { get; set; }
    }
}
