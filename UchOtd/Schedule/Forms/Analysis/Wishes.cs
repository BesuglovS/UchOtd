using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.Views.DBListViews;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms
{
    public partial class Wishes : Form
    {
        private readonly ScheduleRepository _repo;

        private bool listBoxInitialization = true;

        public static bool NeedsUpdateAfterChoosingRings = false;

        public Wishes(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void Wishes_Load(object sender, EventArgs e)
        {
            var teachers = _repo.GetAllTeachers()
                .OrderBy(t => t.FIO)
                .ToList();

            teacherList.ValueMember = "TeacherId";
            teacherList.DisplayMember = "FIO";
            teacherList.DataSource = teachers;

            RingsList.ClearSelected();
            listBoxInitialization = false;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshWishes();
            RefreshRings();
        }

        private void RefreshRings()
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            var teacherRingIds = _repo
                .GetFiltredTeacherRings(tr => tr.Teacher.TeacherId == teacher.TeacherId)
                .Select(tr => tr.Ring.RingId)                
                .ToList();
            
            var allRingViews = RingView.RingsToView(_repo.GetAllRings().OrderBy(r => r.Time.TimeOfDay).ToList());

            listBoxInitialization = true;
            
            RingsList.ValueMember = "RingId";
            RingsList.DisplayMember = "Time";
            RingsList.DataSource = allRingViews;

            RingsList.ClearSelected();

            for (int i = 0; i < RingsList.Items.Count; i++)
            {
                var ringId = allRingViews[i].RingId;

                if (teacherRingIds.Contains(ringId))
                {
                    RingsList.SetSelected(i, true);
                }
            }
            
            listBoxInitialization = false;
        }
        
        private void RefreshWishes()
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            var wishView = RingWeekView.GetRingWeekView(_repo, teacher);

            wishesView.DataSource = wishView;

            CustomTeacherWish wish;

            wish = _repo.GetCustomTeacherWish(teacher, "WindowsPossible");
            if (wish != null)
            {
                windowsPossible.Checked = true;
                windowsPossibleSize.Text = wish.Value.ToString();
            }

            wish = _repo.GetCustomTeacherWish(teacher, "LessonsLimit");
            if (wish != null)
            {
                LessonsLimitedPerDay.Checked = true;
                LessonsLimitPerDay.Text = wish.Value.ToString();
            }

            wish = _repo.GetCustomTeacherWish(teacher, "FitAllLessonsDaysCount");
            if (wish != null)
            {
                FitAllLessonsInXDays.Checked = true;
                FitAllLessonsDaysCount.Text = wish.Value.ToString();
            }


            FormatView();
        }

        private void FormatView()
        {
            wishesView.Columns[0].Visible = false;

            wishesView.Columns[1].HeaderText = "Время";
            wishesView.Columns[2].HeaderText = "Понедельник";
            wishesView.Columns[3].HeaderText = "Вторник";
            wishesView.Columns[4].HeaderText = "Среда";
            wishesView.Columns[5].HeaderText = "Четверг";
            wishesView.Columns[6].HeaderText = "Пятница";
            wishesView.Columns[7].HeaderText = "Суббота";
            wishesView.Columns[8].HeaderText = "Воскресенье";

            //wishesView.Columns[8].Visible = false;
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            SetTeacherWishesForTeacherRings(teacher, 100);

            RefreshWishes();
        }

        private void No_Click(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            SetTeacherWishesForTeacherRings(teacher, 0);

            RefreshWishes();
        }

        private void SetTeacherWishesForTeacherRings(Teacher teacher, int wishAmount)
        {
            foreach (var calendar in _repo.GetAllCalendars())
            {
                var teacherRings = _repo.GetTeacherRings(teacher);
                var rings = teacherRings.Select(tr => tr.Ring);

                foreach (var ring in rings)
                {
                    var wish = new TeacherWish(teacher, calendar, ring, wishAmount);

                    _repo.AddOrUpdateTeacherWish(wish);
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            var teacherWishesIds = _repo
                .GetFiltredTeacherWishes(w => w.Teacher.TeacherId == teacher.TeacherId)
                .Select(w => w.TeacherWishId);

            foreach (var teacherWishId in teacherWishesIds)
            {
                _repo.RemoveTeacherWish(teacherWishId);
            }

            RefreshWishes();
        }

        private void wishesView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && wishesView.CurrentCell != null && wishesView.CurrentCell.ColumnIndex > 1)
            {
                e.Handled = true;
                DataGridViewCell cell = wishesView.Rows[wishesView.CurrentCell.RowIndex].Cells[wishesView.CurrentCell.ColumnIndex];
                wishesView.CurrentCell = cell;
                wishesView.BeginEdit(true);
            }
        }

        private void wishesView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex == -1) || (e.ColumnIndex == -1))
            {
                return;
            }

            var dow = e.ColumnIndex - 1;

            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            DataGridViewCell cell = wishesView.Rows[e.RowIndex].Cells[e.ColumnIndex];

            var textSaved = cell.Value.ToString();

            bool correct = true;

            var simpleWish = -1;
            if (int.TryParse(cell.Value.ToString(), out simpleWish))
            {
                var calendars = _repo.GetDOWCalendars(dow);

                var ring = _repo.GetRing(((List<RingWeekView>)wishesView.DataSource)[e.RowIndex].RingId);

                foreach (var calendar in calendars)
                {
                    var teacherWish = new TeacherWish(teacher, calendar, ring, simpleWish);

                    _repo.AddOrUpdateTeacherWish(teacherWish);
                }

                RefreshWishes();

                return;
            }
            
            var wishesParts = cell.Value.ToString()
                .Split(';')
                .Select(p => p.Replace(" ", string.Empty));

            foreach (var item in wishesParts)
            {
                var wishParts = item.Split('@');

                if (wishParts.Count() != 2)
                {
                    correct = false;
                    break;
                }
                
                try 
	            {
                    var weeks = ScheduleRepository.WeeksStringToList(wishParts[0]);
                    var wish = int.Parse(wishParts[1]);                    

                    foreach (var week in weeks)
                    {
                        var calendar = _repo.GetCalendarFromDowAndWeek(dow, week);
                        var ring = _repo.GetRing(((List<RingWeekView>)wishesView.DataSource)[e.RowIndex].RingId);

                        var teacherWish = new TeacherWish(teacher, calendar, ring, wish);

                        _repo.AddOrUpdateTeacherWish(teacherWish);
                    }

	            }
	            catch
	            {
                    correct = false;
                    break;
	            }                
            }

            if (correct)
            {
                RefreshWishes();
            }
            else
            {
                cell.Value = textSaved;
                MessageBox.Show(cell.Value.ToString(), "Неправильный формат");
            }
        }

        private void MaxSelected_Click(object sender, EventArgs e)
        {
            SetSelectionWish(100);

            RefreshWishes();
        }

        private void SetSelectionWish(int wishValue)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            DataGridViewSelectedCellCollection collection = wishesView.SelectedCells;

            for (int i = 0; i < collection.Count; i++)
            {
                var cell = collection[i];
                var dow = cell.ColumnIndex - 1;

                if (cell.ColumnIndex <= 1)
                {
                    continue;
                }

                var calendars = _repo.GetDOWCalendars(dow);

                var ring = _repo.GetRing(((List<RingWeekView>)wishesView.DataSource)[cell.RowIndex].RingId);

                foreach (var calendar in calendars)
                {
                    var teacherWish = new TeacherWish(teacher, calendar, ring, wishValue);

                    _repo.AddOrUpdateTeacherWish(teacherWish);
                }
            }
        }

        private void MinSelected_Click(object sender, EventArgs e)
        {
            SetSelectionWish(0);

            RefreshWishes();
        }

        private void ValueSelected_Click(object sender, EventArgs e)
        {
            int simpleWish;
            if (int.TryParse(wishToSetValue.Text, out simpleWish))
            {
                SetSelectionWish(simpleWish);

                RefreshWishes();
            }
            else
            {
                MessageBox.Show(wishToSetValue.Text, "Неправильный формат");
            }
        }

        private void OneValue_Click(object sender, EventArgs e)
        {
            int simpleWish;
            if (int.TryParse(wishToSetValue.Text, out simpleWish))
            {
                var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

                SetTeacherWishesForTeacherRings(teacher, simpleWish);

                RefreshWishes();
            }
            else
            {
                MessageBox.Show(wishToSetValue.Text, "Неправильный формат");
            }
        }

        private void windowsPossible_CheckStateChanged(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];
         
            if (windowsPossible.Checked)
            {
                int windowSize;
                if (int.TryParse(windowsPossibleSize.Text, out windowSize))
                {
                    var wish = new CustomTeacherWish(teacher, "WindowsPossible", windowSize.ToString());

                    _repo.AddOrUpdateCustomTeacherWish(wish);
                }
                else
                {
                    MessageBox.Show(windowsPossibleSize.Text, "Неправильный формат числа недель.");
                }
            }
            else
            {
                var wish = _repo.GetCustomTeacherWish(teacher, "WindowsPossible");

                if (wish != null)
                {
                    _repo.RemoveCustomTeacherWish(wish.CustomTeacherWishId);
                }
            }            
        }

        private void LessonsLimitedPerDay_CheckStateChanged(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            if (LessonsLimitedPerDay.Checked)
            {
                int LessonsLimit;
                if (int.TryParse(LessonsLimitPerDay.Text, out LessonsLimit))
                {
                    var wish = new CustomTeacherWish(teacher, "LessonsLimit", LessonsLimit.ToString());

                    _repo.AddOrUpdateCustomTeacherWish(wish);
                }
                else
                {
                    MessageBox.Show(windowsPossibleSize.Text, "Неправильный формат лимита.");
                }
            }
            else
            {
                var wish = _repo.GetCustomTeacherWish(teacher, "LessonsLimit");

                if (wish != null)
                {
                    _repo.RemoveCustomTeacherWish(wish.CustomTeacherWishId);
                }
            }  
        }

        private void FitAllLessonsInXDays_CheckStateChanged(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            if (FitAllLessonsInXDays.Checked)
            {
                int FitAllLessonsDaysInt;
                if (int.TryParse(FitAllLessonsDaysCount.Text, out FitAllLessonsDaysInt))
                {
                    var wish = new CustomTeacherWish(teacher, "FitAllLessonsDaysCount", FitAllLessonsDaysInt.ToString());

                    _repo.AddOrUpdateCustomTeacherWish(wish);
                }
                else
                {
                    MessageBox.Show(windowsPossibleSize.Text, "Неправильный формат лимита.");
                }
            }
            else
            {
                var wish = _repo.GetCustomTeacherWish(teacher, "FitAllLessonsDaysCount");

                if (wish != null)
                {
                    _repo.RemoveCustomTeacherWish(wish.CustomTeacherWishId);
                }
            }  
        }

        private void RingsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxInitialization)
            {
                return;
            }

            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            var teacherRingIds = _repo
                .GetFiltredTeacherRings(tr => tr.Teacher.TeacherId == teacher.TeacherId)
                .Select(tr => tr.Ring.RingId)
                .ToList();


            for (int i = 0; i < RingsList.Items.Count; i++)
            {
                bool selected = RingsList.GetSelected(i);
                int ringId = ((List<RingView>)RingsList.DataSource)[i].RingId;
                var ring = _repo.GetRing(ringId);

                if (selected && !teacherRingIds.Contains(ringId))
                {
                    var newTeacherRing = new TeacherRing(teacher, _repo.GetRing(ringId));
                    _repo.AddTeacherRing(newTeacherRing);

                    var newTeacherWishList = new List<TeacherWish>();

                    for (int dow = 1; dow <= 6; dow++)
                    {
                        newTeacherWishList.AddRange(
                            _repo.GetDOWCalendars(dow)
                            .Select(calendar => new TeacherWish(teacher, calendar, ring, 0)));
                    }

                    _repo.AddTeacherWishRange(newTeacherWishList);

                    RefreshWishes();

                    break;
                }

                if (!selected && teacherRingIds.Contains(ringId))
                {
                    var teacherRing = _repo.GetFirstFiltredTeacherRing(tr =>
                        tr.Teacher.TeacherId == teacher.TeacherId &&
                        tr.Ring.RingId == ringId);

                    _repo.RemoveTeacherRing(teacherRing.TeacherRingId);

                    var teacherWishes = _repo
                        .GetFiltredTeacherWishes(tw =>
                            tw.Teacher.TeacherId == teacher.TeacherId &&
                            tw.Ring.RingId == ringId);

                    foreach (var wish in teacherWishes)
                    {
                        _repo.RemoveTeacherWish(wish.TeacherWishId);
                    }

                    RefreshWishes();

                    break;
                }
                
            }
        }

        private void chooseRings_Click(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            var chooseRingsForm = new ChooseRings(_repo, teacher);
            chooseRingsForm.ShowDialog();

            if (NeedsUpdateAfterChoosingRings)
            {
                RefreshWishes();
                RefreshRings();
            }

            NeedsUpdateAfterChoosingRings = false;
        }        
    }
}
