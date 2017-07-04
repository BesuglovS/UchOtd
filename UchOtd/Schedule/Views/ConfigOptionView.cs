using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DomainClasses.Config;

namespace UchOtd.Schedule.Views
{
    public class ConfigOptionView
    {
        public int ConfigOptionId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string SemesterDisplayName { get; set; }

        public ConfigOptionView()
        {
        }

        public ConfigOptionView(ConfigOption option)
        {
            ConfigOptionId = option.ConfigOptionId;
            Key = option.Key;
            Value = option.Value;
            SemesterDisplayName = option.Semester.DisplayName;
        }

        public static List<ConfigOptionView> ListToView(List<ConfigOption> list)
        {
            return list.Select(co => new ConfigOptionView(co)).ToList();
        }
    }
}
