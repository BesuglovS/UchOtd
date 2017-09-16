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

            hoursWeekFilter.Text = "11-12";

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
                var sortByDiscname = orderByDisciplineName.Checked;
                var groupId = -1;
                if (groupNameList.SelectedValue != null)
                {
                    groupId = (int) groupNameList.SelectedValue;
                }
                else
                {
                    groupNameF = false;
                }
                
                var noCultureF = noCulture.Checked;
                var withExamsOnlyF = WithExamsOnly.Checked;
                var orderbyGroupNameF = orderByGroupname.Checked;
                var noArtF = noArt.Checked;
                var noPostF = noPost.Checked;
                var withLessonsToday = WithLessonsToday.Checked;
                var woTypeSequence = withoutTypeSequence.Checked;
                var hoursCountWeekFiltered = hoursFilteredByWeek.Checked;
                var hoursCountWeekList = new List<int>();
                try
                {
                    hoursCountWeekList = new List<int>();
                    if (!hoursWeekFilter.Text.Contains("-"))
                    {
                        hoursCountWeekList.Add(int.Parse(hoursWeekFilter.Text));
                    }
                    else
                    {
                        var split = hoursWeekFilter.Text.Split('-');
                        var start = int.Parse(split[0]);
                        var finish = int.Parse(split[1]);
                        for (int i = start; i <= finish; i++)
                        {
                            hoursCountWeekList.Add(i);
                        }
                    }
                }
                catch (Exception e)
                {
                }

                try
                {
                    discView = await Task.Run(() =>
                    {
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
                            var groupsListIds = StudentGroupIdsFromGroupId(groupId);

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

                                if ((tfd != null) && (!diffList.Contains(disc.AuditoriumHours - 
                                    _repo.CommonFunctions.GetTfdHours(tfd.TeacherForDisciplineId, false, false, -1))))
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
                            discList = discList.Where(d => !d.Name.ToLower().Contains("физическая культура") && !d.Name.ToLower().Contains("физической культуре")).ToList();
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

                        
                        if (withLessonsToday)
                        {
                            /*
                            discList = discList
                                .Where(
                                    d =>
                                        (_repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == d.DisciplineId) != null) &&
                                        (_repo.Lessons.GetFirstFiltredLesson(l => 
                                            (l.Calendar.Date.Date == DateTime.Today.Date) &&
                                            (l.TeacherForDiscipline.TeacherForDisciplineId == 
                                            _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(tfd => 
                                                tfd.Discipline.DisciplineId == d.DisciplineId).TeacherForDisciplineId)) != null))
                                .ToList();
                            */
                            var tempList = new List<Discipline>();
                            foreach (Discipline disc in discList)
                            {
                                var tfd = _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(
                                    tefd => tefd.Discipline.DisciplineId == disc.DisciplineId);
                                Teacher teacher = null;

                                if (tfd != null)
                                {
                                    teacher = tfd.Teacher;

                                    var teacherDiscsIds =
                                        _repo.TeacherForDisciplines.GetFiltredTeacherForDiscipline(
                                            tefd => tefd.Teacher.TeacherId == teacher.TeacherId)
                                            .Select(tefd => tefd.Discipline.DisciplineId)
                                            .ToList();
                                    if (teacherDiscsIds.Contains(disc.DisciplineId))
                                    {
                                        tempList.Add(disc);
                                    }
                                }
                            }
                            discList = tempList;
                        }

                        if (woTypeSequence)
                        {
                            discList =
                                discList.Where(
                                    d => d.AuditoriumHours != 0 && string.IsNullOrEmpty(d.TypeSequence))
                                    .ToList();
                        }

                        if (orderbyGroupNameF)
                        {
                            discList = discList.OrderBy(disc => disc.StudentGroup.Name).ToList();
                        }
                        
                        if (sortByDiscname)
                        {
                            discList = discList.OrderBy(d => d.Name).ToList();
                        }

                        return DisciplineView.DisciplinesToView(_repo, discList, hoursCountWeekFiltered, hoursCountWeekList).ToList();
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

            Text = "Дисциплины - " + (discView?.Count.ToString() ?? "");

            refresh.Image = null;
            refresh.Text = "Обновить";

            DisciplinesList.DataSource = discView;

            FormatView();

            DisciplinesList.ClearSelection();
        }

        private List<int> StudentGroupIdsFromGroupId(int groupId)
        {
            var studentIds = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId && !sig.Student.Expelled)
                .Select(stig => stig.Student.StudentId)
                .ToList();

            var groupsListIds = _repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                .Select(stig => stig.StudentGroup.StudentGroupId)
                .Distinct()
                .ToList();
            return groupsListIds;
        }

        private void FormatView()
        {
            DisciplinesList.Columns["DisciplineId"].Visible = false;
            DisciplinesList.Columns["DisciplineId"].Width = 40;

            DisciplinesList.Columns["Name"].Width = 270;
            DisciplinesList.Columns["Name"].HeaderText = "Наименование дисциплины";

            DisciplinesList.Columns["TeacherFio"].Width = 80;
            DisciplinesList.Columns["TeacherFio"].HeaderText = "ФИО преподавателя";

            DisciplinesList.Columns["ScheduleHours"].Width = 30;
            DisciplinesList.Columns["ScheduleHours"].HeaderText = "Часов в расписании";

            DisciplinesList.Columns["Attestation"].Width = 80;
            DisciplinesList.Columns["Attestation"].HeaderText = "Форма отчётности";

            DisciplinesList.Columns["AuditoriumHours"].Width = 80;
            DisciplinesList.Columns["AuditoriumHours"].HeaderText = "Аудиторные часы";

            DisciplinesList.Columns["ProposedHours"].Width = 80;
            DisciplinesList.Columns["ProposedHours"].HeaderText = "Неутверждённые часы";

            DisciplinesList.Columns["AuditoriumHoursPerWeek"].Width = 80;
            DisciplinesList.Columns["AuditoriumHoursPerWeek"].HeaderText = "Аудиторные часы (в неделю / ШКОЛА)";

            DisciplinesList.Columns["LectureHours"].Width = 80;
            DisciplinesList.Columns["LectureHours"].HeaderText = "Лекции";

            DisciplinesList.Columns["PracticalHours"].Width = 80;
            DisciplinesList.Columns["PracticalHours"].HeaderText = "Практические / Семинары";

            DisciplinesList.Columns["TypeSequence"].Width = 80;
            DisciplinesList.Columns["TypeSequence"].HeaderText = "Последовательность Л=1/П=2";
        }

        private void DiscipineListViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            var discView = ((List<DisciplineView>)DisciplinesList.DataSource)[e.RowIndex];
            var discipline = _repo.Disciplines.GetDiscipline(discView.DisciplineId);

            DisciplineName.Text = discipline.Name;
            Attestation.SelectedIndex = discipline.Attestation;
            AuditoriumHours.Text = discipline.AuditoriumHours.ToString(CultureInfo.InvariantCulture);
            AuditoriumHoursPerWeek.Text = discipline.AuditoriumHoursPerWeek.ToString(CultureInfo.InvariantCulture);
            LectureHours.Text = discipline.LectureHours.ToString(CultureInfo.InvariantCulture);
            PracticalHours.Text = discipline.PracticalHours.ToString(CultureInfo.InvariantCulture);
            TypeSequence.Text = discipline.TypeSequence;

            Group.SelectedValue = discipline.StudentGroup.StudentGroupId;
        }

        private void DiscipineListViewCellClicked(int rowIndex)
        {
            var discView = ((List<DisciplineView>)DisciplinesList.DataSource)[rowIndex];
            var discipline = _repo.Disciplines.GetDiscipline(discView.DisciplineId);

            DisciplineName.Text = discipline.Name;
            Attestation.SelectedIndex = discipline.Attestation;
            AuditoriumHours.Text = discipline.AuditoriumHours.ToString(CultureInfo.InvariantCulture);
            AuditoriumHoursPerWeek.Text = discipline.AuditoriumHoursPerWeek.ToString(CultureInfo.InvariantCulture);
            LectureHours.Text = discipline.LectureHours.ToString(CultureInfo.InvariantCulture);
            PracticalHours.Text = discipline.PracticalHours.ToString(CultureInfo.InvariantCulture);
            TypeSequence.Text = discipline.TypeSequence;

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
                StudentGroup = disciplineGroup,
                TypeSequence = TypeSequence.Text
            };

            _repo.Disciplines.AddDiscipline(newDiscipline);

            RefreshView();
        }

        private void UpdateClick(object sender, EventArgs e)
        {
            if (DisciplinesList.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineView>)DisciplinesList.DataSource)[DisciplinesList.SelectedCells[0].RowIndex];
                var discipline = _repo.Disciplines.GetDiscipline(discView.DisciplineId);

                discipline.Name = DisciplineName.Text;
                discipline.Attestation = Attestation.SelectedIndex;
                discipline.AuditoriumHours = int.Parse(AuditoriumHours.Text);
                discipline.AuditoriumHoursPerWeek = int.Parse(AuditoriumHoursPerWeek.Text);
                discipline.LectureHours = int.Parse(LectureHours.Text);
                discipline.PracticalHours = int.Parse(PracticalHours.Text);
                discipline.TypeSequence = TypeSequence.Text;

                discipline.StudentGroup = _repo.StudentGroups.GetStudentGroup((int)Group.SelectedValue);

                _repo.Disciplines.UpdateDiscipline(discipline);

                RefreshView();
            }
        }

        private void RemoveClick(object sender, EventArgs e)
        {
            if (DisciplinesList.SelectedRows.Count > 1)
            {
                var discIds = new List<int>();
                for (int i = 0; i < DisciplinesList.SelectedRows.Count; i++)
                {
                    discIds.Add(((List<DisciplineView>)DisciplinesList.DataSource)[DisciplinesList.SelectedRows[i].Index].DisciplineId);
                }

                foreach (var id in discIds)
                {
                    var discNameIds =
                    _repo.DisciplineNames.GetFiltredDisciplineNames(
                        dn => dn.Discipline.DisciplineId == id)
                        .Select(dn => dn.DisciplineNameId)
                        .ToList();
                    foreach (var discNameId in discNameIds)
                    {
                        _repo.DisciplineNames.RemoveDisciplineName(discNameId);
                    }

                    var discAttrIds =
                        _repo.CustomDisciplineAttributes.GetFiltredCustomDisciplineAttributes(
                            cda => cda.Discipline.DisciplineId == id)
                            .Select(cda => cda.CustomDisciplineAttributeId)
                            .ToList();
                    foreach (var discAttrId in discAttrIds)
                    {
                        _repo.CustomDisciplineAttributes.RemoveCustomDisciplineAttribute(discAttrId);
                    }

                    _repo.Disciplines.RemoveDiscipline(id);
                }

                RefreshView();
                return;
            }

            if (DisciplinesList.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineView>)DisciplinesList.DataSource)[DisciplinesList.SelectedCells[0].RowIndex];

                if (_repo.TeacherForDisciplines.GetFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discView.DisciplineId).Count > 0)
                {
                    MessageBox.Show("Эта дисциплина определена преподавателю.");
                    return;
                }

                var discNameIds =
                    _repo.DisciplineNames.GetFiltredDisciplineNames(
                        dn => dn.Discipline.DisciplineId == discView.DisciplineId)
                        .Select(dn => dn.DisciplineNameId)
                        .ToList();
                foreach (var discNameId in discNameIds)
                {
                    _repo.DisciplineNames.RemoveDisciplineName(discNameId);
                }

                var discAttrIds =
                    _repo.CustomDisciplineAttributes.GetFiltredCustomDisciplineAttributes(
                        cda => cda.Discipline.DisciplineId == discView.DisciplineId)
                        .Select(cda => cda.CustomDisciplineAttributeId)
                        .ToList();
                foreach (var discAttrId in discAttrIds)
                {
                    _repo.CustomDisciplineAttributes.RemoveCustomDisciplineAttribute(discAttrId);
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
            var discId = ((List<DisciplineView>)DisciplinesList.DataSource)[e.RowIndex].DisciplineId;
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
                var discView = ((List<DisciplineView>)DisciplinesList.DataSource)[e.RowIndex];

                e.CellStyle.BackColor = PickPercentColor(discView.AuditoriumHours, discView.ScheduleHours);
            }
        }

        private Color PickPercentColor(int auditoriumHours, int scheduleHours)
        {
            if (scheduleHours > auditoriumHours + 1)
            {
                return Color.FromArgb(255, 0, 255);
            }

            if (scheduleHours == auditoriumHours + 1)
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
            if (DisciplinesList.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineView>)DisciplinesList.DataSource)[DisciplinesList.SelectedCells[0].RowIndex];

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
                    var noteIds = _repo.ScheduleNotes.GetFiltredScheduleNotes(n => n.Lesson.LessonId == lessonId)
                        .Select(n => n.ScheduleNoteId)
                        .ToList();

                    foreach (var noteId in noteIds)
                    {
                        _repo.ScheduleNotes.RemoveScheduleNote(noteId);
                    }

                    _repo.Lessons.RemoveLessonWoLog(lessonId);
                }

                _repo.TeacherForDisciplines.RemoveTeacherForDiscipline(tfd.TeacherForDisciplineId);

                var discNameIds =
                    _repo.DisciplineNames.GetFiltredDisciplineNames(
                        dn => dn.Discipline.DisciplineId == discView.DisciplineId)
                        .Select(dn => dn.DisciplineNameId)
                        .ToList();
                foreach (var discNameId in discNameIds)
                {
                    _repo.DisciplineNames.RemoveDisciplineName(discNameId);
                }

                var discAttrIds =
                    _repo.CustomDisciplineAttributes.GetFiltredCustomDisciplineAttributes(
                        cda => cda.Discipline.DisciplineId == discView.DisciplineId)
                        .Select(cda => cda.CustomDisciplineAttributeId)
                        .ToList();
                foreach (var discAttrId in discAttrIds)
                {
                    _repo.CustomDisciplineAttributes.RemoveCustomDisciplineAttribute(discAttrId);
                }

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
                    ((noCulture.Checked) && (disc.Name.ToLower().Contains("физическая культура") || disc.Name.ToLower().Contains("физической культуре"))) ||  // без физической культуры по чекбоксу
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
                    if (_repo.CommonFunctions.GetTfdHours(tfd.TeacherForDisciplineId, false, false, -1) == 0)
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

            var discView = DisciplineView.DisciplinesToView(_repo, discList, false, null);

            DisciplinesList.DataSource = discView.OrderBy(dv => dv.TeacherFio).ToList();

            FormatView();

            DisciplinesList.ClearSelection();
        }

        private void Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Group.SelectedIndex != 0) && (SyncGroupName.Checked))
            {
                groupNameList.SelectedIndex = Group.SelectedIndex;
            }
        }

        private void groupNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((groupNameList.SelectedIndex != 0) && (SyncGroupName.Checked))
            {
                Group.SelectedIndex = groupNameList.SelectedIndex;
            }
        }
        
        private void DisciplinesList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                DiscipineListViewCellClicked(e.RowIndex);

                var discView = ((List<DisciplineView>) DisciplinesList.DataSource)[e.RowIndex];

                var tefd = _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(
                    tfd => tfd.Discipline.DisciplineId == discView.DisciplineId);

                if (tefd == null) return;

                DisciplinesList.DoDragDrop("tfd:" + tefd.TeacherForDisciplineId, DragDropEffects.Copy);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hoursFilteredByWeek.Checked = true;
            hoursWeekFilter.Text = "12-13";
        }
    }
}
