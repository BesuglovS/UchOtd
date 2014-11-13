using System.Linq;
using Schedule.Constants;
using Schedule.DomainClasses.Analyse;
using Schedule.Repositories;
using System.Collections.Generic;

namespace UchOtd.Schedule.Views
{
    class IncompatiblePairView
    {
        public int CdaId { get; set; }
        public string Disc1 { get; set; }
        public string Disc2 { get; set; }

        public IncompatiblePairView()
        {
        }

        public IncompatiblePairView(ScheduleRepository repo, CustomDisciplineAttribute cda)
        {
            CdaId = cda.CustomDisciplineAttributeId;
            
            
            const string separator = " @ ";

            var disciplineSummary = "";
            disciplineSummary += cda.Discipline.StudentGroup.Name + separator;
            disciplineSummary += cda.Discipline.Name + separator;
            disciplineSummary += (Constants.Attestation.ContainsKey(cda.Discipline.Attestation) ? Constants.Attestation[cda.Discipline.Attestation] : "") + separator;
            disciplineSummary += cda.Discipline.AuditoriumHours;

            Disc1 = disciplineSummary;

            int discId;
            int.TryParse(cda.Value, out discId);

            var disc2 = repo.GetDiscipline(discId);

            if (disc2 != null)
            {
                disciplineSummary = "";
                disciplineSummary += disc2.StudentGroup.Name + separator;
                disciplineSummary += disc2.Name + separator;
                disciplineSummary += (Constants.Attestation.ContainsKey(disc2.Attestation) ? Constants.Attestation[disc2.Attestation] : "") + separator;
                disciplineSummary += disc2.AuditoriumHours;

                Disc2 = disciplineSummary;
            }
            else
            {
                Disc2 = "";
            }
        }

        public static List<IncompatiblePairView> FromCdaList(ScheduleRepository repo, List<CustomDisciplineAttribute> cdaList)
        {
            return cdaList.Select(cda => new IncompatiblePairView(repo, cda)).ToList();
        }
    }
}
