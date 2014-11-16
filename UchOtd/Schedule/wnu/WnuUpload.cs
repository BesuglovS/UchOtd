﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.wnu.MySQLViews;

namespace UchOtd.Schedule.wnu
{
    public static class WnuUpload
    {
        public static string UploadHttpPath = "http://wiki.nayanova.edu/_php/includes/";
        public static string UploadFtpPath = "ftp://wiki.nayanova.edu/";
        //public static string UploadPath = "http://localhost/phpstorm/wnu/_php/includes/";

        public static void UploadFile(string sourcefile, string destfile)
        {
            // Get the object used to communicate with the server.
            var request = (FtpWebRequest)WebRequest.Create(UploadFtpPath + destfile);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(Properties.Settings.Default.wnuUserName, Properties.Settings.Default.wnuPassword);

            byte[] b = File.ReadAllBytes(sourcefile);

            request.ContentLength = b.Length;
            using (Stream s = request.GetRequestStream())
            {
                s.Write(b, 0, b.Length);
            }

            var response = (FtpWebResponse)request.GetResponse();

            response.Close();
        }

        public static string UploadTableData(string postData)
        {
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create(UploadHttpPath + "import.php");

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

        public static void UploadSchedule(ScheduleRepository repo, String databaseTablesPrefix)
        {
            var jsonSerializer = new JavaScriptSerializer {MaxJsonLength = 10000000};

            var allAuditoriums = repo.Auditoriums.GetAllAuditoriums();
            var mySqlAuditoriums = MySqlAuditorium.FromAuditoriumList(allAuditoriums);
            var wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "auditoriums", data = jsonSerializer.Serialize(mySqlAuditoriums) };
            string json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var allBuildings = repo.Buildings.GetAllBuildings();
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "buildings", data = jsonSerializer.Serialize(allBuildings) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var calendars = repo.Calendars.GetAllCalendars();
            var mySqlCalendars = MySqlCalendar.FromCalendarList(calendars);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "calendars", data = jsonSerializer.Serialize(mySqlCalendars) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var rings = repo.Rings.GetAllRings();
            var mySqlRings = MySqlRing.FromRingList(rings);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "rings", data = jsonSerializer.Serialize(mySqlRings) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var students = repo.Students.GetAllStudents();
            var mySqlStudents = MySqlStudent.FromStudentList(students);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "students", data = jsonSerializer.Serialize(mySqlStudents) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var studentGroups = repo.StudentGroups.GetAllStudentGroups();
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "studentGroups", data = jsonSerializer.Serialize(studentGroups) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var teachers = repo.Teachers.GetAllTeachers();
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "teachers", data = jsonSerializer.Serialize(teachers) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var disciplines = repo.Disciplines.GetAllDisciplines();
            var mySqlDisciplines = MySqlDiscipline.FromDisciplineList(disciplines);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "disciplines", data = jsonSerializer.Serialize(mySqlDisciplines) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var studentsInGroups = repo.StudentsInGroups.GetAllStudentsInGroups();
            var mySqlStudentsInGroups = MySqlStudentsInGroups.FromStudentsInGroupsList(studentsInGroups);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "studentsInGroups", data = jsonSerializer.Serialize(mySqlStudentsInGroups) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var teacherForDisciplines = repo.TeacherForDisciplines.GetAllTeacherForDiscipline();
            var mySqlTeacherForDisciplines = MySqlTeacherForDiscipline.FromTeacherForDisciplineList(teacherForDisciplines);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "teacherForDisciplines", data = jsonSerializer.Serialize(mySqlTeacherForDisciplines) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var lessons = repo.Lessons.GetFiltredLessons(l => l.State == 0 || l.State == 1);
            var mySqlLessons = MySqlLesson.FromLessonList(lessons);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "lessons", data = jsonSerializer.Serialize(mySqlLessons) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var configs = repo.ConfigOptions.GetAllConfigOptions();
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "configs", data = jsonSerializer.Serialize(configs) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var lessonsLog = repo.LessonLogEvents.GetAllLessonLogEvents();
            var mySqlLogEvent = MySqlLessonLogEvent.FromLessonLogList(lessonsLog);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "lessonLogEvents", data = jsonSerializer.Serialize(mySqlLogEvent) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var auditoriumEvents = repo.AuditoriumEvents.GetAllAuditoriumEvents();
            var mySqlauditoriumEvents = MySqlAuditoriumEvent.FromAuditoriumEventList(auditoriumEvents);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "auditoriumEvents", data = jsonSerializer.Serialize(mySqlauditoriumEvents) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var faculties = repo.Faculties.GetAllFaculties();
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "faculties", data = jsonSerializer.Serialize(faculties) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);

            var gifs = repo.GroupsInFaculties.GetAllGroupsInFaculty();
            var mySqlgifs = MySqlGroupsInFaculty.FromGroupsInFacultyList(gifs);
            wud = new WnuUploadData { dbPrefix = databaseTablesPrefix, tableSelector = "GroupsInFaculties", data = jsonSerializer.Serialize(mySqlgifs) };
            json = jsonSerializer.Serialize(wud);
            UploadTableData(json);
        }
    }
}
