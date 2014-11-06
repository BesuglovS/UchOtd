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

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class StudentGroupBuildingAuditorium : Form
    {
        private readonly ScheduleRepository _repo;

        public StudentGroupBuildingAuditorium(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;            
        }

        private void RefreshView()
        {
            var items = _repo.GetAllGroupBuildingAuditoriums();
            var views = GroupBuildingAuditoriumView.ItemsToView(items);

            itemsListView.DataSource = views;

            FormatView();
        }

        private void StudentGroupBuildingAuditorium_Load(object sender, EventArgs e)
        {
            LoadLists();

            RefreshView();
        }

        private void LoadLists()
        {
            var groups = _repo.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            group.ValueMember = "StudentGroupId";
            group.DisplayMember = "Name";
            group.DataSource = groups;

            var buildings = _repo.GetAllBuildings()
                .OrderBy(b => b.Name)
                .ToList();

            building.ValueMember = "BuildingId";
            building.DisplayMember = "Name";
            building.DataSource = buildings;

            var auditoriums = _repo.GetAllAuditoriums()
                .OrderBy(a => a.Name)
                .ToList();

            auditorium.ValueMember = "AuditoriumId";
            auditorium.DisplayMember = "Name";
            auditorium.DataSource = auditoriums;
        }

        private void FormatView()
        {
            itemsListView.Columns["GroupBuildingAuditoriumId"].Visible = false;

            itemsListView.Columns["StudentGroup"].Width = 70;
            itemsListView.Columns["StudentGroup"].HeaderText = "Группа";

            itemsListView.Columns["Building"].Width = 250;
            itemsListView.Columns["Building"].HeaderText = "Корпус";

            itemsListView.Columns["Auditorium"].Width = 80;
            itemsListView.Columns["Auditorium"].HeaderText = "Аудитория";
        }
        
        private void itemsListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var view = ((List<GroupBuildingAuditoriumView>)itemsListView.DataSource)[e.RowIndex];
            var item = _repo.GetGroupBuildingAuditorium(view.GroupBuildingAuditoriumId);

            group.SelectedValue = item.StudentGroup.StudentGroupId;
            building.SelectedValue = item.Building.BuildingId;
            auditorium.SelectedValue = item.Auditorium.AuditoriumId;
        }

        private void add_Click(object sender, EventArgs e)
        {
            var groupItems = _repo.GetFilteredGroupBuildingAuditoriums(gba => gba.StudentGroup.StudentGroupId == (int)group.SelectedValue);

            if (groupItems.Count != 0)
            {
                MessageBox.Show("Эта группа уже есть.", "Ошибка", MessageBoxButtons.OK);

                return;
            }

            var newItem = new GroupBuildingAuditorium 
            { 
                StudentGroup = (StudentGroup)group.SelectedItem, 
                Building = (Building)building.SelectedItem, 
                Auditorium = (Auditorium)auditorium.SelectedItem 
            };

            _repo.AddGroupBuildingAuditorium(newItem);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (itemsListView.SelectedCells.Count > 0)
            {
                var view = ((List<GroupBuildingAuditoriumView>)itemsListView.DataSource)[itemsListView.SelectedCells[0].RowIndex];
                var item = _repo.GetGroupBuildingAuditorium(view.GroupBuildingAuditoriumId);

                item.StudentGroup = (StudentGroup)group.SelectedItem;
                item.Building = (Building)building.SelectedItem;
                item.Auditorium = (Auditorium)auditorium.SelectedItem;

                _repo.UpdateGroupBuildingAuditorium(item);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (itemsListView.SelectedCells.Count > 0)
            {
                var itemId = ((List<GroupBuildingAuditoriumView>)itemsListView.DataSource)[itemsListView.SelectedCells[0].RowIndex].GroupBuildingAuditoriumId;

                _repo.RemoveGroupBuildingAuditorium(itemId);

                RefreshView();
            }
        }
    }
}
