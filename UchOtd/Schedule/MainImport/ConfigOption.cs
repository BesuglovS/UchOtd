namespace UchOtd.Schedule.MainImport
{
    public class ConfigOption
    {
        public int ConfigOptionId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public ConfigOption()
        {
        }

        public ConfigOption(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
