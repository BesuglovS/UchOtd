using Schedule.DomainClasses.Main;
using System.Collections.Generic;
using System.Linq;

namespace UchOtd.Views
{
    public class StudentListView
    {
        public StudentListView(string id, string data)
        {
            IdString = id;
            DataString = data;
        }

        public static List<StudentListView> FromStudentList(List<Student> list)
        {
            return list
                .Select(student => 
                    new StudentListView(
                        "student@" + student.StudentId, student.F + " " + student.I + " " + student.O + 
                        " (" + student.ZachNumber + ")"))
                .ToList();
        }

        public static List<StudentListView> FromGroupList(List<StudentGroup> list)
        {
            return list
                .Select(studentGroup => 
                    new StudentListView("studentGroup@" + studentGroup.StudentGroupId, studentGroup.Name))
                .ToList();
        }

        public string IdString { get; set; }
        public string DataString { get; set; }
    }
}
