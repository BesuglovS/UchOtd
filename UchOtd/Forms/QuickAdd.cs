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
using UchOtd.Schedule.Views.DBListViews;
using UchOtd.Views;

namespace UchOtd.Forms
{
    public partial class QuickAdd : Form
    {
        ScheduleRepository _repo;
        List<TeacherForDiscipline> tfds;
        List<Auditorium> auds;
        List<dow> dows;

        List<TimeSpan> rings = new List<TimeSpan> {
            new TimeSpan(08, 00, 00),
            new TimeSpan(08, 45, 00),
            new TimeSpan(09, 40, 00),
            new TimeSpan(10, 35, 00),
            new TimeSpan(11, 25, 00),
            new TimeSpan(12, 15, 00),
            new TimeSpan(13, 05, 00),
            new TimeSpan(14, 00, 00),
            new TimeSpan(14, 50, 00),
            new TimeSpan(15, 45, 00),
            new TimeSpan(16, 35, 00),
            new TimeSpan(17, 25, 00),
            new TimeSpan(18, 15, 00),
            new TimeSpan(19, 00, 00)
        };

        public QuickAdd(ScheduleRepository repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        public class dow
        {
            public int dowValue { get; set; }
            public string dowString { get; set; }
        }

        private void QuickAdd_Load(object sender, EventArgs e)
        {
            var groups = _repo.StudentGroups.GetAllStudentGroups().OrderBy(sg => sg.Name).ToList();
            studentGroup.ValueMember = "StudentGroupId";
            studentGroup.DisplayMember = "Name";
            studentGroup.DataSource = groups;

            var allRings = _repo.Rings.GetAllRings().OrderBy(r => r.Time.TimeOfDay).ToList();
            var ringViews = RingView.RingsToView(allRings);
            ringsBox.ValueMember = "RingId";
            ringsBox.DisplayMember = "Time";
            ringsBox.DataSource = ringViews;

            auds = _repo.Auditoriums.GetAll().OrderBy(a => a.Name).ToList();
            Auditorium.ValueMember = "AuditoriumId";
            Auditorium.DisplayMember = "Name";
            Auditorium.DataSource = auds;

            dows = new List<dow> {
                new dow() { dowValue = 1, dowString = "Понедельник" },
                new dow() { dowValue = 2, dowString = "Вторник" },
                new dow() { dowValue = 3, dowString = "Среда" },
                new dow() { dowValue = 4, dowString = "Четверг" },
                new dow() { dowValue = 5, dowString = "Пятница" },
                new dow() { dowValue = 6, dowString = "Суббота" },
                new dow() { dowValue = 7, dowString = "Воскресенье" }
            };
            dowBox.ValueMember = "dowValue";
            dowBox.DisplayMember = "dowString";
            dowBox.DataSource = dows;
        }

        private List<int> StudentGroupIdsFromGroupId(int groupId)
        {
            var studentIds = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId && !sig.Student.Expelled)
                .Select(stig => stig.Student.StudentId)
                .ToList();

            var groupsListIds = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                .Select(stig => stig.StudentGroup.StudentGroupId)
                .Distinct()
                .ToList();
            return groupsListIds;
        }

        private void studentGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                var groupId = (int) studentGroup.SelectedValue;
                var groupIds = StudentGroupIdsFromGroupId(groupId);

                var disciplines = _repo.Disciplines.GetFiltredDisciplines(d => groupIds.Contains(d.StudentGroup.StudentGroupId));

                var IdsToRemove = new List<int>();

                tfds = new List<TeacherForDiscipline>();
                for (int i = 0; i < disciplines.Count(); i++)
                {
                    var discipline = disciplines[i];
                    var tefd = _repo.TeacherForDisciplines
                        .GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discipline.DisciplineId);
                    if (tefd != null)
                    {
                        tfds.Add(tefd);
                    }
                }

                var views = tfdView.FromTfdList(tfds);
                discipline.ValueMember = "TeacherForDisciplineId";
                discipline.DisplayMember = "Summary";
                discipline.DataSource = views;
            }
        }

        private void Auditorium_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Auditorium.Text == "")
                {
                    auds = _repo.Auditoriums.GetAll().OrderBy(a => a.Name).ToList();
                    Auditorium.ValueMember = "AuditoriumId";
                    Auditorium.DisplayMember = "Name";
                    Auditorium.DataSource = auds;
                    return;
                }

                var filteredAuds = auds.Where(a => a.Name.ToLower().Contains(Auditorium.Text.ToLower())).ToList();

                Auditorium.ValueMember = "AuditoriumId";
                Auditorium.DisplayMember = "Name";
                Auditorium.DataSource = filteredAuds;
                Auditorium.SelectionStart = discipline.Text.Length - 1;
                Auditorium.DroppedDown = true;
            }
        }

        private void AddLesson_Click(object sender, EventArgs e)
        {
            var semesterSterts = _repo.CommonFunctions.GetSemesterStarts();
            var week = (int) weekNum.Value;
            var date = semesterSterts.AddDays((week - 1) * 7 + ((int)dowBox.SelectedValue) - 1);

            var calendar = _repo.Calendars.GetAllCalendars()
                .FirstOrDefault(c =>
                    c.Date.Year == date.Year &&
                    c.Date.Month == date.Month &&
                    c.Date.Day == date.Day);

            if (calendar == null)
            {
                MessageBox.Show("Нет такой даты в расписании. " + date.ToString("dd.MM.yyyy"), "Ошибка");
            }

            var ring = _repo.Rings.GetRing((int)ringsBox.SelectedValue);

            var tfd = _repo.TeacherForDisciplines.GetTeacherForDiscipline((int) discipline.SelectedValue);

            var aud = _repo.Auditoriums.Get((int)Auditorium.SelectedValue);

            var lesson = new Lesson() {
                Auditorium = aud,
                Calendar = calendar,
                Ring = ring,
                State = 1,
                TeacherForDiscipline = tfd
            };
            _repo.Lessons.AddLesson(lesson);

            var rtod = ring.Time.TimeOfDay;
            var ringsIndex = -1;
            for (int i = 0; i < rings.Count; i++)
            {
                if (rtod == rings[i])
                {
                    ringsIndex = i;
                    break;
                }
            }

            if (ringsIndex == -1 || ringsIndex == rings.Count-1)
            {
                return;
            }

            var newRingDot = rings[ringsIndex + 1];

            var newRing = _repo.Rings.GetFirstFiltredRing(r => r.Time.Hour == newRingDot.Hours && r.Time.Minute == newRingDot.Minutes);
            if (newRing == null)
            {
                return;
            }

            ringsBox.SelectedValue = newRing.RingId;

            discipline.Focus();
        }

        private void discipline_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                var views = tfdView.FromTfdList(tfds).Where(tfdv => tfdv.Summary.ToLower().Contains(discipline.Text.ToLower())).ToList();

                if (discipline.Text == "")
                {
                    var views2 = tfdView.FromTfdList(tfds);
                    discipline.ValueMember = "TeacherForDisciplineId";
                    discipline.DisplayMember = "Summary";
                    discipline.DataSource = views2;
                    return;
                }

                discipline.ValueMember = "TeacherForDisciplineId";
                discipline.DisplayMember = "Summary";
                discipline.DataSource = views;
                discipline.SelectionStart = discipline.Text.Length - 1;
                discipline.DroppedDown = true;
            }
        }

        private void discipline_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
