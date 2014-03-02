using System.Windows.Forms;
using Schedule.DomainClasses.Config;
using Schedule.Repositories;
using System.Collections.Generic;

namespace Schedule.Forms.DBLists
{
    public partial class ConfigOptionsList : Form
    {
        private readonly ScheduleRepository _repo;

        public ConfigOptionsList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void ConfigOptionsLoad(object sender, System.EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            var optList = _repo.GetAllConfigOptions();

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

        private void add_Click(object sender, System.EventArgs e)
        {
            if (_repo.FindConfigOption(optionKey.Text) != null)
            {
                MessageBox.Show("Такая настройка уже есть.");
                return;
            }

            var newConfigOption = new ConfigOption { Key = optionKey.Text, Value = optionValue.Text};
            _repo.AddConfigOption(newConfigOption);

            RefreshView();
        }

        private void update_Click(object sender, System.EventArgs e)
        {
            if (OptionsListView.SelectedCells.Count > 0)
            {
                var option = ((List<ConfigOption>)OptionsListView.DataSource)[OptionsListView.SelectedCells[0].RowIndex];

                option.Key = optionKey.Text;
                option.Value = optionValue.Text;
                
                _repo.UpdateConfigOption(option);

                RefreshView();
            }
        }

        private void remove_Click(object sender, System.EventArgs e)
        {
            if (OptionsListView.SelectedCells.Count > 0)
            {
                var option = ((List<ConfigOption>)OptionsListView.DataSource)[OptionsListView.SelectedCells[0].RowIndex];
                
                _repo.RemoveConfigOption(option.ConfigOptionId);

                RefreshView();
            }
        }
    }
}
