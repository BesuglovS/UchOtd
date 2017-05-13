using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Views.DBListViews
{
    public class StudentView
    {
        public int StudentId { get; set; }
        public string Fio { get; set; }
        public string ZachNumber { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Orders { get; set; }
        public bool Starosta { get; set; }
        public bool NFactor { get; set; }
        public bool PaidEdu { get; set; }

        public string Summary { get; set; }

        public StudentView()
        {
        }

        public StudentView(Student st)
        {
            StudentId = st.StudentId;
            Fio = st.F + " " + st.I + " " + st.O;
            ZachNumber  = st.ZachNumber;
            BirthDate = st.BirthDate.ToShortDateString();
            Address = st.Address;
            Phone = st.Phone;
            Orders = st.Orders;
            NFactor = st.NFactor;
            PaidEdu = st.PaidEdu;
            Starosta = st.Starosta;

            Summary = Fio + " " + " (" + ZachNumber + ")";
        }

        public static List<StudentView> StudentsToView(List<Student> list)
        {
            return list.Select(st => new StudentView(st)).ToList();
        }
    }
}
