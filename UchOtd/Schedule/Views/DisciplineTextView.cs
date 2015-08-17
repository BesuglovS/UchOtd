using System.Collections.Generic;
using System.Linq;
using Schedule.Constants;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Views
{
    class DisciplineTextView
    {
        public int DisciplineId { get; set; }
        public string DisciplineSummary { get; set; }

        public DisciplineTextView()
        {
        }

        public DisciplineTextView(Discipline discipline)
        {
            const string separator = " @ ";
            
            DisciplineId = discipline.DisciplineId;

            DisciplineSummary = "";
            DisciplineSummary += discipline.StudentGroup.Name + separator;
            DisciplineSummary += discipline.Name + separator;
            DisciplineSummary += (Constants.Attestation.ContainsKey(discipline.Attestation) ? Constants.Attestation[discipline.Attestation] : "") + separator;
            DisciplineSummary += discipline.AuditoriumHours;                        
        }

        public static List<DisciplineTextView> DisciplinesToView(List<Discipline> list)
        {
            return list
                .Select(disc => new DisciplineTextView(disc))
                .OrderBy(d => d.DisciplineSummary.Split('@')[0])
                .ThenBy(d => d.DisciplineSummary.Split('@')[1])
                .ToList();
        }
    }
}
