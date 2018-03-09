using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Schedule.Constants;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Core;
using UchOtd.Properties;
using UchOtd.Schedule.wnu;
using UchOtd.Schedule.wnu.MySQLViews;
using UchOtd.Views;
using Application = Microsoft.Office.Interop.Word.Application;
using Point = System.Drawing.Point;
using Shape = Microsoft.Office.Interop.Word.Shape;
using Task = System.Threading.Tasks.Task;

namespace UchOtd.Forms.Session
{
    public partial class Session : Form
    {
        readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public Session(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;            
        }

        public enum DataViews
        {
            ExamsView
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FillLists();
        }

        private void FillLists()
        {
            var discIds = _repo
                .Exams
                .GetAllExams()
                .Select(e => e.DisciplineId)
                .ToList();

            discIds = discIds.Where(discId => _repo.Disciplines.GetDiscipline(discId) != null).ToList();

            var teachersList = (
                    from discId in discIds
                    select _repo.Disciplines.GetDiscipline(discId)
                    into disc

                    select _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == disc.DisciplineId)
                    into tefd
                    where tefd != null

                    select tefd.Teacher)
                .ToList();

            var groupList = _repo
                .StudentGroups
                .GetFiltredStudentGroups(sg => 
                    !(sg.Name.Contains("-") || sg.Name.Contains("I") || sg.Name.Contains(".")))
                .OrderBy(sg => sg.Name)
                .ToList();


            groupBox.ValueMember = "StudentGroupId";
            groupBox.DisplayMember = "Name";
            groupBox.DataSource = groupList;

            teachersList = teachersList
                .GroupBy(t => t.TeacherId)
                .Select(x => x.First())
                .OrderBy(t => t.FIO)
                .ToList();

            TeacherList.ValueMember = "TeacherId";
            TeacherList.DisplayMember = "FIO";
            TeacherList.DataSource = teachersList;

            var faculties = _repo.Faculties.GetAllFaculties();

            FacultyList.DisplayMember = "Letter";
            FacultyList.ValueMember = "FacultyId";
            FacultyList.DataSource = faculties;

            siteToUpload.Items.Clear();
            foreach (var siteEndPoint in Constants.SitesUploadEndPoints)
            {
                siteToUpload.Items.Add(siteEndPoint);
            }
            if (Constants.SitesUploadEndPoints.Count > 0)
            {
                siteToUpload.SelectedIndex = 0;
            }
        }

        private void BigRedButton_Click(object sender, EventArgs e)
        {
            //_repo.Exams.FillExamListFromSchedule(_repo);
        }

        private void showAll_Click(object sender, EventArgs e)
        {
            var exams = _repo
                .Exams
                .GetAllExams()
                .OrderBy(ex => ex.ConsultationDateTime)
                .ToList();

            var examViewList = ExamView.FromExamList(_repo, exams);

            examsView.DataSource = examViewList;

            TuneDataView(examsView, DataViews.ExamsView);
        }

        private void TuneDataView(DataGridView view, DataViews viewType)
        {
            examsView.RowHeadersVisible = false;

            switch (viewType)
            {
                case DataViews.ExamsView:
                    // ExamId
                    view.Columns["ExamId"].Visible = false;
                    view.Columns["ExamId"].Width = 0;

                    // GroupName
                    view.Columns["GroupName"].Width = 100;
                    view.Columns["GroupName"].HeaderText = "Группа";

                    // TeacherFIO
                    view.Columns["TeacherFIO"].Width = 250;
                    view.Columns["TeacherFIO"].HeaderText = "Ф.И.О. преподавателя";

                    // DisciplineName
                    view.Columns["DisciplineName"].Width = 400;
                    view.Columns["DisciplineName"].HeaderText = "Дисциплина";

                    // ConsultationDateTime
                    view.Columns["ConsultationDateTime"].Width = 100;
                    view.Columns["ConsultationDateTime"].HeaderText = "Дата/Время консультации";

                    // ConsultationAuditoriumId
                    view.Columns["ConsultationAuditorium"].Width = 100;
                    view.Columns["ConsultationAuditorium"].HeaderText = "Аудитория консультации";


                    // ExamDateTime
                    view.Columns["ExamDateTime"].Width = 100;
                    view.Columns["ExamDateTime"].HeaderText = "Дата/Время экзамена";

                    // ExamAuditoriumId
                    view.Columns["ExamAuditorium"].Width = 100;
                    view.Columns["ExamAuditorium"].HeaderText = "Аудитория экзамена";
                    break;
            }

        }

        private void examsView_DoubleClick(object sender, EventArgs e)
        {
            if (examsView.SelectedCells.Count == 0)
                return;

            var updateIndex = examsView.SelectedCells[0].RowIndex;

            var examToUpdateId = ((List<ExamView>)examsView.DataSource)[updateIndex].ExamId;

            var updateForm = new ExamProperties(_repo, examToUpdateId, ExamProperties.ExamPropertiesMode.Edit);
            updateForm.ShowDialog();
        }

        private void UploadClick(object sender, EventArgs e)
        {
            var dbPrefix = uploadPrefix.Text;

            var jsonSerializer = new JavaScriptSerializer();

            var mySqlExams = MySqlExam.FromExamList(_repo.Exams.GetAllExamRecords());
            var wud = new WnuUploadData { dbPrefix = dbPrefix, tableSelector = "exams", data = jsonSerializer.Serialize(mySqlExams) };
            string json = jsonSerializer.Serialize(wud);
            var result = WnuUpload.UploadTableData(json, siteToUpload.Text);

            var mySqLlogEvents = MySqlExamLogEvent.FromLogEventList(_repo.Exams.GetAllLogEvents());
            wud = new WnuUploadData { dbPrefix = dbPrefix, tableSelector = "examsLogEvents", data = jsonSerializer.Serialize(mySqLlogEvents) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json, siteToUpload.Text);
        }
        
        private void WordExport_Click(object sender, EventArgs e)
        {
            if (oneFaculty.Checked)
            {
                WordExport.ExportCustomSessionSchedule(_repo, new List<int> { (int)FacultyList.SelectedValue }, "", false, false, true, null);
            }
            else
            {
                WordExport.ExportCustomSessionSchedule(_repo, null, "", false, false, true, null);
            }
            
        }

        

        

        private async void Refresh_Click(object sender, EventArgs e)
        {
            if (UpdateView.Text == "GO")
            {

                _tokenSource = new CancellationTokenSource();
                _cToken = _tokenSource.Token;

                UpdateView.Text = "";
                UpdateView.Image = Resources.Loading;

                var groupId = (int) groupBox.SelectedValue;

                List<ExamView> examViewList = null;

                await Task.Run(() => {
                                         var groupExams = _repo
                                             .Exams
                                             .GetGroupActiveExams(_repo, groupId, false)
                                             .ToList();

                                         examViewList = ExamView.FromExamList(_repo, groupExams);
                });

                examsView.DataSource = examViewList;

                TuneDataView(examsView, DataViews.ExamsView);

                UpdateView.Image = null;
                UpdateView.Text = "GO";
            }
            else
            {
                _tokenSource.Cancel();
            }
        }

        private async void TeacherSchedule_Click(object sender, EventArgs e)
        {
            if (TeacherSchedule.Text == "GO")
            {

                _tokenSource = new CancellationTokenSource();
                _cToken = _tokenSource.Token;

                TeacherSchedule.Text = "";
                TeacherSchedule.Image = Resources.Loading;

                var teacherId = (int) TeacherList.SelectedValue;

                List<ExamView> examViewList = null;

                await Task.Run(() => {
                    var teacherExams = (
                        from exam in _repo.Exams.GetAllExams()
                        let tefd = _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(tfd =>
                            tfd.Discipline.DisciplineId == exam.DisciplineId)
                        where tefd != null &&
                              tefd.Teacher.TeacherId == teacherId
                        select exam)
                        .ToList();

                    examViewList = ExamView.FromExamList(_repo, teacherExams);
                    
                });

                examsView.DataSource = examViewList;

                TuneDataView(examsView, DataViews.ExamsView);

                TeacherSchedule.Image = null;
                TeacherSchedule.Text = "GO";
            }
            else
            {
                _tokenSource.Cancel();
            }

        }

        private void AddExamsFromSchedule_Click(object sender, EventArgs e)
        {
            _repo.Exams.AddNewExamsFromSchedule(_repo);
        }

        private void RemoveSyncWithSchedule_Click(object sender, EventArgs e)
        {
            _repo.Exams.RemoveSyncWithSchedule(_repo);
        }

        private void Auditoriums_Click(object sender, EventArgs e)
        {
            var auds = _repo.Exams.GetAuditoriumMap(_repo);

            PutAudsOnGrid(auds);
        }

        private void PutAudsOnGrid(Dictionary<DateTime, Dictionary<int, string>> auds)
        {
            examsView.RowHeadersVisible = true;

            var audsById = _repo.Auditoriums.GetAll().ToDictionary(a => a.AuditoriumId, a => a.Name);

            var audList = new List<Auditorium>();
            foreach (var date in auds)
            {
                foreach (var a in date.Value)
                {
                    if (!audList.Select(aud => aud.AuditoriumId).Contains(a.Key))
                    {
                        var aud = _repo.Auditoriums.Get(a.Key);
                        if (aud != null)
                        {
                            audList.Add(aud);
                        }
                    }
                }
            }

            audList = SortAuditoriums(audList);

            var audIdsList = audList.Select(a => a.AuditoriumId).ToList();

            examsView.DataSource = null;

            examsView.RowCount = 0;
            examsView.ColumnCount = 0;
            examsView.RowCount = auds.Count;
            examsView.ColumnCount = audIdsList.Count;

            examsView.RowHeadersWidth = 100;
            for (int i = 0; i < auds.Count; i++)
            {
                examsView.Rows[i].HeaderCell.Value = auds.Keys.OrderBy(a => a).ElementAt(i).ToString("dd.MM.yyyy");
            }

            for (int j = 0; j < audIdsList.Count; j++)
            {
                examsView.Columns[j].HeaderText = audsById[audIdsList[j]];
            }
            AdjustColumnWidth();

            for (int i = 0; i < examsView.Rows.Count; i++)
            {
                var dateItem = auds.OrderBy(a => a.Key).ElementAt(i);

                for (int j = 0; j < examsView.Columns.Count; j++)
                {
                    if (dateItem.Value.ContainsKey(audIdsList[j]))
                    {
                        examsView.Rows[i].Cells[j].Value = dateItem.Value[audIdsList[j]];
                    }
                }
            }

            examsView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            examsView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private void AdjustColumnWidth()
        {
            for (int j = 0; j < examsView.Columns.Count; j++)
            {
                examsView.Columns[j].Width = (int)Math.Round(((examsView.Width - examsView.RowHeadersWidth) * 0.95) / examsView.Columns.Count);
            }
        }

        public List<Auditorium> SortAuditoriums(List<Auditorium> list)
        {
            var result = new List<Auditorium>();
            var a3 = list
                .Where(a => a.Name.Length >= 6 && a.Name.Substring(5, 1) == "3")
                .OrderBy(a => a.Name)
                .ToList();
            foreach (var a in a3)
            {
                result.Add(a);
                list.Remove(a);
            }
            var a1 = list
                .Where(a => a.Name.Length >= 6 && a.Name.Substring(5, 1) == "1")
                .OrderBy(a => a.Name)
                .ToList();
            foreach (var a in a1)
            {
                result.Add(a);
                list.Remove(a);
            }

            result.AddRange(list.OrderBy(a => a.Name));

            return result;
        }
    }
}
