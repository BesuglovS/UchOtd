using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class RingList : Form
    {
        private readonly ScheduleRepository _repo;

        public RingList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void RingForm_Load(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            var ringList = _repo.Rings.GetAllRings();

            var ringView = RingView.RingsToView(ringList);
            ringView = ringView
                .OrderBy(r => DateTime.ParseExact(r.Time, "H:mm", CultureInfo.InvariantCulture))
                .ToList();

            RingListView.DataSource = ringView;

            RingListView.Columns["RingId"].Visible = false;
            RingListView.Columns["Time"].Width = 270;

            RingListView.ClearSelection();
        }

        private void RingListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var ringview = ((List<RingView>)RingListView.DataSource)[e.RowIndex];
            var ring = _repo.Rings.GetRing(ringview.RingId);

            RingTime.Value = ring.Time;
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (_repo.Rings.FindRing(RingTime.Value) != null)
            {
                MessageBox.Show("Такое время уже есть.");
                return;
            }

            var newRing = new Ring { Time = RingTime.Value.Subtract(new TimeSpan(0, 0, 0, RingTime.Value.Second, RingTime.Value.Millisecond))};
            _repo.Rings.AddRing(newRing);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (RingListView.SelectedCells.Count > 0)
            {
                var ringView = ((List<RingView>)RingListView.DataSource)[RingListView.SelectedCells[0].RowIndex];
                var ring = _repo.Rings.GetRing(ringView.RingId);

                ring.Time = RingTime.Value;

                ring.Time = ring.Time.Subtract(new TimeSpan(0, 0, 0, ring.Time.Second, ring.Time.Millisecond));

                _repo.Rings.UpdateRing(ring);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (RingListView.SelectedCells.Count > 0)
            {
                var ringView = ((List<RingView>)RingListView.DataSource)[RingListView.SelectedCells[0].RowIndex];

                if (_repo.Lessons.GetFiltredLessons(l => l.Ring.RingId == ringView.RingId).Count > 0)
                {
                    MessageBox.Show("На данное время есть занятия.");
                    return;
                }

                _repo.Rings.RemoveRing(ringView.RingId);

                RefreshView();
            }
        }

        private void deletewithlessons_Click(object sender, EventArgs e)
        {
            if (RingListView.SelectedCells.Count > 0)
            {
                var ringView = ((List<RingView>)RingListView.DataSource)[RingListView.SelectedCells[0].RowIndex];

                var ringLessons = _repo.Lessons.GetFiltredLessons(l => l.Ring.RingId == ringView.RingId);

                if (ringLessons.Count > 0)
                {
                    foreach (var lesson in ringLessons)
                    {
                        _repo.Lessons.RemoveLesson(lesson.LessonId);
                    }
                }

                _repo.Rings.RemoveRing(ringView.RingId);

                RefreshView();
            }
        }

        private void forceDeleteWithReplace_Click(object sender, EventArgs e)
        {
            if (RingListView.SelectedCells.Count > 0)
            {
                var ringView = ((List<RingView>)RingListView.DataSource)[RingListView.SelectedCells[0].RowIndex];

                var replaceRing = _repo.Rings.FindRing(newRing.Value);

                if (replaceRing == null)
                {
                    replaceRing = new Ring { Time = newRing.Value };
                    _repo.Rings.AddRing(replaceRing);
                }

                var ringLessons = _repo.Lessons.GetFiltredLessons(l => l.Ring.RingId == ringView.RingId);

                if (ringLessons.Count > 0)
                {
                    foreach (var lesson in ringLessons)
                    {
                        lesson.Ring = replaceRing;
                        _repo.Lessons.UpdateLesson(lesson);
                    }                    
                }

                _repo.Rings.RemoveRing(ringView.RingId);

                RefreshView();
            }
        }
    }
}
