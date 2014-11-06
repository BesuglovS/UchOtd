using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UchOtd.Schedule.Views.DBListViews;
using Calendar = Schedule.DomainClasses.Main.Calendar;

namespace Schedule.Forms.DBLists
{
    public partial class CalendarList : Form
    {
        private readonly ScheduleRepository _repo;

        public CalendarList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (_repo.FindCalendar(calendarDate.Value) != null)
            {
                MessageBox.Show("Эта дата уже есть.");
                return;
            }
            
            var newCalendar = new Calendar
            {
                Date = calendarDate.Value.Date,
                State = calendarState.SelectedIndex
            };
            _repo.AddCalendar(newCalendar);

            RefreshView();
        }

        private void CalendarList_Load(object sender, EventArgs e)
        {
            FillCalendarStates();
            RefreshView();
        }

        private void FillCalendarStates()
        {
            int index = 0;

            while (Constants.Constants.CalendarStateDescription.ContainsKey(index))
            {
                calendarState.Items.Add(Constants.Constants.CalendarStateDescription[index]);

                index++;
            }
        }

        private void RefreshView()
        {
            var calendarList = _repo.GetAllCalendars().OrderBy(c => c.Date).ToList();
            var viewList = CalendarView.CalendarsToView(calendarList);

            CalendarListView.DataSource = viewList;

            CalendarListView.Columns["CalendarId"].Visible = false;

            CalendarListView.Columns["Date"].Width = 180;
            CalendarListView.Columns["Date"].HeaderText = "Дата";

            CalendarListView.Columns["State"].Visible = false;

            CalendarListView.Columns["StateString"].Width = 60;
            CalendarListView.Columns["StateString"].HeaderText = "Тип";

            CalendarListView.ClearSelection();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (CalendarListView.SelectedCells.Count > 0)
            {
                var view = ((List<CalendarView>)CalendarListView.DataSource)[CalendarListView.SelectedCells[0].RowIndex];

                var cl = _repo.GetCalendar(view.CalendarId);

                cl.Date = calendarDate.Value;
                cl.State = calendarState.SelectedIndex;

                _repo.UpdateCalendar(cl);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (CalendarListView.SelectedCells.Count > 0)
            {
                var cl = ((List<CalendarView>)CalendarListView.DataSource)[CalendarListView.SelectedCells[0].RowIndex];

                if (_repo.GetFiltredLessons(l => l.Calendar.CalendarId == cl.CalendarId).Count > 0)
                {
                    MessageBox.Show("Дата есть в расписании.");
                    return;
                }

                _repo.RemoveCalendar(cl.CalendarId);

                RefreshView();
            }
        }

        private void deletewithlessons_Click(object sender, EventArgs e)
        {
            if (CalendarListView.SelectedCells.Count > 0)
            {
                var cl = ((List<CalendarView>)CalendarListView.DataSource)[CalendarListView.SelectedCells[0].RowIndex];

                var clLessons = _repo.GetFiltredLessons(l => l.Calendar.CalendarId == cl.CalendarId);

                if (clLessons.Count > 0)
                {
                    foreach (var lesson in clLessons)
                    {
                        _repo.RemoveLesson(lesson.LessonId);
                    }
                }

                _repo.RemoveCalendar(cl.CalendarId);

                RefreshView();
            }
        }

        private void CalendarListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var cl = ((List<CalendarView>)CalendarListView.DataSource)[e.RowIndex];

            calendarDate.Value = cl.Date;
            calendarState.SelectedIndex = cl.State;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var curDate = startDate.Value.Date;

            do
            {
                if (_repo.FindCalendar(curDate) == null)
                {
                    var newCalendar = new Calendar { Date = curDate };
                    _repo.AddCalendar(newCalendar);
                }                

                RefreshView();

                curDate = curDate.AddDays(1);
            } while (curDate <= finishDate.Value.Date);
        }
    }
}
