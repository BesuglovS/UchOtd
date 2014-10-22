using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using UchOtd.Core;

namespace Schedule.Forms
{
    public partial class Auditoriums : Form
    {
        private readonly ScheduleRepository _repo;

        public Auditoriums(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void Mon_Click(object sender, EventArgs e)
        {
            Dictionary<int, Dictionary<int, List<string>>> auds;
            int weekNum = -1;
            int buildingNum = -1;
            if (oneWeek.Checked)
            {
                weekNum = (int)weekNumber.Value;
            }
            if (oneBuilding.Checked)
            {
                buildingNum = (int)buildingList.SelectedValue;
            }
            auds = _repo.getDOWAuds(DayOfWeek.Monday, weekNum, buildingNum);

            if (ExportInWord.Checked)
            {
                WordExport.AuditoriumsExport(_repo, auds, 1, PlusTeacherFIO.Checked);
                return;
            }
            
            PutAudsOnGrid(auds);
        }

        private void Tue_Click(object sender, EventArgs e)
        {
            Dictionary<int, Dictionary<int, List<string>>> auds;
            int weekNum = -1;
            int buildingNum = -1;
            if (oneWeek.Checked)
            {
                weekNum = (int)weekNumber.Value;
            }
            if (oneBuilding.Checked)
            {
                buildingNum = (int)buildingList.SelectedValue;
            }
            auds = _repo.getDOWAuds(DayOfWeek.Tuesday, weekNum, buildingNum);

            if (ExportInWord.Checked)
            {
                WordExport.AuditoriumsExport(_repo, auds, 2, PlusTeacherFIO.Checked);
                return;
            }

            PutAudsOnGrid(auds);
        }

        private void Wed_Click(object sender, EventArgs e)
        {
            Dictionary<int, Dictionary<int, List<string>>> auds;
            int weekNum = -1;
            int buildingNum = -1;
            if (oneWeek.Checked)
            {
                weekNum = (int)weekNumber.Value;
            }
            if (oneBuilding.Checked)
            {
                buildingNum = (int)buildingList.SelectedValue;
            }
            auds = _repo.getDOWAuds(DayOfWeek.Wednesday, weekNum, buildingNum);

            if (ExportInWord.Checked)
            {
                WordExport.AuditoriumsExport(_repo, auds, 3, PlusTeacherFIO.Checked);
                return;
            }

            PutAudsOnGrid(auds);
        }

        private void Thu_Click(object sender, EventArgs e)
        {
            Dictionary<int, Dictionary<int, List<string>>> auds;
            int weekNum = -1;
            int buildingNum = -1;
            if (oneWeek.Checked)
            {
                weekNum = (int)weekNumber.Value;
            }
            if (oneBuilding.Checked)
            {
                buildingNum = (int)buildingList.SelectedValue;
            }
            auds = _repo.getDOWAuds(DayOfWeek.Thursday, weekNum, buildingNum);

            if (ExportInWord.Checked)
            {
                WordExport.AuditoriumsExport(_repo, auds, 4, PlusTeacherFIO.Checked);
                return;
            }

            PutAudsOnGrid(auds);
        }

        private void Fri_Click(object sender, EventArgs e)
        {
            Dictionary<int, Dictionary<int, List<string>>> auds;
            int weekNum = -1;
            int buildingNum = -1;
            if (oneWeek.Checked)
            {
                weekNum = (int)weekNumber.Value;
            }
            if (oneBuilding.Checked)
            {
                buildingNum = (int)buildingList.SelectedValue;
            }
            auds = _repo.getDOWAuds(DayOfWeek.Friday, weekNum, buildingNum);

            if (ExportInWord.Checked)
            {
                WordExport.AuditoriumsExport(_repo, auds, 5, PlusTeacherFIO.Checked);
                return;
            }

            PutAudsOnGrid(auds);
        }

        private void Sat_Click(object sender, EventArgs e)
        {
            Dictionary<int, Dictionary<int, List<string>>> auds;
            int weekNum = -1;
            int buildingNum = -1;
            if (oneWeek.Checked)
            {
                weekNum = (int)weekNumber.Value;
            }
            if (oneBuilding.Checked)
            {
                buildingNum = (int)buildingList.SelectedValue;
            }
            auds = _repo.getDOWAuds(DayOfWeek.Saturday, weekNum, buildingNum);

            if (ExportInWord.Checked)
            {
                WordExport.AuditoriumsExport(_repo, auds, 6, PlusTeacherFIO.Checked);
                return;
            }

            PutAudsOnGrid(auds);
        }

        private void PutAudsOnGrid(Dictionary<int, Dictionary<int, List<string>>> auds)
        {
            var audIdsList = new List<int>();
            foreach (var r in auds)
            {
                foreach (var a in r.Value)
                {
                    if (!audIdsList.Contains(a.Key))
                    {
                        audIdsList.Add(a.Key);
                    }
                }
            }

            var rings = _repo.GetAllRings();
            var audsById = _repo.GetAllAuditoriums().ToDictionary(a => a.AuditoriumId, a => a.Name);

            audIdsList = audIdsList.OrderBy(id => audsById[id]).ToList();

            audView.RowCount = 0;
            audView.ColumnCount = 0;
            audView.RowCount = auds.Count;            
            audView.ColumnCount = audIdsList.Count;

            audView.RowHeadersWidth = 80;
            for (int i = 0; i < auds.Count; i++)
            {
                var ct = rings.First(r => r.RingId == auds.Keys.ElementAt(i)).Time.ToString("H:mm");
                audView.Rows[i].HeaderCell.Value = ct;
            }
            for (int j = 0; j < audIdsList.Count; j++)
            {
                audView.Columns[j].HeaderText = audsById[audIdsList[j]];                
            }
            AdjustColumnWidth();

            for (int i = 0; i < auds.Count; i++)
            {
                for (int j = 0; j < audIdsList.Count; j++)
                {
                    if (auds[auds.Keys.ElementAt(i)].ContainsKey(audIdsList[j]))
                    {
                        var cnt = auds[auds.Keys.ElementAt(i)][audIdsList[j]].Count;
                        int ii = 0;
                        foreach (var kvp in auds[auds.Keys.ElementAt(i)][audIdsList[j]])
                        {
                            if (kvp.Contains('@'))
                            {
                                audView.Rows[i].Cells[j].Value += kvp.Split('@')[0];                                
                                audView.Rows[i].Cells[j].ToolTipText += kvp.Substring(kvp.Split('@')[0].Length + 1);
                                
                                if (ii != cnt - 1)
                                {
                                    audView.Rows[i].Cells[j].Value += Environment.NewLine;
                                    audView.Rows[i].Cells[j].ToolTipText += Environment.NewLine;
                                }
                            }
                            else
                            {                                
                                audView.Rows[i].Cells[j].Value += kvp;
                                if (ii != cnt - 1)
                                {
                                    audView.Rows[i].Cells[j].Value += Environment.NewLine;
                                }
                            }

                            ii++;
                        }
                    }
                    else
                    {
                        audView.Rows[i].Cells[j].Value = "";
                        audView.Rows[i].Cells[j].ToolTipText = "";
                    }
                }
            }

            audView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            audView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private void AdjustColumnWidth()
        {
            for (int j = 0; j < audView.Columns.Count; j++)
            {
                audView.Columns[j].Width = (int)Math.Round(((audView.Width - audView.RowHeadersWidth) * 0.95) / audView.Columns.Count);
            } 
        }

        private void Auditoriums_Load(object sender, EventArgs e)
        {
            var buildings = _repo.GetAllBuildings()
                .OrderBy(b => b.Name)
                .ToList();

            var mainBuilding = buildings.FirstOrDefault(b => b.Name == "ул. Молодогвардейская, 196");
            
            buildingList.DisplayMember = "Name";
            buildingList.ValueMember = "BuildingId";
            buildingList.DataSource = buildings;

            if (mainBuilding != null)
            {
                buildingList.SelectedValue = mainBuilding.BuildingId;
            }
        }

        private void Auditoriums_ResizeEnd(object sender, EventArgs e)
        {
            AdjustColumnWidth();
        }
    }
}
