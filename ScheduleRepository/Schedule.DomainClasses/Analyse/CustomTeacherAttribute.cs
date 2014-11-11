using Schedule.DomainClasses.Main;

namespace Schedule.DomainClasses.Analyse
{
    public class CustomTeacherAttribute
    {
        public int CustomTeacherAttributeId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public CustomTeacherAttribute()
        {
        }

        public CustomTeacherAttribute(Teacher teacher, string key, string value)
        {
            Teacher = teacher;
            Key = key;
            Value = value;
        }
    }
}
