using System;
using System.Collections.Generic;

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

        public class Building
        {
            public int id { get; set; }
            public string Name { get; set; }
        }

        public static List<Building> Buildings = new List<Building> { 
            new Building { id = 2, Name = "ул. Молодогвардейская, 196" },
            new Building { id = 3, Name = "ул. Ярмарочная, 17" },
            new Building { id = 0, Name = "Прочие" }
        };                
    }
}
