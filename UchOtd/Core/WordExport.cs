using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Shape = Microsoft.Office.Interop.Word.Shape;
using Schedule.Constants;

namespace UchOtd.Core
{
    public static class WordExport
    {
        public static void ExportWholeSchedule(
            ScheduleRepository _repo,
            string filename,
            bool save,
            bool quit,
            int lessonLength)
        {
            object oMissing = Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);
            int pageCounter = 0;


            foreach (var faculty in _repo.GetAllFaculties().OrderBy(f => f.SortingOrder))
            {
                //var faculty = _repo.GetFirstFiltredFaculty(f => f.Letter == "Д");

                var facultyName = faculty.Name;

                for (int dayOfWeek = 1; dayOfWeek < 7; dayOfWeek++)
                {
                    string dow = Constants.DOWLocal[dayOfWeek];

                    var schedule = _repo.GetFacultyDOWSchedule(faculty.FacultyId, dayOfWeek);

                    Paragraph oPara1;
                    oPara1 = oDoc.Content.Paragraphs.Add();
                    oPara1.Range.Text = "Расписание";
                    oPara1.Range.Font.Bold = 0;
                    oPara1.Range.Font.Size = 10;
                    oPara1.Range.ParagraphFormat.LineSpacingRule =
                        WdLineSpacing.wdLineSpaceSingle;
                    oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oPara1.SpaceAfter = 0;
                    oPara1.Range.InsertParagraphAfter();

                    Range TextBoxRange = oPara1.Range;

                    oPara1 = oDoc.Content.Paragraphs.Add();
                    //oPara1.Range.Text = "второго семестра 2013 – 2014 учебного года";
                    oPara1.Range.Text = DetectSemesterString(_repo);
                    oPara1.Range.Font.Bold = 0;
                    oPara1.Range.Font.Size = 10;
                    oPara1.Range.ParagraphFormat.LineSpacingRule =
                        WdLineSpacing.wdLineSpaceSingle;
                    oPara1.Range.InsertParagraphAfter();

                    oPara1 = oDoc.Content.Paragraphs.Add();
                    oPara1.Range.Text = facultyName;
                    oPara1.Range.Font.Bold = 0;
                    oPara1.Range.Font.Size = 10;
                    oPara1.Range.ParagraphFormat.LineSpacingRule =
                        WdLineSpacing.wdLineSpaceSingle;
                    oPara1.Range.InsertParagraphAfter();

                    oPara1 = oDoc.Content.Paragraphs.Add();
                    oPara1.Range.Font.Size = 14;
                    oPara1.Range.Text = dow.ToUpper();
                    oPara1.Range.Font.Bold = 1;
                    oPara1.Range.ParagraphFormat.LineSpacingRule =
                        WdLineSpacing.wdLineSpaceSingle;
                    oPara1.Range.InsertParagraphAfter();

                    Shape cornerStamp = oDoc.Shapes.AddTextbox(
                        Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
                        oWord.CentimetersToPoints(22f),
                        oWord.CentimetersToPoints(0.5f),
                        200, 50,
                        TextBoxRange);
                    cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                        WdLineSpacing.wdLineSpaceSingle;
                    if (dow == "Понедельник")
                    {

                        cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                            Environment.NewLine + "Проректор по учебной работе" +
                            Environment.NewLine + "______________     А.В.Синицкий";
                        cornerStamp.TextFrame.TextRange.Font.Size = 10;
                        cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    }
                    cornerStamp.TextFrame.WordWrap = 1;
                    cornerStamp.TextFrame.TextRange.ParagraphFormat.SpaceAfter = 0;
                    cornerStamp.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

                    var timeList = new List<string>();
                    foreach (var group in schedule)
                    {
                        foreach (var time in group.Value.Keys)
                        {
                            if (!timeList.Contains(time))
                            {
                                timeList.Add(time);
                            }
                        }
                    }

                    Table oTable;
                    Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                    oTable = oDoc.Tables.Add(wrdRng, 1 + timeList.Count, 1 + schedule.Count);
                    oTable.Borders.Enable = 1;
                    oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                    oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    oTable.Range.Font.Size = 10;
                    oTable.Range.Font.Bold = 0;

                    oTable.Columns[1].Width = oWord.CentimetersToPoints(2.44f);
                    float colWidth = 25.64F / schedule.Count;
                    for (int i = 0; i < schedule.Count; i++)
                    {
                        oTable.Columns[i + 2].Width = oWord.CentimetersToPoints(colWidth);
                    }

                    oTable.Cell(1, 1).Range.Text = "Время";
                    oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;

                    int groupColumn = 2;

                    Dictionary<int, List<int>> plainGroupsListIds = new Dictionary<int, List<int>>();
                    Dictionary<int, List<int>> nGroupsListIds = new Dictionary<int, List<int>>();


                    foreach (var group in schedule)
                    {
                        var groupName = _repo.GetStudentGroup(group.Key).Name;
                        oTable.Cell(1, groupColumn).Range.Text = groupName.Replace(" (+Н)", "");
                        oTable.Cell(1, groupColumn).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                        groupColumn++;

                        if (groupName.Contains(" (+Н)"))
                        {
                            var plainGroupName = groupName.Replace(" (+Н)", "");
                            var nGroupName = groupName.Replace(" (+", "(");

                            var plainGroupId = _repo.FindStudentGroup(plainGroupName).StudentGroupId;
                            var plainStudentIds = _repo.GetAllStudentsInGroups()
                                    .Where(sig => sig.StudentGroup.StudentGroupId == plainGroupId)
                                    .Select(stig => stig.Student.StudentId)
                                    .ToList();
                            plainGroupsListIds.Add(group.Key, _repo.GetAllStudentsInGroups()
                                    .Where(sig => plainStudentIds.Contains(sig.Student.StudentId))
                                    .Select(stig => stig.StudentGroup.StudentGroupId)
                                    .Distinct()
                                    .ToList());

                            var nGroupId = _repo.FindStudentGroup(nGroupName).StudentGroupId;
                            var nStudentIds = _repo.GetAllStudentsInGroups()
                                    .Where(sig => sig.StudentGroup.StudentGroupId == nGroupId)
                                    .Select(stig => stig.Student.StudentId)
                                    .ToList();
                            nGroupsListIds.Add(group.Key, _repo.GetAllStudentsInGroups()
                                    .Where(sig => nStudentIds.Contains(sig.Student.StudentId))
                                    .Select(stig => stig.StudentGroup.StudentGroupId)
                                    .Distinct()
                                    .ToList());
                        }
                    }

                    var timeRowIndexList = new List<int>();

                    var timeRowIndex = 2;
                    foreach (var time in timeList.OrderBy(t => int.Parse(t.Split(':')[0]) * 60 + int.Parse(t.Split(':')[1])))
                    {
                        var Hour = int.Parse(time.Substring(0, 2));
                        var Minute = int.Parse(time.Substring(3, 2));

                        Minute += lessonLength;

                        while (Minute >= 60)
                        {
                            Hour++;
                            Minute -= 60;
                        }


                        timeRowIndexList.Add(timeRowIndex);
                        oTable.Cell(timeRowIndex, 1).Range.Text = time + " - " +
                            Hour.ToString("D2") + ":" + Minute.ToString("D2");
                        oTable.Cell(timeRowIndex, 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(timeRowIndex, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                        var columnGroupIndex = 2;
                        foreach (var group in schedule)
                        {
                            if (group.Value.ContainsKey(time))
                            {
                                var eventCount = group.Value[time].Count;
                                oTable.Cell(timeRowIndex, columnGroupIndex).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                                var timeTable = oDoc.Tables.Add(oTable.Cell(timeRowIndex, columnGroupIndex).Range, 1, 1);
                                for (int i = 0; i < group.Value[time].Count - 1; i++)
                                {
                                    timeTable.Rows.Add();
                                }
                                for (int i = 0; i < group.Value[time].Count - 1; i++)
                                {
                                    timeTable.Cell(i + 1, 1).Borders[WdBorderType.wdBorderBottom].Visible = true;
                                }
                                timeTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                                timeTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                timeTable.Range.Font.Size = 10;
                                timeTable.Range.Font.Bold = 0;

                                var tfdIndex = 0;
                                foreach (var tfdData in group.Value[time].OrderBy(tfd => tfd.Value.Item2.Select(l => _repo.CalculateWeekNumber(l.Calendar.Date)).Min()))
                                {
                                    var cellText = "";
                                    cellText += tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.Name;
                                    var groupId = tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;
                                    if (plainGroupsListIds.ContainsKey(group.Key))
                                    {
                                        if (plainGroupsListIds[group.Key].Contains(groupId) && nGroupsListIds[group.Key].Contains(groupId))
                                        {
                                            //cellText += " (+Н)";
                                        }
                                        if (!plainGroupsListIds[group.Key].Contains(groupId) && nGroupsListIds[group.Key].Contains(groupId))
                                        {
                                            cellText += " (Н)";
                                        }
                                    }
                                    cellText += Environment.NewLine;
                                    cellText += tfdData.Value.Item2[0].TeacherForDiscipline.Teacher.FIO + Environment.NewLine;
                                    cellText += "(" + tfdData.Value.Item1 + ")" + Environment.NewLine;

                                    var audWeekList = tfdData.Value.Item2.ToDictionary(l => _repo.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
                                    var grouped = audWeekList.GroupBy(a => a.Value);

                                    var enumerable = grouped as List<IGrouping<string, KeyValuePair<int, string>>> ?? grouped.ToList();
                                    var gcount = enumerable.Count();
                                    if (gcount == 1)
                                    {
                                        cellText += enumerable.ElementAt(0).Key;
                                    }
                                    else
                                    {
                                        for (int j = 0; j < gcount; j++)
                                        {
                                            var jItem = enumerable.OrderBy(e => e.Select(ag => ag.Key).ToList().Min()).ElementAt(j);
                                            cellText += ScheduleRepository.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " + jItem.Key;

                                            if (j != gcount - 1)
                                            {
                                                cellText += Environment.NewLine;
                                            }
                                        }
                                    }

                                    timeTable.Cell(tfdIndex + 1, 1).Range.Text = cellText;
                                    timeTable.Cell(tfdIndex + 1, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                                    tfdIndex++;

                                }
                            }

                            columnGroupIndex++;
                        }

                        timeRowIndex++;
                    }
                    /*
                    if (dow == "Суббота")
                    {
                        oPara1 = oDoc.Content.Paragraphs.Add();
                        //oPara1.Range.Text = ;
                        oPara1.Range.Font.Bold = 0;
                        oPara1.Range.Font.Size = 10;
                        oPara1.Range.ParagraphFormat.LineSpacingRule =
                            WdLineSpacing.wdLineSpaceSingle;
                    }*/

                    pageCounter++;
                    int pageCount;
                    float FontSize = 10.5F;
                    do
                    {
                        FontSize -= 0.5F;
                        oTable.Range.Font.Size = FontSize;

                        if (FontSize == 3)
                        {
                            break;
                        }

                        pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
                    } while (pageCount > pageCounter);

                    //var endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oTable.Range.Next().Font.Size = 1;
                    oTable.Range.Next().InsertBreak(WdBreakType.wdPageBreak);
                }
            }

            oDoc.Undo();

            if (save)
            {
                object fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + filename;
                oDoc.SaveAs(ref fileName);
            }

            if (quit)
            {
                oWord.Quit();
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
        }

        private static string DetectSemesterString(ScheduleRepository repo)
        {
            var semesterSterts = repo.GetSemesterStarts();
            var ssYear = semesterSterts.Year;

            if (semesterSterts.Month > 6)
            {
                return "первого семестра " + ssYear + " – " + (ssYear + 1) + " учебного года";
            }
            else
            {
                return "второго семестра " + (ssYear - 1) + " – " + ssYear + " учебного года";
            }
        }

        public static void ExportSchedulePage(
            Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>> schedule,
            string facultyName,
            string filename,
            string dow,
            ScheduleRepository _repo,
            bool save,
            bool quit,
            bool print)
        {
            object oMissing = Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);


            Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = "Расписание";
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.SpaceAfter = 0;
            oPara1.Range.InsertParagraphAfter();

            oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = "второго семестра 2013 – 2014 учебного года";
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Range.InsertParagraphAfter();

            oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = facultyName;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Range.InsertParagraphAfter();

            oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Font.Size = 14;
            oPara1.Range.Text = dow.ToUpper();
            oPara1.Range.Font.Bold = 1;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Range.InsertParagraphAfter();

            Shape cornerStamp = oDoc.Shapes.AddTextbox(
                Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
                oWord.CentimetersToPoints(22f),
                oWord.CentimetersToPoints(0.5f),
                200, 50);
            cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            if (dow == "Понедельник")
            {

                cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                    Environment.NewLine + "Проректор по учебной работе" +
                    Environment.NewLine + "______________     А.В.Синицкий";
                cornerStamp.TextFrame.TextRange.Font.Size = 10;
                cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            }
            cornerStamp.TextFrame.WordWrap = 1;
            cornerStamp.TextFrame.TextRange.ParagraphFormat.SpaceAfter = 0;
            cornerStamp.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

            var timeList = new List<string>();
            foreach (var group in schedule)
            {
                foreach (var time in group.Value.Keys)
                {
                    if (!timeList.Contains(time))
                    {
                        timeList.Add(time);
                    }
                }
            }

            Table oTable;
            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            oTable = oDoc.Tables.Add(wrdRng, 1 + timeList.Count, 1 + schedule.Count);
            oTable.Borders.Enable = 1;
            oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
            oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            oTable.Range.Font.Size = 10;
            oTable.Range.Font.Bold = 0;

            oTable.Columns[1].Width = oWord.CentimetersToPoints(2.44f);
            float colWidth = 25.64F / schedule.Count;
            for (int i = 0; i < schedule.Count; i++)
            {
                oTable.Columns[i + 2].Width = oWord.CentimetersToPoints(colWidth);
            }

            oTable.Cell(1, 1).Range.Text = "Время";
            oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;

            int groupColumn = 2;

            Dictionary<int, List<int>> plainGroupsListIds = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> nGroupsListIds = new Dictionary<int, List<int>>();


            foreach (var group in schedule)
            {
                var groupName = _repo.GetStudentGroup(group.Key).Name;
                oTable.Cell(1, groupColumn).Range.Text = groupName.Replace(" (+Н)", "");
                oTable.Cell(1, groupColumn).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                groupColumn++;

                if (groupName.Contains(" (+Н)"))
                {
                    var plainGroupName = groupName.Replace(" (+Н)", "");
                    var nGroupName = groupName.Replace(" (+", "(");

                    var plainGroupId = _repo.FindStudentGroup(plainGroupName).StudentGroupId;
                    var plainStudentIds = _repo.GetAllStudentsInGroups()
                            .Where(sig => sig.StudentGroup.StudentGroupId == plainGroupId)
                            .Select(stig => stig.Student.StudentId)
                            .ToList();
                    plainGroupsListIds.Add(group.Key, _repo.GetAllStudentsInGroups()
                            .Where(sig => plainStudentIds.Contains(sig.Student.StudentId))
                            .Select(stig => stig.StudentGroup.StudentGroupId)
                            .Distinct()
                            .ToList());

                    var nGroupId = _repo.FindStudentGroup(nGroupName).StudentGroupId;
                    var nStudentIds = _repo.GetAllStudentsInGroups()
                            .Where(sig => sig.StudentGroup.StudentGroupId == nGroupId)
                            .Select(stig => stig.Student.StudentId)
                            .ToList();
                    nGroupsListIds.Add(group.Key, _repo.GetAllStudentsInGroups()
                            .Where(sig => nStudentIds.Contains(sig.Student.StudentId))
                            .Select(stig => stig.StudentGroup.StudentGroupId)
                            .Distinct()
                            .ToList());
                }
            }

            var timeRowIndexList = new List<int>();

            var timeRowIndex = 2;
            foreach (var time in timeList.OrderBy(t => int.Parse(t.Split(':')[0]) * 60 + int.Parse(t.Split(':')[1])))
            {
                var Hour = int.Parse(time.Substring(0, 2));
                var Minute = int.Parse(time.Substring(3, 2));

                Minute += 80;

                while (Minute >= 60)
                {
                    Hour++;
                    Minute -= 60;
                }


                timeRowIndexList.Add(timeRowIndex);
                oTable.Cell(timeRowIndex, 1).Range.Text = time + " - " +
                    Hour.ToString("D2") + ":" + Minute.ToString("D2");
                oTable.Cell(timeRowIndex, 1).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(timeRowIndex, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                var columnGroupIndex = 2;
                foreach (var group in schedule)
                {
                    if (group.Value.ContainsKey(time))
                    {
                        var eventCount = group.Value[time].Count;
                        oTable.Cell(timeRowIndex, columnGroupIndex).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                        var timeTable = oDoc.Tables.Add(oTable.Cell(timeRowIndex, columnGroupIndex).Range, 1, 1);
                        for (int i = 0; i < group.Value[time].Count - 1; i++)
                        {
                            timeTable.Rows.Add();
                        }
                        for (int i = 0; i < group.Value[time].Count - 1; i++)
                        {
                            timeTable.Cell(i + 1, 1).Borders[WdBorderType.wdBorderBottom].Visible = true;
                        }
                        timeTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                        timeTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        timeTable.Range.Font.Size = 10;
                        timeTable.Range.Font.Bold = 0;

                        var tfdIndex = 0;
                        foreach (var tfdData in group.Value[time].OrderBy(tfd => tfd.Value.Item2.Select(l => _repo.CalculateWeekNumber(l.Calendar.Date)).Min()))
                        {
                            var cellText = "";
                            cellText += tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.Name;
                            var groupId = tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;

                            if (plainGroupsListIds.ContainsKey(group.Key))
                            {
                                if (plainGroupsListIds[group.Key].Contains(groupId) && nGroupsListIds[group.Key].Contains(groupId))
                                {
                                    cellText += " (+Н)";
                                }
                                if (!plainGroupsListIds[group.Key].Contains(groupId) && nGroupsListIds[group.Key].Contains(groupId))
                                {
                                    cellText += " (Н)";
                                }
                            }
                            cellText += Environment.NewLine;
                            cellText += tfdData.Value.Item2[0].TeacherForDiscipline.Teacher.FIO + Environment.NewLine;
                            cellText += "(" + tfdData.Value.Item1 + ")" + Environment.NewLine;

                            var audWeekList = tfdData.Value.Item2.ToDictionary(l => _repo.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
                            var grouped = audWeekList.GroupBy(a => a.Value);

                            var enumerable = grouped as List<IGrouping<string, KeyValuePair<int, string>>> ?? grouped.ToList();
                            var gcount = enumerable.Count();
                            if (gcount == 1)
                            {
                                cellText += enumerable.ElementAt(0).Key;
                            }
                            else
                            {
                                for (int j = 0; j < gcount; j++)
                                {
                                    var jItem = enumerable.OrderBy(e => e.Select(ag => ag.Key).ToList().Min()).ElementAt(j);
                                    cellText += ScheduleRepository.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " + jItem.Key;

                                    if (j != gcount - 1)
                                    {
                                        cellText += Environment.NewLine;
                                    }
                                }
                            }

                            timeTable.Cell(tfdIndex + 1, 1).Range.Text = cellText;
                            timeTable.Cell(tfdIndex + 1, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                            tfdIndex++;

                        }
                    }

                    columnGroupIndex++;
                }

                timeRowIndex++;
            }


            int pageCount;
            float FontSize = 10.5F;
            do
            {
                FontSize -= 0.5F;
                oTable.Range.Font.Size = FontSize;

                pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
            } while ((pageCount > 1) || FontSize == 3);

            if (save)
            {
                object fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + filename;
                oDoc.SaveAs(ref fileName);
            }

            if (print)
            {
                object copies = "1";
                object pages = "";
                object range = WdPrintOutRange.wdPrintAllDocument;
                object items = WdPrintOutItem.wdPrintDocumentContent;
                object pageType = WdPrintOutPages.wdPrintAllPages;
                object oTrue = true;
                object oFalse = false;
                object missing = Type.Missing;

                oDoc.PrintOut(ref oTrue, ref oFalse, ref range, ref missing, ref missing, ref missing,
                    ref items, ref copies, ref pages, ref pageType, ref oFalse, ref oTrue,
                    ref missing, ref oFalse, ref missing, ref missing, ref missing, ref missing);

                /*oDoc.PrintOut(ref oTrue, ref oFalse, ref range, ref missing, ref missing, ref missing,
                    ref items, ref copies, ref pages, ref pageType, ref oFalse, ref oTrue,
                    ref missing, ref oFalse, ref missing, ref missing, ref missing, ref missing);*/
            }

            if (quit)
            {
                oWord.Quit();
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
        }
    }
}
