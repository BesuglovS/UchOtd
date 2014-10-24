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

            FormatView();
        }

        private void FormatView()
        {
            wishesView.Columns[1].HeaderText = "Понедельник";
            wishesView.Columns[2].HeaderText = "Вторник";
            wishesView.Columns[3].HeaderText = "Среда";
            wishesView.Columns[4].HeaderText = "Четверг";
            wishesView.Columns[5].HeaderText = "Пятница";
            wishesView.Columns[6].HeaderText = "Суббота";

            wishesView.Columns[7].Visible = false;
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            SetTeacherWishes(teacher, 100);
        }

        private void No_Click(object sender, EventArgs e)
        {
            var teacher = ((List<Teacher>)teacherList.DataSource)[teacherList.SelectedIndex];

            SetTeacherWishes(teacher, 0);
        }

        private void SetTeacherWishes(Teacher teacher, int wishAmount)
        {
            foreach (var calendar in _repo.GetAllCalendars())
            {
                foreach (var ring in _repo.GetAllRings())
                {
                    var wish = new TeacherWish(teacher, calendar, ring, wishAmount);

                    _repo.UpdateOrSetTeacherWish(wish);
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
        }        
    }
}
