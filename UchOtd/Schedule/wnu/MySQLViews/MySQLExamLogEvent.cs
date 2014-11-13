﻿using Schedule.DomainClasses.Session;
using System.Collections.Generic;
using System.Linq;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    public class MySqlExamLogEvent
    {
        public int LogEventId { get; set; }
        public int OldExamId { get; set; }
        public int NewExamId { get; set; }
        public string DateTime { get; set; }

        public static List<MySqlExamLogEvent> FromLogEventList(List<LogEvent> list)
        {
            return list
                .Select(logEvent => new MySqlExamLogEvent
                {
                    LogEventId = logEvent.LogEventId,
                    OldExamId = logEvent.OldExam.ExamId,
                    NewExamId = logEvent.NewExam.ExamId,
                    DateTime = logEvent.DateTime.ToString("dd.MM.yyyy H:mm")
                })
                .ToList();
        }
    }
}
