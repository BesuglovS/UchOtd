using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.DBLists
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
                .Auditoriums
                .GetAll()
                .OrderBy(a => a.Name)
                .ToList();

            var audView = AuditoriumView.AuditoriumsToView(audList);

            AuditoriumListView.DataSource = audView;

            AuditoriumListView.Columns["AuditoriumId"].Visible = false;
            AuditoriumListView.Columns["Name"].Width = 250;
            AuditoriumListView.Columns["BuildingName"].Width = 170;

            AuditoriumListView.ClearSelection();


            var buildings = _repo.Buildings.GetAllBuildings()
                .OrderBy(b => b.Name)
                .ToList();

            BuildingsList.ValueMember = "BuildingId";
            BuildingsList.DisplayMember = "Name";
            BuildingsList.DataSource = buildings;      
        }

        private void AuditoriumListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var audView = ((List<AuditoriumView>)AuditoriumListView.DataSource)[e.RowIndex];

            var aud = _repo.Auditoriums.Get(audView.AuditoriumId);

            AudName.Text = aud.Name;

            if (aud.Building != null)
            {
                BuildingsList.SelectedValue = aud.Building.BuildingId;
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (_repo.Auditoriums.Find(AudName.Text) != null)
            {
                MessageBox.Show("Такая аудитория уже есть.");
                return;
            }

            var building = _repo.Buildings.GetBuilding((int)BuildingsList.SelectedValue);

            var newAuditorium = new Auditorium { Name = AudName.Text, Building = building };
            _repo.Auditoriums.Add(newAuditorium);

            RefreshView();
        }
        
        private void save_Click(object sender, EventArgs e)
        {
            if (AuditoriumListView.SelectedCells.Count > 0)
            {
                var audView = ((List<AuditoriumView>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                var aud = _repo.Auditoriums.Get(audView.AuditoriumId);

                var building = _repo.Buildings.GetBuilding((int)BuildingsList.SelectedValue);

                aud.Name = AudName.Text;
                aud.Building = building;

                _repo.Auditoriums.Update(aud, aud.AuditoriumId);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (AuditoriumListView.SelectedCells.Count > 0)
            {
                var audView = ((List<AuditoriumView>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                var aud = _repo.Auditoriums.Get(audView.AuditoriumId);

                if (_repo.Lessons.GetFiltredLessons(l => l.Auditorium.AuditoriumId == audView.AuditoriumId).Count > 0)
                {
                    MessageBox.Show("Аудитория есть в расписании.");
                    return;
                }

                _repo.Auditoriums.Delete(aud);

                RefreshView();
            }
        }

        private void deletewithlessons_Click(object sender, EventArgs e)
        {
            if (AuditoriumListView.SelectedCells.Count > 0)
            {
                var audView = ((List<AuditoriumView>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                var aud = _repo.Auditoriums.Get(audView.AuditoriumId);

                var audLessons = _repo.Lessons.GetFiltredLessons(l => l.Auditorium.AuditoriumId == aud.AuditoriumId);

                if (audLessons.Count > 0)
                {
                    foreach (var lesson in audLessons)
                    {
                        _repo.Lessons.RemoveLesson(lesson.LessonId);
                    }
                }

                _repo.Auditoriums.Delete(aud);

                RefreshView();
            }
        }

        private void forceDeleteWithReplace_Click(object sender, EventArgs e)
        {
            if (AuditoriumListView.SelectedCells.Count > 0)
            {
                var audView = ((List<AuditoriumView>)AuditoriumListView.DataSource)[AuditoriumListView.SelectedCells[0].RowIndex];

                var aud = _repo.Auditoriums.Get(audView.AuditoriumId);

                var replaceAud = _repo.Auditoriums.Find(newAuditorium.Text);

                if (replaceAud == null)
                {
                    replaceAud = new Auditorium { Name = newAuditorium.Text };
                    _repo.Auditoriums.Add(replaceAud);
                }

                var audLessons = _repo.Lessons.GetFiltredLessons(l => l.Auditorium.AuditoriumId == aud.AuditoriumId);

                if (audLessons.Count > 0)
                {
                    foreach (var lesson in audLessons)
                    {
                        lesson.Auditorium = replaceAud;
                        _repo.Lessons.UpdateLesson(lesson);
                    }                   
                }

                _repo.Auditoriums.Delete(aud);

                RefreshView();
            }
        }
    }
}
