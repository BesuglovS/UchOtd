using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Forms;
using UchOtd.NUDS.Core;
using UchOtd.NUDS.Forms;
using UchOtd.NUDS.View;
using UchOtd.Properties;

namespace UchOtd.NUDS
{
    public partial class ScheduleForm : Form
    {
        readonly ScheduleRepository _repo;

        public ScheduleForm(ScheduleRepository repo)
        {   
            InitializeComponent();

            _repo = repo;
        }

        private void ScheduleFormLoad(object sender, EventArgs e)
        {
            Icon = Resources.NULogo2;
            
            SetGroupListAndDatePicker();

            groupList.SelectedIndex = 0;
            groupList.Text = ((StudentGroup)groupList.Items[0]).Name;
        }

        private void SetGroupListAndDatePicker()
        {
            var groupName = groupList.Text;

            var filteredGroups = _repo
                .GetFiltredStudentGroups(sg => 
                    !sg.Name.Contains('I') && !sg.Name.Contains('-') && !sg.Name.Contains('+'))
                .OrderBy(sg => sg.Name)
                .ToList();
            
            groupList.DataSource = filteredGroups;
            groupList.DisplayMember = "Name";
            groupList.ValueMember = "StudentGroupId";
            groupList.Text = groupName;

            datePicker.Value = DateTime.Now;
            var calendars = _repo.GetAllCalendars();
            datePicker.MinDate = calendars.Select(c => c.Date).Min();
            datePicker.MaxDate = calendars.Select(c => c.Date).Max();
        }

        private void SwitchInterFace(bool enable)
        {
            groupList.Enabled = enable;
            datePicker.Enabled = enable;
            today.Enabled = enable;
            tomorrow.Enabled = enable;
            changes.Enabled = enable;
            teacherSchedule.Enabled = enable;
        }

        private void DatePickerValueChanged(object sender, EventArgs e)
        {
            UpdateDailySchedule();
        }

        private void UpdateDailySchedule()
        {
            if (groupList.SelectedValue is StudentGroup)
            {
                return;
            }
            if (groupList.SelectedValue == null)
            {
                groupList.SelectedIndex = 0;
            }

            loadingLabel.Visible = true;

            var formViewTask = Task<List<DailyScheduleGroupLessonView>>.Factory.StartNew(groupId =>
                {
                    var lList = Utilities.GetDailySchedule(_repo, (int)groupId, datePicker.Value);
                    return DailyScheduleGroupLessonView.FromLessonsList(lList, (int)groupId);
                },
                groupList.SelectedValue
            );

            formViewTask.ContinueWith(
                antecedent =>
                {
                    scheduleView.DataSource = antecedent.Result;                    
                    FormatMainView.DailyScheduleView(scheduleView, this);
                    loadingLabel.Visible = false;
                },
                TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void ScheduleFormResize(object sender, EventArgs e)
        {
            FormatMainView.DailyScheduleView(scheduleView, this);
        }

        private void TodayClick(object sender, EventArgs e)
        {
            var date = DateTime.Now.Date;

            if (date > datePicker.MaxDate)
            {
                date = datePicker.MaxDate;
            }
            if (date < datePicker.MinDate)
            {
                date = datePicker.MinDate;
            }

            datePicker.Value = date;
        }

        private void TomorrowClick(object sender, EventArgs e)
        {
            var date = DateTime.Now.AddDays(1).Date;

            if (date > datePicker.MaxDate)
            {
                date = datePicker.MaxDate;
            }
            if (date < datePicker.MinDate)
            {
                date = datePicker.MinDate;
            }

            datePicker.Value = date;
        }

        private void GroupListSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDailySchedule();
        }
        
        
        private void ChangesClick(object sender, EventArgs e)
        {
            var changesForm = new Changes(_repo, (int)groupList.SelectedValue);
            changesForm.ShowDialog();
        }

        private void ScheduleViewSelectionChanged(object sender, EventArgs e)
        {
            scheduleView.ClearSelection();
        }

        private void TeacherScheduleClick(object sender, EventArgs e)
        {
            var teacherScheduleForm = new TeacherSchedule(_repo);
            teacherScheduleForm.ShowDialog();
        }
    }
}

