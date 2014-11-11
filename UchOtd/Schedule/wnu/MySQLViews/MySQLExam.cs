using Schedule.DomainClasses.Session;
using System.Collections.Generic;
using System.Linq;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    public class MySQLExam
    {
        // Common
        public int ExamId { get; set; }
        public int DisciplineId { get; set; }

        public int IsActive { get; set; }

        // Cons
        public string ConsultationDateTime { get; set; }
        public int ConsultationAuditoriumId { get; set; }

        // Exam
        public string ExamDateTime { get; set; }
        public int ExamAuditoriumId { get; set; }

        public static List<MySQLExam> FromExamList(List<Exam> list)
        {
            return list
                .Select(exam => new MySQLExam
                {
                        ExamId = exam.ExamId, 
                        DisciplineId = exam.DisciplineId, 
                        IsActive = exam.IsActive ? 1 : 0, 
                        ConsultationDateTime = exam.ConsultationDateTime.ToString("dd.MM.yyyy H:mm"), 
                        ConsultationAuditoriumId = exam.ConsultationAuditoriumId, 
                        ExamDateTime = exam.ExamDateTime.ToString("dd.MM.yyyy H:mm"), 
                        ExamAuditoriumId = exam.ExamAuditoriumId
                    })
                .ToList();
        }
    }
}
