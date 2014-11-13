using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Logs;

namespace UchOtd.Schedule.Views
{
    public class LessonLogEventView
    {
        public int LessonLogEventId { get; set; }
        public string OldLesson { get; set; }
        public string NewLesson { get; set; }
        public string DateTime { get; set; }
        public string PublicComment { get; set; }
        public string HiddenComment { get; set; }        

        public LessonLogEventView()
        {
            
        }

        public LessonLogEventView(LessonLogEvent evt)
        {
            LessonLogEventId = evt.LessonLogEventId;
            if (evt.OldLesson == null)
            {
                OldLesson = "";
            }
            else
            {
                OldLesson = evt.OldLesson.TeacherForDiscipline.Discipline.StudentGroup.Name + Environment.NewLine +
                    evt.OldLesson.TeacherForDiscipline.Discipline.Name + Environment.NewLine +
                    evt.OldLesson.TeacherForDiscipline.Teacher.FIO + Environment.NewLine +
                    evt.OldLesson.Calendar.Date.ToString("d.MM.yyyy") + Environment.NewLine +
                    evt.OldLesson.Ring.Time.ToString("H:mm") + Environment.NewLine +
                    evt.OldLesson.Auditorium.Name;
            }
            if (evt.NewLesson == null)
            {
                NewLesson = "";
            }
            else
            {
                NewLesson = evt.NewLesson.TeacherForDiscipline.Discipline.StudentGroup.Name + Environment.NewLine +
                    evt.NewLesson.TeacherForDiscipline.Discipline.Name + Environment.NewLine +
                    evt.NewLesson.TeacherForDiscipline.Teacher.FIO + Environment.NewLine +
                    evt.NewLesson.Calendar.Date.ToString("d.MM.yyyy") + Environment.NewLine +
                    evt.NewLesson.Ring.Time.ToString("H:mm") + Environment.NewLine +
                    evt.NewLesson.Auditorium.Name;
            }
            DateTime = evt.DateTime.ToString("d MM yyyy HH:mm:ss");
            PublicComment = evt.PublicComment;
            HiddenComment = evt.HiddenComment;
        }

        public static List<LessonLogEventView> FromEventList(List<LessonLogEvent> list)
        {
            return list.Select(evt => new LessonLogEventView(evt)).ToList();
        }
    }
}
