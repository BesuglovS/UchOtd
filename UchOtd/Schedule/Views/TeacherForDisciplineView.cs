using System;
using System.Collections.Generic;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;

namespace UchOtd.Schedule.Views
{
    public class TeacherForDisciplineView
    {
        public int tfdId { get; set; }
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public string GroupName { get; set; }
        public int PlanHours { get; set; }
        public int HoursDone { get; set; }
        public int ScheduleHours { get; set; }
        public int PlannedHours { get; set; }
        public string Attestation { get; set; }

        public TeacherForDisciplineView()
        {
        }

        public TeacherForDisciplineView(TeacherForDiscipline tfd)
        {

        }

        public static List<TeacherForDisciplineView> FromTFDList(List<TeacherForDiscipline> list, ScheduleRepository repo)
        {
            var result = new List<TeacherForDisciplineView>();

            foreach (var tfd in list)
            {
                result.Add(new TeacherForDisciplineView()
                {
                     tfdId = tfd.TeacherForDisciplineId,
                     DisciplineId = tfd.Discipline.DisciplineId,
                     DisciplineName = tfd.Discipline.Name,
                     GroupName = tfd.Discipline.StudentGroup.Name,
                     PlanHours = tfd.Discipline.AuditoriumHours,
                     Attestation = global::Schedule.Constants.Constants.Attestation[tfd.Discipline.Attestation],
                     ScheduleHours = repo.getTFDHours(tfd.TeacherForDisciplineId),

                     HoursDone = repo.GetFiltredLessons(l => 
                         (l.State == 1) &&
                         l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId &&
                         (l.Calendar.Date.Date + l.Ring.Time.TimeOfDay) < DateTime.Now).Count * 2,

                    PlannedHours = repo.GetFiltredLessons(l =>
                        l.State == 2 &&
                        l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId &&
                        (l.Calendar.Date.Date + l.Ring.Time.TimeOfDay) > DateTime.Now).Count * 2                    
                });
            }

            return result;
        }
    }
}
