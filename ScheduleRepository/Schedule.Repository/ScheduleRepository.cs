using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using Schedule.DataLayer;
using Schedule.DataLayer.Migrations;
using Schedule.Repositories.Common;
using Schedule.Repositories.Repositories.Analyse;
using Schedule.Repositories.Repositories.Config;
using Schedule.Repositories.Repositories.Logs;
using Schedule.Repositories.Repositories.Main;
using Schedule.Repositories.Repositories.Session;

namespace Schedule.Repositories
{
    public class ScheduleRepository : IDisposable
    {
        public AuditoriumsRepository Auditoriums;
        public BuildingsRepository Buildings;
        public CalendarsRepository Calendars;
        public DisciplinesRepository Disciplines;
        public DisciplineNameRepository DisciplineNames;
        public LessonsRepository Lessons;
        public RingsRepository Rings;
        public StudentsRepository Students;
        public StudentGroupsRepository StudentGroups;
        public StudentsInGroupsRepository StudentsInGroups;
        public TeachersRepository Teachers;
        public TeacherForDisciplinesRepository TeacherForDisciplines;
        public ConfigOptionRepository ConfigOptions;

        public AuditoriumEventsRepository AuditoriumEvents;

        public FacultiesRepository Faculties;
        public GroupsInFacultiesRepository GroupsInFaculties;

        public ScheduleNotesRepository ScheduleNotes;

        public LessonLogEventsRepository LessonLogEvents;

        public TeacherWishesRepository TeacherWishes;
        public CustomTeacherAttributesRepository CustomTeacherAttributes;
        public CustomDisciplineAttributesRepository CustomDisciplineAttributes;
        public CustomStudentGroupAttributesRepository CustomStudentGroupAttributes;
        public ShiftsRepository Shifts;
        public ShiftRingsRepository ShiftRings;

        public ExamsRepository Exams;
        public LogEventsRepository LogEvents;

        public CommonFunctions CommonFunctions;

        private string _connectionString;

        
        public void SetConnectionString(string value)
        {
            _connectionString = value;

            Auditoriums.ConnectionString = value;
            Buildings.ConnectionString = value;
            Calendars.ConnectionString = value;
            Disciplines.ConnectionString = value;
            DisciplineNames.ConnectionString = value;
            Lessons.ConnectionString = value;
            Rings.ConnectionString = value;
            Students.ConnectionString = value;
            StudentGroups.ConnectionString = value;
            StudentsInGroups.ConnectionString = value;
            Teachers.ConnectionString = value;
            TeacherForDisciplines.ConnectionString = value;
            ConfigOptions.ConnectionString = value;

            AuditoriumEvents.ConnectionString = value;

            Faculties.ConnectionString = value;
            GroupsInFaculties.ConnectionString = value;

            ScheduleNotes.ConnectionString = value;

            LessonLogEvents.ConnectionString = value;

            TeacherWishes.ConnectionString = value;
            CustomTeacherAttributes.ConnectionString = value;
            CustomDisciplineAttributes.ConnectionString = value;
            CustomStudentGroupAttributes.ConnectionString = value;
            Shifts.ConnectionString = value;
            ShiftRings.ConnectionString = value;
            Exams.ConnectionString = value;
            LogEvents.ConnectionString = value;

            CommonFunctions.ConnectionString = value;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
        public ScheduleRepository(string connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ScheduleContext, Configuration>());

            Auditoriums = new AuditoriumsRepository();
            Buildings = new BuildingsRepository();
            Calendars = new CalendarsRepository();
            Disciplines = new DisciplinesRepository();
            DisciplineNames = new DisciplineNameRepository();
            Lessons = new LessonsRepository(this);
            Rings = new RingsRepository();
            Students = new StudentsRepository();
            StudentGroups = new StudentGroupsRepository();
            StudentsInGroups = new StudentsInGroupsRepository();
            Teachers = new TeachersRepository();
            TeacherForDisciplines = new TeacherForDisciplinesRepository();
            ConfigOptions = new ConfigOptionRepository();

            AuditoriumEvents = new AuditoriumEventsRepository();

            Faculties = new FacultiesRepository();
            GroupsInFaculties = new GroupsInFacultiesRepository();

            ScheduleNotes = new ScheduleNotesRepository();

            LessonLogEvents = new LessonLogEventsRepository();

            TeacherWishes = new TeacherWishesRepository();

            CustomTeacherAttributes = new CustomTeacherAttributesRepository();
            CustomDisciplineAttributes = new CustomDisciplineAttributesRepository();
            CustomStudentGroupAttributes = new CustomStudentGroupAttributesRepository();

            Shifts = new ShiftsRepository();
            ShiftRings = new ShiftRingsRepository();

            Exams = new ExamsRepository();
            LogEvents = new LogEventsRepository();

            CommonFunctions = new CommonFunctions(this);

            SetConnectionString(connectionString);
        }
        
        public void CreateDb()
        {
            using (var context = new ScheduleContext(GetConnectionString()))
            {
                if (!context.Database.Exists())
                {
                    ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                }
            }
        }

        public void RecreateDb()
        {
            using (var context = new ScheduleContext(GetConnectionString()))
            {
                context.Database.Delete();
                context.Database.CreateIfNotExists();
            }
        }

        public string ExtractDbName(string connectionString)
        {
            int startIndex = connectionString.IndexOf("Database=", StringComparison.Ordinal) + 9;

            if (startIndex == -1)
            {
                return "";
            }

            int endIndex = -1;
            if (startIndex != 0)
            {
                endIndex = connectionString.IndexOf(';', startIndex);
            }

            return connectionString.Substring(startIndex, endIndex - startIndex);
        }

        public void BackupDb(string filename)
        {
            var dbName = ExtractDbName(GetConnectionString());

            if (dbName == "")
            {
                return;
            }

            var backupSql = "BACKUP DATABASE " + dbName + " TO DISK = '" + filename + "' WITH FORMAT, MEDIANAME='" + dbName + "'";

            ExecuteQuery(backupSql);
        }

        public void RestoreDb(string dbName, string filename)
        {
            ExecuteQuery("ALTER DATABASE " + dbName + " SET Single_User WITH Rollback Immediate");

            ExecuteQuery("use master; RESTORE DATABASE " + dbName + " FROM DISK = '" + filename + "' WITH REPLACE");

            ExecuteQuery("ALTER DATABASE " + dbName + " SET Multi_User");
        }

        private void ExecuteQuery(string sqlQuery)
        {
            var sqlConnection1 = new SqlConnection(GetConnectionString());
            var cmd = new SqlCommand
            {
                CommandText = sqlQuery,
                CommandType = CommandType.Text,
                Connection = sqlConnection1
            };

            sqlConnection1.Open();

            cmd.ExecuteNonQuery();

            sqlConnection1.Close();
        }

        public void CloneDb(ScheduleRepository scheduleRepository)
        {
            RecreateDb();

            // TODO : Скопировать базу данных
        }

        public void DebugLog(string message)
        {
            var sw = new StreamWriter("DebugLog.txt", true);
            sw.WriteLine(message);
            sw.Close();
        }
        
        public void Dispose()
        {
        }

        public void TxtBackup(string filename)
        {
            var sw = new StreamWriter(filename);

            foreach (var aud in Auditoriums.GetAll())
            {
                sw.WriteLine(aud.AuditoriumId);
                sw.WriteLine(aud.Building.BuildingId);
            }

            sw.Close();
        }
    }
}
