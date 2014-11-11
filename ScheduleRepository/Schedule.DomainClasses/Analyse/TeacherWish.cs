using Schedule.DomainClasses.Main;

namespace Schedule.DomainClasses.Analyse
{
    public class TeacherWish
    {
        public int TeacherWishId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Calendar Calendar { get; set; }
        public virtual Ring Ring { get; set; }
        public int Wish { get; set; }

        public TeacherWish()
        {
        }

        public TeacherWish(Teacher teacher, Calendar calendar, Ring ring, int wish)
        {
            Teacher = teacher;
            Calendar = calendar;
            Ring = ring;
            Wish = wish;
        }
    }
}
