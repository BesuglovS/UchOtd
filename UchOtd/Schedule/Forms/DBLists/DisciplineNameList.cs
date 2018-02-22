using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Properties;
using UchOtd.Schedule.Views;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class DisciplineNameList : Form
    {
        private readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public DisciplineNameList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void DisciplineNameList_Load(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();

            var discs = _repo.Disciplines.GetAllDisciplines();

            var discsViewList = DisciplineTextView.DisciplinesToView(discs);

            DisciplinesList.DisplayMember = "DisciplineSummary";
            DisciplinesList.ValueMember = "DisciplineId";
            DisciplinesList.DataSource = discsViewList;

            var groups = _repo.StudentGroups.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            StudentGroupList.ValueMember = "StudentGroupId";
            StudentGroupList.DisplayMember = "Name";
            StudentGroupList.DataSource = groups;

            var groups2 = _repo.StudentGroups.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            groupNameList.ValueMember = "StudentGroupId";
            groupNameList.DisplayMember = "Name";
            groupNameList.DataSource = groups2;
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private async void RefreshView()
        {
            List<DisciplineName> discNames = null;


            if (refresh.Text == "Обновить")
            {
                _cToken = _tokenSource.Token;

                refresh.Text = "";
                refresh.Image = Resources.Loading;

                var nameFiltered = nameFilter.Checked;
                var discNamefilter = filter.Text;
                var groupFiltered = groupnameFilter.Checked;
                var groupNameFilter = (int)groupNameList.SelectedValue;

                try
                {
                    discNames = await Task.Run(() => {
                        List<DisciplineName> discNameList = null;

                        if ((discNamefilter != "") && nameFiltered)
                        {
                            discNameList = _repo.DisciplineNames.GetFiltredDisciplineNames(dn => dn.Name.Contains(filter.Text));
                        }
                        else
                        {
                            discNameList = _repo.DisciplineNames.GetAllDisciplineNames();
                        }

                        if (groupFiltered)
                        {
                            var studentIds = _repo
                                .StudentsInGroups
                                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupNameFilter && !sig.Student.Expelled)
                                .ToList()
                                .Select(stig => stig.Student.StudentId);
                            var groupsListIds = _repo
                                .StudentsInGroups
                                .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                                .ToList()
                                .Select(stig => stig.StudentGroup.StudentGroupId);

                            discNameList = discNameList
                                .Where(dn => groupsListIds.Contains(dn.StudentGroup.StudentGroupId))
                                .ToList();
                        }


                        return discNameList.OrderBy(dn => dn.Name).ToList();
                    }, _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            refresh.Image = null;
            refresh.Text = "Обновить";

            var discNameViews = DisciplineNameView.DisciplineNamesToView(discNames);

            DiscipineNameListView.DataSource = discNameViews;

            FormatView();

            DiscipineNameListView.ClearSelection();
        }

        private void FormatView()
        {
            DiscipineNameListView.Columns["DisciplineNameId"].Visible = false;
            DiscipineNameListView.Columns["DisciplineId"].Visible = false;
            DiscipineNameListView.Columns["StudentGroupId"].Visible = false;

            DiscipineNameListView.Columns["Name"].HeaderText = "Название дисциплины";
            DiscipineNameListView.Columns["Name"].Width = 250;

            DiscipineNameListView.Columns["DisciplineSummary"].HeaderText = "Дисциплина";
            DiscipineNameListView.Columns["DisciplineSummary"].Width = 300;

            DiscipineNameListView.Columns["StudentGroupName"].HeaderText = "Группа для названия";
            DiscipineNameListView.Columns["StudentGroupName"].Width = 100;
        }

        private void add_Click(object sender, EventArgs e)
        {
            var discipline =
                _repo.Disciplines.GetFirstFiltredDisciplines(d => d.DisciplineId == (int)DisciplinesList.SelectedValue);

            var studentGroup =
                _repo.StudentGroups.GetFirstFiltredStudentGroups(
                    sg => sg.StudentGroupId == (int)StudentGroupList.SelectedValue);

            var newDisciplineName = new DisciplineName
            {
                Discipline = discipline,
                StudentGroup = studentGroup,
                Name = DisciplineName.Text
            };

            _repo.DisciplineNames.AddDisciplineName(newDisciplineName);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (DiscipineNameListView.SelectedCells.Count > 0)
            {
                var discNameView = ((List<DisciplineView>)DiscipineNameListView.DataSource)[DiscipineNameListView.SelectedCells[0].RowIndex];
                var disciplineName = _repo.DisciplineNames.GetDisciplineName(discNameView.DisciplineId);

                disciplineName.Name = DisciplineName.Text;

                disciplineName.Discipline =
                    _repo.Disciplines.GetFirstFiltredDisciplines(d => d.DisciplineId == (int)DisciplinesList.SelectedValue);
                disciplineName.StudentGroup =
                    _repo.StudentGroups.GetFirstFiltredStudentGroups(
                        sg => sg.StudentGroupId == (int)StudentGroupList.SelectedValue);

                _repo.DisciplineNames.UpdateDisciplineName(disciplineName);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (DiscipineNameListView.SelectedRows.Count > 1)
            {
                var discNamesIds = new List<int>();
                for (int i = 0; i < DiscipineNameListView.SelectedRows.Count; i++)
                {
                    discNamesIds.Add(((List<DisciplineNameView>)DiscipineNameListView.DataSource)[DiscipineNameListView.SelectedRows[i].Index].DisciplineNameId);
                }

                foreach (var id in discNamesIds)
                {
                    _repo.DisciplineNames.RemoveDisciplineName(id);
                }

                RefreshView();
                return;
            }

            if (DiscipineNameListView.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineNameView>)DiscipineNameListView.DataSource)[DiscipineNameListView.SelectedCells[0].RowIndex];

                _repo.DisciplineNames.RemoveDisciplineName(discView.DisciplineNameId);

                RefreshView();
            }
        }

        private void DiscipineNameListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var discNameView = ((List<DisciplineNameView>)DiscipineNameListView.DataSource)[e.RowIndex];
            var disciplineName = _repo.DisciplineNames.GetDisciplineName(discNameView.DisciplineNameId);

            if (disciplineName != null)
            {
                DisciplineName.Text = disciplineName.Name;

                DisciplinesList.SelectedValue = disciplineName.Discipline.DisciplineId;
                StudentGroupList.SelectedValue = disciplineName.StudentGroup.StudentGroupId;
            }
        }
    }
}
