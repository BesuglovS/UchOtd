﻿using System.Collections.Generic;
using System.Linq;
using Schedule.Constants;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;

namespace UchOtd.Schedule.Views.DBListViews
{
    class DisciplineView
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public string TypeSequence { get; set; }
        public string StudentGroupName { get; set; }
        public string TeacherFio { get; set; }
        public int AuditoriumHoursPerWeek { get; set; }
        public int ScheduleHours { get; set; }
        public string Attestation { get; set; } // 0 - ничего; 1 - зачёт; 2 - экзамен; 3 - зачёт и экзамен
        public int AuditoriumHours { get; set; }
        public int ProposedHours { get; set; }
        public int LectureHours { get; set; }
        public int PracticalHours { get; set; }
        //public string TypeSequence { get; set; }

        public DisciplineView()
        {

        }

        public DisciplineView(ScheduleRepository repo, Discipline discipline, bool hoursCountWeekFiltered, List<int> hoursCountWeekFilter)
        {
            DisciplineId = discipline.DisciplineId;
            Name = discipline.Name;
            Attestation = Constants.Attestation.ContainsKey(discipline.Attestation) ? Constants.Attestation[discipline.Attestation] : "";
            AuditoriumHours = discipline.AuditoriumHours;
            AuditoriumHoursPerWeek = discipline.AuditoriumHoursPerWeek;
            LectureHours = discipline.LectureHours;
            PracticalHours = discipline.PracticalHours;
            StudentGroupName = discipline.StudentGroup.Name;
            TypeSequence = discipline.TypeSequence;

            var tefd = repo.TeacherForDisciplines
                .GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discipline.DisciplineId);
            if (tefd != null)
            {
                TeacherFio = tefd.Teacher.FIO;
                ScheduleHours = repo.CommonFunctions.GetTfdHours(tefd.TeacherForDisciplineId, false, hoursCountWeekFiltered, hoursCountWeekFilter);
                ProposedHours = repo.CommonFunctions.GetTfdProposedHours(tefd.TeacherForDisciplineId);
            }
            else
            {
                TeacherFio = "нет";
                ScheduleHours = 0;
            }
        }

        public static List<DisciplineView> DisciplinesToView(ScheduleRepository repo, List<Discipline> list, bool hoursCountWeekFiltered, List<int> hoursCountWeekFilter)
        {
            return list.Select(disc => new DisciplineView(repo, disc, hoursCountWeekFiltered, hoursCountWeekFilter)).ToList();
        }
    }
}
