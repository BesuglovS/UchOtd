using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;

namespace Schedule.Forms.DBLists
{
    public partial class FacultyList : Form
    {
        enum RefreshType { FacultiesOnly = 1, GroupsOnly, FullRefresh };

        private readonly ScheduleRepository _repo;

        public FacultyList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void FacultyListLoad(object sender, EventArgs e)
        {
            LoadStudentGroupList();

            RefreshView(RefreshType.FullRefresh);
        }

        private void RefreshView(RefreshType refreshType)
        {
            if (refreshType == RefreshType.FacultiesOnly || refreshType == RefreshType.FullRefresh)
            {
                var faculties = _repo
                    .GetAllFaculties()
                    .OrderBy(f => f.SortingOrder)
                    .ToList();

                FacultiesListView.DataSource = faculties;

                FacultiesListView.Columns["FacultyId"].Visible = false;
                FacultiesListView.Columns["SortingOrder"].Visible = false;

                FacultiesListView.Columns["Name"].Width = FacultiesListView.Width - 50;

                FacultiesListView.Columns["Letter"].Width = 30;
            }

            if (refreshType == RefreshType.GroupsOnly || refreshType == RefreshType.FullRefresh)
            {
                var faculty = ((List<Faculty>)FacultiesListView.DataSource)[FacultiesListView.SelectedCells[0].RowIndex];

                if (faculty == null)
                {
                    return;
                }

                var facultyGroups = _repo
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                    .Select(gif => gif.StudentGroup)
                    .ToList();

                GroupsView.DataSource = facultyGroups;

                GroupsView.Columns["StudentGroupId"].Visible = false;

                GroupsView.Columns["Name"].Width = GroupsView.Width - 50;
            }
        }

        private void LoadStudentGroupList()
        {
            var studentGroupList = _repo.GetAllStudentGroups().OrderBy(sg => sg.Name).ToList();

            GroupList.DisplayMember = "Name";
            GroupList.ValueMember = "StudentGroupId";
            GroupList.DataSource = studentGroupList;
        }

        private void FacultiesListViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            var faculty = ((List<Faculty>) FacultiesListView.DataSource)[e.RowIndex];

            FacultyName.Text = faculty.Name;
            FacultyLetter.Text = faculty.Letter;
            SortingOrder.Text = faculty.SortingOrder.ToString(CultureInfo.InvariantCulture);

            var facultyGroups = _repo.GetFacultyGroups(faculty.FacultyId).OrderBy(sg => sg.Name).ToList();

            GroupsView.DataSource = facultyGroups;

            GroupsView.Columns["StudentGroupId"].Visible = false;
            GroupsView.Columns["Name"].Width = GroupListPanel.Width - 20;
        }

        private void AddClick(object sender, EventArgs e)
        {
            int sOrder;
            int.TryParse(SortingOrder.Text, out sOrder);

            var newFaculty = new Faculty(FacultyName.Text, FacultyLetter.Text, sOrder);
            _repo.AddFaculty(newFaculty);

            RefreshView(RefreshType.FacultiesOnly);
        }

        private void UpdateClick(object sender, EventArgs e)
        {
            if (FacultiesListView.SelectedCells.Count > 0)
            {
                var faculty = ((List<Faculty>)FacultiesListView.DataSource)[FacultiesListView.SelectedCells[0].RowIndex];

                faculty.Name = FacultyName.Text;
                faculty.Letter = FacultyLetter.Text;
                int sOrder;
                int.TryParse(SortingOrder.Text, out sOrder);
                faculty.SortingOrder = sOrder;

                _repo.UpdateFaculty(faculty);

                RefreshView(RefreshType.FacultiesOnly);
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (FacultiesListView.SelectedCells.Count > 0)
            {
                var faculty = ((List<Faculty>)FacultiesListView.DataSource)[FacultiesListView.SelectedCells[0].RowIndex];

                if (_repo.GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId).Count() > 0)
                {
                    MessageBox.Show("К факультету привязаны группы.");
                    return;
                }

                _repo.RemoveFaculty(faculty.FacultyId);

                RefreshView(RefreshType.FullRefresh);
            }
        }

        private void cascadeDelete_Click(object sender, EventArgs e)
        {
            if (FacultiesListView.SelectedCells.Count > 0)
            {
                var faculty = ((List<Faculty>)FacultiesListView.DataSource)[FacultiesListView.SelectedCells[0].RowIndex];

                var gifIds = _repo
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                    .Select(gif => gif.GroupsInFacultyId)
                    .ToList();

                foreach (var gifId in gifIds)
                {
                    _repo.RemoveGroupsInFaculty(gifId);
                }

                _repo.RemoveFaculty(faculty.FacultyId);
            }
        }

        private void addGroupToFaculty_Click(object sender, EventArgs e)
        {
            if (GroupList.SelectedValue == null)
            {
                return;
            }

            var groupToAdd = _repo.GetStudentGroup((int)GroupList.SelectedValue);

            if (FacultiesListView.SelectedCells.Count > 0)
            {
                var faculty = ((List<Faculty>)FacultiesListView.DataSource)[FacultiesListView.SelectedCells[0].RowIndex];

                var gif = new GroupsInFaculty { StudentGroup = groupToAdd, Faculty = faculty };

                _repo.AddGroupsInFaculty(gif);

                RefreshView(RefreshType.GroupsOnly);
            }
            else
            {
                MessageBox.Show("Не выбран факультет.");
            }
        }

        private void removeStudentFrunGroup_Click(object sender, EventArgs e)
        {
            if (FacultiesListView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выбран факультет.");
            }

            if ((FacultiesListView.SelectedCells.Count > 0) && (GroupsView.SelectedCells.Count > 0))
            {
                var faculty = ((List<Faculty>)FacultiesListView.DataSource)[FacultiesListView.SelectedCells[0].RowIndex];

                var studentGroup = ((List<StudentGroup>)GroupsView.DataSource)[GroupsView.SelectedCells[0].RowIndex];

                var gif = _repo.FindGroupsInFaculty(studentGroup.Name, faculty.Name);

                _repo.RemoveGroupsInFaculty(gif.GroupsInFacultyId);

                RefreshView(RefreshType.GroupsOnly);
            }  
        }

        private void GroupList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                addGroupToFaculty.PerformClick();
            }
        }
        
    }
}
