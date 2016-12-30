using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Constants;
using Schedule.Repositories;
using UchOtd.Core;
using UchOtd.Forms;
using UchOtd.Forms.Notes;
using UchOtd.Forms.Session;
using UchOtd.NUDS;
using UchOtd.NUDS.Forms;
using UchOtd.Repositories;
using UchOtd.Schedule;
using UchOtd.Schedule.Forms;
using UchOtd.Schedule.wnu;

namespace UchOtd
{
    public partial class StartupForm : Form
    {
        public const bool School = false;
        private const string DefaultDbName = "Schedule16172";

        public static List<string> serverList = School ?
            new List<string> { @"127.0.0.1\SQLEXPRESS" } :
            new List<string> { @"UCH-OTD-DISP\SQLEXPRESS", @"127.0.0.1\SQLEXPRESS" };

        public static string CurrentServerName = "";
        //public static string DefaultDbName = "School";

        public ScheduleRepository Repo;
        public UchOtdRepository UOrepo; 

        bool _studentListFormOpened;
        public StudentList StudentListForm;

        bool _scheduleFormOpened;
        public ScheduleForm ScheduleForm;

        bool _teacherScheduleFormOpened;
        public TeacherSchedule TeacherScheduleForm;

        bool _changesFormOpened;
        public Changes ChangesForm;

        bool _buildingFormOpened;
        public BuildingLessons BuildingForm;

        bool _notesFormOpened;
        public Notes NotesForm;

        bool _phonesFormOpened;
        public Phones PhonesForm;

        bool _mainEditFormOpened;
        public MainEditForm EditForm;

        bool _lessonsTfdFormOpened;
        public LessonListByTfd LessonsByTfd;

        bool _teacherLessonsFormOpened;
        public LessonListByTeacher TeacherLessons;

        bool _teacherHoursFormOpened;
        public TeacherHours TeacherDiciplines;

        bool _dailyLessonsFormOpened;
        public DailyLessons DailyLessons;

        bool _sessionFormOpened;
        public Session SessionForm;

        public StartupForm()
        {
            InitializeComponent();

            Task.Run(() => BackupDbLast10Runs(DefaultDbName));
            
            InitRepositories();

            // Контингент - Alt-S
            HotKeyManager.RegisterHotKey(Keys.S, (uint)KeyModifiers.Alt);

            // Просмотр расписания - Alt-V
            HotKeyManager.RegisterHotKey(Keys.V, (uint)KeyModifiers.Alt);

            // Расписание преподавателя - Alt-T
            HotKeyManager.RegisterHotKey(Keys.T, (uint)KeyModifiers.Alt);

            // Изменения расписание - Alt-C
            HotKeyManager.RegisterHotKey(Keys.C, (uint)KeyModifiers.Alt);

            // Занятость аудиторий - Alt-A
            HotKeyManager.RegisterHotKey(Keys.A, (uint)KeyModifiers.Alt);

            // Заметки - Alt-N
            HotKeyManager.RegisterHotKey(Keys.N, (uint)KeyModifiers.Alt);

            // Телефоны - Alt-P
            HotKeyManager.RegisterHotKey(Keys.P, (uint)KeyModifiers.Alt);

            // Редакторование расписания - Alt-R
            HotKeyManager.RegisterHotKey(Keys.R, (uint)KeyModifiers.Alt);

            // Список пар по TFD - Alt-L
            HotKeyManager.RegisterHotKey(Keys.L, (uint)KeyModifiers.Alt);

            // Список пар преподавателя - Alt-Shift-T
            HotKeyManager.RegisterHotKey(Keys.T, (uint)(KeyModifiers.Alt | KeyModifiers.Shift));

            // Список пар преподавателя - Ctrl-Shift-T
            HotKeyManager.RegisterHotKey(Keys.T, (uint)(KeyModifiers.Control | KeyModifiers.Shift));

            // Расписание на день - Alt-D
            HotKeyManager.RegisterHotKey(Keys.D, (uint)KeyModifiers.Alt);

            // Расписание сессии - Ctrl-Alt-S
            HotKeyManager.RegisterHotKey(Keys.S, (uint)(KeyModifiers.Control | KeyModifiers.Alt));

            HotKeyManager.HotKeyPressed += ManageHotKeys;

            if (File.Exists("Initial Database connection string.txt"))
            {
                var sr = new StreamReader("Initial Database connection string.txt");
                var connectionString = sr.ReadLine();
                sr.Close();

                Repo.SetConnectionString(connectionString);
            }

            RefreshDbOrConnectionName();
            
            _studentListFormOpened = false;
            _scheduleFormOpened = false;
            _teacherScheduleFormOpened = false;
            _buildingFormOpened = false;
            _notesFormOpened = false;
            _phonesFormOpened = false;
            _mainEditFormOpened = false;
            _lessonsTfdFormOpened = false;
            _teacherLessonsFormOpened = false;
            _teacherHoursFormOpened = false;
            _dailyLessonsFormOpened = false;
            _sessionFormOpened = false;
            
            trayIcon.Visible = true;
        }

        private void BackupDbLast10Runs(string dbName)
        {
            try
            {
                const string appDataPath = "D:\\UchOtd-DB-Backup";

                if (!Directory.Exists(appDataPath))
                {
                    Directory.CreateDirectory(appDataPath);
                }

                var filesInfo = new DirectoryInfo(appDataPath).GetFiles("*.bak");
                if (filesInfo.Length >= 10)
                {
                    var extrafiles = new List<FileInfo> (new DirectoryInfo(appDataPath).EnumerateFiles("*.bak"))
                        .OrderByDescending(f => f.LastWriteTime)
                        .Skip(9)
                        .ToList();
                    extrafiles.ForEach(f => f.Delete());
                }

                var filename = appDataPath + "\\" + dbName + DateTime.Now.ToString("_dd-MMM_HH-mm-ss") + ".bak";

                BackupDb(dbName, filename);
                
            }
            catch (Exception exc)
            {
                try
                {
                    var sw = new StreamWriter("D:\\UchOtd-errorLog.txt");
                    sw.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " - " + exc.Message);
                    sw.Close();
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void BackupDb(string dbName, string filename)
        {
            // Integrated Security=SSPI;
            var sqlConnection1 = new SqlConnection("data source=tcp:" + CurrentServerName + ",1433;Database=" + dbName + "; User ID=sa;Password=ghjuhfvvf; multipleactiveresultsets=True");
            var cmd = new SqlCommand
            {
                CommandText =
                    "BACKUP DATABASE " + dbName + " TO DISK = '" + filename + "' WITH FORMAT, MEDIANAME='" + dbName +
                    "'",
                CommandType = CommandType.Text,
                Connection = sqlConnection1
            };

            sqlConnection1.Open();

            cmd.ExecuteNonQuery();

            sqlConnection1.Close();
        }

        // Adds an ACL entry on the specified directory for the specified account. 
        public static void AddDirectorySecurity(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
        {
            // Create a new DirectoryInfo object.
            var dInfo = new DirectoryInfo(fileName);

            // Get a DirectorySecurity object that represents the  
            // current security settings.
            var dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(account,
                                                            rights,
                                                            controlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);
        }

        private void InitRepositories()
        {
            CurrentServerName = serverList[0];

            //Repo = new ScheduleRepository("data source=tcp:" + CurrentServerName + ",1433;Database=" + DefaultDbName + "; Integrated Security=SSPI;multipleactiveresultsets=True");
            //UOrepo = new UchOtdRepository("data source=tcp:" + CurrentServerName + ",1433;Database=UchOtd; Integrated Security=SSPI;multipleactiveresultsets=True");

            Repo = new ScheduleRepository("Server=" + CurrentServerName + ",1433;Database=" + DefaultDbName + "; User ID=sa;Password=ghjuhfvvf; multipleactiveresultsets=True");
            UOrepo = new UchOtdRepository("Server=" + CurrentServerName + ",1433;Database=UchOtd; User ID=sa;Password=ghjuhfvvf; multipleactiveresultsets=True");

            if (School)
            {
                uploadTimer.Enabled = true;
            }
        }

        /*
        private static bool PingServerExistence(string server)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted. 
            const string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            const int timeout = 120;
            PingReply reply;
            try
            {
                reply = pingSender.Send(server, timeout, buffer, options);
            }
            catch
            {
                return false;
            }            

            return (reply != null) && (reply.Status == IPStatus.Success);
        }
         */

        private void RefreshDbOrConnectionName()
        {
            if (Repo != null)
            {
                openDBToolStripMenuItem.Text = "Сменить базу данных (" + Utilities.ExtractDbOrConnectionName(Repo.GetConnectionString()) + ")";
            }
            else
            {
                openDBToolStripMenuItem.Text = "Открыть базу данных";
            }
        }

        private void ManageHotKeys(object sender, HotKeyEventArgs e)
        {
            if (e.Modifiers == KeyModifiers.Alt)
            {
                if (e.Key == Keys.S)
                {
                    ShowStudentListForm();
                }
                if (e.Key == Keys.V)
                {
                    ShowScheduleForm();
                }
                if (e.Key == Keys.T)
                {
                    ShowTeacherScheduleForm();
                }
                if (e.Key == Keys.C)
                {
                    ShowChangesForm();
                }
                if (e.Key == Keys.A)
                {
                    ShowBuildingForm();
                }
                if (e.Key == Keys.N)
                {
                    ShowNotesForm();
                }
                if (e.Key == Keys.P)
                {
                    ShowPhonesForm();
                }
                if (e.Key == Keys.R)
                {
                    ShowEditScheduleForm();
                }
                if (e.Key == Keys.L)
                {
                    ShowLessonListByTfd();
                }
                if (e.Key == Keys.D)
                {
                    ShowDailyLessons();
                }
            }

            if (e.Modifiers == (KeyModifiers.Alt | KeyModifiers.Shift))
            {
                if (e.Key == Keys.T)
                {
                    ShowTeacherLessons();
                }
            }

            if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Shift))
            {
                if (e.Key == Keys.T)
                {
                    ShowTeacherHours();
                }
            }

            if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Alt))
            {
                if (e.Key == Keys.S)
                {
                    ShowSession();
                }
            }
        }

        private void ShowDailyLessons()
        {
            if (_dailyLessonsFormOpened)
            {
                DailyLessons.Activate();
                DailyLessons.Focus();
                return;
            }

            DailyLessons = new DailyLessons(Repo);
            _dailyLessonsFormOpened = true;
            DailyLessons.Show();
            _dailyLessonsFormOpened = false;
        }

        private void ShowEditScheduleForm()
        {
            if (_mainEditFormOpened)
            {
                EditForm.Activate();
                EditForm.Focus();
                return;
            }

            EditForm = new MainEditForm(Repo);
            _mainEditFormOpened = true;
            EditForm.Show();
            _mainEditFormOpened = false;
        }

        private void ShowPhonesForm()
        {
            if (_phonesFormOpened)
            {
                PhonesForm.Activate();
                PhonesForm.Focus();
                return;
            }

            PhonesForm = new Phones(UOrepo);
            _phonesFormOpened = true;
            PhonesForm.Show();
            _phonesFormOpened = false;
        }

        private void ShowNotesForm()
        {
            if (_notesFormOpened)
            {
                NotesForm.Activate();
                NotesForm.Focus();
                return;
            }

            NotesForm = new Notes(UOrepo);
            _notesFormOpened = true;
            NotesForm.Show();
            _notesFormOpened = false;
        }
        
        private void ShowBuildingForm()
        {
            if (_buildingFormOpened)
            {
                BuildingForm.Activate();
                BuildingForm.Focus();
                return;
            }

            BuildingForm = new BuildingLessons(Repo);
            _buildingFormOpened = true;
            BuildingForm.Show();
            _buildingFormOpened = false;
        }


        private void ShowTeacherScheduleForm()
        {
            if (_teacherScheduleFormOpened)
            {
                TeacherScheduleForm.Activate();
                TeacherScheduleForm.Focus();
                return;
            }

            TeacherScheduleForm = new TeacherSchedule(Repo);
            _teacherScheduleFormOpened = true;
            TeacherScheduleForm.Show();
            _teacherScheduleFormOpened = false;
        }
        
        private void ShowScheduleForm()
        {
            if (_scheduleFormOpened)
            {
                ScheduleForm.Activate();
                ScheduleForm.Focus();
                return;
            }

            ScheduleForm = new ScheduleForm(Repo);
            _scheduleFormOpened = true;
            ScheduleForm.Show();
            _scheduleFormOpened = false;
        }

        private void ShowStudentListForm()
        {
            if (_studentListFormOpened)
            {
                StudentListForm.Activate();
                StudentListForm.Focus();
                return;
            }

            StudentListForm = new StudentList(Repo);
            _studentListFormOpened = true;
            StudentListForm.Show();
            _studentListFormOpened = false;
        }

        private void ShowChangesForm()
        {
            if (_changesFormOpened)
            {
                ChangesForm.Activate();
                ChangesForm.Focus();
                return;
            }

            ChangesForm = new Changes(Repo, 0);
            _changesFormOpened = true;
            ChangesForm.Show();
            _changesFormOpened = false;
        }

        protected override void SetVisibleCore(bool value)
        {
            // Quick and dirty to keep the main window invisible
            base.SetVisibleCore(false);
        }


        private void ВыходToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void ИзмененияToolStripMenuItemClick(object sender, EventArgs e)
        {
            ShowChangesForm();
        }

        private void РасписаниеПреподавателяToolStripMenuItemClick(object sender, EventArgs e)
        {
            ShowTeacherScheduleForm();
        }

        private void ПоказатьОкноToolStripMenuItemClick(object sender, EventArgs e)
        {
            ShowScheduleForm();
        }

        private void ЗанятостьАудиторийAltAToolStripMenuItemClick(object sender, EventArgs e)
        {
            ShowBuildingForm();
        }

        private void ЗаметкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNotesForm();
        }

        private void ТелефоныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPhonesForm();
        }

        private void КонтингентToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowStudentListForm();
        }

        private void OpenDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openDbForm = new OpenDb(this);
            openDbForm.Show();

            RefreshDbOrConnectionName();
        }

        private void EditScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditScheduleForm();
        }

        private void СписокПарПоДисциплинеAltLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLessonListByTfd();
        }

        private void ShowLessonListByTfd()
        {            
            if (_lessonsTfdFormOpened)
            {
                LessonsByTfd.Activate();
                LessonsByTfd.Focus();
                return;
            }

            LessonsByTfd = new LessonListByTfd(Repo);
            _lessonsTfdFormOpened = true;
            LessonsByTfd.Show();
            _lessonsTfdFormOpened = false;
            
        }

        private void ЧасыПоПреподавателюAltShiftTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTeacherLessons();
        }

        private void ShowTeacherLessons()
        {
            if (_teacherLessonsFormOpened)
            {
                TeacherLessons.Activate();
                TeacherLessons.Focus();
                return;
            }

            TeacherLessons = new LessonListByTeacher(Repo);
            _teacherLessonsFormOpened = true;
            TeacherLessons.Show();
            _teacherLessonsFormOpened = false;            
        }

        private void СписокПарПоПреподавателюCtrlShiftTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowTeacherHours();
        }

        private void ShowTeacherHours()
        {
            if (_teacherHoursFormOpened)
            {
                TeacherDiciplines.Activate();
                TeacherDiciplines.Focus();
                return;
            }

            TeacherDiciplines = new TeacherHours(Repo);
            _teacherHoursFormOpened = true;
            TeacherDiciplines.Show();
            _teacherHoursFormOpened = false;
        }

        private void РасписаниеНаДеньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDailyLessons();
        }

        private void РасписаниеСессииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSession();
        }

        private void ShowSession()
        {
            if (_sessionFormOpened)
            {
                SessionForm.Activate();
                SessionForm.Focus();
                return;
            }

            SessionForm = new Session(Repo);
            _sessionFormOpened = true;
            SessionForm.Show();
            _sessionFormOpened = false;
        }

        private void UploadTimer_Tick(object sender, EventArgs e)
        {
            var tokenSource = new CancellationTokenSource();
            var cToken = tokenSource.Token;
            
            Task.Run(() =>
            {
                try
                {
                    WnuUpload.UploadSchedule(Repo, Constants.SitesUploadEndPoints[Constants.schoolEndPointIndex], School ? "s_" : "", cToken);
                }
                catch
                {
                    // ignored
                }
            }, cToken);
        }
    }
}
