using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Schedule.DomainClasses.Main;
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
            var studentIds = repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId && !sig.Student.Expelled)
                .Select(stig => stig.Student.StudentId)
                .ToList();

            var groupsListIds = repo
                .StudentsInGroups
                .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                .Select(stig => stig.StudentGroup.StudentGroupId)
                .Distinct()
                .ToList();
            return groupsListIds;
        }

        public static List<Lesson> GetGroupActiveLessons(ScheduleRepository repo, StudentGroup group)
        {
            var groupIds = StudentGroupIdsFromGroupId(repo, group.StudentGroupId);

            var lessons = repo.Lessons.GetFiltredLessons(l =>
                l.State == 1 &&
                groupIds.Contains(
                    l.TeacherForDiscipline.Discipline
                        .StudentGroup.StudentGroupId))
                .OrderBy(l => l.Calendar.Date.Date)
                .ThenBy(l => l.Ring.Time.TimeOfDay)
                .ToList();

            return lessons;
        }

        public static int GetLessonLengthFromGroupname(string groupName)
        {
            var split = groupName.Split(' ');
            if (split.Length == 1)
            {
                return 80;
            }

            var split0 = split[0];
            var digit = -1;
            try
            {
                digit = int.Parse(split0);
            }
            catch (Exception e)
            {
                return 80;
            }

            var group40 = new List<int> {1, 2, 3, 4, 5, 6, 7};

            if (group40.Contains(digit))
            {
                return 40;
            }

            return 80;
        }
    }
}
