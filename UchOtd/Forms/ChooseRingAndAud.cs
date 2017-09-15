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
using UchOtd.Schedule;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Forms
{
    public partial class ChooseRingAndAud : Form
    {
        private readonly List<RingView> _rings;
        private readonly MainEditForm _mef;
        private readonly Ring _ring;
        private readonly List<Auditorium> _auds;

        public ChooseRingAndAud(List<Ring> rings, MainEditForm mef, Ring ring, List<Auditorium> auds)
        {
            _rings = RingView.RingsToView(rings.OrderBy(r => r.Time.TimeOfDay).ToList());
            _mef = mef;
            _ring = ring;
            _auds = auds;
            InitializeComponent();
        }

        private void ChooseRing_Load(object sender, EventArgs e)
        {
            audsList.DisplayMember = "Name";
            audsList.DataSource = _auds;

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

            this.SetDesktopLocation(mp.X-50, mp.Y-260);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rIndexes = new List<int>();
            foreach (int rIndex in ringsList.SelectedIndices)
            {
                rIndexes.Add(_rings[rIndex].RingId);
            }

            _mef.ringsChosen(rIndexes, (Auditorium)audsList.SelectedItem);

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
