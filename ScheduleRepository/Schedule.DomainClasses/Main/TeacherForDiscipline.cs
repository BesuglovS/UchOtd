namespace Schedule.DomainClasses.Main
{
    public class TeacherForDiscipline
    {
        public TeacherForDiscipline()
        {
        }

        public TeacherForDiscipline(Teacher teacher, Discipline discipline)
        {
            Teacher = teacher;
            Discipline = discipline;
        }

        public int TeacherForDisciplineId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Discipline Discipline { get; set; }
    }
}
