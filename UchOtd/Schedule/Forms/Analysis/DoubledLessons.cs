using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Analyse;
using Schedule.Repositories;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class DoubledLessons : Form
    {
        private readonly ScheduleRepository _repo;

        public DoubledLessons(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void LastLesson_Load(object sender, EventArgs e)
        {
            RefreshLists();
        }

        private void RefreshLists()
        {
            var notTheLastOnesAttrs = _repo.GetFiltredCustomDisciplineAttributes(cda => cda.Key == "BetterTogether").ToList();
            var notTheLastOnesDiscs = notTheLastOnesAttrs.Select(cda => cda.Discipline).ToList();

            var notTheLastOnesView = DisciplineTextView.DisciplinesToView(notTheLastOnesDiscs);

            NotTheLastOneLessonDiscsList.DataSource = notTheLastOnesView;
            NotTheLastOneLessonDiscsList.ValueMember = "DisciplineId";
            NotTheLastOneLessonDiscsList.DisplayMember = "DisciplineSummary";

            var allDiscsLeft = _repo.GetFiltredDisciplines(d => !notTheLastOnesDiscs.Select(di => di.DisciplineId).ToList().Contains(d.DisciplineId))
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
                var disc = _repo.GetDiscipline(discId);

                var newNotLastLessonAttribute = new CustomDisciplineAttribute(disc, "BetterTogether", "1");

                _repo.AddCustomDisciplineAttribute(newNotLastLessonAttribute);
            }            

            RefreshLists();
        }

        private void Exclude_Click(object sender, EventArgs e)
        {
            if (NotTheLastOneLessonDiscsList.SelectedIndex == -1)
            {
                return;
            }

            var discIds = new List<int>();

            for (int i = 0; i < NotTheLastOneLessonDiscsList.Items.Count; i++)
            {
                bool selected = NotTheLastOneLessonDiscsList.GetSelected(i);
                if (selected)
                {
                    int disciplineId = ((List<DisciplineTextView>)NotTheLastOneLessonDiscsList.DataSource)[i].DisciplineId;

                    discIds.Add(disciplineId);
                }
            }

            foreach (var discId in discIds)
            {
                int localDiscId = discId;
                var notLastLessonAttribute = _repo.GetFirstFiltredCustomDisciplineAttribute(cda => cda.Discipline.DisciplineId == localDiscId && cda.Key == "BetterTogether");

                if (notLastLessonAttribute != null)
                {
                    _repo.RemoveCustomDisciplineAttribute(notLastLessonAttribute.CustomDisciplineAttributeId);
                }
            }            

            RefreshLists();
        }
    }
}
