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
using Schedule.Repositories;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class AnalysisSchool : Form
    {
        private readonly ScheduleRepository _repo;

        CancellationTokenSource tokenSource;
        CancellationToken cToken;

        public AnalysisSchool(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void M(string messageLine)
        {
            messages.BeginInvoke(new Action(() => messages.AppendText(messageLine + "\r\n")));
        }

        private void RunAnalysis()
        {
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
                    M("ОШИБКА - Не определён порядок дисциплин.");

                    return;
                }

                var disciplines = disciplineOrderAttributes
                    .OrderBy(cda => int.Parse(cda.Value))
                    .Select(cda => cda.Discipline)
                    .ToList();

                M("Общее количество дисциплин - " + disciplines.Count);
                M("=================================");


                for (int i = 0; i < disciplines.Count; i++)
                {
                    var discipline = disciplines[i];

                    var disciplineTFD =
                        _repo.GetFirstFiltredTeacherForDiscipline(
                            tfd => tfd.Discipline.DisciplineId == discipline.DisciplineId);

                    if (disciplineTFD == null)
                    {
                        // Дисциплина не назначена преподавателю => пропускаем
                        M("~ \"" + discipline.Name + "\" - нет преподавателя.");

                        continue;
                    }

                    var lessonsInSchedule = _repo.getTFDLessonCount(disciplineTFD.TeacherForDisciplineId);
                    var lessonsInPlan = (int)Math.Round((double)discipline.AuditoriumHours / 2);
                    var lessonsLeftToSet = lessonsInPlan - lessonsInSchedule;

                    if (lessonsLeftToSet < 0)
                    {
                        M("~ > \"" + discipline.Name + "\" " + lessonsInSchedule + " / " + lessonsInPlan);

                        continue;
                    }

                    if (lessonsLeftToSet == 0)
                    {
                        M("~ = \"" + discipline.Name + "\" - " + lessonsInSchedule);

                        continue;
                    }

                    M("< \"" + discipline.Name + "\" " + lessonsInPlan + " / " + lessonsInSchedule + " = " + lessonsLeftToSet);

                    var lessonsProposed = _repo.GetFiltredLessons(l =>
                        l.TeacherForDiscipline.TeacherForDisciplineId == disciplineTFD.TeacherForDisciplineId &&
                        l.State == 2);
                    var lessonsProposedCount = lessonsProposed.Count;

                    var proposedDiff = lessonsLeftToSet - lessonsProposedCount;

                    if (proposedDiff < 0)
                    {
                        M("p > \"" + discipline.Name + "\" " + lessonsLeftToSet + " / " + lessonsProposedCount + " = " +
                          proposedDiff);

                        continue;
                    }

                    if (proposedDiff == 0)
                    {
                        M("p = \"" + discipline.Name + "\"");

                        continue;
                    }

                    M("p < \"" + discipline.Name + "\" " + lessonsLeftToSet + " / " + lessonsProposedCount + " = " + proposedDiff);

                    // Поставить proposedDiff занятий



                    if (cToken.IsCancellationRequested)
                    {
                        M("Анализ отменён.");
                        break;
                    }
                }
            }, cToken);
        }

        private void start_Click(object sender, EventArgs e)
        {
            RunAnalysis();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (tokenSource != null)
            {
                tokenSource.Cancel();
            }
        }

        private void AnalysisSchool_Load(object sender, EventArgs e)
        {
            AdjustButtonsSize();
        }

        private void AnalysisSchool_Resize(object sender, EventArgs e)
        {
            AdjustButtonsSize();
        }

        private void AdjustButtonsSize()
        {
            var buttonWidth = (int)Math.Round((Width - 46f) / 2);

            start.Left = 10;
            start.Width = buttonWidth;

            cancel.Left = 20 + buttonWidth;
            cancel.Width = buttonWidth;
        }
    }
}
