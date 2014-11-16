using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Analyse;
using Schedule.Repositories;
using Schedule.Constants.Analysis;
using Schedule.Constants;
using Schedule.DomainClasses.Main;
using UchOtd.Analysis;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class AnalysisSchool : Form
    {
        public LogLevel CurrentLogLevel;
        // InitialLogLevel
        public LogLevel InitialLogLevel = LogLevel.Normal;

        public List<LogMessage> Log = new List<LogMessage>();
        
        private readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public AnalysisSchool(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;

            FillLogLevels();
        }

        private void FillLogLevels()
        {
            logLevel.DisplayMember = "Description";
            logLevel.ValueMember = "Level";
            logLevel.DataSource = Constants.LogLevels;
                        
            SetLogLevel(InitialLogLevel);
        }

        private void SetLogLevel(LogLevel level)
        {
            for(var i = 0; i < logLevel.Items.Count; i++)
            {
                if (((LogLevel)logLevel.Items[i]).Level == level.Level)
                {
                    logLevel.SelectedIndex = i;

                    return;
                }
            }
        }

        private void M(string messageText, LogLevel messageLogLevel)
        {
            var message = new LogMessage { Time = DateTime.Now, Level = messageLogLevel, Text = messageText };
            Log.Add(message);

            if (CurrentLogLevel.Level >= messageLogLevel.Level)
            {
                messages.BeginInvoke(new Action(() => messages.AppendText(message.Time.ToString("dd.MM.yyyy hh:mm:ss - ") +  messageText + "\r\n")));
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            RunAnalysis();
        }

        private void RunAnalysis()
        {
            start.Enabled = false;

            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }

            _tokenSource = new CancellationTokenSource();
            _cToken = _tokenSource.Token;
            
            Task.Factory.StartNew(() =>
            {
                var disciplineOrderAttributes = _repo
                    .CustomDisciplineAttributes
                    .GetFiltredCustomDisciplineAttributes(cda => cda.Key == "DisciplineOrder");

                if (disciplineOrderAttributes.Count == 0)
                {
                    M("ОШИБКА - Не определён порядок дисциплин.", LogLevel.ErrorsOnly);

                    start.Enabled = true;
                    return;
                }

                var disciplines = disciplineOrderAttributes
                    .OrderBy(cda => int.Parse(cda.Value))
                    .Select(cda => cda.Discipline)
                    .ToList();

                var dowCount = CountDow(_repo, true);

                M("", LogLevel.ErrorsOnly);
                M("Общее количество дисциплин - " + disciplines.Count, LogLevel.ErrorsOnly);
                M("", LogLevel.ErrorsOnly);
                
                for (int i = 0; i < disciplines.Count; i++)
                {
                    var discipline = disciplines[i];

                    var disciplineTfd =
                        _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discipline.DisciplineId);

                    if (disciplineTfd == null)
                    {
                        // Дисциплина не назначена преподавателю => пропускаем
                        M("~ \"" + discipline.Name + "\" " + discipline.StudentGroup.Name + " - нет преподавателя.", 
                            LogLevel.ErrorsAndWarnings);

                        continue;
                    }

                    var groupName = disciplineTfd.Discipline.StudentGroup.Name;

                    var lessonsInSchedule = _repo
                        .CommonFunctions
                        .GetTfdLessonCount(disciplineTfd.TeacherForDisciplineId);
                    var lessonsInPlan = (int)Math.Round((double)discipline.AuditoriumHours / 2);

                    var lessonsInSchedulePerWeekApproximation = HoursToPerWeek(lessonsInSchedule);
                    var planPerWeek = discipline.AuditoriumHoursPerWeek;

                    var lessonsLeftToSet = lessonsInPlan - lessonsInSchedule;
                    var lessonsLeftToSetPerWeek = planPerWeek - lessonsInSchedulePerWeekApproximation;

                    if (lessonsLeftToSetPerWeek < 0)
                    {
                        M("~ > \"" + discipline.Name + "\" " + " - " + groupName + " " + lessonsInSchedulePerWeekApproximation + " / " + planPerWeek, LogLevel.ErrorsAndWarnings);

                        continue;
                    }

                    if (Math.Abs(lessonsLeftToSetPerWeek) < 0.4)
                    {
                        M("~ = \"" + discipline.Name + "\" - " + groupName + " - " + lessonsInSchedulePerWeekApproximation, LogLevel.Max);

                        continue;
                    }

                    M("< \"" + discipline.Name + "\" - " + groupName + " " + planPerWeek + " / " + lessonsInSchedulePerWeekApproximation + " = " + planPerWeek, LogLevel.Normal);

                    var lessonsProposed = _repo
                        .Lessons
                        .GetFiltredLessons(l =>
                        l.TeacherForDiscipline.TeacherForDisciplineId == disciplineTfd.TeacherForDisciplineId &&
                        l.State == 2);
                    var lessonsProposedCount = lessonsProposed.Count;

                    var proposedDiff = lessonsLeftToSet - lessonsProposedCount;
                    var proposedDiffPerWeekApproximation = HoursToPerWeek(proposedDiff);

                    if (proposedDiffPerWeekApproximation < 0)
                    {
                        M("p > \"" + discipline.Name + "\" - " + groupName + " " + lessonsLeftToSet + " / " + lessonsProposedCount + " = " +
                          proposedDiff, LogLevel.ErrorsAndWarnings);

                        continue;
                    }

                    if (proposedDiff == 0)
                    {
                        M("p = \"" + discipline.Name + "\" - " + groupName, LogLevel.Max);

                        continue;
                    }

                    M("p < \"" + discipline.Name + "\" - " + groupName + " " + lessonsLeftToSet + " / " + lessonsProposedCount + " = " + proposedDiff, LogLevel.Normal);

                    // TODO:Поставить proposedDiffPerWeekApproximation занятий

                    // Находим звонки смены
                    var shiftAttribute = _repo
                        .CustomStudentGroupAttributes
                        .GetFirstFiltredCustomStudentGroupAttribute(csga => csga.Key == "Shift" && csga.StudentGroup.StudentGroupId == discipline.StudentGroup.StudentGroupId);
                    
                    if (shiftAttribute == null)
                    {
                        M("ОШИБКА - Для группы " + discipline.StudentGroup.Name + " не задана смена", LogLevel.ErrorsOnly);

                        start.Enabled = true;
                        return;
                    }

                    var groupShiftId = int.Parse(shiftAttribute.Value);

                    var groupShiftRings = _repo
                        .ShiftRings
                        .GetFiltredShiftRings(sr => sr.Shift.ShiftId == groupShiftId)
                        .Select(sr => sr.Ring)
                        .ToList();                    
                    // Находим звонки смены

                    if (groupShiftRings.Count == 0)
                    {
                        var shift = _repo.Shifts.GetShift(groupShiftId);
                        M("ОШИБКА - Для смены " + shift.Name + " не заданы времена начала занятий", LogLevel.ErrorsOnly);

                        start.Enabled = true;
                        return;
                    }


                    

                    if (_cToken.IsCancellationRequested)
                    {
                        M("Анализ отменён.", LogLevel.ErrorsOnly);
                        break;
                    }
                }

                start.BeginInvoke(new Action(() => { start.Enabled = true; }));

            }, _cToken);
        }

        private double HoursToPerWeek(int lessonsInSchedule)
        {
            return Math.Round((lessonsInSchedule * 2d) / 36d) / 2d;
        }

        private Dictionary<int, int> CountDow(ScheduleRepository repo, bool printOut)
        {
            var result = new Dictionary<int, int>();
            for (int i = 1; i <= 7; i++)
            {
                result.Add(i, 0);
            }

            foreach (var calendar in repo.Calendars.GetAllCalendars())
            {
                if (calendar.State == Calendar.Normal)
                {
                    result[Constants.DowRemap[(int) calendar.Date.DayOfWeek]]++;
                }
            }

            if (printOut)
            {
                for (int i = 1; i <= 7; i++)
                {
                    M(Constants.DowLocal[i] + " - " + result[i], LogLevel.ErrorsOnly);
                }
            }

            return result;
        }

        private void AnalysisSchool_Resize(object sender, EventArgs e)
        {
            AdjustButtonsSize();
        }

        private void AdjustButtonsSize()
        {
            var buttonWidth = (int) Math.Round((Width - 46f)/2);

            start.Left = 10;
            start.Width = buttonWidth;

            cancel.Left = 20 + buttonWidth;
            cancel.Width = buttonWidth;
        }

        private void AnalysisSchool_Load(object sender, EventArgs e)
        {
            AdjustButtonsSize();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }
        }
        
        private void logLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentLogLevel = (LogLevel)logLevel.SelectedItem;

            messages.Clear();

            foreach (var message in Log.OrderBy(m => m.Time))
            {
                if (CurrentLogLevel.Level >= message.Level.Level)
                {
                    messages.AppendText(message.Time.ToString("dd.MM.yyyy hh:mm:ss - ") + message.Text + "\r\n");
                }
            }
        }       
    }
}
