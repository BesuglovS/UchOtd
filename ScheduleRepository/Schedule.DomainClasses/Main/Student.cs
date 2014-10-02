using System;

namespace Schedule.DomainClasses.Main
{
    public class Student
    {        
        public Student()
        {
        }

        public Student(string f, string i, string o, string zachNumber, DateTime birthDate, string address, string phone, string orders)
        {
            F = f;
            I = i;
            O = o;
            ZachNumber = zachNumber;
            BirthDate = birthDate;
            Address = address;
            Phone = phone;
            Orders = orders;
        }

        public int StudentId { get; set; }
        public string F { get; set; }
        public string I { get; set; }
        public string O { get; set; }
        public string ZachNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Orders { get; set; }
        public bool Starosta { get; set; }
        public bool NFactor { get; set; }
        public bool PaidEdu { get; set; }
        public bool Expelled { get; set; }
    }    
}
