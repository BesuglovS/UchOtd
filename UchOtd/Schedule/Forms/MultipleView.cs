using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Constants;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Repositories.Common;
using UchOtd.Properties;
using UchOtd.Schedule.Views;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms
{
    public partial class MultipleView : Form
    {
        readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public MultipleView(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void MultipleView_Load(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();

            var groups1 = _repo
                .StudentGroups
                .GetAllStudentGroups()
                .Where(g => !g.Name.Contains(" + ") && !g.Name.Contains("I"))
                .OrderBy(g => g.Name)
                .ToList();
            groups1.Insert(0, new StudentGroup { StudentGroupId = -1, Name = "Empty" });
            var groups2 = new List<StudentGroup>(groups1);
            var groups3 = new List<StudentGroup>(groups1);
            var groups4 = new List<StudentGroup>(groups1);
            var groups5 = new List<StudentGroup>(groups1);


            group1.DisplayMember = "Name";
            group1.ValueMember = "StudentGroupId";
            group1.DataSource = groups1;
            group2.DisplayMember = "Name";
            group2.ValueMember = "StudentGroupId";
            group2.DataSource = groups2;
            group3.DisplayMember = "Name";
            group3.ValueMember = "StudentGroupId";
            group3.DataSource = groups3;
            group4.DisplayMember = "Name";
            group4.ValueMember = "StudentGroupId";
            group4.DataSource = groups4;
            group5.DisplayMember = "Name";
            group5.ValueMember = "StudentGroupId";
            group5.DataSource = groups5;
        }

        private async void update_Click(object sender, EventArgs e)
        {
            var groupsList = new List<int>();
            if ((int)group1.SelectedValue != -1)
            {
                groupsList.Add((int)group1.SelectedValue);
            }
            if ((int)group2.SelectedValue != -1)
            {
                groupsList.Add((int)group2.SelectedValue);
            }
            if ((int)group3.SelectedValue != -1)
            {
                groupsList.Add((int)group3.SelectedValue);
            }
            if ((int)group4.SelectedValue != -1)
            {
                groupsList.Add((int)group4.SelectedValue);
            }
            if ((int)group5.SelectedValue != -1)
            {
                groupsList.Add((int)group5.SelectedValue);
            }
            var groupNames = GetGroupNames(groupsList);

            List<FiveGroupsView> groupsEvents = null;

            if (update.Text == "Update")
            {
                _cToken = _tokenSource.Token;

                update.Text = "";
                update.Image = Resources.Loading;

                var repo = _repo;
                var isShowProposed = showProposed.Checked;

                try
                {
                    groupsEvents = await Task.Run(() =>
                    {
                        var groupsLessons = repo.CommonFunctions.GetGroupedGroupsLessons(groupsList, isShowProposed,
                            _cToken);
                        return CreateGroupsTableView(groupsLessons, _cToken);
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

            update.Image = null;
            update.Text = "Update";

            if (groupsEvents != null)
            {
                view.DataSource = groupsEvents;

                FormatView(groupsList, groupNames);
            }
        }

        private Dictionary<int, string> GetGroupNames(IEnumerable<int> groupsList)
        {
            var result = new Dictionary<int, string>();

            var groups = _repo
                .StudentGroups
                .GetAllStudentGroups()
                .ToList();

            foreach (int gr in groupsList)
            {
                var groupName = groups.FirstOrDefault(g => g.StudentGroupId == gr).Name;

                result.Add(gr, groupName);
            }

            return result;
        }

        private void FormatView(List<int> groupList, Dictionary<int, string> groupNames)
        {
            view.Columns["DowTime"].HeaderText = "День недели + Время";
            view.Columns["DowTime"].Width = 100;

            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["FirstGroupEvents"].HeaderText = groupList.Count > 0 ?
                groupNames[groupList[0]] : "Группа 1";
            view.Columns["FirstGroupEvents"].Width = 150;

            view.Columns["SecondGroupEvents"].HeaderText = groupList.Count > 1 ?
                groupNames[groupList[1]] : "Группа 2";
            view.Columns["SecondGroupEvents"].Width = 150;

            view.Columns["ThirdGroupEvents"].HeaderText = groupList.Count > 2 ?
                groupNames[groupList[2]] : "Группа 3";
            view.Columns["ThirdGroupEvents"].Width = 150;

            view.Columns["FourthGroupEvents"].HeaderText = groupList.Count > 3 ?
                groupNames[groupList[3]] : "Группа 4";
            view.Columns["FourthGroupEvents"].Width = 150;

            view.Columns["FifthGroupEvents"].HeaderText = groupList.Count > 4 ?
                groupNames[groupList[4]] : "Группа 5";
            view.Columns["FifthGroupEvents"].Width = 150;

            view.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            view.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private IEnumerable<GroupView> CreateGroupView(int groupId, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>> data)
        {
            var result = new List<GroupView>();

            var plainGroupName = "";
            var nGroupName = "";

            var group = _repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.StudentGroupId == groupId);

            if (group.Name.Contains(" (+Н)"))
            {
                plainGroupName = group.Name.Replace(" (+Н)", "");
                nGroupName = group.Name.Replace(" (+", "(");
            }

            foreach (var dt in data)
            {
                var eventString = "";

                for (int i = 0; i < dt.Value.Count; i++)
                {
                    var item = dt.Value.ElementAt(i);
                    var tfd = item.Value.Item2[0].TeacherForDiscipline;

                    eventString += tfd.Discipline.Name;
                    if (tfd.Discipline.StudentGroup.StudentGroupId != groupId &&
                        ((plainGroupName == "") || (tfd.Discipline.StudentGroup.Name != plainGroupName)) &&
                        ((nGroupName == "") || (tfd.Discipline.StudentGroup.Name != nGroupName)))
                    {
                        eventString += " (" + tfd.Discipline.StudentGroup.Name + ")";
                    }
                    eventString += Environment.NewLine;
                    eventString += tfd.Teacher.FIO + Environment.NewLine;
                    eventString += "(" + item.Value.Item1 + ")" + Environment.NewLine;

                    var audWeekList = item.Value.Item2.ToDictionary(l => _repo.CommonFunctions.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
                    var grouped = audWeekList.GroupBy(a => a.Value);

                    var enumerable = grouped as List<IGrouping<string, KeyValuePair<int, string>>> ?? grouped.ToList();
                    var gcount = enumerable.Count();
                    if (gcount == 1)
                    {
                        eventString += enumerable.ElementAt(0).Key;
                    }
                    else
                    {
                        for (int j = 0; j < gcount; j++)
                        {
                            var jItem = enumerable.ElementAt(j);
                            eventString += CommonFunctions.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " + jItem.Key;

                            if (j != gcount - 1)
                            {
                                eventString += Environment.NewLine;
                            }
                        }
                    }

                    if (i != dt.Value.Count - 1)
                    {
                        eventString += Environment.NewLine;
                    }

                }

                result.Add(new GroupView { Datetime = dt.Key, Events = eventString });
            }

            return result;
        }

        public List<FiveGroupsView> CreateGroupsTableView(Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>> groupsLessons, CancellationToken cToken)
        {
            var result = new List<FiveGroupsView>();

            var i = 1;
            foreach (var group in groupsLessons)
            {
                cToken.ThrowIfCancellationRequested();

                var groupView = CreateGroupView(group.Key, group.Value);

                foreach (var gv in groupView)
                {
                    var dowString = Constants.DowLocal[int.Parse(gv.Datetime.Substring(0, 1))] + gv.Datetime.Substring(1);

                    var item = result.FirstOrDefault(ri => ri.DowTime == dowString);
                    if (item == null)
                    {
                        item = new FiveGroupsView { DowTime = dowString };
                        result.Add(item);
                    }

                    switch (i)
                    {
                        case 1:
                            item.FirstGroupEvents += gv.Events;
                            break;
                        case 2:
                            item.SecondGroupEvents += gv.Events;
                            break;
                        case 3:
                            item.ThirdGroupEvents += gv.Events;
                            break;
                        case 4:
                            item.FourthGroupEvents += gv.Events;
                            break;
                        case 5:
                            item.FifthGroupEvents += gv.Events;
                            break;
                    }

                }

                result = result
                    .OrderBy(g => Constants.DowLocalReverse[g.DowTime.Split(' ')[0]] * 2000 + int.Parse(g.DowTime.Split(' ')[1].Split(':')[0]) * 60 + int.Parse(g.DowTime.Split(' ')[1].Split(':')[1]))
                    .ToList();
                i++;
            }
            return result;
        }
    }
}
