using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Core;
using UchOtd.NUDS.Core;
using UchOtd.NUDS.View;
using UchOtd.Properties;
using Utilities = UchOtd.NUDS.Core.Utilities;

namespace UchOtd.Forms
{
    public partial class TeacherSchedule : Form
    {
        private readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public TeacherSchedule(ScheduleRepository repo)
        {
            InitializeComponent();
            
            _repo = repo;

            weekFilter.Items.Clear();
            for (int i = 1; i <= 18; i++)
            {
                weekFilter.Items.Add(i);
            }
        }

        private void TeacherScheduleLoad(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();
            
            Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
            Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

            SetTeacherList();
        }

        private void SetTeacherList()
        {
            var tList = _repo.Teachers.GetAllTeachers().OrderBy(t => t.FIO).ToList();

            teacherList.DisplayMember = "FIO";
            teacherList.ValueMember = "TeacherId";
            teacherList.DataSource = tList;

        }
        
        public List<TeacherScheduleTimeView> GetTeacherScheduleToView(int teacherId, bool isWeekFiltered, int weekNumber, bool showingProposed, CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();
            
            var result = new List<TeacherScheduleTimeView>();

            
            List<Lesson> lessonList;
            if (isWeekFiltered)
            {
                lessonList = _repo
                    .Lessons
                    .GetFiltredLessons(l => 
                        l.TeacherForDiscipline.Teacher.TeacherId == teacherId &&
                        ((l.State == 1) || ((l.State == 2) && showingProposed)) && 
                        _repo.CommonFunctions.CalculateWeekNumber(l.Calendar.Date.Date) == weekNumber)
                    .ToList();
            }
            else
            {
                lessonList = _repo
                    .Lessons
                    .GetFiltredLessons(l => 
                        l.TeacherForDiscipline.Teacher.TeacherId == teacherId &&
                        ((l.State == 1) || ((l.State == 2) && showingProposed)))
                    .ToList();
            }

            var teacherBuildingsCount = lessonList.Select(l => l.Auditorium.Building.BuildingId).Distinct().Count();

            var lessonsGrouped = lessonList
                .GroupBy(l => l.Ring.Time)
                .ToDictionary(l => l.Key,
                    l2 => l2.GroupBy(l3 => Constants.DowEnToRu[(int) l3.Calendar.Date.DayOfWeek])
                        .ToDictionary(ll => ll.Key,
                            ll => ll.GroupBy(l4 => l4.TeacherForDiscipline.TeacherForDisciplineId)
                                .ToDictionary(l5 => l5.Key, l5 => l5.ToList())))
                .ToList();

            cToken.ThrowIfCancellationRequested();

            var semesterStartsOption = _repo.ConfigOptions.GetFirstFiltredConfigOption(co => co.Key == "Semester Starts");
            if (semesterStartsOption == null)
            {
                return result;
            }

            foreach (var time in lessonsGrouped.OrderBy(lg => lg.Key.TimeOfDay))
            {
                cToken.ThrowIfCancellationRequested();

                var tstv = new TeacherScheduleTimeView {Time = time.Key.ToString("H:mm")};

                foreach (var timeDowLessons in time.Value)
                {
                    var dow = timeDowLessons.Key;
                    string message = "";
                    int i = 0;
                    foreach (var tfdBundle in timeDowLessons.Value
                        .OrderBy(tfd => tfd.Value.Select(l => l.Calendar.Date).Min()))
                    {
                        message += tfdBundle.Value[0].TeacherForDiscipline.Discipline.StudentGroup.Name;
                        message += Environment.NewLine;
                        message += tfdBundle.Value[0].TeacherForDiscipline.Discipline.Name;
                        message += Environment.NewLine;
                        var semesterStartsDate = DateTime.ParseExact(semesterStartsOption.Value, "yyyy-MM-dd",
                            CultureInfo.InvariantCulture);
                        var weekList = tfdBundle.Value
                            .Select(l => Utilities.WeekFromDate(l.Calendar.Date, semesterStartsDate))
                            .ToList();
                        message += "(" + Utilities.GatherWeeksToString(weekList) + ")";
                        message += Environment.NewLine;
                        var audWeeks = new Dictionary<Auditorium, List<int>>();
                        foreach (var lesson in tfdBundle.Value)
                        {
                            if (!audWeeks.ContainsKey(lesson.Auditorium))
                            {
                                audWeeks.Add(lesson.Auditorium, new List<int>());
                            }

                            audWeeks[lesson.Auditorium].Add(Utilities.WeekFromDate(lesson.Calendar.Date, semesterStartsDate));
                        }
                        var sortedWeeks = audWeeks.OrderBy(aw => aw.Value.Min());
                        if (sortedWeeks.Count() == 1)
                        {
                            var aud = audWeeks.ElementAt(0).Key;
                            message += aud.Name;
                            if (teacherBuildingsCount != 1)
                            {
                                message += " (" + aud.Building.Name + ")";
                            }
                        }
                        else
                        {
                            message = sortedWeeks
                                .Aggregate(message, (current, kvp) =>
                                    current +
                                    (Utilities.GatherWeeksToString(kvp.Value) + " - " + kvp.Key.Name +
                                    ((teacherBuildingsCount != 1) ? (" (" + kvp.Key.Building.Name + ")") : "") +
                                     Environment.NewLine));
                        }

                        if (i != timeDowLessons.Value.Count - 1)
                        {
                            message += Environment.NewLine + Environment.NewLine;
                        }

                        i++;
                    }

                    while (message.EndsWith(Environment.NewLine))
                    {
                        message = message.Substring(0, message.Length - Environment.NewLine.Length);
                    }

                    switch (dow)
                    {
                        case 1:
                            tstv.MonLessons = message;
                            break;
                        case 2:
                            tstv.TueLessons = message;
                            break;
                        case 3:
                            tstv.WedLessons = message;
                            break;
                        case 4:
                            tstv.ThuLessons = message;
                            break;
                        case 5:
                            tstv.FriLessons = message;
                            break;
                        case 6:
                            tstv.SatLessons = message;
                            break;
                        case 7:
                            tstv.SunLessons = message;
                            break;
                    }
                }

                result.Add(tstv);
            }

            return result;
        }

        private void FormatView()
        {
            if ((scheduleView.DataSource == null) ||
                (scheduleView.DataSource != null &&
                 ((List<TeacherScheduleTimeView>)(scheduleView.DataSource)).Count == 0))
            {
                return;
            }

            scheduleView.RowHeadersVisible = false;
            scheduleView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            scheduleView.AutoResizeColumns();
            scheduleView.AutoResizeRows();
            scheduleView.AllowUserToResizeColumns = false;
            scheduleView.AllowUserToResizeRows = false;

            // Time
            scheduleView.Columns["Time"].HeaderText = Resources.TeacherSchedule_FormatView_Time;
            scheduleView.Columns["Time"].Width = 56;
            scheduleView.Columns["Time"].DefaultCellStyle.Font = new Font(scheduleView.DefaultCellStyle.Font.FontFamily, 14);
            scheduleView.Columns["Time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var source = (List<TeacherScheduleTimeView>)scheduleView.DataSource;
            var emptyColumn = new Dictionary<int, bool>();
            for (int i = 1; i <= 7; i++)
            {
                emptyColumn.Add(i, true);
                foreach (TeacherScheduleTimeView time in source)
                {
                    switch (i)
                    {
                        case 1:
                            if (time.MonLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 2:
                            if (time.TueLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 3:
                            if (time.WedLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 4:
                            if (time.ThuLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 5:
                            if (time.FriLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 6:
                            if (time.SatLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 7:
                            if (time.SunLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                    }
                }
            }
            var notEmptyDowColumnCount = 7 - emptyColumn.Count(e => e.Value);

            var emptyColumnIndexes = emptyColumn.Where(e => e.Value).Select(e => e.Key).ToList();

            for (var i = 1; i <= 7; i++)
            {
                scheduleView.Columns[i].Visible = !emptyColumnIndexes.Contains(i);
            }

            // Lessons
            for (int i = 1; i <= 7; i++)
            {
                scheduleView.Columns[i].Width = (scheduleView.Width - 76) / notEmptyDowColumnCount;
                scheduleView.Columns[i].HeaderText = Constants.DowRu[i];
            }

        }

        private void TeacherScheduleResize(object sender, EventArgs e)
        {
            FormatView();
        }

        private void ScheduleViewSelectionChanged(object sender, EventArgs e)
        {
            scheduleView.ClearSelection();
        }

        private void teacherList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private async void refresh_Click(object sender, EventArgs e)
        {
            List<TeacherScheduleTimeView> result = null;

            if (refresh.Text == "Обновить")
            {
                _cToken = _tokenSource.Token;

                refresh.Image = Resources.Loading;
                refresh.Text = "";

                var teacherId = (int) teacherList.SelectedValue;
                var isWeekFiltered = weekFiltered.Checked;
                int weekNum = -1;
                if (isWeekFiltered)
                {
                    int.TryParse(weekFilter.Text, out weekNum);
                }
                var isShowProposed = showProposed.Checked;

                try
                {
                    result = await Task.Run(() =>
                        GetTeacherScheduleToView(teacherId, isWeekFiltered, weekNum, isShowProposed, _cToken),
                        _cToken);
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
            

            if (result != null)
            {
                scheduleView.DataSource = result;

                FormatView();
            }
        }

        private async void WordExport_Click(object sender, EventArgs e)
        {
            if (ExportInWordPortrait.Text == "Word (портретная)")
            {
                _cToken = _tokenSource.Token;

                ExportInWordPortrait.Text = "";
                ExportInWordPortrait.Image = Resources.Loading;

                var isWeekFiltered = weekFiltered.Checked;
                int weekNum = -1;
                if (isWeekFiltered)
                {
                    int.TryParse(weekFilter.Text, out weekNum);
                }
                var isShowProposed = showProposed.Checked;

                try
                {
                    var teacherId = (int)teacherList.SelectedValue;

                    var result = await Task.Run(() => 
                        GetTeacherScheduleToView(teacherId, isWeekFiltered, weekNum, isShowProposed, _cToken),
                        _cToken);

                    var teacher = _repo.Teachers.GetTeacher((int)(teacherList.SelectedValue));

                    WordExport.TeacherSchedule(result, teacher, false, _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            ExportInWordPortrait.Image = null;
            ExportInWordPortrait.Text = "Word (портретная)";
        }

        private async void ExportInWordLandscape_Click(object sender, EventArgs e)
        {
            if (ExportInWordLandscape.Text == "Word (ландшафтная)")
            {
                _cToken = _tokenSource.Token;

                ExportInWordLandscape.Text = "";
                ExportInWordLandscape.Image = Resources.Loading;

                var isWeekFiltered = weekFiltered.Checked;
                int weekNum = -1;
                if (isWeekFiltered)
                {
                    int.TryParse(weekFilter.Text, out weekNum);
                }
                var isShowProposed = showProposed.Checked;
                
                try
                {
                    var teacherId = (int)teacherList.SelectedValue;

                    var result = await Task.Run(() => 
                        GetTeacherScheduleToView(teacherId, isWeekFiltered, weekNum, isShowProposed, _cToken),
                        _cToken);

                    var teacher = _repo.Teachers.GetTeacher((int)(teacherList.SelectedValue));

                    WordExport.TeacherSchedule(result, teacher, true, _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            ExportInWordLandscape.Image = null;
            ExportInWordLandscape.Text = "Word (ландшафтная)";
        }

        private async void ExportAllTeachersInWord_Click(object sender, EventArgs e)
        {
            if (ExportAllTeachersInWord.Text == "Word (все преподаватели)")
            {
                _cToken = _tokenSource.Token;
                var repo = _repo;

                ExportAllTeachersInWord.Text = "";
                ExportAllTeachersInWord.Image = Resources.Loading;
                
                try
                {
                    await Task.Run(() => WordExport.TeachersSchedule(repo, this, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            ExportAllTeachersInWord.Image = null;
            ExportAllTeachersInWord.Text = "Word (все преподаватели)";
        }
    }
}
