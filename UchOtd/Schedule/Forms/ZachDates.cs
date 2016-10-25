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

namespace UchOtd.Schedule.Forms
{
    public partial class ZachDates : Form
    {
        private readonly ScheduleRepository repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public ZachDates(ScheduleRepository repo)
        {
            this.repo = repo;
            InitializeComponent();

            var groups = repo.StudentGroups.GetAllStudentGroups().OrderBy(sg => sg.Name).ToList();

            groupList.ValueMember = "StudentGroupId";
            groupList.DisplayMember = "Name";
            groupList.DataSource = groups;
        }

        private List<int> StudentGroupIdsFromGroupId(int groupId)
        {
            var studentIds = repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId && !sig.Student.Expelled)
                .Select(stig => stig.Student.StudentId)
                .ToList();

            var groupsListIds = repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                .Select(stig => stig.StudentGroup.StudentGroupId)
                .Distinct()
                .ToList();
            return groupsListIds;
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private async void RefreshView()
        {
            _tokenSource = new CancellationTokenSource();

            if (refresh.Text == "Обновить")
            {
                refresh.Text = "";
                refresh.Image = Resources.Loading;

                _cToken = _tokenSource.Token;

                var groupFilter = groupFiltered.Checked;
                var selectedGroupId = (int)groupList.SelectedValue;

                var dateFilter = dateFiltered.Checked;
                var dateValue = filterDate.Value;

                try
                {
                    var datesView = await Task.Run(() =>
                    {
                        var groupIds = StudentGroupIdsFromGroupId(selectedGroupId);

                        var disciplines = repo.Disciplines
                            .GetFiltredDisciplines(d =>
                                d.Attestation == 1 || d.Attestation == 3 || d.Attestation == 4)
                            .ToList();

                        if (groupFilter)
                        {
                            disciplines =
                                disciplines.Where(d => groupIds.Contains(d.StudentGroup.StudentGroupId)).ToList();
                        }

                        var result = new List<ZachDate>();

                        for (int index = 0; index < disciplines.Count; index++)
                        {
                            var discipline = disciplines[index];
                            var lessons =
                                repo.Lessons.GetFiltredLessons(
                                    l =>
                                        l.TeacherForDiscipline.Discipline.DisciplineId == discipline.DisciplineId &&
                                        l.State == 1)
                                    .OrderBy(l => l.Calendar.Date)
                                    .ThenBy(l => l.Ring.Time.TimeOfDay)
                                    .ToList();

                            var zd = new ZachDate
                            {
                                GroupId = discipline.StudentGroup.StudentGroupId,
                                GroupName = discipline.StudentGroup.Name,
                                DisciplineName = discipline.Name,
                                dtDate = lessons.Count != 0 ? lessons.Last().Calendar.Date : new DateTime(2100, 1, 1),
                                Date = lessons.Count != 0 ? lessons.Last().Calendar.Date.ToString("dd.MM.yyyy") : "",
                                ScheduleCompleted = lessons.Count*2 == discipline.AuditoriumHours
                            };

                            result.Add(zd);

                            statusStrip.Invoke((MethodInvoker)(() => status.Text = (index+1) + " / " + disciplines.Count));
                        }

                        if (dateFilter)
                        {
                            result = result.Where(z =>
                                z.dtDate.Year == dateValue.Year &&
                                z.dtDate.Month == dateValue.Month &&
                                z.dtDate.Day == dateValue.Day).ToList();
                        }

                        result = result.OrderBy(z => z.dtDate).ToList();

                        return result;
                    }, _cToken);

                    ZachDatesView.DataSource = datesView;

                    refresh.Image = null;
                    refresh.Text = "Обновить";
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            FormatView();
        }

        private void FormatView()
        {
            try
            {
                ZachDatesView.Columns["GroupId"].Visible = false;

                ZachDatesView.Columns["GroupName"].HeaderText = "Группа";
                ZachDatesView.Columns["GroupName"].Width = 100;

                ZachDatesView.Columns["DisciplineName"].HeaderText = "Наименование дисциплины";
                ZachDatesView.Columns["DisciplineName"].Width = ZachDatesView.Width - 360;

                ZachDatesView.Columns["dtDate"].Visible = false;

                ZachDatesView.Columns["Date"].HeaderText = "Дата последней пары / зачёта";
                ZachDatesView.Columns["Date"].Width = 100;

                ZachDatesView.Columns["ScheduleCompleted"].HeaderText = "Все ли пары стоят в расписании";
                ZachDatesView.Columns["ScheduleCompleted"].Width = 100;
            }
            catch 
            {
                
            }
        }

        private void ZachDates_ResizeEnd(object sender, EventArgs e)
        {
            FormatView();
        }

        private void ZachDates_Load(object sender, EventArgs e)
        {
            filterDate.Value = DateTime.Now;
        }
    }
}
