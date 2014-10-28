using System.Linq;
using Schedule.Repositories;
using System.Windows.Forms;
using Schedule.Views;
using Schedule.Views.DBListViews;
using System;
using System.Collections.Generic;
using System.Drawing;
using Schedule.DomainClasses.Main;
using Schedule.Core;
using UchOtd;

namespace Schedule.Forms.DBLists.Lessons
{
    public partial class AddLesson : Form
    {
        private readonly ScheduleRepository _repo;
        private readonly int tfdId = -1;

        private int selectedBuildingId = -1;

        public AddLesson(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        public AddLesson(ScheduleRepository repo, int tfdId)
        {
            InitializeComponent();

            _repo = repo;

            this.tfdId = tfdId;
        }

        private void radioButtonCheckedChanged(Object sender, EventArgs e)
        {
            var radioButton = (sender as RadioButton);
            if (radioButton.Checked)
            {
                selectedBuildingId = (int)(sender as RadioButton).Tag;
            }

            if (StartupForm.school)
            {
                RefreshLists();
            }
        }

        private string DetectBuildingByGroupName(string groupName)
        {
            if ((groupName.StartsWith("1")))
            {
                return "Ярмарочная";
            }

            if ((groupName.StartsWith("2")) || (groupName.StartsWith("3")) || (groupName.StartsWith("4")) ||
                (groupName.StartsWith("5")) || (groupName.StartsWith("6")) || (groupName.StartsWith("7")))
            {
                return "Чапаевская";
            }

            if ((groupName.StartsWith("8")) || (groupName.StartsWith("9")) || 
                (groupName.StartsWith("10")) || (groupName.StartsWith("11")))
            {
                return "Молодогвардейская";
            }

            return "UFO from outer space";
        }

        private void RefreshLists()
        {
            if (selectedBuildingId == -1)
            {
                return;
            }

            var currentBuilding = _repo.GetBuilding(selectedBuildingId);

            // TFD list
            var AllTfdList = _repo.GetAllTeacherForDiscipline();
            var tfdList = new List<TeacherForDiscipline>();
            foreach (var tfd in AllTfdList)
            {
                var partBuildingName = DetectBuildingByGroupName(tfd.Discipline.StudentGroup.Name);
                var building = _repo.GetFirstFiltredBuilding(b => b.Name.Contains(partBuildingName));

                if (building != null && building.BuildingId == selectedBuildingId)
                {
                    tfdList.Add(tfd);
                }
            }

            var tfdViewList = tfdView.tfdsToView(tfdList);
            tfdViewList = tfdViewList.OrderBy(tfdv => tfdv.tfdSummary).ToList();

            teacherForDisciplineBox.DisplayMember = "tfdSummary";
            teacherForDisciplineBox.ValueMember = "TeacherForDisciplineId";
            teacherForDisciplineBox.DataSource = tfdViewList;
        }

        private void AddLesson_Load(object sender, System.EventArgs e)
        {
            var buildings = _repo.GetAllBuildings();

            var startingPositionX = 10;
            var startingPositionY = 5;
            int buildingCounter = 1;
            foreach (var building in buildings)
            { 
                RadioButton buildingButton = new RadioButton();
                buildingButton.Name = "bb_" + building.BuildingId;
                buildingButton.Tag = building.BuildingId;
                buildingButton.Width = 200;
                buildingButton.Text = building.Name;
                buildingButton.Location = new Point(startingPositionX, startingPositionY);
                buildingButton.CheckedChanged += radioButtonCheckedChanged;
                BuildingsPanel.Controls.Add(buildingButton);
                startingPositionX += 200;

                if (buildingCounter % 3 == 0)
                {
                    startingPositionX = 10;
                    startingPositionY += 20;
                }

                buildingCounter++;
            }

            var BuildingsRowCount = (buildings.Count / 3) + ((buildings.Count % 3 == 0) ? 0 : 1);

            BuildingsPanel.Height = 8 + 20 * BuildingsRowCount;


            // TFD load
            var tfdList = _repo.GetAllTeacherForDiscipline();
            var tfdViewList = tfdView.tfdsToView(tfdList);
            tfdViewList = tfdViewList.OrderBy(tfdv => tfdv.tfdSummary).ToList();

            teacherForDisciplineBox.DataSource = tfdViewList;
            teacherForDisciplineBox.DisplayMember = "tfdSummary";
            teacherForDisciplineBox.ValueMember = "TeacherForDisciplineId";

            if (tfdId != -1)
            {
                teacherForDisciplineBox.SelectedValue = tfdId;
                //ringsBox.Focus();
            }

            // Rings load
            /*
            var ringsList = _repo.GetAllRings()                
                .OrderBy(r => r.Time.TimeOfDay)
                .ToList();
            var ringsView = RingView.RingsToView(ringsList);

            ringsListBox.DataSource = ringsView;
            ringsListBox.DisplayMember = "Time";
            ringsListBox.ValueMember = "RingId";
             */

            // DOW Local
            var dowList = new List<object>();
            foreach (var dow in Constants.Constants.DOWLocal)
            {
                dowList.Add(new { Value = dow.Key, Text = dow.Value });
            }

            DayOfWeekListBox.ValueMember = "Value";
            DayOfWeekListBox.DisplayMember = "Text";
            DayOfWeekListBox.DataSource = dowList;            

            // Public comment
            publicComment.Items.AddRange(Constants.Constants.LessonAddPublicComment.ToArray());
            publicComment.SelectedIndex = 0;

            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Execute_Click(object sender, EventArgs e)
        {            
            if (teacherForDisciplineBox.SelectedValue == null)
            {
                MessageBox.Show("Неправильно выбран TFD.");
                return;
            }

            var weekList = ScheduleRepository.WeeksStringToList(lessonWeeks.Text);

            var tfd = _repo.GetTeacherForDiscipline((int)teacherForDisciplineBox.SelectedValue);
            var disc = tfd.Discipline;
            var studentGroup = disc.StudentGroup;
            var sigFromGroup = _repo.GetFiltredStudentsInGroups(sing => sing.StudentGroup.StudentGroupId == studentGroup.StudentGroupId);
            var studentIdsInGroup = sigFromGroup.Select(studentsInGroupse => studentsInGroupse.Student.StudentId).ToList();
            var studentGroupsIds = _repo.GetFiltredStudentsInGroups(sig => studentIdsInGroup.Contains(sig.Student.StudentId)).Select(sing => sing.StudentGroup.StudentGroupId).Distinct();
                                    
            //var ring = _repo.GetRing((int)ringsBox.SelectedValue);
            var rings = new List<Ring>();
            foreach (var ringView in ringsListBox.SelectedItems)
            {
                rings.Add(_repo.GetRing(((RingView)ringView).RingId));
            }
            var ringIds = rings.Select(r => r.RingId).ToList();
            
            
            var calendarIdsList = new List<int>();
            for (int i = 0; i < weekList.Count; i++)
            {
                var date = _repo.GetDateFromDowAndWeek((int)DayOfWeekListBox.SelectedValue, weekList[i]);
                var calendar = _repo.FindCalendar(date);
                if (calendar != null)
                {
                    calendarIdsList.Add(calendar.CalendarId);
                }
            }
            
            var groupsLessons = _repo
                .GetFiltredLessons(
                    l => studentGroupsIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId) && 
                         calendarIdsList.Contains(l.Calendar.CalendarId) && 
                         //l.Ring.RingId == ring.RingId &&
                         ringIds.Contains(l.Ring.RingId) &&
                         l.IsActive);


            if (groupsLessons.Count != 0)
            {                
                DialogResult OutOfMind = MessageBox.Show("У студентов группы есть занятия. Всё равно добавить?", "ЕГГОГ", MessageBoxButtons.YesNo);
                if (OutOfMind == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            
                        
            Dictionary<int, string> audWeekList = new Dictionary<int,string>();
            audWeekList = Utilities.GetAudWeeksList(auditoriums.Text);

            for (int i = 0; i < weekList.Count; i++)
            {
                foreach (var ring in rings)
                {
                    var lesson = new Lesson();

                    lesson.IsActive = isActive.Checked;
                    lesson.TeacherForDiscipline = tfd;
                    lesson.Ring = ring;

                    var date = _repo.GetDateFromDowAndWeek((int)DayOfWeekListBox.SelectedValue, weekList[i]);
                    var calendar = _repo.FindCalendar(date);
                    if (calendar == null)
                    {
                        calendar = new Calendar(date);
                    }
                    lesson.Calendar = calendar;

                    // Auditorium
                    Auditorium aud;
                    if (audList.SelectedIndex != -1)
                    {
                        aud = _repo.GetAuditorium((int)audList.SelectedValue);
                    }
                    else
                    {
                        if (audWeekList.Keys.Count == 1)
                        {
                            var lessonAud = audWeekList[0];
                            audWeekList.Clear();
                            foreach (var week in weekList)
                            {
                                audWeekList.Add(week, lessonAud);
                            }
                        }

                        aud = _repo.FindAuditorium(audWeekList[weekList[i]]);
                        if (aud == null)
                        {
                            _repo.AddAuditorium(new Auditorium(audWeekList[weekList[i]]));
                            aud = _repo.FindAuditorium(audWeekList[weekList[i]]);
                        }
                    }
                    lesson.Auditorium = aud;

                    _repo.AddLesson(lesson, publicComment.Text, hiddenComment.Text);
                }
            }

            this.Close();
        }

        private void showAuds_Click(object sender, EventArgs e)
        {
            var audsForm = new Auditoriums(_repo);
            audsForm.Show();
        }

        private void ringsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFreeAuds();
        }

        private void dayOfWeekBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFreeAuds();
        }


        private void lessonWeeks_TextChanged(object sender, EventArgs e)
        {
            UpdateFreeAuds();
        }

        private void UpdateFreeAuds()
        {
            List<int> weekList;

            try
            {
                weekList = Utilities.ConvertWeeksToList(lessonWeeks.Text);
            }
            catch
            {
                return;
            }

            if ((DayOfWeekListBox.SelectedIndex == -1) || (ringsListBox.SelectedItems.Count == 0) || (lessonWeeks.Text == ""))
            {
                return;
            }          
            
            var calendarIds = new List<int>();
            foreach (var cal in _repo.GetAllCalendars())
            {
                if (Constants.Constants.DOWRemap[(int)cal.Date.DayOfWeek] - 1 == DayOfWeekListBox.SelectedIndex)
                {
                    var week = _repo.CalculateWeekNumber(cal.Date);
                    if (weekList.Contains(week))
                    {
                        calendarIds.Add(cal.CalendarId);
                    }
                }
            }

            var ringIds = new List<int>();
            foreach (var ringView in ringsListBox.SelectedItems)
            {
                ringIds.Add(((RingView)ringView).RingId);
            }
            
            var res = _repo.GetFreeAuditoriumAtDOWTime(calendarIds, ringIds, selectedBuildingId);

            var c = new Utilities.AudComparer();
            res = res
                .OrderBy(aud => aud, c)
                .ToList();

            audList.DataSource = res;
            audList.ValueMember = "AuditoriumId";
            audList.DisplayMember = "Name";

            audList.SelectedIndex = -1;
        }

        private void reset_Click(object sender, EventArgs e)
        {
            audList.SelectedIndex = -1;
        }

        private void deselectBuilding_Click(object sender, EventArgs e)
        {
            foreach (var controlObject in BuildingsPanel.Controls)
            {
                var button = controlObject as RadioButton;

                button.Checked = false;
            }

            selectedBuildingId = -1;
        }

        private void teacherForDisciplineBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRings();
        }

        private void UpdateRings()
        {
            if (filterRings.Checked)
            {
                UpdateTFDRings();
            }
            else
            {
                var ringsList = _repo.GetAllRings()
                .OrderBy(r => r.Time.TimeOfDay)
                .ToList();
                var ringsView = RingView.RingsToView(ringsList);

                ringsListBox.DataSource = ringsView;
                ringsListBox.DisplayMember = "Time";
                ringsListBox.ValueMember = "RingId";
            }
        }

        private void UpdateTFDRings()
        {
            if (!(teacherForDisciplineBox.SelectedValue is int))
            {
                return;
            }

            var tfdId = (int)teacherForDisciplineBox.SelectedValue;
            var tfd = _repo.GetTeacherForDiscipline(tfdId);

            var teacher = tfd.Teacher;

            var RingsForTeacher = _repo
                .GetTeacherRings(teacher)
                .Select(tr => tr.Ring)
                .OrderBy(r => r.Time.TimeOfDay)
                .ToList();

            // Rings load            
            var ringsView = RingView.RingsToView(RingsForTeacher);

            ringsListBox.DataSource = ringsView;
            ringsListBox.DisplayMember = "Time";
            ringsListBox.ValueMember = "RingId";
        }

        private void filterRings_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRings();
        }
    }
}
