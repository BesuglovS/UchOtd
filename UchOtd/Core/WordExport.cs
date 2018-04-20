using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Schedule.Constants;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Repositories.Common;
using UchOtd.Forms;
using UchOtd.NUDS.View;
using UchOtd.Schedule;
using UchOtd.Schedule.Views.DBListViews;
using Application = Microsoft.Office.Interop.Word.Application;
using Shape = Microsoft.Office.Interop.Word.Shape;
using System = Microsoft.Office.Interop.Word.System;
using System.Globalization;

namespace UchOtd.Core
{
    public static class WordExport
    {
        
        public static void ExportSchedulePage(ScheduleRepository repo, string filename, bool save, bool quit, int lessonLength, int facultyId, 
            int dayOfWeek, int daysOfWeek, bool weekFiltered, List<int> weekFilterList, 
            bool weeksMarksVisible, bool onlyFutureDates, bool BottomSignatures, 
            bool SchoolHeader, CancellationToken cToken)
        {
            object oMissing = Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            cToken.ThrowIfCancellationRequested();

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            var faculty = repo.Faculties.GetFaculty(facultyId);

            var dow = Constants.DowLocal[dayOfWeek];

            var schedule = repo.Lessons.GetFacultyDowSchedule(faculty.FacultyId, dayOfWeek, weekFiltered, weekFilterList, false, onlyFutureDates, null);

            Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = "Расписание";
            oPara1.Range.Font.Bold = 0;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.SpaceAfter = 0;
            oPara1.Range.InsertParagraphAfter();

            var textBoxRange = oPara1.Range;

            oPara1 = oDoc.Content.Paragraphs.Add();
            //oPara1.Range.Text = "второго семестра 2013 – 2014 учебного года";
            oPara1.Range.Text = DetectSemesterString(repo);
            oPara1.Range.Font.Bold = 0;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Range.InsertParagraphAfter();

            oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = faculty.Name;
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
                MsoTextOrientation.msoTextOrientationHorizontal,
                oWord.CentimetersToPoints(22f),
                oWord.CentimetersToPoints(0.5f),
                200, 50,
                textBoxRange);
            cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            if (dow == "Понедельник")
            {
                if (SchoolHeader)
                {
                    cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                                                           Environment.NewLine + "Ректор   " +
                                                           "______________     Наянова М.В." +
                                                           Environment.NewLine + "«___» ____________  20__ г.";
                    cornerStamp.TextFrame.TextRange.Font.Size = 10;
                    cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphRight;
                }
                else
                {
                    var prorUchRabNameOption = repo.ConfigOptions.GetFirstFiltredConfigOption(co => co.Key == "Проректор по учебной работе");
                    var prorUchRabName = (prorUchRabNameOption == null) ? "" : prorUchRabNameOption.Value;

                    cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                                                           Environment.NewLine + "Проректор по учебной работе" +
                                                           Environment.NewLine + "______________     " + prorUchRabName;
                    cornerStamp.TextFrame.TextRange.Font.Size = 10;
                    cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                }
            }

            cornerStamp.TextFrame.WordWrap = 1;
            cornerStamp.TextFrame.TextRange.ParagraphFormat.SpaceAfter = 0;
            cornerStamp.Line.Visible = MsoTriState.msoFalse;

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

            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            cToken.ThrowIfCancellationRequested();

            Table oTable = oDoc.Tables.Add(wrdRng, 1 + timeList.Count, 1 + schedule.Count);
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

            var plainGroupsListIds = new Dictionary<int, List<int>>();
            var nGroupsListIds = new Dictionary<int, List<int>>();
            var plainNGroupIds = new Dictionary<int, Tuple<int, int>>();

            foreach (var group in schedule)
            {
                var groupObject = repo.StudentGroups.GetStudentGroup(group.Key);
                var groupName = groupObject.Name;
                oTable.Cell(1, groupColumn).Range.Text = groupName.Replace(" (+Н)", "");
                oTable.Cell(1, groupColumn).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                groupColumn++;

                if (groupName.Contains(" (+Н)"))
                {
                    var plainGroupName = groupName.Replace(" (+Н)", "");
                    var nGroupName = groupName.Replace(" (+", "(");

                    var plainGroupId = repo.StudentGroups.FindStudentGroup(plainGroupName).StudentGroupId;
                    var plainStudentIds = repo.StudentsInGroups.GetAllStudentsInGroups()
                            .Where(sig => sig.StudentGroup.StudentGroupId == plainGroupId)
                            .Select(stig => stig.Student.StudentId)
                            .ToList();
                    plainGroupsListIds.Add(group.Key, repo.StudentsInGroups.GetAllStudentsInGroups()
                            .Where(sig => plainStudentIds.Contains(sig.Student.StudentId))
                            .Select(stig => stig.StudentGroup.StudentGroupId)
                            .Distinct()
                            .ToList());

                    var nGroupId = repo.StudentGroups.FindStudentGroup(nGroupName).StudentGroupId;
                    var nStudentIds = repo.StudentsInGroups.GetAllStudentsInGroups()
                            .Where(sig => sig.StudentGroup.StudentGroupId == nGroupId)
                            .Select(stig => stig.Student.StudentId)
                            .ToList();
                    nGroupsListIds.Add(group.Key, repo.StudentsInGroups.GetAllStudentsInGroups()
                            .Where(sig => nStudentIds.Contains(sig.Student.StudentId))
                            .Select(stig => stig.StudentGroup.StudentGroupId)
                            .Distinct()
                            .ToList());

                    plainNGroupIds.Add(groupObject.StudentGroupId, new Tuple<int, int>(plainGroupId, nGroupId));
                }
            }

            cToken.ThrowIfCancellationRequested();

            var timeRowIndexList = new List<int>();

            var timeRowIndex = 2;
            foreach (var time in timeList.OrderBy(t => int.Parse(t.Split(':')[0]) * 60 + int.Parse(t.Split(':')[1])))
            {
                cToken.ThrowIfCancellationRequested();

                var hour = int.Parse(time.Substring(0, 2));
                var minute = int.Parse(time.Substring(3, 2));

                minute += lessonLength;

                while (minute >= 60)
                {
                    hour++;
                    minute -= 60;
                }


                timeRowIndexList.Add(timeRowIndex);
                oTable.Cell(timeRowIndex, 1).Range.Text = time + " - " +
                    hour.ToString("D2") + ":" + minute.ToString("D2");
                oTable.Cell(timeRowIndex, 1).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(timeRowIndex, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                var columnGroupIndex = 2;
                foreach (var group in schedule)
                {
                    if (group.Value.ContainsKey(time))
                    {
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
                        foreach (var tfdData in group.Value[time].OrderBy(tfd => tfd.Value.Item2.Select(lwt => repo.CommonFunctions.CalculateWeekNumber(lwt.Item1.Calendar.Date)).Min()))
                        {
                            var cellText = "";

                            // Discipline name
                            var primaryDisciplineName = tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.Name;
                            var names =
                                repo.DisciplineNames.GetDisciplineNamesDictionary(
                                    tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline);

                            if (names.ContainsKey(group.Key))
                            {
                                cellText += names[group.Key];
                            }
                            else
                            {
                                cellText += primaryDisciplineName;
                            }


                            // N + Group modifiers
                            var groupId = tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;
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

                            var tfdGroupId = tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;
                            if ((tfdGroupId != group.Key))
                            {
                                if ((!plainNGroupIds.ContainsKey(group.Key)) || ((tfdGroupId != plainNGroupIds[group.Key].Item1) && (tfdGroupId != plainNGroupIds[group.Key].Item2)))
                                {
                                    cellText += " (" + tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.Name + ")";
                                }
                            }

                            cellText += Environment.NewLine;
                            // Teacher FIO
                            cellText += tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Teacher.FIO + Environment.NewLine;

                            // Total weeks
                            if (weeksMarksVisible)
                            {
                                cellText += "(" + tfdData.Value.Item1 + ")" + Environment.NewLine;
                            }

                            var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date), l => l.Item1.Auditorium.Name);
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
                                    cellText += CommonFunctions.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " + jItem.Key;

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

            if (BottomSignatures && (dayOfWeek == daysOfWeek))
            {
                var oPara3 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara3.Range.Font.Size = 12;
                oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara3.Range.Text = "";
                oPara3.Format.SpaceAfter = 0;
                oPara3.Range.InsertParagraphAfter();

                var headUchOtdNameOption = repo.ConfigOptions.GetFirstFiltredConfigOption(co => co.Key == "Начальник учебного отдела");
                var headUchOtdName = (headUchOtdNameOption == null) ? "" : headUchOtdNameOption.Value;

                oPara3 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara3.Range.Text = "Начальник учебного отдела\t\t" + "_________________  " + headUchOtdName;
                oPara3.Range.Font.Size = 12;
                oPara3.Range.Font.Bold = 0;
                oPara3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara3.Format.SpaceAfter = 0;
                oPara3.Range.InsertParagraphAfter();
                oPara3.Range.InsertParagraphAfter();

                oPara3 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                //"Декан " + UchOtd.NUDS.Core.Constants.facultyTitles[facCounter] + "\t\t_________________  "
                //+ UchOtd.NUDS.Core.Constants.HeadsOfFaculties.ElementAt(facCounter).Value;
                oPara3.Range.Text = faculty.ScheduleSigningTitle + "\t\t_________________  " + faculty.DeanSigningSchedule;
                oPara3.Range.Font.Size = 12;
                oPara3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara3.Range.Font.Bold = 0;
                oPara3.Format.SpaceAfter = 0;
            }

            cToken.ThrowIfCancellationRequested();

            int pageCount;
            var fontSize = 10.5F;
            do
            {
                fontSize -= 0.5F;
                oTable.Range.Font.Size = fontSize;

                if (fontSize <= 3)
                {
                    break;
                }

                pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
            } while (pageCount > 1);

            if (save)
            {
                object fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + filename;
                oDoc.SaveAs(ref fileName);
            }

            if (quit)
            {
                oWord.Quit();
            }

            Marshal.ReleaseComObject(oWord);
        }

        public static void ExportTwoSchedulePages(
            ScheduleRepository repo, string filename, bool save, bool quit,
            int lessonLength, int facultyId, int dayOfWeek, int daysOfWeek,
            bool weekFiltered, List<int> weekFilterList, bool weeksMarksVisible)
        {
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            var faculty = repo.Faculties.GetFaculty(facultyId);

            var firstDaySchedule = repo.Lessons.GetFacultyDowSchedule(faculty.FacultyId, dayOfWeek, weekFiltered, weekFilterList, false, false, null);

            var firstDayTable = PutDayScheduleInWord(repo, lessonLength, weeksMarksVisible, firstDaySchedule, oDoc, oEndOfDoc, oWord, null, dayOfWeek);

            Table secondDayTable = null;

            if (dayOfWeek != 7)
            {
                var secondDaySchedule = repo.Lessons.GetFacultyDowSchedule(faculty.FacultyId, dayOfWeek + 1, weekFiltered, weekFilterList, false, false, null);
                secondDayTable = PutDayScheduleInWord(repo, lessonLength, weeksMarksVisible, secondDaySchedule, oDoc, oEndOfDoc, oWord, firstDayTable, dayOfWeek + 1);
            }

            int pageCount;
            var fontSize = 12.5F;
            do
            {
                fontSize -= 0.5F;
                firstDayTable.Range.Font.Size = fontSize;
                if (secondDayTable != null)
                {
                    secondDayTable.Range.Font.Size = fontSize;
                }

                if (fontSize <= 3)
                {
                    break;
                }

                pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
            } while (pageCount > 1);

            if (save)
            {
                object fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + filename;
                oDoc.SaveAs(ref fileName);
            }

            if (quit)
            {
                oWord.Quit();
            }

            Marshal.ReleaseComObject(oWord);
        }

        private static Table PutDayScheduleInWord(ScheduleRepository repo, int lessonLength, bool weeksMarksVisible,
            Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Tuple<Lesson, int>>, string>>>> schedule, _Document oDoc, object oEndOfDoc, _Application oWord, Table table, int dayOfWeek)
        {
            var timeList = new List<string>();
            foreach (var group in schedule)
            {
                foreach (var time in @group.Value.Keys)
                {
                    if (!timeList.Contains(time))
                    {
                        timeList.Add(time);
                    }
                }
            }

            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            Table oTable;

            var plainGroupsListIds = new Dictionary<int, List<int>>();
            var nGroupsListIds = new Dictionary<int, List<int>>();
            var plainNGroupIds = new Dictionary<int, Tuple<int, int>>();

            int tableRowOffset = 0;

            if (table != null)
            {
                oTable = table;
                tableRowOffset = oTable.Rows.Count;

                for (int i = 0; i < 1 + timeList.Count; i++)
                {
                    oTable.Rows.Add();
                }
            }
            else
            {

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
            }

            oTable.Cell(tableRowOffset + 1, 1).Range.Text = Constants.DowLocal[dayOfWeek];
            oTable.Cell(tableRowOffset + 1, 1).Range.ParagraphFormat.Alignment =
                WdParagraphAlignment.wdAlignParagraphCenter;

            int groupColumn = 2;

            foreach (var group in schedule)
            {
                var groupObject = repo.StudentGroups.GetStudentGroup(@group.Key);
                var groupName = groupObject.Name;

                oTable.Cell(tableRowOffset + 1, groupColumn).Range.Text = groupName.Replace(" (+Н)", "");
                oTable.Cell(tableRowOffset + 1, groupColumn).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                groupColumn++;

                if (groupName.Contains(" (+Н)"))
                {
                    var plainGroupName = groupName.Replace(" (+Н)", "");
                    var nGroupName = groupName.Replace(" (+", "(");

                    var plainGroupId = repo.StudentGroups.FindStudentGroup(plainGroupName).StudentGroupId;
                    var plainStudentIds = repo.StudentsInGroups.GetAllStudentsInGroups()
                        .Where(sig => sig.StudentGroup.StudentGroupId == plainGroupId)
                        .Select(stig => stig.Student.StudentId)
                        .ToList();
                    plainGroupsListIds.Add(@group.Key, repo.StudentsInGroups.GetAllStudentsInGroups()
                        .Where(sig => plainStudentIds.Contains(sig.Student.StudentId))
                        .Select(stig => stig.StudentGroup.StudentGroupId)
                        .Distinct()
                        .ToList());

                    var nGroupId = repo.StudentGroups.FindStudentGroup(nGroupName).StudentGroupId;
                    var nStudentIds = repo.StudentsInGroups.GetAllStudentsInGroups()
                        .Where(sig => sig.StudentGroup.StudentGroupId == nGroupId)
                        .Select(stig => stig.Student.StudentId)
                        .ToList();
                    nGroupsListIds.Add(@group.Key, repo.StudentsInGroups.GetAllStudentsInGroups()
                        .Where(sig => nStudentIds.Contains(sig.Student.StudentId))
                        .Select(stig => stig.StudentGroup.StudentGroupId)
                        .Distinct()
                        .ToList());

                    plainNGroupIds.Add(groupObject.StudentGroupId, new Tuple<int, int>(plainGroupId, nGroupId));
                }
            }

            var timeRowIndexList = new List<int>();

            var timeRowIndex = 2;
            foreach (var time in timeList.OrderBy(t => int.Parse(t.Split(':')[0]) * 60 + int.Parse(t.Split(':')[1])))
            {
                var hour = int.Parse(time.Substring(0, 2));
                var minute = int.Parse(time.Substring(3, 2));

                minute += lessonLength;

                while (minute >= 60)
                {
                    hour++;
                    minute -= 60;
                }


                timeRowIndexList.Add(timeRowIndex);
                oTable.Cell(tableRowOffset + timeRowIndex, 1).Range.Text = time + " - " +
                                                          hour.ToString("D2") + ":" + minute.ToString("D2");
                oTable.Cell(tableRowOffset + timeRowIndex, 1).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(tableRowOffset + timeRowIndex, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                var columnGroupIndex = 2;
                foreach (var group in schedule)
                {
                    if (@group.Value.ContainsKey(time))
                    {
                        oTable.Cell(tableRowOffset + timeRowIndex, columnGroupIndex).VerticalAlignment =
                            WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                        var timeTable = oDoc.Tables.Add(oTable.Cell(tableRowOffset + timeRowIndex, columnGroupIndex).Range, 1, 1);
                        for (int i = 0; i < @group.Value[time].Count - 1; i++)
                        {
                            timeTable.Rows.Add();
                        }
                        for (int i = 0; i < @group.Value[time].Count - 1; i++)
                        {
                            timeTable.Cell(i + 1, 1).Borders[WdBorderType.wdBorderBottom].Visible = true;
                        }
                        timeTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                        timeTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        timeTable.Range.Font.Size = 10;
                        timeTable.Range.Font.Bold = 0;

                        var tfdIndex = 0;
                        foreach (
                            var tfdData in
                                @group.Value[time].OrderBy(
                                    tfd => tfd.Value.Item2.Select(l => repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date)).Min()))
                        {
                            var cellText = "";
                            // Discipline name
                            cellText += tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.Name;

                            // N + Group modifiers
                            var groupId = tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;
                            if (plainGroupsListIds.ContainsKey(@group.Key))
                            {
                                if (plainGroupsListIds[@group.Key].Contains(groupId) &&
                                    nGroupsListIds[@group.Key].Contains(groupId))
                                {
                                    cellText += " (+Н)";
                                }
                                if (!plainGroupsListIds[@group.Key].Contains(groupId) &&
                                    nGroupsListIds[@group.Key].Contains(groupId))
                                {
                                    cellText += " (Н)";
                                }
                            }

                            var tfdGroupId = tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;
                            if ((tfdGroupId != @group.Key))
                            {
                                if ((!plainNGroupIds.ContainsKey(@group.Key)) ||
                                    ((tfdGroupId != plainNGroupIds[@group.Key].Item1) &&
                                     (tfdGroupId != plainNGroupIds[@group.Key].Item2)))
                                {
                                    cellText += " (" + tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.Name +
                                                ")";
                                }
                            }

                            cellText += Environment.NewLine;
                            // Teacher FIO
                            cellText += tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Teacher.FIO + Environment.NewLine;

                            // Total weeks
                            if (weeksMarksVisible)
                            {
                                cellText += "(" + tfdData.Value.Item1 + ")" + Environment.NewLine;
                            }

                            var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date),
                                l => l.Item1.Auditorium.Name);
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
                                    cellText += CommonFunctions.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " +
                                                jItem.Key;

                                    if (j != gcount - 1)
                                    {
                                        cellText += Environment.NewLine;
                                    }
                                }
                            }

                            timeTable.Cell(tfdIndex + 1, 1).Range.Text = cellText;
                            timeTable.Cell(tfdIndex + 1, 1).VerticalAlignment =
                                WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                            tfdIndex++;
                        }
                    }

                    columnGroupIndex++;
                }

                timeRowIndex++;
            }
            return oTable;
        }

        public static void ExportWholeSchedule(
            ScheduleRepository repo,
            string filename,
            bool save,
            bool quit,
            int lessonLength,
            int facultyFilter,
            int daysOfWeek,
            bool schoolHeader,
            bool futureDatesOnly)
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

            List<Faculty> facultiesList = repo.Faculties.GetAllFaculties().OrderBy(f => f.SortingOrder).ToList();

            if (facultyFilter != -1)
            {
                facultiesList = new List<Faculty> { repo.Faculties.GetFaculty(facultyFilter) };
            }


            foreach (var faculty in facultiesList)
            {
                var facultyName = faculty.Name;

                for (int dayOfWeek = 1; dayOfWeek <= daysOfWeek; dayOfWeek++)
                {
                    string dow = Constants.DowLocal[dayOfWeek];

                    var schedule = repo.Lessons.GetFacultyDowSchedule(faculty.FacultyId, dayOfWeek, false, null, false, futureDatesOnly, null);

                    Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
                    oPara1.Range.Text = "Расписание";
                    oPara1.Range.Font.Bold = 0;
                    oPara1.Range.Font.Size = 10;
                    oPara1.Range.ParagraphFormat.LineSpacingRule =
                        WdLineSpacing.wdLineSpaceSingle;
                    oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oPara1.SpaceAfter = 0;
                    oPara1.Range.InsertParagraphAfter();

                    var textBoxRange = oPara1.Range;

                    oPara1 = oDoc.Content.Paragraphs.Add();
                    //oPara1.Range.Text = "второго семестра 2013 – 2014 учебного года";
                    oPara1.Range.Text = DetectSemesterString(repo);
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
                        MsoTextOrientation.msoTextOrientationHorizontal,
                        oWord.CentimetersToPoints(22f),
                        oWord.CentimetersToPoints(0.5f),
                        200, 50,
                        textBoxRange);
                    cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                        WdLineSpacing.wdLineSpaceSingle;
                    if (dow == "Понедельник")
                    {
                        if (!schoolHeader)
                        {
                            var prorUchRabNameOption = repo.ConfigOptions.GetFirstFiltredConfigOption(co => co.Key == "Проректор по учебной работе");
                            var prorUchRabName = (prorUchRabNameOption == null) ? "" : prorUchRabNameOption.Value;

                            cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                                Environment.NewLine + "Проректор по учебной работе" +
                                Environment.NewLine + "______________     " + prorUchRabName;
                            cornerStamp.TextFrame.TextRange.Font.Size = 10;
                            cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        }
                        else
                        {
                            cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                                Environment.NewLine + "Ректор   " + "______________     Наянова М.В." +
                                Environment.NewLine + "«___» ____________  20__ г.";
                            cornerStamp.TextFrame.TextRange.Font.Size = 10;
                            cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        }
                    }
                    cornerStamp.TextFrame.WordWrap = 1;
                    cornerStamp.TextFrame.TextRange.ParagraphFormat.SpaceAfter = 0;
                    cornerStamp.Line.Visible = MsoTriState.msoFalse;

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

                    Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                    Table oTable = oDoc.Tables.Add(wrdRng, 1 + timeList.Count, 1 + schedule.Count);
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

                    var plainGroupsListIds = new Dictionary<int, List<int>>();
                    var nGroupsListIds = new Dictionary<int, List<int>>();


                    foreach (var group in schedule)
                    {
                        var groupName = repo.StudentGroups.GetStudentGroup(group.Key).Name;
                        oTable.Cell(1, groupColumn).Range.Text = groupName.Replace(" (+Н)", "");
                        oTable.Cell(1, groupColumn).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                        groupColumn++;

                        if (groupName.Contains(" (+Н)"))
                        {
                            var plainGroupName = groupName.Replace(" (+Н)", "");
                            var nGroupName = groupName.Replace(" (+", "(");

                            var plainGroupId = repo.StudentGroups.FindStudentGroup(plainGroupName).StudentGroupId;
                            var plainStudentIds = repo.StudentsInGroups.GetAllStudentsInGroups()
                                    .Where(sig => sig.StudentGroup.StudentGroupId == plainGroupId)
                                    .Select(stig => stig.Student.StudentId)
                                    .ToList();
                            plainGroupsListIds.Add(group.Key, repo.StudentsInGroups.GetAllStudentsInGroups()
                                    .Where(sig => plainStudentIds.Contains(sig.Student.StudentId))
                                    .Select(stig => stig.StudentGroup.StudentGroupId)
                                    .Distinct()
                                    .ToList());

                            var nGroupId = repo.StudentGroups.FindStudentGroup(nGroupName).StudentGroupId;
                            var nStudentIds = repo.StudentsInGroups.GetAllStudentsInGroups()
                                    .Where(sig => sig.StudentGroup.StudentGroupId == nGroupId)
                                    .Select(stig => stig.Student.StudentId)
                                    .ToList();
                            nGroupsListIds.Add(group.Key, repo.StudentsInGroups.GetAllStudentsInGroups()
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
                        var hour = int.Parse(time.Substring(0, 2));
                        var minute = int.Parse(time.Substring(3, 2));

                        minute += lessonLength;

                        while (minute >= 60)
                        {
                            hour++;
                            minute -= 60;
                        }


                        timeRowIndexList.Add(timeRowIndex);
                        oTable.Cell(timeRowIndex, 1).Range.Text = time + " - " +
                            hour.ToString("D2") + ":" + minute.ToString("D2");
                        oTable.Cell(timeRowIndex, 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(timeRowIndex, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                        var columnGroupIndex = 2;
                        foreach (var group in schedule)
                        {
                            if (group.Value.ContainsKey(time))
                            {
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
                                foreach (var tfdData in group.Value[time].OrderBy(tfd => tfd.Value.Item2.Select(l => repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date)).Min()))
                                {
                                    var cellText = "";
                                    cellText += tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.Name;
                                    var groupId = tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;
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
                                    cellText += tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Teacher.FIO + Environment.NewLine;
                                    cellText += "(" + tfdData.Value.Item1 + ")" + Environment.NewLine;

                                    var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date), l => l.Item1.Auditorium.Name);
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
                                            cellText += CommonFunctions.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " + jItem.Key;

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

                    if (dayOfWeek == daysOfWeek)
                    {
                        var oPara3 =
                            oDoc.Content.Paragraphs.Add(ref oMissing);
                        oPara3.Range.Font.Size = 12;
                        oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                        oPara3.Range.Text = "";
                        oPara3.Format.SpaceAfter = 0;
                        oPara3.Range.InsertParagraphAfter();

                        var headUchOtdNameOption = repo.ConfigOptions.GetFirstFiltredConfigOption(co => co.Key == "Начальник учебного отдела");
                        var headUchOtdName = (headUchOtdNameOption == null) ? "" : headUchOtdNameOption.Value;

                        oPara3 =
                            oDoc.Content.Paragraphs.Add(ref oMissing);
                        oPara3.Range.Text = "Начальник учебного отдела\t\t" + "_________________  " + headUchOtdName;
                        oPara3.Range.Font.Size = 12;
                        oPara3.Range.Font.Bold = 0;
                        oPara3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                        oPara3.Format.SpaceAfter = 0;
                        oPara3.Range.InsertParagraphAfter();
                        oPara3.Range.InsertParagraphAfter();

                        oPara3 =
                            oDoc.Content.Paragraphs.Add(ref oMissing);
                        //"Декан " + UchOtd.NUDS.Core.Constants.facultyTitles[facCounter] + "\t\t_________________  "
                        //+ UchOtd.NUDS.Core.Constants.HeadsOfFaculties.ElementAt(facCounter).Value;
                        oPara3.Range.Text = faculty.ScheduleSigningTitle + "\t\t_________________  " + faculty.DeanSigningSchedule;
                        oPara3.Range.Font.Size = 12;
                        oPara3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                        oPara3.Range.Font.Bold = 0;
                        oPara3.Format.SpaceAfter = 0;
                        oPara3.Range.InsertParagraphAfter();
                        oPara3.Range.InsertParagraphAfter();
                    }

                    pageCounter++;
                    int pageCount;
                    float fontSize = 10.5F;
                    do
                    {
                        fontSize -= 0.5F;
                        oTable.Range.Font.Size = fontSize;

                        if (fontSize <= 3)
                        {
                            break;
                        }

                        pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
                    } while (pageCount > pageCounter);

                    var endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    endOfDoc.Font.Size = 1;
                    endOfDoc.InsertBreak(WdBreakType.wdSectionBreakNextPage);
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

            Marshal.ReleaseComObject(oWord);
        }

        public static void ExportCustomSchedule(
            // facultyId, List of DOW
            ScheduleRepository repo, Dictionary<int, List<int>> facultyDow,
            string filename, bool save, bool quit, int lessonLength, int daysOfWeek,
            bool schoolHeader, bool onlyFutureDates, bool weekFiltered, List<int> weekFilterList, bool appVisible,
            CancellationToken cToken, Dictionary<string, List<string>> restrictions)
        {
            object oMissing = Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            cToken.ThrowIfCancellationRequested();

            var groupRestrictions = (restrictions == null || restrictions.Count == 0) ? null : (restrictions[restrictions.Keys.First()]);

            //Start Word and create a new document.
            _Application oWord = new Application();
            if (appVisible)
            {
                oWord.Visible = true;
            }
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);
            int pageCounter = 0;

            List<Faculty> facultiesList = repo.Faculties.GetAllFaculties().OrderBy(f => f.SortingOrder).ToList();

            foreach (var faculty in facultiesList)
            {
                var facultyName = faculty.Name;

                if (!facultyDow.ContainsKey(faculty.FacultyId))
                {
                    continue;
                }
                

                for (int dayOfWeek = 1; dayOfWeek <= daysOfWeek; dayOfWeek++)
                {
                    if (!facultyDow[faculty.FacultyId].Contains(dayOfWeek))
                    {
                        continue;
                    }

                    var fList = new List<string> {"1-е классы", "2-е классы", "3-е классы", "4-е классы", "5-е классы", "6-е классы", "7-е классы"};

                    Table oTable, oTable2 = null;

                    if (fList.Contains(faculty.Name))
                    {
                        Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
                        oPara1.Range.Text = "Расписание " + DetectSemesterString(repo);
                        oPara1.Range.Font.Bold = 0;
                        oPara1.Range.Font.Size = 10;
                        oPara1.Range.ParagraphFormat.LineSpacingRule =
                            WdLineSpacing.wdLineSpaceSingle;
                        oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        oPara1.SpaceAfter = 0;
                        oPara1.Range.InsertParagraphAfter();

                        var textBoxRange = oPara1.Range;

                        oPara1 = oDoc.Content.Paragraphs.Add();
                        oPara1.Range.Font.Size = 14;
                        oPara1.Range.Text = Constants.DowLocal[dayOfWeek].ToUpper();
                        oPara1.Range.Font.Bold = 1;
                        //oPara1.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                        //oPara1.Range.ParagraphFormat.LineSpacingRule =
                        //    WdLineSpacing.wdLineSpaceSingle;
                        oPara1.Range.InsertParagraphAfter();

                        Shape cornerStamp = oDoc.Shapes.AddTextbox(
                            MsoTextOrientation.msoTextOrientationHorizontal,
                            oWord.CentimetersToPoints(22f),
                            oWord.CentimetersToPoints(0.5f),
                            200, 50,
                            textBoxRange);
                        cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                            WdLineSpacing.wdLineSpaceSingle;

                        if (dayOfWeek == 1)
                        {
                            cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                                                                   Environment.NewLine +
                                                                   "Ректор   ______________     Наянова М.В.   «___» ____________  20__ г.";
                            cornerStamp.TextFrame.TextRange.Font.Size = 10;
                            cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        }
                        cornerStamp.TextFrame.WordWrap = 1;
                        cornerStamp.TextFrame.TextRange.ParagraphFormat.SpaceAfter = 0;
                        cornerStamp.Line.Visible = MsoTriState.msoFalse;

                        cToken.ThrowIfCancellationRequested();
                        

                        oTable = GetAndPutDowStartSchedule2(repo, dayOfWeek, weekFiltered, weekFilterList, !weekFiltered, faculty, oDoc, oEndOfDoc, oWord, groupRestrictions, cToken);
                        
                        if (dayOfWeek != 7)
                        {
                            oPara1 = oDoc.Content.Paragraphs.Add();
                            oPara1.Range.Font.Size = 14;
                            oPara1.Range.Text = Constants.DowLocal[dayOfWeek+1].ToUpper();
                            oPara1.Range.Font.Bold = 1;
                            oPara1.Range.InsertParagraphAfter();

                            oTable2 = GetAndPutDowStartSchedule2(repo, dayOfWeek + 1, weekFiltered, weekFilterList, !weekFiltered, faculty, oDoc, oEndOfDoc, oWord, groupRestrictions, cToken);
                        }
                        
                        Range wrdRng2 = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                        wrdRng2.InsertParagraphAfter();
                    }
                    else
                    {
                        string dow = Constants.DowLocal[dayOfWeek];

                        cToken.ThrowIfCancellationRequested();

                        var schedule = repo.Lessons.GetFacultyDowSchedule(faculty.FacultyId, dayOfWeek, weekFiltered,
                            weekFilterList, false, onlyFutureDates, groupRestrictions);

                        cToken.ThrowIfCancellationRequested();

                        Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
                        oPara1.Range.Text = "Расписание";
                        oPara1.Range.Font.Bold = 0;
                        oPara1.Range.Font.Size = 10;
                        oPara1.Range.ParagraphFormat.LineSpacingRule =
                            WdLineSpacing.wdLineSpaceSingle;
                        oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        oPara1.SpaceAfter = 0;
                        oPara1.Range.InsertParagraphAfter();

                        Range textBoxRange = oPara1.Range;

                        oPara1 = oDoc.Content.Paragraphs.Add();
                        //oPara1.Range.Text = "второго семестра 2013 – 2014 учебного года";
                        oPara1.Range.Text = DetectSemesterString(repo);
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
                            MsoTextOrientation.msoTextOrientationHorizontal,
                            oWord.CentimetersToPoints(22f),
                            oWord.CentimetersToPoints(0.5f),
                            200, 50,
                            textBoxRange);
                        cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                            WdLineSpacing.wdLineSpaceSingle;
                        if (dow == "Понедельник")
                        {
                            if (!schoolHeader)
                            {
                                var prorUchRabNameOption =
                                    repo.ConfigOptions.GetFirstFiltredConfigOption(
                                        co => co.Key == "Проректор по учебной работе");
                                var prorUchRabName = (prorUchRabNameOption == null) ? "" : prorUchRabNameOption.Value;

                                cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                                                                       Environment.NewLine +
                                                                       "Проректор по учебной работе" +
                                                                       Environment.NewLine + "______________     " +
                                                                       prorUchRabName;
                                cornerStamp.TextFrame.TextRange.Font.Size = 10;
                                cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment =
                                    WdParagraphAlignment.wdAlignParagraphRight;
                            }
                            else
                            {
                                cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                                                                       Environment.NewLine + "Ректор   " +
                                                                       "______________     Наянова М.В." +
                                                                       Environment.NewLine +
                                                                       "«___» ____________  20__ г.";
                                cornerStamp.TextFrame.TextRange.Font.Size = 10;
                                cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment =
                                    WdParagraphAlignment.wdAlignParagraphRight;
                            }
                        }
                        cornerStamp.TextFrame.WordWrap = 1;
                        cornerStamp.TextFrame.TextRange.ParagraphFormat.SpaceAfter = 0;
                        cornerStamp.Line.Visible = MsoTriState.msoFalse;

                        //qrCode 
                        /*
                        Shape qrStamp = oDoc.Shapes.AddTextbox(
                            MsoTextOrientation.msoTextOrientationHorizontal,
                            oWord.CentimetersToPoints(2f),
                            oWord.CentimetersToPoints(0.5f),
                            200, 50,
                            textBoxRange);
                        qrStamp.Line.Visible = MsoTriState.msoFalse;
    
                        object f = false;
                        object tr = true;
                        qrStamp.TextFrame.TextRange.InlineShapes.AddPicture(@"d:\qrcode.png", ref f, ref tr, qrStamp.TextFrame.TextRange);
                        */

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

                        var plainGroupsListIds = new Dictionary<int, List<int>>();
                        var nGroupsListIds = new Dictionary<int, List<int>>();
                        var plainNGroupIds = new Dictionary<int, Tuple<int, int>>();

                        foreach (var group in schedule)
                        {
                            var groupObject = repo.StudentGroups.GetStudentGroup(group.Key);
                            var groupName = groupObject.Name;
                            oTable.Cell(1, groupColumn).Range.Text = groupName.Replace(" (+Н)", "");
                            oTable.Cell(1, groupColumn).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;
                            groupColumn++;

                            if (groupName.Contains(" (+Н)"))
                            {
                                var plainGroupName = groupName.Replace(" (+Н)", "");
                                var nGroupName = groupName.Replace(" (+", "(");

                                var plainGroupId = repo.StudentGroups.FindStudentGroup(plainGroupName).StudentGroupId;
                                var plainStudentIds = repo.StudentsInGroups.GetAllStudentsInGroups()
                                    .Where(sig => sig.StudentGroup.StudentGroupId == plainGroupId)
                                    .Select(stig => stig.Student.StudentId)
                                    .ToList();
                                plainGroupsListIds.Add(group.Key, repo.StudentsInGroups.GetAllStudentsInGroups()
                                    .Where(sig => plainStudentIds.Contains(sig.Student.StudentId))
                                    .Select(stig => stig.StudentGroup.StudentGroupId)
                                    .Distinct()
                                    .ToList());

                                var nGroupId = repo.StudentGroups.FindStudentGroup(nGroupName).StudentGroupId;
                                var nStudentIds = repo.StudentsInGroups.GetAllStudentsInGroups()
                                    .Where(sig => sig.StudentGroup.StudentGroupId == nGroupId)
                                    .Select(stig => stig.Student.StudentId)
                                    .ToList();
                                nGroupsListIds.Add(group.Key, repo.StudentsInGroups.GetAllStudentsInGroups()
                                    .Where(sig => nStudentIds.Contains(sig.Student.StudentId))
                                    .Select(stig => stig.StudentGroup.StudentGroupId)
                                    .Distinct()
                                    .ToList());

                                plainNGroupIds.Add(groupObject.StudentGroupId,
                                    new Tuple<int, int>(plainGroupId, nGroupId));
                            }
                        }

                        cToken.ThrowIfCancellationRequested();

                        var timeRowIndexList = new List<int>();

                        var timeRowIndex = 2;
                        foreach (var time in timeList.OrderBy(
                            t => int.Parse(t.Split(':')[0]) * 60 + int.Parse(t.Split(':')[1])))
                        {
                            cToken.ThrowIfCancellationRequested();

                            var hour = int.Parse(time.Substring(0, 2));
                            var minute = int.Parse(time.Substring(3, 2));

                            minute += lessonLength;

                            while (minute >= 60)
                            {
                                hour++;
                                minute -= 60;
                            }


                            timeRowIndexList.Add(timeRowIndex);
                            oTable.Cell(timeRowIndex, 1).Range.Text = time + " - " +
                                                                      hour.ToString("D2") + ":" + minute.ToString("D2");
                            oTable.Cell(timeRowIndex, 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;
                            oTable.Cell(timeRowIndex, 1).VerticalAlignment =
                                WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                            var columnGroupIndex = 2;
                            foreach (var group in schedule)
                            {
                                if (group.Value.ContainsKey(time))
                                {
                                    oTable.Cell(timeRowIndex, columnGroupIndex).VerticalAlignment =
                                        WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                                    var timeTable =
                                        oDoc.Tables.Add(oTable.Cell(timeRowIndex, columnGroupIndex).Range, 1, 1);
                                    for (int i = 0; i < group.Value[time].Count - 1; i++)
                                    {
                                        timeTable.Rows.Add();
                                    }
                                    for (int i = 0; i < group.Value[time].Count - 1; i++)
                                    {
                                        timeTable.Cell(i + 1, 1).Borders[WdBorderType.wdBorderBottom].Visible = true;
                                    }
                                    timeTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                                    timeTable.Range.ParagraphFormat.Alignment =
                                        WdParagraphAlignment.wdAlignParagraphLeft;
                                    timeTable.Range.Font.Size = 10;
                                    timeTable.Range.Font.Bold = 0;

                                    var tfdIndex = 0;
                                    foreach (var tfdData in group.Value[time].OrderBy(tfd => tfd.Value.Item2
                                        .Select(l => repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date))
                                        .Min()))
                                    {
                                        var cellText = "";
                                        cellText += tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.Name;
                                        var groupId = tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline
                                            .StudentGroup.StudentGroupId;
                                        if (plainGroupsListIds.ContainsKey(group.Key))
                                        {
                                            if (plainGroupsListIds[group.Key].Contains(groupId) &&
                                                nGroupsListIds[group.Key].Contains(groupId))
                                            {
                                                cellText += " (+Н)";
                                            }
                                            if (!plainGroupsListIds[group.Key].Contains(groupId) &&
                                                nGroupsListIds[group.Key].Contains(groupId))
                                            {
                                                cellText += " (Н)";
                                            }
                                        }

                                        var tfdGroupId = tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline
                                            .StudentGroup.StudentGroupId;
                                        if ((tfdGroupId != group.Key))
                                        {
                                            if ((!plainNGroupIds.ContainsKey(group.Key)) ||
                                                ((tfdGroupId != plainNGroupIds[group.Key].Item1) &&
                                                 (tfdGroupId != plainNGroupIds[group.Key].Item2)))
                                            {
                                                cellText += " (" + tfdData.Value.Item2[0].Item1.TeacherForDiscipline
                                                                .Discipline.StudentGroup.Name + ")";
                                            }
                                        }
                                        cellText += Environment.NewLine;
                                        cellText += tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Teacher.FIO +
                                                    Environment.NewLine;

                                        // Weeks
                                        if (!tfdData.Value.Item3.Contains("*"))
                                        {
                                            cellText += "(" + tfdData.Value.Item3 + ")" + Environment.NewLine;
                                        }
                                        else
                                        {
                                            cellText += "(" + tfdData.Value.Item1 + ")" + Environment.NewLine;
                                        }

                                        var audWeekList =
                                            tfdData.Value.Item2.ToDictionary(
                                                l => repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date),
                                                l => l.Item1.Auditorium.Name);
                                        var grouped = audWeekList.GroupBy(a => a.Value);

                                        var enumerable =
                                            grouped as List<IGrouping<string, KeyValuePair<int, string>>> ??
                                            grouped.ToList();
                                        var gcount = enumerable.Count();
                                        if (gcount == 1)
                                        {
                                            cellText += enumerable.ElementAt(0).Key;
                                        }
                                        else
                                        {
                                            for (int j = 0; j < gcount; j++)
                                            {
                                                var jItem = enumerable
                                                    .OrderBy(e => e.Select(ag => ag.Key).ToList().Min()).ElementAt(j);
                                                cellText +=
                                                    CommonFunctions.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) +
                                                    " - " + jItem.Key;

                                                if (j != gcount - 1)
                                                {
                                                    cellText += Environment.NewLine;
                                                }
                                            }
                                        }

                                        timeTable.Cell(tfdIndex + 1, 1).Range.Text = cellText;
                                        timeTable.Cell(tfdIndex + 1, 1).VerticalAlignment =
                                            WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                                        tfdIndex++;

                                    }
                                }

                                columnGroupIndex++;
                            }

                            timeRowIndex++;
                        }

                        if (dayOfWeek == daysOfWeek)
                        {
                            var oPara3 = oDoc.Content.Paragraphs.Add(ref oMissing);
                            oPara3.Range.Font.Size = 12;
                            oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                            oPara3.Range.Text = "";
                            oPara3.Format.SpaceAfter = 0;
                            oPara3.Range.InsertParagraphAfter();

                            var headUchOtdNameOption =
                                repo.ConfigOptions.GetFirstFiltredConfigOption(
                                    co => co.Key == "Начальник учебного отдела");
                            var headUchOtdName = (headUchOtdNameOption == null) ? "" : headUchOtdNameOption.Value;


                            Range wordRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                            Table signTable = oDoc.Tables.Add(wordRng, 2, 2);
                            signTable.Borders.Enable = 0;
                            signTable.Range.Bold = 0;

                            signTable.Cell(1, 1).Range.Text = "Начальник учебного отдела";
                            signTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphLeft;

                            signTable.Cell(1, 2).Range.Text = "_________________  " + headUchOtdName;
                            signTable.Cell(1, 2).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphRight;

                            signTable.Rows[1].Height = oWord.CentimetersToPoints(1);

                            signTable.Cell(2, 1).Range.Text = faculty.ScheduleSigningTitle;
                            signTable.Cell(2, 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphLeft;

                            signTable.Cell(2, 2).Range.Text = "_________________  " + faculty.DeanSigningSchedule;
                            signTable.Cell(2, 2).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphRight;

                            /*
                            oPara3 = oDoc.Content.Paragraphs.Add(ref oMissing);
                            oPara3.Range.Text = "Начальник учебного отдела\t\t" + "_________________  " + headUchOtdName;
                            oPara3.Range.Font.Size = 12;
                            oPara3.Range.Font.Bold = 0;
                            oPara3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                            oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                            oPara3.Format.SpaceAfter = 0;
                            oPara3.Range.InsertParagraphAfter();
                            oPara3.Range.InsertParagraphAfter();
    
                            oPara3 = oDoc.Content.Paragraphs.Add(ref oMissing);
                            //"Декан " + UchOtd.NUDS.Core.Constants.facultyTitles[facCounter] + "\t\t_________________  "
                            //+ UchOtd.NUDS.Core.Constants.HeadsOfFaculties.ElementAt(facCounter).Value;
                            oPara3.Range.Text = faculty.ScheduleSigningTitle + "\t\t_________________  " + faculty.DeanSigningSchedule;
                            oPara3.Range.Font.Size = 12;
                            oPara3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                            oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                            oPara3.Range.Font.Bold = 0;
                            oPara3.Format.SpaceAfter = 0;
                            oPara3.Range.InsertParagraphAfter();
                            oPara3.Range.InsertParagraphAfter();
                            */


                        }

                        cToken.ThrowIfCancellationRequested();

                    }

                    pageCounter++;
                    int pageCount;
                    float fontSize = 10.5F;
                    do
                    {
                        fontSize -= 0.5F;
                        oTable.Range.Font.Size = fontSize;
                        if (fList.Contains(faculty.Name))
                        {
                            oTable2.Range.Font.Size = fontSize;
                        }

                            if (fontSize <= 3)
                        {
                            break;
                        }

                        pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
                    } while (pageCount > pageCounter);

                    var endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    endOfDoc.Font.Size = 1;
                    endOfDoc.InsertBreak(WdBreakType.wdSectionBreakNextPage);
                }
            }

            oDoc.Undo();

            if (save)
            {
                object fileName = filename;
                oDoc.SaveAs(ref fileName);
            }

            if (quit)
            {
                oWord.Quit();
            }

            Marshal.ReleaseComObject(oWord);
        }

        private static void DetectSessionDates(ScheduleRepository repo, out DateTime beginSessionDate, out DateTime endSessionDate)
        {
            var minConsDate = repo.Exams.GetAllExams().Select(e => e.ConsultationDateTime).Min();
            var minExamDate = repo.Exams.GetAllExams().Select(e => e.ExamDateTime).Min();

            beginSessionDate = (minConsDate <= minExamDate) ? minConsDate : minExamDate;

            var maxConsDate = repo.Exams.GetAllExams().Select(e => e.ConsultationDateTime).Max();
            var maxExamDate = repo.Exams.GetAllExams().Select(e => e.ExamDateTime).Max();

            endSessionDate = (maxConsDate <= maxExamDate) ? maxConsDate : maxExamDate;
        }

        public static void ExportCustomSessionSchedule(ScheduleRepository repo, List<int> facultyFilter,
            string filename, bool save, bool quit, bool appVisible, List<string> groupsRestriction)
        {
            DateTime beginSessionDate, endSessionDate;
            DetectSessionDates(repo, out beginSessionDate, out endSessionDate);

            object oMissing = Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.

            _Application oWord = new Application();
            if (appVisible)
            {
                oWord.Visible = true;
            }
            
            _Document oDoc =
                oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            List<Faculty> faculties;

            if (facultyFilter != null)
            {
                faculties = new List<Faculty>();

                for (int i = 0; i < facultyFilter.Count; i++)
                {
                    var faculty = repo.Faculties.GetFaculty(facultyFilter[i]);

                    if (faculty != null)
                    {
                        faculties.Add(faculty);
                    }
                }

            }
            else
            {
                faculties = repo.Faculties.GetAllFaculties().OrderBy(f => f.SortingOrder).ToList();
            }

            foreach (var faculty in faculties)
            {
                var localFaculty = faculty;
                var groupList = repo
                    .GroupsInFaculties
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == localFaculty.FacultyId)
                    .Select(gif => gif.StudentGroup)
                    .ToList();

                if (groupsRestriction != null)
                {
                    groupList = groupList.Where(sg => groupsRestriction.Contains(sg.Name)).ToList();
                }

                var groupIds = groupList.Select(sg => sg.StudentGroupId).ToList();

                var facultyExams = repo.Exams.GetFacultyExams(repo, groupIds);

                facultyExams = facultyExams
                    .OrderBy(fe => fe.Key)
                    .ToDictionary(keyItem => keyItem.Key, valueItem => valueItem.Value);

                Paragraph oPara1 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara1.Range.Font.Size = 24;
                oPara1.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara1.Range.Text = "Расписание";
                oPara1.Format.SpaceAfter = 0;
                oPara1.Range.InsertParagraphAfter();

                oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara1.Range.Font.Size = 14;
                oPara1.Format.SpaceAfter = 0;
                if (new List<int> {9, 10, 11, 12, 1}.Contains(beginSessionDate.Month))
                {
                    int startYear = (beginSessionDate.Month > 1) ? beginSessionDate.Year : beginSessionDate.Year - 1;
                    
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

                Shape signBox = oDoc.Shapes
                    .AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 350, 15, 200, 75, oPara1.Range);

                signBox.Line.Visible = MsoTriState.msoFalse;
                signBox.TextFrame.ContainingRange.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphRight;

                var prorUchRabNameOption = repo.ConfigOptions.GetFirstFiltredConfigOption(co => co.Key == "Проректор по учебной работе");
                var prorUchRabName = (prorUchRabNameOption == null) ? "" : prorUchRabNameOption.Value;

                signBox.TextFrame.ContainingRange.InsertAfter("«УТВЕРЖДАЮ»");
                signBox.TextFrame.ContainingRange.InsertParagraphAfter();
                signBox.TextFrame.ContainingRange.InsertAfter("Проректор по учебной работе");
                signBox.TextFrame.ContainingRange.InsertParagraphAfter();
                signBox.TextFrame.ContainingRange.InsertAfter("____________  " + prorUchRabName);

                Faculty local2Faculty = faculty;
                List<StudentGroup> groups = groupList;

                Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                Table oTable = oDoc.Tables.Add(wrdRng, 1 + facultyExams.Keys.Count, 1 + groups.Count());

                //oTable.Rows(1).HeadingFormat = True;
                //oTable.ApplyStyleHeadingRows = True;
                oTable.Rows[1].HeadingFormat = -1;
                oTable.ApplyStyleHeadingRows = true;

                oTable.Borders.Enable = 1;

                for (int i = 1; i <= oTable.Rows.Count; i++)
                {
                    oTable.Rows[i].AllowBreakAcrossPages = (int)MsoTriState.msoFalse;
                }


                oTable.Cell(1, 1).Range.Text = "Дата";
                oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                for (var column = 1; column <= groups.Count(); column++)
                {
                    oTable.Cell(1, column + 1).Range.Text = groups[column - 1].Name;
                    oTable.Cell(1, column + 1).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;
                }

                for (var row = 2; row <= 1 + facultyExams.Keys.Count; row++)
                {
                    oTable.Cell(row, 1).Range.Text = facultyExams.Keys.ElementAt(row - 2).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU"));
                    oTable.Cell(row, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    oTable.Cell(row, 1).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;
                }

                for (var row = 2; row <= 1 + facultyExams.Keys.Count; row++)
                {
                    DateTime currentDate = facultyExams.Keys.ElementAt(row - 2);

                    for (var column = 1; column <= groups.Count; column++)
                    {
                        if (facultyExams.ContainsKey(currentDate))
                        {
                            if (facultyExams[currentDate].ContainsKey(groupIds[column - 1]))
                            {
                                var eventCount = facultyExams[currentDate][groupIds[column - 1]].Count;

                                oTable.Cell(row, column + 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                                var timeTable = oDoc.Tables.Add(oTable.Cell(row, column + 1).Range, 1, 1);
                                timeTable.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitWindow);
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
                                    cellText += evt.TeacherFio + Environment.NewLine;
                                    cellText += evt.Time.ToString("H:mm") + Environment.NewLine;
                                    cellText += evt.Auditorium;

                                    oPara1 = oDoc.Content.Paragraphs.Add(timeTable.Cell(i + 1, 1).Range);
                                    oPara1.Range.Font.Size = 10;
                                    oPara1.Format.SpaceAfter = 0;
                                    oPara1.Range.Text = cellText;

                                    if (i != eventCount - 1)
                                    {
                                        timeTable.Cell(i + 1, 1).Borders[WdBorderType.wdBorderBottom].Visible = true;
                                    }
                                }
                            }
                        }
                    }
                }


                oTable.Columns[1].Width = oWord.CentimetersToPoints(3);
                for (int i = 0; i < groups.Count; i++)
                {
                    oTable.Columns[i + 2].Width = oWord.CentimetersToPoints(16f / groups.Count);
                }

                oTable.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;

                Paragraph oPara2 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara2.Range.Font.Size = 12;
                oPara2.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara2.Range.Text = "";
                oPara2.Format.SpaceAfter = 0;
                oPara2.Range.InsertParagraphAfter();


                var headUchOtdNameOption = repo
                    .ConfigOptions
                    .GetFirstFiltredConfigOption(co => co.Key == "Начальник учебного отдела");
                var headUchOtdName = (headUchOtdNameOption == null) ? "" : headUchOtdNameOption.Value;

                Range wordRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                Table signTable = oDoc.Tables.Add(wordRng, 2, 2);
                signTable.Borders.Enable = 0;
                signTable.Range.Bold = 0;

                signTable.Cell(1, 1).Range.Text = "Начальник учебного отдела";
                signTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                signTable.Cell(1, 2).Range.Text = "_________________  " + headUchOtdName;
                signTable.Cell(1, 2).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphRight;

                signTable.Rows[1].Height = oWord.CentimetersToPoints(1);

                signTable.Cell(2, 1).Range.Text = faculty.ScheduleSigningTitle;
                signTable.Cell(2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                signTable.Cell(2, 2).Range.Text = "_________________  " + faculty.DeanSigningSchedule;
                signTable.Cell(2, 2).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphRight;


                if (faculty.FacultyId != faculties.OrderBy(f => f.SortingOrder).Last().FacultyId)
                {
                    oDoc.Words.Last.InsertBreak(WdBreakType.wdPageBreak);
                }
            }


            if (save)
            {
                object fileName = filename;
                oDoc.SaveAs(ref fileName);
            }

            if (quit)
            {
                oWord.Quit();
            }

            Marshal.ReleaseComObject(oWord);
        }

        private static string DetectSemesterString(ScheduleRepository repo)
        {
            var semesterSterts = repo.CommonFunctions.GetSemesterStarts();
            var ssYear = semesterSterts.Year;

            if (semesterSterts.Month > 6)
            {
                return "первого семестра " + ssYear + " – " + (ssYear + 1) + " учебного года";
            }

            return "второго семестра " + (ssYear - 1) + " – " + ssYear + " учебного года";
        }

        internal static void ExportTwoDaysInPageFacultySchedule(
            ScheduleRepository repo, string filename, bool save, bool quit,
            int lessonLength, int facultyId, int daysOfWeek,
            bool weekFiltered, List<int> weekFilterList, bool weeksMarksVisible)
        {
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            int currentPageNum = 1;

            var faculty = repo.Faculties.GetFaculty(facultyId);

            for (int dayOfWeek = 1; dayOfWeek <= 5; dayOfWeek += 2)
            {
                var firstDaySchedule = repo.Lessons.GetFacultyDowSchedule(faculty.FacultyId, dayOfWeek, weekFiltered, weekFilterList, false, false, null);

                var firstDayTable = PutDayScheduleInWord(repo, lessonLength, weeksMarksVisible, firstDaySchedule, oDoc, oEndOfDoc, oWord, null, dayOfWeek);


                var secondDaySchedule = repo.Lessons.GetFacultyDowSchedule(faculty.FacultyId, dayOfWeek + 1, weekFiltered, weekFilterList, false, false, null);
                var secondDayTable = PutDayScheduleInWord(repo, lessonLength, weeksMarksVisible, secondDaySchedule, oDoc, oEndOfDoc, oWord, firstDayTable, dayOfWeek + 1);

                var fontSize = 10.5F;
                int pageCount;
                do
                {
                    fontSize -= 0.5F;
                    firstDayTable.Range.Font.Size = fontSize;
                    if (secondDayTable != null)
                    {
                        secondDayTable.Range.Font.Size = fontSize;
                    }

                    if (fontSize <= 3)
                    {
                        break;
                    }

                    pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
                } while (pageCount > currentPageNum);

                var endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                endOfDoc.Font.Size = 1;
                endOfDoc.InsertBreak(WdBreakType.wdSectionBreakNextPage);

                currentPageNum++;
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

            Marshal.ReleaseComObject(oWord);
        }

        public static void WordSchool(ScheduleRepository repo, string filename, bool save, bool quit, 
            int lessonLength, int facultyId, int dayOfWeek, int daysOfWeek, bool weekFiltered, List<int> weekFilterList, 
            bool weeksMarksVisible, CancellationToken cToken)
        {
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            cToken.ThrowIfCancellationRequested();

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            var faculty = repo.Faculties.GetFaculty(facultyId);

            Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = "Расписание";
            oPara1.Range.Font.Bold = 0;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.SpaceAfter = 0;
            oPara1.Range.InsertParagraphAfter();

            var textBoxRange = oPara1.Range;

            oPara1 = oDoc.Content.Paragraphs.Add();
            //oPara1.Range.Text = "второго семестра 2013 – 2014 учебного года";
            oPara1.Range.Text = DetectSemesterString(repo);
            oPara1.Range.Font.Bold = 0;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Range.InsertParagraphAfter();

            oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = faculty.Name;
            oPara1.Range.Font.Bold = 0;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Range.InsertParagraphAfter();

            /*
            oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Font.Size = 14;
            oPara1.Range.Text = Constants.DowLocal[dayOfWeek];
            oPara1.Range.Font.Bold = 1;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Range.InsertParagraphAfter();
            */

            Shape cornerStamp = oDoc.Shapes.AddTextbox(
                MsoTextOrientation.msoTextOrientationHorizontal,
                oWord.CentimetersToPoints(22f),
                oWord.CentimetersToPoints(0.5f),
                200, 50,
                textBoxRange);
            cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;

            if (dayOfWeek == 1)
            {
                cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                            Environment.NewLine + "Ректор   " + "______________     Наянова М.В." +
                            Environment.NewLine + "«___» ____________  20__ г.";
                cornerStamp.TextFrame.TextRange.Font.Size = 10;
                cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            }
            cornerStamp.TextFrame.WordWrap = 1;
            cornerStamp.TextFrame.TextRange.ParagraphFormat.SpaceAfter = 0;
            cornerStamp.Line.Visible = MsoTriState.msoFalse;

            cToken.ThrowIfCancellationRequested();

            Table oTable = GetAndPutDowSchedule(repo, lessonLength, dayOfWeek, weekFiltered, weekFilterList, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, null, cToken);

            Table oTable2 = null;

            if ((dayOfWeek != 6) && (dayOfWeek != 7))
            {
                oTable2 = GetAndPutDowSchedule(repo, lessonLength, dayOfWeek + 1, weekFiltered, weekFilterList, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, oTable, cToken);
            }

            cToken.ThrowIfCancellationRequested();

            int pageCount;
            var fontSize = 10.5F;
            do
            {
                fontSize -= 0.5F;
                oTable.Range.Font.Size = fontSize;

                if (fontSize <= 3)
                {
                    break;
                }

                pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
            } while (pageCount > 1);

            if (save)
            {
                object fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + filename;
                oDoc.SaveAs(ref fileName);
            }

            if (quit)
            {
                oWord.Quit();
            }

            Marshal.ReleaseComObject(oWord);
        }

        public static void TeacherSchedule(List<TeacherScheduleTimeView> result, Teacher teacher, bool landscape, CancellationToken cToken)
        {
            var isColumnEmpty = GetEmptyColumnIndexes(result);
            var columnTitles = new List<string>();
            var columnIndexes = new List<int>();
            for (int i = 1; i <= 7; i++)
            {
                if (!isColumnEmpty[i])
                {
                    columnTitles.Add(Constants.DowLocal[i]);
                    columnIndexes.Add(i);
                }
            }

            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = landscape ? WdOrientation.wdOrientLandscape : WdOrientation.wdOrientPortrait;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = "Расписание СГОАН (" + teacher.FIO + ")";
            oPara1.Range.Font.Bold = 0;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.SpaceAfter = 0;
            oPara1.Range.InsertParagraphAfter();

            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            var dowCount = isColumnEmpty.Count(dow => !dow.Value);

            Table oTable = oDoc.Tables.Add(wrdRng, 1 + result.Count, 1 + dowCount);
            oTable.Borders.Enable = 1;
            oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
            oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            oTable.Range.Font.Size = 10;
            oTable.Range.Font.Bold = 0;

            oTable.Cell(1, 1).Range.Text = "Время";
            oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;

            oTable.Columns[1].Width = oWord.CentimetersToPoints(2.44f);
            float colWidth = (landscape ? 25.64F : 16.3F) / dowCount;
            for (int i = 0; i < dowCount; i++)
            {
                oTable.Columns[i + 2].Width = oWord.CentimetersToPoints(colWidth);
                oTable.Cell(1, i + 2).Range.Text = columnTitles[i];
                oTable.Cell(1, i + 2).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;
            }

            for (int i = 0; i < result.Count; i++)
            {
                oTable.Cell(2 + i, 1).Range.Text = result[i].Time;
                oTable.Cell(2 + i, 1).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(2 + i, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                for (int j = 0; j < dowCount; j++)
                {
                    var dowIndex = columnIndexes[j];
                    switch (dowIndex)
                    {
                        case 1:
                            oTable.Cell(2 + i, 2 + j).Range.Text = result[i].MonLessons;
                            break;
                        case 2:
                            oTable.Cell(2 + i, 2 + j).Range.Text = result[i].TueLessons;
                            break;
                        case 3:
                            oTable.Cell(2 + i, 2 + j).Range.Text = result[i].WedLessons;
                            break;
                        case 4:
                            oTable.Cell(2 + i, 2 + j).Range.Text = result[i].ThuLessons;
                            break;
                        case 5:
                            oTable.Cell(2 + i, 2 + j).Range.Text = result[i].FriLessons;
                            break;
                        case 6:
                            oTable.Cell(2 + i, 2 + j).Range.Text = result[i].SatLessons;
                            break;
                        case 7:
                            oTable.Cell(2 + i, 2 + j).Range.Text = result[i].SunLessons;
                            break;
                    }

                    oTable.Cell(2 + i, 2 + j).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                }
            }


            int pageCount;
            var fontSize = 10.5F;
            do
            {
                fontSize -= 0.5F;
                oTable.Range.Font.Size = fontSize;

                if (fontSize <= 3)
                {
                    break;
                }

                pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
            } while (pageCount > 1);

            Marshal.ReleaseComObject(oWord);
        }

        private static Dictionary<int, bool> GetEmptyColumnIndexes(List<TeacherScheduleTimeView> result)
        {
            var emptyColumn = new Dictionary<int, bool>();
            for (int i = 1; i <= 7; i++)
            {
                emptyColumn.Add(i, true);
                foreach (TeacherScheduleTimeView time in result)
                {
                    switch (i)
                    {
                        case 1:
                            if (time.MonLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 2:
                            if (time.TueLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 3:
                            if (time.WedLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 4:
                            if (time.ThuLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 5:
                            if (time.FriLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 6:
                            if (time.SatLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                        case 7:
                            if (time.SunLessons != null)
                            {
                                emptyColumn[i] = false;
                            }
                            break;
                    }
                }
            }

            return emptyColumn;
        }

        public static void AuditoriumsExport(ScheduleRepository repo,
            Dictionary<int, Dictionary<int, List<string>>> auds,
            int dow, bool addTeacherFio, CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            /*
            Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = Constants.DowLocal[dow];
            oPara1.Range.Font.Bold = 0;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.SpaceAfter = 0;
            oPara1.Range.InsertParagraphAfter();
            */

            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

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

            var rings = repo.Rings.GetAllRings();
            var audsById = repo.Auditoriums.GetAll().ToDictionary(a => a.AuditoriumId, a => a.Name);

            audIdsList = audIdsList.OrderBy(id => audsById[id]).ToList();

            cToken.ThrowIfCancellationRequested();

            Table oTable = oDoc.Tables.Add(wrdRng, 1 + auds.Count, 1 + audIdsList.Count);
            oTable.Borders.Enable = 1;
            oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
            oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            oTable.Range.Font.Size = 10;
            oTable.Range.Font.Bold = 0;

            oTable.Cell(1, 1).Range.Text = Constants.DowLocal[dow];
            oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;

            oTable.Columns[1].Width = oWord.CentimetersToPoints(2.44f);
            float colWidth = 25.64F / audIdsList.Count;
            for (int i = 0; i < audIdsList.Count; i++)
            {
                oTable.Columns[i + 2].Width = oWord.CentimetersToPoints(colWidth);
                oTable.Cell(1, i + 2).Range.Text = audsById[audIdsList[i]];
                oTable.Cell(1, i + 2).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(1, i + 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            }

            for (int i = 0; i < auds.Count; i++)
            {
                cToken.ThrowIfCancellationRequested();

                var ct = rings.First(r => r.RingId == auds.Keys.ElementAt(i)).Time.ToString("H:mm");

                oTable.Cell(2 + i, 1).Range.Text = ct;
                oTable.Cell(2 + i, 1).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(2 + i, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                for (int j = 0; j < audIdsList.Count; j++)
                {
                    var audText = "";
                    if (auds[auds.Keys.ElementAt(i)].ContainsKey(audIdsList[j]))
                    {
                        var cnt = auds[auds.Keys.ElementAt(i)][audIdsList[j]].Count;
                        int ii = 0;
                        foreach (var kvp in auds[auds.Keys.ElementAt(i)][audIdsList[j]])
                        {
                            if (kvp.Contains('@'))
                            {
                                audText += kvp.Split('@')[0];
                                if (addTeacherFio)
                                {
                                    audText += " " + kvp.Substring(kvp.Split('@')[0].Length + 1);
                                }

                                if (ii != cnt - 1)
                                {
                                    audText += Environment.NewLine;
                                }
                            }
                            else
                            {
                                audText += kvp;
                                if (ii != cnt - 1)
                                {
                                    audText += Environment.NewLine;
                                }
                            }

                            ii++;
                        }

                        oTable.Cell(2 + i, 2 + j).Range.Text = audText;
                        oTable.Cell(2 + i, 2 + j).Range.ParagraphFormat.Alignment =
                                    WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(2 + i, 2 + j).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    }
                }
            }

            cToken.ThrowIfCancellationRequested();

            int pageCount;
            var fontSize = 10.5F;
            do
            {
                fontSize -= 0.5F;
                oTable.Range.Font.Size = fontSize;

                if (fontSize <= 3)
                {
                    break;
                }

                pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
            } while (pageCount > 1);

            Marshal.ReleaseComObject(oWord);
        }

        public static void WordSchoolTwoDays(ScheduleRepository repo, string filename, bool save, bool quit, 
            int lessonLength, int facultyId, int dayOfWeek, int daysOfWeek, bool weekFiltered, List<int> weekFilterList, 
            bool weeksMarksVisible, CancellationToken cToken)
        {
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            var faculty = repo.Faculties.GetFaculty(facultyId);

            var oTable = GetAndPutDowSchedule(repo, lessonLength, dayOfWeek, weekFiltered, weekFilterList, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, null, cToken);
            if (dayOfWeek != 7)
            {
                oTable = GetAndPutDowSchedule(repo, lessonLength, dayOfWeek + 1, weekFiltered, weekFilterList, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, oTable, cToken);
            }


            int pageCount;
            var fontSize = 10.5F;
            do
            {
                fontSize -= 0.5F;
                oTable.Range.Font.Size = fontSize;

                if (fontSize <= 3)
                {
                    break;
                }

                pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
            } while (pageCount > 1);

            if (save)
            {
                object fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + filename;
                oDoc.SaveAs(ref fileName);
            }

            if (quit)
            {
                oWord.Quit();
            }

            Marshal.ReleaseComObject(oWord);
        }

        private static Table GetAndPutDowSchedule(ScheduleRepository repo, int lessonLength, int dayOfWeek, bool weekFiltered, 
            List<int> weekFilterList, bool weeksMarksVisible, Faculty faculty, _Document oDoc, object oEndOfDoc, _Application oWord,
            Table tableToContinue, CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            var schedule = repo.Lessons.GetFacultyDowSchedule(faculty.FacultyId, dayOfWeek, weekFiltered, weekFilterList, false, false, null);

            cToken.ThrowIfCancellationRequested();

            var timeList = new List<string>();
            foreach (var group in schedule)
            {
                foreach (var time in @group.Value.Keys)
                {
                    if (!timeList.Contains(time))
                    {
                        timeList.Add(time);
                    }
                }
            }

            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            Table oTable;
            var tableRowOffset = 0;

            if (tableToContinue == null)
            {
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
            }
            else
            {
                oTable = tableToContinue;
                tableRowOffset = oTable.Rows.Count;

                for (int i = 0; i < 1 + timeList.Count; i++)
                {
                    oTable.Rows.Add();

                    for (int j = 1; j <= 1 + schedule.Count; j++)
                    {
                        oTable.Cell(tableRowOffset + i + 1, j).Borders[WdBorderType.wdBorderDiagonalUp].Visible = false;
                    }
                }
            }



            oTable.Cell(tableRowOffset + 1, 1).Range.Text = Constants.DowLocal[dayOfWeek];
            oTable.Cell(tableRowOffset + 1, 1).Range.Bold = 1;
            oTable.Cell(tableRowOffset + 1, 1).Range.ParagraphFormat.Alignment =
                WdParagraphAlignment.wdAlignParagraphCenter;

            int groupColumn = 2;


            foreach (var group in schedule)
            {
                var groupObject = repo.StudentGroups.GetStudentGroup(@group.Key);
                var groupName = groupObject.Name;
                oTable.Cell(tableRowOffset + 1, groupColumn).Range.Text = groupName;
                oTable.Cell(tableRowOffset + 1, groupColumn).Range.Bold = 1;
                oTable.Cell(tableRowOffset + 1, groupColumn).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                groupColumn++;
            }

            var timeRowIndexList = new List<int>();

            var timeRowIndex = 2;
            foreach (var time in timeList.OrderBy(t => int.Parse(t.Split(':')[0]) * 60 + int.Parse(t.Split(':')[1])))
            {

                cToken.ThrowIfCancellationRequested();

                var hour = int.Parse(time.Substring(0, 2));
                var minute = int.Parse(time.Substring(3, 2));

                minute += lessonLength;

                while (minute >= 60)
                {
                    hour++;
                    minute -= 60;
                }


                timeRowIndexList.Add(timeRowIndex);
                oTable.Cell(tableRowOffset + timeRowIndex, 1).Range.Text = time + " - " +
                                                          hour.ToString("D2") + ":" + minute.ToString("D2");
                oTable.Cell(tableRowOffset + timeRowIndex, 1).Range.Bold = 1;
                oTable.Cell(tableRowOffset + timeRowIndex, 1).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(tableRowOffset + timeRowIndex, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                var columnGroupIndex = 2;
                foreach (var group in schedule)
                {
                    if (@group.Value.ContainsKey(time))
                    {
                        oTable.Cell(tableRowOffset + timeRowIndex, columnGroupIndex).VerticalAlignment =
                            WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                        var groupDowTimeLessons = @group.Value[time]
                            .OrderBy(tfd => tfd.Value.Item2.Select(l =>
                                repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date)).Min())
                            .ToList();

                        var groupObject = repo.StudentGroups.GetStudentGroup(@group.Key);
                        var subgroupIds = new List<int>();
                        var subGroupOne = repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.Name == groupObject.Name + "1");
                        if (subGroupOne != null) subgroupIds.Add(subGroupOne.StudentGroupId);
                        var subGroupTwo = repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.Name == groupObject.Name + "2");
                        if (subGroupTwo != null) subgroupIds.Add(subGroupTwo.StudentGroupId);

                        var subgroups = false;

                        var groupIds =
                                groupDowTimeLessons.Select(
                                    l =>
                                        l.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup
                                            .StudentGroupId).ToList();
                        if (groupIds.Intersect(subgroupIds).Count() > 0)
                        {
                            subgroups = true;
                        }


                        Table subgroupsTable = null;
                        Table timeTable = null;

                        List<KeyValuePair<int, Tuple<string, List<Tuple<Lesson, int>>, string>>> group1Items = null;
                        if (subGroupOne != null)
                        {
                            group1Items =
                                groupDowTimeLessons.Where(
                                    l =>
                                        l.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup
                                            .StudentGroupId == subGroupOne.StudentGroupId).ToList();
                        }

                        List<KeyValuePair<int, Tuple<string, List<Tuple<Lesson, int>>, string>>> group2Items = null;
                        if (subGroupTwo != null)
                        {
                            group2Items =
                                groupDowTimeLessons.Where(
                                    l =>
                                        l.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup
                                            .StudentGroupId == subGroupTwo.StudentGroupId).ToList();
                        }


                        if (subgroups)
                        {
                            subgroupsTable =
                                oDoc.Tables.Add(oTable.Cell(tableRowOffset + timeRowIndex, columnGroupIndex).Range, 1, 2);
                            subgroupsTable.Cell(1, 1).VerticalAlignment =
                                WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            subgroupsTable.Cell(1, 2).VerticalAlignment =
                                WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            subgroupsTable.Cell(1, 1).Borders[WdBorderType.wdBorderRight].Visible = true;

                            PutDowSchedulePutGroupOrSubGroupDowTimeItem(repo, oDoc, subgroupsTable.Cell(1, 1), group1Items, true);

                            PutDowSchedulePutGroupOrSubGroupDowTimeItem(repo, oDoc, subgroupsTable.Cell(1, 2), group2Items, true);
                        }
                        else
                        {
                            PutDowSchedulePutGroupOrSubGroupDowTimeItem(repo, oDoc, oTable.Cell(tableRowOffset + timeRowIndex, columnGroupIndex), groupDowTimeLessons, false);
                        }
                    }

                    columnGroupIndex++;
                }

                timeRowIndex++;
            }

            return oTable;
        }

        private static void PutDowSchedulePutGroupOrSubGroupDowTimeItem(ScheduleRepository repo, _Document oDoc, Cell tableCell, List<KeyValuePair<int, Tuple<string, List<Tuple<Lesson, int>>, string>>> groupDowTimeLessons, bool subgroups)
        {
            Table timeTable = oDoc.Tables.Add(tableCell.Range, 1, 1);
            for (int i = 0; i < groupDowTimeLessons.Count - 1; i++)
            {
                timeTable.Rows.Add();
            }


            if (!((groupDowTimeLessons.Count == 2) &&
                  ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) &&
                   (groupDowTimeLessons[1].Value.Item1.Contains("чёт.")))))
            {
                for (int i = 0; i < groupDowTimeLessons.Count - 1; i++)
                {
                    timeTable.Cell(i + 1, 1).Borders[WdBorderType.wdBorderBottom].Visible = true;
                }
            }

            timeTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
            timeTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            timeTable.Range.Font.Size = 10;
            timeTable.Range.Font.Bold = 0;
            
            if (groupDowTimeLessons.Count == 2)
            {
                if (((groupDowTimeLessons[0].Value != null) && (groupDowTimeLessons[1].Value != null)) &&
                    (groupDowTimeLessons[0].Value.Item1.Contains("(чёт.") &&
                     groupDowTimeLessons[1].Value.Item1.Contains("(нечёт.")))
                {
                    var tmp = groupDowTimeLessons[0];
                    groupDowTimeLessons[0] = groupDowTimeLessons[1];
                    groupDowTimeLessons[1] = tmp;
                }
            }

            if (
                ((groupDowTimeLessons.Count == 2) &&
                 ((groupDowTimeLessons[0].Value != null) && (groupDowTimeLessons[1].Value != null)) &&
                 ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) &&
                  (groupDowTimeLessons[1].Value.Item1.Contains("чёт."))))
                || ((groupDowTimeLessons.Count == 1) &&
                    (groupDowTimeLessons[0].Value != null) &&
                    (groupDowTimeLessons[0].Value.Item1.Contains("чёт."))))
            {
                tableCell.Range.Borders[WdBorderType.wdBorderDiagonalUp].LineStyle = WdLineStyle.wdLineStyleSingle;
            }

            for (int dowTimeIndex = 0; dowTimeIndex < groupDowTimeLessons.Count; dowTimeIndex++)
            {
                var tfdData = groupDowTimeLessons[dowTimeIndex];
                var cellText = "";

                if (tfdData.Value == null)
                {
                    dowTimeIndex++;

                    continue;
                }

                // Discipline name
                var discName = tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.Name;

                var shorteningDictionary = new Dictionary<string, string>
                {
                    {"Английский язык", "Англ. яз."},
                    {"Немецкий язык", "Нем. яз."},
                    {"Французский язык", "Франц. яз."},
                    {"Спец.курс немецкий язык и страноведение", "СК Нем. яз."},
                    {"Спец.курс французский язык и страноведение", "СК Фр. яз."},
                };

                if (shorteningDictionary.ContainsKey(discName))
                {
                    discName = shorteningDictionary[discName];
                }

                cellText += discName;
                cellText += Environment.NewLine;

                // Teacher FIO
                string teacherFio = ShortenFio(
                    tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Teacher.FIO,
                    subgroups); 
                cellText += teacherFio + Environment.NewLine;

                // Auditoriums
                var audWeekList =
                    tfdData.Value.Item2.ToDictionary(
                        l => repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date),
                        l => l.Item1.Auditorium.Name);
                var grouped = audWeekList.GroupBy(a => a.Value);

                var enumerable = grouped as List<IGrouping<string, KeyValuePair<int, string>>> ??
                                 grouped.ToList();
                var gcount = enumerable.Count();
                if (gcount == 1)
                {
                    cellText += enumerable.ElementAt(0).Key;
                }
                else
                {
                    for (int j = 0; j < gcount; j++)
                    {
                        var jItem =
                            enumerable.OrderBy(e => e.Select(ag => ag.Key).ToList().Min()).ElementAt(j);
                        cellText += CommonFunctions.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) +
                                    " - " +
                                    jItem.Key;

                        if (j != gcount - 1)
                        {
                            cellText += Environment.NewLine;
                        }
                    }
                }
                // Auditoriums

                // Extra line on diagonal split
                if (groupDowTimeLessons[dowTimeIndex].Value.Item1.Contains("(чёт."))
                {
                    cellText = Environment.NewLine + cellText;
                }

                if (groupDowTimeLessons[dowTimeIndex].Value.Item1.Contains("(нечёт."))
                {
                    cellText = cellText + Environment.NewLine;
                }
                // Extra line on diagonal split

                var rowIndex = 1 + dowTimeIndex;
                var columnIndex = 1;

                timeTable.Cell(rowIndex, columnIndex).Range.Text = cellText;
                timeTable.Cell(rowIndex, columnIndex).VerticalAlignment =
                    WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                if ((groupDowTimeLessons.Count == 2) &&
                    ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) &&
                     (groupDowTimeLessons[1].Value.Item1.Contains("чёт."))))
                {
                    timeTable.Cell(rowIndex, columnIndex).Range.ParagraphFormat.Alignment =
                        (dowTimeIndex == 0)
                            ? WdParagraphAlignment.wdAlignParagraphLeft
                            : WdParagraphAlignment.wdAlignParagraphRight;
                }

                if ((groupDowTimeLessons.Count == 1) &&
                    (groupDowTimeLessons[0].Value.Item1.Contains("(нечёт.")))
                {
                    tableCell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalTop;
                }

                    if ((groupDowTimeLessons.Count == 1) &&
                    (groupDowTimeLessons[0].Value.Item1.Contains("(чёт.")))
                {
                    timeTable.Cell(rowIndex, columnIndex).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphRight;
                    tableCell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalBottom;
                }
            }
        }

        private static Table GetAndPutDowStartSchedule(ScheduleRepository repo, int lessonLength, int dayOfWeek, bool weekFiltered, 
            List<int> weekFilterList, bool weeksMarksVisible, Faculty faculty, _Document oDoc, object oEndOfDoc, _Application oWord, CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            var schedule = repo.Lessons.GetFacultyDowSchedule(faculty.FacultyId, dayOfWeek, weekFiltered, weekFilterList, false, false, null);

            cToken.ThrowIfCancellationRequested();

            var timeList = new List<string>();
            foreach (var group in schedule)
            {
                foreach (var time in @group.Value.Keys)
                {
                    if (!timeList.Contains(time))
                    {
                        timeList.Add(time);
                    }
                }
            }

            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            Table oTable = oDoc.Tables.Add(wrdRng, 1 + timeList.Count, 1 + (schedule.Count * 2));
            oTable.Borders.Enable = 1;
            oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
            oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            oTable.Range.Font.Size = 11;
            oTable.Range.Font.Bold = 0;

            oTable.Columns[1].Width = oWord.CentimetersToPoints(2.44f);
            float colWidth = 25.64F / schedule.Count;
            for (int i = 0; i < schedule.Count * 2; i += 2)
            {
                oTable.Columns[i + 2].Width = oWord.CentimetersToPoints(colWidth - 1.1f);
                oTable.Columns[i + 3].Width = oWord.CentimetersToPoints(1.1f);
            }


            oTable.Range.Font.Underline = WdUnderline.wdUnderlineNone;


            oTable.Cell(1, 1).Range.Text = "Время занятий";//Constants.DOWLocal[dayOfWeek];
            oTable.Cell(1, 1).Range.Font.Bold = 1;
            oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                WdParagraphAlignment.wdAlignParagraphCenter;

            int groupColumn = 2;


            foreach (var group in schedule)
            {
                var groupObject = repo.StudentGroups.GetStudentGroup(@group.Key);
                var groupName = groupObject.Name;
                oTable.Cell(1, groupColumn).Range.Text = groupName;
                oTable.Cell(1, groupColumn).Range.Font.Bold = 1;
                oTable.Cell(1, groupColumn).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(1, groupColumn).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                oTable.Cell(1, groupColumn + 1).Range.Text = "Ауд";
                oTable.Cell(1, groupColumn + 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                groupColumn += 2;
            }

            var timeRowIndexList = new List<int>();

            var timeRowIndex = 2;
            foreach (var time in timeList.OrderBy(t => int.Parse(t.Split(':')[0]) * 60 + int.Parse(t.Split(':')[1])))
            {
                cToken.ThrowIfCancellationRequested();

                var hour = int.Parse(time.Substring(0, 2));
                var minute = int.Parse(time.Substring(3, 2));

                minute += lessonLength;

                while (minute >= 60)
                {
                    hour++;
                    minute -= 60;
                }


                timeRowIndexList.Add(timeRowIndex);
                oTable.Cell(timeRowIndex, 1).Range.Text = time + "-" +
                                                          hour.ToString("D2") + ":" + minute.ToString("D2");
                oTable.Cell(timeRowIndex, 1).Range.Font.Bold = 1;
                oTable.Cell(timeRowIndex, 1).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(timeRowIndex, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                var columnGroupIndex = 2;
                foreach (var group in schedule)
                {
                    if (@group.Value.ContainsKey(time))
                    {
                        oTable.Cell(timeRowIndex, columnGroupIndex).VerticalAlignment =
                            WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                        var groupDowTimeLessons = @group.Value[time]
                            .OrderBy(tfd => tfd.Value.Item2.Select(l =>
                                repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date)).Min())
                            .ToList();
                        var tfdIndex = 0;

                        if (groupDowTimeLessons.Count() == 2)
                        {
                            if (groupDowTimeLessons[0].Value.Item1.Contains("(чёт.") &&
                                groupDowTimeLessons[1].Value.Item1.Contains("(нечёт."))
                            {
                                var tmp = groupDowTimeLessons[0];
                                groupDowTimeLessons[0] = groupDowTimeLessons[1];
                                groupDowTimeLessons[1] = tmp;
                            }
                        }

                        if (
                            ((@group.Value[time].Count == 2) &&
                            ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) && (groupDowTimeLessons[1].Value.Item1.Contains("чёт."))))
                            || ((@group.Value[time].Count == 1) && (groupDowTimeLessons[0].Value.Item1.Contains("чёт."))))
                        {
                            var rng = oTable.Cell(timeRowIndex, columnGroupIndex).Range;
                            rng.Borders[WdBorderType.wdBorderDiagonalUp].LineStyle = WdLineStyle.wdLineStyleSingle;
                        }


                        var groupObject = repo.StudentGroups.GetStudentGroup(@group.Key);
                        var subGroupOne = repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.Name == groupObject.Name + "1");
                        var subGroupTwo = repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.Name == groupObject.Name + "2");

                        if (groupDowTimeLessons.Count() == 2)
                        {
                            if (((subGroupOne != null) && (subGroupTwo != null)) &&
                                ((groupDowTimeLessons[0].Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId == subGroupTwo.StudentGroupId) &&
                                 (groupDowTimeLessons[1].Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId == subGroupOne.StudentGroupId)))
                            {
                                var tmp = groupDowTimeLessons[0];
                                groupDowTimeLessons[0] = groupDowTimeLessons[1];
                                groupDowTimeLessons[1] = tmp;
                            }
                        }

                        var addSubGroupColumn = 0;

                        if ((groupDowTimeLessons.Count() == 1) &&
                            (subGroupOne != null) &&
                            (groupDowTimeLessons[0].Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId == subGroupOne.StudentGroupId))
                        {
                            addSubGroupColumn = 1;

                            var emptytfd = new KeyValuePair<int, Tuple<string, List<Tuple<Lesson, int>>, string>>(-1, null);
                            groupDowTimeLessons.Add(emptytfd);
                        }

                        if ((groupDowTimeLessons.Count() == 1) &&
                            (subGroupTwo != null) &&
                            (groupDowTimeLessons[0].Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId == subGroupTwo.StudentGroupId))
                        {
                            addSubGroupColumn = 1;

                            var emptytfd = new KeyValuePair<int, Tuple<string, List<Tuple<Lesson, int>>, string>>(-1, null);
                            groupDowTimeLessons.Add(emptytfd);

                            var tmp = groupDowTimeLessons[0];
                            groupDowTimeLessons[0] = groupDowTimeLessons[1];
                            groupDowTimeLessons[1] = tmp;
                        }

                        var timeTable = oDoc.Tables.Add(oTable.Cell(timeRowIndex, columnGroupIndex).Range, 1, @group.Value[time].Count + addSubGroupColumn);

                        if (!((groupDowTimeLessons.Count == 2) &&
                            (((groupDowTimeLessons[0].Value != null) && (groupDowTimeLessons[1].Value != null)) &&
                             ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) && (groupDowTimeLessons[1].Value.Item1.Contains("чёт."))))))
                        {
                            for (int i = 0; i < groupDowTimeLessons.Count - 1; i++)
                            {
                                timeTable.Cell(1, i + 1).Borders[WdBorderType.wdBorderRight].Visible = true;
                            }
                        }


                        timeTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                        timeTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        timeTable.Range.Font.Size = 10;
                        timeTable.Range.Font.Bold = 0;


                        foreach (var tfdData in groupDowTimeLessons)
                        {
                            var cellText = "";

                            if (tfdData.Value == null)
                            {
                                tfdIndex++;

                                continue;
                            }

                            var discName = tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.Name;

                            var shorteningDictionary = new Dictionary<string, string>
                            {
                                {"Английский язык", "Англ. яз."},
                                {"Немецкий язык", "Нем. яз."},
                                {"Французский язык", "Франц. яз."},
                                {"Спец.курс немецкий язык и страноведение", "СК Нем. яз."},
                                {"Спец.курс французский язык и страноведение", "СК Фр. яз."},
                            };

                            if (shorteningDictionary.ContainsKey(discName))
                            {
                                discName = shorteningDictionary[discName];
                            }

                            // Discipline name
                            cellText += discName + Environment.NewLine;

                            // Teacher FIO
                            var ommitInitials = @group.Value[time].Count != 1;
                            String teacherFio = ShortenFio(tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Teacher.FIO, ommitInitials);
                            cellText += teacherFio;

                            // Total weeks
                            if (weeksMarksVisible)
                            {
                                /*
                                if (tfdData.Value.Item1.Contains("(чёт."))
                                {
                                    cellText += "(чёт.)" + Environment.NewLine;
                                }
                                if (tfdData.Value.Item1.Contains("(нечёт."))
                                {
                                    cellText += "(нечёт.)" + Environment.NewLine;
                                }
                                */
                                //cellText += "(" + tfdData.Value.Item1 + ")" + Environment.NewLine;
                            }

                            String audText = "";
                            // Auditoriums
                            var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date),
                                l => l.Item1.Auditorium.Name);
                            var grouped = audWeekList.GroupBy(a => a.Value);

                            var enumerable = grouped as List<IGrouping<string, KeyValuePair<int, string>>> ?? grouped.ToList();
                            var gcount = enumerable.Count();
                            if (gcount == 1)
                            {
                                audText += ShortenAudName(enumerable.ElementAt(0).Key);
                            }
                            else
                            {
                                for (int j = 0; j < gcount; j++)
                                {
                                    var jItem = enumerable.OrderBy(e => e.Select(ag => ag.Key).ToList().Min()).ElementAt(j);
                                    audText += CommonFunctions.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " +
                                               ShortenAudName(jItem.Key);

                                    if (j != gcount - 1)
                                    {
                                        audText += Environment.NewLine;
                                    }
                                }
                            }

                            if ((groupDowTimeLessons.Count == 1) &&
                                (groupDowTimeLessons[0].Value.Item1.Contains("(чёт.")))
                            {
                                cellText = Environment.NewLine + cellText;
                            }

                            if ((groupDowTimeLessons.Count == 1) &&
                                (groupDowTimeLessons[0].Value.Item1.Contains("(нечёт.")))
                            {
                                cellText = cellText + Environment.NewLine;
                            }
                            //Auditoriums


                            timeTable.Cell(1, tfdIndex + 1).Range.Text = cellText;
                            timeTable.Cell(1, tfdIndex + 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;

                            /*
                             * FIO in one line
                            var lineSpacing = timeTable.Cell(1, tfdIndex + 1).Range.ParagraphFormat.LineSpacing;

                            var Height = timeTable.Cell(1, tfdIndex + 1).Height;

                            if (Height > lineSpacing * 2)
                            {

                            }
                             */

                            timeTable.Cell(1, tfdIndex + 1).VerticalAlignment =
                                WdCellVerticalAlignment.wdCellAlignVerticalCenter;


                            var audCellText = oTable.Cell(timeRowIndex, columnGroupIndex + 1).Range.Text;
                            if (audCellText == "\r\a")
                            {
                                oTable.Cell(timeRowIndex, columnGroupIndex + 1).Range.Text = audText;
                            }
                            else
                            {
                                oTable.Cell(timeRowIndex, columnGroupIndex + 1).Range.Text = audCellText + "/ " + audText;
                            }

                            oTable.Cell(timeRowIndex, columnGroupIndex + 1).VerticalAlignment =
                                WdCellVerticalAlignment.wdCellAlignVerticalCenter;


                            if ((@group.Value[time].Count == 2) &&
                                ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) && (groupDowTimeLessons[1].Value.Item1.Contains("чёт."))))
                            {
                                timeTable.Cell(1, tfdIndex + 1).Range.ParagraphFormat.Alignment =
                                    (tfdIndex == 0)
                                        ? WdParagraphAlignment.wdAlignParagraphLeft
                                        : WdParagraphAlignment.wdAlignParagraphRight;
                            }

                            if ((groupDowTimeLessons.Count == 1) &&
                                (groupDowTimeLessons[0].Value.Item1.Contains("(чёт.")))
                            {
                                timeTable.Cell(1, tfdIndex + 1).Range.ParagraphFormat.Alignment =
                                    WdParagraphAlignment.wdAlignParagraphRight;
                            }

                            tfdIndex++;
                        }
                    }

                    columnGroupIndex += 2;
                }

                timeRowIndex++;
            }

            return oTable;
        }

        private static Table GetAndPutDowStartSchedule2(ScheduleRepository repo, int dayOfWeek, bool weekFiltered,
            List<int> weekFilterList,
            bool weeksMarksVisible, Faculty faculty, _Document oDoc, object oEndOfDoc, _Application oWord,
            List<string> groupRestrictions, CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            var schedule = repo.Lessons.GetFacultyDowSchedule(faculty.FacultyId, dayOfWeek, weekFiltered, weekFilterList, false, false, groupRestrictions);

            cToken.ThrowIfCancellationRequested();

            var timeList = new List<string>();
            foreach (var group in schedule)
            {
                foreach (var time in @group.Value.Keys)
                {
                    if (!timeList.Contains(time))
                    {
                        timeList.Add(time);
                    }
                }
            }

            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            Table oTable = oDoc.Tables.Add(wrdRng, 1 + timeList.Count, 1 + (schedule.Count * 2));
            oTable.Borders.Enable = 1;
            oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
            oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            oTable.Range.Font.Size = 11;
            oTable.Range.Font.Bold = 0;

            oTable.Columns[1].Width = oWord.CentimetersToPoints(2.44f);
            float colWidth = 25.64F / schedule.Count;
            for (int i = 0; i < schedule.Count * 2; i += 2)
            {
                oTable.Columns[i + 2].Width = oWord.CentimetersToPoints(colWidth - 1.1f);
                oTable.Columns[i + 3].Width = oWord.CentimetersToPoints(1.1f);
            }


            oTable.Range.Font.Underline = WdUnderline.wdUnderlineNone;


            oTable.Cell(1, 1).Range.Text = "Время занятий";//Constants.DOWLocal[dayOfWeek];
            oTable.Cell(1, 1).Range.Font.Bold = 1;
            oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                WdParagraphAlignment.wdAlignParagraphCenter;

            int groupColumn = 2;


            foreach (var group in schedule)
            {
                var groupObject = repo.StudentGroups.GetStudentGroup(@group.Key);
                var groupName = groupObject.Name;
                oTable.Cell(1, groupColumn).Range.Text = groupName;
                oTable.Cell(1, groupColumn).Range.Font.Bold = 1;
                oTable.Cell(1, groupColumn).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(1, groupColumn).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                oTable.Cell(1, groupColumn + 1).Range.Text = "Ауд";
                oTable.Cell(1, groupColumn + 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                groupColumn += 2;
            }

            var timeRowIndexList = new List<int>();

            var timeRowIndex = 2;
            foreach (var time in timeList.OrderBy(t => int.Parse(t.Split(':')[0]) * 60 + int.Parse(t.Split(':')[1])))
            {
                cToken.ThrowIfCancellationRequested();

                var hour = int.Parse(time.Substring(0, 2));
                var minute = int.Parse(time.Substring(3, 2));

                minute += 40; // TODO: fix this with variable length

                while (minute >= 60)
                {
                    hour++;
                    minute -= 60;
                }


                timeRowIndexList.Add(timeRowIndex);
                oTable.Cell(timeRowIndex, 1).Range.Text = time + "-" +
                                                          hour.ToString("D2") + ":" + minute.ToString("D2");
                oTable.Cell(timeRowIndex, 1).Range.Font.Bold = 1;
                oTable.Cell(timeRowIndex, 1).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(timeRowIndex, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                var columnGroupIndex = 2;
                foreach (var group in schedule)
                {
                    if (@group.Value.ContainsKey(time))
                    {
                        oTable.Cell(timeRowIndex, columnGroupIndex).VerticalAlignment =
                            WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                        var groupDowTimeLessons = @group.Value[time]
                            .OrderBy(tfd => tfd.Value.Item2.Select(l =>
                                repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date)).Min())
                            .ToList();
                        var tfdIndex = 0;

                        if (groupDowTimeLessons.Count() == 2)
                        {
                            if (groupDowTimeLessons[0].Value.Item1.Contains("(чёт.") &&
                                groupDowTimeLessons[1].Value.Item1.Contains("(нечёт."))
                            {
                                var tmp = groupDowTimeLessons[0];
                                groupDowTimeLessons[0] = groupDowTimeLessons[1];
                                groupDowTimeLessons[1] = tmp;
                            }
                        }

                        if (
                            ((@group.Value[time].Count == 2) &&
                             ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) && (groupDowTimeLessons[1].Value.Item1.Contains("чёт."))))
                            || ((@group.Value[time].Count == 1) && (groupDowTimeLessons[0].Value.Item1.Contains("чёт."))))
                        {
                            var rng = oTable.Cell(timeRowIndex, columnGroupIndex).Range;
                            rng.Borders[WdBorderType.wdBorderDiagonalUp].LineStyle = WdLineStyle.wdLineStyleSingle;
                        }


                        var groupObject = repo.StudentGroups.GetStudentGroup(@group.Key);
                        var subGroupOne = repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.Name == groupObject.Name + "1");
                        var subGroupTwo = repo.StudentGroups.GetFirstFiltredStudentGroups(sg => sg.Name == groupObject.Name + "2");

                        if (groupDowTimeLessons.Count() == 2)
                        {
                            if (((subGroupOne != null) && (subGroupTwo != null)) &&
                                ((groupDowTimeLessons[0].Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId == subGroupTwo.StudentGroupId) &&
                                 (groupDowTimeLessons[1].Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId == subGroupOne.StudentGroupId)))
                            {
                                var tmp = groupDowTimeLessons[0];
                                groupDowTimeLessons[0] = groupDowTimeLessons[1];
                                groupDowTimeLessons[1] = tmp;
                            }
                        }

                        var addSubGroupColumn = 0;

                        if ((groupDowTimeLessons.Count() == 1) &&
                            (subGroupOne != null) &&
                            (groupDowTimeLessons[0].Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId == subGroupOne.StudentGroupId))
                        {
                            addSubGroupColumn = 1;

                            var emptytfd = new KeyValuePair<int, Tuple<string, List<Tuple<Lesson, int>>, string>>(-1, null);
                            groupDowTimeLessons.Add(emptytfd);
                        }

                        if ((groupDowTimeLessons.Count() == 1) &&
                            (subGroupTwo != null) &&
                            (groupDowTimeLessons[0].Value.Item2[0].Item1.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId == subGroupTwo.StudentGroupId))
                        {
                            addSubGroupColumn = 1;

                            var emptytfd = new KeyValuePair<int, Tuple<string, List<Tuple<Lesson, int>>, string>>(-1, null);
                            groupDowTimeLessons.Add(emptytfd);

                            var tmp = groupDowTimeLessons[0];
                            groupDowTimeLessons[0] = groupDowTimeLessons[1];
                            groupDowTimeLessons[1] = tmp;
                        }

                        var timeTable = oDoc.Tables.Add(oTable.Cell(timeRowIndex, columnGroupIndex).Range, 1, @group.Value[time].Count + addSubGroupColumn);

                        if (!((groupDowTimeLessons.Count == 2) &&
                              (((groupDowTimeLessons[0].Value != null) && (groupDowTimeLessons[1].Value != null)) &&
                               ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) && (groupDowTimeLessons[1].Value.Item1.Contains("чёт."))))))
                        {
                            for (int i = 0; i < groupDowTimeLessons.Count - 1; i++)
                            {
                                timeTable.Cell(1, i + 1).Borders[WdBorderType.wdBorderRight].Visible = true;
                            }
                        }


                        timeTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                        timeTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        timeTable.Range.Font.Size = 10;
                        timeTable.Range.Font.Bold = 0;


                        foreach (var tfdData in groupDowTimeLessons)
                        {
                            var cellText = "";

                            if (tfdData.Value == null)
                            {
                                tfdIndex++;

                                continue;
                            }

                            var discName = tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Discipline.Name;

                            var shorteningDictionary = new Dictionary<string, string>
                            {
                                {"Английский язык", "Англ. яз."},
                                {"Немецкий язык", "Нем. яз."},
                                {"Французский язык", "Франц. яз."},
                                {"Спец.курс немецкий язык и страноведение", "СК Нем. яз."},
                                {"Спец.курс французский язык и страноведение", "СК Фр. яз."},
                            };

                            if (shorteningDictionary.ContainsKey(discName))
                            {
                                discName = shorteningDictionary[discName];
                            }

                            // Discipline name
                            cellText += discName + Environment.NewLine;

                            // Teacher FIO
                            var ommitInitials = @group.Value[time].Count != 1;
                            String teacherFio = ShortenFio(tfdData.Value.Item2[0].Item1.TeacherForDiscipline.Teacher.FIO, ommitInitials);
                            cellText += teacherFio;

                            // Total weeks
                            if (weeksMarksVisible)
                            {
                                /*
                                if (tfdData.Value.Item1.Contains("(чёт."))
                                {
                                    cellText += "(чёт.)" + Environment.NewLine;
                                }
                                if (tfdData.Value.Item1.Contains("(нечёт."))
                                {
                                    cellText += "(нечёт.)" + Environment.NewLine;
                                }
                                */
                                //cellText += "(" + tfdData.Value.Item1 + ")" + Environment.NewLine;
                            }

                            String audText = "";
                            // Auditoriums
                            var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CommonFunctions.CalculateWeekNumber(l.Item1.Calendar.Date),
                                l => l.Item1.Auditorium.Name);
                            var grouped = audWeekList.GroupBy(a => a.Value);

                            var enumerable = grouped as List<IGrouping<string, KeyValuePair<int, string>>> ?? grouped.ToList();
                            var gcount = enumerable.Count();
                            if (gcount == 1)
                            {
                                audText += ShortenAudName(enumerable.ElementAt(0).Key);
                            }
                            else
                            {
                                for (int j = 0; j < gcount; j++)
                                {
                                    var jItem = enumerable.OrderBy(e => e.Select(ag => ag.Key).ToList().Min()).ElementAt(j);
                                    audText += CommonFunctions.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " +
                                               ShortenAudName(jItem.Key);

                                    if (j != gcount - 1)
                                    {
                                        audText += Environment.NewLine;
                                    }
                                }
                            }

                            if ((groupDowTimeLessons.Count == 1) &&
                                (groupDowTimeLessons[0].Value.Item1.Contains("(чёт.")))
                            {
                                cellText = Environment.NewLine + cellText;
                            }

                            if ((groupDowTimeLessons.Count == 1) &&
                                (groupDowTimeLessons[0].Value.Item1.Contains("(нечёт.")))
                            {
                                cellText = cellText + Environment.NewLine;
                            }
                            //Auditoriums


                            timeTable.Cell(1, tfdIndex + 1).Range.Text = cellText;
                            timeTable.Cell(1, tfdIndex + 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;

                            /*
                             * FIO in one line
                            var lineSpacing = timeTable.Cell(1, tfdIndex + 1).Range.ParagraphFormat.LineSpacing;

                            var Height = timeTable.Cell(1, tfdIndex + 1).Height;

                            if (Height > lineSpacing * 2)
                            {

                            }
                             */

                            timeTable.Cell(1, tfdIndex + 1).VerticalAlignment =
                                WdCellVerticalAlignment.wdCellAlignVerticalCenter;


                            var audCellText = oTable.Cell(timeRowIndex, columnGroupIndex + 1).Range.Text;
                            if (audCellText == "\r\a")
                            {
                                oTable.Cell(timeRowIndex, columnGroupIndex + 1).Range.Text = audText;
                            }
                            else
                            {
                                oTable.Cell(timeRowIndex, columnGroupIndex + 1).Range.Text = audCellText + "/ " + audText;
                            }
                            oTable.Cell(timeRowIndex, columnGroupIndex + 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;
                            oTable.Cell(timeRowIndex, columnGroupIndex + 1).VerticalAlignment =
                                WdCellVerticalAlignment.wdCellAlignVerticalCenter;


                            if ((@group.Value[time].Count == 2) &&
                                ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) && (groupDowTimeLessons[1].Value.Item1.Contains("чёт."))))
                            {
                                timeTable.Cell(1, tfdIndex + 1).Range.ParagraphFormat.Alignment =
                                    (tfdIndex == 0)
                                        ? WdParagraphAlignment.wdAlignParagraphLeft
                                        : WdParagraphAlignment.wdAlignParagraphRight;
                            }

                            if ((groupDowTimeLessons.Count == 1) &&
                                (groupDowTimeLessons[0].Value.Item1.Contains("(чёт.")))
                            {
                                timeTable.Cell(1, tfdIndex + 1).Range.ParagraphFormat.Alignment =
                                    WdParagraphAlignment.wdAlignParagraphRight;
                            }

                            tfdIndex++;
                        }
                    }

                    columnGroupIndex += 2;
                }

                timeRowIndex++;
            }

            return oTable;
        }

        private static string ShortenAudName(string audName)
        {
            return (audName.StartsWith("Ауд. ")) ? audName.Substring(5) : audName;
        }

        private static string ShortenFio(string fio, bool ommitInitials)
        {
            var fioParts = fio.Split(' ').ToList();

            if (fioParts.Count != 3)
            {
                return fio;
            }

            return ommitInitials ?
                 fioParts[0]
                : fioParts[0] + " " + fioParts[1].Substring(0, 1) + "." + fioParts[2].Substring(0, 1) + ".";
        }

        public static void ExportWholeScheduleOneGroupPerPage(ScheduleRepository repo, MainEditForm form, CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            int pageCounter = 0;

            foreach (var faculty in repo.Faculties.GetAllFaculties())
            {
                //var faculty = repo.GetFirstFiltredFaculty(f => f.SortingOrder == i);

                var facultyGroups = repo.Faculties.GetFacultyGroups(faculty.FacultyId);

                foreach (var group in facultyGroups)
                {
                    cToken.ThrowIfCancellationRequested();

                    var sStarts = repo.CommonFunctions.GetSemesterStarts();

                    var groupLessons = repo.Lessons.GetGroupedGroupLessons(group.StudentGroupId, sStarts, null, false, false);

                    var groupEvents = form.CreateGroupTableView(group.StudentGroupId, groupLessons, false);

                    Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                    Table oTable = oDoc.Tables.Add(wrdRng, 1 + groupEvents.Count, 7);
                    oTable.Borders.Enable = 1;
                    oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                    oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    oTable.Range.Font.Size = 10;
                    oTable.Range.Font.Bold = 0;

                    oTable.Cell(1, 1).Range.Text = group.Name;
                    oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;

                    oTable.Columns[1].Width = oWord.CentimetersToPoints(2.44f);
                    const float colWidth = 25.64F / 6;
                    for (int j = 1; j <= 6; j++)
                    {
                        oTable.Columns[j + 1].Width = oWord.CentimetersToPoints(colWidth);
                        oTable.Cell(1, j + 1).Range.Text = Constants.DowLocal[j];
                        oTable.Cell(1, j + 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;
                    }

                    for (int j = 0; j < groupEvents.Count; j++)
                    {
                        cToken.ThrowIfCancellationRequested();

                        oTable.Cell(j + 2, 1).Range.Text = groupEvents[j].Time;
                        oTable.Cell(j + 2, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        oTable.Cell(j + 2, 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;

                        oTable.Cell(j + 2, 2).Range.Text = groupEvents[j].MonEvents;
                        oTable.Cell(j + 2, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        oTable.Cell(j + 2, 3).Range.Text = groupEvents[j].TueEvents;
                        oTable.Cell(j + 2, 3).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        oTable.Cell(j + 2, 4).Range.Text = groupEvents[j].WenEvents;
                        oTable.Cell(j + 2, 4).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        oTable.Cell(j + 2, 5).Range.Text = groupEvents[j].ThuEvents;
                        oTable.Cell(j + 2, 5).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        oTable.Cell(j + 2, 6).Range.Text = groupEvents[j].FriEvents;
                        oTable.Cell(j + 2, 6).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        oTable.Cell(j + 2, 7).Range.Text = groupEvents[j].SatEvents;
                        oTable.Cell(j + 2, 7).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    }

                    pageCounter++;
                    int pageCount;
                    float fontSize = 10.5F;
                    do
                    {
                        fontSize -= 0.5F;
                        oTable.Range.Font.Size = fontSize;

                        if (fontSize <= 3)
                        {
                            break;
                        }

                        pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
                    } while (pageCount > pageCounter);

                    var endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    endOfDoc.Font.Size = 1;
                    endOfDoc.InsertBreak(WdBreakType.wdSectionBreakNextPage);
                }
            }
        }

        public static void ExportGroupSchedulePage(ScheduleRepository repo, MainEditForm form, int groupId,
            bool weekFilteredF, List<int> weekFilterList,
            bool onlyFutureDates, CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            var group = repo.StudentGroups.GetStudentGroup(groupId);

            var sStarts = repo.CommonFunctions.GetSemesterStarts();

            cToken.ThrowIfCancellationRequested();

            List<int> weekFilter = null;
            if (weekFilteredF)
            {
                weekFilter = weekFilterList;
            }
            var groupLessons = repo.Lessons.GetGroupedGroupLessons(group.StudentGroupId, sStarts, weekFilter, false, onlyFutureDates);

            cToken.ThrowIfCancellationRequested();

            List<GroupTableView> groupEvents = form.CreateGroupTableView(group.StudentGroupId, groupLessons, false);

            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            Table oTable = oDoc.Tables.Add(wrdRng, 1 + groupEvents.Count, 7);
            oTable.Borders.Enable = 1;
            oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
            oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            oTable.Range.Font.Size = 10;
            oTable.Range.Font.Bold = 0;

            oTable.Cell(1, 1).Range.Text = group.Name;
            oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;

            oTable.Columns[1].Width = oWord.CentimetersToPoints(2.44f);
            const float colWidth = 25.64F / 6;
            for (int j = 1; j <= 6; j++)
            {
                oTable.Columns[j + 1].Width = oWord.CentimetersToPoints(colWidth);
                oTable.Cell(1, j + 1).Range.Text = Constants.DowLocal[j];
                oTable.Cell(1, j + 1).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;
            }

            for (int j = 0; j < groupEvents.Count; j++)
            {
                cToken.ThrowIfCancellationRequested();

                oTable.Cell(j + 2, 1).Range.Text = groupEvents[j].Time;
                oTable.Cell(j + 2, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                oTable.Cell(j + 2, 1).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;

                oTable.Cell(j + 2, 2).Range.Text = groupEvents[j].MonEvents;
                oTable.Cell(j + 2, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                oTable.Cell(j + 2, 3).Range.Text = groupEvents[j].TueEvents;
                oTable.Cell(j + 2, 3).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                oTable.Cell(j + 2, 4).Range.Text = groupEvents[j].WenEvents;
                oTable.Cell(j + 2, 4).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                oTable.Cell(j + 2, 5).Range.Text = groupEvents[j].ThuEvents;
                oTable.Cell(j + 2, 5).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                oTable.Cell(j + 2, 6).Range.Text = groupEvents[j].FriEvents;
                oTable.Cell(j + 2, 6).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                oTable.Cell(j + 2, 7).Range.Text = groupEvents[j].SatEvents;
                oTable.Cell(j + 2, 7).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            }

            cToken.ThrowIfCancellationRequested();

            int pageCount;
            float fontSize = 10.5F;
            do
            {
                fontSize -= 0.5F;
                oTable.Range.Font.Size = fontSize;

                if (fontSize <= 3)
                {
                    break;
                }

                pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
            } while (pageCount > 1);
        }

        public static void WordStartSchool(ScheduleRepository repo, string filename, bool save, bool quit,
            int lessonLength, int facultyId, int dayOfWeek, int daysOfWeek, bool weekFiltered, List<int> weekFilterList,
            bool weeksMarksVisible, CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            var faculty = repo.Faculties.GetFaculty(facultyId);

            Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = "Расписание " + DetectSemesterString(repo);
            oPara1.Range.Font.Bold = 0;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.SpaceAfter = 0;
            oPara1.Range.InsertParagraphAfter();

            var textBoxRange = oPara1.Range;

            oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Font.Size = 14;
            oPara1.Range.Text = Constants.DowLocal[dayOfWeek].ToUpper();
            oPara1.Range.Font.Bold = 1;
            oPara1.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Range.InsertParagraphAfter();

            Shape cornerStamp = oDoc.Shapes.AddTextbox(
                MsoTextOrientation.msoTextOrientationHorizontal,
                oWord.CentimetersToPoints(22f),
                oWord.CentimetersToPoints(0.5f),
                200, 50,
                textBoxRange);
            cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;

            if (dayOfWeek == 1)
            {
                cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                            Environment.NewLine +
                            "Ректор   ______________     Наянова М.В.   «___» ____________  20__ г.";
                cornerStamp.TextFrame.TextRange.Font.Size = 10;
                cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            }
            cornerStamp.TextFrame.WordWrap = 1;
            cornerStamp.TextFrame.TextRange.ParagraphFormat.SpaceAfter = 0;
            cornerStamp.Line.Visible = MsoTriState.msoFalse;

            cToken.ThrowIfCancellationRequested();

            Table oTable = GetAndPutDowStartSchedule(repo, lessonLength, dayOfWeek, weekFiltered, weekFilterList, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, cToken);

            Table oTable2 = null;

            if ((dayOfWeek != 6) && (dayOfWeek != 7))
            {
                oPara1 = oDoc.Content.Paragraphs.Add();
                oPara1.Range.Font.Size = 14;
                oPara1.Range.Text = Constants.DowLocal[dayOfWeek + 1].ToUpper();
                oPara1.Range.Font.Bold = 1;
                oPara1.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                oPara1.Range.ParagraphFormat.LineSpacingRule =
                    WdLineSpacing.wdLineSpaceSingle;
                oPara1.Range.InsertParagraphAfter();

                oTable2 = GetAndPutDowStartSchedule(repo, lessonLength, dayOfWeek + 1, weekFiltered, weekFilterList, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, cToken);
            }

            cToken.ThrowIfCancellationRequested();

            int pageCount;
            var fontSize = 10.5F;
            do
            {
                fontSize -= 0.5F;
                oTable.Range.Font.Size = fontSize;
                if (oTable2 != null)
                {
                    oTable2.Range.Font.Size = fontSize;
                }

                if (fontSize <= 3)
                {
                    break;
                }

                pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
            } while (pageCount > 1);

            if (save)
            {
                object fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + filename;
                oDoc.SaveAs(ref fileName);
            }

            if (quit)
            {
                oWord.Quit();
            }

            Marshal.ReleaseComObject(oWord);
        }

        public static void WordStartSchool2(ScheduleRepository repo, string filename, bool save, bool quit, 
            int lessonLength, int facultyId, int dayOfWeek, int daysOfWeek,
            bool weekFiltered, List<int> weekFilter, bool weeksMarksVisible, CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            var faculty = repo.Faculties.GetFaculty(facultyId);

            Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = "Расписание " + DetectSemesterString(repo);
            oPara1.Range.Font.Bold = 0;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.SpaceAfter = 0;
            oPara1.Range.InsertParagraphAfter();

            var textBoxRange = oPara1.Range;

            oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Font.Size = 14;
            oPara1.Range.Text = Constants.DowLocal[dayOfWeek].ToUpper();
            oPara1.Range.Font.Bold = 1;
            oPara1.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Range.InsertParagraphAfter();

            Shape cornerStamp = oDoc.Shapes.AddTextbox(
                MsoTextOrientation.msoTextOrientationHorizontal,
                oWord.CentimetersToPoints(22f),
                oWord.CentimetersToPoints(0.5f),
                200, 50,
                textBoxRange);
            cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;

            if (dayOfWeek == 1)
            {
                cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                                                       Environment.NewLine +
                                                       "Ректор   ______________     Наянова М.В.   «___» ____________  20__ г.";
                cornerStamp.TextFrame.TextRange.Font.Size = 10;
                cornerStamp.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            }
            cornerStamp.TextFrame.WordWrap = 1;
            cornerStamp.TextFrame.TextRange.ParagraphFormat.SpaceAfter = 0;
            cornerStamp.Line.Visible = MsoTriState.msoFalse;

            cToken.ThrowIfCancellationRequested();

            Table oTable = GetAndPutDowStartSchedule2(repo, dayOfWeek, weekFiltered, weekFilter, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, null, cToken);

            Table oTable2 = null;

            if ((dayOfWeek != 6) && (dayOfWeek != 7))
            {
                oPara1 = oDoc.Content.Paragraphs.Add();
                oPara1.Range.Font.Size = 14;
                oPara1.Range.Text = Constants.DowLocal[dayOfWeek + 1].ToUpper();
                oPara1.Range.Font.Bold = 1;
                oPara1.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                oPara1.Range.ParagraphFormat.LineSpacingRule =
                    WdLineSpacing.wdLineSpaceSingle;
                oPara1.Range.InsertParagraphAfter();

                oTable2 = GetAndPutDowStartSchedule2(repo, dayOfWeek + 1, weekFiltered, weekFilter, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, null, cToken);
            }

            cToken.ThrowIfCancellationRequested();

            int pageCount;
            var fontSize = 10.5F;
            do
            {
                fontSize -= 0.5F;
                oTable.Range.Font.Size = fontSize;
                if (oTable2 != null)
                {
                    oTable2.Range.Font.Size = fontSize;
                }

                if (fontSize <= 3)
                {
                    break;
                }

                pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
            } while (pageCount > 1);

            if (save)
            {
                object fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + filename;
                oDoc.SaveAs(ref fileName);
            }

            if (quit)
            {
                oWord.Quit();
            }

            Marshal.ReleaseComObject(oWord);
        }

        public static void TeachersSchedule(ScheduleRepository repo, TeacherSchedule tsForm, bool OnlyFutureDates, CancellationToken cToken)
        {
            var teachers = repo.Teachers.GetAllTeachers().OrderBy(t => t.FIO).ToList();

            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */
            int pageCounter = 0;

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            foreach (var teacher in teachers)
            {
                var result = tsForm.GetTeacherScheduleToView(teacher.TeacherId, false, null, false, OnlyFutureDates, cToken);

                var isColumnEmpty = GetEmptyColumnIndexes(result);
                var columnTitles = new List<string>();
                var columnIndexes = new List<int>();
                for (int i = 1; i <= 7; i++)
                {
                    if (!isColumnEmpty[i])
                    {
                        columnTitles.Add(Constants.DowLocal[i]);
                        columnIndexes.Add(i);
                    }
                }

                Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
                oPara1.Range.Text = "Расписание СГОАН (" + teacher.FIO + ")";
                oPara1.Range.Font.Bold = 0;
                oPara1.Range.Font.Size = 10;
                oPara1.Range.ParagraphFormat.LineSpacingRule =
                    WdLineSpacing.wdLineSpaceSingle;
                oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                oPara1.SpaceAfter = 0;
                oPara1.Range.InsertParagraphAfter();

                Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                var dowCount = isColumnEmpty.Count(dow => !dow.Value);

                Table oTable = oDoc.Tables.Add(wrdRng, 1 + result.Count, 1 + dowCount);
                oTable.Borders.Enable = 1;
                oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                oTable.Range.Font.Size = 10;
                oTable.Range.Font.Bold = 0;

                oTable.Cell(1, 1).Range.Text = "Время";
                oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;

                oTable.Columns[1].Width = oWord.CentimetersToPoints(2.44f);
                float colWidth = 16.3F / dowCount;
                for (int i = 0; i < dowCount; i++)
                {
                    oTable.Columns[i + 2].Width = oWord.CentimetersToPoints(colWidth);
                    oTable.Cell(1, i + 2).Range.Text = columnTitles[i];
                    oTable.Cell(1, i + 2).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                }

                for (int i = 0; i < result.Count; i++)
                {
                    oTable.Cell(2 + i, 1).Range.Text = result[i].Time;
                    oTable.Cell(2 + i, 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Cell(2 + i, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    for (int j = 0; j < dowCount; j++)
                    {
                        var dowIndex = columnIndexes[j];
                        switch (dowIndex)
                        {
                            case 1:
                                oTable.Cell(2 + i, 2 + j).Range.Text = result[i].MonLessons;
                                break;
                            case 2:
                                oTable.Cell(2 + i, 2 + j).Range.Text = result[i].TueLessons;
                                break;
                            case 3:
                                oTable.Cell(2 + i, 2 + j).Range.Text = result[i].WedLessons;
                                break;
                            case 4:
                                oTable.Cell(2 + i, 2 + j).Range.Text = result[i].ThuLessons;
                                break;
                            case 5:
                                oTable.Cell(2 + i, 2 + j).Range.Text = result[i].FriLessons;
                                break;
                            case 6:
                                oTable.Cell(2 + i, 2 + j).Range.Text = result[i].SatLessons;
                                break;
                            case 7:
                                oTable.Cell(2 + i, 2 + j).Range.Text = result[i].SunLessons;
                                break;
                        }

                        oTable.Cell(2 + i, 2 + j).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    }
                }

                pageCounter++;
                int pageCount;
                float fontSize = 10.5F;
                do
                {
                    fontSize -= 0.5F;
                    oTable.Range.Font.Size = fontSize;

                    if (fontSize <= 3)
                    {
                        break;
                    }

                    pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
                } while (pageCount > pageCounter);

                var endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                endOfDoc.Font.Size = 1;
                endOfDoc.InsertBreak(WdBreakType.wdSectionBreakNextPage);
            }

            oDoc.Undo();

            Marshal.ReleaseComObject(oWord);
        }

        public static void ExportCultureDates(ScheduleRepository repo)
        {
            var discs = repo.Disciplines.GetFiltredDisciplines(d => d.Name.Contains("изическ")).ToList();

            discs = discs.Where(d => !d.StudentGroup.Name.StartsWith("1 ") &&
                                     !d.StudentGroup.Name.StartsWith("2 ") &&
                                     !d.StudentGroup.Name.StartsWith("3 ") &&
                                     !d.StudentGroup.Name.StartsWith("4 ") &&
                                     !d.StudentGroup.Name.StartsWith("5 ") &&
                                     !d.StudentGroup.Name.StartsWith("6 ") &&
                                     !d.StudentGroup.Name.StartsWith("7 ") &&
                                     !d.StudentGroup.Name.StartsWith("8 ") &&
                                     !d.StudentGroup.Name.StartsWith("9 ") &&
                                     !d.StudentGroup.Name.StartsWith("10 ") &&
                                     !d.StudentGroup.Name.StartsWith("11 ")
            ).ToList();

            discs.Sort((x, y) =>
            {
                var teacherFIO1 = "";
                var tfd1 =
                    repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(
                        tefd => tefd.Discipline.DisciplineId == x.DisciplineId);
                if (tfd1 != null)
                {
                    teacherFIO1 = tfd1.Teacher.FIO;
                }

                var teacherFIO2 = "";
                var tfd2 =
                    repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(
                        tefd => tefd.Discipline.DisciplineId == y.DisciplineId);
                if (tfd2 != null)
                {
                    teacherFIO2 = tfd2.Teacher.FIO;
                }

                if ((teacherFIO1 == "") || (teacherFIO2 == ""))
                {
                    return String.CompareOrdinal(teacherFIO1, teacherFIO2);
                }

                if (teacherFIO1 == teacherFIO2)
                {
                    return tfd1.Discipline.StudentGroup.Name == tfd2.Discipline.StudentGroup.Name ?
                        String.CompareOrdinal(tfd1.Discipline.Name, tfd2.Discipline.Name) :
                        String.CompareOrdinal(tfd1.Discipline.StudentGroup.Name, tfd2.Discipline.StudentGroup.Name);
                }

                return String.Compare(teacherFIO1, teacherFIO2, StringComparison.Ordinal);
            });

            object oMissing = Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            TeacherForDiscipline tfd = null;

            for (int i = 0; i < discs.Count; i++)
            {
                var teacherFIO = "";

                tfd =
                    repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(
                        tefd => tefd.Discipline.DisciplineId == discs[i].DisciplineId);
                if (tfd != null)
                {
                    teacherFIO = tfd.Teacher.FIO;
                }

                Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
                oPara1.Range.Text = teacherFIO + " " +
                    discs[i].Name + " " +
                    discs[i].PracticalHours.ToString() + " " +
                    ((Constants.Attestation.ContainsKey(discs[i].Attestation)) ? Constants.Attestation[discs[i].Attestation] : "") + " " +
                    discs[i].StudentGroup.Name;
                oPara1.Range.Font.Bold = 0;
                oPara1.Range.Font.Size = 10;
                oPara1.Range.ParagraphFormat.LineSpacingRule =
                    WdLineSpacing.wdLineSpaceSingle;
                oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                oPara1.SpaceAfter = 0;
                oPara1.Range.InsertParagraphAfter();

                if (tfd == null)
                {
                    continue;
                }

                var tfd1 = tfd;
                var discLessons =
                    repo.Lessons.GetFiltredLessons(
                        l =>
                            l.TeacherForDiscipline.TeacherForDisciplineId == tfd1.TeacherForDisciplineId &&
                            (l.State == 1 || l.State == 2))
                        .ToList();

                discLessons = discLessons.OrderBy(l => l.Calendar.Date).ToList();


                Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                Table oTable = oDoc.Tables.Add(wrdRng, discLessons.Count, 2);
                oTable.Borders.Enable = 1;
                oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                oTable.Range.Font.Size = 14;
                oTable.Range.Font.Bold = 0;

                oTable.Columns[1].Width = oWord.CentimetersToPoints(1.2f);
                oTable.Columns[2].Width = oWord.CentimetersToPoints(3.25f);

                for (int j = 0; j < discLessons.Count; j++)
                {
                    oTable.Cell(j + 1, 1).Range.Text = (j + 1).ToString();
                    oTable.Cell(j + 1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    oTable.Cell(j + 1, 2).Range.Text = discLessons[j].Calendar.Date.ToString("dd.MM.yyyy");
                    oTable.Cell(j + 1, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                }

                var pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
                var fontSize = 14.5F;
                do
                {
                    fontSize -= 0.5F;
                    oTable.Range.Font.Size = fontSize;

                    if (fontSize <= 3)
                    {
                        break;
                    }

                    pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);
                } while (pageCount > ((i + 1) * 2 - 1));

                var endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                endOfDoc.Font.Size = 1;
                endOfDoc.InsertBreak(WdBreakType.wdSectionBreakNextPage);

                oPara1 = oDoc.Content.Paragraphs.Add();
                oPara1.Range.Text = teacherFIO + " " +
                    discs[i].Name + " " +
                    discs[i].PracticalHours.ToString() + " " +
                    ((Constants.Attestation.ContainsKey(discs[i].Attestation)) ? Constants.Attestation[discs[i].Attestation] : "") + " " +
                    discs[i].StudentGroup.Name;
                oPara1.Range.Font.Bold = 0;
                oPara1.Range.Font.Size = 10;
                oPara1.Range.ParagraphFormat.LineSpacingRule =
                    WdLineSpacing.wdLineSpaceSingle;
                oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                oPara1.SpaceAfter = 0;
                oPara1.Range.InsertParagraphAfter();

                var students = repo.StudentsInGroups
                    .GetFiltredStudentsInGroups(
                        sig =>
                            sig.StudentGroup.StudentGroupId == discs[i].StudentGroup.StudentGroupId &&
                            !sig.Student.Expelled)
                    .Select(sig => sig.Student)
                    .OrderBy(s => s.F)
                    .ThenBy(s => s.I)
                    .ThenBy(s => s.O)
                    .ThenBy(s => s.ZachNumber)
                    .ToList();

                endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                oTable = endOfDoc.Tables.Add(endOfDoc, students.Count, 2);
                oTable.Borders.Enable = 1;
                oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                oTable.Range.Font.Size = 14;
                oTable.Range.Font.Bold = 0;

                oTable.Columns[1].Width = oWord.CentimetersToPoints(1.2f);


                for (int j = 0; j < students.Count; j++)
                {
                    var student = students[j];

                    oTable.Cell(j + 1, 1).Range.Text = (j + 1).ToString();
                    oTable.Cell(j + 1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    oTable.Cell(j + 1, 2).Range.Text = student.F + " " + student.I + " " + student.O;
                    oTable.Cell(j + 1, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                }


                endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                endOfDoc.Font.Size = 1;
                endOfDoc.InsertBreak(WdBreakType.wdSectionBreakNextPage);

            }

            oDoc.Undo();

            Marshal.ReleaseComObject(oWord);

        }

        public static void ExportFacultyDates(List<string> dbNames, string facultyName, string filename, bool save,
            bool quit, Dictionary<string, List<string>> restrictions)
        {
            object oMissing = Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application();
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            for (int semIndex = 0; semIndex < dbNames.Count; semIndex++)
            {
                var connectionString = "data source=tcp:" + StartupForm.CurrentServerName + ",1433; Database=" +
                                       dbNames[semIndex] +
                                       "; User ID=sa;Password=ghjuhfvvf; multipleactiveresultsets=True";

                var repo = new ScheduleRepository(connectionString);
                
                var faculty = repo.Faculties.GetFirstFiltredFaculty(f => f.Name.Contains(facultyName));

                var groups = (faculty != null) ? repo
                    .GroupsInFaculties
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                    .Select(gif => gif.StudentGroup)
                    .ToList() : new List<StudentGroup>();

                if (restrictions != null)
                {
                    if (!restrictions.ContainsKey(dbNames[semIndex]))
                    {
                        groups.Clear();
                    }
                    else
                    {
                        groups = groups.Where(sg => restrictions[dbNames[semIndex]].Contains(sg.Name)).ToList();
                    }
                }

                for (int i = 0; i < groups.Count; i++)
                {
                    var studentGroup = groups[i];
                    
                    var studentIds = repo.StudentsInGroups
                        .GetFiltredStudentsInGroups(
                            sig => sig.StudentGroup.StudentGroupId == studentGroup.StudentGroupId &&
                                   !sig.Student.Expelled)
                        .Select(stig => stig.Student.StudentId);

                    var groupsListIds = repo.StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                        .Select(stig => stig.StudentGroup.StudentGroupId);

                    var discs = repo.Disciplines
                        .GetFiltredDisciplines(d => groupsListIds.Contains(d.StudentGroup.StudentGroupId)).ToList();

                    var tfds = new List<TeacherForDiscipline>();
                    foreach (var discipline in discs)
                    {
                        var tefd = repo.TeacherForDisciplines
                            .GetFirstFiltredTeacherForDiscipline(
                                tfd => tfd.Discipline.DisciplineId == discipline.DisciplineId);
                        if (tefd != null)
                        {
                            tfds.Add(tefd);
                        }
                    }

                    var eprst = 999;

                    foreach (var teacherForDiscipline in tfds.OrderBy(tefd => tefd.Discipline.Name)
                        .ThenBy(tefd => tefd.Teacher.FIO))
                    {
                        Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
                        oPara1.Range.Text = "Семестр " + dbNames[semIndex] + "\t" + studentGroup.Name;
                        oPara1.Range.Font.Bold = 0;
                        oPara1.Range.Font.Size = 14;
                        oPara1.Range.ParagraphFormat.LineSpacingRule =
                            WdLineSpacing.wdLineSpaceSingle;
                        oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        oPara1.SpaceAfter = 0;
                        oPara1.Range.InsertParagraphAfter();

                        oPara1 = oDoc.Content.Paragraphs.Add();
                        oPara1.Range.Text = teacherForDiscipline.Discipline.StudentGroup.Name + "\t" +
                                            teacherForDiscipline.Discipline.Name + "\t" +
                                            teacherForDiscipline.Teacher.FIO + "\t" +
                                            teacherForDiscipline.Discipline.AuditoriumHours + "\t" +
                                            teacherForDiscipline.Discipline.LectureHours + "\t" +
                                            teacherForDiscipline.Discipline.PracticalHours + "\t" +
                                            Constants.Attestation[teacherForDiscipline.Discipline.Attestation];
                        oPara1.Range.Font.Bold = 0;
                        oPara1.Range.Font.Size = 10;
                        oPara1.Range.ParagraphFormat.LineSpacingRule =
                            WdLineSpacing.wdLineSpaceSingle;
                        oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        oPara1.SpaceAfter = 0;
                        oPara1.Range.InsertParagraphAfter();

                        var lessons = repo
                            .Lessons
                            .GetFiltredLessons(l =>
                                l.TeacherForDiscipline.TeacherForDisciplineId ==
                                teacherForDiscipline.TeacherForDisciplineId &&
                                (l.State == 1 || l.State == 2))
                            .OrderBy(l => l.Calendar.Date.Date)
                            .ThenBy(l => l.Ring.Time.TimeOfDay)
                            .ToList();

                        Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                        Table oTable = oDoc.Tables.Add(wrdRng, lessons.Count + 1, 5);
                        oTable.Borders.Enable = 1;
                        oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                        oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        oTable.Range.Font.Size = 14;
                        oTable.Range.Font.Bold = 0;

                        oTable.Cell(1, 1).Range.Text = "№";
                        oTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(1, 2).Range.Text = "Дата";
                        oTable.Cell(1, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(1, 3).Range.Text = "Время";
                        oTable.Cell(1, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(1, 4).Range.Text = "Аудитория";
                        oTable.Cell(1, 4).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(1, 5).Range.Text = "Тип занятия";
                        oTable.Cell(1, 5).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;


                        for (int j = 0; j < lessons.Count; j++)
                        {
                            oTable.Cell(j + 2, 1).Range.Text = (j + 1).ToString();
                            oTable.Cell(j + 2, 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;

                            oTable.Cell(j + 2, 2).Range.Text = lessons[j].Calendar.Date.ToString("dd.MM.yyyy");
                            oTable.Cell(j + 2, 2).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;

                            oTable.Cell(j + 2, 3).Range.Text = lessons[j].Ring.Time.ToString("HH:mm");
                            oTable.Cell(j + 2, 3).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;

                            oTable.Cell(j + 2, 4).Range.Text = lessons[j].Auditorium.Name;
                            oTable.Cell(j + 2, 4).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;

                            var lType = repo.Lessons.GetLessonType(lessons[j], true);
                            oTable.Cell(j + 2, 5).Range.Text =
                                (Constants.LessonTypeLongAbbreviation.ContainsKey(lType))
                                    ? Constants.LessonTypeLongAbbreviation[lType]
                                    : "";
                            oTable.Cell(j + 2, 5).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;

                        }

                        var endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                        endOfDoc.Font.Size = 1;
                        endOfDoc.InsertBreak(WdBreakType.wdSectionBreakNextPage);
                    }
                }
            }

            oDoc.Undo();

            if (save)
            {
                object fileName = filename;
                oDoc.SaveAs(ref fileName);
            }

            if (quit)
            {
                oWord.Quit();
            }

            Marshal.ReleaseComObject(oWord);

        }

        public static void ExportTypeSequenceInfoByFaculty(ScheduleRepository repo)
        {
            var faculties = repo.Faculties.GetAllFaculties().OrderBy(f => f.SortingOrder).ToList();

            object oMissing = Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            for (int i = 0; i < faculties.Count; i++)
            {
                var faculty = faculties[i];

                AddPara(oDoc, faculty.Name, 14);

                var facultyGroups =
                    repo.GroupsInFaculties
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                    .Select(gif => gif.StudentGroup)
                    .ToList();

                for (int j = 0; j < facultyGroups.Count; j++)
                {
                    var studentGroup = facultyGroups[j];
                    
                    AddPara(oDoc, studentGroup.Name);

                    var studentIds = repo
                        .StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroup.StudentGroupId && !sig.Student.Expelled)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);

                    var groupsListIds = repo
                        .StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                        .ToList()
                        .Select(stig => stig.StudentGroup.StudentGroupId)
                        .Distinct()
                        .ToList();

                    var groupDisciplines =
                        repo.Disciplines.GetFiltredDisciplines(
                            d => groupsListIds.Contains(d.StudentGroup.StudentGroupId)).ToList();


                    Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    Table oTable = oDoc.Tables.Add(wrdRng, groupDisciplines.Count+1, 2);
                    oTable.Borders.Enable = 1;
                    oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                    oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    oTable.Range.Font.Size = 14;
                    oTable.Range.Font.Bold = 0;

                    oTable.Cell(1, 1).Range.Text = "Дисциплина";
                    oTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    oTable.Cell(1, 2).Range.Text = "Последовательность занятий";
                    oTable.Cell(1, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    for (int k = 0; k < groupDisciplines.Count; k++)
                    {
                        var discipline = groupDisciplines[k];

                        oTable.Cell(k + 2, 1).Range.Text = discipline.Name;
                        oTable.Cell(k + 2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                        var sequence = (discipline.AuditoriumHours != 0 && string.IsNullOrEmpty(discipline.TypeSequence))
                            ? "Нет данных"
                            : discipline.TypeSequence;

                        oTable.Cell(k + 2, 2).Range.Text = sequence;
                        oTable.Cell(k + 2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    }
                }

                AddPara(oDoc, "", 1);

                Range wordRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                Table legendTable = oDoc.Tables.Add(wordRng, 1 + Constants.LessonTypeLongAbbreviation.Count, 2);
                legendTable.Borders.Enable = 1;
                
                legendTable.Cell(1, 1).Range.Text = "Код";
                legendTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                legendTable.Cell(1, 2).Range.Text = "Вид занятия";
                legendTable.Cell(1, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                var lineNumber = 2;
                foreach (var kvp in Constants.LessonTypeLongAbbreviation.OrderBy(p => p.Key))
                {
                    legendTable.Cell(lineNumber, 1).Range.Text = kvp.Key.ToString();
                    legendTable.Cell(lineNumber, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                    legendTable.Cell(lineNumber, 2).Range.Text = kvp.Value;
                    legendTable.Cell(lineNumber, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                    lineNumber++;
                }

                AddPara(oDoc, "", 1);

                var wordRng2 = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                Table signTable = oDoc.Tables.Add(wordRng2, 1, 2);
                signTable.Borders.Enable = 0;
                signTable.Range.Bold = 0;

                
                signTable.Cell(1, 1).Range.Text = faculty.ScheduleSigningTitle;
                signTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                signTable.Cell(1, 2).Range.Text = "_________________  " + faculty.DeanSigningSchedule;
                signTable.Cell(1, 2).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphRight;

                var endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                endOfDoc.Font.Size = 1;
                endOfDoc.InsertBreak(WdBreakType.wdSectionBreakNextPage);
            }

            oDoc.Undo();

            Marshal.ReleaseComObject(oWord);
        }

        private static void AddPara(_Document oDoc, string text, int size = 10, int bold = 0, WdLineSpacing spacing = WdLineSpacing.wdLineSpaceSingle, WdParagraphAlignment alignment = WdParagraphAlignment.wdAlignParagraphCenter)
        {
            Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = text;
            oPara1.Range.Font.Bold = 0;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.SpaceAfter = 0;
            oPara1.Range.InsertParagraphAfter();
        }

        public static void ExportAADisciplineList(List<string> dbNames, string facultyString, string filename,
            bool save, bool quit, bool appVisible, bool appendGroupStudents,
            Dictionary<string, List<string>> groupsRestriction)
        {
            object oMissing = Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord = new Application();
            if (appVisible)
            {
                oWord.Visible = true;
            }
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            for (int semIndex = 0; semIndex < dbNames.Count; semIndex++)
            {
                var connectionString = "data source=tcp:" + StartupForm.CurrentServerName + ",1433; Database=" +
                                       dbNames[semIndex] +
                                       "; User ID=sa;Password=ghjuhfvvf; multipleactiveresultsets=True";

                var repo = new ScheduleRepository(connectionString);

                var coSemesterStarts =
                    repo.ConfigOptions.GetFirstFiltredConfigOption(co => co.Key == "Semester Starts");

                DateTime dtss = DateTime.Now;
                if (coSemesterStarts != null)
                {
                    dtss = DateTime.ParseExact(coSemesterStarts.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                
                var yearsString = "";
                var semesterString = "";

                if (dtss.Month > 6)
                {
                    var y = dtss.Year - 2000;
                    yearsString = y.ToString() + " / " + (y+1).ToString();
                    semesterString = "1";
                }
                else
                {
                    var y = dtss.Year - 1 - 2000;
                    yearsString = y.ToString() + " / " + (y + 1).ToString();
                    semesterString = "2";
                }

                var faculty = repo.Faculties.GetFirstFiltredFaculty(f => f.Name.Contains(facultyString));

                var groups = (faculty != null) ? repo
                    .GroupsInFaculties
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty.FacultyId)
                    .Select(gif => gif.StudentGroup)
                    .ToList() : new List<StudentGroup>();

                if (groupsRestriction != null)
                {
                    if (!groupsRestriction.ContainsKey(dbNames[semIndex]))
                    {
                        continue;
                    }
                    else
                    {
                        groups = groups.Where(sg => groupsRestriction[dbNames[semIndex]].Contains(sg.Name)).ToList();
                    }
                }

                for (int i = 0; i < groups.Count; i++)
                {
                    var studentGroup = groups[i];

                    var studentIds = repo.StudentsInGroups
                        .GetFiltredStudentsInGroups(
                            sig => sig.StudentGroup.StudentGroupId == studentGroup.StudentGroupId &&
                                   !sig.Student.Expelled)
                        .Select(stig => stig.Student.StudentId);

                    var groupsListIds = repo.StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                        .Select(stig => stig.StudentGroup.StudentGroupId);

                    var discs = repo.Disciplines
                        .GetFiltredDisciplines(d => groupsListIds.Contains(d.StudentGroup.StudentGroupId)).ToList();

                    var tfds = new List<TeacherForDiscipline>();
                    foreach (var discipline in discs)
                    {
                        var tefd = repo.TeacherForDisciplines
                            .GetFirstFiltredTeacherForDiscipline(
                                tfd => tfd.Discipline.DisciplineId == discipline.DisciplineId);
                        if (tefd != null)
                        {
                            tfds.Add(tefd);
                        }
                    }
                    
                    Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
                    oPara1.Range.Text = "Семестр " + dbNames[semIndex] + "\t" + studentGroup.Name;
                    oPara1.Range.Font.Bold = 0;
                    oPara1.Range.Font.Size = 14;
                    oPara1.Range.ParagraphFormat.LineSpacingRule =
                        WdLineSpacing.wdLineSpaceSingle;
                    oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    oPara1.SpaceAfter = 0;
                    oPara1.Range.InsertParagraphAfter();

                    Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    Table oTable = oDoc.Tables.Add(wrdRng, tfds.Count + 1, 10 + ((appendGroupStudents) ? 2 : 0));
                    oTable.Borders.Enable = 1;
                    oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                    oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    oTable.Range.Font.Size = 14;
                    oTable.Range.Font.Bold = 0;
                    
                    oTable.Cell(1, 1).Range.Text = "Год";
                    oTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Columns[1].Width = oWord.CentimetersToPoints(1.5f);
                    oTable.Cell(1, 2).Range.Text = "Дисциплина";
                    oTable.Cell(1, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Columns[2].Width = oWord.CentimetersToPoints(5.75f - ((appendGroupStudents) ? 2f : 0f));
                    oTable.Cell(1, 3).Range.Text = "Семестр";
                    oTable.Cell(1, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Columns[3].Width = oWord.CentimetersToPoints(1.25f);
                    oTable.Cell(1, 4).Range.Text = "Часы";
                    oTable.Cell(1, 4).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Columns[4].Width = oWord.CentimetersToPoints(1.5f);
                    oTable.Cell(1, 5).Range.Text = "Лекции";
                    oTable.Cell(1, 5).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Columns[5].Width = oWord.CentimetersToPoints(1.5f);
                    oTable.Cell(1, 6).Range.Text = "Практика";
                    oTable.Cell(1, 6).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Columns[6].Width = oWord.CentimetersToPoints(1.5f);
                    oTable.Cell(1, 7).Range.Text = "Отчётность";
                    oTable.Cell(1, 7).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Columns[7].Width = oWord.CentimetersToPoints(2.5f);
                    oTable.Cell(1, 8).Range.Text = "Группа";
                    oTable.Cell(1, 8).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Columns[8].Width = oWord.CentimetersToPoints(1.5f);
                    oTable.Cell(1, 9).Range.Text = "ФИО преподавателя";
                    oTable.Cell(1, 9).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Columns[9].Width = oWord.CentimetersToPoints(7.75f - ((appendGroupStudents) ? 4f : 0f));
                    oTable.Cell(1, 10).Range.Text = "Группа дисциплины";
                    oTable.Cell(1, 10).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    oTable.Columns[10].Width = oWord.CentimetersToPoints(3.25f - ((appendGroupStudents) ? 1.5f : 0f));
                    if (appendGroupStudents)
                    {
                        oTable.Cell(1, 11).Range.Text = "К-во ст";
                        oTable.Cell(1, 11).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Columns[11].Width = oWord.CentimetersToPoints(1f);

                        oTable.Cell(1, 12).Range.Text = "Студенты";
                        oTable.Cell(1, 12).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Columns[12].Width = oWord.CentimetersToPoints(6.5f);
                    }

                    oTable.Rows[1].HeadingFormat = -1;
                    oTable.ApplyStyleHeadingRows = true;

                    tfds = tfds
                        .OrderBy(tefd => tefd.Teacher.FIO)
                        .ThenBy(tefd => tefd.Discipline.Name)
                        .ToList();

                    

                    for (int j = 0; j < tfds.Count; j++)
                    {
                        var currentTfd = tfds[j];

                        string studentsString = "";
                        var studentsCount = 0;
                        if (appendGroupStudents)
                        {
                            var studentsList = repo.StudentsInGroups
                                .GetFiltredStudentsInGroups(
                                    sig => sig.StudentGroup.StudentGroupId ==
                                           currentTfd.Discipline.StudentGroup.StudentGroupId &&
                                           !sig.Student.Expelled)
                                .ToList()
                                .Select(sig => sig.Student.F + " " + sig.Student.I + " " + sig.Student.O)
                                .OrderBy(a => a)
                                .ToList();
                            studentsCount = studentsList.Count;

                            for (int k = 0; k < studentsList.Count; k++)
                            {
                                studentsString += (k+1).ToString() + ") " + studentsList[k];
                                if (k != studentsList.Count - 1)
                                {
                                    studentsString += Environment.NewLine;
                                }
                            }
                        }

                        oTable.Cell(j + 2, 1).Range.Text = yearsString;
                        oTable.Cell(j + 2, 1).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(j + 2, 2).Range.Text = currentTfd.Discipline.Name;
                        oTable.Cell(j + 2, 2).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphLeft;
                        oTable.Cell(j + 2, 3).Range.Text = semesterString;
                        oTable.Cell(j + 2, 3).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(j + 2, 4).Range.Text = currentTfd.Discipline.AuditoriumHours.ToString();
                        oTable.Cell(j + 2, 4).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(j + 2, 5).Range.Text = currentTfd.Discipline.LectureHours.ToString();
                        oTable.Cell(j + 2, 5).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(j + 2, 6).Range.Text = currentTfd.Discipline.PracticalHours.ToString();
                        oTable.Cell(j + 2, 6).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(j + 2, 7).Range.Text = Constants.Attestation[currentTfd.Discipline.Attestation];
                        oTable.Cell(j + 2, 7).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(j + 2, 8).Range.Text = studentGroup.Name;
                        oTable.Cell(j + 2, 8).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                        oTable.Cell(j + 2, 9).Range.Text = currentTfd.Teacher.FIO;
                        oTable.Cell(j + 2, 9).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphLeft;
                        oTable.Cell(j + 2, 10).Range.Text = currentTfd.Discipline.StudentGroup.Name;
                        oTable.Cell(j + 2, 10).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;

                        if (appendGroupStudents)
                        {
                            oTable.Cell(j + 2, 11).Range.Text = studentsCount.ToString();
                            oTable.Cell(j + 2, 11).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;
                            oTable.Cell(j + 2, 12).Range.Text = studentsString;
                            oTable.Cell(j + 2, 12).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphLeft;
                        }
                    }

                    var endOfDoc = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    endOfDoc.Font.Size = 1;
                    endOfDoc.InsertBreak(WdBreakType.wdSectionBreakNextPage);
                }
            }

            oDoc.Undo();

            if (save)
            {
                object fileName = filename;
                oDoc.SaveAs(ref fileName);
            }

            if (quit)
            {
                oWord.Quit();
            }

            Marshal.ReleaseComObject(oWord);
        }

        public static void MergeDocuments(List<string> filenames, string resultFilename, bool ScheduleSixPagesBreakCorrection)
        {
            // Set up the word object
            object fname = resultFilename;

            // Create Word application
            _Application oWord = new Application();
            
            try
            {
                var orientation = WdOrientation.wdOrientPortrait;
                if (filenames.Count > 0)
                {
                    object firstFileName = filenames[0];
                    var firstDoc = oWord.Documents.Open(firstFileName);
                    orientation = firstDoc.PageSetup.Orientation;
                    firstDoc.Close();
                }

                // Create new file
                _Document oDoc = oWord.Documents.Add();

                oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
                oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
                oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
                oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

                oDoc.PageSetup.Orientation = orientation;

                Selection selection = oWord.Selection;
                
                // Insert file
                for (int i = 0; i < filenames.Count; i++)
                {
                    selection.InsertFile(filenames[i]);

                    var pageCount = oDoc.ComputeStatistics(WdStatistic.wdStatisticPages);

                    if ((i != filenames.Count - 1) && (pageCount % 6 == 0 || !ScheduleSixPagesBreakCorrection))
                    {
                        selection.InsertBreak(WdBreakType.wdPageBreak);
                    }
                }

                // SaveAs
                oDoc.SaveAs(ref fname);
            }
            finally
            {
                // Close Word application
                oWord.Quit();
            }

            Marshal.ReleaseComObject(oWord);
        }
    }
}
