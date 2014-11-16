using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class StudentList : Form
    {
        private readonly ScheduleRepository _repo;

        public StudentList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;

            var groups = _repo.StudentGroups.GetAllStudentGroups().OrderBy(sg => sg.Name).ToList();
            studentGroups.DataSource = groups;
            studentGroups.DisplayMember = "Name";
            studentGroups.ValueMember = "StudentGroupId";
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView(string fFilter = "")
        {
            List<Student> studentList;
            if ((studentGroups.SelectedIndex != -1) && (studentGroups.SelectedValue is int))
            {
                var studentIds = _repo
                    .StudentsInGroups
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == (int)studentGroups.SelectedValue)
                    .Select(sig => sig.Student.StudentId)
                    .ToList();
                studentList = _repo.Students.GetAllStudents().Where(s => studentIds.Contains(s.StudentId)).OrderBy(s => s.Expelled).ThenBy(s => s.F).ToList();
            }
            else
            {
                studentList = _repo.Students.GetAllStudents().OrderBy(s => s.Expelled).ThenBy(s => s.F).ToList();
            }

            if (fFilter != "")
            {
                studentList = studentList.Where(s => s.F.ToLower().Contains(fFilter.ToLower())).ToList();
            }

            var studentView = StudentView.StudentsToView(studentList);
            studentCombo.DataSource = studentView;
            studentCombo.DisplayMember = "Fio";
            studentCombo.ValueMember = "StudentId";


            StudentListView.DataSource = studentView;
            
            StudentListView.Columns["StudentId"].Visible = false;
            StudentListView.Columns["Fio"].Width = 200;
            StudentListView.Columns["Fio"].HeaderText = "Ф.И.О.";
            StudentListView.Columns["ZachNumber"].Width = 80;
            StudentListView.Columns["ZachNumber"].HeaderText = "№ зачётки";
            StudentListView.Columns["BirthDate"].Width = 80;
            StudentListView.Columns["BirthDate"].HeaderText = "Дата рождения";
            StudentListView.Columns["Address"].Width = 150;
            StudentListView.Columns["Address"].HeaderText = "Адрес";
            StudentListView.Columns["Phone"].Width = 80;
            StudentListView.Columns["Phone"].HeaderText = "Телефон";
            StudentListView.Columns["Orders"].Width = 150;
            StudentListView.Columns["Orders"].HeaderText = "Приказы";
            StudentListView.Columns["Starosta"].Width = 50;
            StudentListView.Columns["Starosta"].HeaderText = "Староста";
            StudentListView.Columns["NFactor"].Width = 50;
            StudentListView.Columns["NFactor"].HeaderText = "Наяновец";
            StudentListView.Columns["PaidEdu"].Width = 50;
            StudentListView.Columns["PaidEdu"].HeaderText = "Платное обучение";
            StudentListView.Columns["Expelled"].Width = 50;
            StudentListView.Columns["Expelled"].HeaderText = "Отчислен";
            
            StudentListView.ClearSelection();
        }

        private void StudentListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var studentView = ((List<StudentView>)StudentListView.DataSource)[e.RowIndex];
            var student = _repo.Students.GetStudent(studentView.StudentId);

            FBox.Text = student.F;
            IBox.Text = student.I;
            OBox.Text = student.O;
            ZachNumber.Text = student.ZachNumber;
            BirthDate.Value = student.BirthDate;
            Address.Text = student.Address;
            Phone.Text = student.Phone;
            Starosta.Checked = student.Starosta;
            NFactor.Checked = student.NFactor;
            PayForThis.Checked = student.PaidEdu;
            Expelled.Checked = student.Expelled;
            OrderList.Text = student.Orders;
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (checkZachNumberDistinction.Checked)
            {
                if (_repo.Students.FindStudent(ZachNumber.Text) != null)
                {
                    MessageBox.Show("Такой студент уже есть.");
                    return;
                }
            }

            var newStudent = new Student { 
                F = FBox.Text, 
                I = IBox.Text, 
                O = OBox.Text, 
                Address = Address.Text, 
                BirthDate = BirthDate.Value, 
                Expelled = Expelled.Checked, 
                NFactor = NFactor.Checked, 
                Orders = OrderList.Text,
                PaidEdu = PayForThis.Checked,
                Phone = Phone.Text,
                Starosta = Starosta.Checked,
                ZachNumber = ZachNumber.Text
            };

            _repo.Students.AddStudent(newStudent);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (StudentListView.SelectedCells.Count > 0)
            {
                var studentView = ((List<StudentView>)StudentListView.DataSource)[StudentListView.SelectedCells[0].RowIndex];
                var student = _repo.Students.GetStudent(studentView.StudentId);

                student.F = FBox.Text;
                student.I = IBox.Text;
                student.O = OBox.Text;
                student.Address = Address.Text;
                student.BirthDate = BirthDate.Value;
                student.Expelled = Expelled.Checked;
                student.NFactor = NFactor.Checked;
                student.Orders = OrderList.Text;
                student.PaidEdu = PayForThis.Checked;
                student.Phone = Phone.Text;
                student.Starosta = Starosta.Checked;
                student.ZachNumber = ZachNumber.Text;

                _repo.Students.UpdateStudent(student);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (StudentListView.SelectedCells.Count > 0)
            {
                var studentView = ((List<StudentView>)StudentListView.DataSource)[StudentListView.SelectedCells[0].RowIndex];

                if (_repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.Student.StudentId == studentView.StudentId).Count > 0)
                {
                    MessageBox.Show("Этот студент состоит в группах.");
                    return;
                }

                _repo.Students.RemoveStudent(studentView.StudentId);

                RefreshView();
            }
        }

        private void deletewithlessons_Click(object sender, EventArgs e)
        {
            if (StudentListView.SelectedCells.Count > 0)
            {
                var studentView = ((List<StudentView>)StudentListView.DataSource)[StudentListView.SelectedCells[0].RowIndex];

                var studentGroupLinks = _repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.Student.StudentId == studentView.StudentId);

                if (studentGroupLinks.Count > 0)
                {
                    foreach (var sig in studentGroupLinks)
                    {
                        _repo.StudentsInGroups.RemoveStudentsInGroups(sig.StudentsInGroupsId);
                    }
                }

                _repo.Students.RemoveStudent(studentView.StudentId);

                RefreshView();
            }
        }

        private void studentCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {                
                var sViews = ((List<StudentView>)StudentListView.DataSource);
                var view = sViews.FirstOrDefault(v => v.StudentId == (int)studentCombo.SelectedValue);
                if (view != null)
                {
                    var index = sViews.IndexOf(view);
                    StudentListView.Rows[index].Cells[0].Selected = true;
                    StudentListView_CellClick(this, new DataGridViewCellEventArgs(0, index));
                }
            }
        }

        private void studentCombo_TextChanged(object sender, EventArgs e)
        {

        }

        private void studentCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sViews = ((List<StudentView>)StudentListView.DataSource);
            if (sViews != null)
            {
                var view = sViews.FirstOrDefault(v => v.StudentId == (int)studentCombo.SelectedValue);
                if (view != null)
                {
                    var index = sViews.IndexOf(view);
                    StudentListView.Rows[index].Cells[0].Selected = true;
                    StudentListView_CellClick(this, new DataGridViewCellEventArgs(0, index));
                }
            }
        }

        private void studentGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void showAll_Click(object sender, EventArgs e)
        {
            studentGroups.SelectedIndex = -1;
            RefreshView();
        }

        private void FBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                add.PerformClick();
            }
        }

        private void FBox_TextChanged(object sender, EventArgs e)
        {
            var fio = FBox.Text;
            if (fio.Split(' ').Count() != 3)
            {
                return;
            }
            
            var f = fio.Split(' ')[0];
            var i = fio.Split(' ')[1];
            var o = fio.Split(' ')[2];
            FBox.Text = f[0] + f.Substring(1).ToLower();
            IBox.Text = i;
            OBox.Text = o;
        }

        private void Filter_Click(object sender, EventArgs e)
        {
            RefreshView(FBox.Text);
        }
    }
}
