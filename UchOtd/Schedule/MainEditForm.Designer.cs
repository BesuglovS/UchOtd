namespace Schedule
{
    partial class MainEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.WeekFilter = new System.Windows.Forms.ComboBox();
            this.weekFiltered = new System.Windows.Forms.CheckBox();
            this.WholeScheduleDatesExport = new System.Windows.Forms.Button();
            this.DownloadAndRestore = new System.Windows.Forms.Button();
            this.BackupAndUpload = new System.Windows.Forms.Button();
            this.AllInPDF = new System.Windows.Forms.Button();
            this.CreatePDF = new System.Windows.Forms.Button();
            this.setLayout2 = new System.Windows.Forms.Button();
            this.setLayout = new System.Windows.Forms.Button();
            this.LessonListByTeacher = new System.Windows.Forms.Button();
            this.LessonListByTFD = new System.Windows.Forms.Button();
            this.excelExport = new System.Windows.Forms.Button();
            this.dayDelta = new System.Windows.Forms.Button();
            this.scheduleHours = new System.Windows.Forms.Button();
            this.allChanges = new System.Windows.Forms.Button();
            this.auditoriums = new System.Windows.Forms.Button();
            this.oneAuditorium = new System.Windows.Forms.Button();
            this.teachersHours = new System.Windows.Forms.Button();
            this.ManyGroups = new System.Windows.Forms.Button();
            this.ActiveLessonsCount = new System.Windows.Forms.Button();
            this.DOWList = new System.Windows.Forms.ComboBox();
            this.FacultyList = new System.Windows.Forms.ComboBox();
            this.auditoriumKaput = new System.Windows.Forms.Button();
            this.notEnough = new System.Windows.Forms.Button();
            this.removelesson = new System.Windows.Forms.Button();
            this.LoadToSite = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.importFromText = new System.Windows.Forms.Button();
            this.BigRedButton = new System.Windows.Forms.Button();
            this.showGroupLessons = new System.Windows.Forms.Button();
            this.groupList = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.аудиторииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дниСеместраToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.звонкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.студентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.группыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.преподавателиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.опцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.факультетыгруппыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.занятостьАудиторийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.ScheduleView = new System.Windows.Forms.DataGridView();
            this.DBRestoreName = new System.Windows.Forms.TextBox();
            this.controlsPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.DBRestoreName);
            this.controlsPanel.Controls.Add(this.WeekFilter);
            this.controlsPanel.Controls.Add(this.weekFiltered);
            this.controlsPanel.Controls.Add(this.WholeScheduleDatesExport);
            this.controlsPanel.Controls.Add(this.DownloadAndRestore);
            this.controlsPanel.Controls.Add(this.BackupAndUpload);
            this.controlsPanel.Controls.Add(this.AllInPDF);
            this.controlsPanel.Controls.Add(this.CreatePDF);
            this.controlsPanel.Controls.Add(this.setLayout2);
            this.controlsPanel.Controls.Add(this.setLayout);
            this.controlsPanel.Controls.Add(this.LessonListByTeacher);
            this.controlsPanel.Controls.Add(this.LessonListByTFD);
            this.controlsPanel.Controls.Add(this.excelExport);
            this.controlsPanel.Controls.Add(this.dayDelta);
            this.controlsPanel.Controls.Add(this.scheduleHours);
            this.controlsPanel.Controls.Add(this.allChanges);
            this.controlsPanel.Controls.Add(this.auditoriums);
            this.controlsPanel.Controls.Add(this.oneAuditorium);
            this.controlsPanel.Controls.Add(this.teachersHours);
            this.controlsPanel.Controls.Add(this.ManyGroups);
            this.controlsPanel.Controls.Add(this.ActiveLessonsCount);
            this.controlsPanel.Controls.Add(this.DOWList);
            this.controlsPanel.Controls.Add(this.FacultyList);
            this.controlsPanel.Controls.Add(this.auditoriumKaput);
            this.controlsPanel.Controls.Add(this.notEnough);
            this.controlsPanel.Controls.Add(this.removelesson);
            this.controlsPanel.Controls.Add(this.LoadToSite);
            this.controlsPanel.Controls.Add(this.button1);
            this.controlsPanel.Controls.Add(this.importFromText);
            this.controlsPanel.Controls.Add(this.BigRedButton);
            this.controlsPanel.Controls.Add(this.showGroupLessons);
            this.controlsPanel.Controls.Add(this.groupList);
            this.controlsPanel.Controls.Add(this.menuStrip1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(876, 177);
            this.controlsPanel.TabIndex = 0;
            // 
            // WeekFilter
            // 
            this.WeekFilter.FormattingEnabled = true;
            this.WeekFilter.Location = new System.Drawing.Point(139, 140);
            this.WeekFilter.Name = "WeekFilter";
            this.WeekFilter.Size = new System.Drawing.Size(60, 21);
            this.WeekFilter.TabIndex = 35;
            // 
            // weekFiltered
            // 
            this.weekFiltered.AutoSize = true;
            this.weekFiltered.Location = new System.Drawing.Point(13, 144);
            this.weekFiltered.Name = "weekFiltered";
            this.weekFiltered.Size = new System.Drawing.Size(120, 17);
            this.weekFiltered.TabIndex = 34;
            this.weekFiltered.Text = "Фильтр по неделе";
            this.weekFiltered.UseVisualStyleBackColor = true;
            // 
            // WholeScheduleDatesExport
            // 
            this.WholeScheduleDatesExport.Location = new System.Drawing.Point(130, 56);
            this.WholeScheduleDatesExport.Name = "WholeScheduleDatesExport";
            this.WholeScheduleDatesExport.Size = new System.Drawing.Size(135, 23);
            this.WholeScheduleDatesExport.TabIndex = 33;
            this.WholeScheduleDatesExport.Text = "Все даты расписания";
            this.WholeScheduleDatesExport.UseVisualStyleBackColor = true;
            this.WholeScheduleDatesExport.Click += new System.EventHandler(this.WholeScheduleDatesExport_Click);
            // 
            // DownloadAndRestore
            // 
            this.DownloadAndRestore.Location = new System.Drawing.Point(761, 138);
            this.DownloadAndRestore.Name = "DownloadAndRestore";
            this.DownloadAndRestore.Size = new System.Drawing.Size(103, 23);
            this.DownloadAndRestore.TabIndex = 32;
            this.DownloadAndRestore.Text = "DownloadRestore";
            this.DownloadAndRestore.UseVisualStyleBackColor = true;
            this.DownloadAndRestore.Click += new System.EventHandler(this.DownloadAndRestore_Click);
            // 
            // BackupAndUpload
            // 
            this.BackupAndUpload.Location = new System.Drawing.Point(474, 142);
            this.BackupAndUpload.Name = "BackupAndUpload";
            this.BackupAndUpload.Size = new System.Drawing.Size(110, 23);
            this.BackupAndUpload.TabIndex = 31;
            this.BackupAndUpload.Text = "BackupUpload";
            this.BackupAndUpload.UseVisualStyleBackColor = true;
            this.BackupAndUpload.Click += new System.EventHandler(this.BackupAndUpload_Click);
            // 
            // AllInPDF
            // 
            this.AllInPDF.Location = new System.Drawing.Point(661, 84);
            this.AllInPDF.Name = "AllInPDF";
            this.AllInPDF.Size = new System.Drawing.Size(94, 23);
            this.AllInPDF.TabIndex = 30;
            this.AllInPDF.Text = "Всё в PDF";
            this.AllInPDF.UseVisualStyleBackColor = true;
            this.AllInPDF.Click += new System.EventHandler(this.AllInPDF_Click);
            // 
            // CreatePDF
            // 
            this.CreatePDF.Location = new System.Drawing.Point(615, 84);
            this.CreatePDF.Name = "CreatePDF";
            this.CreatePDF.Size = new System.Drawing.Size(40, 24);
            this.CreatePDF.TabIndex = 29;
            this.CreatePDF.Text = "PDF";
            this.CreatePDF.UseVisualStyleBackColor = true;
            this.CreatePDF.Click += new System.EventHandler(this.CreatePDF_Click);
            // 
            // setLayout2
            // 
            this.setLayout2.Location = new System.Drawing.Point(548, 115);
            this.setLayout2.Name = "setLayout2";
            this.setLayout2.Size = new System.Drawing.Size(36, 21);
            this.setLayout2.TabIndex = 28;
            this.setLayout2.Text = "L2";
            this.setLayout2.UseVisualStyleBackColor = true;
            this.setLayout2.Click += new System.EventHandler(this.setLayout2_Click);
            // 
            // setLayout
            // 
            this.setLayout.Location = new System.Drawing.Point(506, 115);
            this.setLayout.Name = "setLayout";
            this.setLayout.Size = new System.Drawing.Size(36, 21);
            this.setLayout.TabIndex = 27;
            this.setLayout.Text = "L1";
            this.setLayout.UseVisualStyleBackColor = true;
            this.setLayout.Click += new System.EventHandler(this.setLayout_Click);
            // 
            // LessonListByTeacher
            // 
            this.LessonListByTeacher.Location = new System.Drawing.Point(317, 114);
            this.LessonListByTeacher.Name = "LessonListByTeacher";
            this.LessonListByTeacher.Size = new System.Drawing.Size(183, 22);
            this.LessonListByTeacher.TabIndex = 25;
            this.LessonListByTeacher.Text = "Список пар по преподавателям";
            this.LessonListByTeacher.UseVisualStyleBackColor = true;
            this.LessonListByTeacher.Click += new System.EventHandler(this.LessonListByTeacher_Click);
            // 
            // LessonListByTFD
            // 
            this.LessonListByTFD.Location = new System.Drawing.Point(172, 114);
            this.LessonListByTFD.Name = "LessonListByTFD";
            this.LessonListByTFD.Size = new System.Drawing.Size(139, 23);
            this.LessonListByTFD.TabIndex = 24;
            this.LessonListByTFD.Text = "Список пар по TFD";
            this.LessonListByTFD.UseVisualStyleBackColor = true;
            this.LessonListByTFD.Click += new System.EventHandler(this.LessonListByTFD_Click);
            // 
            // excelExport
            // 
            this.excelExport.Location = new System.Drawing.Point(12, 115);
            this.excelExport.Name = "excelExport";
            this.excelExport.Size = new System.Drawing.Size(154, 23);
            this.excelExport.TabIndex = 23;
            this.excelExport.Text = "Экспорт в Excel";
            this.excelExport.UseVisualStyleBackColor = true;
            this.excelExport.Click += new System.EventHandler(this.excelExport_Click);
            // 
            // dayDelta
            // 
            this.dayDelta.Location = new System.Drawing.Point(761, 56);
            this.dayDelta.Name = "dayDelta";
            this.dayDelta.Size = new System.Drawing.Size(103, 23);
            this.dayDelta.TabIndex = 22;
            this.dayDelta.Text = "Δ";
            this.dayDelta.UseVisualStyleBackColor = true;
            this.dayDelta.Click += new System.EventHandler(this.dayDelta_Click);
            // 
            // scheduleHours
            // 
            this.scheduleHours.Location = new System.Drawing.Point(761, 86);
            this.scheduleHours.Name = "scheduleHours";
            this.scheduleHours.Size = new System.Drawing.Size(103, 46);
            this.scheduleHours.TabIndex = 21;
            this.scheduleHours.Text = "Динамика часов в семестре";
            this.scheduleHours.UseVisualStyleBackColor = true;
            this.scheduleHours.Click += new System.EventHandler(this.scheduleHours_Click);
            // 
            // allChanges
            // 
            this.allChanges.Location = new System.Drawing.Point(434, 85);
            this.allChanges.Name = "allChanges";
            this.allChanges.Size = new System.Drawing.Size(154, 23);
            this.allChanges.TabIndex = 20;
            this.allChanges.Text = "Все изменения";
            this.allChanges.UseVisualStyleBackColor = true;
            this.allChanges.Click += new System.EventHandler(this.allChanges_Click);
            // 
            // auditoriums
            // 
            this.auditoriums.Location = new System.Drawing.Point(317, 85);
            this.auditoriums.Name = "auditoriums";
            this.auditoriums.Size = new System.Drawing.Size(111, 23);
            this.auditoriums.TabIndex = 19;
            this.auditoriums.Text = "Все аудитории";
            this.auditoriums.UseVisualStyleBackColor = true;
            this.auditoriums.Click += new System.EventHandler(this.auditoriums_Click);
            // 
            // oneAuditorium
            // 
            this.oneAuditorium.Location = new System.Drawing.Point(172, 85);
            this.oneAuditorium.Name = "oneAuditorium";
            this.oneAuditorium.Size = new System.Drawing.Size(139, 23);
            this.oneAuditorium.TabIndex = 18;
            this.oneAuditorium.Text = "Занятость аудитории";
            this.oneAuditorium.UseVisualStyleBackColor = true;
            this.oneAuditorium.Click += new System.EventHandler(this.oneAuditorium_Click);
            // 
            // teachersHours
            // 
            this.teachersHours.Location = new System.Drawing.Point(12, 86);
            this.teachersHours.Name = "teachersHours";
            this.teachersHours.Size = new System.Drawing.Size(154, 23);
            this.teachersHours.TabIndex = 17;
            this.teachersHours.Text = "Часы по преподавателям";
            this.teachersHours.UseVisualStyleBackColor = true;
            this.teachersHours.Click += new System.EventHandler(this.teachersHours_Click);
            // 
            // ManyGroups
            // 
            this.ManyGroups.Location = new System.Drawing.Point(12, 57);
            this.ManyGroups.Name = "ManyGroups";
            this.ManyGroups.Size = new System.Drawing.Size(112, 23);
            this.ManyGroups.TabIndex = 16;
            this.ManyGroups.Text = "Много групп";
            this.ManyGroups.UseVisualStyleBackColor = true;
            this.ManyGroups.Click += new System.EventHandler(this.ManyGroups_Click);
            // 
            // ActiveLessonsCount
            // 
            this.ActiveLessonsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ActiveLessonsCount.Location = new System.Drawing.Point(494, 56);
            this.ActiveLessonsCount.Name = "ActiveLessonsCount";
            this.ActiveLessonsCount.Size = new System.Drawing.Size(94, 23);
            this.ActiveLessonsCount.TabIndex = 15;
            this.ActiveLessonsCount.Text = "Пар в расписании";
            this.ActiveLessonsCount.UseVisualStyleBackColor = true;
            this.ActiveLessonsCount.Click += new System.EventHandler(this.ActiveLessonsCount_Click);
            // 
            // DOWList
            // 
            this.DOWList.FormattingEnabled = true;
            this.DOWList.Location = new System.Drawing.Point(661, 57);
            this.DOWList.Name = "DOWList";
            this.DOWList.Size = new System.Drawing.Size(94, 21);
            this.DOWList.TabIndex = 11;
            // 
            // FacultyList
            // 
            this.FacultyList.FormattingEnabled = true;
            this.FacultyList.Location = new System.Drawing.Point(614, 56);
            this.FacultyList.Name = "FacultyList";
            this.FacultyList.Size = new System.Drawing.Size(41, 21);
            this.FacultyList.TabIndex = 10;
            // 
            // auditoriumKaput
            // 
            this.auditoriumKaput.Location = new System.Drawing.Point(354, 56);
            this.auditoriumKaput.Name = "auditoriumKaput";
            this.auditoriumKaput.Size = new System.Drawing.Size(134, 23);
            this.auditoriumKaput.TabIndex = 9;
            this.auditoriumKaput.Text = "Коллизии аудиторий";
            this.auditoriumKaput.UseVisualStyleBackColor = true;
            this.auditoriumKaput.Click += new System.EventHandler(this.AuditoriumKaputClick);
            // 
            // notEnough
            // 
            this.notEnough.Location = new System.Drawing.Point(271, 56);
            this.notEnough.Name = "notEnough";
            this.notEnough.Size = new System.Drawing.Size(77, 23);
            this.notEnough.TabIndex = 8;
            this.notEnough.Text = "Не хватает";
            this.notEnough.UseVisualStyleBackColor = true;
            this.notEnough.Click += new System.EventHandler(this.NotEnoughClick);
            // 
            // removelesson
            // 
            this.removelesson.Location = new System.Drawing.Point(474, 25);
            this.removelesson.Name = "removelesson";
            this.removelesson.Size = new System.Drawing.Size(114, 23);
            this.removelesson.TabIndex = 7;
            this.removelesson.Text = "Удалить урок";
            this.removelesson.UseVisualStyleBackColor = true;
            this.removelesson.Click += new System.EventHandler(this.RemovelessonClick);
            // 
            // LoadToSite
            // 
            this.LoadToSite.Location = new System.Drawing.Point(614, 27);
            this.LoadToSite.Name = "LoadToSite";
            this.LoadToSite.Size = new System.Drawing.Size(124, 22);
            this.LoadToSite.TabIndex = 6;
            this.LoadToSite.Text = "Загрузить на сайт";
            this.LoadToSite.UseVisualStyleBackColor = true;
            this.LoadToSite.Click += new System.EventHandler(this.LoadToSiteClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(354, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Добавить урок";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // importFromText
            // 
            this.importFromText.Enabled = false;
            this.importFromText.Location = new System.Drawing.Point(235, 25);
            this.importFromText.Name = "importFromText";
            this.importFromText.Size = new System.Drawing.Size(113, 23);
            this.importFromText.TabIndex = 4;
            this.importFromText.Text = "Import from Text";
            this.importFromText.UseVisualStyleBackColor = true;
            this.importFromText.Click += new System.EventHandler(this.ImportFromTextClick);
            // 
            // BigRedButton
            // 
            this.BigRedButton.Location = new System.Drawing.Point(744, 26);
            this.BigRedButton.Name = "BigRedButton";
            this.BigRedButton.Size = new System.Drawing.Size(120, 23);
            this.BigRedButton.TabIndex = 3;
            this.BigRedButton.Text = "Big red button";
            this.BigRedButton.UseVisualStyleBackColor = true;
            this.BigRedButton.Click += new System.EventHandler(this.BigRedButtonClick);
            // 
            // showGroupLessons
            // 
            this.showGroupLessons.Location = new System.Drawing.Point(139, 25);
            this.showGroupLessons.Name = "showGroupLessons";
            this.showGroupLessons.Size = new System.Drawing.Size(75, 23);
            this.showGroupLessons.TabIndex = 1;
            this.showGroupLessons.Text = "Go";
            this.showGroupLessons.UseVisualStyleBackColor = true;
            this.showGroupLessons.Click += new System.EventHandler(this.ShowGroupLessonsClick);
            // 
            // groupList
            // 
            this.groupList.FormattingEnabled = true;
            this.groupList.Location = new System.Drawing.Point(12, 27);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(121, 21);
            this.groupList.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(876, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.аудиторииToolStripMenuItem,
            this.дниСеместраToolStripMenuItem,
            this.звонкиToolStripMenuItem,
            this.студентыToolStripMenuItem,
            this.группыToolStripMenuItem,
            this.преподавателиToolStripMenuItem,
            this.дисциплиныToolStripMenuItem,
            this.опцииToolStripMenuItem,
            this.факультетыгруппыToolStripMenuItem,
            this.занятостьАудиторийToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // аудиторииToolStripMenuItem
            // 
            this.аудиторииToolStripMenuItem.Name = "аудиторииToolStripMenuItem";
            this.аудиторииToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.аудиторииToolStripMenuItem.Text = "Аудитории";
            this.аудиторииToolStripMenuItem.Click += new System.EventHandler(this.АудиторииToolStripMenuItemClick);
            // 
            // дниСеместраToolStripMenuItem
            // 
            this.дниСеместраToolStripMenuItem.Name = "дниСеместраToolStripMenuItem";
            this.дниСеместраToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.дниСеместраToolStripMenuItem.Text = "Дни семестра";
            this.дниСеместраToolStripMenuItem.Click += new System.EventHandler(this.ДниСеместраToolStripMenuItemClick);
            // 
            // звонкиToolStripMenuItem
            // 
            this.звонкиToolStripMenuItem.Name = "звонкиToolStripMenuItem";
            this.звонкиToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.звонкиToolStripMenuItem.Text = "Звонки";
            this.звонкиToolStripMenuItem.Click += new System.EventHandler(this.ЗвонкиToolStripMenuItemClick);
            // 
            // студентыToolStripMenuItem
            // 
            this.студентыToolStripMenuItem.Name = "студентыToolStripMenuItem";
            this.студентыToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.студентыToolStripMenuItem.Text = "Студенты";
            this.студентыToolStripMenuItem.Click += new System.EventHandler(this.СтудентыToolStripMenuItemClick);
            // 
            // группыToolStripMenuItem
            // 
            this.группыToolStripMenuItem.Name = "группыToolStripMenuItem";
            this.группыToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.группыToolStripMenuItem.Text = "Группы";
            this.группыToolStripMenuItem.Click += new System.EventHandler(this.ГруппыToolStripMenuItemClick);
            // 
            // преподавателиToolStripMenuItem
            // 
            this.преподавателиToolStripMenuItem.Name = "преподавателиToolStripMenuItem";
            this.преподавателиToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.преподавателиToolStripMenuItem.Text = "Преподаватели";
            this.преподавателиToolStripMenuItem.Click += new System.EventHandler(this.ПреподавателиToolStripMenuItemClick);
            // 
            // дисциплиныToolStripMenuItem
            // 
            this.дисциплиныToolStripMenuItem.Name = "дисциплиныToolStripMenuItem";
            this.дисциплиныToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.дисциплиныToolStripMenuItem.Text = "Дисциплины";
            this.дисциплиныToolStripMenuItem.Click += new System.EventHandler(this.ДисциплиныToolStripMenuItemClick);
            // 
            // опцииToolStripMenuItem
            // 
            this.опцииToolStripMenuItem.Name = "опцииToolStripMenuItem";
            this.опцииToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.опцииToolStripMenuItem.Text = "Опции";
            this.опцииToolStripMenuItem.Click += new System.EventHandler(this.ОпцииToolStripMenuItemClick);
            // 
            // факультетыгруппыToolStripMenuItem
            // 
            this.факультетыгруппыToolStripMenuItem.Name = "факультетыгруппыToolStripMenuItem";
            this.факультетыгруппыToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.факультетыгруппыToolStripMenuItem.Text = "Факультеты+группы";
            this.факультетыгруппыToolStripMenuItem.Click += new System.EventHandler(this.ФакультетыгруппыToolStripMenuItemClick);
            // 
            // занятостьАудиторийToolStripMenuItem
            // 
            this.занятостьАудиторийToolStripMenuItem.Name = "занятостьАудиторийToolStripMenuItem";
            this.занятостьАудиторийToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.занятостьАудиторийToolStripMenuItem.Text = "Занятость аудиторий";
            this.занятостьАудиторийToolStripMenuItem.Click += new System.EventHandler(this.занятостьАудиторийToolStripMenuItem_Click);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.ScheduleView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 177);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(876, 412);
            this.viewPanel.TabIndex = 1;
            // 
            // ScheduleView
            // 
            this.ScheduleView.AllowUserToAddRows = false;
            this.ScheduleView.AllowUserToDeleteRows = false;
            this.ScheduleView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ScheduleView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScheduleView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScheduleView.Location = new System.Drawing.Point(0, 0);
            this.ScheduleView.Name = "ScheduleView";
            this.ScheduleView.ReadOnly = true;
            this.ScheduleView.RowHeadersVisible = false;
            this.ScheduleView.Size = new System.Drawing.Size(876, 412);
            this.ScheduleView.TabIndex = 1;
            this.ScheduleView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainViewCellDoubleClick);
            // 
            // DBRestoreName
            // 
            this.DBRestoreName.Location = new System.Drawing.Point(615, 140);
            this.DBRestoreName.Name = "DBRestoreName";
            this.DBRestoreName.Size = new System.Drawing.Size(140, 20);
            this.DBRestoreName.TabIndex = 36;
            // 
            // MainEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 589);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Расписание";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Button showGroupLessons;
        private System.Windows.Forms.ComboBox groupList;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem аудиторииToolStripMenuItem;
        private System.Windows.Forms.Button BigRedButton;
        private System.Windows.Forms.ToolStripMenuItem дниСеместраToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem звонкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem студентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem группыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem преподавателиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дисциплиныToolStripMenuItem;
        private System.Windows.Forms.DataGridView ScheduleView;
        private System.Windows.Forms.Button importFromText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button LoadToSite;
        private System.Windows.Forms.Button removelesson;
        private System.Windows.Forms.ToolStripMenuItem опцииToolStripMenuItem;
        private System.Windows.Forms.Button notEnough;
        private System.Windows.Forms.Button auditoriumKaput;
        private System.Windows.Forms.ToolStripMenuItem факультетыгруппыToolStripMenuItem;
        private System.Windows.Forms.ComboBox FacultyList;
        private System.Windows.Forms.ComboBox DOWList;
        private System.Windows.Forms.Button ActiveLessonsCount;
        private System.Windows.Forms.Button ManyGroups;
        private System.Windows.Forms.ToolStripMenuItem занятостьАудиторийToolStripMenuItem;
        private System.Windows.Forms.Button teachersHours;
        private System.Windows.Forms.Button oneAuditorium;
        private System.Windows.Forms.Button auditoriums;
        private System.Windows.Forms.Button allChanges;
        private System.Windows.Forms.Button scheduleHours;
        private System.Windows.Forms.Button dayDelta;
        private System.Windows.Forms.Button excelExport;
        private System.Windows.Forms.Button LessonListByTFD;
        private System.Windows.Forms.Button LessonListByTeacher;
        private System.Windows.Forms.Button setLayout;
        private System.Windows.Forms.Button setLayout2;
        private System.Windows.Forms.Button CreatePDF;
        private System.Windows.Forms.Button AllInPDF;
        private System.Windows.Forms.Button DownloadAndRestore;
        private System.Windows.Forms.Button BackupAndUpload;
        private System.Windows.Forms.Button WholeScheduleDatesExport;
        private System.Windows.Forms.CheckBox weekFiltered;
        private System.Windows.Forms.ComboBox WeekFilter;
        private System.Windows.Forms.TextBox DBRestoreName;
    }
}

