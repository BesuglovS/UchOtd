using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.DomainClasses.Analyse
{
    public class CustomStudentGroupAttribute
    {
        public int CustomStudentGroupAttributeId { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public CustomStudentGroupAttribute()
        {
        }

        public CustomStudentGroupAttribute(StudentGroup studentGroup, string key, string value)
        {
            StudentGroup = studentGroup;
            Key = key;
            Value = value;
        }
    }
}
