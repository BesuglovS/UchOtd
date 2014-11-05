using Schedule.DomainClasses.Main;
using Schedule.Forms.DBLists.Lessons;
using Schedule.Repositories;
using Schedule.Views.DBListViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UchOtd.Schedule.Forms.DBLists.Lessons;

namespace Schedule.Forms.DBLists
{
    public partial class TeacherList : Form
    {
        enum refreshType { teachersOnly = 1, disciplinesOnly, fullRefresh };
        
        private readonly ScheduleRepository _repo;

        public TeacherList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;            
        }

        private void FillDicsiplinesList(bool useFilter)
        {
            var discIdsFromTFD = _repo.GetAllTeacherForDiscipline().Select(tfd => tfd.Discipline.DisciplineId);

            var disciplines = _repo.GetFiltredDisciplines(d => !discIdsFromTFD.Contains(d.DisciplineId));

            if (useFilter && filter.Text != "")
            {
                disciplines = disciplines.Where(d => d.Name.Contains(filter.Text)).ToList();
            }

            var discView = DisciplineView.DisciplinesToView(_repo, disciplines);

            AllDisciplinesList.DataSource = discView;

            AllDisciplinesList.Columns["DisciplineId"].Visible = false;
            AllDisciplinesList.Columns["Name"].Width = 270;
            AllDisciplinesList.Columns["Attestation"].Width = 80;
            AllDisciplinesList.Columns["AuditoriumHours"].Width = 80;
            AllDisciplinesList.Columns["LectureHours"].Width = 80;
            AllDisciplinesList.Columns["PracticalHours"].Width = 80;
            AllDisciplinesList.Columns["StudentGroupName"].Width = 80;

            AllDisciplinesList.ClearSelection();
        }

        private void TeacherList_Load(object sender, EventArgs e)
        {
            RefreshView((int)refreshType.fullRefresh);            
        }

        private void RefreshView(int refreshType)
        {
            if ((refreshType == 1) || (refreshType == 3))
            {
                var teachersList = _repo.GetAllTeachers();
                teachersList = teachersList.OrderBy(t => t.FIO).ToList();

                TeacherListView.DataSource = teachersList;

                TeacherListView.Columns["TeacherId"].Visible = false;
                TeacherListView.Columns["FIO"].Width = 300;
                TeacherListView.Columns["Phone"].Width = 200;

                //TeacherListView.ClearSelection();
            }
            if ((refreshType == 2) || (refreshType == 3))
            {
                FillDicsiplinesList(true);
            }
            
        }

        private void TeacherListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var teacher = ((List<Teacher>)TeacherListView.DataSource)[e.RowIndex];

            teacherFIO.Text = teacher.FIO;
            teacherPhone.Text = teacher.Phone;

            RefreshTeacherDisciplines(teacher);
        }

        private void RefreshTeacherDisciplines(Teacher teacher)
        {
            var teacherDisciplines = _repo.GetTeacherDisciplines(teacher);

            var discView = DisciplineView.DisciplinesToView(_repo, teacherDisciplines);

            TFDListView.DataSource = discView;

            TFDListView.Columns["DisciplineId"].Visible = false;
            TFDListView.Columns["Name"].Width = 270;
            TFDListView.Columns["Attestation"].Width = 80;
            TFDListView.Columns["AuditoriumHours"].Width = 80;
            TFDListView.Columns["LectureHours"].Width = 80;
            TFDListView.Columns["PracticalHours"].Width = 80;
            TFDListView.Columns["StudentGroupName"].Width = 80;

            TFDListView.ClearSelection();
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (_repo.FindTeacher(teacherFIO.Text, teacherPhone.Text) != null)
            {
                MessageBox.Show("Такой преподаватель уже есть.");
                return;
            }

            var newTeacher = new Teacher { FIO = teacherFIO.Text, Phone = teacherPhone.Text };
            _repo.AddTeacher(newTeacher);

            RefreshView((int)refreshType.teachersOnly);
            
            
            TeacherListView.ClearSelection();
            TeacherListView.Rows[TeacherListView.Rows.Count - 1].Selected = true;
            TeacherListView.FirstDisplayedScrollingRowIndex = TeacherListView.RowCount - 1;
            
            filter.Focus();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (TeacherListView.SelectedCells.Count > 0)
            {
                var teacher = ((List<Teacher>)TeacherListView.DataSource)[TeacherListView.SelectedCells[0].RowIndex];

                teacher.FIO = teacherFIO.Text;
                teacher.Phone = teacherPhone.Text;

                _repo.UpdateTeacher(teacher);

                RefreshView((int)refreshType.teachersOnly);
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (TeacherListView.SelectedCells.Count > 0)
            {
                var teacher = ((List<Teacher>)TeacherListView.DataSource)[TeacherListView.SelectedCells[0].RowIndex];

                if (_repo.GetFiltredTeacherForDiscipline(tfd => tfd.Teacher.TeacherId == teacher.TeacherId).Count > 0)
                {
                    MessageBox.Show("На преподавателя записаны одна или более дисциплин.");
                    return;
                }

                _repo.RemoveTeacher(teacher.TeacherId);

                RefreshView((int)refreshType.teachersOnly);
            }
        }

        private void deletewithlessons_Click(object sender, EventArgs e)
        {
            if (TeacherListView.SelectedCells.Count > 0)
            {
                var teacher = ((List<Teacher>)TeacherListView.DataSource)[TeacherListView.SelectedCells[0].RowIndex];

                var teacherTFDs = _repo.GetFiltredTeacherForDiscipline(tfd => tfd.Teacher.TeacherId == teacher.TeacherId);
                if (teacherTFDs.Count > 0)
                {
                    foreach (var tfd in teacherTFDs)
                    {
                        _repo.RemoveTeacherForDiscipline(tfd.TeacherForDisciplineId);
                    }
                }

                _repo.RemoveTeacher(teacher.TeacherId);

                RefreshView((int)refreshType.fullRefresh);
            }
        }

        private void addTFD_Click(object sender, EventArgs e)
        {
            if (AllDisciplinesList.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выделена дисциплина для добавления.");
                return;
            }

            if (TeacherListView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выделен преподаватель для добавления дисциплины.");
                return;
            }

            var teacher = ((List<Teacher>)TeacherListView.DataSource)[TeacherListView.SelectedCells[0].RowIndex];

            var discView = ((List<DisciplineView>)AllDisciplinesList.DataSource)[AllDisciplinesList.SelectedCells[0].RowIndex];
            var discipline = _repo.GetDiscipline(discView.DisciplineId);

            if (_repo.GetFiltredTeacherForDiscipline(tfdisc => tfdisc.Discipline.DisciplineId == discipline.DisciplineId).Count != 0)
            {
                MessageBox.Show("Дисциплина уже назначена.");
                return;
            }

            var tfd = new TeacherForDiscipline { Teacher = teacher, Discipline = discipline };
            _repo.AddTeacherForDiscipline(tfd);

            RefreshTeacherDisciplines(teacher);

            RefreshView((int)refreshType.disciplinesOnly);
        }

        private void removeTFD_Click(object sender, EventArgs e)
        {
            if (TeacherListView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выделен преподаватель для удаления дисциплины.");
                return;
            }

            if (TFDListView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выделена дисциплина для удаления.");
                return;
            }           

            var teacher = ((List<Teacher>)TeacherListView.DataSource)[TeacherListView.SelectedCells[0].RowIndex];

            var discView = ((List<DisciplineView>)TFDListView.DataSource)[TFDListView.SelectedCells[0].RowIndex];
            var discipline = _repo.GetDiscipline(discView.DisciplineId);
            
            var tfd = _repo.FindTeacherForDiscipline(teacher, discipline);

            if (_repo.GetFiltredRealLessons(l => l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId).Count != 0)
            {
                MessageBox.Show("У преподавателя по данной дисциплине есть занятия в расписании.");
                return;
            }

            _repo.RemoveTeacherForDiscipline(tfd.TeacherForDisciplineId);

            RefreshTeacherDisciplines(teacher);

            RefreshView((int)refreshType.disciplinesOnly);
        }

        private void removeWithLessons_Click(object sender, EventArgs e)
        {
            if (TeacherListView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выделен преподаватель для удаления дисциплины.");
                return;
            }

            if (TFDListView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выделена дисциплина для удаления.");
                return;
            }

            var teacher = ((List<Teacher>)TeacherListView.DataSource)[TeacherListView.SelectedCells[0].RowIndex];

            var discView = ((List<DisciplineView>)TFDListView.DataSource)[TFDListView.SelectedCells[0].RowIndex];
            var discipline = _repo.GetDiscipline(discView.DisciplineId);

            var tfd = _repo.FindTeacherForDiscipline(teacher, discipline);

            var tfdLessons = _repo.GetFiltredRealLessons(l => l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId);

            var lessonIds = tfdLessons.Select(l => l.LessonId).ToList();

            var logIds = _repo
                .GetFiltredLessonLogEvents(lle => ((lle.OldLesson != null) && lessonIds.Contains(lle.OldLesson.LessonId)) || ((lle.NewLesson != null) && lessonIds.Contains(lle.NewLesson.LessonId)))
                .Select(lle => lle.LessonLogEventId);

            foreach (var evtId in logIds)
            {
                _repo.RemoveLessonLogEvent(evtId);
            }

            foreach (var lesson in tfdLessons)            
            {
                _repo.RemoveLessonWOLog(lesson.LessonId);
            }

            _repo.RemoveTeacherForDiscipline(tfd.TeacherForDisciplineId);

            RefreshTeacherDisciplines(teacher);

            RefreshView((int)refreshType.disciplinesOnly);
        }

        private void FilterButtonClick(object sender, EventArgs e)
        {
            RefreshView((int)refreshType.disciplinesOnly);
        }

        private void FilterKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                FilterButtonClick(sender, e);
            }
        }

        private void TeacherFIOTextChanged(object sender, EventArgs e)
        {
            if (teacherFIO.Text.Contains('*'))
            {
                var text = teacherFIO.Text.Split('*');
                teacherFIO.Text = text[0];
                teacherPhone.Text = text[1];

                add.Focus();
            }
        }

        private void TFDListView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var discId = ((List<DisciplineView>)TFDListView.DataSource)[e.RowIndex].DisciplineId;
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
    }
}
