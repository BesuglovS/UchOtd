using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    class MySqlTeacherForDiscipline
    {
        public int TeacherForDisciplineId { get; set; }
        public int TeacherId { get; set; }
        public int DisciplineId { get; set; }

        public MySqlTeacherForDiscipline(TeacherForDiscipline tfd)
        {
            TeacherForDisciplineId = tfd.TeacherForDisciplineId;
            TeacherId = tfd.Teacher.TeacherId;
            DisciplineId = tfd.Discipline.DisciplineId;
        }

        public static List<MySqlTeacherForDiscipline> FromTeacherForDisciplineList(IEnumerable<TeacherForDiscipline> list)
        {
            return list.Select(tfd => new MySqlTeacherForDiscipline(tfd)).ToList();
        }
    }
}
