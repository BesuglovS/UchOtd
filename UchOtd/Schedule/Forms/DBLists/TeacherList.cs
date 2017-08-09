using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Forms.DBLists.Lessons;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class TeacherList : Form
    {
        enum RefreshType { TeachersOnly = 1, DisciplinesOnly, FullRefresh };
        
        private readonly ScheduleRepository _repo;

        public TeacherList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;            
        }

        private void FillDicsiplinesList(bool useFilter)
        {
            var discIdsFromTfd = _repo.TeacherForDisciplines.GetAllTeacherForDiscipline().Select(tfd => tfd.Discipline.DisciplineId);

            var disciplines = _repo.Disciplines.GetFiltredDisciplines(d => !discIdsFromTfd.Contains(d.DisciplineId));

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
            RefreshView((int)RefreshType.FullRefresh);

            var teachers = _repo.Teachers.GetAllTeachers();

            remapToTeacherList.DisplayMember = "FIO";
            remapToTeacherList.ValueMember = "TeacherId";
            remapToTeacherList.DataSource = teachers.OrderBy(t => t.FIO).ToList();
        }

        private void RefreshView(int refreshType)
        {
            if ((refreshType == 1) || (refreshType == 3))
            {
                var teachersList = _repo.Teachers.GetAllTeachers();
                teachersList = teachersList.OrderBy(t => t.FIO).ToList();

                TeacherListView.DataSource = teachersList;

                TeacherListView.Columns["TeacherId"].Visible = false;
                TeacherListView.Columns["FIO"].Width = 300;
                TeacherListView.Columns["Phone"].Width = 200;

                //TeacherListView.ClearSelection();
            }
            if ((refreshType == 2) || (refreshType == 3))
            {
                FillDicsiplinesList(useFilter: true);
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
            var teacherDisciplines = _repo.Disciplines.GetTeacherDisciplines(teacher);

            teacherDisciplines = teacherDisciplines
                .OrderBy(d => d.Semester.StartingYear)
                .ThenBy(d => d.Semester.SemesterInYear)
                .ThenBy(d => d.Name)
                .ToList();

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
            if (_repo.Teachers.FindTeacher(teacherFIO.Text, teacherPhone.Text) != null)
            {
                MessageBox.Show("Такой преподаватель уже есть.");
                return;
            }

            var newTeacher = new Teacher { FIO = teacherFIO.Text, Phone = teacherPhone.Text };
            _repo.Teachers.AddTeacher(newTeacher);

            RefreshView((int)RefreshType.TeachersOnly);

            var teacherList = (List<Teacher>)TeacherListView.DataSource;
            var addedTeacher = teacherList.FirstOrDefault(t => t.FIO == newTeacher.FIO);

            var newIndex = -1;
            for (int i = 0; i < teacherList.Count; i++)
            {
                if (teacherList[i].FIO == newTeacher.FIO)
                {
                    newIndex = i;
                }
            }

            if (newIndex != -1)
            {
                TeacherListView.ClearSelection();
                TeacherListView.Rows[newIndex].Selected = true;
                TeacherListView.FirstDisplayedScrollingRowIndex = newIndex;
            }

            
            
            filter.Focus();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (TeacherListView.SelectedCells.Count > 0)
            {
                var teacher = ((List<Teacher>)TeacherListView.DataSource)[TeacherListView.SelectedCells[0].RowIndex];

                teacher.FIO = teacherFIO.Text;
                teacher.Phone = teacherPhone.Text;

                _repo.Teachers.UpdateTeacher(teacher);

                RefreshView((int)RefreshType.TeachersOnly);
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (TeacherListView.SelectedCells.Count > 0)
            {
                var teacher = ((List<Teacher>)TeacherListView.DataSource)[TeacherListView.SelectedCells[0].RowIndex];

                if (_repo.TeacherForDisciplines.GetFiltredTeacherForDiscipline(tfd => tfd.Teacher.TeacherId == teacher.TeacherId).Count > 0)
                {
                    MessageBox.Show("На преподавателя записаны одна или более дисциплин.");
                    return;
                }

                _repo.Teachers.RemoveTeacher(teacher.TeacherId);

                RefreshView((int)RefreshType.TeachersOnly);
            }
        }

        private void deletewithlessons_Click(object sender, EventArgs e)
        {
            if (TeacherListView.SelectedCells.Count > 0)
            {
                var teacher = ((List<Teacher>)TeacherListView.DataSource)[TeacherListView.SelectedCells[0].RowIndex];

                var teacherTfDs = _repo.TeacherForDisciplines.GetFiltredTeacherForDiscipline(tfd => tfd.Teacher.TeacherId == teacher.TeacherId);
                if (teacherTfDs.Count > 0)
                {
                    foreach (var tfd in teacherTfDs)
                    {
                        _repo.TeacherForDisciplines.RemoveTeacherForDiscipline(tfd.TeacherForDisciplineId);
                    }
                }

                _repo.Teachers.RemoveTeacher(teacher.TeacherId);

                RefreshView((int)RefreshType.FullRefresh);
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
            var discipline = _repo.Disciplines.GetDiscipline(discView.DisciplineId);

            if (_repo.TeacherForDisciplines.GetFiltredTeacherForDiscipline(tfdisc => tfdisc.Discipline.DisciplineId == discipline.DisciplineId).Count != 0)
            {
                MessageBox.Show("Дисциплина уже назначена.");
                return;
            }

            var tfd = new TeacherForDiscipline { Teacher = teacher, Discipline = discipline };
            _repo.TeacherForDisciplines.AddTeacherForDiscipline(tfd);

            RefreshTeacherDisciplines(teacher);

            RefreshView((int)RefreshType.DisciplinesOnly);
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
            var discipline = _repo.Disciplines.GetDiscipline(discView.DisciplineId);

            var tfd = _repo.TeacherForDisciplines.FindTeacherForDiscipline(teacher, discipline);

            if (_repo.Lessons.GetFiltredLessons(l => l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId).Count != 0)
            {
                MessageBox.Show("У преподавателя по данной дисциплине есть занятия в расписании.");
                return;
            }

            _repo.TeacherForDisciplines.RemoveTeacherForDiscipline(tfd.TeacherForDisciplineId);

            RefreshTeacherDisciplines(teacher);

            RefreshView((int)RefreshType.DisciplinesOnly);
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
            var discipline = _repo.Disciplines.GetDiscipline(discView.DisciplineId);

            var tfd = _repo.TeacherForDisciplines.FindTeacherForDiscipline(teacher, discipline);

            var tfdLessons = _repo.Lessons.GetFiltredLessons(l => l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId);

            var lessonIds = tfdLessons.Select(l => l.LessonId).ToList();

            var logIds = _repo
                .LessonLogEvents
                .GetFiltredLessonLogEvents(lle => ((lle.OldLesson != null) && lessonIds.Contains(lle.OldLesson.LessonId)) || ((lle.NewLesson != null) && lessonIds.Contains(lle.NewLesson.LessonId)))
                .Select(lle => lle.LessonLogEventId);

            foreach (var evtId in logIds)
            {
                _repo.LessonLogEvents.RemoveLessonLogEvent(evtId);
            }

            foreach (var lesson in tfdLessons)            
            {
                _repo.Lessons.RemoveLessonWoLog(lesson.LessonId);
            }

            _repo.TeacherForDisciplines.RemoveTeacherForDiscipline(tfd.TeacherForDisciplineId);

            RefreshTeacherDisciplines(teacher);

            RefreshView((int)RefreshType.DisciplinesOnly);
        }

        private void FilterButtonClick(object sender, EventArgs e)
        {
            RefreshView((int)RefreshType.DisciplinesOnly);
        }

        private void FilterKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                FilterButtonClick(sender, e);
            }
        }

        private void TeacherFioTextChanged(object sender, EventArgs e)
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
            var disc = _repo.Disciplines.GetFirstFiltredDisciplines(d => d.DisciplineId == discId);
            var tefd = _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discId);
            if (tefd != null)
            {
                var addLessonForm = new AddLesson(_repo, tefd.TeacherForDisciplineId, disc.Semester);
                addLessonForm.Show();
            }
            else
            {
                var addLessonForm = new AddLesson(_repo);
                addLessonForm.Show();
            }
        }

        private void remapTfd_Click(object sender, EventArgs e)
        {
            if (TeacherListView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выделен преподаватель для переназначения дисциплины.");
                return;
            }

            if (TFDListView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выделена дисциплина для переназначения.");
                return;
            }

            var teacher = ((List<Teacher>)TeacherListView.DataSource)[TeacherListView.SelectedCells[0].RowIndex];

            var discView = ((List<DisciplineView>)TFDListView.DataSource)[TFDListView.SelectedCells[0].RowIndex];

            var tfdSelected = _repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(tfd => tfd.Teacher.TeacherId == teacher.TeacherId && tfd.Discipline.DisciplineId == discView.DisciplineId);

            if (tfdSelected == null)
            {
                MessageBox.Show("Назначение не найдено.");
                return;
            }

            tfdSelected.Teacher = (Teacher)remapToTeacherList.SelectedItem;

            _repo.TeacherForDisciplines.UpdateTeacherForDiscipline(tfdSelected);

            RefreshTeacherDisciplines(teacher);
        }
    }
}
