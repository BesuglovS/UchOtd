using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Repositories.Common;
using UchOtd.Schedule.Views;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class Wishes : Form
    {
        private readonly ScheduleRepository _repo;

        private bool _listBoxInitialization = true;

        public static bool NeedsUpdateAfterChoosingRings = false;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public Wishes(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void Wishes_Load(object sender, EventArgs e)
        {
            _listBoxInitialization = true;
            var teachers = _repo
                .Teachers
                .GetAllTeachers()
                .OrderBy(t => t.FIO)
                .ToList();

            teacherList.ValueMember = "TeacherId";
            teacherList.DisplayMember = "FIO";
            teacherList.DataSource = teachers;

            RingsList.ClearSelected();
            _listBoxInitialization = false;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshWishes();
            RefreshRings();
        }

        private void RefreshRings()
        {
            _listBoxInitialization = true;

            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            var teacherRingIds = _repo
                .CustomTeacherAttributes
                .GetFiltredCustomTeacherAttributes(cta => (cta.Teacher.TeacherId == teacher.TeacherId) && (cta.Key == "TeacherRing"))
                .Select(cta => int.Parse(cta.Value))
                .ToList();

            var allRingViews = RingView.RingsToView(_repo.Rings.GetAllRings().OrderBy(r => r.Time.TimeOfDay).ToList());

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
            
            _listBoxInitialization = false;
        }
        
        private void RefreshWishes()
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            var wishView = RingWeekView.GetRingWeekView(_repo, teacher);

            wishesView.DataSource = wishView;

            var wish = _repo.CustomTeacherAttributes.GetCustomTeacherAttribute(teacher, "WindowsPossible");
            if (wish != null)
            {
                windowsPossible.Checked = true;
                windowsPossibleSize.Text = wish.Value;
            }

            wish = _repo.CustomTeacherAttributes.GetCustomTeacherAttribute(teacher, "LessonsLimit");
            if (wish != null)
            {
                LessonsLimitedPerDay.Checked = true;
                LessonsLimitPerDay.Text = wish.Value;
            }

            wish = _repo.CustomTeacherAttributes.GetCustomTeacherAttribute(teacher, "FitAllLessonsDaysCount");
            if (wish != null)
            {
                FitAllLessonsInXDays.Checked = true;
                FitAllLessonsDaysCount.Text = wish.Value;
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
            foreach (var calendar in _repo.Calendars.GetAllCalendars())
            {
                var rings = _repo
                    .CustomTeacherAttributes
                    .GetFiltredCustomTeacherAttributes(cta =>
                        cta.Teacher.TeacherId == teacher.TeacherId &&
                        cta.Key == "TeacherRing")
                    .Select(cta => _repo.Rings.GetRing(int.Parse(cta.Value)));

                foreach (var ring in rings)
                {
                    var wish = new TeacherWish(teacher, calendar, ring, wishAmount);

                    _repo.TeacherWishes.AddOrUpdateTeacherWish(wish);
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            var teacherWishesIds = _repo
                .TeacherWishes
                .GetFiltredTeacherWishes(w => w.Teacher.TeacherId == teacher.TeacherId)
                .Select(w => w.TeacherWishId);

            foreach (var teacherWishId in teacherWishesIds)
            {
                _repo.TeacherWishes.RemoveTeacherWish(teacherWishId);
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

            int simpleWish;
            if (int.TryParse(cell.Value.ToString(), out simpleWish))
            {
                var calendars = _repo.Calendars.GetDowCalendars(dow);

                var ring = _repo.Rings.GetRing(((List<RingWeekView>)wishesView.DataSource)[e.RowIndex].RingId);

                foreach (var calendar in calendars)
                {
                    var teacherWish = new TeacherWish(teacher, calendar, ring, simpleWish);

                    _repo.TeacherWishes.AddOrUpdateTeacherWish(teacherWish);
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
                    var weeks = CommonFunctions.WeeksStringToList(wishParts[0]);
                    var wish = int.Parse(wishParts[1]);                    

                    foreach (var week in weeks)
                    {
                        var calendar = _repo.CommonFunctions.GetCalendarFromDowAndWeek(dow, week);
                        var ring = _repo.Rings.GetRing(((List<RingWeekView>)wishesView.DataSource)[e.RowIndex].RingId);

                        var teacherWish = new TeacherWish(teacher, calendar, ring, wish);

                        _repo.TeacherWishes.AddOrUpdateTeacherWish(teacherWish);
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

                var calendars = _repo.Calendars.GetDowCalendars(dow);

                var ring = _repo.Rings.GetRing(((List<RingWeekView>)wishesView.DataSource)[cell.RowIndex].RingId);

                foreach (var calendar in calendars)
                {
                    var teacherWish = new TeacherWish(teacher, calendar, ring, wishValue);

                    _repo.TeacherWishes.AddOrUpdateTeacherWish(teacherWish);
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
                    var wish = new CustomTeacherAttribute(teacher, "WindowsPossible", windowSize.ToString(CultureInfo.InvariantCulture));

                    _repo.CustomTeacherAttributes.AddOrUpdateCustomTeacherAttribute(wish);
                }
                else
                {
                    MessageBox.Show(windowsPossibleSize.Text, "Неправильный формат числа недель.");
                }
            }
            else
            {
                var wish = _repo.CustomTeacherAttributes.GetCustomTeacherAttribute(teacher, "WindowsPossible");

                if (wish != null)
                {
                    _repo.CustomTeacherAttributes.RemoveCustomTeacherAttribute(wish.CustomTeacherAttributeId);
                }
            }            
        }

        private void LessonsLimitedPerDay_CheckStateChanged(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            if (LessonsLimitedPerDay.Checked)
            {
                int lessonsLimit;
                if (int.TryParse(LessonsLimitPerDay.Text, out lessonsLimit))
                {
                    var wish = new CustomTeacherAttribute(teacher, "LessonsLimit", lessonsLimit.ToString(CultureInfo.InvariantCulture));

                    _repo.CustomTeacherAttributes.AddOrUpdateCustomTeacherAttribute(wish);
                }
                else
                {
                    MessageBox.Show(windowsPossibleSize.Text, "Неправильный формат лимита.");
                }
            }
            else
            {
                var wish = _repo.CustomTeacherAttributes.GetCustomTeacherAttribute(teacher, "LessonsLimit");

                if (wish != null)
                {
                    _repo.CustomTeacherAttributes.RemoveCustomTeacherAttribute(wish.CustomTeacherAttributeId);
                }
            }  
        }

        private void FitAllLessonsInXDays_CheckStateChanged(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            if (FitAllLessonsInXDays.Checked)
            {
                int fitAllLessonsDaysInt;
                if (int.TryParse(FitAllLessonsDaysCount.Text, out fitAllLessonsDaysInt))
                {
                    var wish = new CustomTeacherAttribute(teacher, "FitAllLessonsDaysCount", fitAllLessonsDaysInt.ToString(CultureInfo.InvariantCulture));

                    _repo.CustomTeacherAttributes.AddOrUpdateCustomTeacherAttribute(wish);
                }
                else
                {
                    MessageBox.Show(windowsPossibleSize.Text, "Неправильный формат лимита.");
                }
            }
            else
            {
                var wish = _repo.CustomTeacherAttributes.GetCustomTeacherAttribute(teacher, "FitAllLessonsDaysCount");

                if (wish != null)
                {
                    _repo.CustomTeacherAttributes.RemoveCustomTeacherAttribute(wish.CustomTeacherAttributeId);
                }
            }  
        }

        private void RingsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_listBoxInitialization)
            {
                return;
            }

            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            var teacherRingIds = _repo
                .CustomTeacherAttributes
                .GetFiltredCustomTeacherAttributes(cta => cta.Teacher.TeacherId == teacher.TeacherId && cta.Key == "TeacherRing")
                .Select(cta => int.Parse(cta.Value))
                .ToList();


            for (int i = 0; i < RingsList.Items.Count; i++)
            {
                bool selected = RingsList.GetSelected(i);
                int ringId = ((List<RingView>)RingsList.DataSource)[i].RingId;
                var ring = _repo.Rings.GetRing(ringId);

                if (selected && !teacherRingIds.Contains(ringId))
                {
                    var newTeacherRingAttribute = 
                        new CustomTeacherAttribute(teacher, "TeacherRing", ringId.ToString(CultureInfo.InvariantCulture));
                    _repo.CustomTeacherAttributes.AddCustomTeacherAttribute(newTeacherRingAttribute);

                    var newTeacherWishList = new List<TeacherWish>();

                    for (int dow = 1; dow <= 7; dow++)
                    {
                        newTeacherWishList.AddRange(
                            _repo.Calendars.GetDowCalendars(dow)
                            .Select(calendar => new TeacherWish(teacher, calendar, ring, 0)));
                    }

                    _repo.TeacherWishes.AddTeacherWishRange(newTeacherWishList);

                    RefreshWishes();

                    break;
                }

                if (!selected && teacherRingIds.Contains(ringId))
                {
                    var teacherRingAttribute = _repo
                        .CustomTeacherAttributes
                        .GetFirstFiltredCustomTeacherAttribute(cta =>
                        cta.Teacher.TeacherId == teacher.TeacherId &&
                        cta.Key == "TeacherRing" &&
                        cta.Value == ringId.ToString(CultureInfo.InvariantCulture));

                    _repo.CustomTeacherAttributes.RemoveCustomTeacherAttribute(teacherRingAttribute.CustomTeacherAttributeId);

                    var teacherWishes = _repo
                        .TeacherWishes
                        .GetFiltredTeacherWishes(tw =>
                            tw.Teacher.TeacherId == teacher.TeacherId &&
                            tw.Ring.RingId == ringId);

                    foreach (var wish in teacherWishes)
                    {
                        _repo.TeacherWishes.RemoveTeacherWish(wish.TeacherWishId);
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

        private void removeAllWishes_Click(object sender, EventArgs e)
        {
            foreach (var wish in _repo.TeacherWishes.GetAllTeacherWishes())
            {
                _repo.TeacherWishes.RemoveTeacherWish(wish.TeacherWishId);
            }

            foreach (var ring in _repo.CustomTeacherAttributes.GetFiltredCustomTeacherAttributes(attr => attr.Key == "TeacherRing"))
            {
                _repo.CustomTeacherAttributes.RemoveCustomTeacherAttribute(ring.CustomTeacherAttributeId);
            }
        }

        private void FillAllWishesAsEmpty_Click(object sender, EventArgs e)
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }

            _tokenSource = new CancellationTokenSource();
            _cToken = _tokenSource.Token;

            Task.Factory.StartNew(() =>
            {
                
                progress.BeginInvoke(new Action(() => progress.Text = "Удаление"));
                var wishIds = _repo.TeacherWishes.GetAllTeacherWishes().Select(tw => tw.TeacherWishId).ToList();
                var wishCount = wishIds.Count;
                for (int i = 0; i < wishIds.Count; i++)                
                {
                    _repo.TeacherWishes.RemoveTeacherWish(wishIds[i]);

                    if (i % 500 == 0)
                    {
                        int counter = i;
                        progress.BeginInvoke(new Action(() => progress.Text = "Удаление - " + counter + " / " + wishCount));
                    }
                }

                progress.BeginInvoke(new Action(() => progress.Text = "Удаление звонков преподавателей"));

                foreach (var ring in _repo.CustomTeacherAttributes.GetFiltredCustomTeacherAttributes(attr => attr.Key == "TeacherRing"))
                {
                    _repo.CustomTeacherAttributes.RemoveCustomTeacherAttribute(ring.CustomTeacherAttributeId);
                }

                var standard80RingsStrings = new List<string>
                {"8:00", "9:25", "11:05", "12:35", "14:00", "15:40", "17:05", "18:35"};

                var rings = _repo.Rings.GetFiltredRings(r => standard80RingsStrings.Contains(r.Time.ToString("H:mm"))).ToList();

                var teachers = _repo.Teachers.GetAllTeachers().OrderBy(t => t.FIO).ToList();
                var teachersCount = teachers.Count;

                
                for (int i = 0; i < teachersCount; i++)
                {                
                    var teacher = teachers[i];

                    int counter = i;
                    progress.BeginInvoke(new Action(() => progress.Text = teacher.FIO + " " + counter + " / " + teachersCount + " = " + (counter / teachersCount).ToString("F2")));

                    foreach (var ring in rings)
                    {
                        var newRing = new CustomTeacherAttribute(teacher, "TeacherRing", ring.RingId.ToString(CultureInfo.InvariantCulture));

                        _repo.CustomTeacherAttributes.AddCustomTeacherAttribute(newRing);
                    }

                    foreach (var calendar in _repo.Calendars.GetAllCalendars().OrderBy(c => c.Date.Date))
                    {
                        foreach (var ring in rings)
                        {
                            var newWish = new TeacherWish(teacher, calendar, ring, 0);

                            _repo.TeacherWishes.AddTeacherWish(newWish);
                        }
                    }
                }

                progress.BeginInvoke(new Action(() => progress.Text = ""));
            }, _cToken);            
        }       
    }
}
