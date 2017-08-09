using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Schedule.Repositories;

namespace UchOtd.Core
{
    public static class Utilities
    {
        public static void Log(string filename, string message)
        {
            var sw = new StreamWriter(filename, true);
            sw.WriteLine(message);
            sw.Close();
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        public static bool MainGroups(string groupName)
        {
            return !groupName.Contains('I') && 
                   !groupName.Contains('-') && 
                   !groupName.Contains('+') && 
                   !groupName.Contains(".)");
        }

        internal static string ExtractDbOrConnectionName(string connectionString)
        {
            if (connectionString.StartsWith("Name="))
            {
                return connectionString.Substring(5);
            }

            var startIndex = connectionString.IndexOf("Database=", StringComparison.Ordinal) + 9;
            var endIndex = -1;
            if (startIndex != 0)
            {
                endIndex = connectionString.IndexOf(';', startIndex);
            }

            return connectionString.Substring(startIndex, endIndex - startIndex);
        }

        public static List<int> StudentGroupIdsFromGroupId(ScheduleRepository repo, int groupId)
        {
            var group = repo.StudentGroups.GetStudentGroup(groupId);

            var studentInGroups = repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId);

            var studentIdAndPeriods = new Dictionary<int, List<Tuple<DateTime, DateTime>>>();
            for (int i = 0; i < studentInGroups.Count; i++)
            {
                var sig = studentInGroups[i];

                if (!studentIdAndPeriods.ContainsKey(sig.Student.StudentId))
                {
                    studentIdAndPeriods.Add(sig.Student.StudentId, new List<Tuple<DateTime, DateTime>>());
                }

                studentIdAndPeriods[sig.Student.StudentId].Add(new Tuple<DateTime, DateTime>(sig.PeriodFrom, sig.PeriodTo));
            }


            var groupsListIds = repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.Semester.SemesterId == group.Semester.SemesterId &&
                                                   studentIdAndPeriods.ContainsKey(sig.Student.StudentId) &&
                                                   PeriodIntersectsWithGroup(new Tuple<DateTime, DateTime>(sig.PeriodFrom, sig.PeriodTo), studentIdAndPeriods[sig.Student.StudentId]))
                .Select(stig => stig.StudentGroup.StudentGroupId)
                .Distinct()
                .ToList();

            return groupsListIds;
        }

        public static bool DateInRange(DateTime date, DateTime startOfThePeriod, DateTime endOfThePeriod)
        {
            var date1 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            var atart1 = new DateTime(startOfThePeriod.Year, startOfThePeriod.Month, startOfThePeriod.Day, 0, 0, 0);
            var end1 = new DateTime(endOfThePeriod.Year, endOfThePeriod.Month, endOfThePeriod.Day, 0, 0, 0);

            return ((date1 >= atart1) && (date1 <= end1));
        }

        public static bool PeriodsIntersects(Tuple<DateTime, DateTime> period1, Tuple<DateTime, DateTime> period2)
        {
            return period1.Item1 <= period2.Item2 && period2.Item1 <= period1.Item2;
        }

        public static bool PeriodIntersectsWithGroup(Tuple<DateTime, DateTime> period1, List<Tuple<DateTime, DateTime>> periodList)
        {
            return periodList.Any(period2 => period1.Item1 <= period2.Item2 && period2.Item1 <= period1.Item2);
        }

        public static List<string> DatesToStringTimeSpans(List<DateTime> dates)
        {
            var result = new List<string>();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime lastDate = baseDate;
            int spanLength = 0;
            for (int i = 0; i < dates.Count; i++)
            {
                var dt = dates[i];

                if (DateTime.Compare(dt, lastDate.AddDays(1)) == 0) // Next date is next day
                {
                    spanLength++;
                }
                else
                {
                    if (spanLength != 0)
                    {
                        if (spanLength > 2)
                        {
                            result.Add(baseDate.ToString("dd.MM.yyyy") + " - " + dates[i - 1].ToString("dd.MM.yyyy"));
                        }
                        else
                        {
                            if (spanLength == 1)
                            {
                                result.Add(baseDate.ToString("dd.MM.yyyy"));
                            }
                            else // spanLength == 2
                            {
                                result.Add(baseDate.ToString("dd.MM.yyyy"));
                                result.Add(dates[i - 1].ToString("dd.MM.yyyy"));
                            }
                        }
                    }

                    baseDate = dt;
                    spanLength = 1;
                }

                lastDate = dt;
            }

            if (spanLength > 2)
            {
                result.Add(baseDate.ToString("dd.MM.yyyy") + " - " + dates[dates.Count - 1].ToString("dd.MM.yyyy"));
            }
            else
            {
                if (spanLength == 1)
                {
                    result.Add(baseDate.ToString("dd.MM.yyyy"));
                }
                else // spanLength == 2
                {
                    result.Add(baseDate.ToString("dd.MM.yyyy"));
                    result.Add(dates[dates.Count - 1].ToString("dd.MM.yyyy"));
                }
            }

            return result;
        }

        public static List<Tuple<DateTime, DateTime>> DatesToTimeSpans(List<DateTime> dates)
        {
            var result = new List<Tuple<DateTime, DateTime>>();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime lastDate = baseDate;
            int spanLength = 0;
            for (int i = 0; i < dates.Count; i++)
            {
                var dt = dates[i];

                if (DateTime.Compare(dt, lastDate.AddDays(1)) == 0) // Next date is next day
                {
                    spanLength++;
                }
                else
                {
                    if (spanLength != 0)
                    {
                        if (spanLength > 2)
                        {
                            result.Add(new Tuple<DateTime, DateTime>(baseDate, dates[i - 1]));
                        }
                        else
                        {
                            if (spanLength == 1)
                            {
                                result.Add(new Tuple <DateTime, DateTime> (baseDate, baseDate));
                            }
                            else // spanLength == 2
                            {
                                result.Add(new Tuple<DateTime, DateTime>(baseDate, baseDate));
                                result.Add(new Tuple<DateTime, DateTime>(dates[i - 1], dates[i - 1]));
                            }
                        }
                    }

                    baseDate = dt;
                    spanLength = 1;
                }

                lastDate = dt;
            }

            if (spanLength > 2)
            {
                result.Add(new Tuple<DateTime, DateTime>(baseDate, dates[dates.Count - 1]));
            }
            else
            {
                if (spanLength == 1)
                {
                    result.Add(new Tuple<DateTime, DateTime>(baseDate, baseDate));
                }
                else // spanLength == 2
                {
                    result.Add(new Tuple<DateTime, DateTime>(baseDate, baseDate));
                    result.Add(new Tuple<DateTime, DateTime>(dates[dates.Count - 1], dates[dates.Count - 1]));
                }
            }

            return result;
        }

        public static List<string> StudentsFioListFromIds(List<int> studentIds, ScheduleRepository repo)
        {
            return repo.Students
                .GetFiltredStudents(st => studentIds.Contains(st.StudentId))
                .Select(s => s.F + " " + s.I + " " + s.O)
                .OrderBy(a => a)
                .ToList();
        }
    }
}
