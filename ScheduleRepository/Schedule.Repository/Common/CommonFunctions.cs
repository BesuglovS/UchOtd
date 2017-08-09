﻿using System;
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
        }

        public static string CombineWeeks(List<int> list)
        {
            var result = new List<string>();
            const int maxWeek = 54;
            var boolWeeks = new bool[maxWeek + 1];

            for (var i = 0; i <= maxWeek; i++)
            {
                boolWeeks[i] = list.Contains(i);
            }

            bool prev = false;
            int baseNum = maxWeek;
            for (var i = 1; i <= maxWeek - 2; i++)
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
            for (var i = 1; i <= maxWeek; i += 2)
            {
                if (!prev && boolWeeks[i])
                {
                    baseNum = i;
                }

                if (!boolWeeks[i] && ((i - baseNum) > 4))
                {
                    result.Add(baseNum + "-" + (i - 2) + " (нечёт.)");

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
            for (var i = 2; i <= maxWeek; i += 2)
            {
                if (!prev && boolWeeks[i])
                {
                    baseNum = i;
                }

                if (!boolWeeks[i] && ((i - baseNum) > 4))
                {
                    result.Add(baseNum + "-" + (i - 2) + " (чёт.)");

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



            for (var i = 1; i <= maxWeek; i++)
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

            var final = result.Count == 0 ? "" : result.Aggregate((current, str) => current + ", " + str);
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

                    int start = int.Parse(st.Substring(0, st.IndexOf('-')));

                    int end = int.Parse(
                        st.Substring(st.IndexOf('-') + 1, st.Length - st.IndexOf('-') - 1));

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

            return result;
        }

        public DateTime GetDateFromDowAndWeek(int week, int dow, Semester semester)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var semesterStartsOption = context.Config
                    .FirstOrDefault(co => 
                        co.Key == "Semester Starts" &&
                        co.Semester.SemesterId == semester.SemesterId);
                if (semesterStartsOption == null)
                {
                    return new DateTime(2000, 1, 1);
                }

                var semesterStarts = DateTime.ParseExact(semesterStartsOption.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var ssDow = (semesterStarts.DayOfWeek != DayOfWeek.Sunday) ? (int)semesterStarts.DayOfWeek : 7;

                return semesterStarts.AddDays((-1) * (ssDow - 1) + (week - 1) * 7 + dow - 1);
            }
        }

        public Calendar GetCalendarFromDowAndWeek(Semester semester, int dow, int week)
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

        public int CalculateWeekNumber(Semester semester, DateTime dateTime)
        {
            var semesterStarts = GetSemesterStarts(semester);
            var ssDow = (semesterStarts.DayOfWeek != DayOfWeek.Sunday) ? (int)semesterStarts.DayOfWeek : 7;

            var ssWeeksMonday = semesterStarts.AddDays((-1) * (ssDow - 1));

            return (dateTime - ssWeeksMonday).Days / 7 + 1;
        }

        public Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>> GetGroupedGroupsLessons(Semester semester, List<int> groupListIds, bool showProposed, CancellationToken cToken)
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
                        .Where(sig => sig.StudentGroup.StudentGroupId == groupId)
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
                                .Select(lesson => CalculateWeekNumber(semester, lesson.Calendar.Date.Date))
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
        public Dictionary<int, Dictionary<int, List<string>>> GetDowAuds(Semester semester, DayOfWeek dow, int weekNumber, int buildingId, bool showProposed)
        {
            var data = new Dictionary<int, Dictionary<int, Dictionary<int, List<Lesson>>>>();

            List<Lesson> dowLessons;
            if (weekNumber == -1)
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
                            CalculateWeekNumber(semester, l.Calendar.Date) == weekNumber)
                        .ToList();
                }
                else
                {
                    dowLessons = _repo.Lessons.GetFiltredLessons(l =>
                            l.Calendar.Date.DayOfWeek == dow &&
                            ((l.State == 1) || ((l.State == 2) && showProposed)) &&
                            CalculateWeekNumber(semester, l.Calendar.Date) == weekNumber &&
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
                            "(" + GetWeekStringFromLessons(semester, tfd.Value) + ")@" +
                            tfd.Value[0].TeacherForDiscipline.Teacher.FIO + "@" + tfd.Value[0].TeacherForDiscipline.Discipline.Name);
                    }
                }
            }

            List<AuditoriumEvent> audEvents;
            if (weekNumber == -1)
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
                        CalculateWeekNumber(semester, evt.Calendar.Date) == weekNumber);
                }
                else
                {
                    audEvents = _repo.AuditoriumEvents.GetFiltredAuditoriumEvents(evt =>
                        evt.Calendar.Date.DayOfWeek == dow &&
                        CalculateWeekNumber(semester, evt.Calendar.Date) == weekNumber &&
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
                            result[ring.Key][aud.Key].Add(evtName + Environment.NewLine + "( " + GetWeekStringFromEvents(semester, eventPair.Value) + " )@" + evtHint);
                        }
                        else
                        {
                            result[ring.Key][aud.Key].Add(eventPair.Value[0].Name + Environment.NewLine + "( " + GetWeekStringFromEvents(semester, eventPair.Value) + " )");
                        }
                    }
                }
            }

            result = result.OrderBy(r => rings[r.Key].TimeOfDay).ToDictionary(r => r.Key, r => r.Value);

            return result;
        }

        // data   - Dictionary<RingId, Dictionary <dow, List<Dictionary<tfd, List<Lesson>>>>>
        // result - Dictionary<RingId, Dictionary <dow, List<tfd/Event-string>>>
        public Dictionary<int, Dictionary<int, List<string>>> GetAud(Semester semester, int auditoriumId, 
            bool showProposed, CancellationToken cToken)
        {
            var data = new Dictionary<int, Dictionary<int, Dictionary<int, List<Lesson>>>>();

            cToken.ThrowIfCancellationRequested();

            var audLessons = _repo.Lessons.GetFiltredLessons(l =>
                l.TeacherForDiscipline.Discipline.Semester.SemesterId == semester.SemesterId &&
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
                            "(" + GetWeekStringFromLessons(semester, tfd.Value) + ")@" +
                            tfd.Value[0].TeacherForDiscipline.Teacher.FIO + "@" + tfd.Value[0].TeacherForDiscipline.Discipline.Name);
                    }
                }
            }

            var semesterStart = _repo.ConfigOptions.GetSemesterStart(semester);
            var ssDate = DateTime.ParseExact(semesterStart.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var semesterEnd = _repo.ConfigOptions.GetSemesterEnd(semester);
            var seDate = DateTime.ParseExact(semesterEnd.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var dowEvents = _repo.AuditoriumEvents.GetFiltredAuditoriumEvents(evt => 
                evt.Auditorium.AuditoriumId == auditoriumId && evt.Calendar.Date.Date >= ssDate &&
                evt.Auditorium.AuditoriumId == auditoriumId && evt.Calendar.Date.Date <= seDate );

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
                            result[ring.Key][dow.Key].Add(evtName + Environment.NewLine + "( " + GetWeekStringFromEvents(semester, eventPair.Value) + " )@" + evtHint);
                        }
                        else
                        {
                            result[ring.Key][dow.Key].Add(eventPair.Value[0].Name + Environment.NewLine + "( " + GetWeekStringFromEvents(semester, eventPair.Value) + " )");
                        }
                    }
                }
            }

            cToken.ThrowIfCancellationRequested();

            result = result.OrderBy(r => rings[r.Key].TimeOfDay).ToDictionary(r => r.Key, r => r.Value);

            return result;
        }

        public string GetWeekStringFromLessons(Semester semester, IEnumerable<Lesson> list)
        {
            var weeksList = list.Select(lesson => CalculateWeekNumber(semester, lesson.Calendar.Date)).ToList();

            string result = CombineWeeks(weeksList);

            return result;
        }

        public string GetWeekStringFromEvents(Semester semester, IEnumerable<AuditoriumEvent> list)
        {
            var weeksList = list.Select(lesson => CalculateWeekNumber(semester, lesson.Calendar.Date)).ToList();

            var result = CombineWeeks(weeksList);

            return result;
        }

        public string GetWeekStringFromWishes(Semester semester, IEnumerable<TeacherWish> list)
        {
            var weeksList = list.Select(wish => CalculateWeekNumber(semester, wish.Calendar.Date)).ToList();

            string result = CombineWeeks(weeksList);

            return result;
        }

        public int GetTfdHours(int tfdId, bool includeProposed = false)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Lessons.Count(l =>
                    ((l.State == 1) || ((l.State == 2) && includeProposed)) &&
                    l.TeacherForDiscipline.TeacherForDisciplineId == tfdId) * 2;
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
        
        public DateTime GetSemesterStarts(Semester semester)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var semesterStartsOption = context
                    .Config
                    .FirstOrDefault(co => co.Key == "Semester Starts" && co.Semester.SemesterId == semester.SemesterId);

                return semesterStartsOption == null ? 
                    new DateTime() : 
                    DateTime.ParseExact(semesterStartsOption.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
        }
    }
}
