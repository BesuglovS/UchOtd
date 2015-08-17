using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DomainClasses.Main
{
    public class DisciplineName
    {
        public int DisciplineNameId { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
        public string Name { get; set; }
        
        public DisciplineName()
        {
        }

        public DisciplineName(int disciplineNameId, Discipline discipline, StudentGroup studentGroup, string name)
        {
            DisciplineNameId = disciplineNameId;
            Discipline = discipline;
            StudentGroup = studentGroup;
            Name = name;
        }
    }
}
