using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using Schedule.DataLayer;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Calendar = Schedule.DomainClasses.Main.Calendar;

namespace Schedule.Repositories.Common
{
    public class CommonFunctions
    {
        private readonly ScheduleRepository _repo;
        public string ConnectionString { get; set; }

        public CommonFunctions(ScheduleRepository repo)
        {
            _repo = repo;
            ConnectionString = _repo.GetConnectionString();
        }

        public static int IndexOfNth(string str, string value, int nth = 1)
        {
            if (nth <= 0)
                throw new ArgumentException("Can not find the zeroth index of substring in string. Must start with 1");
            int offset = str.IndexOf(value);
            for (int i = 1; i < nth; i++)
            {
                if (offset == -1) return -1;
                offset = str.IndexOf(value, offset + 1);
            }
            return offset;
        }

        public static DateTime GetMonday(DateTime date)
        {
            var dow = date.DayOfWeek;
            switch (dow)
            {
                case DayOfWeek.Monday: return date; 
                case DayOfWeek.Tuesday: return date.AddDays(-1); 
                case DayOfWeek.Wednesday: return date.AddDays(-2);
                case DayOfWeek.Thursday: return date.AddDays(-3);
                case DayOfWeek.Friday: return date.AddDays(-4); 
                case DayOfWeek.Saturday: return date.AddDays(-5);
                case DayOfWeek.Sunday: return date.AddDays(-6); 
                default:return date; 
            }
        }

        public static string CombineWeeks(List<int> list)
        {
            var result = new List<string>();
            int maxWeek = list.Max() + 3;
            int minWeek = list.Min() - 3;
            var boolWeeks = new Dictionary<int, bool>();

            for (var i = minWeek; i <= maxWeek; i++)
            {
                boolWeeks[i] = list.Contains(i);
            }

            bool prev = false;
            int baseNum = maxWeek;
            for (var i = minWeek + 1; i <= maxWeek - 2; i++)
            {
                if (!prev && boolWeeks[i])
                {
                    baseNum = i;
                }

                if (!boolWeeks[i] && ((i - baseNum) > 2))
                {
                    result.Add(baseNum + "-" + (i - 1));

                    for (var k = baseNum; k < i; k++)
                    {
                        boolWeeks[k] = false;
                    }
                }

                if (!boolWeeks[i])
                {
                    baseNum = maxWeek + 1;
                }

                prev = boolWeeks[i];
            }


            prev = false;
            baseNum = maxWeek + 1;
            for (var i = minWeek; i <= maxWeek; i += 2)
            {
                if (!prev && boolWeeks[i])
                {
                    baseNum = i;
                }

                if (!boolWeeks[i] && ((i - baseNum) > 4))
                {
                    result.Add(baseNum + "-" + (i - 2) + ((baseNum % 2 == 0) ? " (чёт.)": " (нечёт.)"));

                    for (var k = baseNum; k < i; k += 2)
                    {
                        boolWeeks[k] = false;
                    }
                }

                if (!boolWeeks[i])
                {
                    baseNum = maxWeek + 1;
                }

                prev = boolWeeks[i];
            }

            prev = false;
            baseNum = maxWeek + 1;
            for (var i = minWeek + 1; i <= maxWeek; i += 2)
            {
                if (!prev && boolWeeks[i])
                {
                    baseNum = i;
                }

                if (!boolWeeks[i] && ((i - baseNum) > 4))
                {
                    result.Add(baseNum + "-" + (i - 2) + ((baseNum % 2 == 0) ? " (чёт.)" : " (нечёт.)"));

                    for (var k = baseNum; k < i; k += 2)
                    {
                        boolWeeks[k] = false;
                    }
                }

                if (!boolWeeks[i])
                {
                    baseNum = maxWeek + 1;
                }

                prev = boolWeeks[i];
            }



            for (var i = minWeek; i <= maxWeek; i++)
            {
                if (boolWeeks[i])
                {
                    result.Add(i.ToString(CultureInfo.InvariantCulture));
                }
            }

            result.Sort((a, b) =>
            {
                int aVal, bVal;

                if (a.Contains('-'))
                {
                    int.TryParse(a.Substring(0, a.IndexOf('-')), out aVal);
                }
                else
                {
                    int.TryParse(a, out aVal);
                }

                if (b.Contains('-'))
                {
                    int.TryParse(b.Substring(0, b.IndexOf('-')), out bVal);
                }
                else
                {
                    int.TryParse(b, out bVal);
                }

                if (aVal > bVal) return 1;
                if (bVal > aVal) return -1;
                return 0;
            });

            var final = (result.Count == 0) ? "" : result.Aggregate((current, str) => current + ", " + str);
            return final;
        }

        public static List<int> WeeksStringToList(string weeksString, bool removeParentheses = false)
        {
            var result = new List<int>();

            string str = weeksString;
            if (removeParentheses)
            {
                str = weeksString.Substring(0, weeksString.Length - 1);
                str = str.Substring(1, str.Length - 1);
            }

            foreach (var item in str.Split(','))
            {
                var st = item.Trim(' ');

                if (!st.Contains('-'))
                {
                    result.Add(int.Parse(st));
                }
                else
                {
                    var parsedOK = true;
                    int weekNum = -1;
                    try
                    {
                        weekNum = int.Parse(st);
                    }
                    catch (Exception e)
                    {
                        parsedOK = false;
                    }

                    if (parsedOK)
                    {
                        result.Add(weekNum);
                    }
                    else
                    {
                        int mods = 0; // 0 - нет; 1 - нечётные; 2 - чётные

                        if (st.EndsWith(" (нечёт.)"))
                        {
                            st = st.Substring(0, st.Length - 9);
                            mods = 1;
                        }

                        if (st.EndsWith(" (чёт.)"))
                        {
                            st = st.Substring(0, st.Length - 7);
                            mods = 2;
                        }

                        var minusCount = st.Where(c => c == '-').ToList().Count();
                        int breakIndex = -1;

                        switch (minusCount)
                        {
                            case 1:
                                breakIndex = IndexOfNth(st, "-", 1);
                                break;
                            case 2:
                                breakIndex = IndexOfNth(st, "-", st.IndexOf("--", StringComparison.Ordinal) != -1 ? 1 : 2);
                                break;
                            case 3:
                                breakIndex = IndexOfNth(st, "-", 2);
                                break;
                        }

                        int start = int.Parse(st.Substring(0, breakIndex));

                        int end = int.Parse(
                            st.Substring(breakIndex + 1));

                        for (int i = start; i <= end; i++)
                        {
                            switch (mods)
                            {
                                case 0:
                                    result.Add(i);
                                    break;
                                case 1:
                                    if ((i % 2) == 1)
                                        result.Add(i);
                                    break;
                                case 2:
                                    if ((i % 2) == 0)
                                        result.Add(i);
                                    break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public DateTime GetDateFromDowAndWeek(int dow, int week)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var semesterStartsOption = context.Config
                    .FirstOrDefault(co => co.Key == "Semester Starts");
                if (semesterStartsOption == null)
                {
                    return new DateTime(2000, 1, 1);
                }

                var semesterStarts = DateTime.ParseExact(semesterStartsOption.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var ssDow = (semesterStarts.DayOfWeek != DayOfWeek.Sunday) ? (int)semesterStarts.DayOfWeek : 7;

                return semesterStarts.AddDays((-1) * (ssDow - 1) + (week - 1) * 7 + dow - 1);
            }
        }

        public Calendar GetCalendarFromDowAndWeek(int dow, int week)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var semesterStartsOption = context.Config
                    .FirstOrDefault(co => co.Key == "Semester Starts");
                if (semesterStartsOption == null)
                {
                    return null;
                }

                var semesterStarts = DateTime.ParseExact(semesterStartsOption.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var ssDow = (semesterStarts.DayOfWeek != DayOfWeek.Sunday) ? (int)semesterStarts.DayOfWeek : 7;

                var resultDate = semesterStarts.AddDays((-1) * (ssDow - 1) + (week - 1) * 7 + dow - 1);

                var result = context.Calendars.FirstOrDefault(c => c.Date == resultDate);

                return result;
            }
        }

        public List<Auditorium> GetFreeAuditoriumsAtTime(Calendar calendar, Ring ring)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var occupiedAudIds = context.Lessons
                    .Where(l =>
                        ((l.Calendar.CalendarId == calendar.CalendarId) &&
                         (l.Ring.RingId == ring.RingId)))
                     .Select(l => l.Auditorium.AuditoriumId)
                     .Distinct()
                     .ToList();

                var result = context.Auditoriums.Where(a => !occupiedAudIds.Contains(a.AuditoriumId)).ToList();

                return result;
            }
        }

        public List<Auditorium> GetFreeAuditoriumsAtDowTime(List<int> calendars, List<int> ringIds, int buildingId, bool proposedIncluded)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var occupiedAudIds = context.Lessons
                    .Where(l =>
                        ((calendars.Contains(l.Calendar.CalendarId)) &&
                         (ringIds.Contains(l.Ring.RingId))) &&
                        ((l.State == 1) ||
                         ((l.State == 2) && proposedIncluded)))
                     .Select(l => l.Auditorium.AuditoriumId)
                     .Distinct()
                     .ToList();

                var occupiedAudEventsIds = context.AuditoriumEvents
                    .Where(e =>
                        (calendars.Contains(e.Calendar.CalendarId)) &&
                        (ringIds.Contains(e.Ring.RingId)))
                     .Select(e => e.Auditorium.AuditoriumId)
                     .Distinct()
                     .ToList();

                var result = context.Auditoriums.Where(a => !occupiedAudIds.Contains(a.AuditoriumId) && !occupiedAudEventsIds.Contains(a.AuditoriumId)).ToList();

                if (buildingId != -1)
                {
                    result = result.Where(a => a.Building.BuildingId == buildingId).ToList();
                }

                return result;
            }
        }

        public int CalculateWeekNumber(DateTime dateTime)
        {
            var semesterStarts = GetSemesterStarts();
            var ssDow = (semesterStarts.DayOfWeek != DayOfWeek.Sunday) ? (int)semesterStarts.DayOfWeek : 7;

            var ssWeeksMonday = semesterStarts.AddDays((-1) * (ssDow - 1));

            return (dateTime >= ssWeeksMonday) ? 
                ((dateTime - ssWeeksMonday).Days / 7 + 1) :
                ((-1) * (((ssWeeksMonday - dateTime).Days - 1) / 7));
        }

        public Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>> GetGroupedGroupsLessons(List<int> groupListIds, bool showProposed, CancellationToken cToken)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                // Понедельник 08:00 - {tfdId - {weeks + List<Lesson>}}
                var result = new Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>>();

                foreach (var groupId in groupListIds)
                {
                    cToken.ThrowIfCancellationRequested();

                    result.Add(groupId, new Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>());

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
                        .Include(l => l.Auditorium)
                        .Where(l => groupsListIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId) &&
                            ((l.State == 1) || ((l.State == 2) && showProposed)))
                        .ToList();

                    var groupedLessons = primaryList.GroupBy(l => Constants.Constants.DowRemap[(int)(l.Calendar.Date).DayOfWeek] * 2000 +
                        l.Ring.Time.Hour * 60 + l.Ring.Time.Minute,
                        (dow, lessons) =>
                        new
                        {
                            DOW = dow / 2000,
                            time = ((dow - (dow / 2000) * 2000) / 60).ToString("D2") + ":" + ((dow - (dow / 2000) * 2000) - ((dow - (dow / 2000) * 2000) / 60) * 60).ToString("D2"),
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
                    ).OrderBy(l => l.DOW * 2000 + int.Parse(l.time.Split(':')[0]) * 60 + int.Parse(l.time.Split(':')[1]));

                    foreach (var dateTimeLessons in groupedLessons)
                    {
                        cToken.ThrowIfCancellationRequested();

                        var dowLocal = dateTimeLessons.DOW;

                        result[groupId].Add(dowLocal + " " + dateTimeLessons.time, new Dictionary<int, Tuple<string, List<Lesson>>>());

                        foreach (var lessonGroup in dateTimeLessons.Groups)
                        {
                            var weekList = lessonGroup.Lessons
                                .Select(lesson => CalculateWeekNumber(lesson.Calendar.Date.Date))
                                .ToList();

                            var weekString = CombineWeeks(weekList);

                            result[groupId][dowLocal + " " + dateTimeLessons.time].Add(lessonGroup.TFDForLessonGroup.TeacherForDisciplineId, new Tuple<string, List<Lesson>>(weekString, new List<Lesson>()));

                            foreach (var lesson in lessonGroup.Lessons)
                            {
                                result[groupId][dowLocal + " " + dateTimeLessons.time][lessonGroup.TFDForLessonGroup.TeacherForDisciplineId].Item2.Add(lesson);
                            }
                        }
                    }
                }

                return result;
            }
        }

        // data   - Dictionary<RingId, Dictionary <AuditoriumId, List<Dictionary<tfd, List<Lesson>>>>>
        // result - Dictionary<RingId, Dictionary <AuditoriumId, List<tfd/Event-string>>>
        public Dictionary<int, Dictionary<int, List<string>>> GetDowAuds(DayOfWeek dow, List<int> weekFilterList, int buildingId, bool showProposed)
        {
            var data = new Dictionary<int, Dictionary<int, Dictionary<int, List<Lesson>>>>();

            List<Lesson> dowLessons;
            if (weekFilterList == null || weekFilterList.Count == 0)
            {
                if (buildingId == -1)
                {
                    dowLessons = _repo.Lessons.GetFiltredLessons(l =>
                        l.Calendar.Date.DayOfWeek == dow &&
                        ((l.State == 1) || ((l.State == 2) && showProposed)))
                        .ToList();
                }
                else
                {
                    dowLessons = _repo.Lessons.GetFiltredLessons(l =>
                        l.Calendar.Date.DayOfWeek == dow &&
                        ((l.State == 1) || ((l.State == 2) && showProposed)) &&
                        l.Auditorium.Building.BuildingId == buildingId)
                        .ToList();
                }
            }
            else
            {
                if (buildingId == -1)
                {
                    dowLessons = _repo.Lessons.GetFiltredLessons(l =>
                            l.Calendar.Date.DayOfWeek == dow &&
                            ((l.State == 1) || ((l.State == 2) && showProposed)) &&
                            weekFilterList.Contains(CalculateWeekNumber(l.Calendar.Date)))
                        .ToList();
                }
                else
                {
                    dowLessons = _repo.Lessons.GetFiltredLessons(l =>
                            l.Calendar.Date.DayOfWeek == dow &&
                            ((l.State == 1) || ((l.State == 2) && showProposed)) &&
                            weekFilterList.Contains(CalculateWeekNumber(l.Calendar.Date)) &&
                            l.Auditorium.Building.BuildingId == buildingId)
                        .ToList();
                }
            }

            foreach (var lesson in dowLessons)
            {
                if (!data.ContainsKey(lesson.Ring.RingId))
                {
                    data.Add(lesson.Ring.RingId, new Dictionary<int, Dictionary<int, List<Lesson>>>());
                }

                if (!data[lesson.Ring.RingId].ContainsKey(lesson.Auditorium.AuditoriumId))
                {
                    data[lesson.Ring.RingId].Add(lesson.Auditorium.AuditoriumId, new Dictionary<int, List<Lesson>>());
                }

                if (!data[lesson.Ring.RingId][lesson.Auditorium.AuditoriumId].ContainsKey(lesson.TeacherForDiscipline.TeacherForDisciplineId))
                {
                    data[lesson.Ring.RingId][lesson.Auditorium.AuditoriumId].Add(lesson.TeacherForDiscipline.TeacherForDisciplineId, new List<Lesson>());
                }

                data[lesson.Ring.RingId][lesson.Auditorium.AuditoriumId][lesson.TeacherForDiscipline.TeacherForDisciplineId].Add(lesson);
            }

            var rings = _repo.Rings.GetAllRings().ToDictionary(r => r.RingId, r => r.Time);

            data = data
                .OrderBy(a => rings[a.Key].TimeOfDay)
                .ToDictionary(a => a.Key, a => a.Value);

            var result = new Dictionary<int, Dictionary<int, List<string>>>();

            foreach (var ring in data)
            {
                result.Add(ring.Key, new Dictionary<int, List<string>>());

                foreach (var aud in ring.Value)
                {
                    result[ring.Key].Add(aud.Key, new List<string>());

                    foreach (var tfd in aud.Value)
                    {
                        result[ring.Key][aud.Key].Add(tfd.Value[0].TeacherForDiscipline.Discipline.StudentGroup.Name + Environment.NewLine +
                            "(" + GetWeekStringFromLessons(tfd.Value) + ")@" +
                            tfd.Value[0].TeacherForDiscipline.Teacher.FIO + "@" + tfd.Value[0].TeacherForDiscipline.Discipline.Name);
                    }
                }
            }

            List<AuditoriumEvent> audEvents;
            if (weekFilterList == null || weekFilterList.Count == 0)
            {
                if (buildingId == -1)
                {
                    audEvents = _repo.AuditoriumEvents.GetFiltredAuditoriumEvents(evt => evt.Calendar.Date.DayOfWeek == dow);
                }
                else
                {
                    audEvents = _repo.AuditoriumEvents.GetFiltredAuditoriumEvents(evt => evt.Calendar.Date.DayOfWeek == dow &&
                        evt.Auditorium.Building.BuildingId == buildingId);
                }
            }
            else
            {
                if (buildingId == -1)
                {
                    audEvents = _repo.AuditoriumEvents.GetFiltredAuditoriumEvents(evt =>
                        evt.Calendar.Date.DayOfWeek == dow &&
                        weekFilterList.Contains(CalculateWeekNumber(evt.Calendar.Date)));
                }
                else
                {
                    audEvents = _repo.AuditoriumEvents.GetFiltredAuditoriumEvents(evt =>
                        evt.Calendar.Date.DayOfWeek == dow &&
                        weekFilterList.Contains(CalculateWeekNumber(evt.Calendar.Date)) &&
                        evt.Auditorium.Building.BuildingId == buildingId);
                }
            }

            var eventId = 0;
            var eventsIds = new Dictionary<int, string>();
            var eventsData = new Dictionary<int, Dictionary<int, Dictionary<int, List<AuditoriumEvent>>>>();

            foreach (var evt in audEvents)
            {
                if (!eventsData.ContainsKey(evt.Ring.RingId))
                {
                    eventsData.Add(evt.Ring.RingId, new Dictionary<int, Dictionary<int, List<AuditoriumEvent>>>());
                }

                if (!eventsData[evt.Ring.RingId].ContainsKey(evt.Auditorium.AuditoriumId))
                {
                    eventsData[evt.Ring.RingId].Add(evt.Auditorium.AuditoriumId, new Dictionary<int, List<AuditoriumEvent>>());
                }

                var eventFound = (eventsIds.Count(e => e.Value == evt.Name) > 0);
                int curEventId;
                if (eventFound)
                {
                    curEventId = eventsIds.First(e => e.Value == evt.Name).Key;
                }
                else
                {
                    eventsIds.Add(eventId, evt.Name);
                    curEventId = eventId;
                    eventId++;
                }

                if (!eventsData[evt.Ring.RingId][evt.Auditorium.AuditoriumId].ContainsKey(curEventId))
                {
                    eventsData[evt.Ring.RingId][evt.Auditorium.AuditoriumId].Add(curEventId, new List<AuditoriumEvent>());
                }

                eventsData[evt.Ring.RingId][evt.Auditorium.AuditoriumId][curEventId].Add(evt);
            }

            foreach (var ring in eventsData)
            {
                if (!result.ContainsKey(ring.Key))
                {
                    result.Add(ring.Key, new Dictionary<int, List<string>>());
                }

                foreach (var aud in ring.Value)
                {
                    if (!result[ring.Key].ContainsKey(aud.Key))
                    {
                        result[ring.Key].Add(aud.Key, new List<string>());
                    }

                    foreach (var eventPair in aud.Value)
                    {
                        if (eventPair.Value[0].Name.Contains('@'))
                        {
                            var evtName = eventPair.Value[0].Name.Split('@')[0];
                            var evtHint = eventPair.Value[0].Name.Substring(evtName.Length + 1);
                            result[ring.Key][aud.Key].Add(evtName + Environment.NewLine + "( " + GetWeekStringFromEvents(eventPair.Value) + " )@" + evtHint);
                        }
                        else
                        {
                            result[ring.Key][aud.Key].Add(eventPair.Value[0].Name + Environment.NewLine + "( " + GetWeekStringFromEvents(eventPair.Value) + " )");
                        }
                    }
                }
            }

            result = result.OrderBy(r => rings[r.Key].TimeOfDay).ToDictionary(r => r.Key, r => r.Value);

            return result;
        }

        // data   - Dictionary<RingId, Dictionary <dow, List<Dictionary<tfd, List<Lesson>>>>>
        // result - Dictionary<RingId, Dictionary <dow, List<tfd/Event-string>>>
        public Dictionary<int, Dictionary<int, List<string>>> GetAud(int auditoriumId, 
            bool showProposed, CancellationToken cToken)
        {
            var data = new Dictionary<int, Dictionary<int, Dictionary<int, List<Lesson>>>>();

            cToken.ThrowIfCancellationRequested();

            var audLessons = _repo.Lessons.GetFiltredLessons(l =>
                l.Auditorium.AuditoriumId == auditoriumId &&
                ((l.State == 1) || ((l.State == 2) && showProposed)))
                .ToList();

            foreach (var lesson in audLessons)
            {
                if (!data.ContainsKey(lesson.Ring.RingId))
                {
                    data.Add(lesson.Ring.RingId, new Dictionary<int, Dictionary<int, List<Lesson>>>());
                }

                if (!data[lesson.Ring.RingId].ContainsKey(Constants.Constants.DowRemap[(int)lesson.Calendar.Date.DayOfWeek]))
                {
                    data[lesson.Ring.RingId].Add(Constants.Constants.DowRemap[(int)lesson.Calendar.Date.DayOfWeek], new Dictionary<int, List<Lesson>>());
                }

                if (!data[lesson.Ring.RingId][Constants.Constants.DowRemap[(int)lesson.Calendar.Date.DayOfWeek]].ContainsKey(lesson.TeacherForDiscipline.TeacherForDisciplineId))
                {
                    data[lesson.Ring.RingId][Constants.Constants.DowRemap[(int)lesson.Calendar.Date.DayOfWeek]].Add(lesson.TeacherForDiscipline.TeacherForDisciplineId, new List<Lesson>());
                }

                data[lesson.Ring.RingId][Constants.Constants.DowRemap[(int)lesson.Calendar.Date.DayOfWeek]][lesson.TeacherForDiscipline.TeacherForDisciplineId].Add(lesson);
            }

            cToken.ThrowIfCancellationRequested();

            var rings = _repo.Rings.GetAllRings().ToDictionary(r => r.RingId, r => r.Time);

            data = data
                .OrderBy(a => rings[a.Key].TimeOfDay)
                .ToDictionary(a => a.Key, a => a.Value);

            var result = new Dictionary<int, Dictionary<int, List<string>>>();

            foreach (var ring in data)
            {
                result.Add(ring.Key, new Dictionary<int, List<string>>());

                foreach (var dow in ring.Value)
                {
                    result[ring.Key].Add(dow.Key, new List<string>());

                    foreach (var tfd in dow.Value)
                    {
                        result[ring.Key][dow.Key].Add(tfd.Value[0].TeacherForDiscipline.Discipline.StudentGroup.Name + Environment.NewLine +
                            "(" + GetWeekStringFromLessons(tfd.Value) + ")@" +
                            tfd.Value[0].TeacherForDiscipline.Teacher.FIO + "@" + tfd.Value[0].TeacherForDiscipline.Discipline.Name);
                    }
                }
            }

            var dowEvents = _repo.AuditoriumEvents.GetFiltredAuditoriumEvents(evt => evt.Auditorium.AuditoriumId == auditoriumId);

            var eventId = 0;
            var eventsIds = new Dictionary<int, string>();
            var eventsData = new Dictionary<int, Dictionary<int, Dictionary<int, List<AuditoriumEvent>>>>();

            cToken.ThrowIfCancellationRequested();

            foreach (var evt in dowEvents)
            {
                if (!eventsData.ContainsKey(evt.Ring.RingId))
                {
                    eventsData.Add(evt.Ring.RingId, new Dictionary<int, Dictionary<int, List<AuditoriumEvent>>>());
                }

                if (!eventsData[evt.Ring.RingId].ContainsKey(Constants.Constants.DowRemap[(int)evt.Calendar.Date.DayOfWeek]))
                {
                    eventsData[evt.Ring.RingId].Add(Constants.Constants.DowRemap[(int)evt.Calendar.Date.DayOfWeek], new Dictionary<int, List<AuditoriumEvent>>());
                }

                var eventFound = (eventsIds.Count(e => e.Value == evt.Name) > 0);
                int curEventId;
                if (eventFound)
                {
                    curEventId = eventsIds.First(e => e.Value == evt.Name).Key;
                }
                else
                {
                    eventsIds.Add(eventId, evt.Name);
                    curEventId = eventId;
                    eventId++;
                }

                if (!eventsData[evt.Ring.RingId][Constants.Constants.DowRemap[(int)evt.Calendar.Date.DayOfWeek]].ContainsKey(curEventId))
                {
                    eventsData[evt.Ring.RingId][Constants.Constants.DowRemap[(int)evt.Calendar.Date.DayOfWeek]].Add(curEventId, new List<AuditoriumEvent>());
                }

                eventsData[evt.Ring.RingId][Constants.Constants.DowRemap[(int)evt.Calendar.Date.DayOfWeek]][curEventId].Add(evt);
            }

            cToken.ThrowIfCancellationRequested();

            foreach (var ring in eventsData)
            {
                cToken.ThrowIfCancellationRequested();

                if (!result.ContainsKey(ring.Key))
                {
                    result.Add(ring.Key, new Dictionary<int, List<string>>());
                }

                foreach (var dow in ring.Value)
                {
                    if (!result[ring.Key].ContainsKey(dow.Key))
                    {
                        result[ring.Key].Add(dow.Key, new List<string>());
                    }

                    foreach (var eventPair in dow.Value)
                    {
                        if (eventPair.Value[0].Name.Contains('@'))
                        {
                            var evtName = eventPair.Value[0].Name.Split('@')[0];
                            var evtHint = eventPair.Value[0].Name.Substring(evtName.Length + 1);
                            result[ring.Key][dow.Key].Add(evtName + Environment.NewLine + "( " + GetWeekStringFromEvents(eventPair.Value) + " )@" + evtHint);
                        }
                        else
                        {
                            result[ring.Key][dow.Key].Add(eventPair.Value[0].Name + Environment.NewLine + "( " + GetWeekStringFromEvents(eventPair.Value) + " )");
                        }
                    }
                }
            }

            cToken.ThrowIfCancellationRequested();

            result = result.OrderBy(r => rings[r.Key].TimeOfDay).ToDictionary(r => r.Key, r => r.Value);

            return result;
        }

        public string GetWeekStringFromLessons(IEnumerable<Lesson> list)
        {
            var weeksList = list.Select(lesson => CalculateWeekNumber(lesson.Calendar.Date)).ToList();

            string result = CombineWeeks(weeksList);

            return result;
        }

        public string GetWeekStringFromEvents(IEnumerable<AuditoriumEvent> list)
        {
            var weeksList = list.Select(lesson => CalculateWeekNumber(lesson.Calendar.Date)).ToList();

            var result = CombineWeeks(weeksList);

            return result;
        }

        public string GetWeekStringFromWishes(IEnumerable<TeacherWish> list)
        {
            var weeksList = list.Select(wish => CalculateWeekNumber(wish.Calendar.Date)).ToList();

            string result = CombineWeeks(weeksList);

            return result;
        }

        public int GetTfdHours(int tfdId, bool includeProposed, bool hoursCountWeekFiltered, int hoursCountWeekFilter)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                if (hoursCountWeekFiltered)
                {
                    var lessons = context.Lessons
                                    .Where(l =>
                                    ((l.State == 1) || ((l.State == 2) && includeProposed)) &&
                                    l.TeacherForDiscipline.TeacherForDisciplineId == tfdId)
                                    .ToList();
                    var count = lessons.Count(l => CalculateWeekNumber(l.Calendar.Date) == hoursCountWeekFilter)*2;
                    return count;
                }
                else
                {
                    return context.Lessons.Count(l =>
                               ((l.State == 1) || ((l.State == 2) && includeProposed)) &&
                               l.TeacherForDiscipline.TeacherForDisciplineId == tfdId) * 2;
                }
                
            }
        }

        public int GetTfdHours(int tfdId, bool includeProposed, bool hoursCountWeekFiltered, List<int> hoursCountWeekFilter)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var tefd = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == tfdId);
                if (tefd == null) return -1;

                var hoursPerLesson = -1;
                var building = _repo.Buildings.GetBuildingFromGroupName(tefd.Discipline.StudentGroup.Name);

                if (building.Name.Contains("Чапаевская") || tefd.Discipline.StudentGroup.Name.StartsWith("4"))
                {
                    hoursPerLesson = 1;
                }
                else
                {
                    hoursPerLesson = 2;
                }

                if (hoursCountWeekFiltered)
                {
                    var lessons = context.Lessons
                            .Where(l =>
                                    ((l.State == 1) || ((l.State == 2) && includeProposed)) &&

                                    l.TeacherForDiscipline.TeacherForDisciplineId == tfdId).ToList();
                    return lessons.Count(l => hoursCountWeekFilter.Contains(CalculateWeekNumber(l.Calendar.Date))) * hoursPerLesson;

                }
                else
                {
                    return context.Lessons.Count(l =>
                               ((l.State == 1) || ((l.State == 2) && includeProposed)) &&
                               l.TeacherForDiscipline.TeacherForDisciplineId == tfdId) * hoursPerLesson;
                }

            }
        }

        public int GetTfdProposedHours(int tfdId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Lessons.Count(l =>
                    (l.State == 2) &&
                    (l.TeacherForDiscipline.TeacherForDisciplineId == tfdId)) * 2;
            }
        }

        public int GetTfdLessonCount(int tfdId, bool includeProposed = false)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Lessons.Count(l =>
                    ((l.State == 1) || ((l.State == 2) && includeProposed)) &&
                    l.TeacherForDiscipline.TeacherForDisciplineId == tfdId);
            }
        }
        
        public DateTime GetSemesterStarts()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var semesterStartsOption = context
                    .Config
                    .FirstOrDefault(co => co.Key == "Semester Starts");
                if (semesterStartsOption == null)
                {
                    return new DateTime(2000, 1, 1);
                }

                return DateTime.ParseExact(semesterStartsOption.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
        }
    }
}
