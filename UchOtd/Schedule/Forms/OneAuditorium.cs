using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Repositories;
using UchOtd.Schedule.wnu;

namespace UchOtd.Schedule.Forms
{
    public partial class OneAuditorium : Form
    {
        private readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public OneAuditorium(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void OneAuditorium_Load(object sender, EventArgs e)
        {
            var auds = _repo
                .Auditoriums
                .GetAll()
                .OrderBy(a => a.Name)
                .ToList();

            auditoriumList.DisplayMember = "Name";
            auditoriumList.ValueMember = "AuditoriumId";
            auditoriumList.DataSource = auds;
        }

        private async void auditoriumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }

            _tokenSource = new CancellationTokenSource();
            _cToken = _tokenSource.Token;

            Dictionary<int, Dictionary<int, List<string>>> data = null;
            
            var auditoriumId = (int) auditoriumList.SelectedValue;
            var isShowProposed = showProposed.Checked;

            try
            {
                data = await Task.Run(() => 
                    _repo.CommonFunctions.GetAud(auditoriumId, isShowProposed, _cToken), _cToken);
            }
            catch (OperationCanceledException exc)
            {
            }

            if (data != null)
            {
                PutAudsOnGrid(data);
            }
        }

        private void PutAudsOnGrid(Dictionary<int, Dictionary<int, List<string>>> data)
        {
            var rings = _repo.Rings.GetAllRings();            
                        

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
                view.Columns[j].HeaderText = global::Schedule.Constants.Constants.DowLocal[j+1];
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
