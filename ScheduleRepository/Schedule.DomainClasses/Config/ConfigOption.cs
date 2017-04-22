using Schedule.DomainClasses.Main;

namespace Schedule.DomainClasses.Config
{
    public class ConfigOption
    {
        public int ConfigOptionId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public virtual Semester Semester { get; set; }

        public ConfigOption()
        {
        }

        public ConfigOption(string key, string value, Semester semester)
        {
            Key = key;
            Value = value;
            Semester = semester;
        }
    }
}
