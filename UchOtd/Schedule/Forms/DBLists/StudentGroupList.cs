using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class StudentGroupList : Form
    {
        enum RefreshType { GroupsOnly = 1, StudentsOnly, FullRefresh };

        private readonly ScheduleRepository _repo;

        public StudentGroupList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;            
        }

        private void LoadStudentsList()
        {
            var studentList = _repo
                //.GetFiltredStudents(st => st.Expelled == false)
                .Students
                .GetAllStudents()
                .OrderBy(st => st.F)
                .ThenBy(st => st.I)
                .ToList();

            var studentView = StudentView.StudentsToView(studentList);

            StudentList.DataSource = studentView;
            StudentList.ValueMember = "StudentId";
            StudentList.DisplayMember = "Summary";
        }

        private void StudentGroupListLoad(object sender, EventArgs e)
        {
            RefreshView((int)RefreshType.FullRefresh);

            LoadStudentsList();

            LoadGroupList();
        }

        private void LoadGroupList()
        {
            var studentGroupList = _repo.StudentGroups.GetAllStudentGroups().OrderBy(sg => sg.Name).ToList();

            groupsList.DataSource = studentGroupList;
            groupsList.DisplayMember = "Name";
            groupsList.ValueMember = "StudentGroupId";
        }

        private void RefreshView(int refreshType)
        {
            // 1 - groups only
            // 2 - students only
            // 3 - full refresh
            
            if ((refreshType == 1) || (refreshType == 3))
            {
                var studentGroupList = _repo.StudentGroups.GetAllStudentGroups();
                studentGroupList = studentGroupList.OrderBy(sg => sg.Name).ToList();

                StudentGroupListView.DataSource = studentGroupList;

                StudentGroupListView.Columns["StudentGroupId"].Visible = false;
                StudentGroupListView.Columns["Name"].Width = 240;
            }

            //StudentGroupListView.ClearSelection();

            if ((refreshType == 2) || (refreshType == 3))
            {
                var groupStudents = _repo.Students.GetGroupStudents(StudentGroupName.Text);

                if (groupStudents != null)
                {
                    var studentsView = StudentView.StudentsToView(groupStudents);
                    studentsView = studentsView
                        .OrderBy(s => s.Expelled)
                        .ThenBy(s => s.Fio)
                        .ToList();

                    StudentsInGroupListView.DataSource = studentsView;

                    StudentsInGroupListView.Columns["StudentId"].Visible = false;
                    StudentsInGroupListView.Columns["Fio"].Width = 200;
                    StudentsInGroupListView.Columns["Fio"].HeaderText = "Ф.И.О.";
                    StudentsInGroupListView.Columns["ZachNumber"].Width = 80;
                    StudentsInGroupListView.Columns["ZachNumber"].HeaderText = "№ зачётки";
                    StudentsInGroupListView.Columns["BirthDate"].Width = 80;
                    StudentsInGroupListView.Columns["BirthDate"].HeaderText = "Дата рождения";
                    StudentsInGroupListView.Columns["Address"].Width = 150;
                    StudentsInGroupListView.Columns["Address"].HeaderText = "Адрес";
                    StudentsInGroupListView.Columns["Phone"].Width = 80;
                    StudentsInGroupListView.Columns["Phone"].HeaderText = "Телефон";
                    StudentsInGroupListView.Columns["Orders"].Width = 150;
                    StudentsInGroupListView.Columns["Orders"].HeaderText = "Приказы";
                    StudentsInGroupListView.Columns["Starosta"].Width = 50;
                    StudentsInGroupListView.Columns["Starosta"].HeaderText = "Староста";
                    StudentsInGroupListView.Columns["NFactor"].Width = 50;
                    StudentsInGroupListView.Columns["NFactor"].HeaderText = "Наяновец";
                    StudentsInGroupListView.Columns["PaidEdu"].Width = 50;
                    StudentsInGroupListView.Columns["PaidEdu"].HeaderText = "Платное обучение";
                    StudentsInGroupListView.Columns["Expelled"].Width = 50;
                    StudentsInGroupListView.Columns["Expelled"].HeaderText = "Отчислен";

                    //StudentsInGroupListView.ClearSelection();
                }
            }
        }

        private void StudentGroupListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var studentGroup = ((List<StudentGroup>)StudentGroupListView.DataSource)[e.RowIndex];

            StudentGroupName.Text = studentGroup.Name;

            var groupStudents = _repo.Students.GetGroupStudents(StudentGroupName.Text)
                .OrderBy(s => s.Expelled)
                .ThenBy(s => s.F)
                .ToList();

            var studentsView = StudentView.StudentsToView(groupStudents);

            StudentsInGroupListView.DataSource = studentsView;

            StudentsInGroupListView.Columns["StudentId"].Visible = false;
            StudentsInGroupListView.Columns["Fio"].Width = 200;
            StudentsInGroupListView.Columns["Fio"].HeaderText = "Ф.И.О.";
            StudentsInGroupListView.Columns["ZachNumber"].Width = 80;
            StudentsInGroupListView.Columns["ZachNumber"].HeaderText = "№ зачётки";
            StudentsInGroupListView.Columns["BirthDate"].Width = 80;
            StudentsInGroupListView.Columns["BirthDate"].HeaderText = "Дата рождения";
            StudentsInGroupListView.Columns["Address"].Width = 150;
            StudentsInGroupListView.Columns["Address"].HeaderText = "Адрес";
            StudentsInGroupListView.Columns["Phone"].Width = 80;
            StudentsInGroupListView.Columns["Phone"].HeaderText = "Телефон";
            StudentsInGroupListView.Columns["Orders"].Width = 150;
            StudentsInGroupListView.Columns["Orders"].HeaderText = "Приказы";
            StudentsInGroupListView.Columns["Starosta"].Width = 50;
            StudentsInGroupListView.Columns["Starosta"].HeaderText = "Староста";
            StudentsInGroupListView.Columns["NFactor"].Width = 50;
            StudentsInGroupListView.Columns["NFactor"].HeaderText = "Наяновец";
            StudentsInGroupListView.Columns["PaidEdu"].Width = 50;
            StudentsInGroupListView.Columns["PaidEdu"].HeaderText = "Платное обучение";
            StudentsInGroupListView.Columns["Expelled"].Width = 50;
            StudentsInGroupListView.Columns["Expelled"].HeaderText = "Отчислен";

            //StudentsInGroupListView.ClearSelection();
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (_repo.StudentGroups.FindStudentGroup(StudentGroupName.Text) != null)
            {
                MessageBox.Show("Такая группа уже есть.");
                return;
            }

            var newStudentGroup = new StudentGroup { Name = StudentGroupName.Text };
            _repo.StudentGroups.AddStudentGroup(newStudentGroup);

            RefreshView((int)RefreshType.GroupsOnly);
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (StudentGroupListView.SelectedCells.Count > 0)
            {
                var studentGroup = ((List<StudentGroup>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];

                studentGroup.Name = StudentGroupName.Text;

                _repo.StudentGroups.UpdateStudentGroup(studentGroup);

                RefreshView((int)RefreshType.GroupsOnly);
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (StudentGroupListView.SelectedCells.Count > 0)
            {
                var studentGroup = ((List<StudentGroup>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];

                if (_repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroup.StudentGroupId).Count > 0)
                {
                    MessageBox.Show("В группе есть студенты.");
                    return;
                }

                if (_repo.Disciplines.GetFiltredDisciplines(d => d.StudentGroup.StudentGroupId == studentGroup.StudentGroupId).Count > 0)
                {
                    MessageBox.Show("Группа есть в учебном плане.");
                    return;
                }

                if (_repo
                    .GroupsInFaculties
                    .GetFiltredGroupsInFaculty(gif => gif.StudentGroup.StudentGroupId == studentGroup.StudentGroupId).Count > 0)
                {
                    MessageBox.Show("Группа есть в списке факультета.");
                    return;
                }

                _repo.StudentGroups.RemoveStudentGroup(studentGroup.StudentGroupId);

                RefreshView((int)RefreshType.GroupsOnly);
            }
        }

        private void deletewithlessons_Click(object sender, EventArgs e)
        {
            if (StudentGroupListView.SelectedCells.Count > 0)
            {
                var studentGroup = ((List<StudentGroup>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];

                var studentsInGroup = _repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroup.StudentGroupId);
                if (studentsInGroup.Count > 0)
                {
                    foreach (var sig in studentsInGroup)
                    {
                        _repo.StudentsInGroups.RemoveStudentsInGroups(sig.StudentsInGroupsId);
                    }
                }

                var groupDisciplines = _repo.Disciplines.GetFiltredDisciplines(d => d.StudentGroup.StudentGroupId == studentGroup.StudentGroupId);
                if (groupDisciplines.Count > 0)
                {
                    foreach (var disc in groupDisciplines)
                    {
                        _repo.Disciplines.RemoveDiscipline(disc.DisciplineId);
                    }
                }

                _repo.StudentGroups.RemoveStudentGroup(studentGroup.StudentGroupId);

                RefreshView((int)RefreshType.GroupsOnly);
            }
        }

        private void StudentGroupList_Resize(object sender, EventArgs e)
        {
            //StudentsInGroupListView.Columns["Fio"].Width = StudentListPanel.Width - 20;
        }

        private void addStudentToGroup_Click(object sender, EventArgs e)
        {
            if (StudentList.SelectedValue == null)
            {
                return;
            }

            var studentToAdd = _repo.Students.GetStudent((int)StudentList.SelectedValue);            

            if (StudentGroupListView.SelectedCells.Count > 0)
            {
                var studentGroup = ((List<StudentGroup>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];

                var sig = new StudentsInGroups { Student = studentToAdd, StudentGroup = studentGroup };

                _repo.StudentsInGroups.AddStudentsInGroups(sig);

                RefreshView((int)RefreshType.StudentsOnly);
            }
            else
            {
                MessageBox.Show("Ни одна группа не выделена.");
            }
        }

        private void removeStudentFrunGroup_Click(object sender, EventArgs e)
        {
            if (StudentGroupListView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Ни одна группа не выделена.");
            }

            if ((StudentsInGroupListView.SelectedCells.Count > 0) && (StudentGroupListView.SelectedCells.Count > 0))
            {
                var studentView = ((List<StudentView>)StudentsInGroupListView.DataSource)[StudentsInGroupListView.SelectedCells[0].RowIndex];
                var student = _repo.Students.GetStudent(studentView.StudentId);

                var studentGroup = ((List<StudentGroup>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];

                var sig = _repo.StudentsInGroups.FindStudentsInGroups(student, studentGroup);

                _repo.StudentsInGroups.RemoveStudentsInGroups(sig.StudentsInGroupsId);

                RefreshView((int)RefreshType.StudentsOnly);
            }           
        }

        private void addFromGroup_Click(object sender, EventArgs e)
        {
            var groupToAdd = _repo.StudentGroups.GetStudentGroup((int)groupsList.SelectedValue);

            var studentsToAdd = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupToAdd.StudentGroupId)
                .Select(sig => sig.Student)
                .Where(st => st.Expelled == false);

            if (StudentGroupListView.SelectedCells.Count > 0)
            {
                var studentGroup = ((List<StudentGroup>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];

                foreach (var studentToAdd in studentsToAdd)
                {
                    var sig = new StudentsInGroups { Student = studentToAdd, StudentGroup = studentGroup };
                    _repo.StudentsInGroups.AddStudentsInGroups(sig);
                }                

                RefreshView((int)RefreshType.StudentsOnly);
            }
            else
            {
                MessageBox.Show("Ни одна группа не выделена.");
            }
        }

        private void StudentList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                addStudentToGroup.PerformClick();
            }
        }

        private void StudentGroupName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                add.PerformClick();
            }
        }
    }
}
