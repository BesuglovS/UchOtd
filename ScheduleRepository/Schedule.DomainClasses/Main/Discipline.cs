﻿namespace Schedule.DomainClasses.Main
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public int Attestation { get; set; } // 0 - ничего; 1 - зачёт; 2 - экзамен; 3 - зачёт и экзамен; 4 - зачёт с оценкой
        public int AuditoriumHours { get; set; }
        public int AuditoriumHoursPerWeek { get; set; }
        public int LectureHours { get; set; }
        public int PracticalHours { get; set; }
        public bool CourseProject { get; set; }
        public bool CourseTask { get; set; }
        public bool ControlTask { get; set; }
        public bool Referat { get; set; }
        public bool Essay { get; set; }
        public string TypeSequence { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }
        public virtual Semester Semester { get; set; }

        public Discipline()
        {
        }

        public Discipline(string name, StudentGroup studentGroup,
            int attestation, int auditoriumHours, int lectureHours, int practicalHours, Semester semester)
        {
            Name = name;
            StudentGroup = studentGroup;
            Attestation = attestation;
            AuditoriumHours = auditoriumHours;
            LectureHours = lectureHours;
            PracticalHours = practicalHours;
            Semester = semester;
        }       
    }
}
