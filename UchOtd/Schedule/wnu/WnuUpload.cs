using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using Schedule.Repositories;
using UchOtd.Properties;
using UchOtd.Schedule.wnu.MySQLViews;

namespace UchOtd.Schedule.wnu
{
    public static class WnuUpload
    {
        
        public static string UploadFtpPath = "ftp://wiki.nayanova.edu/";
        //public static string UploadPath = "http://localhost/phpstorm/wnu/_php/includes/";

        public static void UploadFile(string sourcefile, string destfile)
        {
            // Get the object used to communicate with the server.
            var request = (FtpWebRequest)WebRequest.Create(UploadFtpPath + destfile);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(Settings.Default.wnuUserName, Settings.Default.wnuPassword);

            byte[] b = File.ReadAllBytes(sourcefile);

            request.ContentLength = b.Length;
            using (Stream s = request.GetRequestStream())
            {
                s.Write(b, 0, b.Length);
            }

            var response = (FtpWebResponse)request.GetResponse();

            response.Close();
        }

        public static string UploadTableData(string postData, string uploadEndPoint)
        {
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create(uploadEndPoint + "import.php");

            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            if (dataStream != null)
            {
                var reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
            }

            return "dataStream == null";
        }

        public static void UploadSchedule(ScheduleRepository repo, string databaseTablesPrefix, string uploadEndPoint, CancellationToken cToken)
        {
            var jsonSerializer = new JavaScriptSerializer {MaxJsonLength = 10000000};

            cToken.ThrowIfCancellationRequested();

            var allAuditoriums = repo.Auditoriums.GetAll();
            var mySqlAuditoriums = MySqlAuditorium.FromAuditoriumList(allAuditoriums);
            var wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "auditoriums", data = jsonSerializer.Serialize(mySqlAuditoriums) };
            string json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            var allBuildings = repo.Buildings.GetAllBuildings();
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "buildings", data = jsonSerializer.Serialize(allBuildings) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            var calendars = repo.Calendars.GetAllCalendars();
            var mySqlCalendars = MySqlCalendar.FromCalendarList(calendars);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "calendars", data = jsonSerializer.Serialize(mySqlCalendars) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            var rings = repo.Rings.GetAllRings();
            var mySqlRings = MySqlRing.FromRingList(rings);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "rings", data = jsonSerializer.Serialize(mySqlRings) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            var students = repo.Students.GetAllStudents();
            var mySqlStudents = MySqlStudent.FromStudentList(students);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "students", data = jsonSerializer.Serialize(mySqlStudents) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            var studentGroups = repo.StudentGroups.GetAllStudentGroups();
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "studentGroups", data = jsonSerializer.Serialize(studentGroups) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            var teachers = repo.Teachers.GetAllTeachers();
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "teachers", data = jsonSerializer.Serialize(teachers) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            var disciplines = repo.Disciplines.GetAllDisciplines();
            var mySqlDisciplines = MySqlDiscipline.FromDisciplineList(disciplines);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "disciplines", data = jsonSerializer.Serialize(mySqlDisciplines) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            var studentsInGroups = repo.StudentsInGroups.GetAllStudentsInGroups();
            var mySqlStudentsInGroups = MySqlStudentsInGroups.FromStudentsInGroupsList(studentsInGroups);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "studentsInGroups", data = jsonSerializer.Serialize(mySqlStudentsInGroups) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            var teacherForDisciplines = repo.TeacherForDisciplines.GetAllTeacherForDiscipline();
            var mySqlTeacherForDisciplines = MySqlTeacherForDiscipline.FromTeacherForDisciplineList(teacherForDisciplines);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "teacherForDisciplines", data = jsonSerializer.Serialize(mySqlTeacherForDisciplines) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            int splitCount = 3000;
            var lessons = repo.Lessons.GetFiltredLessons(l => l.State == 0 || l.State == 1);
            var partsCount = Math.Ceiling((double)lessons.Count / splitCount);

            for (int i = 0; i < partsCount; i++)
            {
                var lessonsChunk = lessons.Skip(i * splitCount).Take(splitCount).ToList();
                var mySqlLessons = MySqlLesson.FromLessonList(lessonsChunk);
                wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "lessons", append = (i == 0) ? "" : "1", data = jsonSerializer.Serialize(mySqlLessons) };
                json = jsonSerializer.Serialize(wud);
                UploadTableData(json, uploadEndPoint);
            }
            
            cToken.ThrowIfCancellationRequested();

            var configs = repo.ConfigOptions.GetAllConfigOptions();
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "configs", data = jsonSerializer.Serialize(configs) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            splitCount = 3000;
            var lessonsLogEvents = repo.LessonLogEvents.GetAllLessonLogEvents();
            partsCount = Math.Ceiling((double)lessonsLogEvents.Count / splitCount);

            for (int i = 0; i < partsCount; i++)
            {
                var lessonLogEventsChunk = lessonsLogEvents.Skip(i * splitCount).Take(splitCount).ToList();
                var mySqlLogEvent = MySqlLessonLogEvent.FromLessonLogList(lessonLogEventsChunk);
                wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "lessonLogEvents", append = (i == 0) ? "" : "1", data = jsonSerializer.Serialize(mySqlLogEvent) };
                json = jsonSerializer.Serialize(wud);
                UploadTableData(json, uploadEndPoint);
            }

            cToken.ThrowIfCancellationRequested();
            
            var auditoriumEvents = repo.AuditoriumEvents.GetAllAuditoriumEvents();
            var mySqlauditoriumEvents = MySqlAuditoriumEvent.FromAuditoriumEventList(auditoriumEvents);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "auditoriumEvents", data = jsonSerializer.Serialize(mySqlauditoriumEvents) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);
             

            cToken.ThrowIfCancellationRequested();

            var faculties = repo.Faculties.GetAllFaculties();
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "faculties", data = jsonSerializer.Serialize(faculties) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);

            cToken.ThrowIfCancellationRequested();

            var gifs = repo.GroupsInFaculties.GetAllGroupsInFaculty();
            var mySqlgifs = MySqlGroupsInFaculty.FromGroupsInFacultyList(gifs);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "GroupsInFaculties", data = jsonSerializer.Serialize(mySqlgifs) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json, uploadEndPoint);
        }
    }
}
