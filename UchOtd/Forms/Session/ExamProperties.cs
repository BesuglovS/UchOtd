using Schedule.DomainClasses.Main;
using Schedule.DomainClasses.Session;
using Schedule.Repositories;
using System;
using System.Windows.Forms;
using UchOtd.NUDS.Core;


namespace UchOtd.Forms.Session
{
    public partial class ExamProperties : Form
    {
        public enum ExamPropertiesMode
        {
            New,
            Edit
        }

        private readonly ScheduleRepository _repo;
        private readonly Exam _exam;
        private readonly ExamPropertiesMode _mode;

        public ExamProperties(ScheduleRepository repo, int examToUpdateId, ExamPropertiesMode mode)
        {
            InitializeComponent();

            _repo = repo;
            _mode = mode;

            if (_mode == ExamPropertiesMode.Edit)
            {
                _exam = _repo.Exams.GetExam(examToUpdateId);
            }

            if (_mode == ExamPropertiesMode.New)
            {
                _exam = new Exam();
            }
        }

        private void SaveWOLog_Click(object sender, EventArgs e)
        {

        }

        private void ExamProperties_Load(object sender, EventArgs e)
        {

            if (_mode == ExamPropertiesMode.Edit)
            {
                var disc = _repo.Disciplines.GetDiscipline(_exam.DisciplineId);
                var teacher = _repo
                    .TeacherForDisciplines
                    .GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == disc.DisciplineId)
                    .Teacher;

                discipline.Text  = disc.StudentGroup.Name + Environment.NewLine;
                discipline.Text += disc.Name + Environment.NewLine;
                discipline.Text += teacher.FIO + Environment.NewLine;
                discipline.Text += disc.AuditoriumHours + " @ " + disc.LectureHours + " / "  + disc.PracticalHours + Environment.NewLine;
                discipline.Text += disc.Name;

                ConsDate.Value = (_exam.ConsultationDateTime == Constants.DefaultEmptyDateForEvent) ? 
                    Constants.DefaultEditDate : _exam.ConsultationDateTime;
                var cAud = _repo.Auditoriums.GetAuditorium(_exam.ConsultationAuditoriumId);
                ConsAudBox.Text = (cAud != null) ? cAud.Name : "";

                ExamDate.Value = (_exam.ExamDateTime == Constants.DefaultEmptyDateForEvent)
                    ? Constants.DefaultEditDate : _exam.ExamDateTime;
                var eAud = _repo.Auditoriums.GetAuditorium(_exam.ExamAuditoriumId);
                ExamAudBox.Text = (eAud != null) ? eAud.Name : "";
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var newExam = new Exam
            {
                ExamId = _exam.ExamId,
                DisciplineId = _exam.DisciplineId,
                IsActive = true,
                ConsultationDateTime = ConsDate.Value
            };

            var consAud = _repo.Auditoriums.FindAuditorium(ConsAudBox.Text);
            if (consAud != null)
            {
                newExam.ConsultationAuditoriumId = consAud.AuditoriumId;
            }

            newExam.ExamDateTime = ExamDate.Value;
            var examAud = _repo.Auditoriums.FindAuditorium(ExamAudBox.Text);
            if (examAud != null)
            {
                newExam.ExamAuditoriumId = examAud.AuditoriumId;
            }

            _repo.Exams.UpdateExam(newExam);

            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
