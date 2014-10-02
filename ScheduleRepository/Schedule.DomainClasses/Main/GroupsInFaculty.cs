namespace Schedule.DomainClasses.Main
{
    public class GroupsInFaculty
    {
        public GroupsInFaculty()
        {
        }

        public GroupsInFaculty(StudentGroup group, Faculty faculty)
        {
            StudentGroup = group;
            Faculty = faculty;
        }

        public int GroupsInFacultyId { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}
