using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Config;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class ConfigOptionsList : Form
    {
        private readonly ScheduleRepository _repo;

        public ConfigOptionsList(ScheduleRepository repo)
        {
            InitializeComponent();

            var semesters = repo
                .Semesters
                .GetAllSemesters()
                .OrderBy(s => s.StartingYear)
                .ThenBy(s => s.SemesterInYear)
                .ToList();

            semesterList.ValueMember = "SemesterId";
            semesterList.DisplayMember = "DisplayName";
            semesterList.DataSource = semesters;

            _repo = repo;
        }

        private void ConfigOptionsLoad(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            var optList = _repo.ConfigOptions.GetAllConfigOptions();

            if (!showInternalOptions.Checked)
            {
                optList = optList
                    .Where(o => !o.Key.StartsWith("_"))
                    .ToList();
            }

            OptionsListView.DataSource = optList;

            OptionsListView.Columns["ConfigOptionId"].Visible = false;
            OptionsListView.Columns["Key"].Width = 100;
            OptionsListView.Columns["Value"].Width = 170;
        }

        private void OptionsListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var option = ((List<ConfigOption>)OptionsListView.DataSource)[e.RowIndex];

            optionKey.Text = option.Key;
            optionValue.Text = option.Value;

        }

        private void add_Click(object sender, EventArgs e)
        {
            var semester = (Semester)semesterList.SelectedItem;

            if (_repo.ConfigOptions.FindConfigOption(optionKey.Text, semester) != null)
            {
                MessageBox.Show("Такая настройка уже есть.");
                return;
            }

            var newConfigOption = new ConfigOption
            {
                Key = optionKey.Text,
                Value = optionValue.Text,
                Semester = semester
            };
            _repo.ConfigOptions.AddConfigOption(newConfigOption);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (OptionsListView.SelectedCells.Count > 0)
            {
                var option = ((List<ConfigOption>)OptionsListView.DataSource)[OptionsListView.SelectedCells[0].RowIndex];

                option.Key = optionKey.Text;
                option.Value = optionValue.Text;
                option.Semester = _repo.Semesters.GetSemester((int)semesterList.SelectedItem);

                _repo.ConfigOptions.UpdateConfigOption(option);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (OptionsListView.SelectedCells.Count > 0)
            {
                var option = ((List<ConfigOption>)OptionsListView.DataSource)[OptionsListView.SelectedCells[0].RowIndex];
                
                _repo.ConfigOptions.RemoveConfigOption(option.ConfigOptionId);

                RefreshView();
            }
        }

        private void showInternalOptions_CheckedChanged(object sender, EventArgs e)
        {
            RefreshView();
        }
    }
}
