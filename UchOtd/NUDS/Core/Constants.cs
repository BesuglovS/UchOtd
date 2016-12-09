using System;
using System.Collections.Generic;

namespace UchOtd.NUDS.Core
{
    public static class Constants
    {
        public static Dictionary<int, int> DowEnToRu = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 0, 7 } };
        public static Dictionary<int, int> DowRuToEn = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 0 } };

        public static Dictionary<int, string> DowRu = new Dictionary<int, string>
        {
                {1, "Понедельник"},
                {2, "Вторник"},
                {3, "Среда"},
                {4, "Четверг"},
                {5, "Пятница"},
                {6, "Суббота"},
                {7, "Воскресенье"},
        };

        // Session
        public static DateTime DefaultEmptyDateForEvent = new DateTime(2020, 1, 1);

        //public static DateTime DefaultEditDate = new DateTime(2014, 6, 9);
        public static DateTime DefaultEditDate = new DateTime(2017, 1, 9);
    }
}
