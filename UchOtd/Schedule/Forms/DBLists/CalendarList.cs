using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.DBLists
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
            if (_repo.Calendars.FindCalendar(calendarDate.Value) != null)
            {
                MessageBox.Show("Эта дата уже есть.");
                return;
            }
            
            var newCalendar = new Calendar
            {
                Date = calendarDate.Value.Date,
                State = calendarState.SelectedIndex
            };
            _repo.Calendars.AddCalendar(newCalendar);

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

            while (global::Schedule.Constants.Constants.CalendarStateDescription.ContainsKey(index))
            {
                calendarState.Items.Add(global::Schedule.Constants.Constants.CalendarStateDescription[index]);

                index++;
            }
        }

        private void RefreshView()
        {
            var calendarList = _repo.Calendars.GetAllCalendars().OrderBy(c => c.Date).ToList();
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

                var cl = _repo.Calendars.GetCalendar(view.CalendarId);

                cl.Date = calendarDate.Value;
                cl.State = calendarState.SelectedIndex;

                _repo.Calendars.UpdateCalendar(cl);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (CalendarListView.SelectedCells.Count > 0)
            {
                var cl = ((List<CalendarView>)CalendarListView.DataSource)[CalendarListView.SelectedCells[0].RowIndex];

                if (_repo.Lessons.GetFiltredLessons(l => l.Calendar.CalendarId == cl.CalendarId).Count > 0)
                {
                    MessageBox.Show("Дата есть в расписании.");
                    return;
                }

                _repo.Calendars.RemoveCalendar(cl.CalendarId);

                RefreshView();
            }
        }

        private void deletewithlessons_Click(object sender, EventArgs e)
        {
            if (CalendarListView.SelectedCells.Count > 0)
            {
                var cl = ((List<CalendarView>)CalendarListView.DataSource)[CalendarListView.SelectedCells[0].RowIndex];

                var clLessons = _repo.Lessons.GetFiltredLessons(l => l.Calendar.CalendarId == cl.CalendarId);

                if (clLessons.Count > 0)
                {
                    foreach (var lesson in clLessons)
                    {
                        _repo.Lessons.RemoveLesson(lesson.LessonId);
                    }
                }

                _repo.Calendars.RemoveCalendar(cl.CalendarId);

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
                if (_repo.Calendars.FindCalendar(curDate) == null)
                {
                    var newCalendar = new Calendar { Date = curDate };
                    _repo.Calendars.AddCalendar(newCalendar);
                }                

                RefreshView();

                curDate = curDate.AddDays(1);
            } while (curDate <= finishDate.Value.Date);
        }
    }
}
