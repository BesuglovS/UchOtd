using NUDispSchedule.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUDispSchedule.Views
{
    public class lleView
    {
        public int EventId { get; set; }
        public string EventDate { get; set; }
        public int EventType { get; set; } // -1 - unknown; 1 - Lesson Added; 2 - Lesson removed; 3 - Auditorium changed
        public string Message { get; set; }

        public static List<lleView> ListFromLessonLogEvents(List<LessonLogEvent> list)
        {
            var result = new List<lleView>();
            foreach (var ev in list)
            {
                result.Add(new lleView(ev));
            }
            return result;
        }

        public lleView(LessonLogEvent e)
        {
            this.EventId = e.LessonLogEventId;

            this.EventDate = e.DateTime.ToString("dd MM yyyy HH:mm:ss");
            
            this.EventType = -1;
            if ((e.OldLesson == null) && (e.NewLesson != null))
            {
                this.EventType = 1;
            }
            else
            {
                if ((e.OldLesson != null) && (e.NewLesson == null))
                {
                    this.EventType = 2;
                }
                else
                {
                    if ((e.OldLesson.TeacherForDiscipline.TeacherForDisciplineId == e.NewLesson.TeacherForDiscipline.TeacherForDisciplineId) &&
                        (e.OldLesson.Auditorium.AuditoriumId != e.NewLesson.Auditorium.AuditoriumId))
                    {
                        this.EventType = 3;
                    }
                }
            }
            
            switch (EventType)
            {
                case 1:
                    Message  = e.NewLesson.TeacherForDiscipline.Discipline.StudentGroup.Name + Environment.NewLine;
                    Message += e.NewLesson.Calendar.Date.ToString("dd.MM.yyyy") + " - " + e.NewLesson.Ring.Time.ToString("H:mm") + Environment.NewLine;
                    Message += e.NewLesson.TeacherForDiscipline.Discipline.Name + Environment.NewLine;
                    Message += e.NewLesson.TeacherForDiscipline.Teacher.FIO + Environment.NewLine;
                    Message += e.NewLesson.Auditorium.Name;
                    break;
                case 2:
                    Message  = e.OldLesson.TeacherForDiscipline.Discipline.StudentGroup.Name + Environment.NewLine;
                    Message += e.OldLesson.Calendar.Date.ToString("dd.MM.yyyy") + " - " + e.OldLesson.Ring.Time.ToString("H:mm") + Environment.NewLine;
                    Message += e.OldLesson.TeacherForDiscipline.Discipline.Name + Environment.NewLine;
                    Message += e.OldLesson.TeacherForDiscipline.Teacher.FIO + Environment.NewLine;
                    Message += e.OldLesson.Auditorium.Name;
                    break;
                case 3:
                    Message  = e.NewLesson.TeacherForDiscipline.Discipline.StudentGroup.Name + Environment.NewLine;
                    Message += e.NewLesson.Calendar.Date.ToString("dd.MM.yyyy") + " - " + e.NewLesson.Ring.Time.ToString("H:mm") + Environment.NewLine;
                    Message += e.NewLesson.TeacherForDiscipline.Discipline.Name + Environment.NewLine;
                    Message += e.NewLesson.TeacherForDiscipline.Teacher.FIO + Environment.NewLine;
                    Message += e.OldLesson.Auditorium.Name + " => " + e.NewLesson.Auditorium.Name;
                    break;
                default:
                    break;
            }
        }
    }
}
