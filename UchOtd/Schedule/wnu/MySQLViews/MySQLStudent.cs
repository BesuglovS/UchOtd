using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace Schedule.wnu.MySQLViews
{
    public class MySQLStudent
    {
        public int StudentId { get; set; }
        public string F { get; set; }
        public string I { get; set; }
        public string O { get; set; }
        public string ZachNumber { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Orders { get; set; }
        public int Starosta { get; set; }
        public int NFactor { get; set; }
        public int PaidEdu { get; set; }
        public int Expelled { get; set; }

        public MySQLStudent(Student student)
        {
            StudentId = student.StudentId;
            F = student.F;
            I = student.I;
            O = student.O;
            ZachNumber = student.ZachNumber;
            BirthDate = student.BirthDate.ToString("yyyy-MM-dd");
            Address = student.Address;
            Phone = student.Phone;
            Orders = student.Orders;
            Starosta = student.Starosta ? 1 : 0;
            NFactor = student.NFactor ? 1 : 0;
            PaidEdu = student.PaidEdu ? 1 : 0;
            Expelled = student.Expelled ? 1 : 0;
        }

        public static List<MySQLStudent> FromStudentList(IEnumerable<Student> list)
        {
            return list.Select(student => new MySQLStudent(student)).ToList();
        }
    }
}
