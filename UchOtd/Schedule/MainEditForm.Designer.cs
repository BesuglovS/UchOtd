namespace UchOtd.Schedule
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
                Repo.Dispose();
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
            this.startSchoolWordExport = new System.Windows.Forms.Button();
            this.ToDBName = new System.Windows.Forms.TextBox();
            this.DownloadRestore = new System.Windows.Forms.Button();
            this.FromDBName = new System.Windows.Forms.TextBox();
            this.BackupUpload = new System.Windows.Forms.Button();
            this.cb90 = new System.Windows.Forms.CheckBox();
            this.OnePageGroupScheduleWordExport = new System.Windows.Forms.Button();
            this.WordWholeScheduleOneGroupOnePage = new System.Windows.Forms.Button();
            this.uploadPrefix = new System.Windows.Forms.TextBox();
            this.happyBirthday = new System.Windows.Forms.Button();
            this.WordSchool2 = new System.Windows.Forms.Button();
            this.WordSchool = new System.Windows.Forms.Button();
            this.BIGREDBUTTON = new System.Windows.Forms.Button();
            this.WordExportWeekFilter = new System.Windows.Forms.ComboBox();
            this.wordExportWeekFiltered = new System.Windows.Forms.CheckBox();
            this.WordCustom = new System.Windows.Forms.Button();
            this.WordExportButton = new System.Windows.Forms.Button();
            this.WordFacultyFilter = new System.Windows.Forms.ComboBox();
            this.WordOneFaculty = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.WeekFilter = new System.Windows.Forms.ComboBox();
            this.weekFiltered = new System.Windows.Forms.CheckBox();
            this.CreatePDF = new System.Windows.Forms.Button();
            this.allChanges = new System.Windows.Forms.Button();
            this.auditoriums = new System.Windows.Forms.Button();
            this.oneAuditorium = new System.Windows.Forms.Button();
            this.ManyGroups = new System.Windows.Forms.Button();
            this.ActiveLessonsCount = new System.Windows.Forms.Button();
            this.DOWList = new System.Windows.Forms.ComboBox();
            this.FacultyList = new System.Windows.Forms.ComboBox();
            this.auditoriumKaput = new System.Windows.Forms.Button();
            this.LoadToSite = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.showGroupLessons = new System.Windows.Forms.Button();
            this.groupList = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.аудиторииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.корпусаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дниСеместраToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.звонкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.студентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.группыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.преподавателиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.опцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.факультетыгруппыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.занятостьАудиторийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.анализToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пожеланияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.ScheduleView = new System.Windows.Forms.DataGridView();
            this.controlsPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.startSchoolWordExport);
            this.controlsPanel.Controls.Add(this.ToDBName);
            this.controlsPanel.Controls.Add(this.DownloadRestore);
            this.controlsPanel.Controls.Add(this.FromDBName);
            this.controlsPanel.Controls.Add(this.BackupUpload);
            this.controlsPanel.Controls.Add(this.cb90);
            this.controlsPanel.Controls.Add(this.OnePageGroupScheduleWordExport);
            this.controlsPanel.Controls.Add(this.WordWholeScheduleOneGroupOnePage);
            this.controlsPanel.Controls.Add(this.uploadPrefix);
            this.controlsPanel.Controls.Add(this.happyBirthday);
            this.controlsPanel.Controls.Add(this.WordSchool2);
            this.controlsPanel.Controls.Add(this.WordSchool);
            this.controlsPanel.Controls.Add(this.BIGREDBUTTON);
            this.controlsPanel.Controls.Add(this.WordExportWeekFilter);
            this.controlsPanel.Controls.Add(this.wordExportWeekFiltered);
            this.controlsPanel.Controls.Add(this.WordCustom);
            this.controlsPanel.Controls.Add(this.WordExportButton);
            this.controlsPanel.Controls.Add(this.WordFacultyFilter);
            this.controlsPanel.Controls.Add(this.WordOneFaculty);
            this.controlsPanel.Controls.Add(this.button2);
            this.controlsPanel.Controls.Add(this.WeekFilter);
            this.controlsPanel.Controls.Add(this.weekFiltered);
            this.controlsPanel.Controls.Add(this.CreatePDF);
            this.controlsPanel.Controls.Add(this.allChanges);
            this.controlsPanel.Controls.Add(this.auditoriums);
            this.controlsPanel.Controls.Add(this.oneAuditorium);
            this.controlsPanel.Controls.Add(this.ManyGroups);
            this.controlsPanel.Controls.Add(this.ActiveLessonsCount);
            this.controlsPanel.Controls.Add(this.DOWList);
            this.controlsPanel.Controls.Add(this.FacultyList);
            this.controlsPanel.Controls.Add(this.auditoriumKaput);
            this.controlsPanel.Controls.Add(this.LoadToSite);
            this.controlsPanel.Controls.Add(this.button1);
            this.controlsPanel.Controls.Add(this.showGroupLessons);
            this.controlsPanel.Controls.Add(this.groupList);
            this.controlsPanel.Controls.Add(this.menuStrip1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(967, 155);
            this.controlsPanel.TabIndex = 0;
            // 
            // startSchoolWordExport
            // 
            this.startSchoolWordExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startSchoolWordExport.Location = new System.Drawing.Point(740, 52);
            this.startSchoolWordExport.Name = "startSchoolWordExport";
            this.startSchoolWordExport.Size = new System.Drawing.Size(93, 24);
            this.startSchoolWordExport.TabIndex = 62;
            this.startSchoolWordExport.Text = "Word (1-7)";
            this.startSchoolWordExport.UseVisualStyleBackColor = true;
            this.startSchoolWordExport.Click += new System.EventHandler(this.startSchoolWordExport_Click);
            // 
            // ToDBName
            // 
            this.ToDBName.Location = new System.Drawing.Point(871, 118);
            this.ToDBName.Name = "ToDBName";
            this.ToDBName.Size = new System.Drawing.Size(78, 20);
            this.ToDBName.TabIndex = 61;
            // 
            // DownloadRestore
            // 
            this.DownloadRestore.Location = new System.Drawing.Point(790, 109);
            this.DownloadRestore.Name = "DownloadRestore";
            this.DownloadRestore.Size = new System.Drawing.Size(75, 35);
            this.DownloadRestore.TabIndex = 60;
            this.DownloadRestore.Text = "Download + restore";
            this.DownloadRestore.UseVisualStyleBackColor = true;
            // 
            // FromDBName
            // 
            this.FromDBName.Location = new System.Drawing.Point(599, 118);
            this.FromDBName.Name = "FromDBName";
            this.FromDBName.Size = new System.Drawing.Size(100, 20);
            this.FromDBName.TabIndex = 59;
            // 
            // BackupUpload
            // 
            this.BackupUpload.Location = new System.Drawing.Point(709, 110);
            this.BackupUpload.Name = "BackupUpload";
            this.BackupUpload.Size = new System.Drawing.Size(75, 35);
            this.BackupUpload.TabIndex = 58;
            this.BackupUpload.Text = "Backup + Upload";
            this.BackupUpload.UseVisualStyleBackColor = true;
            this.BackupUpload.Click += new System.EventHandler(this.BackupUpload_Click);
            // 
            // cb90
            // 
            this.cb90.AutoSize = true;
            this.cb90.Location = new System.Drawing.Point(526, 109);
            this.cb90.Name = "cb90";
            this.cb90.Size = new System.Drawing.Size(71, 17);
            this.cb90.TabIndex = 56;
            this.cb90.Text = "90 минут";
            this.cb90.UseVisualStyleBackColor = true;
            // 
            // OnePageGroupScheduleWordExport
            // 
            this.OnePageGroupScheduleWordExport.Location = new System.Drawing.Point(94, 115);
            this.OnePageGroupScheduleWordExport.Name = "OnePageGroupScheduleWordExport";
            this.OnePageGroupScheduleWordExport.Size = new System.Drawing.Size(165, 23);
            this.OnePageGroupScheduleWordExport.TabIndex = 55;
            this.OnePageGroupScheduleWordExport.Text = "Экспорт в Word - одна группа";
            this.OnePageGroupScheduleWordExport.UseVisualStyleBackColor = true;
            this.OnePageGroupScheduleWordExport.Click += new System.EventHandler(this.OnePageGroupScheduleWordExport_Click);
            // 
            // WordWholeScheduleOneGroupOnePage
            // 
            this.WordWholeScheduleOneGroupOnePage.Location = new System.Drawing.Point(265, 115);
            this.WordWholeScheduleOneGroupOnePage.Name = "WordWholeScheduleOneGroupOnePage";
            this.WordWholeScheduleOneGroupOnePage.Size = new System.Drawing.Size(255, 23);
            this.WordWholeScheduleOneGroupOnePage.TabIndex = 54;
            this.WordWholeScheduleOneGroupOnePage.Text = "Всё расписание в Word 1 группа на 1 стр.";
            this.WordWholeScheduleOneGroupOnePage.UseVisualStyleBackColor = true;
            this.WordWholeScheduleOneGroupOnePage.Click += new System.EventHandler(this.WordWholeScheduleOneGroupOnePage_Click);
            // 
            // uploadPrefix
            // 
            this.uploadPrefix.Location = new System.Drawing.Point(306, 27);
            this.uploadPrefix.Name = "uploadPrefix";
            this.uploadPrefix.Size = new System.Drawing.Size(31, 20);
            this.uploadPrefix.TabIndex = 53;
            // 
            // happyBirthday
            // 
            this.happyBirthday.Location = new System.Drawing.Point(13, 115);
            this.happyBirthday.Name = "happyBirthday";
            this.happyBirthday.Size = new System.Drawing.Size(75, 23);
            this.happyBirthday.TabIndex = 52;
            this.happyBirthday.Text = "Happy";
            this.happyBirthday.UseVisualStyleBackColor = true;
            this.happyBirthday.Click += new System.EventHandler(this.happyBirthday_Click);
            // 
            // WordSchool2
            // 
            this.WordSchool2.Location = new System.Drawing.Point(705, 86);
            this.WordSchool2.Name = "WordSchool2";
            this.WordSchool2.Size = new System.Drawing.Size(123, 23);
            this.WordSchool2.TabIndex = 51;
            this.WordSchool2.Text = "Word (ШКОЛА) 2 дн.";
            this.WordSchool2.UseVisualStyleBackColor = true;
            this.WordSchool2.Click += new System.EventHandler(this.WordSchool2_Click);
            // 
            // WordSchool
            // 
            this.WordSchool.Location = new System.Drawing.Point(740, 25);
            this.WordSchool.Name = "WordSchool";
            this.WordSchool.Size = new System.Drawing.Size(93, 26);
            this.WordSchool.TabIndex = 50;
            this.WordSchool.Text = "Word (ШКОЛА)";
            this.WordSchool.UseVisualStyleBackColor = true;
            this.WordSchool.Click += new System.EventHandler(this.WordSchool_Click_1);
            // 
            // BIGREDBUTTON
            // 
            this.BIGREDBUTTON.Location = new System.Drawing.Point(834, 85);
            this.BIGREDBUTTON.Name = "BIGREDBUTTON";
            this.BIGREDBUTTON.Size = new System.Drawing.Size(115, 24);
            this.BIGREDBUTTON.TabIndex = 49;
            this.BIGREDBUTTON.Text = "BIG RED BUTTON";
            this.BIGREDBUTTON.UseVisualStyleBackColor = true;
            this.BIGREDBUTTON.Click += new System.EventHandler(this.BIGREDBUTTON_Click);
            // 
            // WordExportWeekFilter
            // 
            this.WordExportWeekFilter.FormattingEnabled = true;
            this.WordExportWeekFilter.Location = new System.Drawing.Point(839, 56);
            this.WordExportWeekFilter.Name = "WordExportWeekFilter";
            this.WordExportWeekFilter.Size = new System.Drawing.Size(120, 21);
            this.WordExportWeekFilter.TabIndex = 46;
            // 
            // wordExportWeekFiltered
            // 
            this.wordExportWeekFiltered.AutoSize = true;
            this.wordExportWeekFiltered.Location = new System.Drawing.Point(839, 30);
            this.wordExportWeekFiltered.Name = "wordExportWeekFiltered";
            this.wordExportWeekFiltered.Size = new System.Drawing.Size(120, 17);
            this.wordExportWeekFiltered.TabIndex = 45;
            this.wordExportWeekFiltered.Text = "Фильтр по неделе";
            this.wordExportWeekFiltered.UseVisualStyleBackColor = true;
            // 
            // WordCustom
            // 
            this.WordCustom.Location = new System.Drawing.Point(663, 54);
            this.WordCustom.Name = "WordCustom";
            this.WordCustom.Size = new System.Drawing.Size(71, 23);
            this.WordCustom.TabIndex = 44;
            this.WordCustom.Text = "Word +";
            this.WordCustom.UseVisualStyleBackColor = true;
            this.WordCustom.Click += new System.EventHandler(this.WordCustom_Click);
            // 
            // WordExportButton
            // 
            this.WordExportButton.Location = new System.Drawing.Point(663, 27);
            this.WordExportButton.Name = "WordExportButton";
            this.WordExportButton.Size = new System.Drawing.Size(71, 24);
            this.WordExportButton.TabIndex = 43;
            this.WordExportButton.Text = "Word";
            this.WordExportButton.UseVisualStyleBackColor = true;
            this.WordExportButton.Click += new System.EventHandler(this.WordExport_Click);
            // 
            // WordFacultyFilter
            // 
            this.WordFacultyFilter.FormattingEnabled = true;
            this.WordFacultyFilter.Location = new System.Drawing.Point(571, 82);
            this.WordFacultyFilter.Name = "WordFacultyFilter";
            this.WordFacultyFilter.Size = new System.Drawing.Size(54, 21);
            this.WordFacultyFilter.TabIndex = 40;
            // 
            // WordOneFaculty
            // 
            this.WordOneFaculty.AutoSize = true;
            this.WordOneFaculty.Location = new System.Drawing.Point(459, 86);
            this.WordOneFaculty.Name = "WordOneFaculty";
            this.WordOneFaculty.Size = new System.Drawing.Size(106, 17);
            this.WordOneFaculty.TabIndex = 39;
            this.WordOneFaculty.Text = "один факультет";
            this.WordOneFaculty.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(631, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 23);
            this.button2.TabIndex = 38;
            this.button2.Text = "Word";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // WeekFilter
            // 
            this.WeekFilter.FormattingEnabled = true;
            this.WeekFilter.Location = new System.Drawing.Point(139, 54);
            this.WeekFilter.Name = "WeekFilter";
            this.WeekFilter.Size = new System.Drawing.Size(59, 21);
            this.WeekFilter.TabIndex = 35;
            // 
            // weekFiltered
            // 
            this.weekFiltered.AutoSize = true;
            this.weekFiltered.Location = new System.Drawing.Point(13, 59);
            this.weekFiltered.Name = "weekFiltered";
            this.weekFiltered.Size = new System.Drawing.Size(120, 17);
            this.weekFiltered.TabIndex = 34;
            this.weekFiltered.Text = "Фильтр по неделе";
            this.weekFiltered.UseVisualStyleBackColor = true;
            // 
            // CreatePDF
            // 
            this.CreatePDF.Location = new System.Drawing.Point(460, 54);
            this.CreatePDF.Name = "CreatePDF";
            this.CreatePDF.Size = new System.Drawing.Size(197, 23);
            this.CreatePDF.TabIndex = 29;
            this.CreatePDF.Text = "PDF";
            this.CreatePDF.UseVisualStyleBackColor = true;
            this.CreatePDF.Click += new System.EventHandler(this.CreatePDF_Click);
            // 
            // allChanges
            // 
            this.allChanges.Location = new System.Drawing.Point(265, 86);
            this.allChanges.Name = "allChanges";
            this.allChanges.Size = new System.Drawing.Size(148, 23);
            this.allChanges.TabIndex = 20;
            this.allChanges.Text = "Все изменения";
            this.allChanges.UseVisualStyleBackColor = true;
            this.allChanges.Click += new System.EventHandler(this.allChanges_Click);
            // 
            // auditoriums
            // 
            this.auditoriums.Location = new System.Drawing.Point(148, 86);
            this.auditoriums.Name = "auditoriums";
            this.auditoriums.Size = new System.Drawing.Size(111, 23);
            this.auditoriums.TabIndex = 19;
            this.auditoriums.Text = "Все аудитории";
            this.auditoriums.UseVisualStyleBackColor = true;
            this.auditoriums.Click += new System.EventHandler(this.auditoriums_Click);
            // 
            // oneAuditorium
            // 
            this.oneAuditorium.Location = new System.Drawing.Point(13, 86);
            this.oneAuditorium.Name = "oneAuditorium";
            this.oneAuditorium.Size = new System.Drawing.Size(129, 23);
            this.oneAuditorium.TabIndex = 18;
            this.oneAuditorium.Text = "Занятость аудитории";
            this.oneAuditorium.UseVisualStyleBackColor = true;
            this.oneAuditorium.Click += new System.EventHandler(this.oneAuditorium_Click);
            // 
            // ManyGroups
            // 
            this.ManyGroups.Location = new System.Drawing.Point(206, 54);
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
            this.ActiveLessonsCount.Location = new System.Drawing.Point(419, 86);
            this.ActiveLessonsCount.Name = "ActiveLessonsCount";
            this.ActiveLessonsCount.Size = new System.Drawing.Size(29, 23);
            this.ActiveLessonsCount.TabIndex = 15;
            this.ActiveLessonsCount.Text = "%";
            this.ActiveLessonsCount.UseVisualStyleBackColor = true;
            this.ActiveLessonsCount.Click += new System.EventHandler(this.ActiveLessonsCount_Click);
            // 
            // DOWList
            // 
            this.DOWList.FormattingEnabled = true;
            this.DOWList.Location = new System.Drawing.Point(563, 27);
            this.DOWList.Name = "DOWList";
            this.DOWList.Size = new System.Drawing.Size(94, 21);
            this.DOWList.TabIndex = 11;
            // 
            // FacultyList
            // 
            this.FacultyList.FormattingEnabled = true;
            this.FacultyList.Location = new System.Drawing.Point(459, 27);
            this.FacultyList.Name = "FacultyList";
            this.FacultyList.Size = new System.Drawing.Size(98, 21);
            this.FacultyList.TabIndex = 10;
            // 
            // auditoriumKaput
            // 
            this.auditoriumKaput.Location = new System.Drawing.Point(324, 54);
            this.auditoriumKaput.Name = "auditoriumKaput";
            this.auditoriumKaput.Size = new System.Drawing.Size(124, 23);
            this.auditoriumKaput.TabIndex = 9;
            this.auditoriumKaput.Text = "Коллизии аудиторий";
            this.auditoriumKaput.UseVisualStyleBackColor = true;
            this.auditoriumKaput.Click += new System.EventHandler(this.AuditoriumKaputClick);
            // 
            // LoadToSite
            // 
            this.LoadToSite.Location = new System.Drawing.Point(343, 25);
            this.LoadToSite.Name = "LoadToSite";
            this.LoadToSite.Size = new System.Drawing.Size(110, 23);
            this.LoadToSite.TabIndex = 6;
            this.LoadToSite.Text = "Загрузить на сайт";
            this.LoadToSite.UseVisualStyleBackColor = true;
            this.LoadToSite.Click += new System.EventHandler(this.LoadToSiteClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Добавить урок";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // showGroupLessons
            // 
            this.showGroupLessons.Location = new System.Drawing.Point(139, 25);
            this.showGroupLessons.Name = "showGroupLessons";
            this.showGroupLessons.Size = new System.Drawing.Size(59, 23);
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
            this.справочникиToolStripMenuItem,
            this.анализToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(967, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.аудиторииToolStripMenuItem,
            this.корпусаToolStripMenuItem,
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
            // корпусаToolStripMenuItem
            // 
            this.корпусаToolStripMenuItem.Name = "корпусаToolStripMenuItem";
            this.корпусаToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.корпусаToolStripMenuItem.Text = "Корпуса";
            this.корпусаToolStripMenuItem.Click += new System.EventHandler(this.корпусаToolStripMenuItem_Click);
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
            // анализToolStripMenuItem
            // 
            this.анализToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.пожеланияToolStripMenuItem});
            this.анализToolStripMenuItem.Name = "анализToolStripMenuItem";
            this.анализToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.анализToolStripMenuItem.Text = "Анализ";
            // 
            // пожеланияToolStripMenuItem
            // 
            this.пожеланияToolStripMenuItem.Name = "пожеланияToolStripMenuItem";
            this.пожеланияToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.пожеланияToolStripMenuItem.Text = "Пожелания";
            this.пожеланияToolStripMenuItem.Click += new System.EventHandler(this.пожеланияToolStripMenuItem_Click);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.ScheduleView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 155);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(967, 434);
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
            this.ScheduleView.Size = new System.Drawing.Size(967, 434);
            this.ScheduleView.TabIndex = 1;
            this.ScheduleView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainViewCellDoubleClick);
            // 
            // MainEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 589);
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
        private System.Windows.Forms.ToolStripMenuItem дниСеместраToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem звонкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem студентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem группыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem преподавателиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дисциплиныToolStripMenuItem;
        private System.Windows.Forms.DataGridView ScheduleView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button LoadToSite;
        private System.Windows.Forms.ToolStripMenuItem опцииToolStripMenuItem;
        private System.Windows.Forms.Button auditoriumKaput;
        private System.Windows.Forms.ToolStripMenuItem факультетыгруппыToolStripMenuItem;
        private System.Windows.Forms.ComboBox FacultyList;
        private System.Windows.Forms.ComboBox DOWList;
        private System.Windows.Forms.Button ActiveLessonsCount;
        private System.Windows.Forms.Button ManyGroups;
        private System.Windows.Forms.ToolStripMenuItem занятостьАудиторийToolStripMenuItem;
        private System.Windows.Forms.Button oneAuditorium;
        private System.Windows.Forms.Button auditoriums;
        private System.Windows.Forms.Button allChanges;
        private System.Windows.Forms.Button CreatePDF;
        private System.Windows.Forms.CheckBox weekFiltered;
        private System.Windows.Forms.ComboBox WeekFilter;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox WordFacultyFilter;
        private System.Windows.Forms.CheckBox WordOneFaculty;
        private System.Windows.Forms.Button WordExportButton;
        private System.Windows.Forms.Button WordCustom;
        private System.Windows.Forms.ComboBox WordExportWeekFilter;
        private System.Windows.Forms.CheckBox wordExportWeekFiltered;
        private System.Windows.Forms.Button BIGREDBUTTON;
        private System.Windows.Forms.Button WordSchool;
        private System.Windows.Forms.Button WordSchool2;
        private System.Windows.Forms.Button happyBirthday;
        private System.Windows.Forms.TextBox uploadPrefix;
        private System.Windows.Forms.Button WordWholeScheduleOneGroupOnePage;
        private System.Windows.Forms.Button OnePageGroupScheduleWordExport;
        private System.Windows.Forms.CheckBox cb90;
        private System.Windows.Forms.TextBox ToDBName;
        private System.Windows.Forms.Button DownloadRestore;
        private System.Windows.Forms.TextBox FromDBName;
        private System.Windows.Forms.Button BackupUpload;
        private System.Windows.Forms.Button startSchoolWordExport;
        private System.Windows.Forms.ToolStripMenuItem корпусаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem анализToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пожеланияToolStripMenuItem;
    }
}

