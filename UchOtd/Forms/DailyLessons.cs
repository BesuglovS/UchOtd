using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UchOtd.Core;

namespace UchOtd.Forms
{
    public partial class DailyLessons : Form
    {
        readonly ScheduleRepository repo;

        private readonly TaskScheduler _uiScheduler;

        CancellationTokenSource tokenSource;
        CancellationToken cToken;

        public DailyLessons(ScheduleRepository repo)
        {
            InitializeComponent();

            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            this.repo = repo;
        }

        private void DaylyLessons_Load(object sender, EventArgs e)
        {
            var faculties = repo.GetAllFaculties();

            facultyFilter.ValueMember = "FacultyId";
            facultyFilter.DisplayMember = "Name";
            facultyFilter.DataSource = faculties;

            var groups = MainGroups();
                        
            studentGroupList.DisplayMember = "Name";
            studentGroupList.DataSource = groups;

            lessonsDate.Value = DateTime.Now.Date.Date;
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            if (tokenSource != null)
            {
                tokenSource.Cancel();
            }

            tokenSource = new CancellationTokenSource();
            cToken = tokenSource.Token;
            
            bool isfacultyFiltered = facultyFiltered.Checked;
            int facultyFilterSelectedValue = (int)facultyFilter.SelectedValue;

            bool isStudentGroupsFiltered = studentGroupsFiltered.Checked;
            List<StudentGroup> groups = null;
            if (isStudentGroupsFiltered)
            {
                groups = new List<StudentGroup>();
                foreach (Object groupObject in studentGroupList.SelectedItems)
                {
                    StudentGroup group = groupObject as StudentGroup;
                    groups.Add(group);
                }
            }

            var data = new DailyLessonsData();

            data.groups = MainGroups();

            if (isfacultyFiltered)
            {
                data.groups = repo
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == facultyFilterSelectedValue)
                    .Select(gif => gif.StudentGroup)
                    .ToList();
            }

            if (isStudentGroupsFiltered)
            {
                var groupIds = groups.Select(g => g.StudentGroupId).ToList();

                data.groups = data.groups
                    .Where(g => groupIds.Contains(g.StudentGroupId))
                    .ToList();
            }

            if (data.groups.Count == 0)
            {
                MessageBox.Show("В итоговом списке нет ни одной группы.", "Незадача");                
                return;
            }

            loadingLabel.Visible = true;
            
            var calculateTask = Task.Factory.StartNew(() =>
            {   
                // Dictionary<StudentGroupId,Dictionary<ringId, List<Lessons>>>
                data.lessonsData = new Dictionary<int, Dictionary<int, List<Lesson>>>();
                data.rings = new List<Ring>();

                foreach (var group in data.groups)
                {
                    data.lessonsData.Add(group.StudentGroupId, new Dictionary<int, List<Lesson>>());

                    var studentIds = repo
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);

                    var groupIds = repo
                        .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                        .ToList()
                        .Select(stig => stig.StudentGroup.StudentGroupId);

                    var dailyLessons = repo.GetFiltredRealLessons(l =>
                        l.IsActive &&
                        l.Calendar.Date == lessonsDate.Value &&
                        groupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId));

                    foreach (var lesson in dailyLessons)
                    {
                        if (!data.lessonsData[group.StudentGroupId].ContainsKey(lesson.Ring.RingId))
                        {
                            data.lessonsData[group.StudentGroupId].Add(lesson.Ring.RingId, new List<Lesson>());
                        }
                        if (!data.rings.Select(r => r.RingId).ToList().Contains(lesson.Ring.RingId))
                        {
                            data.rings.Add(lesson.Ring);
                        }

                        data.lessonsData[group.StudentGroupId][lesson.Ring.RingId].Add(lesson);
                    }
                }

                data.rings = data.rings.OrderBy(r => r.Time.TimeOfDay).ToList();

                return data;
            }, cToken);

            if (calculateTask.Status == TaskStatus.Canceled)
            {
                return;
            }

            calculateTask.ContinueWith(
                antecedent =>
                {
                    var LessonsData = antecedent.Result;

                    if (LessonsData == null)
                    {
                        return;
                    }

                    view.RowCount = LessonsData.rings.Count + 1;
                    view.ColumnCount = LessonsData.lessonsData.Count + 1;

                    int columnIndex1 = 1;
                    foreach (var group in LessonsData.lessonsData)
                    {
                        view.Rows[0].Cells[columnIndex1].Value = repo.GetStudentGroup(group.Key).Name;

                        columnIndex1++;
                    }

                    int rowIndex = 1;
                    foreach (var ring in LessonsData.rings)
                    {
                        view.Rows[rowIndex].Cells[0].Value = LessonsData.rings[rowIndex - 1].Time.ToString("H:mm");

                        //foreach (var group in result[ring.RingId])
                        for (int columnIndex = 1; columnIndex < view.Columns.Count; columnIndex++)
                        {
                            var group = repo.GetStudentGroup(LessonsData.groups[columnIndex - 1].StudentGroupId);

                            if (LessonsData.lessonsData[group.StudentGroupId].ContainsKey(ring.RingId))
                            {
                                var lesson = LessonsData.lessonsData[group.StudentGroupId][ring.RingId][0];

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

                    loadingLabel.Visible = false;
                },
                cToken,
                TaskContinuationOptions.None,
                _uiScheduler
            );
            
            
        }

        private List<StudentGroup> MainGroups()
        {
            return repo
                                .GetFiltredStudentGroups(sg =>
                                    !(sg.Name.Contains("-") || sg.Name.Contains("+") || sg.Name.Contains("I") ||
                                      sg.Name.Length == 1 || sg.Name.Contains("(Н)") || sg.Name.Contains(".")))
                                .ToList();
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
                if (view.Columns.Count > 1)
                {
                    var columnWidth = (view.Width - 80) / (view.Columns.Count - 1);
                    for (int i = 1; i <= view.Columns.Count - 1; i++)
                    {
                        view.Columns[i].Width = columnWidth;
                    }
                }

                view.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            }
        }

        private void DailyLessons_ResizeEnd(object sender, EventArgs e)
        {
            ResizeColumns();
        }

        private void view_SelectionChanged(object sender, EventArgs e)
        {
            view.ClearSelection();
        }
    }
}
