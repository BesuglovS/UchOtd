using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Properties;
using UchOtd.Views;

namespace UchOtd.Forms
{
    public partial class TeachersAddLessons : Form
    {
        public ScheduleRepository Repo { get; set; }

        public TeachersAddLessons(ScheduleRepository Repo)
        {
            this.Repo = Repo;
            InitializeComponent();
        }

        private async void refresh_Click(object sender, EventArgs e)
        {
            var teachersList = new List<TeacherWithLocation>();

            refresh.Text = "";
            refresh.Image = Resources.Loading;

            await Task.Run(() =>
            {
                var todaysLesson = Repo.Lessons.GetFiltredLessons(l => (l.State == 1) && (l.Calendar.Date.Date == DateTime.Today.Date));
                var todaysTeacherIds = (from t in todaysLesson where t.TeacherForDiscipline?.Teacher != null select t.TeacherForDiscipline.Teacher.TeacherId).ToList();

                var groupedLessons = new Dictionary<int, List<Lesson>>();
                for (int i = 0; i < todaysLesson.Count; i++)
                {
                    if (!groupedLessons.ContainsKey(todaysLesson[i].TeacherForDiscipline.Teacher.TeacherId))
                    {
                        groupedLessons.Add(todaysLesson[i].TeacherForDiscipline.Teacher.TeacherId, new List<Lesson>());
                    }

                    groupedLessons[todaysLesson[i].TeacherForDiscipline.Teacher.TeacherId].Add(todaysLesson[i]);
                }

                var discs = Repo.Disciplines.GetAllDisciplines();

                var discsLessons = new Dictionary<int, int>(); // DiscId + lessonCount
                var allLessons = Repo.Lessons.GetAllActiveLessons();
                for (int i = 0; i < allLessons.Count; i++)
                {
                    if (!discsLessons.ContainsKey(allLessons[i].TeacherForDiscipline.Discipline.DisciplineId))
                    {
                        discsLessons.Add(allLessons[i].TeacherForDiscipline.Discipline.DisciplineId, 0);
                    }

                    discsLessons[allLessons[i].TeacherForDiscipline.Discipline.DisciplineId]++;
                }

                var teachersIdList = new List<int>();

                for (int i = 0; i < discs.Count; i++)
                {
                    if (discs[i].AuditoriumHours == 0)
                    {
                        continue;
                    }

                    var teacher =
                        Repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(
                            tfd => tfd.Discipline.DisciplineId == discs[i].DisciplineId).Teacher;

                    if (teacher != null)
                    {
                        if (!discsLessons.ContainsKey(discs[i].DisciplineId) || (discs[i].AuditoriumHours/2 != discsLessons[discs[i].DisciplineId]))
                        {
                            if (todaysTeacherIds.Contains(teacher.TeacherId))
                            {
                                for (int j = 0; j < groupedLessons[teacher.TeacherId].Count; j++)
                                {
                                    teachersList.Add(new TeacherWithLocation(teacher.FIO,
                                        groupedLessons[teacher.TeacherId][j].Auditorium.Name,
                                        groupedLessons[teacher.TeacherId][j].Calendar.Date.ToString("dd.MM.yyyy"),
                                        groupedLessons[teacher.TeacherId][j].Ring.Time.ToString("H:mm")));
                                }
                            }
                        }
                    }
                }
            });

            teachersList = teachersList.OrderBy(tv => tv.Ring).ThenBy(tv => tv.Teacher).ToList();
            var res = teachersList
                .Select(tvl => new { tvl.Teacher, tvl.Calendar, tvl.Ring, tvl.Auditorium })
                .Distinct()
                .ToList();

            teachers.DataSource = res;

            refresh.Text = "Обновить";
            refresh.Image = null;
        }
    }
}
