using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule;
using UchOtd.Schedule.Core;
using Calendar = Google.Apis.Calendar.v3.Data.Calendar;

namespace UchOtd.Core
{
    public static class GoogleCalendarService
    {
        public static CalendarService Service;
        public static string TimeZone = "Europe/Samara";

        public static string NUCredentials = "client_secret.json";
        public static string NACredentials = "client_secret2.json";

        public static CalendarService InitService(string credentialsFilename)
        {
            string[] Scopes = { CalendarService.Scope.Calendar };
            string ApplicationName = "UchOtd";

            UserCredential credential;

            using (var stream =
                new FileStream(credentialsFilename, FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/" + credentialsFilename);

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            Service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return Service;
        }

        public static IList<CalendarListEntry> GetList()
        {
            return Service.CalendarList.List().Execute().Items;
        }

        public static CalendarListEntry GetCalendarBySummary(string summary)
        {
            var calendars = GetList();
            var calendar = calendars.FirstOrDefault(cal => cal.Summary == summary);
            return calendar;
        }

        public static bool DeleteBySummary(string summary)
        {
            var calendars = GetList();
            var calendar = calendars.FirstOrDefault(cal => cal.Summary == summary);
            if (calendar != null)
            {
                Service.Calendars.Delete(calendar.Id).Execute();
                return true;
            }

            return false;
        }

        public static Calendar InsertWithSummary(string summary)
        {
            var calendar = new Calendar
            {
                Summary = summary,
                TimeZone = TimeZone
            };
            return Service.Calendars.Insert(calendar).Execute();
        }

        public static void AddEventsToCalendar(CalendarListEntry calendar, List<Lesson> groupLessons, StudentGroup group, MainEditForm mainForm, ToolStripStatusLabel status)
        {
            var lessonLengthInMinutes = 80; //Utilities.GetLessonLengthFromGroupname(group.Name);

            var oldEvents = GetCalendarEvents(calendar);
            var lessonsToDelete = (oldEvents.Count > 0) ? (Enumerable.Range(0, oldEvents.Count-1).ToList()) : (new List<int>());

            for (var i = 0; i < groupLessons.Count; i++)
            {
                var lesson = groupLessons[i];

                var cDate = lesson.Calendar.Date.Date;
                var rTime = lesson.Ring.Time;

                var startDateTime = new DateTime(
                    cDate.Year, cDate.Month, cDate.Day,
                    rTime.Hour, rTime.Minute, rTime.Second);
                var edtStart = new EventDateTime()
                {
                    DateTime = startDateTime,
                    TimeZone = TimeZone
                };

                var endDateTime = startDateTime.AddMinutes(lessonLengthInMinutes);
                var edtEnd = new EventDateTime()
                {
                    DateTime = endDateTime,
                    TimeZone = TimeZone
                };

                var lessonSummary = SummaryFromLesson(lesson);

                var seacrhResult = oldEvents.Where(e =>
                    e.Start.DateTime?.CompareTo(startDateTime) == 0 &&
                    e.End.DateTime?.CompareTo(endDateTime) == 0 &&
                    e.Summary == lessonSummary
                );

                if (!seacrhResult.Any())
                {
                    lessonsToDelete.Remove(i);

                    var evt = new Event
                    {
                        Summary = lessonSummary,
                        Start = edtStart,
                        End = edtEnd
                    };

                    bool OK = true;
                    do
                    {
                        OK = true;

                        try
                        {
                            var newEvent = Service.Events.Insert(evt, calendar.Id).Execute();
                        }
                        catch (Exception e)
                        {
                            OK = false;
                            ThreadSleep.Run(ThreadSleep.Up);
                        }

                        
                    } while (!OK);

                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        status.Text = calendar.Summary + " " + (i+1) + " / " + groupLessons.Count + " = " + String.Format("{0:#,0.000}", ((i + 1) * 100 / groupLessons.Count)) + "%";
                        // runs on UI thread
                    });


                    ThreadSleep.Run(ThreadSleep.Reset);
                }
            }

            foreach (var lessonIndex in lessonsToDelete)
            {
                var eventToDelete = oldEvents[lessonIndex];

                Service.Events.Delete(calendar.Id, eventToDelete.Id);
            }
        }

        private static IList<Event> GetCalendarEvents(CalendarListEntry calendar)
        {
            var request = Service.Events.List(calendar.Id);
            request.MaxResults = 2500;

            return request.Execute().Items;
        }

        private static string SummaryFromLesson(Lesson lesson)
        {
            return lesson.TeacherForDiscipline.Discipline.Name +
                   Environment.NewLine +
                   lesson.TeacherForDiscipline.Teacher.FIO +
                   Environment.NewLine +
                   lesson.Auditorium.Name;
        }

        public static CalendarListEntry InsertIfNotExistsWithSummary(string groupName)
        {
            var searchResult = GetList().FirstOrDefault(c => c.Summary == groupName);
            if (searchResult != null)
            {
                return searchResult;
            }

            InsertWithSummary(groupName);
            
            return GetList().FirstOrDefault(c => c.Summary == groupName);
        }

        public static void UploadGroupLessonEvents(ScheduleRepository repo, StudentGroup @group, MainEditForm mainForm, ToolStripStatusLabel status)
        {
            var calendar = InsertIfNotExistsWithSummary(group.Name);
            var groupLessons = Utilities.GetGroupActiveLessons(repo, group);
            AddEventsToCalendar(calendar, groupLessons, group, mainForm, status);
        }

        public static void ClearCalendar(CalendarListEntry calendar, ToolStripStatusLabel status, MainEditForm form)
        {
            var eventList = GetCalendarEvents(calendar).OrderBy(evt => evt.Start.DateTime).ToList();

            for (int i = 0; i < eventList.Count; i++)
            {
                var evt = eventList[i];
                Service.Events.Delete(calendar.Id, evt.Id).Execute();

                form.Invoke((MethodInvoker)delegate
                {
                    status.Text = calendar.Summary + " " + (i + 1) + " / " + eventList.Count + " =  " + String.Format("{0:#,0.000}", ((i + 1) * 100 / eventList.Count)) + "%";
                    // runs on UI thread
                });

                Thread.Sleep(1000);
            }
        }
    }
}
