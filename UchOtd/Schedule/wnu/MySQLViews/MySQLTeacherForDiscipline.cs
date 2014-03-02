using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace Schedule.wnu.MySQLViews
{
    class MySQLTeacherForDiscipline
    {
        public int TeacherForDisciplineId { get; set; }
        public int TeacherId { get; set; }
        public int DisciplineId { get; set; }

        public MySQLTeacherForDiscipline(TeacherForDiscipline tfd)
        {
            TeacherForDisciplineId = tfd.TeacherForDisciplineId;
            TeacherId = tfd.Teacher.TeacherId;
            DisciplineId = tfd.Discipline.DisciplineId;
        }

        public static List<MySQLTeacherForDiscipline> FromTeacherForDisciplineList(IEnumerable<TeacherForDiscipline> list)
        {
            return list.Select(tfd => new MySQLTeacherForDiscipline(tfd)).ToList();
        }
    }
}
