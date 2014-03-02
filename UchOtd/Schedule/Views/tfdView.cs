using Schedule.DomainClasses.Main;
using System.Collections.Generic;

namespace Schedule.Views
{
    public class tfdView
    {
        public tfdView()
        {
        }

        public tfdView(TeacherForDiscipline tfd)
        {
            TeacherForDisciplineId = tfd.TeacherForDisciplineId;
            tfdSummary = tfd.Teacher.FIO + " " + tfd.Discipline.Name + " " +
                tfd.Discipline.AuditoriumHours + "@" +
                tfd.Discipline.LectureHours + "/" + tfd.Discipline.PracticalHours + " " +
                Constants.Constants.Attestation[tfd.Discipline.Attestation] + " " + 
                tfd.Discipline.StudentGroup.Name;
        }

        public static List<tfdView> tfdsToView(List<TeacherForDiscipline> tfdList)
        {
            var result = new List<tfdView>();
            foreach (var tfd in tfdList)
            {
                result.Add(new tfdView(tfd));
            }

            return result;
        }

        public int TeacherForDisciplineId { get; set; }
        public string tfdSummary { get; set; }
    }
}
