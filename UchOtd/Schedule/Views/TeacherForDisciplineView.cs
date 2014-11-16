using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.Constants;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;

namespace UchOtd.Schedule.Views
{
    public class TeacherForDisciplineView
    {
        public int TfdId { get; set; }
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public string GroupName { get; set; }
        public int PlanHours { get; set; }
        public int HoursDone { get; set; }
        public int ScheduleHours { get; set; }
        public int PlannedHours { get; set; }
        public string Attestation { get; set; }

        public static List<TeacherForDisciplineView> FromTfdList(List<TeacherForDiscipline> list, ScheduleRepository repo)
        {
            return list.Select(tfd => new TeacherForDisciplineView
            {
                TfdId = tfd.TeacherForDisciplineId, 
                DisciplineId = tfd.Discipline.DisciplineId, 
                DisciplineName = tfd.Discipline.Name, 
                GroupName = tfd.Discipline.StudentGroup.Name, 
                PlanHours = tfd.Discipline.AuditoriumHours, 
                Attestation = Constants.Attestation[tfd.Discipline.Attestation],
                ScheduleHours = repo.CommonFunctions.GetTfdHours(tfd.TeacherForDisciplineId),
                HoursDone = repo.Lessons.GetFiltredLessons(l => 
                    (l.State == 1) && 
                    l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId && 
                    (l.Calendar.Date.Date + l.Ring.Time.TimeOfDay) < DateTime.Now).Count*2, 
                PlannedHours = repo.Lessons.GetFiltredLessons(l => 
                    l.State == 2 && 
                    l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId && 
                    (l.Calendar.Date.Date + l.Ring.Time.TimeOfDay) > DateTime.Now).Count*2
            }).ToList();
        }
    }
}
