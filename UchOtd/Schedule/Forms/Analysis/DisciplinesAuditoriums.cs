using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            var auds = GetAudsFromSelectedTeacher();
            var discAudIds = auds.Select(a => a.AuditoriumId).ToList();


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

        private List<Auditorium> GetAudsFromSelectedTeacher()
        {
            var datasource = (List<DisciplineTextView>)discList.DataSource;
            var discView = datasource[discList.SelectedIndex];
            var dicsId = discView.DisciplineId;

            selectedDiscipline = _repo.GetDiscipline(dicsId);

            var auds = _repo.GetDisciplinesAuditoriums(selectedDiscipline);
            return auds;
        }

        private void audList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var auds = GetAudsFromSelectedTeacher();
            var discAudIds = auds.Select(a => a.AuditoriumId).ToList();

            for (int i = 0; i < audList.Items.Count; i++)
            {
                bool selected = audList.GetSelected(i);
                var aud = ((List<Auditorium>)audList.DataSource)[i];
                int audId = aud.AuditoriumId;

                if (selected && !discAudIds.Contains(audId))
                {
                    var newDiscAud = new DisciplineAuditorium(selectedDiscipline, aud);

                    _repo.AddDisciplineAuditorium(newDiscAud);

                    break;
                }

                if (!selected && discAudIds.Contains(audId))
                {
                    
                    var discAud = _repo.FindDisciplineAuditorium(aud, selectedDiscipline);

                    if (discAud != null)
                    {
                        _repo.RemoveDisciplineAuditorium(discAud.DisciplineAuditoriumId);
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
                var newDiscAud = new DisciplineAuditorium(selectedDiscipline, aud);

                _repo.AddDisciplineAuditorium(newDiscAud);
            }

            RefreshAudsList();
        }

        private void None_Click(object sender, EventArgs e)
        {
            var daIds = _repo.GetFiltredDisciplineAuditorium(da => da.Discipline.DisciplineId == selectedDiscipline.DisciplineId)
                .Select(da => da.DisciplineAuditoriumId)
                .ToList();

            foreach (var daId in daIds)
            {
                _repo.RemoveDisciplineAuditorium(daId);
            }

            RefreshAudsList();
        }
    }
}
