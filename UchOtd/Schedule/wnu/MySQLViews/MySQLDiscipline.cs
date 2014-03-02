using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace Schedule.wnu.MySQLViews
{
    class MySQLDiscipline
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public int Attestation { get; set; } // 0 - ничего; 1 - зачёт; 2 - экзамен; 3 - зачёт и экзамен
        public int AuditoriumHours { get; set; }
        public int LectureHours { get; set; }
        public int PracticalHours { get; set; }
        public int StudentGroupId { get; set; }


        public MySQLDiscipline(Discipline discipline)
        {
            DisciplineId = discipline.DisciplineId;
            Name = discipline.Name;
            Attestation = discipline.Attestation;
            AuditoriumHours = discipline.AuditoriumHours;
            LectureHours = discipline.LectureHours;
            PracticalHours = discipline.PracticalHours;
            StudentGroupId = discipline.StudentGroup.StudentGroupId;
        }

        public static List<MySQLDiscipline> FromDisciplineList(IEnumerable<Discipline> list)
        {
            return list.Select(discipline => new MySQLDiscipline(discipline)).ToList();
        }
    }
}
