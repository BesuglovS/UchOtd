using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Logs;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    class MySqlLessonLogEvent
    {

        public int LessonLogEventId { get; set; }
        public int OldLessonId { get; set; }
        public int NewLessonId { get; set; }
        public string DateTime { get; set; }
        public string PublicComment { get; set; }
        public string HiddenComment { get; set; }

        public MySqlLessonLogEvent(LessonLogEvent logEvent)
        {
            LessonLogEventId = logEvent.LessonLogEventId;
            if (logEvent.OldLesson != null)
            {
                OldLessonId = logEvent.OldLesson.LessonId;
            }
            else
            {
                OldLessonId = -1;
            }
            if (logEvent.NewLesson != null)
            {
                NewLessonId = logEvent.NewLesson.LessonId;
            }
            else
            {
                NewLessonId = -1;
            }
            DateTime = logEvent.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
            PublicComment = logEvent.PublicComment;
            HiddenComment = logEvent.HiddenComment;
        }

        public static List<MySqlLessonLogEvent> FromLessonLogList(IEnumerable<LessonLogEvent> list)
        {
            return list.Select(logEvent => new MySqlLessonLogEvent(logEvent)).ToList();
        }
    
    }
}
