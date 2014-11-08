using System;
using System.Collections.Generic;
using UchOtd.Schedule.Analysis;

namespace Schedule.Constants
{
    public static class Constants
    {

        public static DateTime DefaultEmptyDateForEvent = new DateTime(2020, 1, 1);

        public static Dictionary<int, int> DOWRemap = new Dictionary<int, int> 
        { { 0, 7 }, { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 } };

        public static Dictionary<int, string> DOWLocal = new Dictionary<int, string> { 
            { 1, "Понедельник" }, 
            { 2, "Вторник" }, 
            { 3, "Среда" }, 
            { 4, "Четверг" }, 
            { 5, "Пятница" }, 
            { 6, "Суббота" }, 
            { 7, "Воскресенье" } 
        };

        public static Dictionary<string, int> DOWLocalReverse = new Dictionary<string, int> { 
            { "Понедельник", 1 }, 
            { "Вторник", 2 }, 
            { "Среда", 3 }, 
            { "Четверг", 4 }, 
            { "Пятница", 5 }, 
            { "Суббота", 6 }, 
            { "Воскресенье", 7 } 
        };

        // 0 - ничего; 1 - зачёт; 2 - экзамен; 3 - зачёт и экзамен
        public static Dictionary<int, string> Attestation = new Dictionary<int, string> {
            { 0, "-" }, 
            { 1, "Зачёт" }, 
            { 2, "Экзамен" }, 
            { 3, "Зачёт + Экзамен" },
            { 4, "Зачёт с оценкой" }
        };

        // Public Comment on Lesson Add
        public static List<string> LessonAddPublicComment = new List<string>
        {
            "Первоначальная постановка в расписание",
            "Добавление занятий для соответствия учебному плану"
        };

        // Calendar State Description Dictionary
        public static Dictionary<int, string> CalendarStateDescription = new Dictionary<int, string>()
        {
            {0, "Обычный"},
            {1, "Праздник"}
        };

        public static List<LogLevel> LogLevels = new List<LogLevel> { 
            LogLevel.ErrorsOnly, 
            LogLevel.ErrorsAndWarnings,
            LogLevel.Normal,
            LogLevel.Max
        };
    }
}
