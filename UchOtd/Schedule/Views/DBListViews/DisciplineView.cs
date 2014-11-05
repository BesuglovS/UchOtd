using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Views.DBListViews
{
    class DisciplineView
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public string StudentGroupName { get; set; }
        public string TeacherFIO { get; set; }
        public int ScheduleHours { get; set; }
        public string Attestation { get; set; } // 0 - ничего; 1 - зачёт; 2 - экзамен; 3 - зачёт и экзамен
        public int AuditoriumHours { get; set; }
        public int LectureHours { get; set; }
        public int PracticalHours { get; set; }        

        public DisciplineView()
        {

        }

        public DisciplineView(ScheduleRepository repo, Discipline discipline)
        {
            DisciplineId = discipline.DisciplineId;
            Name = discipline.Name;
            Attestation = Constants.Constants.Attestation.ContainsKey(discipline.Attestation) ? Constants.Constants.Attestation[discipline.Attestation] : "";
            AuditoriumHours = discipline.AuditoriumHours;
            LectureHours = discipline.LectureHours;
            PracticalHours = discipline.PracticalHours;
            StudentGroupName = discipline.StudentGroup.Name;

            var tefd = repo.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discipline.DisciplineId);
            if (tefd != null)
            {
                TeacherFIO = tefd.Teacher.FIO;
                ScheduleHours = repo.getTFDHours(tefd.TeacherForDisciplineId);
            }
            else
            {
                TeacherFIO = "нет";
                ScheduleHours = 0;
            }
        }

        public static List<DisciplineView> DisciplinesToView(ScheduleRepository repo, List<Discipline> list)
        {
            return list.Select(disc => new DisciplineView(repo, disc)).ToList();
        }
    }
}
