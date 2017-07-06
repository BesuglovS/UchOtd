﻿using System;
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

            SemesterList.ValueMember = "SemesterId";
            SemesterList.DisplayMember = "DisplayName";
            SemesterList.DataSource = semesters;

            var semesters2 = _repo
                .Semesters
                .GetAllSemesters()
                .OrderBy(s => s.StartingYear)
                .ThenBy(s => s.SemesterInYear)
                .ToList();

            SemesterFilter.ValueMember = "SemesterId";
            SemesterFilter.DisplayMember = "DisplayName";
            SemesterFilter.DataSource = semesters2;


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

            var studentList2 = _repo
                .Students
                .GetAllStudents()
                .OrderBy(st => st.F)
                .ThenBy(st => st.I)
                .ToList();
            var studentView2 = StudentView.StudentsToView(studentList2);
            studentFilter.DataSource = studentView2;
            studentFilter.ValueMember = "StudentId";
            studentFilter.DisplayMember = "Summary";

            var studentGroupList = _repo.StudentGroups
                .GetAllStudentGroups()
                .OrderBy(sg => sg.Semester.StartingYear)
                .ThenBy(sg => sg.Semester.SemesterInYear)
                .ThenBy(sg => sg.Name)
                .ToList();
            var sgViewList = StudentGroupView.ViewFromList(studentGroupList);

            groupsList.DataSource = sgViewList;
            groupsList.DisplayMember = "NameWithSemester";
            groupsList.ValueMember = "StudentGroupId";
        }

        private void StudentGroupListLoad(object sender, EventArgs e)
        {
            RefreshView(RefreshType.FullRefresh);
            
            LoadLists();
        }

        

        private void RefreshView(RefreshType refreshType)
        {
            // 1 - groups only
            // 2 - students only
            // 3 - full refresh
            if ((refreshType == RefreshType.StudentsOnly) && (studentFiltered.Checked))
            {
                StudentFilteredRefresh();
                return;
            }
            
            if ((refreshType == RefreshType.GroupsOnly) || (refreshType == RefreshType.FullRefresh))
            {
                var studentGroupList = _repo.StudentGroups.GetAllStudentGroups();

                var semester = (Semester)SemesterFilter.SelectedItem;

                if ((semesterFiltered.Checked) && (semester != null))
                {
                    studentGroupList =
                        studentGroupList.Where(sg => sg.Semester.SemesterId == semester.SemesterId).ToList();
                }

                studentGroupList = studentGroupList
                    .OrderBy(sg => sg.Semester.StartingYear)
                    .ThenBy(sg => sg.Semester.SemesterInYear)
                    .ThenBy(sg => sg.Name)
                    .ToList();
                var sgViewList = StudentGroupView.ViewFromList(studentGroupList);

                StudentGroupListView.DataSource = sgViewList;

                StudentGroupListView.Columns["StudentGroupId"].Visible = false;
                StudentGroupListView.Columns["Name"].Width = 120;
                StudentGroupListView.Columns["SemesterDisplayName"].Width = 120;

                StudentGroupListView.Columns["NameWithSemester"].Visible = false;
            }

            //StudentGroupListView.ClearSelection();

            if ((refreshType == RefreshType.StudentsOnly) || (refreshType == RefreshType.FullRefresh))
            {
                var studentGroupView = ((List<StudentGroupView>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];
                var studentGroup = _repo.StudentGroups.GetStudentGroup(studentGroupView.StudentGroupId);

                var groupSigs =
                    _repo.StudentsInGroups.GetFiltredStudentsInGroups(
                        sig => sig.StudentGroup.StudentGroupId == studentGroup.StudentGroupId);

                var sigView = StudentsInGroupsView.SigToView(groupSigs)
                    .OrderBy(sig => sig.PeriodFrom)
                    .ThenBy(sig => sig.StudentFioZachNum)
                    .ThenBy(sig => sig.StudentGroupName)
                    .ToList();

                StudentsInGroupListView.DataSource = sigView;

                FormatView();

                //StudentsInGroupListView.ClearSelection();
            }
        }

        private void StudentGroupListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var studentGroupView = ((List<StudentGroupView>)StudentGroupListView.DataSource)[e.RowIndex];
            var studentGroup = _repo.StudentGroups.GetStudentGroup(studentGroupView.StudentGroupId);

            StudentGroupName.Text = studentGroup.Name;
            SemesterList.SelectedValue = studentGroup.Semester.SemesterId;
            
            var groupStudents = _repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroup.StudentGroupId)
                .OrderBy(sig => sig.Student.F)
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

            StudentsInGroupListView.Columns["StudentGroupName"].Width = 120;
            StudentsInGroupListView.Columns["StudentGroupName"].HeaderText = "Группа";

            StudentsInGroupListView.Columns["PeriodFrom"].Width = 100;
            StudentsInGroupListView.Columns["PeriodFrom"].HeaderText = "Начало периода";
            StudentsInGroupListView.Columns["PeriodFrom"].DefaultCellStyle.Format = "dd.MM.yyyy";

            StudentsInGroupListView.Columns["PeriodTo"].Width = 100;
            StudentsInGroupListView.Columns["PeriodTo"].HeaderText = "Конец периода";
            StudentsInGroupListView.Columns["PeriodTo"].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        private void add_Click(object sender, EventArgs e)
        {
            var semester = (Semester)SemesterList.SelectedItem;

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

            RefreshView(RefreshType.GroupsOnly);
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (StudentGroupListView.SelectedCells.Count > 0)
            {
                var studentGroupId = ((List<StudentGroupView>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex].StudentGroupId;
                var studentGroup =
                    _repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.StudentGroupId == studentGroupId);

                studentGroup.Name = StudentGroupName.Text;
                studentGroup.Semester = (Semester)SemesterList.SelectedItem;

                _repo.StudentGroups.UpdateStudentGroup(studentGroup);

                RefreshView(RefreshType.GroupsOnly);
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (StudentGroupListView.SelectedCells.Count > 0)
            {
                var studentGroupId = ((List<StudentGroupView>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex].StudentGroupId;
                var studentGroup =
                    _repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.StudentGroupId == studentGroupId);

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

                RefreshView(RefreshType.GroupsOnly);
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
                var studentGroup = _repo.StudentGroups.GetStudentGroup(studentGroupView.StudentGroupId);

                var sig = new StudentsInGroups
                {
                    Student = studentToAdd,
                    StudentGroup = studentGroup,
                    PeriodFrom = PeriodStart.Value,
                    PeriodTo = PeriodEnd.Value
                };

                _repo.StudentsInGroups.AddStudentsInGroups(sig);

                RefreshView(RefreshType.StudentsOnly);
            }
            else
            {
                MessageBox.Show("Ни одна группа не выделена.");
            }
        }

        private void removeStudentFrunGroup_Click(object sender, EventArgs e)
        {
            if (StudentsInGroupListView.SelectedCells.Count == 1)
            {
                var sigView = ((List<StudentsInGroupsView>)StudentsInGroupListView.DataSource)[StudentsInGroupListView.SelectedCells[0].RowIndex];
                var sigId = sigView.StudentsInGroupsId;
                
                _repo.StudentsInGroups.RemoveStudentsInGroups(sigId);

                RefreshView(RefreshType.StudentsOnly);
            }

            if (StudentsInGroupListView.SelectedCells.Count > 1)
            {
                var rowIndexes = new List<int>();

                for (int i = 0; i < StudentsInGroupListView.SelectedCells.Count; i++)
                {
                    var cell = StudentsInGroupListView.SelectedCells[i];
                    if (!rowIndexes.Contains(cell.RowIndex))
                    {
                        rowIndexes.Add(cell.RowIndex);
                    }
                }

                foreach (var rowIndex in rowIndexes)
                {
                    var sigView = ((List<StudentsInGroupsView>)StudentsInGroupListView.DataSource)[rowIndex];
                    var sigId = sigView.StudentsInGroupsId;

                    _repo.StudentsInGroups.RemoveStudentsInGroups(sigId);
                }

                RefreshView(RefreshType.StudentsOnly);
            }
        }

        private void addFromGroup_Click(object sender, EventArgs e)
        {
            var groupToAdd = _repo.StudentGroups.GetStudentGroup((int)groupsList.SelectedValue);

            var studentsToAdd = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupToAdd.StudentGroupId)
                .Select(sig => sig.Student);

            if (StudentGroupListView.SelectedCells.Count > 0)
            {
                var studentGroupView = ((List<StudentGroupView>)StudentGroupListView.DataSource)[StudentGroupListView.SelectedCells[0].RowIndex];
                var studentGroup = _repo.StudentGroups.GetStudentGroup(studentGroupView.StudentGroupId);

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

                RefreshView(RefreshType.StudentsOnly);
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
            RefreshView(RefreshType.GroupsOnly);
        }

        private void updateSig_Click(object sender, EventArgs e)
        {
            if (StudentsInGroupListView.SelectedCells.Count == 1)
            {
                var sigView = ((List<StudentsInGroupsView>)StudentsInGroupListView.DataSource)[StudentsInGroupListView.SelectedCells[0].RowIndex];
                var sig = _repo.StudentsInGroups.GetStudentsInGroups(sigView.StudentsInGroupsId);

                var sgView =
                    ((List<StudentGroupView>) StudentGroupListView.DataSource)[
                        StudentGroupListView.SelectedCells[0].RowIndex];
                
                sig.StudentGroup = _repo.StudentGroups.GetStudentGroup(sgView.StudentGroupId);
                sig.Student = _repo.Students.GetStudent((int)StudentList.SelectedValue);

                sig.PeriodFrom = PeriodStart.Value.Date;
                sig.PeriodTo = PeriodEnd.Value.Date;

                _repo.StudentsInGroups.UpdateStudentsInGroups(sig);

                RefreshView(RefreshType.StudentsOnly);
            }

            if (StudentsInGroupListView.SelectedCells.Count > 1)
            {
                var rowIndexes = new List<int>();

                for (int i = 0; i < StudentsInGroupListView.SelectedCells.Count; i++)
                {
                    var cell = StudentsInGroupListView.SelectedCells[i];
                    if (!rowIndexes.Contains(cell.RowIndex))
                    {
                        rowIndexes.Add(cell.RowIndex);
                    }
                }

                foreach (var rowIndex in rowIndexes)
                {
                    var sigView = ((List<StudentsInGroupsView>)StudentsInGroupListView.DataSource)[rowIndex];
                    var sig = _repo.StudentsInGroups.GetStudentsInGroups(sigView.StudentsInGroupsId);
                    
                    sig.PeriodFrom = PeriodStart.Value.Date;
                    sig.PeriodTo = PeriodEnd.Value.Date;

                    _repo.StudentsInGroups.UpdateStudentsInGroups(sig);
                }

                RefreshView(RefreshType.StudentsOnly);
            }
        }

        private void StudentsInGroupListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            var sigView = ((List<StudentsInGroupsView>)StudentsInGroupListView.DataSource)[e.RowIndex];
            var sig = _repo.StudentsInGroups.GetStudentsInGroups(sigView.StudentsInGroupsId);

            var studentGroup = sig.StudentGroup;
            StudentGroupName.Text = studentGroup.Name;

            var sgViews = ((List<StudentGroupView>)StudentGroupListView.DataSource).Select(sgv => sgv.StudentGroupId).ToList();
            if (sgViews.Contains(studentGroup.StudentGroupId))
            {
                StudentGroupListView.CurrentCell = StudentGroupListView[1, sgViews.IndexOf(studentGroup.StudentGroupId)];
            }

            

            SemesterList.SelectedValue = studentGroup.Semester.SemesterId;

            StudentList.SelectedValue = sig.Student.StudentId;

            PeriodStart.Value = sig.PeriodFrom;
            PeriodEnd.Value = sig.PeriodTo;
        }

        private void studentFilterRefresh_Click(object sender, EventArgs e)
        {
            StudentFilteredRefresh();
        }

        private void StudentFilteredRefresh()
        {
            if (StudentList.SelectedValue == null)
            {
                return;
            }
            var studentId = (int) studentFilter.SelectedValue;

            var studentSigs = _repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.Student.StudentId == studentId);

            var sigView = StudentsInGroupsView.SigToView(studentSigs)
                .OrderBy(sig => sig.PeriodFrom)
                .ThenBy(sig => sig.StudentFioZachNum)
                .ThenBy(sig => sig.StudentGroupName)
                .ToList();

            StudentsInGroupListView.DataSource = sigView;

            FormatView();
        }

        private void removeAllSig_Click(object sender, EventArgs e)
        {
            var student = _repo.Students.GetStudent((int)StudentList.SelectedValue);

            var sigs =
                _repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.Student.StudentId == student.StudentId);

            foreach (var studentInGroup in sigs)
            {
                _repo.StudentsInGroups.RemoveStudentsInGroups(studentInGroup.StudentsInGroupsId);
            }
        }
    }
}
