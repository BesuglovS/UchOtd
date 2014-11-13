using System.Collections.Generic;
using System.Linq;
using Schedule.Constants;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Views
{
    public class TfdView
    {
        public TfdView()
        {
        }

        public TfdView(TeacherForDiscipline tfd)
        {
            TeacherForDisciplineId = tfd.TeacherForDisciplineId;
            TfdSummary = tfd.Teacher.FIO + " " + tfd.Discipline.Name + " " +
                tfd.Discipline.AuditoriumHours + "@" +
                tfd.Discipline.LectureHours + "/" + tfd.Discipline.PracticalHours + " " +
                Constants.Attestation[tfd.Discipline.Attestation] + " " + 
                tfd.Discipline.StudentGroup.Name;
        }

        public static List<TfdView> TfdsToView(List<TeacherForDiscipline> tfdList)
        {
            return tfdList.Select(tfd => new TfdView(tfd)).ToList();
        }

        public int TeacherForDisciplineId { get; set; }
        public string TfdSummary { get; set; }
    }
}
