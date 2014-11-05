using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            var groups = _repo.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            Group.ValueMember = "StudentGroupId";
            Group.DisplayMember = "Name";
            Group.DataSource = groups;

            var groups2 = _repo.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            groupNameList.ValueMember = "StudentGroupId";
            groupNameList.DisplayMember = "Name";
            groupNameList.DataSource = groups2;

            RefreshView();
        }

        private void RefreshView()
        {
            List<GroupPeriod> periodsList;

            if ((filter.Text != "") && discnameFilter.Checked)
            {
                periodsList = _repo.GetFiltredGroupPeriods(gp => gp.Name.Contains(filter.Text));
            }
            else
            {
                periodsList = _repo.GetAllGroupPeriods();
            }

            if (groupnameFilter.Checked)
            {
                var studentIds = _repo
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == (int)groupNameList.SelectedValue)
                    .ToList()
                    .Select(stig => stig.Student.StudentId);
                var groupsListIds = _repo
                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .ToList()
                    .Select(stig => stig.StudentGroup.StudentGroupId);

                periodsList = periodsList
                    .Where(gp => groupsListIds.Contains(gp.StudentGroup.StudentGroupId))
                    .ToList();
            }

            var periodsView = GroupPeriodView.GroupPeriodsToView(periodsList);

            PeriodsListView.DataSource = periodsView;

            FormatView();

            PeriodsListView.ClearSelection();
        }

        private void FormatView()
        {
            PeriodsListView.Columns["GroupPeriodId"].Visible = false;
            PeriodsListView.Columns["Name"].Width = 250;
            PeriodsListView.Columns["StudentGroup"].Width = 80;
            PeriodsListView.Columns["Start"].Width = 100;
            PeriodsListView.Columns["End"].Width = 100;
        }

        private void PeriodsListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var view = ((List<GroupPeriodView>) PeriodsListView.DataSource)[e.RowIndex];
            var groupPeriod = _repo.GetGroupPeriod(view.GroupPeriodId);

            PeriodName.Text = groupPeriod.Name;
            Group.SelectedValue = groupPeriod.StudentGroup.StudentGroupId;
            startOfPeriod.Value = groupPeriod.Start.Date;
            endOfPeriod.Value = groupPeriod.End.Date;
        }

        private void add_Click(object sender, EventArgs e)
        {
            var newPeriod = new GroupPeriod
            {
                Name = PeriodName.Text,
                StudentGroup = _repo.GetStudentGroup((int)Group.SelectedValue),
                Start = startOfPeriod.Value.Date,
                End = endOfPeriod.Value.Date
            };

            _repo.AddGroupPeriod(newPeriod);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (PeriodsListView.SelectedCells.Count > 0)
            {
                var view = ((List<GroupPeriodView>)PeriodsListView.DataSource)[PeriodsListView.SelectedCells[0].RowIndex];
                var groupPeriod = _repo.GetGroupPeriod(view.GroupPeriodId);

                groupPeriod.Name = PeriodName.Text;
                groupPeriod.StudentGroup = _repo.GetStudentGroup((int)Group.SelectedValue);
                groupPeriod.Start = startOfPeriod.Value.Date;
                groupPeriod.End = endOfPeriod.Value.Date;

                _repo.UpdateGroupPeriod(groupPeriod);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (PeriodsListView.SelectedCells.Count > 0)
            {
                var view = ((List<GroupPeriodView>)PeriodsListView.DataSource)[PeriodsListView.SelectedCells[0].RowIndex];

                _repo.RemoveGroupPeriod(view.GroupPeriodId);

                RefreshView();
            }
        }
    }
}
