using System.Threading.Tasks;
using Schedule.Constants;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using UchOtd.Core;
using System.Threading;

namespace UchOtd.Forms
{
    public partial class BuildingLessons : Form
    {
        readonly ScheduleRepository _repo;

        private readonly TaskScheduler _uiScheduler;
        private static int counter = 0;

        CancellationTokenSource tokenSource;
        CancellationToken cToken;

        public BuildingLessons(ScheduleRepository repo)
        {
            InitializeComponent();

            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            _repo = repo;
        }

        private void BuildingLessonsLoad(object sender, EventArgs e)
        {
            building.DisplayMember = "Name";
            building.ValueMember = "id";
            building.DataSource = Constants.Buildings;

            var initialCalendar = _repo.GetFirstFiltredCalendar(c => c.Date.Date == DateTime.Now.Date);
            if (initialCalendar == null)
            {
                var ss = _repo.GetFirstFiltredConfigOption(co => co.Key == "Semester Starts");
                initialCalendar = _repo.GetFirstFiltredCalendar(c => c.Date == DateTime.ParseExact(ss.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            }

            lessonsDate.Value = initialCalendar.Date;
        }

        private void BuildingSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBuildingAuditoriums();
        }

        private void DateValueChanged(object sender, EventArgs e)
        {
            UpdateBuildingAuditoriums();
        }

        private void UpdateBuildingAuditoriums()
        {
            if (tokenSource != null)
            {
                tokenSource.Cancel();
            }

            tokenSource = new CancellationTokenSource();
            cToken = tokenSource.Token;

            counter++;

            if ((building.SelectedIndex == -1) || (lessonsDate.Value == new DateTime(1985, 4, 4)))
            {
                return;
            }

            BuildingLessonsData data = new BuildingLessonsData();

            loadingLabel.Visible = true;

            var buildingId = (int)building.SelectedValue;
            var viewDate = lessonsDate.Value.Date;

            var calculateAudsTask = Task.Factory.StartNew(() =>
            {
                var lessons = _repo
                    .GetFiltredLessons(l =>
                        l.IsActive &&
                        AuditoriumBuilding(l.Auditorium.Name) == buildingId &&
                        l.Calendar.Date.Date == viewDate);
                cToken.ThrowIfCancellationRequested();

                var evts = _repo
                    .GetFiltredAuditoriumEvents(ae =>
                        AuditoriumBuilding(ae.Auditorium.Name) == buildingId &&
                        ae.Calendar.Date.Date == viewDate
                    );
                cToken.ThrowIfCancellationRequested();

                var evtsRings = evts
                    .Select(e => e.Ring)
                    .Distinct()
                    .OrderBy(r => r.Time.TimeOfDay)
                    .ToList();

                data.rings = lessons
                    .Select(l => l.Ring)
                    .Concat(evtsRings)                    
                    .OrderBy(r => r.Time.TimeOfDay)
                    .ToList()
                    .DistinctBy(r => r.RingId)
                    .ToList();

                var auditoriums = lessons
                    .Select(l => l.Auditorium)
                    .Distinct()
                    .ToList();
                var evtsAuditoriums = evts
                    .Select(e => e.Auditorium)
                    .Distinct()
                    .ToList();

                var totalAuds = auditoriums
                    .Concat(evtsAuditoriums)                    
                    .ToList();

                totalAuds = totalAuds
                    .DistinctBy(a => a.AuditoriumId)
                    .ToList();

                data.sortedAuditoriums = SortAuditoriums(totalAuds, buildingId);

                data.buildingAuditoriums = new Dictionary<int, Dictionary<int, string>>();
                // Занятия
                foreach (Ring ring in data.rings)
                {
                    cToken.ThrowIfCancellationRequested();
                    data.buildingAuditoriums.Add(ring.RingId, new Dictionary<int, string>());

                    foreach (Auditorium aud in data.sortedAuditoriums)
                    {
                        var ll = lessons
                            .Where(l => l.Ring.RingId == ring.RingId &&
                                        l.Auditorium.AuditoriumId == aud.AuditoriumId)
                            .ToList();
                        if (ll.Any())
                        {
                            if (data.buildingAuditoriums[ring.RingId].ContainsKey(aud.AuditoriumId))
                            {
                                data.buildingAuditoriums[ring.RingId][aud.AuditoriumId] += Environment.NewLine + LessonToString(ll[0]);
                            }
                            else
                            {
                                data.buildingAuditoriums[ring.RingId].Add(aud.AuditoriumId, LessonToString(ll[0]));
                            }
                        }

                        var curEvents = evts
                            .Where(e => e.Ring.RingId == ring.RingId &&
                                        e.Auditorium.AuditoriumId == aud.AuditoriumId)
                            .ToList();

                        if (curEvents.Any())
                        {
                            if (!data.buildingAuditoriums[ring.RingId].ContainsKey(aud.AuditoriumId))
                            {
                                data.buildingAuditoriums[ring.RingId].Add(aud.AuditoriumId, "");
                            }

                            if (data.buildingAuditoriums[ring.RingId][aud.AuditoriumId] != "")
                            {
                                data.buildingAuditoriums[ring.RingId][aud.AuditoriumId] += Environment.NewLine;
                            }
                            data.buildingAuditoriums[ring.RingId][aud.AuditoriumId] += curEvents[0].Name;
                        }

                    }
                }

                return data;
            }, cToken);

            if (calculateAudsTask.Status == TaskStatus.Canceled)
            {
                return;
            }

            calculateAudsTask.ContinueWith(
                antecedent =>
                {
                    var auditoriumsData = antecedent.Result;
                    auditoriumsView.RowCount = data.rings.Count + 1;
                    auditoriumsView.ColumnCount = auditoriumsData.sortedAuditoriums.Count + 1;

                    auditoriumsView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    auditoriumsView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                    for (int i = 0; i < auditoriumsData.rings.Count; i++)
                    {
                        auditoriumsView.Rows[i + 1].Cells[0].Value = auditoriumsData.rings[i].Time.ToString("H:mm");
                    }
                    for (int j = 0; j < auditoriumsData.sortedAuditoriums.Count; j++)
                    {
                        auditoriumsView.Rows[0].Cells[j + 1].Value = auditoriumsData.sortedAuditoriums[j].Name;
                    }
                    for (int i = 0; i < auditoriumsData.rings.Count; i++)
                    {
                        for (int j = 0; j < auditoriumsData.sortedAuditoriums.Count; j++)
                        {
                            if (auditoriumsData.buildingAuditoriums[auditoriumsData.rings[i].RingId]
                                .ContainsKey(auditoriumsData.sortedAuditoriums[j].AuditoriumId))
                            {
                                var evt = auditoriumsData.buildingAuditoriums[auditoriumsData.rings[i].RingId]
                                    [auditoriumsData.sortedAuditoriums[j].AuditoriumId];
                                auditoriumsView.Rows[i + 1].Cells[j + 1].Value = evt;
                            }
                            else
                            {
                                auditoriumsView.Rows[i + 1].Cells[j + 1].Value = "";
                            }
                        }
                    }

                    auditoriumsView.Width = Screen.PrimaryScreen.Bounds.Width - 20;

                    loadingLabel.Visible = false;
                },
                cToken,
                TaskContinuationOptions.None,
                _uiScheduler
            );

            if (calculateAudsTask.Status == TaskStatus.Canceled)
            {
                return;
            }
        }

        private string LessonToString(Lesson lesson)
        {
            return lesson.TeacherForDiscipline.Discipline.StudentGroup.Name + Environment.NewLine +
                lesson.TeacherForDiscipline.Discipline.Name + Environment.NewLine +
                lesson.TeacherForDiscipline.Teacher.FIO;
        }

        public List<Auditorium> SortAuditoriums(List<Auditorium> list, int buildingIndex)
        {
            switch (buildingIndex)
            {
                case 2:
                    var result = new List<Auditorium>();
                    var a3 = list
                        .Where(a => a.Name.Length>= 6 && a.Name.Substring(5, 1) == "3")
                        .OrderBy(a => a.Name)
                        .ToList();
                    foreach (var a in a3)
                    {
                        result.Add(a);
                        list.Remove(a);
                    }
                    var a1 = list
                        .Where(a => a.Name.Substring(5, 1) == "1")
                        .OrderBy(a => a.Name)
                        .ToList();
                    foreach (var a in a1)
                    {
                        result.Add(a);
                        list.Remove(a);
                    }
                    result.AddRange(list.OrderBy(a => a.Name));
                    return result;
                case 3:
                case 0:
                    return list.OrderBy(a => a.Name).ToList();
            }

            return null;
        }

        private int AuditoriumBuilding(string auditoriumName)
        {
            if (auditoriumName.StartsWith("Корп № 3"))
            {
                return 3;
            }

            if (((auditoriumName.Length >= 6) && (Char.IsDigit(auditoriumName[5]) || auditoriumName == "Ауд. ШКОЛА")) || (auditoriumName == "Ауд. "))
            {
                return 2;
            }

            return 0;
        }
    }
}
