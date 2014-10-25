using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms
{
    public partial class Wishes : Form
    {
        private readonly ScheduleRepository _repo;

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
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshWishes();
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

            wishesView.Columns[8].Visible = false;
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            SetTeacherWishes(teacher, 100);

            RefreshWishes();
        }

        private void No_Click(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            SetTeacherWishes(teacher, 0);

            RefreshWishes();
        }

        private void SetTeacherWishes(Teacher teacher, int wishAmount)
        {
            foreach (var calendar in _repo.GetAllCalendars())
            {
                foreach (var ring in _repo.GetAllRings())
                {
                    var wish = new TeacherWish(teacher, calendar, ring, wishAmount);

                    _repo.UpdateOrAddTeacherWish(wish);
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
            if (e.KeyCode == Keys.Tab && wishesView.CurrentCell != null && wishesView.CurrentCell.ColumnIndex > 1)
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

                    _repo.UpdateOrAddTeacherWish(teacherWish);
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

                        _repo.UpdateOrAddTeacherWish(teacherWish);
                    }

	            }
	            catch (Exception exc)
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

                if (cell.ColumnIndex > 1)
                {
                    continue;
                }

                var calendars = _repo.GetDOWCalendars(dow);

                var ring = _repo.GetRing(((List<RingWeekView>)wishesView.DataSource)[cell.RowIndex].RingId);

                foreach (var calendar in calendars)
                {
                    var teacherWish = new TeacherWish(teacher, calendar, ring, wishValue);

                    _repo.UpdateOrAddTeacherWish(teacherWish);
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

                SetTeacherWishes(teacher, simpleWish);

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

                    _repo.UpdateOrAddCustomTeacherWish(wish);
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

                    _repo.UpdateOrAddCustomTeacherWish(wish);
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

                    _repo.UpdateOrAddCustomTeacherWish(wish);
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
    }
}
