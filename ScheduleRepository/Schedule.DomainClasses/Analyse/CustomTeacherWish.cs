using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.DomainClasses.Analyse
{
    public class CustomTeacherWish
    {
        public int CustomTeacherWishId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public CustomTeacherWish()
        {
        }

        public CustomTeacherWish(Teacher teacher, string key, string value)
        {
            Teacher = teacher;
            Key = key;
            Value = value;
        }
    }
}
