using Schedule.DomainClasses.Main;
using Schedule.Forms.DBLists.Lessons;
using Schedule.Repositories;
using Schedule.Views.DBListViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UchOtd.Schedule.Forms.DBLists.Lessons;

namespace Schedule.Forms.DBLists
{
    public partial class DisciplineList : Form
    {
        private readonly ScheduleRepository _repo;

        public DisciplineList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void DisciplineListLoad(object sender, EventArgs e)
        {
            Attestation.Items.Clear();
            foreach (var attestationForm in Constants.Constants.Attestation)
            {
                Attestation.Items.Add(attestationForm.Value);
            }


            var groups = _repo.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            Group.ValueMember = "StudentGroupId";
            Group.DisplayMember = "Name";
            Group.DataSource = groups;

            var groups2 = _repo.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            groupNameList.ValueMember = "StudentGroupId";
            groupNameList.DisplayMember = "Name";
            groupNameList.DataSource = groups2;


            checkForDoubleDiscsOnAdding.Text = "Проверять дубликаты дисциплин\r\nпри добавлении";

            //RefreshView();
        }

        private void RefreshView()
        {
            List<Discipline> discList;

            if ((filter.Text != "") && discnameFilter.Checked)
            {
                discList = _repo.GetFiltredDisciplines(disc => disc.Name.Contains(filter.Text));
            }
            else
            {
                discList = _repo.GetAllDisciplines(); 
            }

            if (groupnameFilter.Checked)
            {
                var studentIds = _repo
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == (int)groupNameList.SelectedValue)
                    .ToList()
                    .Select(stig => stig.Student.StudentId);
                var groupsListIds = _repo
                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .ToList()
                    .Select(stig => stig.StudentGroup.StudentGroupId);

                discList = discList
                    .Where(d => groupsListIds.Contains(d.StudentGroup.StudentGroupId))
                    .ToList();
            }

            if (HoursFitFiltered.Checked)
            {
                var discListFiltered = new List<Discipline>();

                foreach (var disc in discList)
                {
                    var tfd = _repo.GetFirstFiltredTeacherForDiscipline(tefd => tefd.Discipline.DisciplineId == disc.DisciplineId);

                    var diffList = new List<int>();
                    diffList.Add(0);
                    if (DifferenceByOne.Checked)
                    {
                        diffList.Add(-1);
                    }

                    if ((tfd != null) && (!diffList.Contains(disc.AuditoriumHours - _repo.getTFDHours(tfd.TeacherForDisciplineId))))
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

            if (orderByGroupname.Checked)
            {
                discList = discList.OrderBy(disc => disc.StudentGroup.Name).ToList();
            }
            else
            {
                // ORDER BY NAME
                discList = discList.OrderBy(disc => disc.Name).ToList();
            }

            Text = "Дисциплины - " + discList.Count();

            var discView = DisciplineView.DisciplinesToView(_repo, discList);

            DiscipineListView.DataSource = discView.OrderBy(dv =>dv.TeacherFIO).ToList();

            FormatView();

            DiscipineListView.ClearSelection();
        }

        private void FormatView()
        {
            DiscipineListView.Columns["DisciplineId"].Visible = false;
            DiscipineListView.Columns["DisciplineId"].Width = 40;
            DiscipineListView.Columns["Name"].Width = 270;
            DiscipineListView.Columns["TeacherFIO"].Width = 80;
            DiscipineListView.Columns["ScheduleHours"].Width = 30;
            DiscipineListView.Columns["Attestation"].Width = 80;
            DiscipineListView.Columns["AuditoriumHours"].Width = 80;
            DiscipineListView.Columns["LectureHours"].Width = 80;
            DiscipineListView.Columns["PracticalHours"].Width = 80;
            DiscipineListView.Columns["StudentGroupName"].Width = 120;
        }

        private void DiscipineListViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[e.RowIndex];
            var discipline = _repo.GetDiscipline(discView.DisciplineId);

            DisciplineName.Text = discipline.Name;
            Attestation.SelectedIndex = discipline.Attestation;
            AuditoriumHours.Text = discipline.AuditoriumHours.ToString();
            LectureHours.Text = discipline.LectureHours.ToString();
            PracticalHours.Text = discipline.PracticalHours.ToString();

            Group.SelectedValue = discipline.StudentGroup.StudentGroupId;
        }

        private void AddClick(object sender, EventArgs e)
        {
            int audHours;
            int.TryParse(AuditoriumHours.Text, out audHours);
            int lecHours;
            int.TryParse(LectureHours.Text, out lecHours);
            int practHours;
            int.TryParse(PracticalHours.Text, out practHours);
            
            if ((checkForDoubleDiscsOnAdding.Checked) && 
                (_repo.FindDiscipline(DisciplineName.Text, Attestation.SelectedIndex,
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
                Attestation.SelectedIndex = Constants.Constants.Attestation.Where(a => a.Value == "-").FirstOrDefault().Key;
            }

            var disciplineGroup = _repo.FindStudentGroup(Group.Text);
            if (disciplineGroup == null)
            {
                disciplineGroup = new StudentGroup { Name = Group.Text };
                _repo.AddStudentGroup(disciplineGroup);
            }

            var newDiscipline = new Discipline
            {
                Attestation = Attestation.SelectedIndex,
                AuditoriumHours = audHours,
                LectureHours = lecHours,
                PracticalHours = practHours,
                Name = DisciplineName.Text,
                StudentGroup = disciplineGroup
            };

            _repo.AddDiscipline(newDiscipline);

            RefreshView();
        }

        private void UpdateClick(object sender, EventArgs e)
        {
            if (DiscipineListView.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[DiscipineListView.SelectedCells[0].RowIndex];
                var discipline = _repo.GetDiscipline(discView.DisciplineId);

                discipline.Name = DisciplineName.Text;
                discipline.Attestation = Attestation.SelectedIndex;
                discipline.AuditoriumHours = int.Parse(AuditoriumHours.Text);
                discipline.LectureHours = int.Parse(LectureHours.Text);
                discipline.PracticalHours = int.Parse(PracticalHours.Text);

                discipline.StudentGroup = _repo.GetStudentGroup((int)Group.SelectedValue);

                _repo.UpdateDiscipline(discipline);

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
                    _repo.RemoveDiscipline(id);
                }

                RefreshView();
                return;
            }

            if (DiscipineListView.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[DiscipineListView.SelectedCells[0].RowIndex];

                if (_repo.GetFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discView.DisciplineId).Count > 0)
                {
                    MessageBox.Show("Эта дисциплина определена преподавателю.");
                    return;
                }

                _repo.RemoveDiscipline(discView.DisciplineId);

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
            var tefd = _repo.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discId);
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

        private Color PickPercentColor(int AuditoriumHours, int ScheduleHours)
        {
            if (ScheduleHours > AuditoriumHours+1)
            {
                return Color.FromArgb(255, 0, 255);
            }

            if (ScheduleHours == AuditoriumHours+1)
            {
                return Color.FromArgb(200, 255, 0);
            }
            
            if (ScheduleHours == AuditoriumHours)
            {
                return Color.FromArgb(0, 255, 0);
            }

            if (ScheduleHours >= AuditoriumHours * 0.9)
            {
                return Color.FromArgb(255, 255, 0);
            }
            
            if (ScheduleHours >= AuditoriumHours * 0.5)
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
                
                var tfd = _repo.GetFirstFiltredTeacherForDiscipline(tefd => tefd.Discipline.DisciplineId == discView.DisciplineId);

                if (tfd == null)
                {
                    MessageBox.Show("Дисциплина не назначена преподавателю.");
                    return;
                }

                var lessonIds = _repo
                    .GetFiltredRealLessons(l => l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId)
                    .Select(l => l.LessonId);

                var logEventIds = _repo.GetFiltredLessonLogEvents(lle =>
                    ((lle.OldLesson != null) && (lessonIds.Contains(lle.OldLesson.LessonId))) ||
                    ((lle.NewLesson != null) && (lessonIds.Contains(lle.NewLesson.LessonId))))
                    .Select(lle => lle.LessonLogEventId);

                foreach (var lleId in logEventIds)
                {
                    _repo.RemoveLessonLogEvent(lleId);
                }

                foreach (var lessonId in lessonIds)
                {
                    _repo.RemoveLessonWOLog(lessonId);
                }

                _repo.RemoveTeacherForDiscipline(tfd.TeacherForDisciplineId);

                _repo.RemoveDiscipline(discView.DisciplineId);

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
            var groups = _repo.GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            Group.ValueMember = "StudentGroupId";
            Group.DisplayMember = "Name";
            Group.DataSource = groups;
        }

        private void zeroHours_Click(object sender, EventArgs e)
        {
            var discList = new List<Discipline>();

            foreach (var disc in _repo.GetAllDisciplines())
            {
                if (disc.AuditoriumHours == 0)
                {
                    continue;
                }

                var tfd = _repo.GetFirstFiltredTeacherForDiscipline(tefd => tefd.Discipline.DisciplineId == disc.DisciplineId);

                if (tfd != null)
                {
                    if (_repo.getTFDHours(tfd.TeacherForDisciplineId) == 0)
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

            DiscipineListView.DataSource = discView.OrderBy(dv => dv.TeacherFIO).ToList();

            FormatView();

            DiscipineListView.ClearSelection();
        }
    }
}
