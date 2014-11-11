using System.Globalization;
using Schedule.DomainClasses.Analyse;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class DisciplineByOrder : Form
    {
        private readonly ScheduleRepository _repo;

        public DisciplineByOrder(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;

            discsView.AllowDrop = true;

            LoadDisciplines();
        }

        private void LoadDisciplines()
        {
            var DisciplineViews = new List<DisciplineTextView>();

            var attributes = _repo.GetFiltredCustomDisciplineAttributes(cda => cda.Key == "DisciplineOrder").ToList();
            var discIds = attributes.Select(a => a.Discipline.DisciplineId).ToList();

            foreach (var attribute in attributes.OrderBy(a => int.Parse(a.Value))
                .ThenBy(a => a.Discipline.StudentGroup.Name)
                .ThenBy(a => a.Discipline.Name)
                .ThenBy(a => a.Discipline.AuditoriumHours))
            {
                DisciplineViews.Add(new DisciplineTextView(attribute.Discipline));
            }

            var discsLeft = _repo.GetFiltredDisciplines(d => !discIds.Contains(d.DisciplineId)).ToList();

            foreach (var disc in discsLeft.OrderBy(d => d.StudentGroup.Name)
                .ThenBy(d => d.Name)
                .ThenBy(d => d.AuditoriumHours))
            {
                DisciplineViews.Add(new DisciplineTextView(disc));
            }

            discsView.DisplayMember = "DisciplineSummary";
            discsView.ValueMember = "DisciplineId";

            foreach (var view in DisciplineViews)
            {
                discsView.Items.Add(view);
            }
        }

        private void discsView_MouseDown(object sender, MouseEventArgs e)
        {
            int ix = discsView.IndexFromPoint(e.Location);

            if (ix != -1)
            {
                discsView.DoDragDrop(ix.ToString(CultureInfo.InvariantCulture), DragDropEffects.Move);
            }
        }

        private void discsView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void discsView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                int dix = Convert.ToInt32(e.Data.GetData(DataFormats.Text));
                int ix = discsView.IndexFromPoint(discsView.PointToClient(new Point(e.X, e.Y)));
                if (ix != -1)
                {
                    object obj = discsView.Items[dix];
                    discsView.Items.Remove(obj);
                    discsView.Items.Insert(ix, obj);
                }
            }
        }

        private void DisciplineByOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            for(var i = 0; i < discsView.Items.Count; i++)
            {                
                var item = ((DisciplineTextView)discsView.Items[i]);

                var disc = _repo.GetDiscipline(item.DisciplineId);

                var orderAttr = new CustomDisciplineAttribute(disc, "DisciplineOrder", i.ToString(CultureInfo.InvariantCulture));

                _repo.AddOrUpdateCustomDisciplineAttribute(orderAttr);
            }
        }
    }
}
