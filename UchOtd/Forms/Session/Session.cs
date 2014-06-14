using Schedule.DomainClasses.Main;
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
using Word = Microsoft.Office.Interop.Word;
using System.Globalization;
using Microsoft.Office.Core;
using System.Net;
using UchOtd.Views;
using UchOtd.NUDS.Core;
using UchOtd.Schedule.wnu.MySQLViews;
using Schedule.wnu;
using Schedule.DomainClasses.Session;

namespace UchOtd.Forms.Session
{
    public partial class Session : Form
    {
        readonly ScheduleRepository _repo;

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
                .GetAllExams()
                .Select(e => e.DisciplineId)
                .ToList();

            //var groupList = new List<StudentGroup>();
            var TeachersList = new List<Teacher>();

            foreach (var discId in discIds)
            {
                var disc = _repo.GetDiscipline(discId);

                //groupList.Add(disc.StudentGroup);

                var tefd = _repo.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == disc.DisciplineId);

                if (tefd != null)
                {
                    TeachersList.Add(tefd.Teacher);
                }
            }

            /*groupList = groupList
                .GroupBy(g => g.StudentGroupId)
                .Select(x => x.First())                
                .OrderBy(g => g.Name)                
                .ToList();*/

            var groupList = _repo
                .GetFiltredStudentGroups(sg => !(sg.Name.Contains("-") || sg.Name.Contains("I") || sg.Name.Contains(".")))
                .ToList();


            groupBox.ValueMember = "StudentGroupId";
            groupBox.DisplayMember = "Name";
            groupBox.DataSource = groupList;

            TeachersList = TeachersList
                .GroupBy(t => t.TeacherId)
                .Select(x => x.First())
                .OrderBy(t => t.FIO)
                .ToList();

            TeacherList.ValueMember = "TeacherId";
            TeacherList.DisplayMember = "FIO";
            TeacherList.DataSource = TeachersList;

            var faculties = _repo.GetAllFaculties();

            FacultyList.DisplayMember = "Letter";
            FacultyList.ValueMember = "FacultyId";
            FacultyList.DataSource = faculties;
        }

        private void BigRedButton_Click(object sender, EventArgs e)
        {
            _repo.FillExamListFromSchedule(_repo);

            var eprst = 999;
        }

        private void showAll_Click(object sender, EventArgs e)
        {
            var exams = _repo
                .GetAllExams()
                .OrderBy(ex => ex.ConsultationDateTime)
                .ToList();

            var ExamViewList = ExamView.FromExamList(_repo, exams);

            examsView.DataSource = ExamViewList;

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

        private void groupBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UpdateExamsView();
        }

        private void UpdateExamsView()
        {
            var groupExams = _repo
                .GetGroupActiveExams(_repo, (int)groupBox.SelectedValue, false)
                .ToList();

            var ExamViewList = ExamView.FromExamList(_repo, groupExams);

            examsView.DataSource = ExamViewList;

            TuneDataView(examsView, DataViews.ExamsView);
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
            string result;

            var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var mySQLExams = MySQLExam.FromExamList(_repo.GetAllExamRecords());
            var wud = new WnuUploadData { tableSelector = "exams", data = jsonSerializer.Serialize(mySQLExams) };
            string json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var MySQLlogEvents = MySQLExamLogEvent.FromLogEventList(_repo.GetAllLogEvents());
            wud = new WnuUploadData { tableSelector = "examsLogEvents", data = jsonSerializer.Serialize(MySQLlogEvents) };
            json = jsonSerializer.Serialize(wud);
            result = WnuUpload.UploadTableData(json);
        }
        
        private void WordExport_Click(object sender, EventArgs e)
        {
            if (oneFaculty.Checked)
            {
                SaveAsWordDocument((int)FacultyList.SelectedValue);
            }
            else
            {
                SaveAsWordDocument(-1);
            }
            
        }

        private void SaveAsWordDocument(int facultyFilter)
        {
            DateTime beginSessionDate, endSessionDate;
            DetectSessionDates(out beginSessionDate, out endSessionDate);


            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.

            Word._Application oWord = new Word.Application();
            oWord.Visible = true;
            Word._Document oDoc =
                oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            List<Faculty> faculties = null;

            if (facultyFilter != -1)
            {
                var onefaculty = _repo.GetFaculty((int)FacultyList.SelectedValue);
                if (oneFaculty == null)
                {
                    faculties = _repo.GetAllFaculties().OrderBy(f => f.SortingOrder).ToList();
                }
                else
                {
                    faculties = new List<Faculty>();
                    faculties.Add(onefaculty);
                }
            }
            else
            {
                faculties = _repo.GetAllFaculties().OrderBy(f => f.SortingOrder).ToList();
            }
            
            foreach (var faculty in faculties)
            {
                /*
                var groupIds = new List<int>();                
                foreach (var group in Constants.facultyGroups.ElementAt(facCounter).Value)
                {
                    var groupId = _repo.FindStudentGroup(group);
                    if (groupId != null)
                    {
                        groupIds.Add(groupId.StudentGroupId);
                    }
                }*/

                var groupIds = _repo
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                    .Select(gif => gif.StudentGroup.StudentGroupId)
                    .ToList();

                var facultyExams = _repo.GetFacultyExams(_repo, groupIds);

                facultyExams = facultyExams
                    .OrderBy(fe => fe.Key)
                    .ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);

                Word.Paragraph oPara1 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara1.Range.Font.Size = 24;
                oPara1.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara1.Range.Text = "Расписание";
                oPara1.Format.SpaceAfter = 0;
                oPara1.Range.InsertParagraphAfter();

                oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara1.Range.Font.Size = 14;
                oPara1.Format.SpaceAfter = 0;
                if (beginSessionDate.Month < 3)
                {
                    var startYear = beginSessionDate.Year - 1;
                    oPara1.Range.Text = "зимней сессии " + startYear + "-" + (startYear + 1) + " учебного года" +
                        Environment.NewLine +
                        faculty.Name;
                }
                else
                {
                    var startYear = beginSessionDate.Year - 1;
                    oPara1.Range.Text = "летней сессии " + startYear + "-" + (startYear + 1) + " учебного года" +
                        Environment.NewLine +
                        faculty.Name;
                }
                oPara1.Range.InsertParagraphAfter();

                Word.Shape signBox = oDoc.Shapes
                    .AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 350, 15, 200, 75, oPara1.Range);

                signBox.Line.Visible = MsoTriState.msoFalse;
                signBox.TextFrame.ContainingRange.ParagraphFormat.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphRight;

                var ProrUchRabNameOption = _repo.GetFirstFiltredConfigOption(co => co.Key == "Проректор по учебной работе");
                var ProrUchRabName = (ProrUchRabNameOption == null) ? "" : ProrUchRabNameOption.Value;

                signBox.TextFrame.ContainingRange.InsertAfter("«УТВЕРЖДАЮ»");
                signBox.TextFrame.ContainingRange.InsertParagraphAfter();
                signBox.TextFrame.ContainingRange.InsertAfter("Проректор по учебной работе");
                signBox.TextFrame.ContainingRange.InsertParagraphAfter();
                signBox.TextFrame.ContainingRange.InsertAfter("____________  " + ProrUchRabName);

                List<StudentGroup> groups = _repo
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                    .Select(gif => gif.StudentGroup)
                    .ToList();

                Word.Table oTable;
                Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oTable = oDoc.Tables.Add(wrdRng, 1 + facultyExams.Keys.Count, 1 + groups.Count());

                //oTable.Rows(1).HeadingFormat = True;
                //oTable.ApplyStyleHeadingRows = True;
                oTable.Rows[1].HeadingFormat = -1;
                oTable.ApplyStyleHeadingRows = true;

                oTable.Borders.Enable = 1;

                for (int i = 1; i <= oTable.Rows.Count; i++)
                {
                    oTable.Rows[i].AllowBreakAcrossPages = (int)Microsoft.Office.Core.MsoTriState.msoFalse;
                }


                oTable.Cell(1, 1).Range.Text = "Дата";
                oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                        Word.WdParagraphAlignment.wdAlignParagraphCenter;
                for (var column = 1; column <= groups.Count(); column++)
                {
                    oTable.Cell(1, column + 1).Range.Text = groups[column - 1].Name;
                    oTable.Cell(1, column + 1).Range.ParagraphFormat.Alignment =
                        Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                for (var row = 2; row <= 1 + facultyExams.Keys.Count; row++)
                {
                    oTable.Cell(row, 1).Range.Text = facultyExams.Keys.ElementAt(row - 2).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU"));
                    oTable.Cell(row, 1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    oTable.Cell(row, 1).Range.ParagraphFormat.Alignment =
                        Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                DateTime currentDate;

                for (var row = 2; row <= 1 + facultyExams.Keys.Count; row++)
                {
                    currentDate = facultyExams.Keys.ElementAt(row - 2);

                    for (var column = 1; column <= groups.Count; column++)
                    {
                        if (facultyExams.ContainsKey(currentDate))
                        {
                            if (facultyExams[currentDate].ContainsKey(groupIds[column - 1]))
                            {
                                var eventCount = facultyExams[currentDate][groupIds[column - 1]].Count;

                                oTable.Cell(row, column + 1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                                var timeTable = oDoc.Tables.Add(oTable.Cell(row, column + 1).Range, 1, 1);
                                timeTable.AutoFitBehavior(Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow);
                                if (eventCount > 1)
                                {
                                    for (int i = 1; i < eventCount; i++)
                                    {
                                        timeTable.Rows.Add();
                                    }
                                }

                                for (int i = 0; i < eventCount; i++)
                                {
                                    string cellText = "";

                                    var evt = facultyExams[currentDate][groupIds[column - 1]][i];

                                    // Консультация || Экзамен                                                                
                                    if (evt.IsExam)
                                    {
                                        cellText += "Экзамен";
                                    }
                                    else
                                    {
                                        cellText += "Консультация";
                                    }

                                    cellText += Environment.NewLine;
                                    cellText += evt.DisciplineName + Environment.NewLine;
                                    cellText += evt.TeacherFIO + Environment.NewLine;
                                    cellText += evt.Time.ToString("H:mm") + Environment.NewLine;
                                    cellText += evt.Auditorium;

                                    oPara1 = oDoc.Content.Paragraphs.Add(timeTable.Cell(i + 1, 1).Range);
                                    oPara1.Range.Font.Size = 10;
                                    oPara1.Format.SpaceAfter = 0;
                                    oPara1.Range.Text = cellText;

                                    if (i != eventCount - 1)
                                    {
                                        timeTable.Cell(i + 1, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].Visible = true;
                                    }
                                }
                            }
                        }
                    }
                }


                oTable.Columns[1].Width = oWord.CentimetersToPoints(3);
                for (int i = 0; i < groups.Count; i++)
                {
                    oTable.Columns[i + 2].Width = oWord.CentimetersToPoints(16 / groups.Count);
                }

                oTable.Rows.Alignment = Word.WdRowAlignment.wdAlignRowCenter;

                Word.Paragraph oPara2 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara2.Range.Font.Size = 12;
                oPara2.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara2.Range.Text = "";
                oPara2.Format.SpaceAfter = 0;
                oPara2.Range.InsertParagraphAfter();


                var HeadUchOtdNameOption = _repo.GetFirstFiltredConfigOption(co => co.Key == "Начальник учебного отдела");
                var HeadUchOtdName = (HeadUchOtdNameOption == null) ? "" : HeadUchOtdNameOption.Value;

                oPara2 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara2.Range.Font.Size = 12;
                oPara2.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara2.Range.Text = "Начальник учебного отдела\t\t" + "_________________  " + HeadUchOtdName;
                oPara2.Format.SpaceAfter = 0;
                oPara2.Range.InsertParagraphAfter();
                oPara2.Range.InsertParagraphAfter();

                oPara2 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara2.Range.Font.Size = 12;
                oPara2.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara2.Range.Text = faculty.SessionSigningTitle + "\t\t_________________  " + faculty.DeanSigningSessionSchedule;
                oPara2.Format.SpaceAfter = 0;
                oPara2.Range.InsertParagraphAfter();
                oPara2.Range.InsertParagraphAfter();

                if (faculty.FacultyId != faculties.OrderBy(f => f.SortingOrder).Last().FacultyId)
                {
                    oDoc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                }

                Application.DoEvents();
            }

            object fileName = Application.StartupPath + @"\Export2.docx";

            //oDoc.SaveAs(ref fileName);

            //oWord.Quit();

        }

        private void DetectSessionDates(out DateTime beginSessionDate, out DateTime endSessionDate)
        {
            var minConsDate = _repo.GetAllExams().Select(e => e.ConsultationDateTime).Min();
            var minExamDate = _repo.GetAllExams().Select(e => e.ExamDateTime).Min();

            beginSessionDate = (minConsDate <= minExamDate) ? minConsDate : minExamDate;

            var maxConsDate = _repo.GetAllExams().Select(e => e.ConsultationDateTime).Max();
            var maxExamDate = _repo.GetAllExams().Select(e => e.ExamDateTime).Max();

            endSessionDate = (maxConsDate <= maxExamDate) ? maxConsDate : maxExamDate;
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            UpdateExamsView();
        }

        private void TeacherSchedule_Click(object sender, EventArgs e)
        {
            var teacherExams = new List<Exam>();

            foreach (var exam in _repo.GetAllExams())
            {
                var tefd = _repo.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == exam.DisciplineId);

                if (tefd != null && tefd.Teacher.TeacherId == (int)TeacherList.SelectedValue)
                {
                    teacherExams.Add(exam);
                }
            }

            var ExamViewList = ExamView.FromExamList(_repo, teacherExams);

            examsView.DataSource = ExamViewList;

            TuneDataView(examsView, DataViews.ExamsView);
        }

        private void AddExamsFromSchedule_Click(object sender, EventArgs e)
        {
            _repo.AddNewExamsFromSchedule(_repo);
        }

        private void RemoveSyncWithSchedule_Click(object sender, EventArgs e)
        {
            _repo.RemoveSyncWithSchedule(_repo);
        }

        private void Auditoriums_Click(object sender, EventArgs e)
        {
            var auds = _repo.GetAuditoriumMap(_repo);

            PutAudsOnGrid(auds);
        }

        private void PutAudsOnGrid(Dictionary<DateTime, Dictionary<int, string>> auds)
        {
            examsView.RowHeadersVisible = true;

            var audsById = _repo.GetAllAuditoriums().ToDictionary(a => a.AuditoriumId, a => a.Name);

            var audList = new List<Auditorium>();
            foreach (var date in auds)
            {
                foreach (var a in date.Value)
                {
                    if (!audList.Select(aud => aud.AuditoriumId).Contains(a.Key))
                    {
                        var aud = _repo.GetAuditorium(a.Key);
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
                var DateItem = auds.OrderBy(a => a.Key).ElementAt(i);

                for (int j = 0; j < examsView.Columns.Count; j++)
                {
                    if (DateItem.Value.ContainsKey(audIdsList[j]))
                    {
                        examsView.Rows[i].Cells[j].Value = DateItem.Value[audIdsList[j]];
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

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            AdjustColumnWidth();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                AdjustColumnWidth();
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

            foreach (var aud in list.OrderBy(a => a.Name))
            {
                result.Add(aud);
            }

            return result;
        }

        private int AuditoriumBuilding(string auditoriumName)
        {
            if (auditoriumName.StartsWith("Корп № 3"))
            {
                return 3;
            }

            if (((auditoriumName.Length >= 6) && (Char.IsDigit(auditoriumName[5]) || auditoriumName == "Ауд. ШКОЛА")) || (auditoriumName == "Ауд. "))
            {
                return 2;
            }

            return 0;
        }       
    }
}
