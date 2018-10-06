using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchOtd.Views
{
    public class tfdView
    {
        public int TeacherForDisciplineId { get; set; }
        public string Summary { get; set; }

        public tfdView(TeacherForDiscipline tfd)
        {
            TeacherForDisciplineId = tfd.TeacherForDisciplineId;
            Summary = tfd.Discipline.Name + " " + tfd.Discipline.StudentGroup.Name + " " + tfd.Teacher.FIO;
        }

        public static List<tfdView> FromTfdList(List<TeacherForDiscipline> list)
        {
            return list.Select(tfd => new tfdView(tfd)).ToList();
        }

    }
}
