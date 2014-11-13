﻿using System.Linq;
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

        public static List<ExamView> FromExamList(ScheduleRepository repo, List<Exam> list)
        {
            return (from exam in list
                let disc = repo.GetFirstFiltredDisciplines(d => d.DisciplineId == exam.DisciplineId)
                let consAud = exam.ConsultationAuditoriumId != 0 ? repo.GetAuditorium(exam.ConsultationAuditoriumId).Name : ""
                let examAud = (exam.ExamAuditoriumId != 0) ? repo.GetAuditorium(exam.ExamAuditoriumId).Name : ""
                select new ExamView
                {
                    ExamId = exam.ExamId, ConsultationAuditorium = consAud, ConsultationDateTime = exam.ConsultationDateTime, DisciplineName = disc.Name, ExamAuditorium = examAud, ExamDateTime = exam.ExamDateTime, GroupName = disc.StudentGroup.Name
                }).ToList();
        }
    }
}
