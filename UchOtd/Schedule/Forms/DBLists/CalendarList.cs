using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            var newCalendar = new Calendar { Date = calendarDate.Value.Date };
            _repo.AddCalendar(newCalendar);

            RefreshView();
        }

        private void CalendarList_Load(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            var calendarList = _repo.GetAllCalendars().OrderBy(c => c.Date).ToList();

            CalendarListView.DataSource = calendarList;

            CalendarListView.Columns["CalendarId"].Visible = false;
            CalendarListView.Columns["Date"].Width = 240;

            CalendarListView.ClearSelection();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (CalendarListView.SelectedCells.Count > 0)
            {
                var cl = ((List<Calendar>)CalendarListView.DataSource)[CalendarListView.SelectedCells[0].RowIndex];

                cl.Date = calendarDate.Value;

                _repo.UpdateCalendar(cl);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (CalendarListView.SelectedCells.Count > 0)
            {
                var cl = ((List<Calendar>)CalendarListView.DataSource)[CalendarListView.SelectedCells[0].RowIndex];

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
                var cl = ((List<Calendar>)CalendarListView.DataSource)[CalendarListView.SelectedCells[0].RowIndex];

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
            var cl = ((List<Calendar>)CalendarListView.DataSource)[e.RowIndex];

            calendarDate.Value = cl.Date;
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
