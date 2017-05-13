using System;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Core;
using UchOtd.Properties;

namespace UchOtd.Forms
{
    public partial class StudentProperties : Form
    {
        private readonly ScheduleRepository _repo;
        private readonly Student _student;
        private readonly StudentList _studentList;
        private readonly StudentDetailsMode _mode;

        public StudentProperties(StudentList parent, ScheduleRepository repo, int studentId, StudentDetailsMode mode)
        {
            InitializeComponent();

            _studentList = parent;
            _repo = repo;
            _student = _repo.Students.GetStudent(studentId);
            _mode = mode;

            if ((_student == null) && mode == StudentDetailsMode.Edit)
            {
                Close();
            }
        }

        private void StudentPropertiesLoad(object sender, EventArgs e)
        {
            if (_mode == StudentDetailsMode.New)
            {
                SetControlsFromStudent(null);
            }
            if (_mode == StudentDetailsMode.Edit)
            {
                SetControlsFromStudent(_student);
            }
        }

        private void SetControlsFromStudent(Student studentToSet)
        {
            if (studentToSet == null)
            {
                FamilyBox.Text = "";
                NameBox.Text = "";
                PatronymicBox.Text = "";
                IdNumBox.Text = "";
                BirthDateBox.Value = DateTime.Now;
                AddressBox.Text = "";
                PhoneBox.Text = "";
                OrdersBox.Text = "";
                StarostaBox.Checked = false;
                FromSchoolBox.Checked = false;
                PaidLearningBox.Checked = false;
                ExpelledBox.Checked = false;
                StudentGroupBox.Text = "";
                StudentGroupBox.ReadOnly = false;
                return;
            }

            FamilyBox.Text = studentToSet.F;
            NameBox.Text = studentToSet.I;
            PatronymicBox.Text = studentToSet.O;
            IdNumBox.Text = studentToSet.ZachNumber;
            BirthDateBox.Value = studentToSet.BirthDate;
            AddressBox.Text = studentToSet.Address;
            PhoneBox.Text = studentToSet.Phone;
            OrdersBox.Text = studentToSet.Orders;
            StarostaBox.Checked = studentToSet.Starosta;
            FromSchoolBox.Checked = studentToSet.NFactor;
            PaidLearningBox.Checked = studentToSet.PaidEdu;

            var studentGroupIds = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.Student.StudentId == studentToSet.StudentId)
                .Select(sig => sig.StudentGroup.StudentGroupId)
                .ToList();

            var studentGroup = _repo
                .StudentGroups
                .GetFiltredStudentGroups(sg => 
                    studentGroupIds.Contains(sg.StudentGroupId) && 
                    !sg.Name.Contains('I') && !sg.Name.Contains('-') && 
                    !sg.Name.Contains('+') && !sg.Name.Contains(".)"))
                .OrderBy(g => g.Name)
                .ToList();

            if (studentGroup.Count > 0)
            {
                StudentGroupBox.Text = studentGroup[0].Name;
            }

            var groupsList = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.Student.StudentId == studentToSet.StudentId)
                .Select(sig => sig.StudentGroup.Name)
                .Where(groupname => groupname != studentGroup[0].Name)
                .OrderBy(n => n)
                .ToList();

            if (groupsList.Count > 0)
            {
                StudentGroupBox.Text += Resources.OpenParenthesis + groupsList.Aggregate((a,b) => a + ", " + b) + Resources.CloseParenthesis;
            }
            StudentGroupBox.ReadOnly = true;
        }

        private void CancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FamilyBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
