using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class BuildingList : Form
    {
        private readonly ScheduleRepository _repo;

        public BuildingList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void BuildingsList_Load(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            var buildingsList = _repo.Buildings.GetAllBuildings()
                .OrderBy(b => b.Name)
                .ToList();

            
            BuildingsListView.DataSource = buildingsList;

            BuildingsListView.Columns["BuildingId"].Visible = false;

            BuildingsListView.Columns["Name"].Width = BuildingsListView.Width - 10;
        }

        private void add_Click(object sender, EventArgs e)
        {
            var newBuilding = new Building(BuildingName.Text);
            _repo.Buildings.AddBuilding(newBuilding);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (BuildingsListView.SelectedCells.Count > 0)
            {
                var building = ((List<Building>)BuildingsListView.DataSource)[BuildingsListView.SelectedCells[0].RowIndex];

                building.Name = BuildingName.Text;

                _repo.Buildings.UpdateBuilding(building);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (BuildingsListView.SelectedCells.Count > 0)
            {
                var building = ((List<Building>)BuildingsListView.DataSource)[BuildingsListView.SelectedCells[0].RowIndex];

                if (_repo.Auditoriums.FindAll(a => a.Building.BuildingId == building.BuildingId).Any())
                {
                    MessageBox.Show("К корпусу привязаны аудитории.");
                    return;
                }

                _repo.Buildings.RemoveBuilding(building.BuildingId);

                RefreshView();
            }
        }

        private void BuildingsListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var building = ((List<Building>)BuildingsListView.DataSource)[e.RowIndex];

            BuildingName.Text = building.Name;
        }
    }
}
