using Schedule.Constants;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Repositories.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UchOtd.Core;
using UchOtd.NUDS.Core;

namespace UchOtd.Forms
{    
    public partial class AuditoriumCollisionsLog : Form
    {
        ScheduleRepository _repo;

        public static Dictionary<int, string> DowLocal = new Dictionary<int, string> {
            { 1, "Понедельник" },
            { 2, "Вторник" },
            { 3, "Среда" },
            { 4, "Четверг" },
            { 5, "Пятница" },
            { 6, "Суббота" },
            { 7, "Воскресенье" }
        };

        public AuditoriumCollisionsLog(ScheduleRepository repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        private void AuditoriumCollisionsLog_Load(object sender, EventArgs e)
        {
            var buildings = _repo.Buildings.GetAllBuildings();

            BuildingsListbox.DisplayMember = "Name";
            BuildingsListbox.ValueMember = "BuildingId";
            BuildingsListbox.DataSource = buildings;

            var dows = new List<dowItem>();
            var dowKeys = DowLocal.Keys.OrderBy(k => k).ToList();
            for (int i = 0; i < dowKeys.Count; i++)
            {
                dows.Add(new dowItem { Id = dowKeys[i], LocalName = DowLocal[dowKeys[i]]});
            }
            
            DowListbox.DisplayMember = "LocalName";
            DowListbox.ValueMember = "Id";
            DowListbox.DataSource = dows;
        }

        private async void Run_Click(object sender, EventArgs e)
        {
            await Task.Run(() => { 
                RunCheck();
            });
        }

        private void RunCheck()
        {
            var LogFilename = "AuditoriumCollisions.txt";
            var buildingIds = new List<int>();
            foreach (var item in BuildingsListbox.SelectedItems)
            {
                buildingIds.Add(((Building)item).BuildingId);
            }

            var auditoriumIds = _repo.Auditoriums.GetAll()
                .Where(a => buildingIds.Contains(a.Building.BuildingId))
                .ToList();

            var dows = new List<int>();
            foreach (var item in DowListbox.SelectedItems)
            {
                dows.Add(((dowItem)item).Id);
            }
            dows = dows.OrderBy(dw => dw).ToList();

            var weeks = new List<int>();
            NUDS.Core.Utilities.getWeeksFromString(out weeks, WeeksTextbox.Text);
            weeks = weeks.OrderBy(w => w).ToList();

            var pairs = new List<Tuple<Lesson, Lesson>>();

            for (int i = 0; i < weeks.Count; i++)
            {
                var week = weeks[i];

                for (int j = 0; j < dows.Count; j++)
                {
                    var dow = dows[j];

                    Invoke((MethodInvoker)delegate
                    {
                        StatusLabel.Text = "Неделя " + week + " День недели " + dow;
                    });

                    var ssMonday = CommonFunctions.GetMonday(_repo.CommonFunctions.GetSemesterStarts());
                    var date = ssMonday.AddDays((week - 1) * 7 + (dow - 1));

                    var calendar = _repo.Calendars.GetAllCalendars()
                        .FirstOrDefault(c =>
                            c.Date.Year == date.Year &&
                            c.Date.Month == date.Month &&
                            c.Date.Day == date.Day);
                    if (calendar != null)
                    {
                        var lessonsByAuditorium = _repo.Lessons.GetFiltredLessons(l =>
                            l.State == 1 &&
                            l.Calendar.CalendarId == calendar.CalendarId &&
                            buildingIds.Contains(l.Auditorium.Building.BuildingId))
                            .GroupBy(l => l.Auditorium.AuditoriumId)
                            .ToDictionary(l => l.Key, l => l.ToList());

                        foreach (var alPair in lessonsByAuditorium)
                        {
                            var audId = alPair.Key;
                            var audLessons = alPair.Value;

                            for (int k = 0; k < audLessons.Count - 2; k++)
                            {
                                for (int l = k + 1; l < audLessons.Count - 1; l++)
                                {
                                    var l1 = audLessons[k];
                                    var l2 = audLessons[l];

                                    var Groups40 = new List<string> { "1", "2", "3", "4", "5", "6", "7" };

                                    var l1GroupStart = l1.TeacherForDiscipline.Discipline.StudentGroup.Name.Split(' ')[0];
                                    var lesson1Length = 80;
                                    if (Groups40.Contains(l1GroupStart))
                                    {
                                        lesson1Length = 40;
                                    }

                                    var l2GroupStart = l2.TeacherForDiscipline.Discipline.StudentGroup.Name.Split(' ')[0];
                                    var lesson2Length = 80;
                                    if (Groups40.Contains(l2GroupStart))
                                    {
                                        lesson2Length = 40;
                                    }

                                    var time1Start = l1.Ring.Time.TimeOfDay;
                                    var time1End = time1Start.Add(new TimeSpan(0, 0, lesson1Length, 0));
                                    var time2Start = l2.Ring.Time.TimeOfDay;
                                    var time2End = time2Start.Add(new TimeSpan(0, 0, lesson2Length, 0));

                                    if (time1Start < time2End && time2Start < time1End)
                                    {
                                        pairs.Add(Tuple.Create(l1, l2));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var audDictionary = _repo.Auditoriums.GetAll().ToDictionary(a => a.AuditoriumId, a => a);

            TextFileUtilities.CreateOrEmptyFile(LogFilename);
            for (int i = 0; i < pairs.Count; i++)
            {
                var pair = pairs[i];
                var l1 = pair.Item1;
                var l2 = pair.Item2;

                TextFileUtilities.WriteStringList(LogFilename, new List<string>
                {
                    l1.Auditorium.Name,
                    l1.Calendar.Date.ToString("dd.MM.yyyy") + " --- " +
                        DowLocal[DOW(l1.Calendar.Date)] +
                        " (" + _repo.CommonFunctions.CalculateWeekNumber(l1.Calendar.Date) +")",
                    l1.Ring.Time.ToString("HH:mm") + " " +
                        l1.TeacherForDiscipline.Discipline.StudentGroup.Name + " " +
                        l1.TeacherForDiscipline.Discipline.Name + " " +
                        l1.TeacherForDiscipline.Teacher.FIO,
                    l2.Ring.Time.ToString("HH:mm") + " " +
                        l2.TeacherForDiscipline.Discipline.StudentGroup.Name + " " +
                        l2.TeacherForDiscipline.Discipline.Name + " " +
                        l2.TeacherForDiscipline.Teacher.FIO,
                    ""
                });
            }
        }

        public int DOW(DateTime dt)
        {
            var dow = dt.DayOfWeek;
            switch (dow)
            {
                case DayOfWeek.Sunday:
                    return 7;
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                default:
                    return -1;
            }
        }
    }    

    public class dowItem
    {
        public int Id { get; set; }
        public string LocalName { get; set; }
    }
}
