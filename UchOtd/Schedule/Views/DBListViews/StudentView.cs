using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Views.DBListViews
{
    public class StudentView
    {
        public int StudentId { get; set; }
        public string FIO { get; set; }
        public string ZachNumber { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Orders { get; set; }
        public bool Starosta { get; set; }
        public bool NFactor { get; set; }
        public bool PaidEdu { get; set; }
        public bool Expelled { get; set; }

        public StudentView()
        {
        }

        public StudentView(Student st)
        {
            StudentId = st.StudentId;
            FIO = st.F + " " + st.I + " " + st.O;
            ZachNumber  = st.ZachNumber;
            BirthDate = st.BirthDate.ToShortDateString();
            Address = st.Address;
            Phone = st.Phone;
            Orders = st.Orders;
            Expelled = st.Expelled;
            NFactor = st.NFactor;
            PaidEdu = st.PaidEdu;
            Starosta = st.Starosta;
        }

        public static List<StudentView> StudentsToView(List<Student> list)
        {
            var result = new List<StudentView>();

            foreach (var st in list)
            {
                result.Add(new StudentView(st));
            }

            return result;
        }       
    }
}
