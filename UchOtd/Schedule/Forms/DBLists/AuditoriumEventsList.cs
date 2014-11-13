using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class AuditoriumEventsList : Form
    {
        private readonly ScheduleRepository _repo;

        public AuditoriumEventsList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void add_Click(object sender, EventArgs e)
        {            
            if (useDataSet.Checked)
            {
                var dow = startDate.Value.DayOfWeek;
                var date = startDate.Value;
                do
                {
                    if (date.DayOfWeek == dow)
                    {
                        DateTime localDate = date;
                        var calendar = _repo.GetFirstFiltredCalendar(c => c.Date.Date == localDate.Date);

                        if (calendar == null)
                        {
                            MessageBox.Show("Не найдена дата: " + date.ToString("d.m.Y"), "Oops");
                            return;
                        }

                        var newEvent = new AuditoriumEvent
                        {
                            Name = eventName.Text,
                            Calendar = calendar,
                            Ring = _repo.GetRing((int)eventTime.SelectedValue),
                            Auditorium = _repo.GetAuditorium((int)eventAuditorium.SelectedValue)
                        };

                        _repo.AddAuditoriumEvent(newEvent);
                    }

                    date = date.AddDays(1);
                } while(date <= finishDate.Value);

            }
            else
            {
                var calendar = _repo.GetFirstFiltredCalendar(c => c.Date.Date == eventDate.Value.Date);

                if (calendar == null)
                {
                    MessageBox.Show("Не найдена дата", "Oops");
                    return;
                }

                var newEvent = new AuditoriumEvent
                {
                    Name = eventName.Text,
                    Calendar = calendar,
                    Ring = _repo.GetRing((int)eventTime.SelectedValue),
                    Auditorium = _repo.GetAuditorium((int)eventAuditorium.SelectedValue)
                };

                _repo.AddAuditoriumEvent(newEvent);
            }
            RefreshView();
        }
        
        private void RefreshView(DateTime? date = null)
        {
            List<AuditoriumEvent> eventsList;

            if ((date != null) || filterBox.Checked)
            {
                if (date == null)
                {
                    date = eventsDate.Value;
                }
                eventsList = _repo
                    .GetAllAuditoriumEvents()
                    .Where(e => e.Calendar.Date.Date == date.Value.Date)
                    .ToList();
            }
            else
            {
                eventsList = _repo
                    .GetAllAuditoriumEvents();                
            }

            eventsView.DataSource = AuditoriumEventView.AuditoriumEventsToView(eventsList);

            eventsView.Columns["AuditoriumEventId"].Visible = false;
            eventsView.Columns["Name"].Width = 270;
            eventsView.Columns["Name"].HeaderText = "Название праздника";
            eventsView.Columns["Calendar"].Width = 100;
            eventsView.Columns["Calendar"].HeaderText = "Дата";
            eventsView.Columns["Ring"].Width = 100;
            eventsView.Columns["Ring"].HeaderText = "Время";
            eventsView.Columns["Auditorium"].Width = 100;
            eventsView.Columns["Auditorium"].HeaderText = "Аудитория";

            eventsView.ClearSelection();
        }

        private void AuditoriumEventsList_Load(object sender, EventArgs e)
        {
            var minDate = _repo.GetAllCalendars().OrderBy(c => c.Date).FirstOrDefault().Date;
            var maxDate = _repo.GetAllCalendars().OrderBy(c => c.Date).LastOrDefault().Date;
            eventDate.MinDate = minDate;
            eventDate.MaxDate = maxDate;

            startDate.Value = minDate;
            finishDate.Value = minDate;

            var ringsList = _repo.GetAllRings()
                .OrderBy(r => r.Time.TimeOfDay)
                .ToList();
            var ringsView = RingView.RingsToView(ringsList);

            eventTime.DataSource = ringsView;
            eventTime.DisplayMember = "Time";
            eventTime.ValueMember = "RingId";

            var auds = _repo.GetAllAuditoriums();
            eventAuditorium.DataSource = auds;
            eventAuditorium.DisplayMember = "Name";
            eventAuditorium.ValueMember = "AuditoriumId";

            RefreshView();            
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (eventsView.SelectedCells.Count > 0)
            {
                var calendar = _repo.GetFirstFiltredCalendar(c => c.Date == eventDate.Value);

                if (calendar == null)
                {
                    MessageBox.Show("Дата не найдена", "Oops!");
                    return;
                }

                var aeView = ((List<AuditoriumEventView>)eventsView.DataSource)[eventsView.SelectedCells[0].RowIndex];

                var ae = _repo.GetAuditoriumEvent(aeView.AuditoriumEventId);

                ae.Name = eventName.Text;
                ae.Calendar = calendar;
                ae.Ring = _repo.GetRing((int)eventTime.SelectedValue);
                ae.Auditorium = _repo.GetAuditorium((int)eventAuditorium.SelectedValue);

                _repo.UpdateAuditoriumEvent(ae);

                RefreshView();
            }

        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (eventsView.SelectedRows.Count > 1)
            {
                var eventIds = new List<int>();
                for (int i = 0; i < eventsView.SelectedRows.Count; i++)
                {
                    eventIds.Add(((List<AuditoriumEventView>)eventsView.DataSource)[eventsView.SelectedRows[i].Index].AuditoriumEventId);
                }

                foreach (var id in eventIds)
                {
                    _repo.RemoveAuditoriumEvent(id);
                }

                RefreshView();
                return;
            }

            if (eventsView.SelectedCells.Count > 0)
            {
                var audEventView = ((List<AuditoriumEventView>)eventsView.DataSource)[eventsView.SelectedCells[0].RowIndex];

                _repo.RemoveAuditoriumEvent(audEventView.AuditoriumEventId);

                RefreshView();
            }
        }

        private void eventsView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var evtId = ((List<AuditoriumEventView>)eventsView.DataSource)[e.RowIndex].AuditoriumEventId;
            var evt = _repo.GetAuditoriumEvent(evtId);

            eventName.Text = evt.Name;
            eventDate.Value = evt.Calendar.Date.Date;
            eventTime.Text = evt.Ring.Time.ToString("H:mm");
            eventAuditorium.Text = evt.Auditorium.Name;
            
        }

        private void filter_Click(object sender, EventArgs e)
        {
            RefreshView(eventsDate.Value.Date);
        }

        private void showAll_Click(object sender, EventArgs e)
        {
            RefreshView();
        }
    }
}
