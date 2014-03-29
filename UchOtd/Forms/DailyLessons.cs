using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UchOtd.Forms
{
    public partial class DailyLessons : Form
    {
        readonly ScheduleRepository repo;

        public DailyLessons(ScheduleRepository repo)
        {
            InitializeComponent();

            this.repo = repo;
        }

        private void DaylyLessons_Load(object sender, EventArgs e)
        {
            var faculties = repo.GetAllFaculties();

            facultyFilter.ValueMember = "FacultyId";
            facultyFilter.DisplayMember = "Name";
            facultyFilter.DataSource = faculties;

            lessonsDate.Value = DateTime.Now.Date.Date;
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            var groups = repo
                .GetFiltredStudentGroups(sg => 
                    !(sg.Name.Contains("-") || sg.Name.Contains("+") || sg.Name.Contains("I") || 
                      sg.Name.Length == 1 || sg.Name.Contains("(Н)") || sg.Name.Contains(".")))
                .ToList();

            if (facultyFiltered.Checked)
            {
                groups = repo
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == (int)facultyFilter.SelectedValue)
                    .Select(gif => gif.StudentGroup)
                    .ToList();               
            }

            // Dictionary<StudentGroupId,Dictionary<ringId, List<Lessons>>>
            var result = new Dictionary<int, Dictionary<int, List<Lesson>>>();
            var rings = new List<Ring>();

            foreach (var group in groups)
            {
                result.Add(group.StudentGroupId, new Dictionary<int, List<Lesson>>());

                var studentIds = repo
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                    .ToList()
                    .Select(stig => stig.Student.StudentId);

                var groupIds = repo
                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .ToList()
                    .Select(stig => stig.StudentGroup.StudentGroupId);

                var dailyLessons = repo.GetFiltredLessons(l => 
                    l.IsActive &&
                    l.Calendar.Date == lessonsDate.Value &&
                    groupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId));

                foreach (var lesson in dailyLessons)
                {
                    if (!result[group.StudentGroupId].ContainsKey(lesson.Ring.RingId))
                    {
                        result[group.StudentGroupId].Add(lesson.Ring.RingId, new List<Lesson>());
                    }
                    if (!rings.Select(r => r.RingId).ToList().Contains(lesson.Ring.RingId))
                    {
                        rings.Add(lesson.Ring);
                    }

                    result[group.StudentGroupId][lesson.Ring.RingId].Add(lesson);
                }
            }

            rings = rings.OrderBy(r => r.Time.TimeOfDay).ToList();

            view.RowCount = rings.Count + 1;
            view.ColumnCount = result.Count + 1;

            int columnIndex1 = 1;
            foreach (var group in result)
            {
                view.Rows[0].Cells[columnIndex1].Value = repo.GetStudentGroup(group.Key).Name;

                columnIndex1++;
            }

            int rowIndex = 1;
            foreach (var ring in rings)
            {
                view.Rows[rowIndex].Cells[0].Value = rings[rowIndex-1].Time.ToString("H:mm");

                //foreach (var group in result[ring.RingId])
                for(int columnIndex = 1; columnIndex < view.Columns.Count; columnIndex++)
                {
                    var group = repo.GetStudentGroup(groups[columnIndex - 1].StudentGroupId);

                    if (result[group.StudentGroupId].ContainsKey(ring.RingId))
                    {
                        var lesson = result[group.StudentGroupId][ring.RingId][0];

                        bool groupsNotEqual = lesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId != group.StudentGroupId;

                        view.Rows[rowIndex].Cells[columnIndex].Value = LessonToString(lesson, groupsNotEqual);
                    }
                    else
                    {
                        view.Rows[rowIndex].Cells[columnIndex].Value = "";
                    }

                }

                rowIndex++;
            }            

            view.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            ResizeColumns();
            
        }

        private string LessonToString(Lesson lesson, bool groupsNotEqual)
        {
            return (groupsNotEqual ? lesson.TeacherForDiscipline.Discipline.StudentGroup.Name + Environment.NewLine : "") +
                lesson.TeacherForDiscipline.Discipline.Name + Environment.NewLine +
                lesson.TeacherForDiscipline.Teacher.FIO + Environment.NewLine + 
                lesson.Auditorium.Name;
        }

        private void ResizeColumns()
        {
            if (view.Columns.Count != 0)
            {
                view.Columns[0].Width = 75;
                var columnWidth = (view.Width - 80) / (view.Columns.Count - 1);
                for (int i = 1; i <= view.Columns.Count - 1; i++)
                {
                    view.Columns[i].Width = columnWidth;
                }

                view.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            }
        }

        private void DailyLessons_ResizeEnd(object sender, EventArgs e)
        {
            ResizeColumns();
        }
    }
}
