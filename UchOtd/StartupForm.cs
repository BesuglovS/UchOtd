﻿using System;
using System.Windows.Forms;
using Schedule.Repositories;
using UchOtd.Forms;
using UchOtd.NUDS;
using UchOtd.NUDS.Forms;
using UchOtd.Forms.Notes;
using UchOtd.Repositories;
using System.IO;
using UchOtd.Core;
using Schedule;
using System.Net.NetworkInformation;
using System.Text;
using System.Collections.Generic;

namespace UchOtd
{
    public partial class StartupForm : Form
    {
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

        public StartupForm()
        {
            InitializeComponent();

            InitRepositories();

            // Контингент - Alt-S
            HotKeyManager.RegisterHotKey(Keys.S, KeyModifiers.Alt);
            // Просмотр расписания - Alt-V
            HotKeyManager.RegisterHotKey(Keys.V, KeyModifiers.Alt);
            // Расписание преподавателя - Alt-T
            HotKeyManager.RegisterHotKey(Keys.T, KeyModifiers.Alt);
            // Изменения расписание - Alt-C
            HotKeyManager.RegisterHotKey(Keys.C, KeyModifiers.Alt);
            // Занятость аудиторий - Alt-A
            HotKeyManager.RegisterHotKey(Keys.A, KeyModifiers.Alt);
            // Заметки - Alt-N
            HotKeyManager.RegisterHotKey(Keys.N, KeyModifiers.Alt);
            // Телефоны - Alt-P
            HotKeyManager.RegisterHotKey(Keys.P, KeyModifiers.Alt);
            // Редакторование расписания - Alt-R
            HotKeyManager.RegisterHotKey(Keys.R, KeyModifiers.Alt);

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

            trayIcon.Visible = true;
        }

        private void InitRepositories()
        {
            var ServerList = new List<string>() { 
                "127.0.0.1", 
                "10.13.3.1"
            };

            bool successPing = false;
            int connectionIndex = 0;
            do
            {
                var serverName = ServerList[connectionIndex];
                if (PingServerExistence(serverName))
                {
                    successPing = true;

                    _repo = new ScheduleRepository("data source=tcp:" + serverName + ",1433;Database=ScheduleDB;User ID = sa;Password = ghjuhfvvf;multipleactiveresultsets=True");
                    _UOrepo = new UchOtdRepository("data source=tcp:" + serverName + ",1433;Database=UchOtd;User ID = sa;Password = ghjuhfvvf;multipleactiveresultsets=True");
                }
            } while (!successPing && connectionIndex != ServerList.Count);

            if (!successPing)
            {
                MessageBox.Show("Не удалось подключится к базе данных.");
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
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = null;
            try
            {
                reply = pingSender.Send(server, timeout, buffer, options);
            }
            catch (Exception e)
            {
                return false;
            }            

            if ((reply != null) && (reply.Status == IPStatus.Success))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void RefreshDbOrConnectionName()
        {
            if (_repo != null)
            {
                openDBToolStripMenuItem.Text = "Сменить базу данных (" + Utilities.ExtractDBOrConnectionName(_repo.ConnectionString) + ")";
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
            }
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
            PhonesForm.ShowDialog();
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
            NotesForm.ShowDialog();
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
            _teacherScheduleFormOpened = true;
            BuildingForm.ShowDialog();
            _teacherScheduleFormOpened = false;
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
            TeacherScheduleForm.ShowDialog();
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
            ScheduleForm.ShowDialog();
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
            StudentListForm.ShowDialog();
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
            ChangesForm.ShowDialog();
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
            openDBForm.ShowDialog();

            RefreshDbOrConnectionName();
        }

        private void EditScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditScheduleForm();
        }                
    }
}
