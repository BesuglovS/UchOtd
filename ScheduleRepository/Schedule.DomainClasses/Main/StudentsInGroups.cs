namespace Schedule.DomainClasses.Main
{
    public class StudentsInGroups
    {
        public StudentsInGroups()
        {
        }

        public StudentsInGroups(Student student, StudentGroup studentGroup)
        {
            Student = student;
            StudentGroup = studentGroup;
        }

        public int StudentsInGroupsId { get; set; }
        public virtual Student Student { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
    }
}
