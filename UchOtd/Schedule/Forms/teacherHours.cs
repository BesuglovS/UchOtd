using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Repositories;
using UchOtd.Properties;
using UchOtd.Schedule.Forms.DBLists.Lessons;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms
{
    public partial class TeacherHours : Form
    {
        private readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;
        
        public TeacherHours(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void teacherHours_Load(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();

            var teachers = _repo
                .Teachers
                .GetAllTeachers()
                .OrderBy(t => t.FIO)
                .ToList();

            teachersList.DisplayMember = "FIO";
            teachersList.ValueMember = "TeacherId";
            teachersList.DataSource = teachers;
        }

        private void teachersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RefreshView();
        }
        
        private void FormatView()
        {
            view.Columns["TfdId"].Visible = false;

            view.Columns["DisciplineId"].Visible = false;

            view.Columns["DisciplineName"].HeaderText = "Дисциплина";
            view.Columns["DisciplineName"].Width = 200;

            view.Columns["GroupName"].HeaderText = "Группа";
            view.Columns["GroupName"].Width = 80;

            view.Columns["PlanHours"].HeaderText = "Часы по плану";
            view.Columns["PlanHours"].Width = 80;

            view.Columns["ScheduleHours"].HeaderText = "Часы в расписании";
            view.Columns["ScheduleHours"].Width = 80;

            view.Columns["HoursDone"].HeaderText = "Выполнено на данный момент";
            view.Columns["HoursDone"].Width = 80;

            view.Columns["PlannedHours"].HeaderText = "Предполагаемые часы";
            view.Columns["PlannedHours"].Width = 80;

            view.Columns["Attestation"].HeaderText = "Форма отчётности";
            view.Columns["Attestation"].Width = 80;
        }
        
        private Color PickPercentColor(int planHours, int scheduleHours)
        {
            if (scheduleHours > planHours + 1)
            {
                return Color.FromArgb(255, 0, 255);
            }

            if (scheduleHours == planHours + 1)
            {
                return Color.FromArgb(200, 255, 0);
            }

            if (scheduleHours == planHours)
            {
                return Color.FromArgb(0, 255, 0);
            }

            if (scheduleHours >= planHours * 0.9)
            {
                return Color.FromArgb(255, 255, 0);
            }

            if (scheduleHours >= planHours * 0.5)
            {
                return Color.FromArgb(255, 128, 0);
            }

            return Color.FromArgb(255, 0, 0);
        }

        private void view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                var discView = ((List<TeacherForDisciplineView>)view.DataSource)[e.RowIndex];

                e.CellStyle.BackColor = PickPercentColor(discView.PlanHours, discView.ScheduleHours);
            }
        }

        private void view_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var discId = ((List<TeacherForDisciplineView>)view.DataSource)[e.RowIndex].DisciplineId;
            var disc = _repo.Disciplines.GetFirstFiltredDisciplines(d => d.DisciplineId == discId);
            var tefd = _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discId);
            if (tefd != null)
            {
                var addLessonForm = new AddLesson(_repo, tefd.TeacherForDisciplineId, disc.Semester);
                addLessonForm.Show();
            }
            else
            {
                var addLessonForm = new AddLesson(_repo);
                addLessonForm.Show();
            }
        }

        private bool getWeekFilter(ComboBox weekList, out List<int> weekFilterList)
        {
            var text = weekList.Text;
            weekFilterList = new List<int>();
            try
            {
                if (!text.Contains("-"))
                {
                    weekFilterList.Add(int.Parse(text));
                }
                else
                {
                    var split = text.Split('-');
                    var start = int.Parse(split[0]);
                    var finish = int.Parse(split[1]);
                    for (int i = start; i <= finish; i++)
                    {
                        weekFilterList.Add(i);
                    }
                }
            }
            catch (Exception exception)
            {
                return true;
            }
            return false;
        }

        private async void update_Click(object sender, EventArgs e)
        {

            List<int> weekFilterList = null;
            var ishoursWeekFiltered = hoursWeekFiltered.Checked;
            if (ishoursWeekFiltered)
            {
                if (getWeekFilter(HoursWeekFilter, out weekFilterList)) return;
            }

            List<TeacherForDisciplineView> tfdInfo = null;

            if (update.Text == "Обновить")
            {
                _cToken = _tokenSource.Token;

                update.Text = "";
                update.Image = Resources.Loading;

                var teacherId = (int)teachersList.SelectedValue;

                try
                {
                    tfdInfo = await Task.Run(() =>
                    {
                        var tfds = _repo.TeacherForDisciplines
                        .GetFiltredTeacherForDiscipline(tfd => tfd.Teacher.TeacherId == teacherId);

                        return TeacherForDisciplineView.FromTfdList(tfds, _repo, ishoursWeekFiltered, weekFilterList);
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
            update.Text = "Обновить";

            if (tfdInfo != null)
            {
                view.DataSource = tfdInfo;

                FormatView();
            }
        }
    }
}
