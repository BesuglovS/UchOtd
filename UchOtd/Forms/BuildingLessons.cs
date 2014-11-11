using System.Threading.Tasks;
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
        
        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public BuildingLessons(ScheduleRepository repo)
        {
            InitializeComponent();

            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            _repo = repo;
        }

        private void BuildingLessonsLoad(object sender, EventArgs e)
        {
            var buildings = _repo.GetAllBuildings()
                .OrderBy(b => b.Name)
                .ToList();

            var mainBuilding = buildings.FirstOrDefault(b => b.Name == "ул. Молодогвардейская, 196");

            building.DisplayMember = "Name";
            building.ValueMember = "BuildingId";
            building.DataSource = buildings;

            if (mainBuilding != null)
            {
                building.SelectedValue = mainBuilding.BuildingId;
            }

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
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }

            _tokenSource = new CancellationTokenSource();
            _cToken = _tokenSource.Token;

            if ((building.SelectedIndex == -1) || (lessonsDate.Value == new DateTime(1985, 4, 4)))
            {
                return;
            }

            var data = new BuildingLessonsData();

            loadingLabel.Visible = true;

            var buildingId = (int)building.SelectedValue;
            var viewDate = lessonsDate.Value.Date;

            var calculateAudsTask = Task.Factory.StartNew(() =>
            {
                var lessons = _repo
                    .GetFiltredLessons(l =>
                        ((l.State == 1) || ((l.State == 2) && showProposed.Checked)) &&
                        l.Auditorium.Building.BuildingId == buildingId &&
                        l.Calendar.Date.Date == viewDate);
                _cToken.ThrowIfCancellationRequested();

                var evts = _repo
                    .GetFiltredAuditoriumEvents(ae =>
                        ae.Auditorium.Building.BuildingId == buildingId &&
                        ae.Calendar.Date.Date == viewDate
                    );
                _cToken.ThrowIfCancellationRequested();

                var evtsRings = evts
                    .Select(e => e.Ring)
                    .Distinct()
                    .OrderBy(r => r.Time.TimeOfDay)
                    .ToList();

                data.Rings = lessons
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

                data.SortedAuditoriums = SortAuditoriums(totalAuds, buildingId);

                data.BuildingAuditoriums = new Dictionary<int, Dictionary<int, string>>();
                // Занятия
                foreach (Ring ring in data.Rings)
                {
                    _cToken.ThrowIfCancellationRequested();
                    data.BuildingAuditoriums.Add(ring.RingId, new Dictionary<int, string>());

                    foreach (Auditorium aud in data.SortedAuditoriums)
                    {
                        var ll = lessons
                            .Where(l => l.Ring.RingId == ring.RingId &&
                                        l.Auditorium.AuditoriumId == aud.AuditoriumId)
                            .ToList();
                        if (ll.Any())
                        {
                            if (data.BuildingAuditoriums[ring.RingId].ContainsKey(aud.AuditoriumId))
                            {
                                data.BuildingAuditoriums[ring.RingId][aud.AuditoriumId] += Environment.NewLine + LessonToString(ll[0]);
                            }
                            else
                            {
                                data.BuildingAuditoriums[ring.RingId].Add(aud.AuditoriumId, LessonToString(ll[0]));
                            }
                        }

                        var curEvents = evts
                            .Where(e => e.Ring.RingId == ring.RingId &&
                                        e.Auditorium.AuditoriumId == aud.AuditoriumId)
                            .ToList();

                        if (curEvents.Any())
                        {
                            if (!data.BuildingAuditoriums[ring.RingId].ContainsKey(aud.AuditoriumId))
                            {
                                data.BuildingAuditoriums[ring.RingId].Add(aud.AuditoriumId, "");
                            }

                            if (data.BuildingAuditoriums[ring.RingId][aud.AuditoriumId] != "")
                            {
                                data.BuildingAuditoriums[ring.RingId][aud.AuditoriumId] += Environment.NewLine;
                            }
                            data.BuildingAuditoriums[ring.RingId][aud.AuditoriumId] += curEvents[0].Name;
                        }

                    }
                }

                return data;
            }, _cToken);

            if (calculateAudsTask.Status == TaskStatus.Canceled)
            {
                return;
            }

            calculateAudsTask.ContinueWith(
                antecedent =>
                {
                    var auditoriumsData = antecedent.Result;
                    auditoriumsView.RowCount = data.Rings.Count + 1;
                    auditoriumsView.ColumnCount = auditoriumsData.SortedAuditoriums.Count + 1;

                    auditoriumsView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    auditoriumsView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                    for (int i = 0; i < auditoriumsData.Rings.Count; i++)
                    {
                        auditoriumsView.Rows[i + 1].Cells[0].Value = auditoriumsData.Rings[i].Time.ToString("H:mm");
                    }
                    for (int j = 0; j < auditoriumsData.SortedAuditoriums.Count; j++)
                    {
                        auditoriumsView.Rows[0].Cells[j + 1].Value = auditoriumsData.SortedAuditoriums[j].Name;
                    }
                    for (int i = 0; i < auditoriumsData.Rings.Count; i++)
                    {
                        for (int j = 0; j < auditoriumsData.SortedAuditoriums.Count; j++)
                        {
                            if (auditoriumsData.BuildingAuditoriums[auditoriumsData.Rings[i].RingId]
                                .ContainsKey(auditoriumsData.SortedAuditoriums[j].AuditoriumId))
                            {
                                var evt = auditoriumsData.BuildingAuditoriums[auditoriumsData.Rings[i].RingId]
                                    [auditoriumsData.SortedAuditoriums[j].AuditoriumId];
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
                _cToken,
                TaskContinuationOptions.None,
                _uiScheduler
            );

            if (calculateAudsTask.Status == TaskStatus.Canceled)
            {
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
            var building = _repo.GetFirstFiltredBuilding(b => b.BuildingId == buildingIndex);
            if (building != null)
            {
                if (building.Name == "ул. Молодогвардейская, 196")
                {
                    var result = new List<Auditorium>();
                    var a3 = list
                        .Where(a => a.Name.Length >= 6 && a.Name.Substring(5, 1) == "3")
                        .OrderBy(a => a.Name)
                        .ToList();
                    foreach (var a in a3)
                    {
                        result.Add(a);
                        list.Remove(a);
                    }
                    var a1 = list
                        .Where(a => a.Name.Length >= 6 && a.Name.Substring(5, 1) == "1")
                        .OrderBy(a => a.Name)
                        .ToList();
                    foreach (var a in a1)
                    {
                        result.Add(a);
                        list.Remove(a);
                    }
                    result.AddRange(list.OrderBy(a => a.Name));
                    return result;
                }
            }            

            return list.OrderBy(a => a.Name).ToList();
        }
    }
}
