using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    class MySqlDiscipline
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public int Attestation { get; set; } // 0 - ничего; 1 - зачёт; 2 - экзамен; 3 - зачёт и экзамен
        public int AuditoriumHours { get; set; }
        public int AuditoriumHoursPerWeek { get; set; }
        public int LectureHours { get; set; }
        public int PracticalHours { get; set; }
        
        public bool CourseProject { get; set; }
        public bool CourseTask { get; set; }
        public bool ControlTask { get; set; }
        public bool Referat { get; set; }
        public bool Essay { get; set; }

        public int StudentGroupId { get; set; }

        public MySqlDiscipline(Discipline discipline)
        {
            DisciplineId = discipline.DisciplineId;
            Name = discipline.Name;
            Attestation = discipline.Attestation;
            AuditoriumHours = discipline.AuditoriumHours;
            AuditoriumHoursPerWeek = discipline.AuditoriumHoursPerWeek;
            LectureHours = discipline.LectureHours;
            PracticalHours = discipline.PracticalHours;
            CourseProject = discipline.CourseProject;
            CourseTask = discipline.CourseTask;
            ControlTask = discipline.ControlTask;
            Referat = discipline.Referat;
            Essay = discipline.Essay;

            StudentGroupId = discipline.StudentGroup.StudentGroupId;
        }

        public static List<MySqlDiscipline> FromDisciplineList(IEnumerable<Discipline> list)
        {
            return list.Select(discipline => new MySqlDiscipline(discipline)).ToList();
        }
    }
}
