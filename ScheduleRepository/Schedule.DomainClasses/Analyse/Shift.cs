﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.DomainClasses.Analyse
{
    public class Shift
    {
        public Shift()
        {
        }

        public Shift(string name)
        {
            Name = name;
        }

        public int ShiftId { get; set; }
        public string Name { get; set; }
    }
}
