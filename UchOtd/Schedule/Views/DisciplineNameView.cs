using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.Constants;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Views
{
    class DisciplineNameView
    {
        public int DisciplineNameId { get; set; }
        public int DisciplineId { get; set; }
        public int StudentGroupId { get; set; }
        public string Name { get; set; }
        public string DisciplineSummary { get; set; }
        public string StudentGroupName { get; set; }

        public DisciplineNameView()
        {
        }

        public DisciplineNameView(DisciplineName disciplineName)
        {
            DisciplineNameId = disciplineName.DisciplineNameId;
            DisciplineId = disciplineName.Discipline.DisciplineId;
            StudentGroupId = disciplineName.StudentGroup.StudentGroupId;
            Name = disciplineName.Name;

            var discSummary = "";
            var separator = " @ ";
            discSummary += disciplineName.Discipline.StudentGroup.Name + separator;
            discSummary += disciplineName.Discipline.Name + separator;
            discSummary += (Constants.Attestation.ContainsKey(disciplineName.Discipline.Attestation) ? Constants.Attestation[disciplineName.Discipline.Attestation] : "") + separator;
            discSummary += disciplineName.Discipline.AuditoriumHours;
            DisciplineSummary = discSummary;

            StudentGroupName = disciplineName.StudentGroup.Name;
        }

        public static List<DisciplineNameView> DisciplineNamesToView(List<DisciplineName> list)
        {
            return list
                .Select(disc => new DisciplineNameView(disc))
                .OrderBy(d => d.DisciplineSummary.Split('@')[0])
                .ThenBy(d => d.DisciplineSummary.Split('@')[1])
                .ToList();
        }
    }
}
