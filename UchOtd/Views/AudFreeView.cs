using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchOtd.Views
{
    public class AudFreeView
    {
        public int AuditoriumId { get; set; }
        public string Name { get; set; }
        public Boolean Free { get; set; }
        public string FancyName => Free ? Name : "!ЗАНЯТО! - " + Name;

        public AudFreeView(int auditoriumId, string name, bool free)
        {
            AuditoriumId = auditoriumId;
            Name = name;
            Free = free;
        }
    }
}
