using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchOtd.Schedule.wnu.MySQLViews
{
    public class MySQLScheduleNote
    {
        public int ScheduleNoteId { get; set; }
        public int LessonId { get; set; }        
        public string Text { get; set; }
        public int LateAmount { get; set; }

        public MySQLScheduleNote(ScheduleNote note)
        {
            ScheduleNoteId = note.ScheduleNoteId;
            LessonId = note.Lesson.LessonId;
            Text = note.Text;
            LateAmount = note.LateAmount;
        }

        public static List<MySQLScheduleNote> FromNotes(IEnumerable<ScheduleNote> list)
        {
            return list.Select(note => new MySQLScheduleNote(note)).ToList();
        }
    }
}
