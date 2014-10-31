using Schedule.DomainClasses.Analyse;
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
    public partial class IncompatiblePairs : Form
    {
        private readonly ScheduleRepository _repo;

        public IncompatiblePairs(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void IncompatiblePairs_Load(object sender, EventArgs e)
        {
            var discs1 = _repo.GetAllDisciplines()
                .OrderBy(d => d.StudentGroup.Name)
                .ThenBy(d => d.Name)
                .ThenBy(d => d.AuditoriumHours)
                .ToList();
            var discsView1 = DisciplineTextView.DisciplinesToView(discs1);

            var discs2 = _repo.GetAllDisciplines()
                .OrderBy(d => d.StudentGroup.Name)
                .ThenBy(d => d.Name)
                .ThenBy(d => d.AuditoriumHours)
                .ToList();
            var discsView2 = DisciplineTextView.DisciplinesToView(discs2);

            disc1.DataSource = discsView1;
            disc1.ValueMember = "DisciplineId";
            disc1.DisplayMember = "DisciplineSummary";

            disc2.DataSource = discsView2;
            disc2.ValueMember = "DisciplineId";
            disc2.DisplayMember = "DisciplineSummary";

            RefreshPairsView();

        }

        private void RefreshPairsView()
        {
            var pairs = _repo.GetFiltredCustomDisciplineAttributes(cda => cda.Key == "IncompatiblePair").ToList();

            var IPViews = IncompatiblePairView.FromCDAList(_repo, pairs);

            PairsView.DataSource = IPViews;

            PairsView.RowHeadersVisible = false;

            PairsView.Columns["cdaId"].Visible = false;

            PairsView.Columns["Disc1"].HeaderText = "Дисциплина 1";
            PairsView.Columns["Disc1"].Width = (int)Math.Round(PairsView.Width * 0.45);

            PairsView.Columns["Disc2"].HeaderText = "Дисциплина 2";
            PairsView.Columns["Disc2"].Width = (int)Math.Round(PairsView.Width * 0.45);
        }

        private void AddPair_Click(object sender, EventArgs e)
        {
            var disc1Id = (int)disc1.SelectedValue;
            var discipline1 = _repo.GetDiscipline(disc1Id);

            var disc2Id = (int)disc2.SelectedValue;

            
            if (disc1Id == disc2Id)
            {
                MessageBox.Show("Не стоит добавлять одинаковые(((", "Ошибочка");

                return;
            }
            

            var incompatiblePairAttribute = new CustomDisciplineAttribute(discipline1, "IncompatiblePair", disc2Id.ToString());

            _repo.AddCustomDisciplineAttribute(incompatiblePairAttribute);

            RefreshPairsView();
        }

        private void removePair_Click(object sender, EventArgs e)
        {
            if (PairsView.SelectedCells.Count == 0)
            {
                return;
            }

            var rowIndex = PairsView.SelectedCells[0].RowIndex;

            var cdaId = ((List<IncompatiblePairView>)PairsView.DataSource)[rowIndex].cdaId;

            _repo.RemoveCustomDisciplineAttribute(cdaId);

            RefreshPairsView();
        }
    }
}
