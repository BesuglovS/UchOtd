﻿using System;
using System.Windows.Forms;
using Schedule.DomainClasses.Config;
using Schedule.Repositories;
using UchOtd.Forms;
using UchOtd.NUDS;
using UchOtd.NUDS.Forms;
using UchOtd.Forms.Notes;
using UchOtd.Repositories;
using System.IO;
using UchOtd.Core;
using System.Net.NetworkInformation;
using System.Text;
using System.Collections.Generic;
using UchOtd.Forms.Session;
using UchOtd.Schedule;
using Schedule.wnu;
using System.Threading.Tasks;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Security.AccessControl;
using UchOtd.Schedule.Forms;

namespace UchOtd
{
    public partial class StartupForm : Form
    {
        public static bool school = false;
        public static string DefaultDBName = "Schedule14151";
        //public static string DefaultDBName = "School";

        public ScheduleRepository _repo;
        public UchOtdRepository _UOrepo; 

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

        bool _NotesFormOpened;
        public Notes NotesForm;

        bool _PhonesFormOpened;
        public Phones PhonesForm;

        bool _MainEditFormOpened;
        public MainEditForm EditForm;

        bool _LessonsTFDFormOpened;
        public LessonListByTFD LessonsByTFD;

        bool _teacherLessonsFormOpened;
        public LessonListByTeacher teacherLessons;

        bool _teacherHoursFormOpened;
        public teacherHours teacherDiciplines;

        bool _dailyLessonsFormOpened;
        public DailyLessons dailyLessons;

        bool _sessionFormOpened;
        public Session sessionForm;

        public StartupForm()
        {
            InitializeComponent();

            BackupDBLast10Runs(DefaultDBName);

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

                _repo.ConnectionString = connectionString;
            }

            RefreshDbOrConnectionName();
            
            _studentListFormOpened = false;
            _scheduleFormOpened = false;
            _teacherScheduleFormOpened = false;
            _buildingFormOpened = false;
            _NotesFormOpened = false;
            _PhonesFormOpened = false;
            _MainEditFormOpened = false;
            _LessonsTFDFormOpened = false;
            _teacherLessonsFormOpened = false;
            _teacherHoursFormOpened = false;
            _dailyLessonsFormOpened = false;
            _sessionFormOpened = false;
            
            trayIcon.Visible = true;
        }

        private void BackupDBLast10Runs(string dbName)
        {
            try
            {
                const string AppDataPath = "D:\\UchOtd-DB-Backup";

                if (!Directory.Exists(AppDataPath))
                {
                    Directory.CreateDirectory(AppDataPath);
                }

                var filesInfo = new DirectoryInfo(AppDataPath).GetFiles("*.bak");
                if (filesInfo.Length >= 10)
                {
                    var extrafiles = new List<FileInfo> (new DirectoryInfo(AppDataPath).EnumerateFiles("*.bak"))
                        .OrderByDescending(f => f.LastWriteTime)
                        .Skip(9)
                        .ToList();
                    extrafiles.ForEach(f => f.Delete());
                }

                var filename = AppDataPath + "\\" + dbName + DateTime.Now.ToString("_dd-MMM_HH-mm-ss") + ".bak";

                BackupDB(dbName, filename);
                
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
                }
                
            }
        }

        private void BackupDB(string dbName, string filename)
        {
            SqlConnection sqlConnection1 = new SqlConnection("data source=tcp:127.0.0.1,1433;Database=" + dbName + ";User ID = sa;Password = ghjuhfvvf;multipleactiveresultsets=True");
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "BACKUP DATABASE " + dbName + " TO DISK = '" + filename + "' WITH FORMAT, MEDIANAME='" + dbName + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            cmd.ExecuteNonQuery();

            sqlConnection1.Close();
        }

        // Adds an ACL entry on the specified directory for the specified account. 
        public static void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the  
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);
        }

        private void InitRepositories()
        {
            var serverList = new List<string>
            { 
                "127.0.0.1",
                "uch-otd-disp"                
            };
            /*
            bool successPing = false;
            int connectionIndex = 0;
            do
            {
                var serverName = ServerList[connectionIndex];
                if (PingServerExistence(serverName))
                {
                    successPing = true;

                    _repo = new ScheduleRepository("data source=tcp:" + serverName + ",1433;Database=Schedule14151;User ID = sa;Password = ghjuhfvvf;multipleactiveresultsets=True");
                    _UOrepo = new UchOtdRepository("data source=tcp:" + serverName + ",1433;Database=UchOtd;User ID = sa;Password = ghjuhfvvf;multipleactiveresultsets=True");
                }
            } while (!successPing && connectionIndex != ServerList.Count);

            if (!successPing)
            {
                MessageBox.Show("Не удалось подключится к базе данных.");
            }
             */

            //_repo = new ScheduleRepository("data source=tcp:" + ServerList[0] + ",1433;Database=Schedule14151;User ID = sa;Password = ghjuhfvvf;multipleactiveresultsets=True");
            _repo = new ScheduleRepository("data source=tcp:" + serverList[0] + ",1433;Database=" + DefaultDBName + ";User ID = sa;Password = ghjuhfvvf;multipleactiveresultsets=True");
            _UOrepo = new UchOtdRepository("data source=tcp:" + serverList[0] + ",1433;Database=UchOtd;User ID = sa;Password = ghjuhfvvf;multipleactiveresultsets=True");

            PropagateIsActiveToStateIfNeeded();

            if (school)
            {
                uploadTimer.Enabled = true;
            }
        }

        private void PropagateIsActiveToStateIfNeeded()
        {
            var propagateOption = _repo.GetFirstFiltredConfigOption(co => co.Key == "_IsActivePropagatedToState");

            if (propagateOption == null)
            {
                _repo.PropagateIsActiveToState();
                propagateOption = new ConfigOption { Key = "_IsActivePropagatedToState", Value = "" };
                _repo.AddConfigOption(propagateOption);
            }
        }

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

        private void RefreshDbOrConnectionName()
        {
            if (_repo != null)
            {
                openDBToolStripMenuItem.Text = "Сменить базу данных (" + Utilities.ExtractDbOrConnectionName(_repo.ConnectionString) + ")";
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
                    ShowLessonListByTFD();
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
                dailyLessons.Activate();
                dailyLessons.Focus();
                return;
            }

            dailyLessons = new DailyLessons(_repo);
            _dailyLessonsFormOpened = true;
            dailyLessons.Show();
            _dailyLessonsFormOpened = false;
        }

        private void ShowEditScheduleForm()
        {
            if (_MainEditFormOpened)
            {
                EditForm.Activate();
                EditForm.Focus();
                return;
            }

            EditForm = new MainEditForm(_repo);
            _MainEditFormOpened = true;
            EditForm.Show();
            _MainEditFormOpened = false;
        }

        private void ShowPhonesForm()
        {
            if (_PhonesFormOpened)
            {
                PhonesForm.Activate();
                PhonesForm.Focus();
                return;
            }

            PhonesForm = new Phones(_UOrepo);
            _PhonesFormOpened = true;
            PhonesForm.Show();
            _PhonesFormOpened = false;
        }

        private void ShowNotesForm()
        {
            if (_NotesFormOpened)
            {
                NotesForm.Activate();
                NotesForm.Focus();
                return;
            }

            NotesForm = new Notes(_UOrepo);
            _NotesFormOpened = true;
            NotesForm.Show();
            _NotesFormOpened = false;
        }
        
        private void ShowBuildingForm()
        {
            if (_buildingFormOpened)
            {
                BuildingForm.Activate();
                BuildingForm.Focus();
                return;
            }

            BuildingForm = new BuildingLessons(_repo);
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

            TeacherScheduleForm = new TeacherSchedule(_repo);
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

            ScheduleForm = new ScheduleForm(_repo);
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

            StudentListForm = new StudentList(_repo);
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

            ChangesForm = new Changes(_repo, 0);
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

        private void заметкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNotesForm();
        }

        private void телефоныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPhonesForm();
        }

        private void контингентToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowStudentListForm();
        }

        private void openDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openDBForm = new OpenDB(this);
            openDBForm.Show();

            RefreshDbOrConnectionName();
        }

        private void EditScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditScheduleForm();
        }

        private void списокПарПоДисциплинеAltLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLessonListByTFD();
        }

        private void ShowLessonListByTFD()
        {            
            if (_LessonsTFDFormOpened)
            {
                LessonsByTFD.Activate();
                LessonsByTFD.Focus();
                return;
            }

            LessonsByTFD = new LessonListByTFD(_repo);
            _LessonsTFDFormOpened = true;
            LessonsByTFD.Show();
            _LessonsTFDFormOpened = false;
            
        }

        private void часыПоПреподавателюAltShiftTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTeacherLessons();
        }

        private void ShowTeacherLessons()
        {
            if (_teacherLessonsFormOpened)
            {
                teacherLessons.Activate();
                teacherLessons.Focus();
                return;
            }

            teacherLessons = new LessonListByTeacher(_repo);
            _teacherLessonsFormOpened = true;
            teacherLessons.Show();
            _teacherLessonsFormOpened = false;            
        }

        private void списокПарПоПреподавателюCtrlShiftTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowTeacherHours();
        }

        private void ShowTeacherHours()
        {
            if (_teacherHoursFormOpened)
            {
                teacherDiciplines.Activate();
                teacherDiciplines.Focus();
                return;
            }

            teacherDiciplines = new teacherHours(_repo);
            _teacherHoursFormOpened = true;
            teacherDiciplines.Show();
            _teacherHoursFormOpened = false;
        }

        private void расписаниеНаДеньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDailyLessons();
        }

        private void расписаниеСессииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSession();
        }

        private void ShowSession()
        {
            if (_sessionFormOpened)
            {
                sessionForm.Activate();
                sessionForm.Focus();
                return;
            }

            sessionForm = new Session(_repo);
            _sessionFormOpened = true;
            sessionForm.Show();
            _sessionFormOpened = false;
        }

        private void uploadTimer_Tick(object sender, EventArgs e)
        {            
            Task t = Task.Factory.StartNew(() =>
            {
                try
                {
                    WnuUpload.UploadSchedule(_repo, school ? "s_" : "");
                }
                catch 
                {   
                }                
            });                         
        }                
    }
}
