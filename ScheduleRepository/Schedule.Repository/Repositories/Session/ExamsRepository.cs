using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DataLayer;
using Schedule.DomainClasses.Session;

namespace Schedule.Repositories.Repositories.Session
{
    public class ExamsRepository:BaseRepository<Exam>
    {
        public void ClearAllExams()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var examIds = context.Exams.Select(e => e.ExamId).ToList();

                foreach (var examId in examIds)
                {
                    RemoveExam(examId);
                }
            }
        }

        public void ClearExamLogs()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var logIds = context.EventLog.Select(le => le.LogEventId).ToList();

                foreach (var logId in logIds)
                {
                    RemoveLogEvent(logId);
                }
            }
        }

        private void RemoveLogEvent(int logEventId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var logEvent = context.EventLog.FirstOrDefault(le => le.LogEventId == logEventId);

                context.EventLog.Remove(logEvent);
                context.SaveChanges();
            }
        }

        private LogEvent GetLogEvent(int logEventId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context
                    .EventLog
                    .Include(e => e.OldExam)
                    .Include(e => e.NewExam)
                    .FirstOrDefault(le => le.LogEventId == logEventId);
            }
        }


        public int GetTotalExamsCount()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context
                    .Exams
                    .Count(e => e.IsActive);
            }
        }

        public List<Exam> GetAllExamRecords()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context
                    .Exams
                    .ToList();
            }
        }

        public List<Exam> GetAllExams()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context
                    .Exams
                    .Where(e => e.IsActive)
                    .ToList();
            }
        }

        public List<Exam> GetFiltredExams(Func<Exam, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Exams.ToList().Where(condition).ToList();
            }
        }

        public Exam GetFirstFiltredExam(Func<Exam, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Exams.ToList().FirstOrDefault(condition);
            }
        }

        public Exam GetExam(int examId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Exams.FirstOrDefault(a => a.ExamId == examId);
            }
        }

        public void AddExam(Exam exam)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                exam.ExamId = 0;

                context.Exams.Add(exam);
                context.SaveChanges();
            }
        }

        public void UpdateExam(Exam exam)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var oldExam = context.Exams.FirstOrDefault(e => e.ExamId == exam.ExamId);
                if (oldExam != null)
                {
                    oldExam.IsActive = false;

                    exam.ExamId = 0;

                    context.Exams.Add(exam);
                    context.SaveChanges();

                    var logEntry = new LogEvent { OldExam = oldExam, NewExam = exam, DateTime = DateTime.Now };

                    context.EventLog.Add(logEntry);
                }
                context.SaveChanges();
            }
        }

        public void UpdateExamWoLog(Exam exam)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curExam = GetExam(exam.ExamId);

                curExam.ConsultationAuditoriumId = exam.ConsultationAuditoriumId;
                curExam.ConsultationDateTime = exam.ConsultationDateTime;
                curExam.DisciplineId = exam.DisciplineId;
                curExam.ExamAuditoriumId = exam.ExamAuditoriumId;
                curExam.ExamDateTime = exam.ExamDateTime;
                curExam.ExamId = exam.ExamId;
                curExam.IsActive = exam.IsActive;

                context.SaveChanges();
            }
        }

        public void RemoveExam(int examId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var exam = context.Exams.FirstOrDefault(e => e.ExamId == examId);

                context.Exams.Remove(exam);
                context.SaveChanges();
            }

        }

        public void AddExamsRange(IEnumerable<Exam> examList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var exam in examList)
                {
                    exam.ExamId = 0;
                    context.Exams.Add(exam);
                }

                context.SaveChanges();
            }
        }

        public void FillExamListFromSchedule(ScheduleRepository sRepo)
        {
            ClearExamLogs();

            ClearAllExams();

            var examDiscs = sRepo
                .Disciplines
                .GetFiltredDisciplines(d => d.Attestation == 2 || d.Attestation == 3)
                .ToList();

            foreach (var disc in examDiscs)
            {
                AddExam(new Exam
                {
                    DisciplineId = disc.DisciplineId,
                    IsActive = true,
                    ConsultationDateTime = Constants.Constants.DefaultEmptyDateForEvent,
                    ExamDateTime = Constants.Constants.DefaultEmptyDateForEvent
                });
            }
        }

        public List<Exam> GetGroupActiveExams(ScheduleRepository sRepo, int groupId, bool limitToExactGroup = true)
        {
            List<int> discIds;

            if (limitToExactGroup)
            {
                discIds = sRepo
                    .Disciplines
                    .GetFiltredDisciplines(d => d.StudentGroup.StudentGroupId == groupId && (d.Attestation == 2 || d.Attestation == 3))
                    .Select(d => d.DisciplineId)
                    .Distinct()
                    .ToList();
            }
            else
            {
                var studentIds = sRepo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId)
                .ToList()
                .Select(stig => stig.Student.StudentId);

                var groupsListIds = sRepo.StudentsInGroups.GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .ToList()
                    .Select(stig => stig.StudentGroup.StudentGroupId);

                discIds = sRepo
                    .Disciplines
                    .GetFiltredDisciplines(d => groupsListIds.Contains(d.StudentGroup.StudentGroupId) && (d.Attestation == 2 || d.Attestation == 3))
                    .Select(d => d.DisciplineId)
                    .Distinct()
                    .ToList();
            }

            return GetFiltredExams(e => discIds.Contains(e.DisciplineId) && e.IsActive)
                .OrderBy(e => e.ConsultationDateTime)
                .ToList();
        }

        public List<LogEvent> GetAllLogEvents()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context
                    .EventLog
                    .Include(e => e.OldExam)
                    .Include(e => e.NewExam)
                    .ToList();
            }
        }

        public void SaveChanges()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                context.SaveChanges();
            }
        }

        public Dictionary<DateTime, Dictionary<int, List<SessionEvent>>>
            GetFacultyExams(ScheduleRepository Repo, List<int> groupIds)
        {
            // Дата - (id группы + список строк)
            var result = new Dictionary<DateTime, Dictionary<int, List<SessionEvent>>>();

            foreach (var groupId in groupIds)
            {
                var groupExams = GetGroupActiveExams(Repo, groupId, false);

                foreach (var exam in groupExams)
                {
                    var examGroups = new List<int>();
                    var discipline = Repo.Disciplines.GetDiscipline(exam.DisciplineId);

                    string fio = "";

                    var tfd = Repo.TeacherForDisciplines.GetFirstFiltredTeacherForDiscipline(tefd => tefd.Discipline.DisciplineId == discipline.DisciplineId);
                    if (tfd != null)
                    {
                        fio = tfd.Teacher.FIO;
                    }

                    if (!groupIds.Contains(discipline.StudentGroup.StudentGroupId))
                    {
                        var studentIds = Repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == discipline.StudentGroup.StudentGroupId)
                            .ToList()
                            .Select(stig => stig.Student.StudentId);

                        var groupsListIds = Repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                            .ToList()
                            .Select(stig => stig.StudentGroup.StudentGroupId);

                        examGroups.AddRange(groupIds.Where(groupsListIds.Contains));
                    }
                    if (examGroups.Count == 0)
                    {
                        examGroups.Add(discipline.StudentGroup.StudentGroupId);
                    }

                    if (exam.ConsultationDateTime != Constants.Constants.DefaultEmptyDateForEvent)
                    {
                        if (!result.ContainsKey(exam.ConsultationDateTime.Date))
                        {
                            result.Add(exam.ConsultationDateTime.Date, new Dictionary<int, List<SessionEvent>>());
                            foreach (var examGroupId in examGroups)
                            {
                                result[exam.ConsultationDateTime.Date].Add(examGroupId, new List<SessionEvent>());
                            }
                        }
                        foreach (var examGroupId in examGroups)
                        {
                            if (!result[exam.ConsultationDateTime.Date].ContainsKey(examGroupId))
                            {
                                result[exam.ConsultationDateTime.Date].Add(examGroupId, new List<SessionEvent>());
                            }
                        }

                        var consAud = Repo.Auditoriums.GetAuditorium(exam.ConsultationAuditoriumId);
                        string consAudName = "";
                        if (consAud != null)
                        {
                            consAudName = consAud.Name;
                        }

                        foreach (var examGroupId in examGroups)
                        {
                            if (examGroupId == groupId)
                            {
                                result[exam.ConsultationDateTime.Date][examGroupId].Add(new SessionEvent
                                {
                                    IsExam = false,
                                    DisciplineName = discipline.Name,
                                    TeacherFio = fio,
                                    Time = exam.ConsultationDateTime,
                                    Auditorium = consAudName
                                });
                            }
                        }
                    }

                    if (exam.ExamDateTime != Constants.Constants.DefaultEmptyDateForEvent)
                    {
                        if (!result.ContainsKey(exam.ExamDateTime.Date))
                        {
                            result.Add(exam.ExamDateTime.Date, new Dictionary<int, List<SessionEvent>>());
                            foreach (var examGroupId in examGroups)
                            {
                                result[exam.ExamDateTime.Date].Add(examGroupId, new List<SessionEvent>());
                            }
                        }
                        foreach (var examGroupId in examGroups)
                        {
                            if (!result[exam.ExamDateTime.Date].ContainsKey(examGroupId))
                            {
                                result[exam.ExamDateTime.Date].Add(examGroupId, new List<SessionEvent>());
                            }
                        }

                        var examAud = Repo.Auditoriums.GetAuditorium(exam.ExamAuditoriumId);
                        string examAudName = "";
                        if (examAud != null)
                        {
                            examAudName = examAud.Name;
                        }

                        var localGroupId = groupId;
                        foreach (var examGroupId in examGroups.Where(examGroupId => examGroupId == localGroupId))
                        {
                            result[exam.ExamDateTime.Date][examGroupId].Add(new SessionEvent
                            {
                                IsExam = true,
                                DisciplineName = discipline.Name,
                                TeacherFio = fio,
                                Time = exam.ExamDateTime,
                                Auditorium = examAudName
                            });
                        }
                    }
                }
            }

            return result;
        }

        public void AddNewExamsFromSchedule(ScheduleRepository sRepo)
        {
            var examDiscs = sRepo
                .Disciplines
                .GetFiltredDisciplines(d => d.Attestation == 2 || d.Attestation == 3)
                .ToList();

            var examDiscIds = GetAllExams()
                .Select(e => e.DisciplineId)
                .ToList();

            foreach (var disc in examDiscs)
            {
                if (examDiscIds.Contains(disc.DisciplineId))
                {
                    continue;
                }

                AddExam(new Exam
                {
                    DisciplineId = disc.DisciplineId,
                    IsActive = true,
                    ConsultationDateTime = Constants.Constants.DefaultEmptyDateForEvent,
                    ExamDateTime = Constants.Constants.DefaultEmptyDateForEvent
                });
            }
        }

        public void RemoveSyncWithSchedule(ScheduleRepository sRepo)
        {
            var examsToRemove = (
                    from exam in GetAllExams()
                    let localExam = exam
                    let disc = sRepo.Disciplines.GetFirstFiltredDisciplines(d => d.DisciplineId == localExam.DisciplineId)
                    where disc == null || !(disc.Attestation == 2 || disc.Attestation == 3)
                    select exam.ExamId)
                .ToList();

            foreach (var examId in examsToRemove)
            {
                var logIds = GetAllLogEvents()
                    .Where(le => (le.OldExam != null && le.OldExam.ExamId == examId) || (le.NewExam != null && le.NewExam.ExamId == examId))
                    .Select(le => le.LogEventId)
                    .ToList();

                foreach (var logId in logIds)
                {
                    RemoveLogEvent(logId);
                }

                RemoveExam(examId);
            }
        }

        // Dictionary<DateTime, Dictionary<auditoriumId, String>>
        public Dictionary<DateTime, Dictionary<int, String>> GetAuditoriumMap(ScheduleRepository sRepo)
        {
            var result = new Dictionary<DateTime, Dictionary<int, String>>();

            foreach (var exam in GetAllExams())
            {
                var disc = sRepo.Disciplines.GetDiscipline(exam.DisciplineId);


                if (!result.ContainsKey(exam.ConsultationDateTime.Date))
                {
                    result.Add(exam.ConsultationDateTime.Date, new Dictionary<int, string>());
                }

                if (!result[exam.ConsultationDateTime.Date].ContainsKey(exam.ConsultationAuditoriumId))
                {
                    result[exam.ConsultationDateTime.Date].Add(exam.ConsultationAuditoriumId, "");
                }

                result[exam.ConsultationDateTime.Date][exam.ConsultationAuditoriumId] += "\n" +
                    disc.StudentGroup.Name + "\nК\n" + exam.ConsultationDateTime.ToString("H:mm");


                if (!result.ContainsKey(exam.ExamDateTime.Date))
                {
                    result.Add(exam.ExamDateTime.Date, new Dictionary<int, string>());
                }

                if (!result[exam.ExamDateTime.Date].ContainsKey(exam.ExamAuditoriumId))
                {
                    result[exam.ExamDateTime.Date].Add(exam.ExamAuditoriumId, "");
                }

                result[exam.ExamDateTime.Date][exam.ExamAuditoriumId] += "\n" +
                    disc.StudentGroup.Name + "\nЭ\n" + exam.ExamDateTime.ToString("H:mm");
            }

            return result;
        }


        public class SessionEvent
        {
            public bool IsExam { get; set; }
            public string DisciplineName { get; set; }
            public string TeacherFio { get; set; }
            public DateTime Time { get; set; }
            public string Auditorium { get; set; }
        }
    }
}
