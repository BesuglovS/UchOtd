using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Core;

namespace UchOtd.Forms
{
    public partial class DailyLessons : Form
    {
        readonly ScheduleRepository _repo;

        private readonly TaskScheduler _uiScheduler;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public DailyLessons(ScheduleRepository repo)
        {
            InitializeComponent();

            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            _repo = repo;
        }

        private void DaylyLessons_Load(object sender, EventArgs e)
        {
            var faculties = _repo.Faculties.GetAllFaculties();

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
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }

            _tokenSource = new CancellationTokenSource();
            _cToken = _tokenSource.Token;
            
            var isfacultyFiltered = facultyFiltered.Checked;
            var facultyFilterSelectedValue = (int)facultyFilter.SelectedValue;

            bool isStudentGroupsFiltered = studentGroupsFiltered.Checked;
            List<StudentGroup> groups = null;
            if (isStudentGroupsFiltered)
            {
                groups = (
                        from object groupObject in 
                        studentGroupList.SelectedItems 
                        select groupObject as StudentGroup)
                    .ToList();
            }

            var data = new DailyLessonsData
            {
                Groups = MainGroups()
            };

            if (isfacultyFiltered)
            {
                data.Groups = _repo
                    .GroupsInFaculties
                    .GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == facultyFilterSelectedValue)
                    .Select(gif => gif.StudentGroup)
                    .ToList();
            }

            if (isStudentGroupsFiltered)
            {
                var groupIds = groups.Select(g => g.StudentGroupId).ToList();

                data.Groups = data.Groups
                    .Where(g => groupIds.Contains(g.StudentGroupId))
                    .ToList();
            }

            if (data.Groups.Count == 0)
            {
                MessageBox.Show("В итоговом списке нет ни одной группы.", "Незадача");                
                return;
            }

            loadingLabel.Visible = true;
            
            var calculateTask = Task.Factory.StartNew(() =>
            {   
                // Dictionary<StudentGroupId,Dictionary<ringId, List<Lessons>>>
                data.LessonsData = new Dictionary<int, Dictionary<int, List<Lesson>>>();
                data.Rings = new List<Ring>();

                foreach (var group in data.Groups)
                {
                    data.LessonsData.Add(group.StudentGroupId, new Dictionary<int, List<Lesson>>());

                    var localGroup = group;
                    var studentIds = _repo
                        .StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == localGroup.StudentGroupId)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);

                    var groupIds = _repo
                        .StudentsInGroups
                        .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                        .ToList()
                        .Select(stig => stig.StudentGroup.StudentGroupId);

                    var dailyLessons = _repo.Lessons.GetFiltredLessons(l =>
                        ((l.State == 1) || ((l.State == 2) && showProposed.Checked)) &&
                        l.Calendar.Date == lessonsDate.Value &&
                        groupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId));

                    foreach (var lesson in dailyLessons)
                    {
                        if (!data.LessonsData[group.StudentGroupId].ContainsKey(lesson.Ring.RingId))
                        {
                            data.LessonsData[group.StudentGroupId].Add(lesson.Ring.RingId, new List<Lesson>());
                        }
                        if (!data.Rings.Select(r => r.RingId).ToList().Contains(lesson.Ring.RingId))
                        {
                            data.Rings.Add(lesson.Ring);
                        }

                        data.LessonsData[group.StudentGroupId][lesson.Ring.RingId].Add(lesson);
                    }
                }

                data.Rings = data.Rings.OrderBy(r => r.Time.TimeOfDay).ToList();

                return data;
            }, _cToken);

            if (calculateTask.Status == TaskStatus.Canceled)
            {
                return;
            }

            calculateTask.ContinueWith(
                antecedent =>
                {
                    var lessonsData = antecedent.Result;

                    if (lessonsData == null)
                    {
                        return;
                    }

                    view.RowCount = lessonsData.Rings.Count + 1;
                    view.ColumnCount = lessonsData.LessonsData.Count + 1;

                    int columnIndex1 = 1;
                    foreach (var group in lessonsData.LessonsData)
                    {
                        view.Rows[0].Cells[columnIndex1].Value = _repo.StudentGroups.GetStudentGroup(group.Key).Name;

                        columnIndex1++;
                    }

                    int rowIndex = 1;
                    foreach (var ring in lessonsData.Rings)
                    {
                        view.Rows[rowIndex].Cells[0].Value = lessonsData.Rings[rowIndex - 1].Time.ToString("H:mm");

                        //foreach (var group in result[ring.RingId])
                        for (int columnIndex = 1; columnIndex < view.Columns.Count; columnIndex++)
                        {
                            var group = _repo.StudentGroups.GetStudentGroup(lessonsData.Groups[columnIndex - 1].StudentGroupId);

                            if (lessonsData.LessonsData[group.StudentGroupId].ContainsKey(ring.RingId))
                            {
                                var lesson = lessonsData.LessonsData[group.StudentGroupId][ring.RingId][0];

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
                _cToken,
                TaskContinuationOptions.None,
                _uiScheduler
            );
            
            
        }

        private List<StudentGroup> MainGroups()
        {
            return _repo
                .StudentGroups.GetFiltredStudentGroups(sg =>
                    !(sg.Name.Contains("-") || sg.Name.Contains("+") || sg.Name.Contains("I") ||
                    sg.Name.Length == 1 || sg.Name.Contains("(Н)") || sg.Name.Contains(".")))
                .OrderBy(sg => sg.Name)
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
