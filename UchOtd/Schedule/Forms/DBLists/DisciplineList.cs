using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Constants;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Properties;
using UchOtd.Schedule.Forms.DBLists.Lessons;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class DisciplineList : Form
    {
        private readonly ScheduleRepository _repo;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public DisciplineList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void DisciplineListLoad(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();

            Attestation.Items.Clear();
            foreach (var attestationForm in Constants.Attestation)
            {
                Attestation.Items.Add(attestationForm.Value);
            }


            var groups = _repo.StudentGroups.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            Group.ValueMember = "StudentGroupId";
            Group.DisplayMember = "Name";
            Group.DataSource = groups;

            var groups2 = _repo.StudentGroups.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            groupNameList.ValueMember = "StudentGroupId";
            groupNameList.DisplayMember = "Name";
            groupNameList.DataSource = groups2;


            checkForDoubleDiscsOnAdding.Text = "Проверять дубликаты дисциплин\r\nпри добавлении";

            //RefreshView();
        }

        private async void RefreshView()
        {
            List<DisciplineView> discView = null;
                       

            if (refresh.Text == "Обновить")
            {
                _cToken = _tokenSource.Token;

                refresh.Text = "";
                refresh.Image = Resources.Loading;

                var filterText = filter.Text;
                var discNameF = discnameFilter.Checked;
                var groupNameF = groupnameFilter.Checked;
                var hourFitF = HoursFitFiltered.Checked;
                var mixedGroupsF = mixedGroups.Checked;
                var groupId = (int)groupNameList.SelectedValue;
                var noCultureF = noCulture.Checked;
                var withExamsOnlyF = WithExamsOnly.Checked;
                var orderbyGroupNameF = orderByGroupname.Checked;
                var noArtF = noArt.Checked;
                var noPostF = noPost.Checked;

                try
                {
                    discView = await Task.Run(() => {
                        List<Discipline> discList = null;

                        if ((filterText != "") && discnameFilter.Checked)
                        {
                            discList = _repo.Disciplines.GetFiltredDisciplines(disc => disc.Name.Contains(filter.Text));
                        }
                        else
                        {
                            discList = _repo.Disciplines.GetAllDisciplines();
                        }

                        if (groupNameF)
                        {
                            var studentIds = _repo
                                .StudentsInGroups
                                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId)
                                .Select(stig => stig.Student.StudentId)
                                .ToList();
                            var groupsListIds = _repo
                                .StudentsInGroups
                                .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                                .Select(stig => stig.StudentGroup.StudentGroupId)
                                .Distinct()
                                .ToList();

                            discList = discList
                                .Where(d => groupsListIds.Contains(d.StudentGroup.StudentGroupId))
                                .ToList();
                        }

                        if (HoursFitFiltered.Checked)
                        {
                            var discListFiltered = new List<Discipline>();

                            foreach (var disc in discList)
                            {
                                var localDisc = disc;
                                var tfd = _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(tefd =>
                                    tefd.Discipline.DisciplineId == localDisc.DisciplineId);

                                var diffList = new List<int> { 0 };
                                if (DifferenceByOne.Checked)
                                {
                                    diffList.Add(-1);
                                }

                                if ((tfd != null) && (!diffList.Contains(disc.AuditoriumHours - _repo.CommonFunctions.GetTfdHours(tfd.TeacherForDisciplineId))))
                                {
                                    discListFiltered.Add(disc);
                                }
                            }

                            discList = discListFiltered;
                        }

                        if (mixedGroups.Checked)
                        {
                            discList = discList.Where(disc => disc.StudentGroup.Name.Contains(" + ")).ToList();
                        }

                        if (noCultureF)
                        {
                            discList = discList.Where(d => !d.Name.ToLower().Contains("физическая культура")).ToList();
                        }

                        if (withExamsOnlyF)
                        {
                            discList = discList.Where(d => (d.Attestation == 2) || (d.Attestation == 3)).ToList();
                        }

                        if (noArtF)
                        {
                            discList = discList.Where(d => !d.StudentGroup.Name.Contains(" И")).ToList();
                        }

                        if (noPostF)
                        {
                            discList =
                                discList.Where(
                                    d =>
                                        !(d.StudentGroup.Name.StartsWith("1 ") || 
                                          d.StudentGroup.Name.StartsWith("2 ") ||
                                          d.StudentGroup.Name.StartsWith("3 "))).ToList();
                        }

                        discList = orderbyGroupNameF ?
                            discList.OrderBy(disc => disc.StudentGroup.Name).ToList() :
                            discList.OrderBy(disc => disc.Name).ToList();

                        return DisciplineView.DisciplinesToView(_repo, discList).OrderBy(v => v.TeacherFio).ToList();
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

            Text = "Дисциплины - " + ((discView != null) ? discView.Count().ToString() : "");

            refresh.Image = null;
            refresh.Text = "Обновить";
            
            DiscipineListView.DataSource = discView;

            FormatView();

            DiscipineListView.ClearSelection();
        }

        private void FormatView()
        {
            DiscipineListView.Columns["DisciplineId"].Visible = false;
            DiscipineListView.Columns["DisciplineId"].Width = 40;

            DiscipineListView.Columns["Name"].Width = 270;
            DiscipineListView.Columns["Name"].HeaderText = "Наименование дисциплины";

            DiscipineListView.Columns["TeacherFio"].Width = 80;
            DiscipineListView.Columns["TeacherFio"].HeaderText = "ФИО преподавателя";

            DiscipineListView.Columns["ScheduleHours"].Width = 30;
            DiscipineListView.Columns["ScheduleHours"].HeaderText = "Часов в расписании";

            DiscipineListView.Columns["Attestation"].Width = 80;
            DiscipineListView.Columns["Attestation"].HeaderText = "Форма отчётности";

            DiscipineListView.Columns["AuditoriumHours"].Width = 80;
            DiscipineListView.Columns["AuditoriumHours"].HeaderText = "Аудиторные часы";

            DiscipineListView.Columns["ProposedHours"].Width = 80;
            DiscipineListView.Columns["ProposedHours"].HeaderText = "Неутверждённые часы";

            DiscipineListView.Columns["AuditoriumHoursPerWeek"].Width = 80;
            DiscipineListView.Columns["AuditoriumHoursPerWeek"].HeaderText = "Аудиторные часы (в неделю / ШКОЛА)";

            DiscipineListView.Columns["LectureHours"].Width = 80;
            DiscipineListView.Columns["LectureHours"].HeaderText = "Лекции";

            DiscipineListView.Columns["PracticalHours"].Width = 80;
            DiscipineListView.Columns["PracticalHours"].HeaderText = "Практические / Семинары";

            DiscipineListView.Columns["StudentGroupName"].Width = 120;
            DiscipineListView.Columns["StudentGroupName"].HeaderText = "Группа";
        }

        private void DiscipineListViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[e.RowIndex];
            var discipline = _repo.Disciplines.GetDiscipline(discView.DisciplineId);

            DisciplineName.Text = discipline.Name;
            Attestation.SelectedIndex = discipline.Attestation;
            AuditoriumHours.Text = discipline.AuditoriumHours.ToString(CultureInfo.InvariantCulture);
            AuditoriumHoursPerWeek.Text = discipline.AuditoriumHoursPerWeek.ToString(CultureInfo.InvariantCulture);
            LectureHours.Text = discipline.LectureHours.ToString(CultureInfo.InvariantCulture);
            PracticalHours.Text = discipline.PracticalHours.ToString(CultureInfo.InvariantCulture);

            Group.SelectedValue = discipline.StudentGroup.StudentGroupId;
        }

        private void AddClick(object sender, EventArgs e)
        {
            int audHours;
            int.TryParse(AuditoriumHours.Text, out audHours);
            int audHoursPerWeek;
            int.TryParse(AuditoriumHoursPerWeek.Text, out audHoursPerWeek);
            int lecHours;
            int.TryParse(LectureHours.Text, out lecHours);
            int practHours;
            int.TryParse(PracticalHours.Text, out practHours);
            
            if ((checkForDoubleDiscsOnAdding.Checked) && 
                (_repo.Disciplines.FindDiscipline(DisciplineName.Text, Attestation.SelectedIndex,
                audHours, lecHours, practHours, Group.Text) != null))
            {
                var dialogResult = MessageBox.Show("Такая дисциплина уже есть", "Всё равно добавить?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //do something, in this case nothing
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            if (Attestation.SelectedIndex == -1)
            {
                Attestation.SelectedIndex = Constants.Attestation.FirstOrDefault(a => a.Value == "-").Key;
            }

            var disciplineGroup = _repo.StudentGroups.FindStudentGroup(Group.Text);
            if (disciplineGroup == null)
            {
                disciplineGroup = new StudentGroup { Name = Group.Text };
                _repo.StudentGroups.AddStudentGroup(disciplineGroup);
            }

            var newDiscipline = new Discipline
            {
                Attestation = Attestation.SelectedIndex,
                AuditoriumHours = audHours,
                AuditoriumHoursPerWeek = audHoursPerWeek,
                LectureHours = lecHours,
                PracticalHours = practHours,
                Name = DisciplineName.Text,
                StudentGroup = disciplineGroup
            };

            _repo.Disciplines.AddDiscipline(newDiscipline);

            RefreshView();
        }

        private void UpdateClick(object sender, EventArgs e)
        {
            if (DiscipineListView.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[DiscipineListView.SelectedCells[0].RowIndex];
                var discipline = _repo.Disciplines.GetDiscipline(discView.DisciplineId);

                discipline.Name = DisciplineName.Text;
                discipline.Attestation = Attestation.SelectedIndex;
                discipline.AuditoriumHours = int.Parse(AuditoriumHours.Text);
                discipline.AuditoriumHoursPerWeek = int.Parse(AuditoriumHoursPerWeek.Text);
                discipline.LectureHours = int.Parse(LectureHours.Text);
                discipline.PracticalHours = int.Parse(PracticalHours.Text);

                discipline.StudentGroup = _repo.StudentGroups.GetStudentGroup((int)Group.SelectedValue);

                _repo.Disciplines.UpdateDiscipline(discipline);

                RefreshView();
            }
        }

        private void RemoveClick(object sender, EventArgs e)
        {
            if (DiscipineListView.SelectedRows.Count > 1)
            {
                var discIds = new List<int>();
                for (int i = 0; i < DiscipineListView.SelectedRows.Count; i++)
                {
                    discIds.Add(((List<DisciplineView>)DiscipineListView.DataSource)[DiscipineListView.SelectedRows[i].Index].DisciplineId);
                }

                foreach (var id in discIds)
                {
                    _repo.Disciplines.RemoveDiscipline(id);
                }

                RefreshView();
                return;
            }

            if (DiscipineListView.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[DiscipineListView.SelectedCells[0].RowIndex];

                if (_repo.TeacherForDisciplines.GetFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discView.DisciplineId).Count > 0)
                {
                    MessageBox.Show("Эта дисциплина определена преподавателю.");
                    return;
                }

                _repo.Disciplines.RemoveDiscipline(discView.DisciplineId);

                RefreshView();
            }
        }

        private void PasteClick(object sender, EventArgs e)
        {
            DisciplineName.Text = Clipboard.GetText();
        }

        private void HoursEnter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void AuditoriumHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                add.PerformClick();
            }
        }

        private void DisciplineName_Leave(object sender, EventArgs e)
        {
            if (DisciplineName.Text.Length > 0)
            {
                DisciplineName.Text = DisciplineName.Text.Substring(0, 1).ToUpper() + DisciplineName.Text.Substring(1);
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void DiscipineListView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var discId = ((List<DisciplineView>)DiscipineListView.DataSource)[e.RowIndex].DisciplineId;
            var tefd = _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discId);
            if (tefd != null)
            {
                var addLessonForm = new AddLesson(_repo, tefd.TeacherForDisciplineId);
                addLessonForm.Show();               
            }
            else
            {
                var addLessonForm = new AddLesson(_repo);
                addLessonForm.Show();
            }
        }

        private void DiscipineListView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[e.RowIndex];

                e.CellStyle.BackColor = PickPercentColor(discView.AuditoriumHours, discView.ScheduleHours);
            }
        }

        private Color PickPercentColor(int auditoriumHours, int scheduleHours)
        {
            if (scheduleHours > auditoriumHours+1)
            {
                return Color.FromArgb(255, 0, 255);
            }

            if (scheduleHours == auditoriumHours+1)
            {
                return Color.FromArgb(200, 255, 0);
            }
            
            if (scheduleHours == auditoriumHours)
            {
                return Color.FromArgb(0, 255, 0);
            }

            if (scheduleHours >= auditoriumHours * 0.9)
            {
                return Color.FromArgb(255, 255, 0);
            }
            
            if (scheduleHours >= auditoriumHours * 0.5)
            {
                return Color.FromArgb(255, 128, 0);
            }

            return Color.FromArgb(255, 0, 0);
        }

        private void CompletelyDelete_Click(object sender, EventArgs e)
        {
            if (DiscipineListView.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[DiscipineListView.SelectedCells[0].RowIndex];
                
                var tfd = _repo.TeacherForDisciplines
                    .GetFirstFiltredTeacherForDiscipline(tefd => 
                        tefd.Discipline.DisciplineId == discView.DisciplineId);

                if (tfd == null)
                {
                    MessageBox.Show("Дисциплина не назначена преподавателю.");
                    return;
                }

                var lessonIds = _repo
                    .Lessons
                    .GetFiltredLessons(l => l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId)
                    .Select(l => l.LessonId);

                var logEventIds = _repo
                    .LessonLogEvents
                    .GetFiltredLessonLogEvents(lle =>
                    ((lle.OldLesson != null) && (lessonIds.Contains(lle.OldLesson.LessonId))) ||
                    ((lle.NewLesson != null) && (lessonIds.Contains(lle.NewLesson.LessonId))))
                    .Select(lle => lle.LessonLogEventId);

                foreach (var lleId in logEventIds)
                {
                    _repo.LessonLogEvents.RemoveLessonLogEvent(lleId);
                }

                foreach (var lessonId in lessonIds)
                {
                    _repo.Lessons.RemoveLessonWoLog(lessonId);
                }

                _repo.TeacherForDisciplines.RemoveTeacherForDiscipline(tfd.TeacherForDisciplineId);

                _repo.Disciplines.RemoveDiscipline(discView.DisciplineId);

                RefreshView();
            }
        }

        private void DisciplineName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                add.PerformClick();
            }
        }

        private void reloadGroupList_Click(object sender, EventArgs e)
        {
            var groups = _repo.StudentGroups.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            Group.ValueMember = "StudentGroupId";
            Group.DisplayMember = "Name";
            Group.DataSource = groups;
        }

        private void zeroHours_Click(object sender, EventArgs e)
        {
            var discList = new List<Discipline>();

            foreach (var disc in _repo.Disciplines.GetAllDisciplines())
            {
                if ((disc.AuditoriumHours == 0) || 
                    ((noCulture.Checked) && (disc.Name.ToLower().Contains("физическая культура"))) ||  // без физической культуры по чекбоксу
                    ((noArt.Checked) && (disc.StudentGroup.Name.Contains(" И")))) // без факультета искусств по чекбоксу
                {
                    continue;
                }

                if (noPost.Checked)
                {
                    if (disc.StudentGroup.Name.StartsWith("1 ") ||
                        disc.StudentGroup.Name.StartsWith("2 ") ||
                        disc.StudentGroup.Name.StartsWith("3 "))
                    {
                        continue;
                    }
                }

                var localDisc = disc;
                var tfd = _repo.TeacherForDisciplines
                    .GetFirstFiltredTeacherForDiscipline(tefd => 
                    tefd.Discipline.DisciplineId == localDisc.DisciplineId);

                if (tfd != null)
                {
                    if (_repo.CommonFunctions.GetTfdHours(tfd.TeacherForDisciplineId) == 0)
                    {
                        discList.Add(disc);
                    }
                }
                else
                {
                    discList.Add(disc);
                }
            }

            Text = "Дисциплины - " + discList.Count();

            var discView = DisciplineView.DisciplinesToView(_repo, discList);

            DiscipineListView.DataSource = discView.OrderBy(dv => dv.TeacherFio).ToList();

            FormatView();

            DiscipineListView.ClearSelection();
        }
    }
}
