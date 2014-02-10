using System.Collections.Generic;

namespace UchOtd.NUDS.Core
{
    public static class Constants
    {
        public static Dictionary<int, int> DOWEnToRu = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 0, 7 } };
        public static Dictionary<int, int> DOWRuToEn = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 0 } };

        public static Dictionary<int, string> DOWRU = new Dictionary<int, string>
                                                          {
                {1, "Понедельник"},
                {2, "Вторник"},
                {3, "Среда"},
                {4, "Четверг"},
                {5, "Пятница"},
                {6, "Суббота"},
                {7, "Воскресенье"},
            };

        public static Dictionary<int, int> DOWRemap = new Dictionary<int, int> { { 0, 7 }, { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 } };
        
    }
}
