using Schedule.DomainClasses.Session;
using Schedule.Repositories;
using System;
using System.Collections.Generic;

namespace UchOtd.Views
{
    public class ExamView
    {
        // Common
        public int ExamId { get; set; }
        public string GroupName { get; set; }
        public string DisciplineName { get; set; }
        
        // Cons
        public DateTime ConsultationDateTime { get; set; }
        public string ConsultationAuditorium { get; set; }

        // Exam
        public DateTime ExamDateTime { get; set; }
        public string ExamAuditorium { get; set; }

        public static List<ExamView> FromExamList(ScheduleRepository _repo, List<Exam> list)
        {
            var result = new List<ExamView>();

            foreach (var exam in list)
            {
                var disc = _repo.GetFirstFiltredDisciplines(d => d.DisciplineId == exam.DisciplineId);


                string consAud;
                if (exam.ConsultationAuditoriumId != 0)
                {
                    consAud = _repo.GetAuditorium(exam.ConsultationAuditoriumId).Name;
                }
                else
                {
                    consAud = "";
                }

                var examAud = (exam.ExamAuditoriumId != 0) ? _repo.GetAuditorium(exam.ExamAuditoriumId).Name : "";

                result.Add(new ExamView
                {
                    ExamId = exam.ExamId,
                    ConsultationAuditorium = consAud,
                    ConsultationDateTime = exam.ConsultationDateTime,
                    DisciplineName = disc.Name,
                    ExamAuditorium = examAud,
                    ExamDateTime = exam.ExamDateTime,
                    GroupName = disc.StudentGroup.Name
                });
            }

            return result;
        }
    }
}
