using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;

namespace UchOtd.Schedule.Forms.DBLists
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
                    .Faculties
                    .GetAllFaculties()
                    .OrderBy(f => f.SortingOrder)
                    .ToList();

                FacultiesListView.DataSource = faculties;

                FacultiesListView.Columns["FacultyId"].Visible = false;
                FacultiesListView.Columns["SortingOrder"].Visible = false;

                FacultiesListView.Columns["Name"].Width = (int)Math.Round((FacultiesListView.Width - 30) * 0.6);

                FacultiesListView.Columns["ScheduleSigningTitle"].Width = (int)Math.Round((FacultiesListView.Width - 30) * 0.1);
                FacultiesListView.Columns["DeanSigningSchedule"].Width = (int)Math.Round((FacultiesListView.Width - 30) * 0.1);

                FacultiesListView.Columns["SessionSigningTitle"].Width = (int)Math.Round((FacultiesListView.Width - 30) * 0.1);
                FacultiesListView.Columns["DeanSigningSessionSchedule"].Width = (int)Math.Round((FacultiesListView.Width - 30) * 0.1);

                FacultiesListView.Columns["Letter"].Width = 30;
            }

            if (refreshType == RefreshType.GroupsOnly || refreshType == RefreshType.FullRefresh)
            {
                Faculty faculty = null;
                if (FacultiesListView.SelectedCells.Count > 0)
                {
                    faculty = ((List<Faculty>)FacultiesListView.DataSource)[FacultiesListView.SelectedCells[0].RowIndex];
                }

                if (faculty == null)
                {
                    return;
                }

                var facultyGroups = _repo
                    .GroupsInFaculties
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
            var studentGroupList = _repo.StudentGroups.GetAllStudentGroups().OrderBy(sg => sg.Name).ToList();

            GroupList.DisplayMember = "Name";
            GroupList.ValueMember = "StudentGroupId";
            GroupList.DataSource = studentGroupList;
        }

        private void FacultiesListViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            var faculty = ((List<Faculty>)FacultiesListView.DataSource)[e.RowIndex];

            FacultyName.Text = faculty.Name;
            FacultyLetter.Text = faculty.Letter;
            SortingOrder.Text = faculty.SortingOrder.ToString(CultureInfo.InvariantCulture);

            TitleOfSemesterScheduleSigner.Text = faculty.ScheduleSigningTitle;
            SemesterScheduleSigner.Text = faculty.DeanSigningSchedule;

            TitleOfSessionScheduleSigner.Text = faculty.SessionSigningTitle;
            SessionScheduleSigner.Text = faculty.DeanSigningSessionSchedule;


            var facultyGroups = _repo.Faculties.GetFacultyGroups(faculty.FacultyId).OrderBy(sg => sg.Name).ToList();

            GroupsView.DataSource = facultyGroups;

            GroupsView.Columns["StudentGroupId"].Visible = false;
            GroupsView.Columns["Name"].Width = GroupListPanel.Width - 20;
        }

        private void AddClick(object sender, EventArgs e)
        {
            int sOrder;
            int.TryParse(SortingOrder.Text, out sOrder);

            var newFaculty = new Faculty(FacultyName.Text, FacultyLetter.Text, sOrder,
                TitleOfSemesterScheduleSigner.Text, SemesterScheduleSigner.Text,
                TitleOfSessionScheduleSigner.Text, SessionScheduleSigner.Text);
            _repo.Faculties.AddFaculty(newFaculty);

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

                faculty.ScheduleSigningTitle = TitleOfSemesterScheduleSigner.Text;
                faculty.DeanSigningSchedule = SemesterScheduleSigner.Text;

                faculty.SessionSigningTitle = TitleOfSessionScheduleSigner.Text;
                faculty.DeanSigningSessionSchedule = SessionScheduleSigner.Text;

                _repo.Faculties.UpdateFaculty(faculty);

                RefreshView(RefreshType.FacultiesOnly);
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (FacultiesListView.SelectedCells.Count > 0)
            {
                var faculty = ((List<Faculty>)FacultiesListView.DataSource)[FacultiesListView.SelectedCells[0].RowIndex];

                if (_repo.GroupsInFaculties.GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId).Any())
                {
                    MessageBox.Show("К факультету привязаны группы.");
                    return;
                }

                _repo.Faculties.RemoveFaculty(faculty.FacultyId);

                RefreshView(RefreshType.FullRefresh);
            }
        }

        private void cascadeDelete_Click(object sender, EventArgs e)
        {
            if (FacultiesListView.SelectedCells.Count > 0)
            {
                var faculty = ((List<Faculty>)FacultiesListView.DataSource)[FacultiesListView.SelectedCells[0].RowIndex];

                var gifIds = _repo
                    .GroupsInFaculties
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                    .Select(gif => gif.GroupsInFacultyId)
                    .ToList();

                foreach (var gifId in gifIds)
                {
                    _repo.GroupsInFaculties.RemoveGroupsInFaculty(gifId);
                }

                _repo.Faculties.RemoveFaculty(faculty.FacultyId);
            }
        }

        private void addGroupToFaculty_Click(object sender, EventArgs e)
        {
            if (GroupList.SelectedValue == null)
            {
                return;
            }

            var groupToAdd = _repo.StudentGroups.GetStudentGroup((int)GroupList.SelectedValue);

            if (FacultiesListView.SelectedCells.Count > 0)
            {
                var faculty = ((List<Faculty>)FacultiesListView.DataSource)[FacultiesListView.SelectedCells[0].RowIndex];

                var gif = new GroupsInFaculty { StudentGroup = groupToAdd, Faculty = faculty };

                _repo.GroupsInFaculties.AddGroupsInFaculty(gif);

                RefreshView(RefreshType.GroupsOnly);
            }
            else
            {
                MessageBox.Show("Не выбран факультет.");
            }
        }

        private void removeGroupFromFaculty_Click(object sender, EventArgs e)
        {
            if (FacultiesListView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выбран факультет.");
            }

            if ((FacultiesListView.SelectedCells.Count > 0) && (GroupsView.SelectedCells.Count > 0))
            {
                var faculty = ((List<Faculty>)FacultiesListView.DataSource)[FacultiesListView.SelectedCells[0].RowIndex];

                var studentGroup = ((List<StudentGroup>)GroupsView.DataSource)[GroupsView.SelectedCells[0].RowIndex];

                var gif = _repo.GroupsInFaculties.FindGroupsInFaculty(studentGroup.Name, faculty.Name);

                _repo.GroupsInFaculties.RemoveGroupsInFaculty(gif.GroupsInFacultyId);

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
