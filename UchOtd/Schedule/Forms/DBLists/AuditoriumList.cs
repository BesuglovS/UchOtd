using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Schedule.Forms.DBLists
{
    public partial class AuditoriumList : Form
    {
        private readonly ScheduleRepository _repo;

        public AuditoriumList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void AuditoriumList_Load(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            var audList = _repo
                .GetAllAuditoriums()
                .OrderBy(a => a.Name)
                .ToList();

            AuditoriumListView.DataSource = audList;

            AuditoriumListView.Columns["AuditoriumId"].Visible = false;
            AuditoriumListView.Columns["Name"].Width = 270;

            AuditoriumListView.ClearSelection();
        }

        private void AuditoriumListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var Aud = ((List<Auditorium>)AuditoriumListView.DataSource)[e.RowIndex];

            AudName.Text = Aud.Name;
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (_repo.FindAuditorium(AudName.Text) != null)
            {
                MessageBox.Show("Такая аудитория уже есть.");
                return;
            }

            var newAuditorium = new Auditorium { Name = AudName.Text };
            _repo.AddAuditorium(newAuditorium);

            RefreshView();
        }
        
        private void save_Click(object sender, EventArgs e)
        {
            if (AuditoriumListView.SelectedCells.Count > 0)
            {
                var Aud = ((List<Auditorium>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                Aud.Name = AudName.Text;

                _repo.UpdateAuditorium(Aud);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (AuditoriumListView.SelectedCells.Count > 0)
            {
                var Aud = ((List<Auditorium>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                if (_repo.GetFiltredLessons(l => l.Auditorium.AuditoriumId == Aud.AuditoriumId).Count > 0)
                {
                    MessageBox.Show("Аудитория есть в расписании.");
                    return;
                }
                
                _repo.RemoveAuditorium(Aud.AuditoriumId);

                RefreshView();
            }
        }

        private void deletewithlessons_Click(object sender, EventArgs e)
        {
            if (AuditoriumListView.SelectedCells.Count > 0)
            {
                var Aud = ((List<Auditorium>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                var audLessons = _repo.GetFiltredLessons(l => l.Auditorium.AuditoriumId == Aud.AuditoriumId);

                if (audLessons.Count > 0)
                {
                    foreach (var lesson in audLessons)
                    {
                        _repo.RemoveLesson(lesson.LessonId);
                    }
                }

                _repo.RemoveAuditorium(Aud.AuditoriumId);

                RefreshView();
            }
        }

        private void forceDeleteWithReplace_Click(object sender, EventArgs e)
        {
            if (AuditoriumListView.SelectedCells.Count > 0)
            {
                var Aud = ((List<Auditorium>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                var replaceAud = _repo.FindAuditorium(newAuditorium.Text);

                if (replaceAud == null)
                {
                    replaceAud = new Auditorium { Name = newAuditorium.Text };
                    _repo.AddAuditorium(replaceAud);
                }

                var audLessons = _repo.GetFiltredLessons(l => l.Auditorium.AuditoriumId == Aud.AuditoriumId);

                if (audLessons.Count > 0)
                {
                    foreach (var lesson in audLessons)
                    {
                        lesson.Auditorium = replaceAud;
                        _repo.UpdateLesson(lesson);
                    }                   
                }

                _repo.RemoveAuditorium(Aud.AuditoriumId);

                RefreshView();
            }
        }
    }
}
