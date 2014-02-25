using System;
using System.Windows.Forms;
using Schedule.Repositories;
using UchOtd.Forms;
using UchOtd.NUDS;
using UchOtd.NUDS.Forms;
using UchOtd.Forms.Notes;
using UchOtd.Repositories;
using System.IO;
using UchOtd.Core;

namespace UchOtd
{
    public partial class StartupForm : Form
    {
        public readonly ScheduleRepository _repo = new ScheduleRepository();
        public readonly UchOtdRepository _UOrepo = new UchOtdRepository(); 

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

        public StartupForm()
        {
            InitializeComponent();

            // Peek EF Database connection
            _repo.GetAllFaculties();

            // Контингент - Alt-S
            HotKeyManager.RegisterHotKey(Keys.S, KeyModifiers.Alt);
            // Расписание - Alt-R
            HotKeyManager.RegisterHotKey(Keys.R, KeyModifiers.Alt);
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

            trayIcon.Visible = true;
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
                if (e.Key == Keys.R)
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
            }
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
    }
}
