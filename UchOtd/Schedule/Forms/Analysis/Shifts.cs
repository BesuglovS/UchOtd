using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class Shifts : Form
    {
        private readonly ScheduleRepository _repo;

        public Shifts(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void Shifts_Load(object sender, EventArgs e)
        {
            RefreshShifts();
        }

        private void RefreshShifts()
        {
            var allshifts = _repo.Shifts.GetAllShifts();

            shiftsListBox.DisplayMember = "Name";
            shiftsListBox.ValueMember = "ShiftId";
            shiftsListBox.DataSource = allshifts;            
        }

        private void RefreshRings(Shift shift)
        {
            var selectedShiftId = shift.ShiftId;

            var shiftRings = _repo.ShiftRings.GetShiftRings(selectedShiftId);
            var shiftRingIds = shiftRings.Select(r => r.RingId).ToList();

            var shiftRingsView = RingView.RingsToView(shiftRings.OrderBy(r => r.Time.TimeOfDay).ToList());

            ShiftRingsListBox.ValueMember = "RingId";
            ShiftRingsListBox.DisplayMember = "Time";
            ShiftRingsListBox.DataSource = shiftRingsView;

            var ringsLeft = _repo.Rings.GetFiltredRings(r => !shiftRingIds.Contains(r.RingId));

            var ringsLeftView = RingView.RingsToView(ringsLeft.OrderBy(r => r.Time.TimeOfDay).ToList());

            AllRingsListBox.ValueMember = "RingId";
            AllRingsListBox.DisplayMember = "Time";
            AllRingsListBox.DataSource = ringsLeftView;
        }

        private void shiftsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedshift = (Shift)shiftsListBox.SelectedItem;

            shiftName.Text = selectedshift.Name;

            RefreshRings(selectedshift);

            
        }

        private void add_Click(object sender, EventArgs e)
        {
            var newShift = new Shift(shiftName.Text);

            _repo.Shifts.AddShift(newShift);

            RefreshShifts();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (shiftsListBox.SelectedItem != null)
            {
                var selectedShift = (Shift)shiftsListBox.SelectedItem;

                selectedShift.Name = shiftName.Text;

                _repo.Shifts.UpdateShift(selectedShift);

                RefreshShifts();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (shiftsListBox.SelectedItem != null)
            {
                var selectedShift = (Shift)shiftsListBox.SelectedItem;

                _repo.Shifts.RemoveShift(selectedShift.ShiftId);

                RefreshShifts();
            }
        }

        private void AddShiftRing_Click(object sender, EventArgs e)
        {
            if (shiftsListBox.SelectedItem != null && AllRingsListBox.SelectedItem != null)
            {
                var selectedShift = (Shift)shiftsListBox.SelectedItem;

                var selectedRings = new List<Ring>();
                for (int i = 0; i < AllRingsListBox.Items.Count; i++)
                {
                    bool selected = AllRingsListBox.GetSelected(i);
                    int ringId = ((List<RingView>)AllRingsListBox.DataSource)[i].RingId;

                    if (selected)
                    {
                        selectedRings.Add(_repo.Rings.GetRing(ringId));
                    }
                }
                
                foreach (var ring in selectedRings)
                {
                    var localRing = ring;
                    var foundedRing = _repo
                        .ShiftRings
                        .GetFirstFiltredShiftRing(sr =>
                    sr.Shift.ShiftId == selectedShift.ShiftId &&
                    sr.Ring.RingId == localRing.RingId);

                    if (foundedRing == null)
                    {
                        _repo.ShiftRings
                            .AddShiftRing(new ShiftRing(selectedShift, ring));
                    }
                }
            }
            else
            {
                if ((shiftsListBox.SelectedItem == null) && (AllRingsListBox.SelectedItem != null))
                {
                    MessageBox.Show("Не выбрана смена.", "Ошибка");
                }

                if ((shiftsListBox.SelectedItem != null) && (AllRingsListBox.SelectedItem == null))
                {
                    MessageBox.Show("Не выбран звонок.", "Ошибка");
                }

                if ((shiftsListBox.SelectedItem == null) && (AllRingsListBox.SelectedItem == null))
                {
                    MessageBox.Show("Не выбраны ни смена, ни звонок.", "Ошибка");
                }
                    
            }

            RefreshRings((Shift)shiftsListBox.SelectedItem);
        }

        private void RemoveShiftRing_Click(object sender, EventArgs e)
        {
            if (shiftsListBox.SelectedItem != null && ShiftRingsListBox.SelectedItem != null)
            {
                var selectedShift = (Shift)shiftsListBox.SelectedItem;
                
                var selectedRings = new List<Ring>();
                for (int i = 0; i < ShiftRingsListBox.Items.Count; i++)
                {
                    bool selected = ShiftRingsListBox.GetSelected(i);
                    int ringId = ((List<RingView>)ShiftRingsListBox.DataSource)[i].RingId;

                    if (selected)
                    {
                        selectedRings.Add(_repo.Rings.GetRing(ringId));
                    }
                }

                foreach (var ring in selectedRings)
                {
                    var foundedRing = _repo.ShiftRings
                        .GetFirstFiltredShiftRing(sr =>
                        sr.Shift.ShiftId == selectedShift.ShiftId &&
                        sr.Ring.RingId == ring.RingId);

                    if (foundedRing != null)
                    {
                        _repo.ShiftRings.RemoveShiftRing(foundedRing.ShiftRingId);
                    }
                }
            }
            else
            {
                if ((shiftsListBox.SelectedItem == null) && (ShiftRingsListBox.SelectedItem != null))
                {
                    MessageBox.Show("Не выбрана смена.", "Ошибка");
                }

                if ((shiftsListBox.SelectedItem != null) && (ShiftRingsListBox.SelectedItem == null))
                {
                    MessageBox.Show("Не выбран звонок.", "Ошибка");
                }

                if ((shiftsListBox.SelectedItem == null) && (ShiftRingsListBox.SelectedItem == null))
                {
                    MessageBox.Show("Не выбраны ни смена, ни звонок.", "Ошибка");
                }
            }

            RefreshRings((Shift)shiftsListBox.SelectedItem);
        }        
    }
}
