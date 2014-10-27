using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms
{
    public partial class ChooseRings : Form
    {
        private readonly ScheduleRepository _repo;
        private readonly Teacher teacher;

        public ChooseRings(ScheduleRepository repo, Teacher teacher)
        {
            InitializeComponent();

            this._repo = repo;
            this.teacher = teacher;
        }

        private void ChooseRings_Load(object sender, EventArgs e)
        {
            RefreshRings();
        }

        private void RefreshRings()
        {
            var teacherRingIds = _repo
                .GetFiltredTeacherRings(tr => tr.Teacher.TeacherId == teacher.TeacherId)
                .Select(tr => tr.Ring.RingId)
                .ToList();

            var allRingViews = RingView.RingsToView(_repo.GetAllRings());

            RingsList.ValueMember = "RingId";
            RingsList.DisplayMember = "Time";
            RingsList.DataSource = allRingViews;

            RingsList.ClearSelected();

            for (int i = 0; i < RingsList.Items.Count; i++)
            {
                var ringId = allRingViews[i].RingId;

                if (teacherRingIds.Contains(ringId))
                {
                    RingsList.SetSelected(i, true);
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            var teacherRingIds = _repo
                .GetFiltredTeacherRings(tr => tr.Teacher.TeacherId == teacher.TeacherId)
                .Select(tr => tr.Ring.RingId)
                .ToList();


            for (int i = 0; i < RingsList.Items.Count; i++)
            {
                bool selected = RingsList.GetSelected(i);
                int ringId = ((List<RingView>)RingsList.DataSource)[i].RingId;
                var ring = _repo.GetRing(ringId);

                if (selected && !teacherRingIds.Contains(ringId))
                {
                    Wishes.NeedsUpdateAfterChoosingRings = true;

                    var newTeacherRing = new TeacherRing(teacher, _repo.GetRing(ringId));
                    _repo.AddTeacherRing(newTeacherRing);

                    var newTeacherWishList = new List<TeacherWish>();

                    for (int dow = 1; dow <= 6; dow++)
                    {
                        newTeacherWishList.AddRange(
                            _repo.GetDOWCalendars(dow)
                            .Select(calendar => new TeacherWish(teacher, calendar, ring, 0)));
                    }

                    _repo.AddTeacherWishRange(newTeacherWishList);
                }

                if (!selected && teacherRingIds.Contains(ringId))
                {
                    Wishes.NeedsUpdateAfterChoosingRings = true;

                    var teacherRing = _repo.GetFirstFiltredTeacherRing(tr =>
                        tr.Teacher.TeacherId == teacher.TeacherId &&
                        tr.Ring.RingId == ringId);

                    _repo.RemoveTeacherRing(teacherRing.TeacherRingId);

                    var teacherWishes = _repo
                        .GetFiltredTeacherWishes(tw =>
                            tw.Teacher.TeacherId == teacher.TeacherId &&
                            tw.Ring.RingId == ringId);

                    foreach (var wish in teacherWishes)
                    {
                        _repo.RemoveTeacherWish(wish.TeacherWishId);
                    }
                }
            }

            Close();
        }

        private void MolRings_Click(object sender, EventArgs e)
        {
            var standard80Rings = new List<string>
            {"08:00", "09:25", "11:05", "12:35", "14:00", "15:40", "17:05", "18:35"};

            var allRings = _repo.GetAllRings();
            var allRingViews = RingView.RingsToView(allRings);

            RingsList.ValueMember = "RingId";
            RingsList.DisplayMember = "Time";
            RingsList.DataSource = allRingViews;

            RingsList.ClearSelected();

            for (int i = 0; i < RingsList.Items.Count; i++)
            {
                RingsList.SetSelected(i, standard80Rings.Contains(allRings[i].Time.ToString("HH:mm")));
            }
        }
    }
}
