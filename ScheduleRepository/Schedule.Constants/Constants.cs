﻿using System;
using System.Collections.Generic;
using Schedule.Constants.Analysis;

namespace Schedule.Constants
{
    public static class Constants
    {
        public static Dictionary<int, string>  LessonTypeAbbreviation = new Dictionary<int, string>()
        {
            { 0, "*" }, // Нет данных
            { 1, "Л" }, // Лекция
            { 2, "П" }, // Практика
            { 3, "ЛП" }, // Лекция / Практика
            { 4, "ПЛ" }, // Практика / Лекция
            { 5, "И" }, // Индивидуальные занятия
        };

        public static Dictionary<int, string> LessonTypeLongAbbreviation = new Dictionary<int, string>()
        {
            { 0, "*" }, // Нет данных
            { 1, "Лекция" }, // Лекция
            { 2, "Практика" }, // Практика
            { 3, "Лекция/Практика" }, // Лекция / Практика
            { 4, "Практика/Лекция" }, // Практика / Лекция
            { 5, "Индивидуальные занятия" }, // Индивидуальные занятия
        };

        public static DateTime DefaultEmptyDateForEvent = new DateTime(2020, 1, 1);

        public static Dictionary<int, int> DowRemap = new Dictionary<int, int> 
        { { 0, 7 }, { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 } };

        public static Dictionary<int, string> DowLocal = new Dictionary<int, string> { 
            { 1, "Понедельник" }, 
            { 2, "Вторник" }, 
            { 3, "Среда" }, 
            { 4, "Четверг" }, 
            { 5, "Пятница" }, 
            { 6, "Суббота" }, 
            { 7, "Воскресенье" } 
        };

        public static Dictionary<string, int> DowLocalReverse = new Dictionary<string, int> { 
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
        public static Dictionary<int, string> CalendarStateDescription = new Dictionary<int, string>
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

        public static List<String> SitesUploadEndPoints = new List<string>
        {
            "http://wiki.nayanova.edu/_php/includes/import.php",
            "http://wiki.nayanova.edu/new/api/import.php",
            "http://wiki.nayanova.edu/api/import.php",            
            "http://schedule.lv/api/import.php"
        };

        public static int schoolEndPointIndex = 0;

        public static Dictionary<int, string> RuMonthNames = new Dictionary<int, string>
        {
            { 1, "Январь" },
            { 2, "Февраль" },
            { 3, "Март" },
            { 4, "Апрель" },
            { 5, "Май" },
            { 6, "Июнь" },
            { 7, "Июль" },
            { 8, "Август" },
            { 9, "Сентябрь" },
            { 10, "Октябрь" },
            { 11, "Ноябрь" },
            { 12, "Декабрь" },
        };
    }
}
