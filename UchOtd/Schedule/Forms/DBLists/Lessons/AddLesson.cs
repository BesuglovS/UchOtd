using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Schedule.Constants;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Repositories.Common;
using UchOtd.Schedule.Core;
using UchOtd.Schedule.Views;
using UchOtd.Schedule.Views.DBListViews;
using Calendar = Schedule.DomainClasses.Main.Calendar;

namespace UchOtd.Schedule.Forms.DBLists.Lessons
{
    public partial class AddLesson : Form
    {
        private readonly ScheduleRepository _repo;
        private readonly int _tfdId = -1;

        private int _selectedBuildingId = -1;

        public AddLesson(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        public AddLesson(ScheduleRepository repo, int tfdId)
        {
            InitializeComponent();

            _repo = repo;

            _tfdId = tfdId;
        }

        private void RadioButtonCheckedChanged(Object sender, EventArgs e)
        {
            var radioButton = (sender as RadioButton);
            if (radioButton.Checked)
            {
                _selectedBuildingId = (int)(sender as RadioButton).Tag;
            }

            /*
            if (StartupForm.School)
            {
                RefreshLists();
            }
            */ 
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
            if (_selectedBuildingId == -1)
            {
                return;
            }

            _repo.Buildings.GetBuilding(_selectedBuildingId);

            // TFD list
            /*
            var allTfdList = _repo.TeacherForDisciplines.GetAllTeacherForDiscipline();
            var tfdList = (
                    from tfd in allTfdList
                    let partBuildingName = DetectBuildingByGroupName(tfd.Discipline.StudentGroup.Name)
                    let building = _repo.Buildings.GetFirstFiltredBuilding(b => b.Name.Contains(partBuildingName))
                    where building != null && building.BuildingId == _selectedBuildingId
                    select tfd)
                .ToList();

            var tfdViewList = TfdView.TfdsToView(tfdList);
            tfdViewList = tfdViewList.OrderBy(tfdv => tfdv.TfdSummary).ToList();

            teacherForDisciplineBox.DisplayMember = "TfdSummary";
            teacherForDisciplineBox.ValueMember = "TeacherForDisciplineId";
            teacherForDisciplineBox.DataSource = tfdViewList;
             */
        }

        private void AddLesson_Load(object sender, EventArgs e)
        {
            var buildings = _repo.Buildings.GetAllBuildings();

            var startingPositionX = 10;
            var startingPositionY = 5;
            int buildingCounter = 1;
            foreach (var building in buildings)
            { 
                var buildingButton = new RadioButton
                {
                    Name = "bb_" + building.BuildingId,
                    Tag = building.BuildingId,
                    Width = 200,
                    Text = building.Name,
                    Location = new Point(startingPositionX, startingPositionY)
                };
                buildingButton.CheckedChanged += RadioButtonCheckedChanged;
                BuildingsPanel.Controls.Add(buildingButton);
                startingPositionX += 200;

                if (buildingCounter % 3 == 0)
                {
                    startingPositionX = 10;
                    startingPositionY += 20;
                }

                buildingCounter++;
            }

            var buildingsRowCount = (buildings.Count / 3) + ((buildings.Count % 3 == 0) ? 0 : 1);

            BuildingsPanel.Height = 8 + 20 * buildingsRowCount;


            // TFD load
            var tfdList = _repo.TeacherForDisciplines.GetAllTeacherForDiscipline();
            var tfdViewList = TfdView.TfdsToView(tfdList);
            tfdViewList = tfdViewList.OrderBy(tfdv => tfdv.TfdSummary).ToList();

            teacherForDisciplineBox.DataSource = tfdViewList;
            teacherForDisciplineBox.DisplayMember = "TfdSummary";
            teacherForDisciplineBox.ValueMember = "TeacherForDisciplineId";

            if (_tfdId != -1)
            {
                teacherForDisciplineBox.SelectedValue = _tfdId;
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
            foreach (var dow in Constants.DowLocal)
            {
                dowList.Add(new { Value = dow.Key, Text = dow.Value });
            }

            DayOfWeekListBox.ValueMember = "Value";
            DayOfWeekListBox.DisplayMember = "Text";
            DayOfWeekListBox.DataSource = dowList;            

            // Public comment
            publicComment.DataSource = Constants.LessonAddPublicComment;
            publicComment.SelectedIndex = 0;

            Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Execute_Click(object sender, EventArgs e)
        {            
            if (teacherForDisciplineBox.SelectedValue == null)
            {
                MessageBox.Show("Неправильно выбран TFD.");
                return;
            }

            var weekList = CommonFunctions.WeeksStringToList(lessonWeeks.Text);

            var tfd = _repo.TeacherForDisciplines.GetTeacherForDiscipline((int)teacherForDisciplineBox.SelectedValue);
            var disc = tfd.Discipline;
            var studentGroup = disc.StudentGroup;
            var sigFromGroup = _repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroup.StudentGroupId && !sig.Student.Expelled);
            var studentIdsInGroup = sigFromGroup.Select(studentsInGroupse => studentsInGroupse.Student.StudentId).ToList();
            var studentGroupsIds = _repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => studentIdsInGroup.Contains(sig.Student.StudentId)).Select(sing => sing.StudentGroup.StudentGroupId).Distinct();
                                    
            var rings = (
                    from object ringView in ringsListBox.SelectedItems
                    select _repo.Rings.GetRing(((RingView) ringView).RingId))
                .ToList();
            var ringIds = rings.Select(r => r.RingId).ToList();
            
            
            var calendarsList = (
                    from week in weekList
                    select _repo.CommonFunctions.GetDateFromDowAndWeek((int)DayOfWeekListBox.SelectedValue, week)
                    into date
                    select _repo.Calendars.FindCalendar(date)
                    into calendar
                    where calendar != null
                    select calendar)
                .ToList();

            var calendarIdsList = calendarsList.Select(c => c.CalendarId).ToList();

            // Есть уроки в это время
            var groupsLessons = _repo
                .Lessons
                .GetFiltredLessons(
                    l => studentGroupsIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId) && 
                         calendarIdsList.Contains(l.Calendar.CalendarId) && 
                         ringIds.Contains(l.Ring.RingId) &&
                         (l.State == 1));


            if (groupsLessons.Count != 0)
            {                
                var outOfMind = MessageBox.Show("У студентов группы есть занятия. Всё равно добавить?", "ЕГГОГ", MessageBoxButtons.YesNo);
                if (outOfMind == DialogResult.No)
                {
                    return;
                }
            }
            // Есть уроки в это время

            // Есть практика/каникулы и т.п. в это время
            var periods =
                _repo.CustomStudentGroupAttributes.GetFiltredCustomStudentGroupAttributes(
                    csga => studentGroupsIds.Contains(csga.StudentGroup.StudentGroupId) && csga.Value.EndsWith("+"));

            var lessonsPeriods = new List<CustomStudentGroupAttribute>();

            foreach (var period in periods)
            {
                var startDate = DateTime.ParseExact(period.Value.Split('@')[1], "dd.MM.yyyy",
                    CultureInfo.InvariantCulture);

                var endDate = DateTime.ParseExact(period.Value.Split('@')[2], "dd.MM.yyyy",
                    CultureInfo.InvariantCulture);

                foreach (var calendar in calendarsList)
                {
                    if ((calendar.Date.Date >= startDate.Date) && (calendar.Date.Date <= endDate.Date))
                    {
                        lessonsPeriods.Add(period);
                    }
                }
                
            }

            if (lessonsPeriods.Count != 0)
            {                
                var outOfMind = MessageBox.Show("У студентов группы есть периоды без занятий в это время. Всё равно добавить?", "ЕГГОГ", MessageBoxButtons.YesNo);
                if (outOfMind == DialogResult.No)
                {
                    return;
                }
            }

            // Есть практика/каникулы и т.п. в это время
                        
            var audWeekList = Utilities.GetAudWeeksList(auditoriums.Text);

            foreach (int week in weekList)
            {
                foreach (var ring in rings)
                {
                    var lesson = new Lesson
                    {
                        State = ProposedLesson.Checked ? 2 : (isActive.Checked ? 1 : 0),
                        TeacherForDiscipline = tfd, 
                        Ring = ring
                    };

                    var date = _repo.CommonFunctions.GetDateFromDowAndWeek((int)DayOfWeekListBox.SelectedValue, week);
                    var calendar = _repo.Calendars.FindCalendar(date) ?? new Calendar(date);
                    lesson.Calendar = calendar;

                    // Auditorium
                    Auditorium aud;
                    if (audList.SelectedIndex != -1)
                    {
                        aud = _repo.Auditoriums.Get((int)audList.SelectedValue);
                    }
                    else
                    {
                        if (audWeekList.Keys.Count == 1)
                        {
                            var lessonAud = audWeekList[0];
                            audWeekList.Clear();
                            foreach (var localWeek in weekList)
                            {
                                audWeekList.Add(localWeek, lessonAud);
                            }
                        }

                        //lesson.LengthInMinutes = audWeekList[week].Item2;

                        aud = _repo.Auditoriums.Find(audWeekList[week].Item1);
                        if (aud == null)
                        {
                            var firstBuilding = _repo.Buildings.GetFirstFiltredBuilding(b => true);

                            if (firstBuilding != null)
                            {
                                _repo.Auditoriums.Add(new Auditorium(audWeekList[week].Item1, firstBuilding));
                                aud = _repo.Auditoriums.Find(audWeekList[week].Item1);
                            }
                        }
                    }
                    lesson.Auditorium = aud;

                    // State
                    if (ProposedLesson.Checked)
                    {
                        lesson.State = 2;
                    }

                    _repo.Lessons.AddLesson(lesson, publicComment.Text, hiddenComment.Text);
                }
            }

            Close();
        }

        private void showAuds_Click(object sender, EventArgs e)
        {
            var audsForm = new Auditoriums(_repo);
            audsForm.Show();
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
                weekList = CommonFunctions.WeeksStringToList(lessonWeeks.Text);
            }
            catch
            {
                return;
            }

            if ((DayOfWeekListBox.SelectedIndex == -1) || (ringsListBox.SelectedItems.Count == 0) || (lessonWeeks.Text == ""))
            {
                return;
            }          
            
            var calendarIds = (
                    from cal in _repo.Calendars.GetAllCalendars()
                    where Constants.DowRemap[(int) cal.Date.DayOfWeek] - 1 == DayOfWeekListBox.SelectedIndex
                    let week = _repo.CommonFunctions.CalculateWeekNumber(cal.Date)
                    where weekList.Contains(week)
                    select cal.CalendarId)
                .ToList();

            var ringIds = (
                    from object ringView in ringsListBox.SelectedItems
                    select ((RingView) ringView).RingId)
                .ToList();

            var res = _repo.CommonFunctions.GetFreeAuditoriumsAtDowTime(calendarIds, ringIds, _selectedBuildingId, proposedIncluded.Checked);

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

                if (button != null)
                {
                    button.Checked = false;
                }
            }

            _selectedBuildingId = -1;
        }

        private void teacherForDisciplineBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRings();
        }

        private void UpdateRings()
        {
            if (filterRings.Checked)
            {
                UpdateTfdRings();
            }
            else
            {
                var ringsList = _repo.Rings.GetAllRings()
                .OrderBy(r => r.Time.TimeOfDay)
                .ToList();
                var ringsView = RingView.RingsToView(ringsList);

                ringsListBox.DataSource = ringsView;
                ringsListBox.DisplayMember = "Time";
                ringsListBox.ValueMember = "RingId";
            }
        }

        private void UpdateTfdRings()
        {
            if (!(teacherForDisciplineBox.SelectedValue is int))
            {
                return;
            }

            var tfdId = (int)teacherForDisciplineBox.SelectedValue;
            var tfd = _repo.TeacherForDisciplines.GetTeacherForDiscipline(tfdId);

            var teacher = tfd.Teacher;

            var ringsForTeacher = _repo
                .CustomTeacherAttributes
                .GetFiltredCustomTeacherAttributes(cta => 
                    cta.Teacher.TeacherId == teacher.TeacherId &&
                    cta.Key == "TeacherRing")
                .Select(cta => _repo.Rings.GetRing(int.Parse(cta.Value)))
                .OrderBy(r => r.Time.TimeOfDay)
                .ToList();

            // Rings load            
            var ringsView = RingView.RingsToView(ringsForTeacher);

            ringsListBox.DataSource = ringsView;
            ringsListBox.DisplayMember = "Time";
            ringsListBox.ValueMember = "RingId";
        }

        private void filterRings_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRings();
        }

        private void proposedIncluded_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFreeAuds();
        }
    }
}
