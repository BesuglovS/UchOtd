namespace Schedule.DomainClasses.Main
{
    public class Lesson
    {
        public Lesson()
        {
        }

        public Lesson(TeacherForDiscipline teacherForDiscipline, Calendar calendar,
                      Ring ring, Auditorium auditorium)
        {
            TeacherForDiscipline = teacherForDiscipline;
            Calendar = calendar;
            Ring = ring;
            Auditorium = auditorium;
        }

        public int LessonId { get; set; }
        public bool IsActive { get; set; }
        public int State { get; set; }
        public virtual TeacherForDiscipline TeacherForDiscipline { get; set; }
        public virtual Calendar Calendar { get; set; }
        public virtual Ring Ring { get; set; }
        public virtual Auditorium Auditorium { get; set; }
    }
}
