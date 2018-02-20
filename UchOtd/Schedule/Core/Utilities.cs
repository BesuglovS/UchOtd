using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Schedule.DomainClasses.Main;
using Schedule.Repositories.Common;

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

        public static int FindLcm(int a, int b)
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

        // week + audName/LessonLength
        public static Dictionary<int,Tuple<string, int>> GetAudWeeksList(string weekAuds)
        {
            var result = new Dictionary<int, Tuple<string, int>>();
            var audsArray = weekAuds.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var auds = new List<string>(audsArray);

            if (auds.Count == 1)
            {

                var st = auds[0];

                var lessonLength = 0;

                if (st.Contains("("))
                {
                    var llb = st.IndexOf("(") + 1;
                    var lle = st.IndexOf(")") - 1;
                    var llStr = st.Substring(llb, lle - llb + 1);

                    try
                    {
                        lessonLength = int.Parse(llStr);
                    }
                    catch (Exception e)
                    {
                    }

                    st = st.Substring(0, llb - 2).Trim(' ');
                }

                result.Add(int.MinValue, new Tuple<string, int>(st, lessonLength));
            }
            else
            {
                foreach (var aud in auds)
                {
                    var dashPos = aud.IndexOf(" - ", StringComparison.Ordinal);
                    var weeksString = aud.Substring(0, dashPos);
                    var weeks = CommonFunctions.WeeksStringToList(weeksString);
                    var auditorium = aud.Substring(dashPos + 3, aud.Length - (dashPos + 3));
                    foreach (var week in weeks)
                    {
                        if (!result.ContainsKey(week))
                        {
                            result.Add(week, null);
                        }

                        result[week] = new Tuple<string, int>(auditorium, week);
                    }
                }
            }

            return result;
        }
        
        internal static string ExtractDbOrConnectionName(string connectionString)
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
