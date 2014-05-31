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
        private readonly int _examId;
        private Exam _exam;
        private readonly ExamPropertiesMode _mode;

        public ExamProperties(ScheduleRepository Repo, int examToUpdateId, ExamPropertiesMode mode)
        {
            InitializeComponent();

            _repo = Repo;
            _examId = examToUpdateId;
            _mode = mode;

            if (_mode == ExamPropertiesMode.Edit)
            {
                _exam = _repo.GetExam(_examId);
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
                var disc = _repo.GetDiscipline(_exam.DisciplineId);
                var teacher = _repo
                    .GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == disc.DisciplineId)
                    .Teacher;

                discipline.Text  = disc.StudentGroup.Name + Environment.NewLine;
                discipline.Text += disc.Name + Environment.NewLine;
                discipline.Text += teacher.FIO + Environment.NewLine;
                discipline.Text += disc.AuditoriumHours + " @ " + disc.LectureHours + " / "  + disc.PracticalHours + Environment.NewLine;
                discipline.Text += disc.Name;

                if (_exam.ConsultationDateTime == Constants.DefaultEmptyDateForEvent)
                {
                    ConsDate.Value = Constants.DefaultEditDate;
                }
                else
                {
                    ConsDate.Value = _exam.ConsultationDateTime;
                }
                var cAud = _repo.GetAuditorium(_exam.ConsultationAuditoriumId);
                if (cAud != null)
                {
                    ConsAudBox.Text = cAud.Name;
                }
                else
                {
                    ConsAudBox.Text = "";
                }

                if (_exam.ExamDateTime == Constants.DefaultEmptyDateForEvent)
                {
                    ExamDate.Value = Constants.DefaultEditDate;   
                }
                else
                {
                    ExamDate.Value = _exam.ExamDateTime;
                }
                var eAud = _repo.GetAuditorium(_exam.ExamAuditoriumId);
                if (eAud != null)
                {
                    ExamAudBox.Text = eAud.Name;
                }
                else
                {
                    ExamAudBox.Text = "";
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var newExam = new Exam() { ExamId = _exam.ExamId, DisciplineId = _exam.DisciplineId, IsActive = true };

            newExam.ConsultationDateTime = ConsDate.Value;
            var consAud = _repo.FindAuditorium(ConsAudBox.Text);
            if (consAud != null)
            {
                newExam.ConsultationAuditoriumId = consAud.AuditoriumId;
            }

            newExam.ExamDateTime = ExamDate.Value;
            var examAud = _repo.FindAuditorium(ExamAudBox.Text);
            if (examAud != null)
            {
                newExam.ExamAuditoriumId = examAud.AuditoriumId;
            }

            _repo.UpdateExam(newExam);

            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
