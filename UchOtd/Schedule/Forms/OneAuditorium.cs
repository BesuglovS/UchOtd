using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule.Forms
{
    public partial class OneAuditorium : Form
    {
        private readonly ScheduleRepository _repo;

        public OneAuditorium(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void OneAuditorium_Load(object sender, EventArgs e)
        {
            var auds = _repo
                .GetAllAuditoriums()
                .OrderBy(a => a.Name)
                .ToList();

            auditoriumList.DisplayMember = "Name";
            auditoriumList.ValueMember = "AuditoriumId";
            auditoriumList.DataSource = auds;
        }

        private void auditoriumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var data = _repo.getAud((int)auditoriumList.SelectedValue, showProposed.Checked);

            PutAudsOnGrid(data);
        }

        private void PutAudsOnGrid(Dictionary<int, Dictionary<int, List<string>>> data)
        {
            var rings = _repo.GetAllRings();            
                        

            view.RowCount = 0;
            view.ColumnCount = 0;
            view.RowCount = data.Count;
            view.ColumnCount = 7;

            view.RowHeadersWidth = 80;
            for (int i = 0; i < data.Count; i++)
            {
                var ct = rings.First(r => r.RingId == data.Keys.ElementAt(i)).Time.ToString("H:mm");
                view.Rows[i].HeaderCell.Value = ct;
            }
            for (int j = 0; j < 7; j++)
            {
                view.Columns[j].HeaderText = Constants.Constants.DOWLocal[j+1];
            }

            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    if (data[data.Keys.ElementAt(i)].ContainsKey(j))
                    {
                        var cnt = data[data.Keys.ElementAt(i)][j].Count;
                        int ii = 0;
                        foreach (var kvp in data[data.Keys.ElementAt(i)][j])
                        {
                            if (kvp.Contains('@'))
                            {
                                view.Rows[i].Cells[j-1].Value += kvp.Split('@')[0];
                                view.Rows[i].Cells[j-1].ToolTipText += kvp.Substring(kvp.Split('@')[0].Length + 1);

                                if (ii != cnt - 1)
                                {
                                    view.Rows[i].Cells[j-1].Value += Environment.NewLine;
                                    view.Rows[i].Cells[j-1].ToolTipText += Environment.NewLine;
                                }
                            }
                            else
                            {
                                view.Rows[i].Cells[j-1].Value += kvp;
                                if (ii != cnt - 1)
                                {
                                    view.Rows[i].Cells[j-1].Value += Environment.NewLine;
                                }
                            }

                            ii++;
                        }
                    }
                    else
                    {
                        view.Rows[i].Cells[j-1].Value = "";
                        view.Rows[i].Cells[j-1].ToolTipText = "";
                    }
                }
            }

            view.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            view.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }
    }
}
