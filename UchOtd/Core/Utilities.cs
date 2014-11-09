using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DomainClasses.Session;

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
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static bool MainGroups(string groupName)
        {
            if (!groupName.Contains('I') && 
                !groupName.Contains('-') && 
                !groupName.Contains('+') && 
                !groupName.Contains(".)"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static string ExtractDbOrConnectionName(string connectionString)
        {
            if (connectionString.StartsWith("Name="))
            {
                return connectionString.Substring(5);
            }
            else
            {
                int startIndex = connectionString.IndexOf("Database=") + 9;
                int endIndex = -1;
                if (startIndex != 0)
                {
                    endIndex = connectionString.IndexOf(';', startIndex);
                }

                return connectionString.Substring(startIndex, endIndex - startIndex);
            }
        }
    }
}
