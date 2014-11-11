﻿using Schedule.DomainClasses.Analyse;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms
{
    public partial class LastLesson : Form
    {
        private readonly ScheduleRepository _repo;

        public LastLesson(ScheduleRepository repo)
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
            var NotTheLastOnesAttrs = _repo.GetFiltredCustomDisciplineAttributes(cda => cda.Key == "NotTheLastOne").ToList();
            var NotTheLastOnesDiscs = NotTheLastOnesAttrs.Select(cda => cda.Discipline).ToList();

            var NotTheLastOnesView = DisciplineTextView.DisciplinesToView(NotTheLastOnesDiscs);

            NotTheLastOneLessonDiscsList.DataSource = NotTheLastOnesView;
            NotTheLastOneLessonDiscsList.ValueMember = "DisciplineId";
            NotTheLastOneLessonDiscsList.DisplayMember = "DisciplineSummary";

            var allDiscsLeft = _repo.GetFiltredDisciplines(d => !NotTheLastOnesDiscs.Select(di => di.DisciplineId).ToList().Contains(d.DisciplineId))
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

                var newNotLastLessonAttribute = new CustomDisciplineAttribute(disc, "NotTheLastOne", "1");

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
                var NotLastLessonAttribute = _repo.GetFirstFiltredCustomDisciplineAttribute(cda => cda.Discipline.DisciplineId == discId && cda.Key == "NotTheLastOne");

                if (NotLastLessonAttribute != null)
                {
                    _repo.RemoveCustomDisciplineAttribute(NotLastLessonAttribute.CustomDisciplineAttributeId);
                }
            }            

            RefreshLists();
        }
    }
}
