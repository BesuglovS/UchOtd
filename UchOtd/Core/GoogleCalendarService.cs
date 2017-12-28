using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Calendar = Google.Apis.Calendar.v3.Data.Calendar;

namespace UchOtd.Core
{
    public static class GoogleCalendarService
    {
        public static CalendarService Service;
        public static string TimeZone = "Europe/Samara";

        public static CalendarService InitService()
        {
            string[] Scopes = { CalendarService.Scope.Calendar };
            string ApplicationName = "UchOtd";

            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

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

        public static bool DeleteBySummary(string summary)
        {
            var calendars = GetList();
            var calendar = calendars.FirstOrDefault(cal => cal.Summary == "15 А");
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

        public static void AddEventsToCalendar(CalendarListEntry calendar, List<Lesson> groupLessons, StudentGroup group)
        {
            var lessonLengthInMinutes = Utilities.GetLessonLengthFromGroupname(group.Name);

            var oldEvents = GetCalendarEvents(calendar);
            var lessonsToDelete = Enumerable.Range(0, oldEvents.Count-1).ToList();

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

                    var newEvent = Service.Events.Insert(evt, calendar.Id).Execute();

                    Thread.Sleep(500);
                }

                foreach (var lessonIndex in lessonsToDelete)
                {
                    var eventToDelete = oldEvents[lessonIndex];

                    Service.Events.Delete(calendar.Id, eventToDelete.Id);
                }

                var eprst = 999;
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

        public static void UploadGroupLessonEvents(ScheduleRepository repo, StudentGroup @group)
        {
            var calendar = InsertIfNotExistsWithSummary(group.Name);
            var groupLessons = Utilities.GetGroupActiveLessons(repo, group);
            AddEventsToCalendar(calendar, groupLessons, group);
        }
    }
}
