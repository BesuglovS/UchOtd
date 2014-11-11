using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Core
{
    public static class Utilities
    {
        public class AudComparer: IComparer<Auditorium>
        {
            public int Compare(Auditorium x, Auditorium y)
            {
                if ((x.Name.StartsWith("Ауд. 3")) && (!y.Name.StartsWith("Ауд. 3")))
                {
                    return -1;
                }

                if ((!x.Name.StartsWith("Ауд. 3")) && (y.Name.StartsWith("Ауд. 3")))
                {
                    return 1;
                }

                return String.CompareOrdinal(x.Name, y.Name);
            }
        }

        public static void DebugLog(string message)
        {
            var sw = new StreamWriter("DebugLog.txt", true);
            sw.WriteLine(message);
            sw.Close();
        }

        public static int FindLCM(int a, int b)
        {
            int num1, num2;
            if (a > b)
            {
                num1 = a; num2 = b;
            }
            else
            {
                num1 = b; num2 = a;
            }

            for (int i = 1; i <= num2; i++)
            {
                if ((num1 * i) % num2 == 0)
                {
                    return i * num1;
                }
            }
            return num2;
        }

        public static Dictionary<int,string> GetAudWeeksList(string weekAuds)
        {
            var result = new Dictionary<int, string>();
            var audsArray = weekAuds.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var auds = new List<string>(audsArray);

            if (auds.Count == 1)
            {
                result.Add(0, auds[0]);
            }
            else
            {
                foreach (var aud in auds)
                {
                    var dashPos = aud.IndexOf(" - ", StringComparison.Ordinal);
                    var weeksString = aud.Substring(0, dashPos);
                    var weeks = ConvertWeeksToList(weeksString);
                    var auditorium = aud.Substring(dashPos + 3, aud.Length - (dashPos + 3));
                    foreach (var week in weeks)
                    {
                        if (!result.ContainsKey(week))
                        {
                            result.Add(week, auditorium);
                        }
                    }
                }
            }

            return result;
        }

        public static List<int> ConvertWeeksToList(string p, bool removeParentheses = false)
        {
            var result = new List<int>();

            string str = p;
            if (removeParentheses)
            {
                str = p.Substring(0, p.Length - 1);
                str = str.Substring(1, str.Length - 1);
            }

            int mods = 0; // 0 - нет; 1 - нечётные; 2 - чётные

            foreach (var item in str.Split(','))
            {
                var st = item.Trim(' ');

                if (!st.Contains('-'))
                {
                    result.Add(int.Parse(st));
                }
                else
                {

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

        internal static string ExtractDBOrConnectionName(string connectionString)
        {
            if (connectionString.StartsWith("Name="))
            {
                return connectionString.Substring(5);
            }

            int startIndex = connectionString.IndexOf("Database=", StringComparison.Ordinal) + 9;
            int endIndex = -1;
            if (startIndex != 0)
            {
                endIndex = connectionString.IndexOf(';', startIndex);
            }

            return connectionString.Substring(startIndex, endIndex - startIndex);
        }
    }
}
