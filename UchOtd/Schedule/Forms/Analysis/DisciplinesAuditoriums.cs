using System.Globalization;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UchOtd.Schedule.Views;

namespace UchOtd.Schedule.Forms
{
    public partial class DisciplinesAuditoriums : Form
    {
        private readonly ScheduleRepository _repo;

        private Discipline selectedDiscipline;

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
            var allDiscs = _repo.GetAllDisciplines()
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
            

            var allAuds = _repo.GetAllAuditoriums().OrderBy(a => a.Name).ToList();

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

            selectedDiscipline = _repo.GetDiscipline(dicsId);

            var audIds = _repo.GetFiltredCustomDisciplineAttributes(cda =>
                    cda.Discipline.DisciplineId == selectedDiscipline.DisciplineId &&
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
                    var newDiscAudAttribute = new CustomDisciplineAttribute(selectedDiscipline, "DisciplineAuditorium", audId.ToString(CultureInfo.InvariantCulture));

                    _repo.AddCustomDisciplineAttribute(newDiscAudAttribute);

                    break;
                }

                if (!selected && discAudIds.Contains(audId))
                {
                    
                    var discAudAttribute = _repo
                        .GetFirstFiltredCustomDisciplineAttribute(cda => 
                            cda.Discipline.DisciplineId == selectedDiscipline.DisciplineId &&
                            cda.Key == "DisciplineAuditorium");

                    if (discAudAttribute != null)
                    {
                        _repo.RemoveCustomDisciplineAttribute(discAudAttribute.CustomDisciplineAttributeId);
                    }

                    break;
                }
            }
        }

        private void All_Click(object sender, EventArgs e)
        {
            var allAuds = _repo.GetAllAuditoriums();

            foreach (var aud in allAuds)
            {
                var newDiscAudAttribute = new CustomDisciplineAttribute(selectedDiscipline, "DisciplineAuditorium", aud.AuditoriumId.ToString(CultureInfo.InvariantCulture));

                _repo.AddCustomDisciplineAttribute(newDiscAudAttribute);
            }

            RefreshAudsList();
        }

        private void None_Click(object sender, EventArgs e)
        {
            var cdaIds = _repo
                .GetFiltredCustomDisciplineAttributes(cda => 
                    cda.Discipline.DisciplineId == selectedDiscipline.DisciplineId &&
                    cda.Key == "DisciplineAuditorium")
                .Select(cda => cda.CustomDisciplineAttributeId)
                .ToList();

            foreach (var cdaId in cdaIds)
            {
                _repo.RemoveCustomDisciplineAttribute(cdaId);
            }

            RefreshAudsList();
        }
    }
}
