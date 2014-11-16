using Schedule.DomainClasses.Analyse;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class DisciplinesWithAud : Form
    {
        private readonly ScheduleRepository _repo;

        public DisciplinesWithAud(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void DisciplinesWithAud_Load(object sender, EventArgs e)
        {
            RefreshLists();
        }

        private void RefreshLists()
        {
            var withAudAttrs = _repo.CustomDisciplineAttributes.GetFiltredCustomDisciplineAttributes(cda => cda.Key == "WithAud").ToList();
            var withAudDiscs = withAudAttrs.Select(cda => cda.Discipline).ToList();

            var withAudView = DisciplineTextView.DisciplinesToView(withAudDiscs);

            WithAudDiscsList.DataSource = withAudView;
            WithAudDiscsList.ValueMember = "DisciplineId";
            WithAudDiscsList.DisplayMember = "DisciplineSummary";

            var allDiscsLeft = _repo.Disciplines.GetFiltredDisciplines(d => !withAudDiscs.Select(di => di.DisciplineId).ToList().Contains(d.DisciplineId))
                .OrderBy(d => d.StudentGroup.Name)
                .ThenBy(d => d.Name)
                .ThenBy(d => d.AuditoriumHours)
                .ToList();
            var discsView = DisciplineTextView.DisciplinesToView(allDiscsLeft);

            allDiscsList.DataSource = discsView;
            allDiscsList.ValueMember = "DisciplineId";
            allDiscsList.DisplayMember = "DisciplineSummary";
        }

        private void Include_Click(object sender, EventArgs e)
        {
            if (allDiscsList.SelectedIndex == -1)
            {
                return;
            }

            var discIds = new List<int>();

            for (int i = 0; i < allDiscsList.Items.Count; i++)
            {
                bool selected = allDiscsList.GetSelected(i);
                if (selected)
                {
                    int disciplineId = ((List<DisciplineTextView>)allDiscsList.DataSource)[i].DisciplineId;

                    discIds.Add(disciplineId);
                }
            }

            foreach (var discId in discIds)
            {
                var disc = _repo.Disciplines.GetDiscipline(discId);

                var newNotLastLessonAttribute = new CustomDisciplineAttribute(disc, "WithAud", "1");

                _repo.CustomDisciplineAttributes.AddCustomDisciplineAttribute(newNotLastLessonAttribute);
            }

            RefreshLists();
        }

        private void Exclude_Click(object sender, EventArgs e)
        {
            if (WithAudDiscsList.SelectedIndex == -1)
            {
                return;
            }

            var discIds = new List<int>();

            for (var i = 0; i < WithAudDiscsList.Items.Count; i++)
            {
                if (WithAudDiscsList.GetSelected(i))
                {
                    discIds.Add(((List<DisciplineTextView>)WithAudDiscsList.DataSource)[i].DisciplineId);
                }
            }

            foreach (var withAudAttribute in discIds.Select(localDiscId => _repo
                .CustomDisciplineAttributes
                .GetFirstFiltredCustomDisciplineAttribute(cda => 
                    cda.Discipline.DisciplineId == localDiscId && cda.Key == "WithAud"))
                    .Where(withAudAttribute => withAudAttribute != null))
            {
                _repo.CustomDisciplineAttributes
                    .RemoveCustomDisciplineAttribute(withAudAttribute.CustomDisciplineAttributeId);
            }

            RefreshLists();
        }
    }
}
