using Schedule.Constants;
using Schedule.DomainClasses.Main;
using System.Collections.Generic;

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
            string separator = " @ ";
            
            DisciplineId = discipline.DisciplineId;

            DisciplineSummary = "";
            DisciplineSummary += discipline.StudentGroup.Name + separator;
            DisciplineSummary += discipline.Name + separator;
            DisciplineSummary += (Constants.Attestation.ContainsKey(discipline.Attestation) ? Constants.Attestation[discipline.Attestation] : "") + separator;
            DisciplineSummary += discipline.AuditoriumHours;                        
        }

        public static List<DisciplineTextView> DisciplinesToView(List<Discipline> list)
        {
            var result = new List<DisciplineTextView>();

            foreach (var disc in list)
            {
                result.Add(new DisciplineTextView(disc));
            }

            return result;
        }     
           
    }
}
