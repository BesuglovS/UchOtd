using Schedule.Constants;
using Schedule.DomainClasses.Analyse;
using Schedule.Repositories;
using System.Collections.Generic;

namespace UchOtd.Schedule.Views
{
    class IncompatiblePairView
    {
        public int cdaId { get; set; }
        public string Disc1 { get; set; }
        public string Disc2 { get; set; }

        public IncompatiblePairView()
        {
        }

        public IncompatiblePairView(ScheduleRepository repo, CustomDisciplineAttribute cda)
        {
            cdaId = cda.CustomDisciplineAttributeId;
            
            
            var separator = " @ ";

            var DisciplineSummary = "";
            DisciplineSummary += cda.Discipline.StudentGroup.Name + separator;
            DisciplineSummary += cda.Discipline.Name + separator;
            DisciplineSummary += (Constants.Attestation.ContainsKey(cda.Discipline.Attestation) ? Constants.Attestation[cda.Discipline.Attestation] : "") + separator;
            DisciplineSummary += cda.Discipline.AuditoriumHours;

            Disc1 = DisciplineSummary;

            int discId = -1;
            int.TryParse(cda.Value, out discId);

            var disc2 = repo.GetDiscipline(discId);

            if (disc2 != null)
            {
                DisciplineSummary = "";
                DisciplineSummary += disc2.StudentGroup.Name + separator;
                DisciplineSummary += disc2.Name + separator;
                DisciplineSummary += (Constants.Attestation.ContainsKey(disc2.Attestation) ? Constants.Attestation[disc2.Attestation] : "") + separator;
                DisciplineSummary += disc2.AuditoriumHours;

                Disc2 = DisciplineSummary;
            }
            else
            {
                Disc2 = "";
            }
        }

        public static List<IncompatiblePairView> FromCDAList(ScheduleRepository repo, List<CustomDisciplineAttribute> cdaList)
        {
            var result = new List<IncompatiblePairView>();

            foreach (var cda in cdaList)
            {
                result.Add(new IncompatiblePairView(repo, cda));
            }

            return result;
        }
    }
}
