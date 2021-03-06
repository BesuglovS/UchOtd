﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Repositories;
using UchOtd.Core;
using UchOtd.Properties;

namespace UchOtd.Schedule.Forms
{
    public partial class Auditoriums : Form
    {
        private readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        private Dictionary<int, Dictionary<int, List<string>>> auds;

        public Auditoriums(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private bool getWeekFilter(ComboBox weekList, out List<int> weekFilterList)
        {
            var text = weekList.Text;
            weekFilterList = new List<int>();
            try
            {
                if (!text.Contains("-"))
                {
                    weekFilterList.Add(int.Parse(text));
                }
                else
                {
                    var split = text.Split('-');
                    var start = int.Parse(split[0]);
                    var finish = int.Parse(split[1]);
                    for (int i = start; i <= finish; i++)
                    {
                        weekFilterList.Add(i);
                    }
                }
            }
            catch (Exception exception)
            {
                return true;
            }
            return false;
        }

        private async void Mon_Click(object sender, EventArgs e)
        {
            int buildingId = -1;
            List<int> weekFilterList = null;
            if (weekFiltered.Checked)
            {
                if (getWeekFilter(weekFilter, out weekFilterList)) return;
            }
            if (oneBuilding.Checked)
            {
                buildingId = (int)buildingList.SelectedValue;
            }
            var isShowProposed = showProposed.Checked;
            var wordExport = ExportInWord.Checked;

            auds = null;

            if (Mon.Text == "Понедельник")
            {
                _cToken = _tokenSource.Token;

                Mon.Text = "";
                Mon.Image = Resources.Loading;

                try
                {
                    auds = await Task.Run(() =>
                    {
                        var localAuds = _repo.CommonFunctions.GetDowAuds(DayOfWeek.Monday, weekFilterList, buildingId, isShowProposed);

                        if (wordExport)
                        {
                            WordExport.AuditoriumsExport(_repo, localAuds, 1, PlusTeacherFIO.Checked, _cToken);

                            return null;
                        }

                        return localAuds;
                    }, _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            Mon.Image = null;
            Mon.Text = "Понедельник";
            
            if (!wordExport && (auds != null))
            {
                PutAudsOnGrid(auds);
            }
        }

        private async void Tue_Click(object sender, EventArgs e)
        {
            int buildingId = -1;
            List<int> weekFilterList = null;
            if (weekFiltered.Checked)
            {
                if (getWeekFilter(weekFilter, out weekFilterList)) return;
            }
            if (oneBuilding.Checked)
            {
                buildingId = (int)buildingList.SelectedValue;
            }
            var isShowProposed = showProposed.Checked;
            var wordExport = ExportInWord.Checked;

            auds = null;

            if (Tue.Text == "Вторник")
            {
                _cToken = _tokenSource.Token;

                Tue.Text = "";
                Tue.Image = Resources.Loading;

                try
                {
                    auds = await Task.Run(() =>
                    {
                        var localAuds = _repo.CommonFunctions.GetDowAuds(DayOfWeek.Tuesday, weekFilterList, buildingId, isShowProposed);

                        if (wordExport)
                        {
                            WordExport.AuditoriumsExport(_repo, localAuds, 2, PlusTeacherFIO.Checked, _cToken);

                            return null;
                        }

                        return localAuds;
                    }, _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            Tue.Image = null;
            Tue.Text = "Вторник";

            if (!wordExport && (auds != null))
            {
                PutAudsOnGrid(auds);
            }
        }

        private async void Wed_Click(object sender, EventArgs e)
        {
            int buildingId = -1;
            List<int> weekFilterList = null;
            if (weekFiltered.Checked)
            {
                if (getWeekFilter(weekFilter, out weekFilterList)) return;
            }
            if (oneBuilding.Checked)
            {
                buildingId = (int)buildingList.SelectedValue;
            }
            var isShowProposed = showProposed.Checked;
            var wordExport = ExportInWord.Checked;

            auds = null;

            if (Wed.Text == "Среда")
            {
                _cToken = _tokenSource.Token;

                Wed.Text = "";
                Wed.Image = Resources.Loading;

                try
                {
                    auds = await Task.Run(() =>
                    {
                        var localAuds = _repo.CommonFunctions.GetDowAuds(DayOfWeek.Wednesday, weekFilterList, buildingId, isShowProposed);

                        if (wordExport)
                        {
                            WordExport.AuditoriumsExport(_repo, localAuds, 3, PlusTeacherFIO.Checked, _cToken);

                            return null;
                        }

                        return localAuds;
                    }, _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            Wed.Image = null;
            Wed.Text = "Среда";

            if (!wordExport && (auds != null))
            {
                PutAudsOnGrid(auds);
            }
        }

        private async void Thu_Click(object sender, EventArgs e)
        {
            int buildingId = -1;
            List<int> weekFilterList = null;
            if (weekFiltered.Checked)
            {
                if (getWeekFilter(weekFilter, out weekFilterList)) return;
            }
            if (oneBuilding.Checked)
            {
                buildingId = (int)buildingList.SelectedValue;
            }
            var isShowProposed = showProposed.Checked;
            var wordExport = ExportInWord.Checked;

            auds = null;

            if (Thu.Text == "Четверг")
            {
                _cToken = _tokenSource.Token;

                Thu.Text = "";
                Thu.Image = Resources.Loading;

                try
                {
                    auds = await Task.Run(() =>
                    {
                        var localAuds = _repo.CommonFunctions.GetDowAuds(DayOfWeek.Thursday, weekFilterList, buildingId, isShowProposed);

                        if (wordExport)
                        {
                            WordExport.AuditoriumsExport(_repo, localAuds, 4, PlusTeacherFIO.Checked, _cToken);

                            return null;
                        }

                        return localAuds;
                    }, _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            Thu.Image = null;
            Thu.Text = "Четверг";

            if (!wordExport && (auds != null))
            {
                PutAudsOnGrid(auds);
            }
        }

        private async void Fri_Click(object sender, EventArgs e)
        {
            int buildingId = -1;
            List<int> weekFilterList = null;
            if (weekFiltered.Checked)
            {
                if (getWeekFilter(weekFilter, out weekFilterList)) return;
            }
            if (oneBuilding.Checked)
            {
                buildingId = (int)buildingList.SelectedValue;
            }
            var isShowProposed = showProposed.Checked;
            var wordExport = ExportInWord.Checked;

            auds = null;

            if (Fri.Text == "Пятница")
            {
                _cToken = _tokenSource.Token;

                Fri.Text = "";
                Fri.Image = Resources.Loading;

                try
                {
                    auds = await Task.Run(() =>
                    {
                        var localAuds = _repo.CommonFunctions.GetDowAuds(DayOfWeek.Friday, weekFilterList, buildingId, isShowProposed);

                        if (wordExport)
                        {
                            WordExport.AuditoriumsExport(_repo, localAuds, 5, PlusTeacherFIO.Checked, _cToken);

                            return null;
                        }

                        return localAuds;
                    }, _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            Fri.Image = null;
            Fri.Text = "Пятница";

            if (!wordExport && (auds != null))
            {
                PutAudsOnGrid(auds);
            }
        }

        private async void Sat_Click(object sender, EventArgs e)
        {
            int buildingId = -1;
            List<int> weekFilterList = null;
            if (weekFiltered.Checked)
            {
                if (getWeekFilter(weekFilter, out weekFilterList)) return;
            }
            if (oneBuilding.Checked)
            {
                buildingId = (int)buildingList.SelectedValue;
            }
            var isShowProposed = showProposed.Checked;
            var wordExport = ExportInWord.Checked;

            auds = null;

            if (Sat.Text == "Суббота")
            {
                _cToken = _tokenSource.Token;

                Sat.Text = "";
                Sat.Image = Resources.Loading;

                try
                {
                    auds = await Task.Run(() =>
                    {
                        var localAuds = _repo.CommonFunctions.GetDowAuds(DayOfWeek.Saturday, weekFilterList, buildingId, isShowProposed);

                        if (wordExport)
                        {
                            WordExport.AuditoriumsExport(_repo, localAuds, 6, PlusTeacherFIO.Checked, _cToken);

                            return null;
                        }

                        return localAuds;
                    }, _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            Sat.Image = null;
            Sat.Text = "Суббота";

            if (!wordExport && (auds != null))
            {
                PutAudsOnGrid(auds);
            }
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

            var rings = _repo.Rings.GetAllRings();
            var audsById = _repo.Auditoriums.GetAll().ToDictionary(a => a.AuditoriumId, a => a.Name);

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
            _tokenSource = new CancellationTokenSource();

            var buildings = _repo.Buildings.GetAllBuildings()
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

            weekFilter.Items.Clear();
            for (int i = 1; i < 18; i++)
            {
                weekFilter.Items.Add(i);
            }
        }

        private void Auditoriums_ResizeEnd(object sender, EventArgs e)
        {
            AdjustColumnWidth();
        }

        private void audView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                //var source = (List<GroupTableView>)ScheduleView.DataSource;
                //var timeString = source[e.RowIndex].Time;
                //var ring = RingFromTimeString(timeString);
                //var ringId = -1;
                //if (ring != null)
                //{
                //    ringId = ring.RingId;
                //}
                //var dow = e.ColumnIndex;
                //var eprst = 999;
                //var week = 1;
                //try
                //{
                //    week = int.Parse(WeekFilter.Text);
                //}
                //catch (Exception exc)
                //{
                //    return;
                //}

                //ScheduleView.DoDragDrop("lesson:" + ringId + ":" + dow, DragDropEffects.Copy);
            }
        }
    }
}
