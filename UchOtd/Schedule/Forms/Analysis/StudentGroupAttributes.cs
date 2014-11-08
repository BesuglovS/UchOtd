﻿using Schedule.DomainClasses.Analyse;
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
    public partial class StudentGroupAttributes : Form
    {
        private readonly ScheduleRepository _repo;

        public StudentGroupAttributes(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;            
        }

        private void RefreshView()
        {
            var attributeNames = new List<string> { "Building", "Auditorium", "Shift" };

            var items = _repo.GetFiltredCustomStudentGroupAttributes(csga => attributeNames.Contains(csga.Key));
            var views = GroupAttributesView.ItemsToView(_repo, items);

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

            var shifts = _repo.GetAllShifts();

            shift.ValueMember = "ShiftId";
            shift.DisplayMember = "Name";
            shift.DataSource = shifts;
        }

        private void FormatView()
        {
            itemsListView.Columns["StudentGroupId"].Visible = false;

            itemsListView.Columns["StudentGroup"].Width = 70;
            itemsListView.Columns["StudentGroup"].HeaderText = "Группа";

            itemsListView.Columns["Building"].Width = 250;
            itemsListView.Columns["Building"].HeaderText = "Корпус";

            itemsListView.Columns["Auditorium"].Width = 80;
            itemsListView.Columns["Auditorium"].HeaderText = "Аудитория";

            itemsListView.Columns["Shift"].Width = 80;
            itemsListView.Columns["Shift"].HeaderText = "Смена";
        }
        
        private void itemsListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var view = ((List<GroupAttributesView>)itemsListView.DataSource)[e.RowIndex];
            var items = _repo.GetFiltredCustomStudentGroupAttributes(csga => csga.StudentGroup.StudentGroupId == view.StudentGroupId);

            group.SelectedValue = view.StudentGroupId;

            var buildingAttribute = items.FirstOrDefault(csga => csga.Key == "Building");
            if (buildingAttribute != null)
            {
                building.SelectedValue = int.Parse(buildingAttribute.Value);
            }

            var auditoriumAttribute = items.FirstOrDefault(csga => csga.Key == "Auditorium");
            if (auditoriumAttribute != null)
            {
                auditorium.SelectedValue = int.Parse(auditoriumAttribute.Value);
            }

            var shiftAttribute = items.FirstOrDefault(csga => csga.Key == "State");
            if (shiftAttribute != null)
            {
                shift.SelectedValue = int.Parse(shiftAttribute.Value);
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            var groupItems = _repo.GetFiltredCustomStudentGroupAttributes(csga => csga.StudentGroup.StudentGroupId == (int)group.SelectedValue);

            if (groupItems.Count != 0)
            {
                MessageBox.Show("Эта группа уже есть.", "Ошибка", MessageBoxButtons.OK);

                return;
            }

            var newBuildingAttribute = new CustomStudentGroupAttribute 
            { 
                StudentGroup = (StudentGroup)group.SelectedItem, 
                Key = "Building",
                Value = ((int)building.SelectedValue).ToString()
            };
            _repo.AddOrUpdateCustomStudentGroupAttribute(newBuildingAttribute);

            var newAuditoriumAttrbute = new CustomStudentGroupAttribute
            {
                StudentGroup = (StudentGroup)group.SelectedItem,
                Key = "Auditorium",
                Value = ((int)auditorium.SelectedValue).ToString()
            };
            _repo.AddOrUpdateCustomStudentGroupAttribute(newAuditoriumAttrbute);

            var newShiftAttrbute = new CustomStudentGroupAttribute
            {
                StudentGroup = (StudentGroup)group.SelectedItem,
                Key = "Shift",
                Value = ((int)shift.SelectedValue).ToString()
            };
            _repo.AddOrUpdateCustomStudentGroupAttribute(newShiftAttrbute);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (itemsListView.SelectedCells.Count > 0)
            {
                var newBuildingAttribute = new CustomStudentGroupAttribute
                {
                    StudentGroup = (StudentGroup)group.SelectedItem,
                    Key = "Building",
                    Value = ((int)building.SelectedValue).ToString()
                };
                _repo.AddOrUpdateCustomStudentGroupAttribute(newBuildingAttribute);

                var newAuditoriumAttrbute = new CustomStudentGroupAttribute
                {
                    StudentGroup = (StudentGroup)group.SelectedItem,
                    Key = "Auditorium",
                    Value = ((int)auditorium.SelectedValue).ToString()
                };
                _repo.AddOrUpdateCustomStudentGroupAttribute(newAuditoriumAttrbute);

                var newShiftAttrbute = new CustomStudentGroupAttribute
                {
                    StudentGroup = (StudentGroup)group.SelectedItem,
                    Key = "Shift",
                    Value = ((int)shift.SelectedValue).ToString()
                };
                _repo.AddOrUpdateCustomStudentGroupAttribute(newShiftAttrbute);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (itemsListView.SelectedCells.Count > 0)
            {
                var groupId = ((List<GroupAttributesView>)itemsListView.DataSource)[itemsListView.SelectedCells[0].RowIndex].StudentGroupId;

                var groupItemIds = _repo
                    .GetFiltredCustomStudentGroupAttributes(csga => csga.StudentGroup.StudentGroupId == groupId)
                    .Select(csga => csga.CustomStudentGroupAttributeId);

                foreach (var csgaId in groupItemIds)
                {
                    _repo.RemoveCustomStudentGroupAttribute(csgaId);
                }                

                RefreshView();
            }
        }
    }
}
