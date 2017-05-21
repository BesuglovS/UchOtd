using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Repositories;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.Merge
{
    public partial class MergeStudents : Form
    {
        private readonly ScheduleRepository _repo;

        public MergeStudents(ScheduleRepository repo)
        {
            _repo = repo;
            InitializeComponent();
        }

        private void Go_Click(object sender, EventArgs e)
        {
            var student1 = _repo.Students.GetStudent((int) studentList1.SelectedValue);
            var student2 = _repo.Students.GetStudent((int) studentList2.SelectedValue);

            var sig2 =
                _repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.Student.StudentId == student2.StudentId);

            foreach (var sig in sig2)
            {
                sig.Student = student1;

                _repo.StudentsInGroups.UpdateStudentsInGroups(sig);
            }

            _repo.Students.RemoveStudent(student2.StudentId);
        }

        private void MergeStudents_Load(object sender, EventArgs e)
        {
            LoadLists();

        }

        private void LoadLists()
        {
            var list1 = _repo
                .Students
                .GetAllStudents()
                .OrderBy(st => st.F)
                .ThenBy(st => st.I)
                .ToList();
            var studentView = StudentView.StudentsToView(list1);
            studentList1.DataSource = studentView;
            studentList1.ValueMember = "StudentId";
            studentList1.DisplayMember = "Summary";

            var list2 = _repo
                .Students
                .GetAllStudents()
                .OrderBy(st => st.F)
                .ThenBy(st => st.I)
                .ToList();
            var studentView2 = StudentView.StudentsToView(list2);
            studentList2.DataSource = studentView2;
            studentList2.ValueMember = "StudentId";
            studentList2.DisplayMember = "Summary";
        }
    }
}
