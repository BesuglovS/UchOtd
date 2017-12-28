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
using Schedule.Repositories.Common;
using UchOtd.Schedule;
using UchOtd.Schedule.Views.DBListViews;
using UchOtd.Views;

namespace UchOtd.Forms
{
    public partial class ChooseRingAndAud : Form
    {
        private readonly List<RingView> _rings;
        private readonly MainEditForm _mef;
        private readonly Ring _ring;
        private readonly Building _building;
        private readonly List<Auditorium> _auds;
        private readonly ScheduleRepository _repo;
        private readonly List<int> _weeks;
        private readonly int _dow;

        public ChooseRingAndAud(ScheduleRepository repo, MainEditForm mef, Ring ring, Building building, List<int> weeks, int dow)
        {
            _repo = repo;
            var rings = _repo.Rings.GetAllRings();
            _rings = RingView.RingsToView(rings.OrderBy(r => r.Time.TimeOfDay).ToList());
            _weeks = weeks;
            _dow = dow;
            _mef = mef;
            _ring = ring;
            _building = building;
            _auds = _repo.Auditoriums.FindAll(a => a.Building.BuildingId == building.BuildingId).ToList();
            InitializeComponent();
        }

        private void ChooseRing_Load(object sender, EventArgs e)
        {
            audsList.DisplayMember = "FancyName";
            audsList.DataSource = _auds.Select(a => new AudFreeView(a.AuditoriumId, a.Name, true)).ToList();

            ringsList.DisplayMember = "Time";
            ringsList.DataSource = _rings;

            if (_ring != null)
            {
                int index = -1;
                for (int i = 0; i < _rings.Count; i++)
                {
                    if (_rings[i].RingId == _ring.RingId)
                    {
                        index = i;
                        break;
                    }
                }

                
                if (index != -1)
                {
                    if (_rings.Count > 0)
                    {
                        ringsList.SetSelected(0, false);
                    }
                    ringsList.SetSelected(index, true);
                }
            }

            var mp = MousePosition;

            this.SetDesktopLocation(mp.X-100, mp.Y-30);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rIndexes = new List<int>();
            foreach (int rIndex in ringsList.SelectedIndices)
            {
                rIndexes.Add(_rings[rIndex].RingId);
            }

            var aView = (AudFreeView) audsList.SelectedItem;
            var aud = _repo.Auditoriums.Get(aView.AuditoriumId);

            _mef.ringsChosen(rIndexes, aud);

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ringsList_SelectedValueChanged(object sender, EventArgs e)
        {
            var cf = new CommonFunctions(_repo) {ConnectionString = _repo.GetConnectionString()};

            var auds = new List<List<int>>();
            List<int> result = null;

            foreach (var week in _weeks)
            {
                var calendar = cf.GetCalendarFromDowAndWeek(_dow, week);

                var rIndexes = new List<int>();
                
                foreach (int rIndex in ringsList.SelectedIndices)
                {
                    rIndexes.Add(_rings[rIndex].RingId);

                    auds.Add(_repo.Auditoriums
                        .getFreeAuds(calendar.CalendarId, _rings[rIndex].RingId, _building.BuildingId)
                        .Select(a => a.AuditoriumId).ToList());
                }

                if (auds.Count == 0) return;

                result = auds[0];

                for (int i = 1; i < auds.Count; i++)
                {
                    result = result.Intersect(auds[i]).ToList();
                }
            }

            var resultAuds = _repo.Auditoriums.FindAll(a => result.Contains(a.AuditoriumId));
            if (resultAuds.Count == 0)
            {
                resultAuds = _repo.Auditoriums.GetAll();
            }

            var buildingAuds = _repo.Auditoriums.FindAll(a => a.Building.BuildingId == _building.BuildingId);
            List<int> FreeAudIds = resultAuds.Select(a => a.AuditoriumId).ToList();

            var finalAuds = buildingAuds
                .Select(a => new AudFreeView(a.AuditoriumId, a.Name, FreeAudIds.Contains(a.AuditoriumId))).ToList();

            audsList.DataSource = finalAuds;

        }

        private void audsList_DoubleClick(object sender, EventArgs e)
        {
            button1_Click(this, e);
        }
    }
}
