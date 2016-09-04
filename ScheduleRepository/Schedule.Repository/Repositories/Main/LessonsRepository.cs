using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Logs;
using Schedule.DomainClasses.Main;
using Schedule.Repositories.Common;

namespace Schedule.Repositories.Repositories.Main
{
    public class LessonsRepository: BaseRepository<Lesson>
    {
        private ScheduleRepository _repo;

        public LessonsRepository(ScheduleRepository repo)
        {
            _repo = repo;
        }
        public List<Lesson> GetAllLessons()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Lessons
                    .Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium.Building)
                    .ToList();
            }
        }

        public List<Lesson> GetAllActiveLessons()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Lessons.Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium.Building)
                    .Where(l => l.State == 1).ToList();
            }
        }

        public List<Lesson> GetFiltredLessons(Func<Lesson, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Lessons.Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium.Building)
                    .ToList().Where(condition).ToList();
            }
        }

        public Lesson GetFirstFiltredLesson(Func<Lesson, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Lessons
                    .Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium.Building)
                    .ToList().FirstOrDefault(condition);
            }
        }

        public Lesson GetFirstFiltredRealLesson(Func<Lesson, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Lessons
                    .Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium.Building)
                    .Where(l => (l.State == 0) || (l.State == 1))
                    .ToList().FirstOrDefault(condition);
            }
        }

        public Lesson GetLesson(int lessonId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Lessons
                    .Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium.Building)
                    .FirstOrDefault(l => l.LessonId == lessonId);
            }
        }

        public void AddLessonWoLog(Lesson lesson)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                lesson.LessonId = 0;

                lesson.TeacherForDiscipline = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == lesson.TeacherForDiscipline.TeacherForDisciplineId);
                lesson.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == lesson.Calendar.CalendarId);
                lesson.Ring = context.Rings.FirstOrDefault(r => r.RingId == lesson.Ring.RingId);
                lesson.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == lesson.Auditorium.AuditoriumId);

                context.Lessons.Add(lesson);

                context.SaveChanges();
            }
        }

        public void AddLesson(Lesson lesson, string publicComment = "", string hiddenComment = "")
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                lesson.LessonId = 0;

                lesson.TeacherForDiscipline = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == lesson.TeacherForDiscipline.TeacherForDisciplineId);
                lesson.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == lesson.Calendar.CalendarId);
                lesson.Ring = context.Rings.FirstOrDefault(r => r.RingId == lesson.Ring.RingId);
                lesson.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == lesson.Auditorium.AuditoriumId);

                context.Lessons.Add(lesson);

                if (lesson.State != 2)
                {
                    context.LessonLog.Add(
                        new LessonLogEvent
                        {
                            OldLesson = null,
                            NewLesson = lesson,
                            DateTime = DateTime.Now,
                            PublicComment = publicComment,
                            HiddenComment = hiddenComment
                        }
                        );
                }
                
                context.SaveChanges();                
            }
        }

        public void UpdateLesson(Lesson lesson)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curLesson = context.Lessons.FirstOrDefault(l => l.LessonId == lesson.LessonId);

                if (curLesson != null)
                {
                    curLesson.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == lesson.Auditorium.AuditoriumId);
                    curLesson.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == lesson.Calendar.CalendarId);
                    curLesson.Ring = context.Rings.FirstOrDefault(r => r.RingId == lesson.Ring.RingId);
                    curLesson.TeacherForDiscipline =
                        context.TeacherForDiscipline.FirstOrDefault(
                            tfd => tfd.TeacherForDisciplineId == lesson.TeacherForDiscipline.TeacherForDisciplineId);
                    curLesson.State = lesson.State;
                }

                context.SaveChanges();
            }
        }

        public void RemoveLessonWoLog(int lessonId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var lesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonId);

                context.Lessons.Remove(lesson);

                context.SaveChanges();
            }
        }

        public void RemoveLessonActiveStateWoLog(int lessonId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curLesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonId);

                if (curLesson != null)
                {
                    curLesson.State = 0;
                }

                context.SaveChanges();
            }
        }

        public void RemoveLesson(int lessonId, string publicComment = "", string hiddenComment = "")
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var lesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonId);

                if (lesson != null)
                {
                    if (lesson.State == 2)
                    {
                        context.Lessons.Remove(lesson);
                    }
                    else
                    {
                        lesson.State = 0;

                        context.LessonLog.Add(
                            new LessonLogEvent
                            {
                                OldLesson = lesson,
                                NewLesson = null,
                                DateTime = DateTime.Now,
                                PublicComment = publicComment,
                                HiddenComment = hiddenComment
                            }
                            );
                    }
                }

                context.SaveChanges();
            }
        }

        public void AddLessonRange(IEnumerable<Lesson> lessonList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var lesson in lessonList)
                {
                    lesson.LessonId = 0;

                    lesson.TeacherForDiscipline = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == lesson.TeacherForDiscipline.TeacherForDisciplineId);
                    lesson.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == lesson.Calendar.CalendarId);
                    lesson.Ring = context.Rings.FirstOrDefault(r => r.RingId == lesson.Ring.RingId);
                    lesson.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == lesson.Auditorium.AuditoriumId);

                    context.Lessons.Add(lesson);
                }

                context.SaveChanges();
            }
        }

        public Dictionary<string, Dictionary<string, Tuple<string, List<Lesson>>>> GetGroupedGroupLessons
            (int groupId, DateTime semesterStarts, int weekfilter, bool putProposedLessons, bool onlyFutureDates)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                // Понедельник 08:00 - {tfdId+State - {weeks + List<Lesson>}}
                var result = new Dictionary<string, Dictionary<string, Tuple<string, List<Lesson>>>>();

                var studentIds = context.StudentsInGroups
                    .Where(sig => sig.StudentGroup.StudentGroupId == groupId && !sig.Student.Expelled)
                    .ToList()
                    .Select(stig => stig.Student.StudentId);
                var groupsListIds = context.StudentsInGroups
                    .Where(sig => studentIds.Contains(sig.Student.StudentId))
                    .ToList()
                    .Select(stig => stig.StudentGroup.StudentGroupId);

                var primaryList = context.Lessons
                    .Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium.Building)
                    .Where(l =>
                        groupsListIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId) &&
                         ((l.State == 1) || (l.State == 2)))
                    .ToList();

                if (onlyFutureDates)
                {
                    primaryList = primaryList.Where(l => DateTime.Now.Date <= l.Calendar.Date.Date).ToList();
                }

                if (!putProposedLessons)
                {
                    primaryList = primaryList.Where(l => l.State != 2).ToList();
                }

                if (weekfilter != -1)
                {
                    primaryList = primaryList
                        .Where(l => _repo.CommonFunctions.CalculateWeekNumber(l.Calendar.Date) == weekfilter)
                        .ToList();
                }

                var groupedLessons = primaryList
                    .GroupBy(l => Constants.Constants.DowRemap[(int)(l.Calendar.Date).DayOfWeek] * 2000 +
                    l.Ring.Time.Hour * 60 + l.Ring.Time.Minute,
                    (dowTime, lessons) =>
                    new
                    {
                        DOW = dowTime / 2000,
                        time = ((dowTime - (dowTime / 2000) * 2000) / 60).ToString("D2") + ":" + ((dowTime - (dowTime / 2000) * 2000) - ((dowTime - (dowTime / 2000) * 2000) / 60) * 60).ToString("D2"),
                        Groups = lessons.GroupBy(ls => ls.TeacherForDiscipline.TeacherForDisciplineId.ToString(CultureInfo.InvariantCulture) + "+" + ls.State,
                            (tfdAndState, tfdLessons) =>
                            new
                            {
                                TFDAndStateForLessonGroup = tfdAndState,
                                Weeks = "",
                                Lessons = tfdLessons
                            }
                        )
                    }
                ).OrderBy(l => l.DOW * 2000 + int.Parse(l.time.Split(':')[0]) * 60 + int.Parse(l.time.Split(':')[1]));

                foreach (var dateTimeLessons in groupedLessons)
                {
                    var dowLocal = dateTimeLessons.DOW;

                    result.Add(dowLocal + " " + dateTimeLessons.time, new Dictionary<string, Tuple<string, List<Lesson>>>());

                    foreach (var lessonGroup in dateTimeLessons.Groups)
                    {
                        var weekList = lessonGroup.Lessons
                            .Select(lesson => _repo.CommonFunctions.CalculateWeekNumber(lesson.Calendar.Date.Date))
                            .ToList();

                        var weekString = CommonFunctions.CombineWeeks(weekList);

                        result[dowLocal + " " + dateTimeLessons.time].Add(lessonGroup.TFDAndStateForLessonGroup, new Tuple<string, List<Lesson>>(weekString, new List<Lesson>()));

                        foreach (var lesson in lessonGroup.Lessons)
                        {
                            result[dowLocal + " " + dateTimeLessons.time][lessonGroup.TFDAndStateForLessonGroup].Item2.Add(lesson);
                        }
                    }
                }

                return result;
            }
        }

        public Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Tuple<Lesson, int>>, string>>>>
            GetFacultyDowSchedule(int facultyId, int dowRu, bool weekFiltered, int weekFilter, bool includeProposed, bool onlyFutureDates)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                // GroupId - 08:00 - {tfdId - {weeks + List<Lesson, LessonType> + weeksAndTypes}}
                var result = new Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Tuple<Lesson, int>>, string>>>>();

                var facultyGroupIds = context
                    .GroupsInFaculties
                    .Where(gif => gif.Faculty.FacultyId == facultyId)
                    .Select(gif => gif.StudentGroup.StudentGroupId)
                    .ToList();

                foreach (var facultyGroupId in facultyGroupIds)
                {
                    var groupId = facultyGroupId;

                    result.Add(groupId, new Dictionary<string, Dictionary<int, Tuple<string, List<Tuple<Lesson, int>>, string>>>());

                    var studentIds = context.StudentsInGroups
                        .Where(sig => sig.StudentGroup.StudentGroupId == groupId && !sig.Student.Expelled)
                        .Select(stig => stig.Student.StudentId)
                        .ToList();
                    var groupsListIds = context.StudentsInGroups
                        .Where(sig => studentIds.Contains(sig.Student.StudentId))
                        .Select(sig => sig.StudentGroup.StudentGroupId)
                        .ToList();

                    var primaryList =
                        context.Lessons
                        .Include(l => l.TeacherForDiscipline.Teacher)
                        .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                        .Include(l => l.Calendar)
                        .Include(l => l.Ring)
                        .Include(l => l.Auditorium.Building)
                        .Where(
                            l =>
                            ((l.State == 1) || ((l.State == 2) && (includeProposed))) &&
                            groupsListIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId)
                        )
                        .ToList();

                    if (onlyFutureDates)
                    {
                        primaryList = primaryList.Where(l => DateTime.Now.Date <= l.Calendar.Date.Date).ToList();
                    }

                    primaryList = primaryList
                        .Where(l => Constants.Constants.DowRemap[(int)(l.Calendar.Date).DayOfWeek] == dowRu)
                        .ToList();

                    if (weekFiltered)
                    {
                        primaryList = primaryList
                            .Where(l => _repo.CommonFunctions.CalculateWeekNumber(l.Calendar.Date) == weekFilter)
                            .ToList();
                    }

                    var groupedLessons = primaryList.GroupBy(
                        l => l.Ring.Time.Hour * 60 + l.Ring.Time.Minute,
                        (lTime, lessons) =>
                        new
                        {
                            time = (lTime / 60).ToString("D2") + ":" + (lTime % 60).ToString("D2"),
                            Groups = lessons.GroupBy(ls => ls.TeacherForDiscipline,
                                                       (tfd, tfdLessons) =>
                                                       new
                                                       {
                                                           TFDForLessonGroup = tfd,
                                                           Weeks = "",
                                                           Lessons = tfdLessons
                                                       }
                                                     )
                        }
                        ).OrderBy(l => int.Parse(l.time.Split(':')[0]) * 60 + int.Parse(l.time.Split(':')[1]));


                    foreach (var dateTimeLessons in groupedLessons)
                    {

                        result[groupId].Add(dateTimeLessons.time, new Dictionary<int, Tuple<string, List<Tuple<Lesson, int>>, string>>());

                        foreach (var lessonGroup in dateTimeLessons.Groups)
                        {
                            var weekList = lessonGroup.Lessons
                                .ToDictionary(lesson => lesson.LessonId, lesson => new Tuple<int, int> (_repo.CommonFunctions.CalculateWeekNumber(lesson.Calendar.Date.Date), GetLessonType(lesson)));

                            var weekString = CommonFunctions.CombineWeeks(weekList.Select(kvp => kvp.Value.Item1).ToList());

                            var grouped = new Dictionary<int, List<int>>(); // LessonType, List<weeks>

                            foreach (var keyValuePair in weekList)
                            {
                                if (!grouped.ContainsKey(keyValuePair.Value.Item2))
                                {
                                    grouped.Add(keyValuePair.Value.Item2, new List<int>());
                                }

                                grouped[keyValuePair.Value.Item2].Add(keyValuePair.Value.Item1);
                            }

                            var weeksWithType = string.Join("; ", 
                                grouped.Select(
                                    lessonType =>
                                        (Constants.Constants.LessonTypeAbbreviation.ContainsKey(lessonType.Key)
                                            ? Constants.Constants.LessonTypeAbbreviation[lessonType.Key]
                                            : lessonType.Key.ToString()) + ":" +
                                        CommonFunctions.CombineWeeks(lessonType.Value)).ToList());

                            result[groupId][dateTimeLessons.time].Add(
                                lessonGroup.TFDForLessonGroup.TeacherForDisciplineId,
                                new Tuple<string, List<Tuple<Lesson, int>>, string>(weekString, new List<Tuple<Lesson, int>>(), weeksWithType));

                            foreach (var lesson in lessonGroup.Lessons)
                            {
                                result[groupId][dateTimeLessons.time][
                                    lessonGroup.TFDForLessonGroup.TeacherForDisciplineId].Item2.Add(new Tuple<Lesson, int>(lesson, weekList[lesson.LessonId].Item2));
                            }
                        }
                    }
                }

                return result;
            }
        }

        private int GetLessonType(Lesson lesson)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var typeSequence = lesson.TeacherForDiscipline.Discipline.TypeSequence;

                if (typeSequence == null)
                {
                    return 0;
                }

                var lessonsList = context.Lessons
                    .Where(l => (l.TeacherForDiscipline.TeacherForDisciplineId == lesson.TeacherForDiscipline.TeacherForDisciplineId) && (l.State == 1))
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .ToList();

                lessonsList.Sort((x, y) =>
                {
                    var xDate = x.Calendar.Date.Date;
                    var yDate = y.Calendar.Date.Date;

                    if (xDate > yDate)
                    {
                        return 1;
                    }

                    if (xDate < yDate)
                    {
                        return -1;
                    }

                    if (x.Ring.Time.Hour > y.Ring.Time.Hour)
                    {
                        return 1;
                    }

                    if (x.Ring.Time.Hour < y.Ring.Time.Hour)
                    {
                        return -1;
                    }

                    if (x.Ring.Time.Minute > y.Ring.Time.Minute)
                    {
                        return 1;
                    }

                    if (x.Ring.Time.Minute < y.Ring.Time.Minute)
                    {
                        return -1;
                    }

                    return 0;
                });

                var lessonIds = lessonsList.Select(l => l.LessonId).ToList();

                var index = lessonIds.IndexOf(lesson.LessonId);

                
                return typeSequence.Length < index ? 0 : int.Parse(typeSequence[index].ToString());
            }
        }
    }
}
