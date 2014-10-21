using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using NUDispSchedule.Views;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Shape = Microsoft.Office.Interop.Word.Shape;
using Schedule.Constants;
using Schedule.Views.DBListViews;
using UchOtd.Schedule;

namespace UchOtd.Core
{
    public static class WordExport
    {
        public static void ExportSchedulePage(
            ScheduleRepository repo, string filename, bool save, bool quit, 
            int lessonLength, int facultyId, int dayOfWeek, int daysOfWeek, 
            bool weekFiltered, int weekFilter, bool weeksMarksVisible)
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

            var faculty = repo.GetFaculty(facultyId);

            var dow = Constants.DOWLocal[dayOfWeek];

            var schedule = repo.GetFacultyDOWSchedule(faculty.FacultyId, dayOfWeek, weekFiltered, weekFilter);

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
                Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
                oWord.CentimetersToPoints(22f),
                oWord.CentimetersToPoints(0.5f),
                200, 50,
                textBoxRange);
            cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            if (dow == "Понедельник")
            {
                var prorUchRabNameOption = repo.GetFirstFiltredConfigOption(co => co.Key == "Проректор по учебной работе");
                var prorUchRabName = (prorUchRabNameOption == null) ? "" : prorUchRabNameOption.Value;

                cornerStamp.TextFrame.TextRange.Text = @"«УТВЕРЖДАЮ»" +
                    Environment.NewLine + "Проректор по учебной работе" +
                    Environment.NewLine + "______________     " + prorUchRabName;
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
            var plainNGroupIds = new Dictionary<int, Tuple<int,int>>();

            foreach (var group in schedule)
            {
                var groupObject = repo.GetStudentGroup(group.Key);
                var groupName = groupObject.Name;
                oTable.Cell(1, groupColumn).Range.Text = groupName.Replace(" (+Н)", "");
                oTable.Cell(1, groupColumn).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                groupColumn++;

                if (groupName.Contains(" (+Н)"))
                {
                    var plainGroupName = groupName.Replace(" (+Н)", "");
                    var nGroupName = groupName.Replace(" (+", "(");

                    var plainGroupId = repo.FindStudentGroup(plainGroupName).StudentGroupId;
                    var plainStudentIds = repo.GetAllStudentsInGroups()
                            .Where(sig => sig.StudentGroup.StudentGroupId == plainGroupId)
                            .Select(stig => stig.Student.StudentId)
                            .ToList();
                    plainGroupsListIds.Add(group.Key, repo.GetAllStudentsInGroups()
                            .Where(sig => plainStudentIds.Contains(sig.Student.StudentId))
                            .Select(stig => stig.StudentGroup.StudentGroupId)
                            .Distinct()
                            .ToList());

                    var nGroupId = repo.FindStudentGroup(nGroupName).StudentGroupId;
                    var nStudentIds = repo.GetAllStudentsInGroups()
                            .Where(sig => sig.StudentGroup.StudentGroupId == nGroupId)
                            .Select(stig => stig.Student.StudentId)
                            .ToList();
                    nGroupsListIds.Add(group.Key, repo.GetAllStudentsInGroups()
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
                        foreach (var tfdData in group.Value[time].OrderBy(tfd => tfd.Value.Item2.Select(l => repo.CalculateWeekNumber(l.Calendar.Date)).Min()))
                        {
                            var cellText = "";
                            // Discipline name
                            cellText += tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.Name;

                            // N + Group modifiers
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

                            var tfdGroupId = tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;
                            if ((tfdGroupId != group.Key))
                            {
                                if ((!plainNGroupIds.ContainsKey(group.Key)) || ((tfdGroupId != plainNGroupIds[group.Key].Item1) && (tfdGroupId != plainNGroupIds[group.Key].Item2)))
                                {
                                    cellText += " (" + tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.StudentGroup.Name + ")";
                                }
                            }

                            cellText += Environment.NewLine;
                            // Teacher FIO
                            cellText += tfdData.Value.Item2[0].TeacherForDiscipline.Teacher.FIO + Environment.NewLine;

                            // Total weeks
                            if (weeksMarksVisible)
                            {
                                cellText += "(" + tfdData.Value.Item1 + ")" + Environment.NewLine;
                            }

                            var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
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

            if (dayOfWeek == daysOfWeek)
            {
                var oPara3 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara3.Range.Font.Size = 12;
                oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara3.Range.Text = "";
                oPara3.Format.SpaceAfter = 0;
                oPara3.Range.InsertParagraphAfter();

                var headUchOtdNameOption = repo.GetFirstFiltredConfigOption(co => co.Key == "Начальник учебного отдела");
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

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
        }

        public static void ExportTwoSchedulePages(
            ScheduleRepository repo, string filename, bool save, bool quit,
            int lessonLength, int facultyId, int dayOfWeek, int daysOfWeek,
            bool weekFiltered, int weekFilter, bool weeksMarksVisible)
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

            var faculty = repo.GetFaculty(facultyId);

            var firstDaySchedule = repo.GetFacultyDOWSchedule(faculty.FacultyId, dayOfWeek, weekFiltered, weekFilter);

            var firstDayTable = PutDayScheduleInWord(repo, lessonLength, weeksMarksVisible, firstDaySchedule, oDoc, oEndOfDoc, oWord, null, dayOfWeek);

            Table secondDayTable = null;

            if (dayOfWeek != 7)
            {
                var secondDaySchedule = repo.GetFacultyDOWSchedule(faculty.FacultyId, dayOfWeek+1, weekFiltered, weekFilter);
                secondDayTable = PutDayScheduleInWord(repo, lessonLength, weeksMarksVisible, secondDaySchedule, oDoc, oEndOfDoc, oWord, firstDayTable, dayOfWeek+1);
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

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
        }

        private static Table PutDayScheduleInWord(ScheduleRepository repo, int lessonLength, bool weeksMarksVisible,
            Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>> schedule, _Document oDoc, object oEndOfDoc, _Application oWord, Table table, int dayOfWeek)
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

            oTable.Cell(tableRowOffset + 1, 1).Range.Text = Constants.DOWLocal[dayOfWeek];
            oTable.Cell(tableRowOffset + 1, 1).Range.ParagraphFormat.Alignment =
                WdParagraphAlignment.wdAlignParagraphCenter;

            int groupColumn = 2;

            foreach (var group in schedule)
            {
                var groupObject = repo.GetStudentGroup(@group.Key);
                var groupName = groupObject.Name;

                oTable.Cell(tableRowOffset + 1, groupColumn).Range.Text = groupName.Replace(" (+Н)", "");
                oTable.Cell(tableRowOffset + 1, groupColumn).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                groupColumn++;     

                if (groupName.Contains(" (+Н)"))
                {
                    var plainGroupName = groupName.Replace(" (+Н)", "");
                    var nGroupName = groupName.Replace(" (+", "(");

                    var plainGroupId = repo.FindStudentGroup(plainGroupName).StudentGroupId;
                    var plainStudentIds = repo.GetAllStudentsInGroups()
                        .Where(sig => sig.StudentGroup.StudentGroupId == plainGroupId)
                        .Select(stig => stig.Student.StudentId)
                        .ToList();
                    plainGroupsListIds.Add(@group.Key, repo.GetAllStudentsInGroups()
                        .Where(sig => plainStudentIds.Contains(sig.Student.StudentId))
                        .Select(stig => stig.StudentGroup.StudentGroupId)
                        .Distinct()
                        .ToList());

                    var nGroupId = repo.FindStudentGroup(nGroupName).StudentGroupId;
                    var nStudentIds = repo.GetAllStudentsInGroups()
                        .Where(sig => sig.StudentGroup.StudentGroupId == nGroupId)
                        .Select(stig => stig.Student.StudentId)
                        .ToList();
                    nGroupsListIds.Add(@group.Key, repo.GetAllStudentsInGroups()
                        .Where(sig => nStudentIds.Contains(sig.Student.StudentId))
                        .Select(stig => stig.StudentGroup.StudentGroupId)
                        .Distinct()
                        .ToList());

                    plainNGroupIds.Add(groupObject.StudentGroupId, new Tuple<int, int>(plainGroupId, nGroupId));
                }
            }

            var timeRowIndexList = new List<int>();

            var timeRowIndex = 2;
            foreach (var time in timeList.OrderBy(t => int.Parse(t.Split(':')[0])*60 + int.Parse(t.Split(':')[1])))
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
                                    tfd => tfd.Value.Item2.Select(l => repo.CalculateWeekNumber(l.Calendar.Date)).Min()))
                        {
                            var cellText = "";
                            // Discipline name
                            cellText += tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.Name;

                            // N + Group modifiers
                            var groupId = tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;
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

                            var tfdGroupId = tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;
                            if ((tfdGroupId != @group.Key))
                            {
                                if ((!plainNGroupIds.ContainsKey(@group.Key)) ||
                                    ((tfdGroupId != plainNGroupIds[@group.Key].Item1) &&
                                     (tfdGroupId != plainNGroupIds[@group.Key].Item2)))
                                {
                                    cellText += " (" + tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.StudentGroup.Name +
                                                ")";
                                }
                            }

                            cellText += Environment.NewLine;
                            // Teacher FIO
                            cellText += tfdData.Value.Item2[0].TeacherForDiscipline.Teacher.FIO + Environment.NewLine;

                            // Total weeks
                            if (weeksMarksVisible)
                            {
                                cellText += "(" + tfdData.Value.Item1 + ")" + Environment.NewLine;
                            }

                            var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CalculateWeekNumber(l.Calendar.Date),
                                l => l.Auditorium.Name);
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
                                    cellText += ScheduleRepository.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " +
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
            bool SchoolHeader)
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

            List<Faculty> facultiesList;
            facultiesList = repo.GetAllFaculties().OrderBy(f => f.SortingOrder).ToList();

            if (facultyFilter != -1)
            {
                facultiesList = new List<Faculty>();
                facultiesList.Add(repo.GetFaculty(facultyFilter));
            }


            foreach (var faculty in facultiesList)
            {
                var facultyName = faculty.Name;

                for (int dayOfWeek = 1; dayOfWeek <= daysOfWeek; dayOfWeek++)
                {
                    string dow = Constants.DOWLocal[dayOfWeek];

                    var schedule = repo.GetFacultyDOWSchedule(faculty.FacultyId, dayOfWeek, false, -1);

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
                        Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
                        oWord.CentimetersToPoints(22f),
                        oWord.CentimetersToPoints(0.5f),
                        200, 50,
                        textBoxRange);
                    cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                        WdLineSpacing.wdLineSpaceSingle;
                    if (dow == "Понедельник")
                    {
                        if (!SchoolHeader)
                        {
                            var prorUchRabNameOption = repo.GetFirstFiltredConfigOption(co => co.Key == "Проректор по учебной работе");
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
                        var groupName = repo.GetStudentGroup(group.Key).Name;
                        oTable.Cell(1, groupColumn).Range.Text = groupName.Replace(" (+Н)", "");
                        oTable.Cell(1, groupColumn).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                        groupColumn++;

                        if (groupName.Contains(" (+Н)"))
                        {
                            var plainGroupName = groupName.Replace(" (+Н)", "");
                            var nGroupName = groupName.Replace(" (+", "(");

                            var plainGroupId = repo.FindStudentGroup(plainGroupName).StudentGroupId;
                            var plainStudentIds = repo.GetAllStudentsInGroups()
                                    .Where(sig => sig.StudentGroup.StudentGroupId == plainGroupId)
                                    .Select(stig => stig.Student.StudentId)
                                    .ToList();
                            plainGroupsListIds.Add(group.Key, repo.GetAllStudentsInGroups()
                                    .Where(sig => plainStudentIds.Contains(sig.Student.StudentId))
                                    .Select(stig => stig.StudentGroup.StudentGroupId)
                                    .Distinct()
                                    .ToList());

                            var nGroupId = repo.FindStudentGroup(nGroupName).StudentGroupId;
                            var nStudentIds = repo.GetAllStudentsInGroups()
                                    .Where(sig => sig.StudentGroup.StudentGroupId == nGroupId)
                                    .Select(stig => stig.Student.StudentId)
                                    .ToList();
                            nGroupsListIds.Add(group.Key, repo.GetAllStudentsInGroups()
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
                                foreach (var tfdData in group.Value[time].OrderBy(tfd => tfd.Value.Item2.Select(l => repo.CalculateWeekNumber(l.Calendar.Date)).Min()))
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

                                    var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
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

                    if (dayOfWeek == daysOfWeek)
                    {
                        var oPara3 =
                            oDoc.Content.Paragraphs.Add(ref oMissing);                        
                        oPara3.Range.Font.Size = 12;
                        oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                        oPara3.Range.Text = "";
                        oPara3.Format.SpaceAfter = 0;
                        oPara3.Range.InsertParagraphAfter();

                        var headUchOtdNameOption = repo.GetFirstFiltredConfigOption(co => co.Key == "Начальник учебного отдела");
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

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
        }

        public static void ExportCustomSchedule(
            // facultyId, List od DOW
            Dictionary<int, List<int>> facultyDow,
            ScheduleRepository repo,
            string filename,
            bool save,
            bool quit,
            int lessonLength,            
            int daysOfWeek,
            bool SchoolHeader)
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

            List<Faculty> facultiesList;
            facultiesList = repo.GetAllFaculties().OrderBy(f => f.SortingOrder).ToList();

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

                    string dow = Constants.DOWLocal[dayOfWeek];

                    var schedule = repo.GetFacultyDOWSchedule(faculty.FacultyId, dayOfWeek, false, -1);

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
                        Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
                        oWord.CentimetersToPoints(22f),
                        oWord.CentimetersToPoints(0.5f),
                        200, 50,
                        textBoxRange);
                    cornerStamp.TextFrame.TextRange.ParagraphFormat.LineSpacingRule =
                        WdLineSpacing.wdLineSpaceSingle;
                    if (dow == "Понедельник")
                    {
                        if (!SchoolHeader)
                        {
                            var prorUchRabNameOption = repo.GetFirstFiltredConfigOption(co => co.Key == "Проректор по учебной работе");
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
                        var groupName = repo.GetStudentGroup(group.Key).Name;
                        oTable.Cell(1, groupColumn).Range.Text = groupName.Replace(" (+Н)", "");
                        oTable.Cell(1, groupColumn).Range.ParagraphFormat.Alignment =
                            WdParagraphAlignment.wdAlignParagraphCenter;
                        groupColumn++;

                        if (groupName.Contains(" (+Н)"))
                        {
                            var plainGroupName = groupName.Replace(" (+Н)", "");
                            var nGroupName = groupName.Replace(" (+", "(");

                            var plainGroupId = repo.FindStudentGroup(plainGroupName).StudentGroupId;
                            var plainStudentIds = repo.GetAllStudentsInGroups()
                                    .Where(sig => sig.StudentGroup.StudentGroupId == plainGroupId)
                                    .Select(stig => stig.Student.StudentId)
                                    .ToList();
                            plainGroupsListIds.Add(group.Key, repo.GetAllStudentsInGroups()
                                    .Where(sig => plainStudentIds.Contains(sig.Student.StudentId))
                                    .Select(stig => stig.StudentGroup.StudentGroupId)
                                    .Distinct()
                                    .ToList());

                            var nGroupId = repo.FindStudentGroup(nGroupName).StudentGroupId;
                            var nStudentIds = repo.GetAllStudentsInGroups()
                                    .Where(sig => sig.StudentGroup.StudentGroupId == nGroupId)
                                    .Select(stig => stig.Student.StudentId)
                                    .ToList();
                            nGroupsListIds.Add(group.Key, repo.GetAllStudentsInGroups()
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
                                foreach (var tfdData in group.Value[time].OrderBy(tfd => tfd.Value.Item2.Select(l => repo.CalculateWeekNumber(l.Calendar.Date)).Min()))
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

                                    var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
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

                    if (dayOfWeek == daysOfWeek)
                    {
                        var oPara3 =
                            oDoc.Content.Paragraphs.Add(ref oMissing);
                        oPara3.Range.Font.Size = 12;
                        oPara3.Format.LineSpacing = oWord.LinesToPoints(1);
                        oPara3.Range.Text = "";
                        oPara3.Format.SpaceAfter = 0;
                        oPara3.Range.InsertParagraphAfter();

                        var headUchOtdNameOption = repo.GetFirstFiltredConfigOption(co => co.Key == "Начальник учебного отдела");
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
            
            return "второго семестра " + (ssYear - 1) + " – " + ssYear + " учебного года";
        }

        internal static void ExportTwoDaysInPageFacultySchedule(
            ScheduleRepository repo, string filename, bool save, bool quit,
            int lessonLength, int facultyId, int daysOfWeek,
            bool weekFiltered, int weekFilter, bool weeksMarksVisible)
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

            int pageCount;
            int currentPageNum = 1;

            var faculty = repo.GetFaculty(facultyId);

            for (int dayOfWeek = 1; dayOfWeek <= 5; dayOfWeek += 2)
            {
                var firstDaySchedule = repo.GetFacultyDOWSchedule(faculty.FacultyId, dayOfWeek, weekFiltered, weekFilter);

                var firstDayTable = PutDayScheduleInWord(repo, lessonLength, weeksMarksVisible, firstDaySchedule, oDoc, oEndOfDoc, oWord, null, dayOfWeek);

                Table secondDayTable = null;


                var secondDaySchedule = repo.GetFacultyDOWSchedule(faculty.FacultyId, dayOfWeek + 1, weekFiltered, weekFilter);
                secondDayTable = PutDayScheduleInWord(repo, lessonLength, weeksMarksVisible, secondDaySchedule, oDoc, oEndOfDoc, oWord, firstDayTable, dayOfWeek + 1);

                var fontSize = 10.5F;
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

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
        }

        public static void WordSchool(
            ScheduleRepository repo, string filename, bool save, bool quit, 
            int lessonLength, int facultyId, int dayOfWeek, int daysOfWeek,
            bool weekFiltered, int weekFilter, bool weeksMarksVisible)
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

            var faculty = repo.GetFaculty(facultyId);

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
            oPara1.Range.Text = Constants.DOWLocal[dayOfWeek];
            oPara1.Range.Font.Bold = 1;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Range.InsertParagraphAfter();

            Shape cornerStamp = oDoc.Shapes.AddTextbox(
                Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
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
            cornerStamp.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

            Table oTable = GetAndPutDOWSchedule(repo, lessonLength, dayOfWeek, weekFiltered, weekFilter, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, null);

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

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
        }

        public static void TeacherSchedule(List<TeacherScheduleTimeView> result, Teacher teacher, bool landscape)
        {
            var IsColumnEmpty = GetEmptyColumnIndexes(result);
            var columnTitles = new List<string>();
            var columnIndexes = new List<int>();
            for (int i = 1; i <= 7; i++)
            {
                if (!IsColumnEmpty[i])
                {
                    columnTitles.Add(Constants.DOWLocal[i]);
                    columnIndexes.Add(i);
                }
            }
            
            object oMissing = Missing.Value;
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

            var dowCount = IsColumnEmpty.Count(dow => !dow.Value);

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
                oTable.Cell(1, i+2).Range.Text = columnTitles[i];
                oTable.Cell(1, i+2).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;
            }

            for (int i = 0; i < result.Count; i++)
            {
                oTable.Cell(2+i, 1).Range.Text = result[i].Time;
                oTable.Cell(2+i, 1).Range.ParagraphFormat.Alignment =
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

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
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

        public static void AuditoriumsExport(
            ScheduleRepository repo, Dictionary<int, Dictionary<int, List<string>>> auds
            , int dow, bool addTeacherFio)
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

            Paragraph oPara1 = oDoc.Content.Paragraphs.Add();
            oPara1.Range.Text = Constants.DOWLocal[dow];
            oPara1.Range.Font.Bold = 0;
            oPara1.Range.Font.Size = 10;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.SpaceAfter = 0;
            oPara1.Range.InsertParagraphAfter();

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

            var rings = repo.GetAllRings();
            var audsById = repo.GetAllAuditoriums().ToDictionary(a => a.AuditoriumId, a => a.Name);

            audIdsList = audIdsList.OrderBy(id => audsById[id]).ToList();
            
            Table oTable = oDoc.Tables.Add(wrdRng, 1 + auds.Count, 1 + audIdsList.Count);
            oTable.Borders.Enable = 1;
            oTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
            oTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            oTable.Range.Font.Size = 10;
            oTable.Range.Font.Bold = 0;

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

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
        }

        public static void WordSchoolTwoDays(
            ScheduleRepository repo, string filename, bool save, bool quit,
            int lessonLength, int facultyId, int dayOfWeek, int daysOfWeek,
            bool weekFiltered, int weekFilter, bool weeksMarksVisible)
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

            var faculty = repo.GetFaculty(facultyId);
            
            Table oTable = null;

            oTable = GetAndPutDOWSchedule(repo, lessonLength, dayOfWeek, weekFiltered, weekFilter, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, null);
            if (dayOfWeek != 7)
            {
                oTable = GetAndPutDOWSchedule(repo, lessonLength, dayOfWeek + 1, weekFiltered, weekFilter, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, oTable);
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

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
        }

        private static Table GetAndPutDOWSchedule(
            ScheduleRepository repo, int lessonLength, int dayOfWeek, 
            bool weekFiltered, int weekFilter, bool weeksMarksVisible,
            Faculty faculty, _Document oDoc, object oEndOfDoc, _Application oWord,
            Table tableToContinue)
        {
            var schedule = repo.GetFacultyDOWSchedule(faculty.FacultyId, dayOfWeek, weekFiltered, weekFilter);

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

            Table oTable = null;
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
                float colWidth = 25.64F/schedule.Count;
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
                }
            }

            oTable.Cell(tableRowOffset + 1, 1).Range.Text = Constants.DOWLocal[dayOfWeek];
            oTable.Cell(tableRowOffset + 1, 1).Range.ParagraphFormat.Alignment =
                WdParagraphAlignment.wdAlignParagraphCenter;

            int groupColumn = 2;


            foreach (var group in schedule)
            {
                var groupObject = repo.GetStudentGroup(@group.Key);
                var groupName = groupObject.Name;
                oTable.Cell(tableRowOffset + 1, groupColumn).Range.Text = groupName;
                oTable.Cell(tableRowOffset + 1, groupColumn).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                groupColumn++;
            }

            var timeRowIndexList = new List<int>();

            var timeRowIndex = 2;
            foreach (var time in timeList.OrderBy(t => int.Parse(t.Split(':')[0])*60 + int.Parse(t.Split(':')[1])))
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

                        var groupDowTimeLessons = @group.Value[time]
                            .OrderBy(tfd => tfd.Value.Item2.Select(l =>
                                repo.CalculateWeekNumber(l.Calendar.Date)).Min())
                            .ToList();

                        if (!((@group.Value[time].Count == 2) && ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) && (groupDowTimeLessons[1].Value.Item1.Contains("чёт.")))))
                        {
                            for (int i = 0; i < @group.Value[time].Count - 1; i++)
                            {
                                timeTable.Cell(i + 1, 1).Borders[WdBorderType.wdBorderBottom].Visible = true;
                            }
                        }
                        timeTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                        timeTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        timeTable.Range.Font.Size = 10;
                        timeTable.Range.Font.Bold = 0;

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
                            var rng = oTable.Cell(tableRowOffset + timeRowIndex, columnGroupIndex).Range;
                            rng.Borders[WdBorderType.wdBorderDiagonalUp].LineStyle = WdLineStyle.wdLineStyleSingle;
                        }

                        foreach (var tfdData in groupDowTimeLessons)
                        {
                            var cellText = "";
                            // Discipline name
                            cellText += tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.Name;

                            // N + Group modifiers
                            var groupId = tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;


                            var tfdGroupId = tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;
                            if ((tfdGroupId != @group.Key))
                            {
                                cellText += " (" + tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.StudentGroup.Name +
                                            ")";
                            }

                            cellText += Environment.NewLine;
                            // Teacher FIO
                            cellText += tfdData.Value.Item2[0].TeacherForDiscipline.Teacher.FIO + Environment.NewLine;

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

                            var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CalculateWeekNumber(l.Calendar.Date),
                                l => l.Auditorium.Name);
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
                                    cellText += ScheduleRepository.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " +
                                                jItem.Key;

                                    if (j != gcount - 1)
                                    {
                                        cellText += Environment.NewLine;
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

                            timeTable.Cell(tfdIndex + 1, 1).Range.Text = cellText;
                            timeTable.Cell(tfdIndex + 1, 1).VerticalAlignment =
                                WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            if ((@group.Value[time].Count == 2) &&
                                ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) && (groupDowTimeLessons[1].Value.Item1.Contains("чёт."))))
                            {
                                timeTable.Cell(tfdIndex + 1, 1).Range.ParagraphFormat.Alignment =
                                    (tfdIndex == 0)
                                        ? WdParagraphAlignment.wdAlignParagraphLeft
                                        : WdParagraphAlignment.wdAlignParagraphRight;
                            }

                            if ((groupDowTimeLessons.Count == 1) &&
                                (groupDowTimeLessons[0].Value.Item1.Contains("(чёт.")))
                            {
                                timeTable.Cell(tfdIndex + 1, 1).Range.ParagraphFormat.Alignment = 
                                    WdParagraphAlignment.wdAlignParagraphRight;
                            }

                            tfdIndex++;
                        }
                    }

                    columnGroupIndex++;
                }

                timeRowIndex++;
            }

            return oTable;
        }

        private static Table GetAndPutDOWStartSchedule(
            ScheduleRepository repo, int lessonLength, int dayOfWeek,
            bool weekFiltered, int weekFilter, bool weeksMarksVisible,
            Faculty faculty, _Document oDoc, object oEndOfDoc, _Application oWord,
            Table tableToContinue)
        {
            var schedule = repo.GetFacultyDOWSchedule(faculty.FacultyId, dayOfWeek, weekFiltered, weekFilter);

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
            oTable.Range.Font.Size = 10;
            oTable.Range.Font.Bold = 0;

            oTable.Columns[1].Width = oWord.CentimetersToPoints(2.44f);
            float colWidth = 25.64F / schedule.Count;
            for (int i = 0; i < schedule.Count * 2; i+=2)
            {
                oTable.Columns[i + 2].Width = oWord.CentimetersToPoints(colWidth-1.1f);
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
                var groupObject = repo.GetStudentGroup(@group.Key);
                var groupName = groupObject.Name;
                oTable.Cell(1, groupColumn).Range.Text = groupName;
                oTable.Cell(1, groupColumn).Range.Font.Bold = 1;
                oTable.Cell(1, groupColumn).Range.ParagraphFormat.Alignment =
                    WdParagraphAlignment.wdAlignParagraphCenter;
                oTable.Cell(1, groupColumn).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                oTable.Cell(1, groupColumn + 1).Range.Text = "Ауд.";
                oTable.Cell(1, groupColumn + 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                groupColumn += 2;
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

                        var timeTable = oDoc.Tables.Add(oTable.Cell(timeRowIndex, columnGroupIndex).Range, 1, @group.Value[time].Count);
                        /*
                        for (int i = 0; i < @group.Value[time].Count - 1; i++)
                        {
                            timeTable.Columns.Add();
                        }
                         */

                        var groupDowTimeLessons = @group.Value[time]
                            .OrderBy(tfd => tfd.Value.Item2.Select(l =>
                                repo.CalculateWeekNumber(l.Calendar.Date)).Min())
                            .ToList();

                        if (!((@group.Value[time].Count == 2) && ((groupDowTimeLessons[0].Value.Item1.Contains("нечёт.")) && (groupDowTimeLessons[1].Value.Item1.Contains("чёт.")))))
                        {
                            for (int i = 0; i < @group.Value[time].Count - 1; i++)
                            {
                                timeTable.Cell(1, i + 1).Borders[WdBorderType.wdBorderRight].Visible = true;
                            }
                        }
                        timeTable.Range.ParagraphFormat.SpaceAfter = 0.0F;
                        timeTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        timeTable.Range.Font.Size = 10;
                        timeTable.Range.Font.Bold = 0;

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

                        foreach (var tfdData in groupDowTimeLessons)
                        {
                            var cellText = "";

                            var discName = tfdData.Value.Item2[0].TeacherForDiscipline.Discipline.Name;

                            var ShorteningDictionary = new Dictionary<string, string>();
                            ShorteningDictionary.Add("Английский язык", "Англ. яз.");
                            ShorteningDictionary.Add("Немецкий язык", "Нем. яз.");
                            ShorteningDictionary.Add("Французский язык", "Франц. яз.");

                            if (ShorteningDictionary.ContainsKey(discName))
                            {
                                discName = ShorteningDictionary[discName];
                            }
                            
                            // Discipline name
                            cellText += discName + Environment.NewLine;

                            // Teacher FIO
                            String TeacherFIO = ShortenFIO(tfdData.Value.Item2[0].TeacherForDiscipline.Teacher.FIO);
                            cellText += TeacherFIO;

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
                            var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CalculateWeekNumber(l.Calendar.Date),
                                l => l.Auditorium.Name);
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
                                    audText += ScheduleRepository.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " +
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
                                oTable.Cell(timeRowIndex, columnGroupIndex + 1).Range.Text = audCellText + " / " + audText;
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

        private static string ShortenAudName(string AudName)
        {
            return (AudName.StartsWith("Ауд. ")) ? AudName.Substring(5) : AudName;
        }

        private static string ShortenFIO(string FIO)
        {
            List<string> FioParts = FIO.Split(' ').ToList();

            if (FioParts.Count != 3)
            {
                return FIO;
            }
            else
            {
                return FioParts[0] + " " + FioParts[1].Substring(0, 1) + "." + FioParts[2].Substring(0, 1) + ".";
            }

        }

        public static void ExportSchedulePage(ScheduleRepository repo, MainEditForm form)
        {
            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */
            
            Range wrdRng;

            int pageCounter = 0;

            foreach (var faculty in repo.GetAllFaculties())            
            {
                //var faculty = repo.GetFirstFiltredFaculty(f => f.SortingOrder == i);

                var facultyGroups = repo.GetFacultyGroups(faculty.FacultyId);

                foreach (var group in facultyGroups)
                {
                    var sStarts = repo.GetSemesterStarts();

                    var groupLessons = repo.GetGroupedGroupLessons(group.StudentGroupId, sStarts);

                    List<GroupTableView> groupEvents = form.CreateGroupTableView(group.StudentGroupId, groupLessons);

                    wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

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
                    float colWidth = 25.64F / 6;
                    for (int j = 1; j <= 6; j++)
                    {
                        oTable.Columns[j + 1].Width = oWord.CentimetersToPoints(colWidth);
                        oTable.Cell(1, j + 1).Range.Text = Constants.DOWLocal[j];
                        oTable.Cell(1, j + 1).Range.ParagraphFormat.Alignment =
                                WdParagraphAlignment.wdAlignParagraphCenter;
                    }

                    for (int j = 0; j < groupEvents.Count; j++)
                    {
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

        public static void ExportGroupSchedulePage(ScheduleRepository repo, MainEditForm form, int groupId)
        {
            //Start Word and create a new document.
            _Application oWord = new Application { Visible = true };
            _Document oDoc = oWord.Documents.Add();

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            Range wrdRng;

            var group = repo.GetStudentGroup(groupId);
            
            var sStarts = repo.GetSemesterStarts();

            var groupLessons = repo.GetGroupedGroupLessons(group.StudentGroupId, sStarts);

            List<GroupTableView> groupEvents = form.CreateGroupTableView(group.StudentGroupId, groupLessons);

            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

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
            float colWidth = 25.64F / 6;
            for (int j = 1; j <= 6; j++)
            {
                oTable.Columns[j + 1].Width = oWord.CentimetersToPoints(colWidth);
                oTable.Cell(1, j + 1).Range.Text = Constants.DOWLocal[j];
                oTable.Cell(1, j + 1).Range.ParagraphFormat.Alignment =
                        WdParagraphAlignment.wdAlignParagraphCenter;
            }

            for (int j = 0; j < groupEvents.Count; j++)
            {
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

        public static void WordStartSchool(
            ScheduleRepository repo, string filename, bool save, bool quit,
            int lessonLength, int facultyId, int dayOfWeek, int daysOfWeek,
            bool weekFiltered, int weekFilter, bool weeksMarksVisible)
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

            var faculty = repo.GetFaculty(facultyId);

            Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add();
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
            oPara1.Range.Text = Constants.DOWLocal[dayOfWeek].ToUpper();
            oPara1.Range.Font.Bold = 1;
            oPara1.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
            oPara1.Range.ParagraphFormat.LineSpacingRule =
                WdLineSpacing.wdLineSpaceSingle;
            oPara1.Range.InsertParagraphAfter();

            Shape cornerStamp = oDoc.Shapes.AddTextbox(
                Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
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
            cornerStamp.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

            Table oTable = GetAndPutDOWStartSchedule(repo, lessonLength, dayOfWeek, weekFiltered, weekFilter, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, null);

            Table oTable2 = null;
            
            if ((dayOfWeek != 6) && (dayOfWeek != 7))
            {
                oPara1 = oDoc.Content.Paragraphs.Add();
                oPara1.Range.Font.Size = 14;
                oPara1.Range.Text = Constants.DOWLocal[dayOfWeek + 1].ToUpper();
                oPara1.Range.Font.Bold = 1;
                oPara1.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                oPara1.Range.ParagraphFormat.LineSpacingRule =
                    WdLineSpacing.wdLineSpaceSingle;
                oPara1.Range.InsertParagraphAfter();

                oTable2 = GetAndPutDOWStartSchedule(repo, lessonLength, dayOfWeek + 1, weekFiltered, weekFilter, weeksMarksVisible, faculty, oDoc, oEndOfDoc, oWord, null);
            }

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

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
        }
    }
}
