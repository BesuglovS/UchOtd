using System;

namespace Schedule.DomainClasses.Session
{
    public class Exam
    {
        // Common
        public int ExamId { get; set; }
        public int DisciplineId { get; set; }

        public bool IsActive { get; set; }

        // Cons
        public DateTime ConsultationDateTime { get; set; }
        public int ConsultationAuditoriumId { get; set; }

        // Exam
        public DateTime ExamDateTime { get; set; }
        public int ExamAuditoriumId { get; set; }
    }
}

