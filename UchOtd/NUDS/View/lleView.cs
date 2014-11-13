using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Logs;

namespace UchOtd.NUDS.View
{
    public class LleView
    {
        public int EventId { get; set; }
        public string EventDate { get; set; }
        public int EventType { get; set; } // -1 - unknown; 1 - Lesson Added; 2 - Lesson removed; 3 - Auditorium changed
        public string Message { get; set; }

        public static List<LleView> ListFromLessonLogEvents(List<LessonLogEvent> list)
        {
            return list.Select(ev => new LleView(ev)).ToList();
        }

        public LleView(LessonLogEvent e)
        {
            EventId = e.LessonLogEventId;

            EventDate = e.DateTime.ToString("dd MM yyyy HH:mm:ss");
            
            EventType = -1;
            if ((e.OldLesson == null) && (e.NewLesson != null))
            {
                EventType = 1;
            }
            else
            {
                if ((e.OldLesson != null) && (e.NewLesson == null))
                {
                    EventType = 2;
                }
                else
                {
                    
                    if ((e.OldLesson.TeacherForDiscipline.TeacherForDisciplineId ==
                            e.NewLesson.TeacherForDiscipline.TeacherForDisciplineId) &&
                        (e.OldLesson.Auditorium.AuditoriumId != e.NewLesson.Auditorium.AuditoriumId))
                    {
                        EventType = 3;
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
            }
        }
    }
}
