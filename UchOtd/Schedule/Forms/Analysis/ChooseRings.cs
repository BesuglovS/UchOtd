using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class ChooseRings : Form
    {
        private readonly ScheduleRepository _repo;
        private readonly Teacher _teacher;

        public ChooseRings(ScheduleRepository repo, Teacher teacher)
        {
            InitializeComponent();

            _repo = repo;
            _teacher = teacher;
        }

        private void ChooseRings_Load(object sender, EventArgs e)
        {
            RefreshRings();
        }

        private void RefreshRings()
        {
            var teacherRingIds = _repo
                .GetFiltredCustomTeacherAttributes(cta => (cta.Teacher.TeacherId == _teacher.TeacherId) && (cta.Key == "TeacherRing"))
                .Select(cta => int.Parse(cta.Value))
                .ToList();

            var allRingViews = RingView.RingsToView(_repo.GetAllRings().OrderBy(r => r.Time.TimeOfDay).ToList());

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
                .GetFiltredCustomTeacherAttributes(cta => (cta.Teacher.TeacherId == _teacher.TeacherId) && (cta.Key == "TeacherRing"))
                .Select(cta => int.Parse(cta.Value))
                .ToList();

            for (int i = 0; i < RingsList.Items.Count; i++)
            {
                bool selected = RingsList.GetSelected(i);
                int ringId = ((List<RingView>)RingsList.DataSource)[i].RingId;
                var ring = _repo.GetRing(ringId);

                if (selected && !teacherRingIds.Contains(ringId))
                {
                    Wishes.NeedsUpdateAfterChoosingRings = true;

                    var newTeacherRingAttribute = new CustomTeacherAttribute(_teacher, "TeacherRing", ringId.ToString(CultureInfo.InvariantCulture));
                    _repo.AddCustomTeacherAttribute(newTeacherRingAttribute);

                    var newTeacherWishList = new List<TeacherWish>();

                    newTeacherWishList.AddRange(
                        _repo.GetAllCalendars()
                            .Select(calendar => new TeacherWish(_teacher, calendar, ring, 0)));

                    _repo.AddTeacherWishRange(newTeacherWishList);
                }

                if (!selected && teacherRingIds.Contains(ringId))
                {
                    Wishes.NeedsUpdateAfterChoosingRings = true;

                    var teacherRingAttribute = _repo
                        .GetFirstFiltredCustomTeacherAttribute( cta =>
                        cta.Teacher.TeacherId == _teacher.TeacherId &&
                        cta.Key == "TeacherRing" &&
                        cta.Value == ringId.ToString(CultureInfo.InvariantCulture));

                    _repo.RemoveCustomTeacherAttribute(teacherRingAttribute.CustomTeacherAttributeId);

                    var teacherWishes = _repo
                        .GetFiltredTeacherWishes(tw =>
                            tw.Teacher.TeacherId == _teacher.TeacherId &&
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
            {"8:00", "9:25", "11:05", "12:35", "14:00", "15:40", "17:05", "18:35"};

            RingsList.ClearSelected();

            var datasource = (List<RingView>)RingsList.DataSource;

            for (int i = 0; i < datasource.Count; i++)
            {
                RingsList.SetSelected(i, standard80Rings.Contains(datasource[i].Time));
            }
        }
    }
}
