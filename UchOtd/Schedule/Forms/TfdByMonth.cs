using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Constants;
using Schedule.Repositories;
using UchOtd.Properties;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms
{
    public partial class TfdByMonth : Form
    {
        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        private readonly ScheduleRepository _repo;

        private bool[] _pristineMonth;

        private TfdByMonth form;

        public TfdByMonth(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
            form = this;
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private List<int> StudentGroupIdsFromGroupId(int groupId)
        {
            var studentIds = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId && !sig.Student.Expelled)
                .Select(stig => stig.Student.StudentId)
                .ToList();

            var groupsListIds = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                .Select(stig => stig.StudentGroup.StudentGroupId)
                .Distinct()
                .ToList();
            return groupsListIds;
        }

        private async void RefreshView()
        {
            List<TfdItemByMonth> resultView = null;

            if (refresh.Text == "Обновить")
            {
                _cToken = _tokenSource.Token;

                refresh.Text = "";
                refresh.Image = Resources.Loading;

                var filteredByDiscName = filteredByDisciplineName.Checked;
                var DiscNameFilter = DisciplineNameFilter.Text;
                var filteredByTeacherFIO = this.filteredByTeacherFIO.Checked;
                var TeacherFioFilter = (int)TeacherFIOFilter.SelectedValue;
                var filteredByGroup = FilteredByStudentGroup.Checked;
                var GroupFilter = (int)StudentGroupFilter.SelectedValue;

                try
                {
                    resultView = await Task.Run(() =>
                    {
                        var tfdList = _repo.TeacherForDisciplines.GetAllTeacherForDiscipline();

                        if ((filteredByDiscName) && (DiscNameFilter != ""))
                        {
                            tfdList = tfdList.Where(tfd => tfd.Discipline.Name.Contains(DiscNameFilter)).ToList();
                        }

                        if (filteredByTeacherFIO && (TeacherFioFilter != -1))
                        {
                            tfdList = tfdList.Where(tfd => tfd.Teacher.TeacherId == TeacherFioFilter).ToList();
                        }

                        if (filteredByGroup && (GroupFilter != -1))
                        {
                            var groups = StudentGroupIdsFromGroupId(GroupFilter);

                            tfdList = tfdList.Where(tfd => groups.Contains(tfd.Discipline.StudentGroup.StudentGroupId)).ToList();
                        }

                        var result = new List<TfdItemByMonth>();

                        _pristineMonth = new bool[13] { false, true, true, true, true, true, true, true, true, true, true, true, true };

                        for (int i = 0; i < tfdList.Count; i++)
                        {
                            var tfdId = tfdList[i].TeacherForDisciplineId;
                            var resultItem = new TfdItemByMonth
                            {
                                TfdId = tfdList[i].TeacherForDisciplineId,
                                DisciplineName = tfdList[i].Discipline.Name
                            };

                            var lessons =
                                _repo.Lessons.GetFiltredLessons(
                                    l => l.TeacherForDiscipline.TeacherForDisciplineId == tfdId && l.State == 1)
                                    .ToList();

                            foreach (var lesson in lessons)
                            {
                                switch (lesson.Calendar.Date.Month)
                                {
                                    case 1:
                                        resultItem.JanHours += 2;
                                        _pristineMonth[1] = false;
                                        break;
                                    case 2:
                                        resultItem.FebHours += 2;
                                        _pristineMonth[2] = false;
                                        break;
                                    case 3:
                                        resultItem.MarHours += 2;
                                        _pristineMonth[3] = false;
                                        break;
                                    case 4:
                                        resultItem.AprHours += 2;
                                        _pristineMonth[4] = false;
                                        break;
                                    case 5:
                                        resultItem.MayHours += 2;
                                        _pristineMonth[5] = false;
                                        break;
                                    case 6:
                                        resultItem.JunHours += 2;
                                        _pristineMonth[6] = false;
                                        break;
                                    case 7:
                                        resultItem.JulHours += 2;
                                        _pristineMonth[7] = false;
                                        break;
                                    case 8:
                                        resultItem.AugHours += 2;
                                        _pristineMonth[8] = false;
                                        break;
                                    case 9:
                                        resultItem.SepHours += 2;
                                        _pristineMonth[9] = false;
                                        break;
                                    case 10:
                                        resultItem.OctHours += 2;
                                        _pristineMonth[10] = false;
                                        break;
                                    case 11:
                                        resultItem.NovHours += 2;
                                        _pristineMonth[11] = false;
                                        break;
                                    case 12:
                                        resultItem.DecHours += 2;
                                        _pristineMonth[12] = false;
                                        break;
                                }
                            }

                            result.Add(resultItem);

                            Invoke((MethodInvoker)delegate { Text = "Дисциплины - " + (i + 1) + " / " + tfdList.Count; });
                        }


                        return result;
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

            Text = "Дисциплины - " + (resultView?.Count.ToString() ?? "");

            refresh.Image = null;
            refresh.Text = "Обновить";

            ByMonthView.DataSource = resultView;

            FormatView();

            ByMonthView.ClearSelection();
        }

        private void FormatView()
        {
            if (ByMonthView.DataSource == null)
            {
                return;
            }

            ByMonthView.Columns["DisciplineName"].HeaderText = "Наименование дисциплины";
            ByMonthView.Columns["DisciplineName"].Width = 300;

            ByMonthView.Columns["JanHours"].HeaderText = Constants.RuMonthNames[1];
            ByMonthView.Columns["FebHours"].HeaderText = Constants.RuMonthNames[2];
            ByMonthView.Columns["MarHours"].HeaderText = Constants.RuMonthNames[3];
            ByMonthView.Columns["AprHours"].HeaderText = Constants.RuMonthNames[4];
            ByMonthView.Columns["MayHours"].HeaderText = Constants.RuMonthNames[5];
            ByMonthView.Columns["JunHours"].HeaderText = Constants.RuMonthNames[6];
            ByMonthView.Columns["JulHours"].HeaderText = Constants.RuMonthNames[7];
            ByMonthView.Columns["AugHours"].HeaderText = Constants.RuMonthNames[8];
            ByMonthView.Columns["SepHours"].HeaderText = Constants.RuMonthNames[9];
            ByMonthView.Columns["OctHours"].HeaderText = Constants.RuMonthNames[10];
            ByMonthView.Columns["NovHours"].HeaderText = Constants.RuMonthNames[11];
            ByMonthView.Columns["DecHours"].HeaderText = Constants.RuMonthNames[12];

            ByMonthView.Columns["TfdId"].Visible = false;
            ByMonthView.Columns["JanHours"].Visible = !_pristineMonth[1];
            ByMonthView.Columns["FebHours"].Visible = !_pristineMonth[2];
            ByMonthView.Columns["MarHours"].Visible = !_pristineMonth[3];
            ByMonthView.Columns["AprHours"].Visible = !_pristineMonth[4];
            ByMonthView.Columns["MayHours"].Visible = !_pristineMonth[5];
            ByMonthView.Columns["JunHours"].Visible = !_pristineMonth[6];
            ByMonthView.Columns["JulHours"].Visible = !_pristineMonth[7];
            ByMonthView.Columns["AugHours"].Visible = !_pristineMonth[8];
            ByMonthView.Columns["SepHours"].Visible = !_pristineMonth[9];
            ByMonthView.Columns["OctHours"].Visible = !_pristineMonth[10];
            ByMonthView.Columns["NovHours"].Visible = !_pristineMonth[11];
            ByMonthView.Columns["DecHours"].Visible = !_pristineMonth[12];

            var colVisible = 0;
            for (var i = 1; i <= 12; i++)
            {
                if (!_pristineMonth[i])
                {
                    colVisible++;
                }
            }

            if (colVisible > 0)
            {
                var monthColWidth = (ByMonthView.Width - 380) / colVisible;

                ByMonthView.Columns["JanHours"].Width = monthColWidth;
                ByMonthView.Columns["FebHours"].Width = monthColWidth;
                ByMonthView.Columns["MarHours"].Width = monthColWidth;
                ByMonthView.Columns["AprHours"].Width = monthColWidth;
                ByMonthView.Columns["MayHours"].Width = monthColWidth;
                ByMonthView.Columns["JunHours"].Width = monthColWidth;
                ByMonthView.Columns["JulHours"].Width = monthColWidth;
                ByMonthView.Columns["AugHours"].Width = monthColWidth;
                ByMonthView.Columns["SepHours"].Width = monthColWidth;
                ByMonthView.Columns["OctHours"].Width = monthColWidth;
                ByMonthView.Columns["NovHours"].Width = monthColWidth;
                ByMonthView.Columns["DecHours"].Width = monthColWidth;
            }
        }

        private void TfdByMonth_Load(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();

            var teachersList = _repo.Teachers.GetAllTeachers().OrderBy(t => t.FIO).ToList();
            TeacherFIOFilter.ValueMember = "TeacherId";
            TeacherFIOFilter.DisplayMember = "FIO";
            TeacherFIOFilter.DataSource = teachersList;

            var studentGroups = _repo.StudentGroups.GetAllStudentGroups().OrderBy(sg => sg.Name).ToList();
            StudentGroupFilter.ValueMember = "StudentGroupId";
            StudentGroupFilter.DisplayMember = "Name";
            StudentGroupFilter.DataSource = studentGroups;
        }

        private void TfdByMonth_ResizeEnd(object sender, EventArgs e)
        {
            FormatView();
        }

        private void ByMonthView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            var tfdId = ((List<TfdItemByMonth>)ByMonthView.DataSource)[e.RowIndex].TfdId;

            var lessonsByTfd = new LessonListByTfd(_repo, tfdId);
            lessonsByTfd.Show();
        }
    }
}
