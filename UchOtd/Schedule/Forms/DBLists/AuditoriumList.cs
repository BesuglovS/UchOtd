using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using UchOtd.Schedule.Views.DBListViews;

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

            var audView = AuditoriumView.AuditoriumsToView(audList);

            AuditoriumListView.DataSource = audView;

            AuditoriumListView.Columns["AuditoriumId"].Visible = false;
            AuditoriumListView.Columns["Name"].Width = 250;
            AuditoriumListView.Columns["BuildingName"].Width = 170;

            AuditoriumListView.ClearSelection();


            var buildings = _repo.GetAllBuildings()
                .OrderBy(b => b.Name)
                .ToList();

            BuildingsList.ValueMember = "BuildingId";
            BuildingsList.DisplayMember = "Name";
            BuildingsList.DataSource = buildings;      
        }

        private void AuditoriumListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var AudView = ((List<AuditoriumView>)AuditoriumListView.DataSource)[e.RowIndex];

            var Aud = _repo.GetAuditorium(AudView.AuditoriumId);

            AudName.Text = Aud.Name;

            if (Aud.Building != null)
            {
                BuildingsList.SelectedValue = Aud.Building.BuildingId;
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (_repo.FindAuditorium(AudName.Text) != null)
            {
                MessageBox.Show("Такая аудитория уже есть.");
                return;
            }

            var building = _repo.GetBuilding((int)BuildingsList.SelectedValue);

            var newAuditorium = new Auditorium { Name = AudName.Text, Building = building };
            _repo.AddAuditorium(newAuditorium);

            RefreshView();
        }
        
        private void save_Click(object sender, EventArgs e)
        {
            if (AuditoriumListView.SelectedCells.Count > 0)
            {
                var AudView = ((List<AuditoriumView>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                var Aud = _repo.GetAuditorium(AudView.AuditoriumId);

                var building = _repo.GetBuilding((int)BuildingsList.SelectedValue);

                Aud.Name = AudName.Text;
                Aud.Building = building;

                _repo.UpdateAuditorium(Aud);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (AuditoriumListView.SelectedCells.Count > 0)
            {
                var AudView = ((List<AuditoriumView>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                if (_repo.GetFiltredRealLessons(l => l.Auditorium.AuditoriumId == AudView.AuditoriumId).Count > 0)
                {
                    MessageBox.Show("Аудитория есть в расписании.");
                    return;
                }

                _repo.RemoveAuditorium(AudView.AuditoriumId);

                RefreshView();
            }
        }

        private void deletewithlessons_Click(object sender, EventArgs e)
        {
            if (AuditoriumListView.SelectedCells.Count > 0)
            {
                var AudView = ((List<AuditoriumView>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                var Aud = _repo.GetAuditorium(AudView.AuditoriumId);

                var audLessons = _repo.GetFiltredRealLessons(l => l.Auditorium.AuditoriumId == Aud.AuditoriumId);

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
                var AudView = ((List<AuditoriumView>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                var Aud = _repo.GetAuditorium(AudView.AuditoriumId);

                var replaceAud = _repo.FindAuditorium(newAuditorium.Text);

                if (replaceAud == null)
                {
                    replaceAud = new Auditorium { Name = newAuditorium.Text };
                    _repo.AddAuditorium(replaceAud);
                }

                var audLessons = _repo.GetFiltredRealLessons(l => l.Auditorium.AuditoriumId == Aud.AuditoriumId);

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
