using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Analyse;
using Schedule.Repositories;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class GroupPeriods : Form
    {
        private readonly ScheduleRepository _repo;

        public GroupPeriods(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void GroupPeriods_Load(object sender, EventArgs e)
        {
            var groups = _repo.StudentGroups.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            Group.ValueMember = "StudentGroupId";
            Group.DisplayMember = "Name";
            Group.DataSource = groups;

            var groups2 = _repo.StudentGroups.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            groupNameList.ValueMember = "StudentGroupId";
            groupNameList.DisplayMember = "Name";
            groupNameList.DataSource = groups2;

            RefreshView();
        }

        private void RefreshView()
        {
            List<CustomStudentGroupAttribute> periodsList;

            if ((filter.Text != "") && discnameFilter.Checked)
            {
                periodsList = _repo
                    .CustomStudentGroupAttributes
                    .GetFiltredCustomStudentGroupAttributes(csga => 
                        csga.Key == "StudentGroupPeriod" && 
                        csga.Value.Split('@')[0].Contains(filter.Text));
            }
            else
            {
                periodsList = _repo
                    .CustomStudentGroupAttributes
                    .GetFiltredCustomStudentGroupAttributes(csga => csga.Key == "StudentGroupPeriod");
            }

            if (groupnameFilter.Checked)
            {
                var studentIds = _repo
                    .StudentsInGroups
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == (int)groupNameList.SelectedValue)
                    .ToList()
                    .Select(stig => stig.Student.StudentId);
                var groupsListIds = _repo
                    .StudentsInGroups
                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .ToList()
                    .Select(stig => stig.StudentGroup.StudentGroupId);

                periodsList = periodsList
                    .Where(csga => groupsListIds.Contains(csga.StudentGroup.StudentGroupId))
                    .ToList();
            }

            var periodsView = GroupPeriodView.GroupPeriodsToView(_repo, periodsList);

            PeriodsListView.DataSource = periodsView;

            FormatView();

            PeriodsListView.ClearSelection();
        }

        private void FormatView()
        {
            PeriodsListView.Columns["CustomStudentGroupAttributeId"].Visible = false;

            PeriodsListView.Columns["Name"].Width = 250;
            PeriodsListView.Columns["StudentGroupName"].Width = 80;
            PeriodsListView.Columns["Start"].Width = 100;
            PeriodsListView.Columns["End"].Width = 100;
        }

        private void PeriodsListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var view = ((List<GroupPeriodView>) PeriodsListView.DataSource)[e.RowIndex];
            var groupPeriodAttribute = _repo
                .CustomStudentGroupAttributes
                .GetCustomStudentGroupAttribute(view.CustomStudentGroupAttributeId);

            var valueParts = groupPeriodAttribute.Value.Split('@');

            Group.SelectedValue = groupPeriodAttribute.StudentGroup.StudentGroupId;
            PeriodName.Text = valueParts[0];
            startOfPeriod.Value = DateTime.ParseExact(valueParts[1], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            endOfPeriod.Value = DateTime.ParseExact(valueParts[2], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            noLessonsPeriod.Checked = (valueParts[3] == "+");
        }

        private void add_Click(object sender, EventArgs e)
        {
            var newPeriod = new CustomStudentGroupAttribute
            {
                StudentGroup = _repo.StudentGroups.GetStudentGroup((int)Group.SelectedValue),
                Key = "StudentGroupPeriod",
                Value = PeriodName.Text + "@" + startOfPeriod.Value.ToString("dd.MM.yyyy") + "@" + endOfPeriod.Value.ToString("dd.MM.yyyy") +
                    "@" + (noLessonsPeriod.Checked ? "+" : "-")
            };

            _repo.CustomStudentGroupAttributes
                .AddCustomStudentGroupAttribute(newPeriod);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (PeriodsListView.SelectedCells.Count > 0)
            {
                var view = ((List<GroupPeriodView>)PeriodsListView.DataSource)[PeriodsListView.SelectedCells[0].RowIndex];
                var groupPeriod = _repo.CustomStudentGroupAttributes.GetCustomStudentGroupAttribute(view.CustomStudentGroupAttributeId);


                groupPeriod.StudentGroup = _repo.StudentGroups.GetStudentGroup((int)Group.SelectedValue);
                groupPeriod.Key = "StudentGroupPeriod";
                groupPeriod.Value = PeriodName.Text + "@" + startOfPeriod.Value.ToString("dd.MM.yyyy") + "@" + endOfPeriod.Value.ToString("dd.MM.yyyy") +
                    "@" + (noLessonsPeriod.Checked ? "+" : "-");

                _repo.CustomStudentGroupAttributes.UpdateCustomStudentGroupAttribute(groupPeriod);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (PeriodsListView.SelectedCells.Count > 0)
            {
                var view = ((List<GroupPeriodView>)PeriodsListView.DataSource)[PeriodsListView.SelectedCells[0].RowIndex];

                _repo.CustomStudentGroupAttributes.RemoveCustomStudentGroupAttribute(view.CustomStudentGroupAttributeId);

                RefreshView();
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            RefreshView();
        }
    }
}
