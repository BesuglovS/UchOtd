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

        private void LoadLists()
        {
            var semesters = _repo
                .Semesters
                .GetAllSemesters()
                .OrderBy(s => s.StartingYear)
                .ThenBy(s => s.SemesterInYear)
                .ToList();

            semesterList.ValueMember = "SemesterId";
            semesterList.DisplayMember = "DisplayName";
            semesterList.DataSource = semesters;


            var studentList = _repo
                .Students
                .GetAllStudents()
                .OrderBy(st => st.F)
                .ThenBy(st => st.I)
                .ToList();

            var studentView = StudentView.StudentsToView(studentList);

            StudentList.DataSource = studentView;
            StudentList.ValueMember = "StudentId";
            StudentList.DisplayMember = "Summary";

            var studentGroupList = _repo.StudentGroups.GetAllStudentGroups().OrderBy(sg => sg.Name).ToList();
            var sgViewList = StudentGroupView.ViewFromList(studentGroupList);

            groupsList.DataSource = sgViewList;
            groupsList.DisplayMember = "NameWithSemester";
            groupsList.ValueMember = "StudentGroupId";
        }

        private void StudentGroupListLoad(object sender, EventArgs e)
        {
            RefreshView((int)RefreshType.FullRefresh);
            
            LoadLists();
        }

        

        private void RefreshView(int refreshType)
        {
            // 1 - groups only
            // 2 - students only
            // 3 - full refresh
            
            if ((refreshType == 1) || (refreshType == 3))
            {
                var studentGroupList = _repo.StudentGroups.GetAllStudentGroups();

                var semester = (Semester)semesterList.SelectedItem;

                if ((semesterFiltered.Checked) && (semester != null))
                {
                    studentGroupList =
                        studentGroupList.Where(sg => sg.Semester.SemesterId == semester.SemesterId).ToList();
                }

                studentGroupList = studentGroupList.OrderBy(sg => sg.Name).ToList();
                var sgViewList = StudentGroupView.ViewFromList(studentGroupList);

                StudentGroupListView.DataSource = sgViewList;

                StudentGroupListView.Columns["StudentGroupId"].Visible = false;
                StudentGroupListView.Columns["Name"].Width = 120;
                StudentGroupListView.Columns["SemesterDisplayName"].Width = 120;

                StudentGroupListView.Columns["NameWithSemester"].Visible = false;
            }

            //StudentGroupListView.ClearSelection();

            if ((refreshType == 2) || (refreshType == 3))
            {
                var studentGroupView = ((List<StudentGroupView>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];
                var studentGroup = _repo.StudentGroups.Get(studentGroupView.StudentGroupId);

                var groupSigs =
                    _repo.StudentsInGroups.GetFiltredStudentsInGroups(
                        sig => sig.StudentGroup.StudentGroupId == studentGroup.StudentGroupId);

                var sigView = StudentsInGroupsView.SigToView(groupSigs)
                    .OrderBy(sig => sig.PeriodFrom)
                    .ThenBy(sig => sig.StudentFioZachNum)
                    .ToList();

                StudentsInGroupListView.DataSource = sigView;

                FormatView();

                //StudentsInGroupListView.ClearSelection();
            }
        }

        private void StudentGroupListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var studentGroupView = ((List<StudentGroupView>)StudentGroupListView.DataSource)[e.RowIndex];
            var studentGroup = _repo.StudentGroups.Get(studentGroupView.StudentGroupId);
            
            var groupStudents = _repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroup.StudentGroupId)
                .OrderBy(sig => sig.Student.Expelled)
                .ThenBy(sig => sig.Student.F)
                .ThenBy(sig => sig.Student.I)
                .ThenBy(sig => sig.Student.O)
                .ToList();

            var studentsView = StudentsInGroupsView.SigToView(groupStudents);

            StudentsInGroupListView.DataSource = studentsView;

            FormatView();

            //StudentsInGroupListView.ClearSelection();
        }

        private void FormatView()
        {
            StudentsInGroupListView.Columns["StudentsInGroupsId"].Visible = false;
            StudentsInGroupListView.Columns["StudentId"].Visible = false;
            StudentsInGroupListView.Columns["StudentGroupId"].Visible = false;

            StudentsInGroupListView.Columns["StudentFioZachNum"].Width = 200;
            StudentsInGroupListView.Columns["StudentFioZachNum"].HeaderText = "Ф.И.О. + № зачётки";

            StudentsInGroupListView.Columns["StudentGroup"].Width = 200;
            StudentsInGroupListView.Columns["StudentGroup"].HeaderText = "Группа";

            StudentsInGroupListView.Columns["PeriodFrom"].Width = 200;
            StudentsInGroupListView.Columns["PeriodFrom"].HeaderText = "Начало периода";
            StudentsInGroupListView.Columns["PeriodFrom"].DefaultCellStyle.Format = "dd.MM.yyyy";

            StudentsInGroupListView.Columns["PeriodTo"].Width = 200;
            StudentsInGroupListView.Columns["PeriodTo"].HeaderText = "Конец периода";
            StudentsInGroupListView.Columns["PeriodTo"].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        private void add_Click(object sender, EventArgs e)
        {
            var semester = (Semester)semesterList.SelectedItem;

            if (_repo.StudentGroups.FindStudentGroup(StudentGroupName.Text, semester) != null)
            {
                MessageBox.Show("Такая группа уже есть.");
                return;
            }

            var newStudentGroup = new StudentGroup
            {
                Name = StudentGroupName.Text,
                Semester = semester
            };

            _repo.StudentGroups.AddStudentGroup(newStudentGroup);

            RefreshView((int)RefreshType.GroupsOnly);
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (StudentGroupListView.SelectedCells.Count > 0)
            {
                var studentGroup = ((List<StudentGroup>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];

                studentGroup.Name = StudentGroupName.Text;
                studentGroup.Semester = _repo.Semesters.GetSemester((int)semesterList.SelectedItem);

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
        
        private void addStudentToGroup_Click(object sender, EventArgs e)
        {
            if (StudentList.SelectedValue == null)
            {
                return;
            }

            var studentToAdd = _repo.Students.GetStudent((int)StudentList.SelectedValue);            

            if (StudentGroupListView.SelectedCells.Count > 0)
            {
                var studentGroupView = ((List<StudentGroupView>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];
                var studentGroup = _repo.StudentGroups.Get(studentGroupView.StudentGroupId);

                var sig = new StudentsInGroups
                {
                    Student = studentToAdd,
                    StudentGroup = studentGroup,
                    PeriodFrom = PeriodStart.Value,
                    PeriodTo = PeriodEnd.Value
                };

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
            if (StudentsInGroupListView.SelectedCells.Count > 0)
            {
                var sigView = ((List<StudentsInGroupsView>)StudentsInGroupListView.DataSource)[StudentsInGroupListView.SelectedCells[0].RowIndex];
                var sigId = sigView.StudentsInGroupsId;
                
                _repo.StudentsInGroups.RemoveStudentsInGroups(sigId);

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
                var studentGroupView = ((List<StudentGroupView>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];
                var studentGroup = _repo.StudentGroups.Get(studentGroupView.StudentGroupId);

                foreach (var studentToAdd in studentsToAdd)
                {
                    var sig = new StudentsInGroups
                    {
                        Student = studentToAdd,
                        StudentGroup = studentGroup,
                        PeriodFrom = PeriodStart.Value,
                        PeriodTo = PeriodEnd.Value
                    };

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

        private void refresh_Click(object sender, EventArgs e)
        {
            RefreshView((int)RefreshType.GroupsOnly);
        }

        private void updateSig_Click(object sender, EventArgs e)
        {
            if (StudentsInGroupListView.SelectedCells.Count > 0)
            {
                var sigView = ((List<StudentsInGroupsView>)StudentsInGroupListView.DataSource)[StudentsInGroupListView.SelectedCells[0].RowIndex];
                var sig = _repo.StudentsInGroups.Get(sigView.StudentsInGroupsId);

                _repo.StudentsInGroups.RemoveStudentsInGroups(sigId);

                RefreshView((int)RefreshType.StudentsOnly);

                /*
                 * var studentGroup = ((List<StudentGroup>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];

                studentGroup.Name = StudentGroupName.Text;
                studentGroup.Semester = _repo.Semesters.GetSemester((int)semesterList.SelectedItem);

                _repo.StudentGroups.UpdateStudentGroup(studentGroup);

                RefreshView((int)RefreshType.GroupsOnly);
                 */
            }
        }

        private void StudentsInGroupListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var sigView = ((List<StudentsInGroupsView>)StudentsInGroupListView.DataSource)[e.RowIndex];
            var sig = _repo.StudentsInGroups.Get(sigView.StudentsInGroupsId);
            
            sig.

            StudentsInGroupListView.DataSource = studentsView;

            FormatView();
        }
    }
}
