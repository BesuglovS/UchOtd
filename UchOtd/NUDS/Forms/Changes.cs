using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.NUDS.View;
using UchOtd.Properties;

namespace UchOtd.NUDS.Forms
{
    public partial class Changes : Form
    {
        readonly ScheduleRepository _repo;
        private int _groupId;
        private readonly int _initialGroupId;
        private int _calendarId = -1;        


        private readonly TaskScheduler _uiScheduler;

        public Changes(ScheduleRepository repo, int groupId)
        {
            InitializeComponent();

            Icon = Resources.diffIcon;
                        
            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            _repo = repo;
            _initialGroupId = groupId;
            _groupId = groupId;
        }

        private void ChangesLoad(object sender, EventArgs e)
        {
            Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
            Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

            var initialCalendar = _repo.Calendars.GetFirstFiltredCalendar(c => c.Date.Date == DateTime.Now.Date);
            if (initialCalendar == null)
            {
                var ss = _repo.ConfigOptions.GetFirstFiltredConfigOption(co => co.Key == "Semester Starts");
                initialCalendar = _repo.Calendars.GetFirstFiltredCalendar(c => c.Date == DateTime.ParseExact(ss.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            }

            SetGroupListFromSchedule();

            datePicker.Value = initialCalendar.Date;            
        }

        private void SetGroupChangesView(List<LleView> evtsView)
        {
            changesView.Columns.Clear();
            changesView.DataSource = evtsView;
            SetChangesView();
        }

        private List<LleView> GetGroupChanges(int groupId, int calendarId)
        {
            var studentIds = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId && !sig.Student.Expelled)
                .Select(stig => stig.Student.StudentId)
                .ToList();

            var groupIds = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                .Select(stig => stig.StudentGroup.StudentGroupId)
                .Distinct()
                .ToList();

            List<LessonLogEvent> evts;

            if (calendarId == -1)
            {
                evts = _repo
                    .LessonLogEvents
                    .GetFiltredLessonLogEvents(
                        et =>
                        ((et.OldLesson != null) &&
                         groupIds.Contains(et.OldLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId)) ||
                        ((et.NewLesson != null) &&
                         groupIds.Contains(et.NewLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId)))
                    .OrderByDescending(et => et.DateTime)
                    .ToList();
            }
            else
            {
                evts = _repo
                    .LessonLogEvents
                    .GetFiltredLessonLogEvents(
                        et => ((et.OldLesson != null) && et.OldLesson.Calendar.CalendarId == calendarId &&
                                      groupIds.Contains(et.OldLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId)) ||
                                     ((et.NewLesson != null) && et.NewLesson.Calendar.CalendarId == calendarId &&
                                      groupIds.Contains(et.NewLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId)))
                    .OrderByDescending(et => et.DateTime)
                    .ToList();
            }

            var evtsView = LleView.ListFromLessonLogEvents(evts);
            return evtsView;
        }

        private void SetChangesView()
        {
            changesView.ColumnHeadersVisible = false;
            changesView.RowHeadersVisible = false;
            changesView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            changesView.AutoResizeColumns();
            changesView.AutoResizeRows();
            changesView.AllowUserToResizeColumns = false;
            changesView.AllowUserToResizeRows = false;

            // EventId
            changesView.Columns["EventId"].Visible = false;
            changesView.Columns["EventId"].Width = 0;

            //changesView.Columns[1].DefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.FontFamily, 14);
            //changesView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // EventDate
            changesView.Columns["EventDate"].Width = Percent(20, changesView.Width - 20);

            // EventType
            changesView.Columns["EventType"].Visible = false;
            changesView.Columns["EventType"].Width = 0;

            // Message
            changesView.Columns["Message"].Width = Percent(60, changesView.Width - 20);

            var img = new DataGridViewImageColumn { Name = "img" };

            changesView.Columns.Insert(2, img);

            int numberOfRows = changesView.RowCount;
            Image lessonAddedImage = Resources.LessonAdded;
            Image lessonRemovedImage = Resources.LessonRemoved;
            Image auditoriumChangedImage = Resources.AuditoriumChanged;

            for (int i = 0; i < numberOfRows; i++)
            {
                switch (changesView.Rows[i].Cells["EventType"].Value.ToString())
                {
                    case "1":
                        changesView.Rows[i].Cells["img"].Value = lessonAddedImage;
                        break;
                    case "2":
                        changesView.Rows[i].Cells["img"].Value = lessonRemovedImage;
                        break;
                    case "3":
                        changesView.Rows[i].Cells["img"].Value = auditoriumChangedImage;
                        break;
                }

            }

            // img
            changesView.Columns["img"].Width = Percent(20, changesView.Width - 20);
        }

        private int Percent(int percent, int whole)
        {
            return (int)Math.Round((double)whole * percent / 100);
        }

        private void SetGroupListFromSchedule()
        {
            var filteredGroups = _repo
                .StudentGroups.GetFiltredStudentGroups(sg => !sg.Name.Contains('I') && !sg.Name.Contains('-') && !sg.Name.Contains('+'))
                .OrderBy(sg => sg.Name)
                .ToList();
            
            groupList.DataSource = filteredGroups;
            groupList.DisplayMember = "Name";
            groupList.ValueMember = "StudentGroupId";

            /*
            groupList.SelectedValue = _initialGroupId;

            var studentGroup = _repo.GetFirstFiltredStudentGroups(sg => sg.StudentGroupId == _initialGroupId);
            if (studentGroup != null)
            {
                groupList.SelectedText = studentGroup.Name;
            }
             */
        }

        private void GroupListSelectedValueChanged(object sender, EventArgs e)
        {
            if (groupList.SelectedValue is StudentGroup)
            {
                return;
            }

            if (groupList.SelectedValue != null)
            {
                _groupId = (int)groupList.SelectedValue;

                var evtsView = GetGroupChanges(_groupId, _calendarId);

                SetGroupChangesView(evtsView);
            }
        }

        private void TomorrowsChangesClick(object sender, EventArgs e)
        {
            if (datePicker.Value == DateTime.Now.AddDays(1).Date)
            {
                DatePickerValueChanged(this, e);
            }
            else
            {
                datePicker.Value = DateTime.Now.AddDays(1).Date;
            }
        }

        private void TodaysChangesClick(object sender, EventArgs e)
        {
            if (datePicker.Value == DateTime.Now.Date)
            {
                DatePickerValueChanged(this, e);
            }
            else
            {
                datePicker.Value = DateTime.Now.Date;
            }
        }

        private void ChangesViewCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (changesView.Columns["img"] == null)
            {
                return;
            }

            if ((e.ColumnIndex == changesView.Columns["img"].Index) && (e.Value != null))
            {
                DataGridViewCell cell =
                    changesView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                DataGridViewCell valueCell =
                    changesView.Rows[e.RowIndex].Cells["EventType"];
                var eventType = (int)valueCell.Value;

                switch (eventType)
                {
                    case 1:
                        cell.ToolTipText = "Добавлена пара";
                        break;
                    case 2:
                        cell.ToolTipText = "Отменена пара";
                        break;
                    case 3:
                        cell.ToolTipText = "Изменена аудитория";
                        break;
                    default:
                        cell.ToolTipText = "Страх и ужас";
                        break;

                }
            }
        }

        private void AllChangesClick(object sender, EventArgs e)
        {
            var loadingEvents = Task.Factory.StartNew(() => GetGroupChanges(_groupId, -1));

            loadingEvents.ContinueWith(
                antecedent => SetGroupChangesView(antecedent.Result),
                _uiScheduler
            );
        }

        private void DatePickerValueChanged(object sender, EventArgs e)
        {
            var loadingEvents = Task.Factory.StartNew(() =>
            {
                var calendar = _repo.Calendars.GetFirstFiltredCalendar(c => c.Date.Date == datePicker.Value.Date);
                if (calendar != null)
                {
                    _calendarId = calendar.CalendarId;
                }
                else
                {
                    _calendarId = -1;
                }
                return GetGroupChanges(_groupId, _calendarId);
            }
            );

            loadingEvents.ContinueWith(
                antecedent => SetGroupChangesView(antecedent.Result),
                _uiScheduler
            );
        }

        private void ChangesViewSelectionChanged(object sender, EventArgs e)
        {
            changesView.ClearSelection();
        }
    }
}
