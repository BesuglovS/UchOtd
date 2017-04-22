namespace Schedule.DomainClasses.Main
{
    public class StudentGroup
    {
        public StudentGroup()
        {
        }

        public StudentGroup(string name, Semester semester)
        {
            Name = name;
            Semester = semester;
        }

        public int StudentGroupId { get; set; }
        public string Name { get; set; }
        public virtual Semester Semester { get; set; }
    }
}
