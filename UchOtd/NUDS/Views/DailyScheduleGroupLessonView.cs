using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUDispSchedule.Main;

namespace NUDispSchedule.Views
{
    public class DailyScheduleGroupLessonView
    {
        public DailyScheduleGroupLessonView(Lesson l, int groupId)
        {
            LessonId = l.LessonId;
            Ring = l.Ring.Time.ToString("H:mm");
            if (l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId != groupId)
            {
                LessonSummary = l.TeacherForDiscipline.Discipline.StudentGroup.Name + "\n";
            }
            LessonSummary += l.TeacherForDiscipline.Discipline.Name + "\n";
            LessonSummary += l.TeacherForDiscipline.Teacher.FIO + "\n";
            LessonSummary += l.Auditorium.Name;
        }

        public static List<DailyScheduleGroupLessonView> FromLessonsList(List<Lesson> lList, int groupId)
        {
            return lList.Select(lesson => new DailyScheduleGroupLessonView(lesson, groupId)).ToList();
        }

        public int LessonId { get; set; }
        public string Ring { get; set; }
        public string LessonSummary { get; set; }
    }
}
