using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
    }
}
