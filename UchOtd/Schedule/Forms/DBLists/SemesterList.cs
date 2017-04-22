using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class SemesterList : Form
    {
        private readonly ScheduleRepository _repo;

        public SemesterList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void SemesterList_Load(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            var semesterList = _repo.Semesters.GetAllSemesters()
                .OrderBy(s => s.StartingYear)
                .ThenBy(s => s.SemesterInYear)
                .ToList();


            SemestersListView.DataSource = semesterList;

            SemestersListView.Columns["SemesterId"].Visible = false;

            SemestersListView.Columns["StartingYear"].Width = 80;
            SemestersListView.Columns["StartingYear"].HeaderText = "Год начала учебного года";

            SemestersListView.Columns["SemesterInYear"].Width = 80;
            SemestersListView.Columns["SemesterInYear"].HeaderText = "Номер семестра в году";

            SemestersListView.Columns["DisplayName"].Width = 80;
            SemestersListView.Columns["DisplayName"].HeaderText = "Короткое имя";
        }

        private void add_Click(object sender, EventArgs e)
        {
            int startingYear = 0;
            int.TryParse(StartingYear.Text, out startingYear);

            int semesterInYear = 0;
            int.TryParse(SemesterInYear.Text, out semesterInYear);

            var newSemester = new Semester(startingYear, semesterInYear, DisplayName.Text);
            _repo.Semesters.AddSemester(newSemester);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (SemestersListView.SelectedCells.Count > 0)
            {
                int startingYear = 0;
                int.TryParse(StartingYear.Text, out startingYear);

                int semesterInYear = 0;
                int.TryParse(SemesterInYear.Text, out semesterInYear);

                var semester = ((List<Semester>)SemestersListView.DataSource)[SemestersListView.SelectedCells[0].RowIndex];

                semester.StartingYear = startingYear;
                semester.SemesterInYear = semesterInYear;

                _repo.Semesters.UpdateSemester(semester);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (SemestersListView.SelectedCells.Count > 0)
            {
                var semester = ((List<Semester>)SemestersListView.DataSource)[SemestersListView.SelectedCells[0].RowIndex];

                var outOfMind = MessageBox.Show("А не сошёл ли ты с ума?))", "ЕГГОГ", MessageBoxButtons.YesNo);
                if (outOfMind == DialogResult.No)
                {
                    return;
                }

                _repo.Semesters.RemoveSemester(semester.SemesterId);

                RefreshView();
            }
        }

        private void SemestersListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var semester = ((List<Semester>)SemestersListView.DataSource)[e.RowIndex];

            StartingYear.Text = semester.StartingYear.ToString();
            SemesterInYear.Text = semester.SemesterInYear.ToString();
            DisplayName.Text = semester.DisplayName;
        }
    }
}
