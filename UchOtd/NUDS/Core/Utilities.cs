using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using IWshRuntimeLibrary;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;

namespace UchOtd.NUDS.Core
{
    public static class Utilities
    {
        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation, string shortcutDescription)
        {
            string shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = shortcutDescription;                                                             // The description of the shortcut
            shortcut.IconLocation = Assembly.GetExecutingAssembly().GetName().CodeBase + ", 0";   // The icon of the shortcut            
            shortcut.TargetPath = targetFileLocation + " -Startup";                                                // The path of the file that will launch when the shortcut is run
            shortcut.Save();                                                                                        // Save the shortcut
        }

        public static List<Lesson> GetDailySchedule(ScheduleRepository repo, int groupId, DateTime date, bool limitToExactGroup, bool showProposed)
        {
            List<Lesson> result;
            if (limitToExactGroup)
            {
                result = repo
                    .Lessons
                    .GetFiltredLessons(l =>
                        ((l.State == 1) || ((l.State == 2) && showProposed)) &&
                        (l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId == groupId) &&
                        (l.Calendar.Date == date))
                    .OrderBy(l => l.Ring.Time.TimeOfDay)
                    .ToList();
            }
            else
            {
                var studentIds = repo
                    .StudentsInGroups
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId && !sig.Student.Expelled)
                    .Select(stig => stig.Student.StudentId)
                    .ToList();

                var groupIds = repo
                    .StudentsInGroups
                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .Select(stig => stig.StudentGroup.StudentGroupId)
                    .Distinct()
                    .ToList();

                result = repo
                    .Lessons
                    .GetFiltredLessons(l =>
                        (groupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId)) &&
                        (l.Calendar.Date.Date == date.Date) &&
                        ((l.State == 1) || ((l.State == 2) && showProposed)))
                    .OrderBy(l => l.Ring.Time.TimeOfDay)
                    .ToList();

            }

            return result;
        }
    }
}
