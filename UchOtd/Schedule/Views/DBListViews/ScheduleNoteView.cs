using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Main;

namespace UchOtd.Schedule.Views.DBListViews
{
    public class ScheduleNoteView
    {
        public int ScheduleNoteId { get; set; }
        public string Text { get; set; }
        public string LessonString { get; set; }
        public int LateAmount { get; set; }

        public ScheduleNoteView(ScheduleNote note)
        {
            ScheduleNoteId = note.ScheduleNoteId;
            Text = note.Text;
            LateAmount = note.LateAmount;
            
            LessonString = ((note.Lesson != null) && (note.IsLesson))
                ? ((note.Lesson.State == 1) ? "+" : ((note.Lesson.State == 0) ? "-" : note.Lesson.State.ToString())) + " " + 
                  note.Lesson.Calendar.Date.ToString("dd.MM.yyyy") + " " +
                  note.Lesson.Ring.Time.ToString("HH:mm") + " " +
                  note.Lesson.TeacherForDiscipline.Teacher.FIO + " " +
                  note.Lesson.TeacherForDiscipline.Discipline.Name + " " +
                  note.Lesson.TeacherForDiscipline.Discipline.StudentGroup.Name
                : "";
        }

        public static List<ScheduleNoteView> NotesToView(List<ScheduleNote> list)
        {
            return list.Select(note => new ScheduleNoteView(note)).ToList();
        }
    }
}
