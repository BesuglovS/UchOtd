using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.DomainClasses.Analyse
{
    public class CustomDisciplineAttribute
    {
        public int CustomDisciplineAttributeId { get; set; }
        public virtual Discipline Discipline { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public CustomDisciplineAttribute()
        {
        }

        public CustomDisciplineAttribute(Discipline discipline, string key, string value)
        {
            Discipline = discipline;
            Key = key;
            Value = value;
        }
    }
}
