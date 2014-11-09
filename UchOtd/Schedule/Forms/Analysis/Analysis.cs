using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Constants;
using Schedule.Constants.Analysis;
using Schedule.Repositories;
using Schedule.DomainClasses.Main;
using UchOtd.Analysis;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class Analysis : Form
    {
        public LogLevel CurrentLogLevel;
        // InitialLogLevel
        public LogLevel InitialLogLevel = LogLevel.Normal;

        public List<LogMessage> log = new List<LogMessage>();
        
        private readonly ScheduleRepository _repo;

        CancellationTokenSource tokenSource;
        CancellationToken cToken;

        public Analysis(ScheduleRepository repo)
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

        private bool SetLogLevel(LogLevel LogLevel)
        {
            for(var i = 0; i < logLevel.Items.Count; i++)
            {
                if (((LogLevel)logLevel.Items[i]).Level == LogLevel.Level)
                {
                    logLevel.SelectedIndex = i;

                    return true;
                }
            }

            return false;
        }

        private void M(string messageText, LogLevel messageLogLevel)
        {
            var message = new LogMessage { Time = DateTime.Now, Level = messageLogLevel, Text = messageText };
            log.Add(message);

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

            if (tokenSource != null)
            {
                tokenSource.Cancel();
            }

            tokenSource = new CancellationTokenSource();
            cToken = tokenSource.Token;
            
            var analyseTask = Task.Factory.StartNew(() =>
            {
                var disciplineOrderAttributes = _repo
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

                var dowCount = CountDOW(_repo, true);

                M("", LogLevel.ErrorsOnly);
                M("Общее количество дисциплин - " + disciplines.Count, LogLevel.ErrorsOnly);
                M("", LogLevel.ErrorsOnly);
                
                for (int i = 0; i < disciplines.Count; i++)
                {
                    var discipline = disciplines[i];

                    var disciplineTFD =
                        _repo.GetFirstFiltredTeacherForDiscipline(
                            tfd => tfd.Discipline.DisciplineId == discipline.DisciplineId);

                    if (disciplineTFD == null)
                    {
                        // Дисциплина не назначена преподавателю => пропускаем
                        M("~ \"" + discipline.Name + "\" " + discipline.StudentGroup.Name + " - нет преподавателя.", 
                            LogLevel.ErrorsAndWarnings);

                        continue;
                    }

                    var groupName = disciplineTFD.Discipline.StudentGroup.Name;

                    var lessonsInSchedule = _repo.getTFDLessonCount(disciplineTFD.TeacherForDisciplineId);
                    var lessonsInPlan = (int)Math.Round((double)discipline.AuditoriumHours / 2);
                    var lessonsLeftToSet = lessonsInPlan - lessonsInSchedule;

                    if (lessonsLeftToSet < 0)
                    {
                        M("~ > \"" + discipline.Name + "\" " + " - " + groupName + " " + lessonsInSchedule + " / " + lessonsInPlan, LogLevel.ErrorsAndWarnings);

                        continue;
                    }

                    if (lessonsLeftToSet == 0)
                    {
                        M("~ = \"" + discipline.Name + "\" - " + groupName + " - " + lessonsInSchedule, LogLevel.Max);

                        continue;
                    }

                    M("< \"" + discipline.Name + "\" - " + groupName + " " + lessonsInPlan + " / " + lessonsInSchedule + " = " + lessonsLeftToSet, LogLevel.Normal);

                    var lessonsProposed = _repo.GetFiltredLessons(l =>
                        l.TeacherForDiscipline.TeacherForDisciplineId == disciplineTFD.TeacherForDisciplineId &&
                        l.State == 2);
                    var lessonsProposedCount = lessonsProposed.Count;

                    var proposedDiff = lessonsLeftToSet - lessonsProposedCount;

                    if (proposedDiff < 0)
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

                    // TODO:Поставить proposedDiff занятий



                    if (cToken.IsCancellationRequested)
                    {
                        M("Анализ отменён.", LogLevel.ErrorsOnly);
                        break;
                    }
                }

                start.BeginInvoke(new Action(() => { start.Enabled = true; }));

            }, cToken);
        }

        private Dictionary<int, int> CountDOW(ScheduleRepository repo, bool printOut)
        {
            var result = new Dictionary<int, int>();
            for (int i = 1; i <= 7; i++)
            {
                result.Add(i, 0);
            }

            foreach (var calendar in repo.GetAllCalendars())
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

        private void Analysis_Resize(object sender, EventArgs e)
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

        private void Analysis_Load(object sender, EventArgs e)
        {
            AdjustButtonsSize();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (tokenSource != null)
            {
                tokenSource.Cancel();
            }
        }

        private void logLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentLogLevel = (LogLevel)logLevel.SelectedItem;

            messages.Clear();

            foreach (var message in log.OrderBy(m => m.Time))
            {
                if (CurrentLogLevel.Level >= message.Level.Level)
                {
                    messages.AppendText(message.Time.ToString("dd.MM.yyyy hh:mm:ss - ") + message.Text + "\r\n");
                }
            }
        }
    }
}
