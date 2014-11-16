using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms.Analysis
{
    public partial class DisciplinesAuditoriums : Form
    {
        private readonly ScheduleRepository _repo;

        private Discipline _selectedDiscipline;

        public DisciplinesAuditoriums(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void DisciplinesAuditoriums_Load(object sender, EventArgs e)
        {
            RefreshDiscList();            
        }

        private void RefreshDiscList()
        {
            var allDiscs = _repo.Disciplines.GetAllDisciplines()
                .OrderBy(d => d.StudentGroup.Name)
                .ThenBy(d => d.Name)
                .ThenBy(d => d.AuditoriumHours)
                .ToList();
            var discsView = DisciplineTextView.DisciplinesToView(allDiscs);

            discList.DataSource = discsView;
            discList.ValueMember = "DisciplineId";
            discList.DisplayMember = "DisciplineSummary";            
        }


        private void tfdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAudsList();
        }

        private void RefreshAudsList()
        {
            var discAudIds = GetAudIdsFromSelectedDiscipline();


            var allAuds = _repo.Auditoriums.GetAllAuditoriums().OrderBy(a => a.Name).ToList();

            audList.DisplayMember = "Name";
            audList.ValueMember = "AuditoriumId";
            audList.DataSource = allAuds;

            audList.ClearSelected();

            for (int i = 0; i < audList.Items.Count; i++)
            {
                if (discAudIds.Contains(allAuds[i].AuditoriumId))
                {
                    audList.SetSelected(i, true);
                }
            }            
        }

        private List<int> GetAudIdsFromSelectedDiscipline()
        {
            var datasource = (List<DisciplineTextView>)discList.DataSource;
            var discView = datasource[discList.SelectedIndex];
            var dicsId = discView.DisciplineId;

            _selectedDiscipline = _repo.Disciplines.GetDiscipline(dicsId);

            var audIds = _repo
                    .CustomDisciplineAttributes
                    .GetFiltredCustomDisciplineAttributes(cda =>
                    cda.Discipline.DisciplineId == _selectedDiscipline.DisciplineId &&
                    cda.Key == "DisciplineAuditorium")
                .Select(cda => int.Parse(cda.Value))
                .ToList();
            return audIds;
        }

        private void audList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var discAudIds = GetAudIdsFromSelectedDiscipline();
            
            for (int i = 0; i < audList.Items.Count; i++)
            {
                bool selected = audList.GetSelected(i);
                var aud = ((List<Auditorium>)audList.DataSource)[i];
                int audId = aud.AuditoriumId;

                if (selected && !discAudIds.Contains(audId))
                {
                    var newDiscAudAttribute = new CustomDisciplineAttribute(_selectedDiscipline, "DisciplineAuditorium", audId.ToString(CultureInfo.InvariantCulture));

                    _repo.CustomDisciplineAttributes.AddCustomDisciplineAttribute(newDiscAudAttribute);

                    break;
                }

                if (!selected && discAudIds.Contains(audId))
                {
                    
                    var discAudAttribute = _repo
                        .CustomDisciplineAttributes
                        .GetFirstFiltredCustomDisciplineAttribute(cda => 
                            cda.Discipline.DisciplineId == _selectedDiscipline.DisciplineId &&
                            cda.Key == "DisciplineAuditorium");

                    if (discAudAttribute != null)
                    {
                        _repo.CustomDisciplineAttributes.RemoveCustomDisciplineAttribute(discAudAttribute.CustomDisciplineAttributeId);
                    }

                    break;
                }
            }
        }

        private void All_Click(object sender, EventArgs e)
        {
            var allAuds = _repo.Auditoriums.GetAllAuditoriums();

            foreach (var aud in allAuds)
            {
                var newDiscAudAttribute = new CustomDisciplineAttribute(_selectedDiscipline, "DisciplineAuditorium", aud.AuditoriumId.ToString(CultureInfo.InvariantCulture));

                _repo.CustomDisciplineAttributes.AddCustomDisciplineAttribute(newDiscAudAttribute);
            }

            RefreshAudsList();
        }

        private void None_Click(object sender, EventArgs e)
        {
            var cdaIds = _repo
                .CustomDisciplineAttributes
                .GetFiltredCustomDisciplineAttributes(cda => 
                    cda.Discipline.DisciplineId == _selectedDiscipline.DisciplineId &&
                    cda.Key == "DisciplineAuditorium")
                .Select(cda => cda.CustomDisciplineAttributeId)
                .ToList();

            foreach (var cdaId in cdaIds)
            {
                _repo.CustomDisciplineAttributes.RemoveCustomDisciplineAttribute(cdaId);
            }

            RefreshAudsList();
        }
    }
}
