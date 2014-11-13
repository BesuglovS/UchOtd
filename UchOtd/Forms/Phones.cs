using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UchOtd.Core;
using UchOtd.DomainClasses;
using UchOtd.Repositories;

namespace UchOtd.Forms
{
    public partial class Phones : Form
    {
        readonly UchOtdRepository _uoRepo;
        private readonly TaskScheduler _uiScheduler;

        public Phones(UchOtdRepository uoRepo)
        {
            InitializeComponent();

            _uoRepo = uoRepo;

            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        private void Phones_Load(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            var phones = _uoRepo
                .GetFiltredPhones(p => 
                    p.Name.ToUpper().Contains(NameBox.Text.ToUpper()) ||
                    p.Name.ToUpper().Contains(LayoutSupport.ConvertEnRu(NameBox.Text.ToLower()).ToUpper()))
                .OrderBy(p => p.Name)
                .ToList();

            view.DataSource = phones;

            FormatView();
        }

        private void FormatView()
        {
            view.Columns["PhoneId"].Visible = false;

            view.Columns["Name"].HeaderText = "Имя";
            view.Columns["Name"].Width = (int)Math.Round(view.Width * 0.6);

            view.Columns["Number"].HeaderText = "Номер";
            view.Columns["Number"].Width = (int)Math.Round(view.Width * 0.35);

        }

        private void add_Click(object sender, EventArgs e)
        {
            var newPhone = new Phone { Name = NameBox.Text, Number = NumberBox.Text };

            _uoRepo.AddPhone(newPhone);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (view.SelectedCells.Count > 0)
            {
                var phone = ((List<Phone>)view.DataSource)[view.SelectedCells[0].RowIndex];

                phone.Name = NameBox.Text;
                phone.Number = NumberBox.Text;

                _uoRepo.UpdatePhone(phone);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (view.SelectedCells.Count > 0)
            {
                var phone = ((List<Phone>)view.DataSource)[view.SelectedCells[0].RowIndex];

                _uoRepo.RemovePhone(phone.PhoneId);

                NameBox.Text = "";

                RefreshView();
            }
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void view_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var phone = ((List<Phone>)view.DataSource)[e.RowIndex];

            NameBox.Text = phone.Name;
            NumberBox.Text = phone.Number;
        }

        private void NameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void NumberBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void import_Click(object sender, EventArgs e)
        {
            var phones = new List<Phone>();

            var sr = new StreamReader(@"d:\bs\расписание\phonebook.txt");
            string line;
            while((line = sr.ReadLine()) != null)
            {
                var name = line.Split('*')[0];
                var numbers = line.Substring(name.Length + 1);

                var phone = new Phone { Name = name, Number = numbers };

                phones.Add(phone);
            }

            _uoRepo.AddPhonesRange(phones);
        }

        private void clear_Click(object sender, EventArgs e)
        {
            var phoneIds = _uoRepo.GetAllPhones().Select(p => p.PhoneId);

            foreach (var pid in phoneIds)
            {
                _uoRepo.RemovePhone(pid);
            }
        }
    }
}
