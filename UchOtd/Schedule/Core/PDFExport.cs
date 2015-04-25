using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using Schedule.Constants;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Repositories.Common;

namespace UchOtd.Schedule.Core
{
    public static class PdfExport
    {
        public static void ExportSchedulePage(
            Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>> schedule,
            string facultyName,
            string filename,
            string dow,
            ScheduleRepository repo,
            bool save,
            bool quit,
            bool print)
        {
            double scheduleFontsize = 10;
            int pageCount;
            Document document;
            do
            {
                // Create a new PDF document
                document = CreateDocument(repo, facultyName, dow, schedule, scheduleFontsize);

                // Create a renderer and prepare (=layout) the document
                var docRenderer = new DocumentRenderer(document);
                docRenderer.PrepareDocument();
                pageCount = docRenderer.FormattedDocument.PageCount;

                scheduleFontsize -= 0.5;
            } while (pageCount > 1);

            // Render the file
            var pdfRenderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always) {Document = document};
            pdfRenderer.RenderDocument();

            // Save the document...                
            if (save)
            {
                pdfRenderer.PdfDocument.Save(filename);
            }
            // ...and start a viewer.
            //Process.Start(filename);

            if (print)
            {
                if (!save)
                {
                    pdfRenderer.PdfDocument.Save(filename);
                }

                SendToPrinter(filename);
            }
        }

        private static void SendToPrinter(String filename)
        {
            var info = new ProcessStartInfo
            {
                Verb = "print",
                FileName = filename,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            var p = new Process {StartInfo = info};
            p.Start();

            p.WaitForInputIdle();
            Thread.Sleep(3000);
            if (false == p.CloseMainWindow())
                p.Kill();
        }


        private static Document CreateDocument(ScheduleRepository repo, string facultyName, string dow, 
            Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>> schedule,
            double scheduleFontsize)
        {
            var result = new Document();
            Section section = result.AddSection();

            SetPageLayout(result);

            if (dow == "Понедельник")
            {
                SetCornerStamp(section);
            }

            SetHeaderText(section, repo, facultyName, dow);

            PutScheduleTable(repo, section, schedule, scheduleFontsize);

            return result;
        }

        private static void SetPageLayout(Document result)
        {
            result.DefaultPageSetup.PageFormat = PageFormat.A4;

            result.DefaultPageSetup.Orientation = Orientation.Landscape;

            var cm1 = Unit.FromCentimeter(1);
            result.DefaultPageSetup.TopMargin = cm1;
            result.DefaultPageSetup.RightMargin = cm1;
            result.DefaultPageSetup.BottomMargin = cm1;
            result.DefaultPageSetup.LeftMargin = cm1;
        }

        private static void SetCornerStamp(Section section)
        {
            // Create the text frame for the address
            var addressFrame = section.AddTextFrame();
            addressFrame.Height = "2.0cm";
            addressFrame.Width = "5.0cm";

            addressFrame.Left = Unit.FromCentimeter(23);
            addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;

            addressFrame.Top = "0.5cm";
            addressFrame.RelativeVertical = RelativeVertical.Page;

            var paragraph = addressFrame.AddParagraph("«УТВЕРЖДАЮ»");
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.SpaceAfter = 3;

            paragraph = addressFrame.AddParagraph("Проректор по учебной работе");
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.SpaceAfter = 3;

            paragraph = addressFrame.AddParagraph("______________     А.В.Синицкий");
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.SpaceAfter = 3;
        }

        private static void SetHeaderText(Section section, ScheduleRepository repo, string facultyName, string dow)
        {
            // Create the text frame for the address
            var addressFrame = section.AddTextFrame();
            addressFrame.Height = "3.0cm";
            addressFrame.Width = "7.0cm";

            addressFrame.Left = Unit.FromCentimeter(10);
            addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;

            addressFrame.Top = "0.5cm";
            addressFrame.RelativeVertical = RelativeVertical.Page;

            var paragraph = addressFrame.AddParagraph("Расписание");
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Size = 10;
            paragraph.Format.SpaceAfter = 3;

            var semesterString = DetectSemesterString(repo);
            paragraph = addressFrame.AddParagraph(semesterString);
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Size = 10;
            paragraph.Format.SpaceAfter = 3;

            paragraph = addressFrame.AddParagraph(facultyName);
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Size = 10;
            paragraph.Format.SpaceAfter = 3;

            paragraph = addressFrame.AddParagraph(dow.ToUpper());
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Size = 14;
            paragraph.Format.SpaceAfter = 3;
        }

        private static string DetectSemesterString(ScheduleRepository repo)
        {
            var semesterSterts = repo.CommonFunctions.GetSemesterStarts();
            var ssYear = semesterSterts.Year;

            if (semesterSterts.Month > 6)
            {
                return "первого семестра " + ssYear + " – " + (ssYear+1) + " учебного года";
            }

            return "второго семестра " + (ssYear-1) + " – " + ssYear + " учебного года";
        }        

        private static void PutScheduleTable(ScheduleRepository repo, Section section,
            Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>> schedule,
            double scheduleFontsize)
        {
            var paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "1.25cm";

            var table = section.AddTable();
            table.Style = "Table";
            table.Borders.Color = new Color(0, 0, 0);
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            Column column = table.AddColumn("3.7cm");
            column.Format.Alignment = ParagraphAlignment.Center;

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

            string colWidth = (24.0 / schedule.Count).ToString("F1").Replace(',','.');
            for (int i = 0; i < schedule.Count; i++)
            {
                column = table.AddColumn(colWidth + "cm");
                column.Format.Alignment = ParagraphAlignment.Right;
            }

            int groupColumn = 1;

            var plainGroupsListIds = new Dictionary<int, List<int>>();
            var nGroupsListIds = new Dictionary<int, List<int>>();

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;

            row.Cells[0].AddParagraph("Время");
            
            foreach (var group in schedule)
            {
                var groupName = repo.StudentGroups.GetStudentGroup(group.Key).Name;
                row.Cells[groupColumn].AddParagraph(groupName.Replace(" (+Н)", ""));
                
                groupColumn++;

                if (groupName.Contains(" (+Н)"))
                {
                    var plainGroupName = groupName.Replace(" (+Н)", "");
                    var nGroupName = groupName.Replace(" (+", "(");

                    var plainGroupId = repo.StudentGroups.FindStudentGroup(plainGroupName).StudentGroupId;
                    var plainStudentIds = repo
                            .StudentsInGroups
                            .GetAllStudentsInGroups()
                            .Where(sig => sig.StudentGroup.StudentGroupId == plainGroupId)
                            .Select(stig => stig.Student.StudentId)
                            .ToList();
                    plainGroupsListIds.Add(group.Key, repo
                            .StudentsInGroups
                            .GetAllStudentsInGroups()
                            .Where(sig => plainStudentIds.Contains(sig.Student.StudentId))
                            .Select(stig => stig.StudentGroup.StudentGroupId)
                            .Distinct()
                            .ToList());

                    var nGroupId = repo.StudentGroups.FindStudentGroup(nGroupName).StudentGroupId;
                    var nStudentIds = repo
                            .StudentsInGroups
                            .GetAllStudentsInGroups()
                            .Where(sig => sig.StudentGroup.StudentGroupId == nGroupId)
                            .Select(stig => stig.Student.StudentId)
                            .ToList();
                    nGroupsListIds.Add(group.Key, repo
                            .StudentsInGroups
                            .GetAllStudentsInGroups()
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
                var timeRow = table.AddRow();
                timeRow.VerticalAlignment = VerticalAlignment.Center;

                var hour = int.Parse(time.Substring(0, 2));
                var minute = int.Parse(time.Substring(3, 2));

                minute += 80;

                while (minute >= 60)
                {
                    hour++;
                    minute -= 60;
                }


                timeRowIndexList.Add(timeRowIndex);

                timeRow.Cells[0].Format.Alignment = ParagraphAlignment.Center;
                timeRow.Cells[0].AddParagraph(time + " - " + hour.ToString("D2") + ":" + minute.ToString("D2"));
                

                var columnGroupIndex = 1;
                foreach (var group in schedule)
                {
                    if (group.Value.ContainsKey(time))
                    {
                        var cellTable = timeRow.Cells[columnGroupIndex].Elements.AddTable();
                        cellTable.AddColumn(table.Columns[columnGroupIndex].Width.Centimeter + "cm");
                        cellTable.Borders.Width = 0;

                        foreach (var tfdData in group.Value[time].OrderBy(tfd => tfd.Value.Item2.Select(l => repo.CommonFunctions.CalculateWeekNumber(l.Calendar.Date)).Min()))
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

                            var audWeekList = tfdData.Value.Item2.ToDictionary(l => repo.CommonFunctions.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
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

                            Row cellTableRow = cellTable.AddRow();

                            cellTableRow.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                            cellTableRow.Cells[0].Format.Font.Size = scheduleFontsize;
                            cellTableRow.Cells[0].AddParagraph(cellText);
                        }
                    }
                    

                    columnGroupIndex++;
                }

                timeRowIndex++;
            }            
        }

        public static void ExportWholeSchedule(
            string filename, 
            ScheduleRepository repo,
            bool save,
            bool quit,
            bool print)
        {
            throw new NotImplementedException();
        }

        public static void PrintWholeSchedule(ScheduleRepository repo)
        {
            
            /*foreach (var faculty in _repo.GetAllFaculties().OrderBy(f => f.SortingOrder))
            {*/
                //var facultyId = faculty.FacultyId;
                var facultyId = repo.Faculties.GetFirstFiltredFaculty(f => f.Letter == "Т").FacultyId;
                var facultyName = repo.Faculties.GetFaculty(facultyId).Name;
                
                
                for (int i = 1; i <= 6; i++)
                {
                    //var i = 4;
                    var facultyDowLessons = repo.Lessons.GetFacultyDowSchedule(facultyId, i, false, -1, false, false);
                    ExportSchedulePage(facultyDowLessons, facultyName, "Export.pdf", Constants.DowLocal[i], repo, false, false, true);
                }
            //}
        }
    }
}
