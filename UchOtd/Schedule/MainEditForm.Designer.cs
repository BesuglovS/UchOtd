using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule
{
    partial class MainEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.week17 = new System.Windows.Forms.Button();
            this.week16 = new System.Windows.Forms.Button();
            this.FontSmaller = new System.Windows.Forms.Button();
            this.FontBigger = new System.Windows.Forms.Button();
            this.ElevenTwelveWeek = new System.Windows.Forms.Button();
            this.semester = new System.Windows.Forms.ComboBox();
            this.changeSemesterDb = new System.Windows.Forms.Button();
            this.siteToUpload = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.OnlyFutureDatesExportInWord = new System.Windows.Forms.CheckBox();
            this.removeAllProposedLessons = new System.Windows.Forms.Button();
            this.analyseSchool = new System.Windows.Forms.Button();
            this.analyse = new System.Windows.Forms.Button();
            this.showProposedLessons = new System.Windows.Forms.CheckBox();
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
            this.названияДисциплинToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.опцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.факультетыгруппыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.занятостьАудиторийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.заметкиКРасписаниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.анализToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пожеланияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.аудиторииДисциплинToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.нельзяСтавитьПоследнимУрокомToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.порядокПостановкиДисциплинВРасписаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.периодыГруппToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.корпусИПреимущественнаяАудиторияГруппыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныСГарантированнойНаружнейАудиториейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сменыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.teachersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗачётовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийПоМесяцамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортВWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.датыПоФизическойКультуреToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.последовательностьТиповЗанятийЛППоФакультетамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийМатематиковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеСессииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныРасписанияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийФилологовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеСессииToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныРасписанияToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийЭкономистовмагистратураToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлаToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеСессииToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныРасписанияToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийЮристовмагистратураToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлаToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеСессииToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныРасписанияToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьПоследовательностиТиповЗанятияИзФайлаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.искусствоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлаToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеСессииToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныРасписаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.горюшкинToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлаToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеСессииToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныРасписанияToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.датыЗанятийToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеСессииToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныРасписанияToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.аудиторииДисциплинToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.перевестиАудиторииВФорматООПToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bigRedButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьРасписаниеТекущейГруппыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.процентЗанятийПоДатамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортДатЗачётовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проверитьПравильностьПоследовательностейВидовЗанятийЛПToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортСпискаПреподавателейНа308ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.спискиПреподавателейПоКорпусамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортАудиторийДисциплинToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортБазыДанныхВТекстовыйФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backup10AAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restore10AAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMathDiscs100AAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поправитьАудиторииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поправитьАудиторииФакультетаИскусствToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поправитьАудиторииСессииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.убратьГруппыИностранныхЯзыковИзНазванийДисциплинToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.графикПроцессаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проверитьКоллизииПреподавателейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьРасписаниеНаДеньНеделюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеПереходовМеждуКорпусамиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.неточности811ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.освободитьМестоВРасписанииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreA0718OriginalDbsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.быстроДобавитьЗанятияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleCalendarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GoogleCalendarExport = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьКаледнарьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.ScheduleView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.viewPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.week17);
            this.controlsPanel.Controls.Add(this.week16);
            this.controlsPanel.Controls.Add(this.FontSmaller);
            this.controlsPanel.Controls.Add(this.FontBigger);
            this.controlsPanel.Controls.Add(this.ElevenTwelveWeek);
            this.controlsPanel.Controls.Add(this.semester);
            this.controlsPanel.Controls.Add(this.changeSemesterDb);
            this.controlsPanel.Controls.Add(this.siteToUpload);
            this.controlsPanel.Controls.Add(this.button3);
            this.controlsPanel.Controls.Add(this.OnlyFutureDatesExportInWord);
            this.controlsPanel.Controls.Add(this.removeAllProposedLessons);
            this.controlsPanel.Controls.Add(this.analyseSchool);
            this.controlsPanel.Controls.Add(this.analyse);
            this.controlsPanel.Controls.Add(this.showProposedLessons);
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
            this.controlsPanel.Size = new System.Drawing.Size(971, 196);
            this.controlsPanel.TabIndex = 0;
            // 
            // week17
            // 
            this.week17.Location = new System.Drawing.Point(238, 165);
            this.week17.Name = "week17";
            this.week17.Size = new System.Drawing.Size(29, 21);
            this.week17.TabIndex = 76;
            this.week17.Text = "11";
            this.week17.UseVisualStyleBackColor = true;
            this.week17.Click += new System.EventHandler(this.week17_Click);
            // 
            // week16
            // 
            this.week16.Location = new System.Drawing.Point(203, 165);
            this.week16.Name = "week16";
            this.week16.Size = new System.Drawing.Size(29, 21);
            this.week16.TabIndex = 75;
            this.week16.Text = "10";
            this.week16.UseVisualStyleBackColor = true;
            this.week16.Click += new System.EventHandler(this.week16_Click);
            // 
            // FontSmaller
            // 
            this.FontSmaller.Location = new System.Drawing.Point(280, 165);
            this.FontSmaller.Name = "FontSmaller";
            this.FontSmaller.Size = new System.Drawing.Size(58, 21);
            this.FontSmaller.TabIndex = 74;
            this.FontSmaller.Text = "Шрифт -";
            this.FontSmaller.UseVisualStyleBackColor = true;
            this.FontSmaller.Click += new System.EventHandler(this.FontSmaller_Click);
            // 
            // FontBigger
            // 
            this.FontBigger.Location = new System.Drawing.Point(344, 165);
            this.FontBigger.Name = "FontBigger";
            this.FontBigger.Size = new System.Drawing.Size(63, 21);
            this.FontBigger.TabIndex = 73;
            this.FontBigger.Text = "Шрифт +";
            this.FontBigger.UseVisualStyleBackColor = true;
            this.FontBigger.Click += new System.EventHandler(this.FontBigger_Click);
            // 
            // ElevenTwelveWeek
            // 
            this.ElevenTwelveWeek.Location = new System.Drawing.Point(148, 165);
            this.ElevenTwelveWeek.Name = "ElevenTwelveWeek";
            this.ElevenTwelveWeek.Size = new System.Drawing.Size(49, 21);
            this.ElevenTwelveWeek.TabIndex = 72;
            this.ElevenTwelveWeek.Text = "10-11";
            this.ElevenTwelveWeek.UseVisualStyleBackColor = true;
            this.ElevenTwelveWeek.Click += new System.EventHandler(this.ElevenTwelveWeek_Click);
            // 
            // semester
            // 
            this.semester.FormattingEnabled = true;
            this.semester.Location = new System.Drawing.Point(13, 165);
            this.semester.Name = "semester";
            this.semester.Size = new System.Drawing.Size(75, 21);
            this.semester.TabIndex = 71;
            // 
            // changeSemesterDb
            // 
            this.changeSemesterDb.Location = new System.Drawing.Point(94, 165);
            this.changeSemesterDb.Name = "changeSemesterDb";
            this.changeSemesterDb.Size = new System.Drawing.Size(48, 21);
            this.changeSemesterDb.TabIndex = 70;
            this.changeSemesterDb.Text = "GO";
            this.changeSemesterDb.UseVisualStyleBackColor = true;
            this.changeSemesterDb.Click += new System.EventHandler(this.changeSemesterDb_Click);
            // 
            // siteToUpload
            // 
            this.siteToUpload.FormattingEnabled = true;
            this.siteToUpload.Location = new System.Drawing.Point(306, 51);
            this.siteToUpload.Name = "siteToUpload";
            this.siteToUpload.Size = new System.Drawing.Size(107, 21);
            this.siteToUpload.TabIndex = 69;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(871, 114);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 23);
            this.button3.TabIndex = 68;
            this.button3.Text = "txtBackup";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // OnlyFutureDatesExportInWord
            // 
            this.OnlyFutureDatesExportInWord.AutoSize = true;
            this.OnlyFutureDatesExportInWord.Location = new System.Drawing.Point(668, 82);
            this.OnlyFutureDatesExportInWord.Name = "OnlyFutureDatesExportInWord";
            this.OnlyFutureDatesExportInWord.Size = new System.Drawing.Size(155, 17);
            this.OnlyFutureDatesExportInWord.TabIndex = 67;
            this.OnlyFutureDatesExportInWord.Text = "только последующие дни";
            this.OnlyFutureDatesExportInWord.UseVisualStyleBackColor = true;
            // 
            // removeAllProposedLessons
            // 
            this.removeAllProposedLessons.Location = new System.Drawing.Point(460, 80);
            this.removeAllProposedLessons.Name = "removeAllProposedLessons";
            this.removeAllProposedLessons.Size = new System.Drawing.Size(197, 23);
            this.removeAllProposedLessons.TabIndex = 66;
            this.removeAllProposedLessons.Text = "Удалить все преполагаемые уроки";
            this.removeAllProposedLessons.UseVisualStyleBackColor = true;
            this.removeAllProposedLessons.Click += new System.EventHandler(this.removeAllProposedLessons_Click);
            // 
            // analyseSchool
            // 
            this.analyseSchool.Location = new System.Drawing.Point(324, 80);
            this.analyseSchool.Name = "analyseSchool";
            this.analyseSchool.Size = new System.Drawing.Size(129, 23);
            this.analyseSchool.TabIndex = 65;
            this.analyseSchool.Text = "Сделать ВСЁ (ШКОЛА)";
            this.analyseSchool.UseVisualStyleBackColor = true;
            this.analyseSchool.Click += new System.EventHandler(this.analyseSchool_Click);
            // 
            // analyse
            // 
            this.analyse.Location = new System.Drawing.Point(225, 80);
            this.analyse.Name = "analyse";
            this.analyse.Size = new System.Drawing.Size(93, 23);
            this.analyse.TabIndex = 64;
            this.analyse.Text = "Сделать ВСЁ";
            this.analyse.UseVisualStyleBackColor = true;
            this.analyse.Click += new System.EventHandler(this.analyse_Click);
            // 
            // showProposedLessons
            // 
            this.showProposedLessons.AutoSize = true;
            this.showProposedLessons.Checked = true;
            this.showProposedLessons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showProposedLessons.Location = new System.Drawing.Point(12, 86);
            this.showProposedLessons.Name = "showProposedLessons";
            this.showProposedLessons.Size = new System.Drawing.Size(210, 17);
            this.showProposedLessons.TabIndex = 63;
            this.showProposedLessons.Text = "Показывать неутверждённые уроки";
            this.showProposedLessons.UseVisualStyleBackColor = true;
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
            this.ToDBName.Location = new System.Drawing.Point(871, 143);
            this.ToDBName.Name = "ToDBName";
            this.ToDBName.Size = new System.Drawing.Size(88, 20);
            this.ToDBName.TabIndex = 61;
            this.ToDBName.Text = "Schedule18191";
            // 
            // DownloadRestore
            // 
            this.DownloadRestore.Location = new System.Drawing.Point(790, 128);
            this.DownloadRestore.Name = "DownloadRestore";
            this.DownloadRestore.Size = new System.Drawing.Size(75, 35);
            this.DownloadRestore.TabIndex = 60;
            this.DownloadRestore.Text = "Download + restore";
            this.DownloadRestore.UseVisualStyleBackColor = true;
            this.DownloadRestore.Click += new System.EventHandler(this.DownloadAndRestore_Click);
            // 
            // FromDBName
            // 
            this.FromDBName.Location = new System.Drawing.Point(603, 143);
            this.FromDBName.Name = "FromDBName";
            this.FromDBName.Size = new System.Drawing.Size(100, 20);
            this.FromDBName.TabIndex = 59;
            this.FromDBName.Text = "Schedule18191";
            // 
            // BackupUpload
            // 
            this.BackupUpload.Location = new System.Drawing.Point(709, 128);
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
            this.cb90.Location = new System.Drawing.Point(526, 134);
            this.cb90.Name = "cb90";
            this.cb90.Size = new System.Drawing.Size(71, 17);
            this.cb90.TabIndex = 56;
            this.cb90.Text = "90 минут";
            this.cb90.UseVisualStyleBackColor = true;
            // 
            // OnePageGroupScheduleWordExport
            // 
            this.OnePageGroupScheduleWordExport.Location = new System.Drawing.Point(94, 136);
            this.OnePageGroupScheduleWordExport.Name = "OnePageGroupScheduleWordExport";
            this.OnePageGroupScheduleWordExport.Size = new System.Drawing.Size(165, 23);
            this.OnePageGroupScheduleWordExport.TabIndex = 55;
            this.OnePageGroupScheduleWordExport.Text = "Экспорт в Word - одна группа";
            this.OnePageGroupScheduleWordExport.UseVisualStyleBackColor = true;
            this.OnePageGroupScheduleWordExport.Click += new System.EventHandler(this.OnePageGroupScheduleWordExport_Click);
            // 
            // WordWholeScheduleOneGroupOnePage
            // 
            this.WordWholeScheduleOneGroupOnePage.Location = new System.Drawing.Point(265, 136);
            this.WordWholeScheduleOneGroupOnePage.Name = "WordWholeScheduleOneGroupOnePage";
            this.WordWholeScheduleOneGroupOnePage.Size = new System.Drawing.Size(255, 23);
            this.WordWholeScheduleOneGroupOnePage.TabIndex = 54;
            this.WordWholeScheduleOneGroupOnePage.Text = "Всё расписание в Word 1 группа на 1 стр.";
            this.WordWholeScheduleOneGroupOnePage.UseVisualStyleBackColor = true;
            this.WordWholeScheduleOneGroupOnePage.Click += new System.EventHandler(this.WordWholeScheduleOneGroupOnePage_Click);
            // 
            // uploadPrefix
            // 
            this.uploadPrefix.Location = new System.Drawing.Point(306, 25);
            this.uploadPrefix.Name = "uploadPrefix";
            this.uploadPrefix.Size = new System.Drawing.Size(81, 20);
            this.uploadPrefix.TabIndex = 53;
            // 
            // happyBirthday
            // 
            this.happyBirthday.Location = new System.Drawing.Point(13, 136);
            this.happyBirthday.Name = "happyBirthday";
            this.happyBirthday.Size = new System.Drawing.Size(75, 23);
            this.happyBirthday.TabIndex = 52;
            this.happyBirthday.Text = "Happy";
            this.happyBirthday.UseVisualStyleBackColor = true;
            this.happyBirthday.Click += new System.EventHandler(this.happyBirthday_Click);
            // 
            // WordSchool2
            // 
            this.WordSchool2.Location = new System.Drawing.Point(709, 99);
            this.WordSchool2.Name = "WordSchool2";
            this.WordSchool2.Size = new System.Drawing.Size(124, 23);
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
            this.WordSchool.Text = "Word (8-11)";
            this.WordSchool.UseVisualStyleBackColor = true;
            this.WordSchool.Click += new System.EventHandler(this.WordSchool_Click_1);
            // 
            // BIGREDBUTTON
            // 
            this.BIGREDBUTTON.Location = new System.Drawing.Point(839, 85);
            this.BIGREDBUTTON.Name = "BIGREDBUTTON";
            this.BIGREDBUTTON.Size = new System.Drawing.Size(120, 24);
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
            this.WordFacultyFilter.Location = new System.Drawing.Point(571, 107);
            this.WordFacultyFilter.Name = "WordFacultyFilter";
            this.WordFacultyFilter.Size = new System.Drawing.Size(54, 21);
            this.WordFacultyFilter.TabIndex = 40;
            // 
            // WordOneFaculty
            // 
            this.WordOneFaculty.AutoSize = true;
            this.WordOneFaculty.Location = new System.Drawing.Point(459, 111);
            this.WordOneFaculty.Name = "WordOneFaculty";
            this.WordOneFaculty.Size = new System.Drawing.Size(106, 17);
            this.WordOneFaculty.TabIndex = 39;
            this.WordOneFaculty.Text = "один факультет";
            this.WordOneFaculty.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(631, 105);
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
            this.CreatePDF.Location = new System.Drawing.Point(552, 54);
            this.CreatePDF.Name = "CreatePDF";
            this.CreatePDF.Size = new System.Drawing.Size(105, 23);
            this.CreatePDF.TabIndex = 29;
            this.CreatePDF.Text = "PDF";
            this.CreatePDF.UseVisualStyleBackColor = true;
            this.CreatePDF.Click += new System.EventHandler(this.CreatePDF_Click);
            // 
            // allChanges
            // 
            this.allChanges.Location = new System.Drawing.Point(265, 107);
            this.allChanges.Name = "allChanges";
            this.allChanges.Size = new System.Drawing.Size(148, 23);
            this.allChanges.TabIndex = 20;
            this.allChanges.Text = "Все изменения";
            this.allChanges.UseVisualStyleBackColor = true;
            this.allChanges.Click += new System.EventHandler(this.allChanges_Click);
            // 
            // auditoriums
            // 
            this.auditoriums.Location = new System.Drawing.Point(148, 107);
            this.auditoriums.Name = "auditoriums";
            this.auditoriums.Size = new System.Drawing.Size(111, 23);
            this.auditoriums.TabIndex = 19;
            this.auditoriums.Text = "Все аудитории";
            this.auditoriums.UseVisualStyleBackColor = true;
            this.auditoriums.Click += new System.EventHandler(this.auditoriums_Click);
            // 
            // oneAuditorium
            // 
            this.oneAuditorium.Location = new System.Drawing.Point(13, 107);
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
            this.ManyGroups.Size = new System.Drawing.Size(79, 23);
            this.ManyGroups.TabIndex = 16;
            this.ManyGroups.Text = "Много групп";
            this.ManyGroups.UseVisualStyleBackColor = true;
            this.ManyGroups.Click += new System.EventHandler(this.ManyGroups_Click);
            // 
            // ActiveLessonsCount
            // 
            this.ActiveLessonsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ActiveLessonsCount.Location = new System.Drawing.Point(419, 107);
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
            this.FacultyList.Location = new System.Drawing.Point(507, 27);
            this.FacultyList.Name = "FacultyList";
            this.FacultyList.Size = new System.Drawing.Size(50, 21);
            this.FacultyList.TabIndex = 10;
            // 
            // auditoriumKaput
            // 
            this.auditoriumKaput.Location = new System.Drawing.Point(419, 54);
            this.auditoriumKaput.Name = "auditoriumKaput";
            this.auditoriumKaput.Size = new System.Drawing.Size(127, 23);
            this.auditoriumKaput.TabIndex = 9;
            this.auditoriumKaput.Text = "Коллизии аудиторий";
            this.auditoriumKaput.UseVisualStyleBackColor = true;
            this.auditoriumKaput.Click += new System.EventHandler(this.AuditoriumKaputClick);
            // 
            // LoadToSite
            // 
            this.LoadToSite.Location = new System.Drawing.Point(393, 25);
            this.LoadToSite.Name = "LoadToSite";
            this.LoadToSite.Size = new System.Drawing.Size(108, 23);
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
            this.анализToolStripMenuItem,
            this.экспортВWordToolStripMenuItem,
            this.bigRedButtonToolStripMenuItem,
            this.googleCalendarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(971, 24);
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
            this.названияДисциплинToolStripMenuItem,
            this.опцииToolStripMenuItem,
            this.факультетыгруппыToolStripMenuItem,
            this.занятостьАудиторийToolStripMenuItem,
            this.toolStripMenuItem1,
            this.заметкиКРасписаниюToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // аудиторииToolStripMenuItem
            // 
            this.аудиторииToolStripMenuItem.Name = "аудиторииToolStripMenuItem";
            this.аудиторииToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.аудиторииToolStripMenuItem.Text = "Аудитории";
            this.аудиторииToolStripMenuItem.Click += new System.EventHandler(this.АудиторииToolStripMenuItemClick);
            // 
            // корпусаToolStripMenuItem
            // 
            this.корпусаToolStripMenuItem.Name = "корпусаToolStripMenuItem";
            this.корпусаToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.корпусаToolStripMenuItem.Text = "Корпуса";
            this.корпусаToolStripMenuItem.Click += new System.EventHandler(this.корпусаToolStripMenuItem_Click);
            // 
            // дниСеместраToolStripMenuItem
            // 
            this.дниСеместраToolStripMenuItem.Name = "дниСеместраToolStripMenuItem";
            this.дниСеместраToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.дниСеместраToolStripMenuItem.Text = "Дни семестра";
            this.дниСеместраToolStripMenuItem.Click += new System.EventHandler(this.ДниСеместраToolStripMenuItemClick);
            // 
            // звонкиToolStripMenuItem
            // 
            this.звонкиToolStripMenuItem.Name = "звонкиToolStripMenuItem";
            this.звонкиToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.звонкиToolStripMenuItem.Text = "Звонки";
            this.звонкиToolStripMenuItem.Click += new System.EventHandler(this.ЗвонкиToolStripMenuItemClick);
            // 
            // студентыToolStripMenuItem
            // 
            this.студентыToolStripMenuItem.Name = "студентыToolStripMenuItem";
            this.студентыToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.студентыToolStripMenuItem.Text = "Студенты";
            this.студентыToolStripMenuItem.Click += new System.EventHandler(this.СтудентыToolStripMenuItemClick);
            // 
            // группыToolStripMenuItem
            // 
            this.группыToolStripMenuItem.Name = "группыToolStripMenuItem";
            this.группыToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.группыToolStripMenuItem.Text = "Группы";
            this.группыToolStripMenuItem.Click += new System.EventHandler(this.ГруппыToolStripMenuItemClick);
            // 
            // преподавателиToolStripMenuItem
            // 
            this.преподавателиToolStripMenuItem.Name = "преподавателиToolStripMenuItem";
            this.преподавателиToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.преподавателиToolStripMenuItem.Text = "Преподаватели";
            this.преподавателиToolStripMenuItem.Click += new System.EventHandler(this.ПреподавателиToolStripMenuItemClick);
            // 
            // дисциплиныToolStripMenuItem
            // 
            this.дисциплиныToolStripMenuItem.Name = "дисциплиныToolStripMenuItem";
            this.дисциплиныToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.дисциплиныToolStripMenuItem.Text = "Дисциплины";
            this.дисциплиныToolStripMenuItem.Click += new System.EventHandler(this.ДисциплиныToolStripMenuItemClick);
            // 
            // названияДисциплинToolStripMenuItem
            // 
            this.названияДисциплинToolStripMenuItem.Name = "названияДисциплинToolStripMenuItem";
            this.названияДисциплинToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.названияДисциплинToolStripMenuItem.Text = "Названия дисциплин";
            this.названияДисциплинToolStripMenuItem.Click += new System.EventHandler(this.названияДисциплинToolStripMenuItem_Click);
            // 
            // опцииToolStripMenuItem
            // 
            this.опцииToolStripMenuItem.Name = "опцииToolStripMenuItem";
            this.опцииToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.опцииToolStripMenuItem.Text = "Опции";
            this.опцииToolStripMenuItem.Click += new System.EventHandler(this.ОпцииToolStripMenuItemClick);
            // 
            // факультетыгруппыToolStripMenuItem
            // 
            this.факультетыгруппыToolStripMenuItem.Name = "факультетыгруппыToolStripMenuItem";
            this.факультетыгруппыToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.факультетыгруппыToolStripMenuItem.Text = "Факультеты+группы";
            this.факультетыгруппыToolStripMenuItem.Click += new System.EventHandler(this.ФакультетыгруппыToolStripMenuItemClick);
            // 
            // занятостьАудиторийToolStripMenuItem
            // 
            this.занятостьАудиторийToolStripMenuItem.Name = "занятостьАудиторийToolStripMenuItem";
            this.занятостьАудиторийToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.занятостьАудиторийToolStripMenuItem.Text = "Занятость аудиторий";
            this.занятостьАудиторийToolStripMenuItem.Click += new System.EventHandler(this.занятостьАудиторийToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(198, 6);
            // 
            // заметкиКРасписаниюToolStripMenuItem
            // 
            this.заметкиКРасписаниюToolStripMenuItem.Name = "заметкиКРасписаниюToolStripMenuItem";
            this.заметкиКРасписаниюToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.заметкиКРасписаниюToolStripMenuItem.Text = "Заметки к расписанию";
            this.заметкиКРасписаниюToolStripMenuItem.Click += new System.EventHandler(this.заметкиКРасписаниюToolStripMenuItem_Click);
            // 
            // анализToolStripMenuItem
            // 
            this.анализToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.пожеланияToolStripMenuItem,
            this.аудиторииДисциплинToolStripMenuItem,
            this.нельзяСтавитьПоследнимУрокомToolStripMenuItem,
            this.парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem,
            this.дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem,
            this.порядокПостановкиДисциплинВРасписаниеToolStripMenuItem,
            this.периодыГруппToolStripMenuItem,
            this.корпусИПреимущественнаяАудиторияГруппыToolStripMenuItem,
            this.дисциплиныСГарантированнойНаружнейАудиториейToolStripMenuItem,
            this.сменыToolStripMenuItem,
            this.toolStripMenuItem2,
            this.teachersToolStripMenuItem,
            this.датыЗачётовToolStripMenuItem,
            this.датыЗанятийПоМесяцамToolStripMenuItem});
            this.анализToolStripMenuItem.Name = "анализToolStripMenuItem";
            this.анализToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.анализToolStripMenuItem.Text = "Анализ";
            // 
            // пожеланияToolStripMenuItem
            // 
            this.пожеланияToolStripMenuItem.Name = "пожеланияToolStripMenuItem";
            this.пожеланияToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.пожеланияToolStripMenuItem.Text = "Пожелания";
            this.пожеланияToolStripMenuItem.Click += new System.EventHandler(this.пожеланияToolStripMenuItem_Click);
            // 
            // аудиторииДисциплинToolStripMenuItem
            // 
            this.аудиторииДисциплинToolStripMenuItem.Name = "аудиторииДисциплинToolStripMenuItem";
            this.аудиторииДисциплинToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.аудиторииДисциплинToolStripMenuItem.Text = "Аудитории дисциплин";
            this.аудиторииДисциплинToolStripMenuItem.Click += new System.EventHandler(this.аудиторииДисциплинToolStripMenuItem_Click);
            // 
            // нельзяСтавитьПоследнимУрокомToolStripMenuItem
            // 
            this.нельзяСтавитьПоследнимУрокомToolStripMenuItem.Name = "нельзяСтавитьПоследнимУрокомToolStripMenuItem";
            this.нельзяСтавитьПоследнимУрокомToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.нельзяСтавитьПоследнимУрокомToolStripMenuItem.Text = "Нельзя ставить последним уроком";
            this.нельзяСтавитьПоследнимУрокомToolStripMenuItem.Click += new System.EventHandler(this.нельзяСтавитьПоследнимУрокомToolStripMenuItem_Click);
            // 
            // парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem
            // 
            this.парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem.Name = "парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem";
            this.парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem.Text = "Пары дисциплин нельзя ставить в один день";
            this.парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem.Click += new System.EventHandler(this.парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem_Click);
            // 
            // дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem
            // 
            this.дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem.Name = "дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem";
            this.дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem.Text = "Дисциплины лучше ставить по 2 урока";
            this.дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem.Click += new System.EventHandler(this.дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem_Click);
            // 
            // порядокПостановкиДисциплинВРасписаниеToolStripMenuItem
            // 
            this.порядокПостановкиДисциплинВРасписаниеToolStripMenuItem.Name = "порядокПостановкиДисциплинВРасписаниеToolStripMenuItem";
            this.порядокПостановкиДисциплинВРасписаниеToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.порядокПостановкиДисциплинВРасписаниеToolStripMenuItem.Text = "Порядок постановки дисциплин в расписание";
            this.порядокПостановкиДисциплинВРасписаниеToolStripMenuItem.Click += new System.EventHandler(this.порядокПостановкиДисциплинВРасписаниеToolStripMenuItem_Click);
            // 
            // периодыГруппToolStripMenuItem
            // 
            this.периодыГруппToolStripMenuItem.Name = "периодыГруппToolStripMenuItem";
            this.периодыГруппToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.периодыГруппToolStripMenuItem.Text = "Периоды групп";
            this.периодыГруппToolStripMenuItem.Click += new System.EventHandler(this.периодыГруппToolStripMenuItem_Click);
            // 
            // корпусИПреимущественнаяАудиторияГруппыToolStripMenuItem
            // 
            this.корпусИПреимущественнаяАудиторияГруппыToolStripMenuItem.Name = "корпусИПреимущественнаяАудиторияГруппыToolStripMenuItem";
            this.корпусИПреимущественнаяАудиторияГруппыToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.корпусИПреимущественнаяАудиторияГруппыToolStripMenuItem.Text = "Параметры групп";
            this.корпусИПреимущественнаяАудиторияГруппыToolStripMenuItem.Click += new System.EventHandler(this.корпусИПреимущественнаяАудиторияГруппыToolStripMenuItem_Click);
            // 
            // дисциплиныСГарантированнойНаружнейАудиториейToolStripMenuItem
            // 
            this.дисциплиныСГарантированнойНаружнейАудиториейToolStripMenuItem.Name = "дисциплиныСГарантированнойНаружнейАудиториейToolStripMenuItem";
            this.дисциплиныСГарантированнойНаружнейАудиториейToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.дисциплиныСГарантированнойНаружнейАудиториейToolStripMenuItem.Text = "Дисциплины с гарантированной наружней аудиторией";
            this.дисциплиныСГарантированнойНаружнейАудиториейToolStripMenuItem.Click += new System.EventHandler(this.дисциплиныСГарантированнойНаружнейАудиториейToolStripMenuItem_Click);
            // 
            // сменыToolStripMenuItem
            // 
            this.сменыToolStripMenuItem.Name = "сменыToolStripMenuItem";
            this.сменыToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.сменыToolStripMenuItem.Text = "Смены";
            this.сменыToolStripMenuItem.Click += new System.EventHandler(this.сменыToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(413, 6);
            // 
            // teachersToolStripMenuItem
            // 
            this.teachersToolStripMenuItem.Name = "teachersToolStripMenuItem";
            this.teachersToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.teachersToolStripMenuItem.Text = "Преподаватели, которым необходимо дополнить расписание";
            this.teachersToolStripMenuItem.Click += new System.EventHandler(this.teachersToolStripMenuItem_Click);
            // 
            // датыЗачётовToolStripMenuItem
            // 
            this.датыЗачётовToolStripMenuItem.Name = "датыЗачётовToolStripMenuItem";
            this.датыЗачётовToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.датыЗачётовToolStripMenuItem.Text = "Даты зачётов";
            this.датыЗачётовToolStripMenuItem.Click += new System.EventHandler(this.датыЗачётовToolStripMenuItem_Click);
            // 
            // датыЗанятийПоМесяцамToolStripMenuItem
            // 
            this.датыЗанятийПоМесяцамToolStripMenuItem.Name = "датыЗанятийПоМесяцамToolStripMenuItem";
            this.датыЗанятийПоМесяцамToolStripMenuItem.Size = new System.Drawing.Size(416, 22);
            this.датыЗанятийПоМесяцамToolStripMenuItem.Text = "Даты занятий по месяцам";
            this.датыЗанятийПоМесяцамToolStripMenuItem.Click += new System.EventHandler(this.датыЗанятийПоМесяцамToolStripMenuItem_Click);
            // 
            // экспортВWordToolStripMenuItem
            // 
            this.экспортВWordToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.датыПоФизическойКультуреToolStripMenuItem,
            this.последовательностьТиповЗанятийЛППоФакультетамToolStripMenuItem,
            this.датыЗанятийМатематиковToolStripMenuItem,
            this.датыЗанятийФилологовToolStripMenuItem,
            this.датыЗанятийЭкономистовмагистратураToolStripMenuItem,
            this.датыЗанятийЮристовмагистратураToolStripMenuItem,
            this.вставитьПоследовательностиТиповЗанятияИзФайлаToolStripMenuItem,
            this.искусствоToolStripMenuItem,
            this.горюшкинToolStripMenuItem,
            this.toolStripMenuItem5});
            this.экспортВWordToolStripMenuItem.Name = "экспортВWordToolStripMenuItem";
            this.экспортВWordToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.экспортВWordToolStripMenuItem.Text = "Экспорт в Word ";
            // 
            // датыПоФизическойКультуреToolStripMenuItem
            // 
            this.датыПоФизическойКультуреToolStripMenuItem.Name = "датыПоФизическойКультуреToolStripMenuItem";
            this.датыПоФизическойКультуреToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
            this.датыПоФизическойКультуреToolStripMenuItem.Text = "Даты по физической культуре + списки групп";
            this.датыПоФизическойКультуреToolStripMenuItem.Click += new System.EventHandler(this.датыПоФизическойКультуреToolStripMenuItem_Click);
            // 
            // последовательностьТиповЗанятийЛППоФакультетамToolStripMenuItem
            // 
            this.последовательностьТиповЗанятийЛППоФакультетамToolStripMenuItem.Name = "последовательностьТиповЗанятийЛППоФакультетамToolStripMenuItem";
            this.последовательностьТиповЗанятийЛППоФакультетамToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
            this.последовательностьТиповЗанятийЛППоФакультетамToolStripMenuItem.Text = "Последовательность типов занятий (Л/П) по факультетам";
            this.последовательностьТиповЗанятийЛППоФакультетамToolStripMenuItem.Click += new System.EventHandler(this.последовательностьТиповЗанятийЛППоФакультетамToolStripMenuItem_Click);
            // 
            // датыЗанятийМатематиковToolStripMenuItem
            // 
            this.датыЗанятийМатематиковToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлаToolStripMenuItem,
            this.датыЗанятийToolStripMenuItem3,
            this.расписаниеToolStripMenuItem3,
            this.расписаниеСессииToolStripMenuItem,
            this.дисциплиныРасписанияToolStripMenuItem});
            this.датыЗанятийМатематиковToolStripMenuItem.Name = "датыЗанятийМатематиковToolStripMenuItem";
            this.датыЗанятийМатематиковToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
            this.датыЗанятийМатематиковToolStripMenuItem.Text = "Математики";
            // 
            // файлаToolStripMenuItem
            // 
            this.файлаToolStripMenuItem.Name = "файлаToolStripMenuItem";
            this.файлаToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.файлаToolStripMenuItem.Text = "4 файла";
            this.файлаToolStripMenuItem.Click += new System.EventHandler(this.Math4Files);
            // 
            // датыЗанятийToolStripMenuItem3
            // 
            this.датыЗанятийToolStripMenuItem3.Name = "датыЗанятийToolStripMenuItem3";
            this.датыЗанятийToolStripMenuItem3.Size = new System.Drawing.Size(214, 22);
            this.датыЗанятийToolStripMenuItem3.Text = "Даты занятий";
            this.датыЗанятийToolStripMenuItem3.Click += new System.EventHandler(this.MathJournalDates);
            // 
            // расписаниеToolStripMenuItem3
            // 
            this.расписаниеToolStripMenuItem3.Name = "расписаниеToolStripMenuItem3";
            this.расписаниеToolStripMenuItem3.Size = new System.Drawing.Size(214, 22);
            this.расписаниеToolStripMenuItem3.Text = "Расписание";
            this.расписаниеToolStripMenuItem3.Click += new System.EventHandler(this.MathSchedule);
            // 
            // расписаниеСессииToolStripMenuItem
            // 
            this.расписаниеСессииToolStripMenuItem.Name = "расписаниеСессииToolStripMenuItem";
            this.расписаниеСессииToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.расписаниеСессииToolStripMenuItem.Text = "Расписание сессии";
            this.расписаниеСессииToolStripMenuItem.Click += new System.EventHandler(this.MathSessionSchedule);
            // 
            // дисциплиныРасписанияToolStripMenuItem
            // 
            this.дисциплиныРасписанияToolStripMenuItem.Name = "дисциплиныРасписанияToolStripMenuItem";
            this.дисциплиныРасписанияToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.дисциплиныРасписанияToolStripMenuItem.Text = "Дисциплины расписания";
            this.дисциплиныРасписанияToolStripMenuItem.Click += new System.EventHandler(this.MathDisciplines);
            // 
            // датыЗанятийФилологовToolStripMenuItem
            // 
            this.датыЗанятийФилологовToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлаToolStripMenuItem1,
            this.датыЗанятийToolStripMenuItem,
            this.расписаниеToolStripMenuItem,
            this.расписаниеСессииToolStripMenuItem1,
            this.дисциплиныРасписанияToolStripMenuItem1});
            this.датыЗанятийФилологовToolStripMenuItem.Name = "датыЗанятийФилологовToolStripMenuItem";
            this.датыЗанятийФилологовToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
            this.датыЗанятийФилологовToolStripMenuItem.Text = "Филологи";
            // 
            // файлаToolStripMenuItem1
            // 
            this.файлаToolStripMenuItem1.Name = "файлаToolStripMenuItem1";
            this.файлаToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.файлаToolStripMenuItem1.Text = "4 файла";
            this.файлаToolStripMenuItem1.Click += new System.EventHandler(this.Phil4Files);
            // 
            // датыЗанятийToolStripMenuItem
            // 
            this.датыЗанятийToolStripMenuItem.Name = "датыЗанятийToolStripMenuItem";
            this.датыЗанятийToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.датыЗанятийToolStripMenuItem.Text = "Даты занятий";
            this.датыЗанятийToolStripMenuItem.Click += new System.EventHandler(this.PhilJournalDates);
            // 
            // расписаниеToolStripMenuItem
            // 
            this.расписаниеToolStripMenuItem.Name = "расписаниеToolStripMenuItem";
            this.расписаниеToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.расписаниеToolStripMenuItem.Text = "Расписание";
            this.расписаниеToolStripMenuItem.Click += new System.EventHandler(this.PhilSchedule);
            // 
            // расписаниеСессииToolStripMenuItem1
            // 
            this.расписаниеСессииToolStripMenuItem1.Name = "расписаниеСессииToolStripMenuItem1";
            this.расписаниеСессииToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.расписаниеСессииToolStripMenuItem1.Text = "Расписание сессии";
            this.расписаниеСессииToolStripMenuItem1.Click += new System.EventHandler(this.PhilSessionSchedule);
            // 
            // дисциплиныРасписанияToolStripMenuItem1
            // 
            this.дисциплиныРасписанияToolStripMenuItem1.Name = "дисциплиныРасписанияToolStripMenuItem1";
            this.дисциплиныРасписанияToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.дисциплиныРасписанияToolStripMenuItem1.Text = "Дисциплины расписания";
            this.дисциплиныРасписанияToolStripMenuItem1.Click += new System.EventHandler(this.PhilDisciplines);
            // 
            // датыЗанятийЭкономистовмагистратураToolStripMenuItem
            // 
            this.датыЗанятийЭкономистовмагистратураToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлаToolStripMenuItem2,
            this.датыЗанятийToolStripMenuItem1,
            this.расписаниеToolStripMenuItem1,
            this.расписаниеСессииToolStripMenuItem2,
            this.дисциплиныРасписанияToolStripMenuItem2});
            this.датыЗанятийЭкономистовмагистратураToolStripMenuItem.Name = "датыЗанятийЭкономистовмагистратураToolStripMenuItem";
            this.датыЗанятийЭкономистовмагистратураToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
            this.датыЗанятийЭкономистовмагистратураToolStripMenuItem.Text = "Экономисты (магистратура)";
            // 
            // файлаToolStripMenuItem2
            // 
            this.файлаToolStripMenuItem2.Name = "файлаToolStripMenuItem2";
            this.файлаToolStripMenuItem2.Size = new System.Drawing.Size(214, 22);
            this.файлаToolStripMenuItem2.Text = "4 файла";
            this.файлаToolStripMenuItem2.Click += new System.EventHandler(this.EconM4Files);
            // 
            // датыЗанятийToolStripMenuItem1
            // 
            this.датыЗанятийToolStripMenuItem1.Name = "датыЗанятийToolStripMenuItem1";
            this.датыЗанятийToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.датыЗанятийToolStripMenuItem1.Text = "Даты занятий";
            this.датыЗанятийToolStripMenuItem1.Click += new System.EventHandler(this.EconMJournalDates);
            // 
            // расписаниеToolStripMenuItem1
            // 
            this.расписаниеToolStripMenuItem1.Name = "расписаниеToolStripMenuItem1";
            this.расписаниеToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.расписаниеToolStripMenuItem1.Text = "Расписание";
            this.расписаниеToolStripMenuItem1.Click += new System.EventHandler(this.EconMSchedule);
            // 
            // расписаниеСессииToolStripMenuItem2
            // 
            this.расписаниеСессииToolStripMenuItem2.Name = "расписаниеСессииToolStripMenuItem2";
            this.расписаниеСессииToolStripMenuItem2.Size = new System.Drawing.Size(214, 22);
            this.расписаниеСессииToolStripMenuItem2.Text = "Расписание сессии";
            this.расписаниеСессииToolStripMenuItem2.Click += new System.EventHandler(this.EconMSessionSchedule);
            // 
            // дисциплиныРасписанияToolStripMenuItem2
            // 
            this.дисциплиныРасписанияToolStripMenuItem2.Name = "дисциплиныРасписанияToolStripMenuItem2";
            this.дисциплиныРасписанияToolStripMenuItem2.Size = new System.Drawing.Size(214, 22);
            this.дисциплиныРасписанияToolStripMenuItem2.Text = "Дисциплины расписания";
            this.дисциплиныРасписанияToolStripMenuItem2.Click += new System.EventHandler(this.EconMDisciplines);
            // 
            // датыЗанятийЮристовмагистратураToolStripMenuItem
            // 
            this.датыЗанятийЮристовмагистратураToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлаToolStripMenuItem3,
            this.датыЗанятийToolStripMenuItem2,
            this.расписаниеToolStripMenuItem2,
            this.расписаниеСессииToolStripMenuItem3,
            this.дисциплиныРасписанияToolStripMenuItem3});
            this.датыЗанятийЮристовмагистратураToolStripMenuItem.Name = "датыЗанятийЮристовмагистратураToolStripMenuItem";
            this.датыЗанятийЮристовмагистратураToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
            this.датыЗанятийЮристовмагистратураToolStripMenuItem.Text = "Юристы (магистратура)";
            // 
            // файлаToolStripMenuItem3
            // 
            this.файлаToolStripMenuItem3.Name = "файлаToolStripMenuItem3";
            this.файлаToolStripMenuItem3.Size = new System.Drawing.Size(214, 22);
            this.файлаToolStripMenuItem3.Text = "4 файла";
            this.файлаToolStripMenuItem3.Click += new System.EventHandler(this.LawM4Files);
            // 
            // датыЗанятийToolStripMenuItem2
            // 
            this.датыЗанятийToolStripMenuItem2.Name = "датыЗанятийToolStripMenuItem2";
            this.датыЗанятийToolStripMenuItem2.Size = new System.Drawing.Size(214, 22);
            this.датыЗанятийToolStripMenuItem2.Text = "Даты занятий";
            this.датыЗанятийToolStripMenuItem2.Click += new System.EventHandler(this.LawMJournalDates);
            // 
            // расписаниеToolStripMenuItem2
            // 
            this.расписаниеToolStripMenuItem2.Name = "расписаниеToolStripMenuItem2";
            this.расписаниеToolStripMenuItem2.Size = new System.Drawing.Size(214, 22);
            this.расписаниеToolStripMenuItem2.Text = "Расписание";
            this.расписаниеToolStripMenuItem2.Click += new System.EventHandler(this.LawMSchedule);
            // 
            // расписаниеСессииToolStripMenuItem3
            // 
            this.расписаниеСессииToolStripMenuItem3.Name = "расписаниеСессииToolStripMenuItem3";
            this.расписаниеСессииToolStripMenuItem3.Size = new System.Drawing.Size(214, 22);
            this.расписаниеСессииToolStripMenuItem3.Text = "Расписание сессии";
            this.расписаниеСессииToolStripMenuItem3.Click += new System.EventHandler(this.LawMSessionSchedule);
            // 
            // дисциплиныРасписанияToolStripMenuItem3
            // 
            this.дисциплиныРасписанияToolStripMenuItem3.Name = "дисциплиныРасписанияToolStripMenuItem3";
            this.дисциплиныРасписанияToolStripMenuItem3.Size = new System.Drawing.Size(214, 22);
            this.дисциплиныРасписанияToolStripMenuItem3.Text = "Дисциплины расписания";
            this.дисциплиныРасписанияToolStripMenuItem3.Click += new System.EventHandler(this.LawMDisciplines);
            // 
            // вставитьПоследовательностиТиповЗанятияИзФайлаToolStripMenuItem
            // 
            this.вставитьПоследовательностиТиповЗанятияИзФайлаToolStripMenuItem.Name = "вставитьПоследовательностиТиповЗанятияИзФайлаToolStripMenuItem";
            this.вставитьПоследовательностиТиповЗанятияИзФайлаToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
            this.вставитьПоследовательностиТиповЗанятияИзФайлаToolStripMenuItem.Text = "Вставить последовательности типов занятий из файла";
            this.вставитьПоследовательностиТиповЗанятияИзФайлаToolStripMenuItem.Click += new System.EventHandler(this.вставитьПоследовательностиТиповЗанятияИзФайлаToolStripMenuItem_Click);
            // 
            // искусствоToolStripMenuItem
            // 
            this.искусствоToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлаToolStripMenuItem4,
            this.датыЗанятийToolStripMenuItem4,
            this.расписаниеToolStripMenuItem4,
            this.расписаниеСессииToolStripMenuItem4,
            this.дисциплиныРасписаниеToolStripMenuItem});
            this.искусствоToolStripMenuItem.Name = "искусствоToolStripMenuItem";
            this.искусствоToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
            this.искусствоToolStripMenuItem.Text = "Искусство";
            // 
            // файлаToolStripMenuItem4
            // 
            this.файлаToolStripMenuItem4.Name = "файлаToolStripMenuItem4";
            this.файлаToolStripMenuItem4.Size = new System.Drawing.Size(214, 22);
            this.файлаToolStripMenuItem4.Text = "4 файла";
            this.файлаToolStripMenuItem4.Click += new System.EventHandler(this.Art4Files);
            // 
            // датыЗанятийToolStripMenuItem4
            // 
            this.датыЗанятийToolStripMenuItem4.Name = "датыЗанятийToolStripMenuItem4";
            this.датыЗанятийToolStripMenuItem4.Size = new System.Drawing.Size(214, 22);
            this.датыЗанятийToolStripMenuItem4.Text = "Даты занятий";
            this.датыЗанятийToolStripMenuItem4.Click += new System.EventHandler(this.ArtJournalDates);
            // 
            // расписаниеToolStripMenuItem4
            // 
            this.расписаниеToolStripMenuItem4.Name = "расписаниеToolStripMenuItem4";
            this.расписаниеToolStripMenuItem4.Size = new System.Drawing.Size(214, 22);
            this.расписаниеToolStripMenuItem4.Text = "Расписание";
            this.расписаниеToolStripMenuItem4.Click += new System.EventHandler(this.ArtSchedule);
            // 
            // расписаниеСессииToolStripMenuItem4
            // 
            this.расписаниеСессииToolStripMenuItem4.Name = "расписаниеСессииToolStripMenuItem4";
            this.расписаниеСессииToolStripMenuItem4.Size = new System.Drawing.Size(214, 22);
            this.расписаниеСессииToolStripMenuItem4.Text = "Расписание сессии";
            this.расписаниеСессииToolStripMenuItem4.Click += new System.EventHandler(this.ArtSessionSchedule);
            // 
            // дисциплиныРасписаниеToolStripMenuItem
            // 
            this.дисциплиныРасписаниеToolStripMenuItem.Name = "дисциплиныРасписаниеToolStripMenuItem";
            this.дисциплиныРасписаниеToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.дисциплиныРасписаниеToolStripMenuItem.Text = "Дисциплины расписания";
            this.дисциплиныРасписаниеToolStripMenuItem.Click += new System.EventHandler(this.ArtDisciplines);
            // 
            // горюшкинToolStripMenuItem
            // 
            this.горюшкинToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлаToolStripMenuItem5,
            this.датыЗанятийToolStripMenuItem5,
            this.расписаниеToolStripMenuItem5,
            this.расписаниеСессииToolStripMenuItem5,
            this.дисциплиныРасписанияToolStripMenuItem4});
            this.горюшкинToolStripMenuItem.Name = "горюшкинToolStripMenuItem";
            this.горюшкинToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
            this.горюшкинToolStripMenuItem.Text = "Горюшкин";
            // 
            // файлаToolStripMenuItem5
            // 
            this.файлаToolStripMenuItem5.Name = "файлаToolStripMenuItem5";
            this.файлаToolStripMenuItem5.Size = new System.Drawing.Size(214, 22);
            this.файлаToolStripMenuItem5.Text = "4 файла";
            this.файлаToolStripMenuItem5.Click += new System.EventHandler(this.Gor4Files);
            // 
            // датыЗанятийToolStripMenuItem5
            // 
            this.датыЗанятийToolStripMenuItem5.Name = "датыЗанятийToolStripMenuItem5";
            this.датыЗанятийToolStripMenuItem5.Size = new System.Drawing.Size(214, 22);
            this.датыЗанятийToolStripMenuItem5.Text = "Даты занятий";
            this.датыЗанятийToolStripMenuItem5.Click += new System.EventHandler(this.GorJournalDates);
            // 
            // расписаниеToolStripMenuItem5
            // 
            this.расписаниеToolStripMenuItem5.Name = "расписаниеToolStripMenuItem5";
            this.расписаниеToolStripMenuItem5.Size = new System.Drawing.Size(214, 22);
            this.расписаниеToolStripMenuItem5.Text = "Расписание";
            this.расписаниеToolStripMenuItem5.Click += new System.EventHandler(this.GorSchedule);
            // 
            // расписаниеСессииToolStripMenuItem5
            // 
            this.расписаниеСессииToolStripMenuItem5.Name = "расписаниеСессииToolStripMenuItem5";
            this.расписаниеСессииToolStripMenuItem5.Size = new System.Drawing.Size(214, 22);
            this.расписаниеСессииToolStripMenuItem5.Text = "Расписание сессии";
            this.расписаниеСессииToolStripMenuItem5.Click += new System.EventHandler(this.GorSessionSchedule);
            // 
            // дисциплиныРасписанияToolStripMenuItem4
            // 
            this.дисциплиныРасписанияToolStripMenuItem4.Name = "дисциплиныРасписанияToolStripMenuItem4";
            this.дисциплиныРасписанияToolStripMenuItem4.Size = new System.Drawing.Size(214, 22);
            this.дисциплиныРасписанияToolStripMenuItem4.Text = "Дисциплины расписания";
            this.дисциплиныРасписанияToolStripMenuItem4.Click += new System.EventHandler(this.GorDisciplines);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.датыЗанятийToolStripMenuItem6,
            this.расписаниеToolStripMenuItem6,
            this.расписаниеСессииToolStripMenuItem6,
            this.дисциплиныРасписанияToolStripMenuItem5,
            this.аудиторииДисциплинToolStripMenuItem1,
            this.перевестиАудиторииВФорматООПToolStripMenuItem});
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(394, 22);
            this.toolStripMenuItem5.Text = "0718";
            // 
            // датыЗанятийToolStripMenuItem6
            // 
            this.датыЗанятийToolStripMenuItem6.Name = "датыЗанятийToolStripMenuItem6";
            this.датыЗанятийToolStripMenuItem6.Size = new System.Drawing.Size(278, 22);
            this.датыЗанятийToolStripMenuItem6.Text = "Даты занятий";
            this.датыЗанятийToolStripMenuItem6.Click += new System.EventHandler(this.датыЗанятийToolStripMenuItem6_Click);
            // 
            // расписаниеToolStripMenuItem6
            // 
            this.расписаниеToolStripMenuItem6.Name = "расписаниеToolStripMenuItem6";
            this.расписаниеToolStripMenuItem6.Size = new System.Drawing.Size(278, 22);
            this.расписаниеToolStripMenuItem6.Text = "Расписание";
            this.расписаниеToolStripMenuItem6.Click += new System.EventHandler(this.расписаниеToolStripMenuItem6_Click);
            // 
            // расписаниеСессииToolStripMenuItem6
            // 
            this.расписаниеСессииToolStripMenuItem6.Name = "расписаниеСессииToolStripMenuItem6";
            this.расписаниеСессииToolStripMenuItem6.Size = new System.Drawing.Size(278, 22);
            this.расписаниеСессииToolStripMenuItem6.Text = "Расписание сессии";
            this.расписаниеСессииToolStripMenuItem6.Click += new System.EventHandler(this.расписаниеСессииToolStripMenuItem6_Click);
            // 
            // дисциплиныРасписанияToolStripMenuItem5
            // 
            this.дисциплиныРасписанияToolStripMenuItem5.Name = "дисциплиныРасписанияToolStripMenuItem5";
            this.дисциплиныРасписанияToolStripMenuItem5.Size = new System.Drawing.Size(278, 22);
            this.дисциплиныРасписанияToolStripMenuItem5.Text = "Дисциплины расписания";
            this.дисциплиныРасписанияToolStripMenuItem5.Click += new System.EventHandler(this.дисциплиныРасписанияToolStripMenuItem5_Click);
            // 
            // аудиторииДисциплинToolStripMenuItem1
            // 
            this.аудиторииДисциплинToolStripMenuItem1.Name = "аудиторииДисциплинToolStripMenuItem1";
            this.аудиторииДисциплинToolStripMenuItem1.Size = new System.Drawing.Size(278, 22);
            this.аудиторииДисциплинToolStripMenuItem1.Text = "Аудитории дисциплин";
            this.аудиторииДисциплинToolStripMenuItem1.Click += new System.EventHandler(this.аудиторииДисциплинToolStripMenuItem1_Click);
            // 
            // перевестиАудиторииВФорматООПToolStripMenuItem
            // 
            this.перевестиАудиторииВФорматООПToolStripMenuItem.Name = "перевестиАудиторииВФорматООПToolStripMenuItem";
            this.перевестиАудиторииВФорматООПToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.перевестиАудиторииВФорматООПToolStripMenuItem.Text = "Перевести аудитории в формат ООП";
            this.перевестиАудиторииВФорматООПToolStripMenuItem.Click += new System.EventHandler(this.перевестиАудиторииВФорматООПToolStripMenuItem_Click);
            // 
            // bigRedButtonToolStripMenuItem
            // 
            this.bigRedButtonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьРасписаниеТекущейГруппыToolStripMenuItem,
            this.процентЗанятийПоДатамToolStripMenuItem,
            this.экспортДатЗачётовToolStripMenuItem,
            this.проверитьПравильностьПоследовательностейВидовЗанятийЛПToolStripMenuItem,
            this.экспортСпискаПреподавателейНа308ToolStripMenuItem,
            this.спискиПреподавателейПоКорпусамToolStripMenuItem,
            this.экспортАудиторийДисциплинToolStripMenuItem,
            this.экспортБазыДанныхВТекстовыйФайлToolStripMenuItem,
            this.backup10AAToolStripMenuItem,
            this.restore10AAToolStripMenuItem,
            this.exportMathDiscs100AAToolStripMenuItem,
            this.поправитьАудиторииToolStripMenuItem,
            this.поправитьАудиторииФакультетаИскусствToolStripMenuItem,
            this.поправитьАудиторииСессииToolStripMenuItem,
            this.убратьГруппыИностранныхЯзыковИзНазванийДисциплинToolStripMenuItem,
            this.графикПроцессаToolStripMenuItem,
            this.проверитьКоллизииПреподавателейToolStripMenuItem,
            this.копироватьРасписаниеНаДеньНеделюToolStripMenuItem,
            this.расписаниеПереходовМеждуКорпусамиToolStripMenuItem,
            this.неточности811ToolStripMenuItem,
            this.освободитьМестоВРасписанииToolStripMenuItem,
            this.restoreA0718OriginalDbsToolStripMenuItem,
            this.быстроДобавитьЗанятияToolStripMenuItem});
            this.bigRedButtonToolStripMenuItem.Name = "bigRedButtonToolStripMenuItem";
            this.bigRedButtonToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.bigRedButtonToolStripMenuItem.Text = "BIG RED BUTTON";
            // 
            // удалитьРасписаниеТекущейГруппыToolStripMenuItem
            // 
            this.удалитьРасписаниеТекущейГруппыToolStripMenuItem.Name = "удалитьРасписаниеТекущейГруппыToolStripMenuItem";
            this.удалитьРасписаниеТекущейГруппыToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.удалитьРасписаниеТекущейГруппыToolStripMenuItem.Text = "Удалить расписание текущей группы";
            this.удалитьРасписаниеТекущейГруппыToolStripMenuItem.Click += new System.EventHandler(this.удалитьРасписаниеТекущейГруппыToolStripMenuItem_Click);
            // 
            // процентЗанятийПоДатамToolStripMenuItem
            // 
            this.процентЗанятийПоДатамToolStripMenuItem.Name = "процентЗанятийПоДатамToolStripMenuItem";
            this.процентЗанятийПоДатамToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.процентЗанятийПоДатамToolStripMenuItem.Text = "Процент занятий по датам";
            this.процентЗанятийПоДатамToolStripMenuItem.Click += new System.EventHandler(this.процентЗанятийПоДатамToolStripMenuItem_Click);
            // 
            // экспортДатЗачётовToolStripMenuItem
            // 
            this.экспортДатЗачётовToolStripMenuItem.Name = "экспортДатЗачётовToolStripMenuItem";
            this.экспортДатЗачётовToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.экспортДатЗачётовToolStripMenuItem.Text = "Экспорт дат зачётов";
            this.экспортДатЗачётовToolStripMenuItem.Click += new System.EventHandler(this.экспортДатЗачётовToolStripMenuItem_Click);
            // 
            // проверитьПравильностьПоследовательностейВидовЗанятийЛПToolStripMenuItem
            // 
            this.проверитьПравильностьПоследовательностейВидовЗанятийЛПToolStripMenuItem.Name = "проверитьПравильностьПоследовательностейВидовЗанятийЛПToolStripMenuItem";
            this.проверитьПравильностьПоследовательностейВидовЗанятийЛПToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.проверитьПравильностьПоследовательностейВидовЗанятийЛПToolStripMenuItem.Text = "Проверить правильность последовательностей видов занятий (Л/П)";
            this.проверитьПравильностьПоследовательностейВидовЗанятийЛПToolStripMenuItem.Click += new System.EventHandler(this.проверитьПравильностьПоследовательностейВидовЗанятийЛПToolStripMenuItem_Click);
            // 
            // экспортСпискаПреподавателейНа308ToolStripMenuItem
            // 
            this.экспортСпискаПреподавателейНа308ToolStripMenuItem.Name = "экспортСпискаПреподавателейНа308ToolStripMenuItem";
            this.экспортСпискаПреподавателейНа308ToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.экспортСпискаПреподавателейНа308ToolStripMenuItem.Text = "Экспорт списка преподавателей на 308";
            this.экспортСпискаПреподавателейНа308ToolStripMenuItem.Click += new System.EventHandler(this.экспортСпискаПреподавателейНа308ToolStripMenuItem_Click);
            // 
            // спискиПреподавателейПоКорпусамToolStripMenuItem
            // 
            this.спискиПреподавателейПоКорпусамToolStripMenuItem.Name = "спискиПреподавателейПоКорпусамToolStripMenuItem";
            this.спискиПреподавателейПоКорпусамToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.спискиПреподавателейПоКорпусамToolStripMenuItem.Text = "Списки преподавателей по корпусам";
            this.спискиПреподавателейПоКорпусамToolStripMenuItem.Click += new System.EventHandler(this.спискиПреподавателейПоКорпусамToolStripMenuItem_Click);
            // 
            // экспортАудиторийДисциплинToolStripMenuItem
            // 
            this.экспортАудиторийДисциплинToolStripMenuItem.Name = "экспортАудиторийДисциплинToolStripMenuItem";
            this.экспортАудиторийДисциплинToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.экспортАудиторийДисциплинToolStripMenuItem.Text = "Экспорт аудиторий дисциплин";
            this.экспортАудиторийДисциплинToolStripMenuItem.Click += new System.EventHandler(this.экспортАудиторийДисциплинToolStripMenuItem_Click);
            // 
            // экспортБазыДанныхВТекстовыйФайлToolStripMenuItem
            // 
            this.экспортБазыДанныхВТекстовыйФайлToolStripMenuItem.Name = "экспортБазыДанныхВТекстовыйФайлToolStripMenuItem";
            this.экспортБазыДанныхВТекстовыйФайлToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.экспортБазыДанныхВТекстовыйФайлToolStripMenuItem.Text = "Экспорт базы данных в текстовый файл";
            this.экспортБазыДанныхВТекстовыйФайлToolStripMenuItem.Click += new System.EventHandler(this.экспортБазыДанныхВТекстовыйФайлToolStripMenuItem_Click);
            // 
            // backup10AAToolStripMenuItem
            // 
            this.backup10AAToolStripMenuItem.Name = "backup10AAToolStripMenuItem";
            this.backup10AAToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.backup10AAToolStripMenuItem.Text = "Backup 10 AA";
            this.backup10AAToolStripMenuItem.Click += new System.EventHandler(this.backup10AAToolStripMenuItem_Click);
            // 
            // restore10AAToolStripMenuItem
            // 
            this.restore10AAToolStripMenuItem.Name = "restore10AAToolStripMenuItem";
            this.restore10AAToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.restore10AAToolStripMenuItem.Text = "Restore 10 AA";
            this.restore10AAToolStripMenuItem.Click += new System.EventHandler(this.restore10AAToolStripMenuItem_Click);
            // 
            // exportMathDiscs100AAToolStripMenuItem
            // 
            this.exportMathDiscs100AAToolStripMenuItem.Name = "exportMathDiscs100AAToolStripMenuItem";
            this.exportMathDiscs100AAToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.exportMathDiscs100AAToolStripMenuItem.Text = "Export Math Discs 10AA";
            this.exportMathDiscs100AAToolStripMenuItem.Click += new System.EventHandler(this.exportMathDiscs10AAToolStripMenuItem_Click);
            // 
            // поправитьАудиторииToolStripMenuItem
            // 
            this.поправитьАудиторииToolStripMenuItem.Name = "поправитьАудиторииToolStripMenuItem";
            this.поправитьАудиторииToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.поправитьАудиторииToolStripMenuItem.Text = "Поправить аудитории";
            this.поправитьАудиторииToolStripMenuItem.Click += new System.EventHandler(this.поправитьАудиторииToolStripMenuItem_Click);
            // 
            // поправитьАудиторииФакультетаИскусствToolStripMenuItem
            // 
            this.поправитьАудиторииФакультетаИскусствToolStripMenuItem.Name = "поправитьАудиторииФакультетаИскусствToolStripMenuItem";
            this.поправитьАудиторииФакультетаИскусствToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.поправитьАудиторииФакультетаИскусствToolStripMenuItem.Text = "Поправить аудитории факультета искусств";
            this.поправитьАудиторииФакультетаИскусствToolStripMenuItem.Click += new System.EventHandler(this.поправитьАудиторииФакультетаИскусствToolStripMenuItem_Click);
            // 
            // поправитьАудиторииСессииToolStripMenuItem
            // 
            this.поправитьАудиторииСессииToolStripMenuItem.Name = "поправитьАудиторииСессииToolStripMenuItem";
            this.поправитьАудиторииСессииToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.поправитьАудиторииСессииToolStripMenuItem.Text = "Поправить аудитории сессии";
            this.поправитьАудиторииСессииToolStripMenuItem.Click += new System.EventHandler(this.поправитьАудиторииСессииToolStripMenuItem_Click);
            // 
            // убратьГруппыИностранныхЯзыковИзНазванийДисциплинToolStripMenuItem
            // 
            this.убратьГруппыИностранныхЯзыковИзНазванийДисциплинToolStripMenuItem.Name = "убратьГруппыИностранныхЯзыковИзНазванийДисциплинToolStripMenuItem";
            this.убратьГруппыИностранныхЯзыковИзНазванийДисциплинToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.убратьГруппыИностранныхЯзыковИзНазванийДисциплинToolStripMenuItem.Text = "Убрать группы иностранных языков из названий дисциплин";
            this.убратьГруппыИностранныхЯзыковИзНазванийДисциплинToolStripMenuItem.Click += new System.EventHandler(this.убратьГруппыИностранныхЯзыковИзНазванийДисциплинToolStripMenuItem_Click);
            // 
            // графикПроцессаToolStripMenuItem
            // 
            this.графикПроцессаToolStripMenuItem.Name = "графикПроцессаToolStripMenuItem";
            this.графикПроцессаToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.графикПроцессаToolStripMenuItem.Text = "График процесса";
            this.графикПроцессаToolStripMenuItem.Click += new System.EventHandler(this.графикПроцессаToolStripMenuItem_Click);
            // 
            // проверитьКоллизииПреподавателейToolStripMenuItem
            // 
            this.проверитьКоллизииПреподавателейToolStripMenuItem.Name = "проверитьКоллизииПреподавателейToolStripMenuItem";
            this.проверитьКоллизииПреподавателейToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.проверитьКоллизииПреподавателейToolStripMenuItem.Text = "Проверить коллизии преподавателей";
            this.проверитьКоллизииПреподавателейToolStripMenuItem.Click += new System.EventHandler(this.проверитьКоллизииПреподавателейToolStripMenuItem_Click);
            // 
            // копироватьРасписаниеНаДеньНеделюToolStripMenuItem
            // 
            this.копироватьРасписаниеНаДеньНеделюToolStripMenuItem.Name = "копироватьРасписаниеНаДеньНеделюToolStripMenuItem";
            this.копироватьРасписаниеНаДеньНеделюToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.копироватьРасписаниеНаДеньНеделюToolStripMenuItem.Text = "Копировать расписание на день неделю";
            this.копироватьРасписаниеНаДеньНеделюToolStripMenuItem.Click += new System.EventHandler(this.копироватьРасписаниеНаДеньНеделюToolStripMenuItem_Click);
            // 
            // расписаниеПереходовМеждуКорпусамиToolStripMenuItem
            // 
            this.расписаниеПереходовМеждуКорпусамиToolStripMenuItem.Name = "расписаниеПереходовМеждуКорпусамиToolStripMenuItem";
            this.расписаниеПереходовМеждуКорпусамиToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.расписаниеПереходовМеждуКорпусамиToolStripMenuItem.Text = "Расписание переходов между корпусами";
            this.расписаниеПереходовМеждуКорпусамиToolStripMenuItem.Click += new System.EventHandler(this.расписаниеПереходовМеждуКорпусамиToolStripMenuItem_Click);
            // 
            // неточности811ToolStripMenuItem
            // 
            this.неточности811ToolStripMenuItem.Name = "неточности811ToolStripMenuItem";
            this.неточности811ToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.неточности811ToolStripMenuItem.Text = "Неточности 8-11";
            this.неточности811ToolStripMenuItem.Click += new System.EventHandler(this.неточности811ToolStripMenuItem_Click);
            // 
            // освободитьМестоВРасписанииToolStripMenuItem
            // 
            this.освободитьМестоВРасписанииToolStripMenuItem.Name = "освободитьМестоВРасписанииToolStripMenuItem";
            this.освободитьМестоВРасписанииToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.освободитьМестоВРасписанииToolStripMenuItem.Text = "Освободить место в расписании";
            this.освободитьМестоВРасписанииToolStripMenuItem.Click += new System.EventHandler(this.освободитьМестоВРасписанииToolStripMenuItem_Click);
            // 
            // restoreA0718OriginalDbsToolStripMenuItem
            // 
            this.restoreA0718OriginalDbsToolStripMenuItem.Name = "restoreA0718OriginalDbsToolStripMenuItem";
            this.restoreA0718OriginalDbsToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.restoreA0718OriginalDbsToolStripMenuItem.Text = "Restore A0718 Original DBs";
            this.restoreA0718OriginalDbsToolStripMenuItem.Click += new System.EventHandler(this.restoreA0718OriginalDbsToolStripMenuItem_Click);
            // 
            // быстроДобавитьЗанятияToolStripMenuItem
            // 
            this.быстроДобавитьЗанятияToolStripMenuItem.Name = "быстроДобавитьЗанятияToolStripMenuItem";
            this.быстроДобавитьЗанятияToolStripMenuItem.Size = new System.Drawing.Size(451, 22);
            this.быстроДобавитьЗанятияToolStripMenuItem.Text = "Быстро добавить занятия";
            this.быстроДобавитьЗанятияToolStripMenuItem.Click += new System.EventHandler(this.быстроДобавитьЗанятияToolStripMenuItem_Click);
            // 
            // googleCalendarToolStripMenuItem
            // 
            this.googleCalendarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GoogleCalendarExport,
            this.очиститьКаледнарьToolStripMenuItem});
            this.googleCalendarToolStripMenuItem.Name = "googleCalendarToolStripMenuItem";
            this.googleCalendarToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.googleCalendarToolStripMenuItem.Text = "Google Calendar";
            // 
            // GoogleCalendarExport
            // 
            this.GoogleCalendarExport.Name = "GoogleCalendarExport";
            this.GoogleCalendarExport.Size = new System.Drawing.Size(187, 22);
            this.GoogleCalendarExport.Text = "Экспорт расписания";
            this.GoogleCalendarExport.Click += new System.EventHandler(this.GoogleCalendarExport_Click);
            // 
            // очиститьКаледнарьToolStripMenuItem
            // 
            this.очиститьКаледнарьToolStripMenuItem.Name = "очиститьКаледнарьToolStripMenuItem";
            this.очиститьКаледнарьToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.очиститьКаледнарьToolStripMenuItem.Text = "Очистить каледнарь";
            this.очиститьКаледнарьToolStripMenuItem.Click += new System.EventHandler(this.очиститьКаледнарьToolStripMenuItem_Click);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.statusStrip1);
            this.viewPanel.Controls.Add(this.ScheduleView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 196);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(971, 393);
            this.viewPanel.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 371);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(971, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 17);
            // 
            // ScheduleView
            // 
            this.ScheduleView.AllowDrop = true;
            this.ScheduleView.AllowUserToAddRows = false;
            this.ScheduleView.AllowUserToDeleteRows = false;
            this.ScheduleView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ScheduleView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScheduleView.ContextMenuStrip = this.contextMenuStrip1;
            this.ScheduleView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScheduleView.Location = new System.Drawing.Point(0, 0);
            this.ScheduleView.Name = "ScheduleView";
            this.ScheduleView.ReadOnly = true;
            this.ScheduleView.RowHeadersVisible = false;
            this.ScheduleView.Size = new System.Drawing.Size(971, 393);
            this.ScheduleView.TabIndex = 1;
            this.ScheduleView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScheduleView_CellContentClick);
            this.ScheduleView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainViewCellDoubleClick);
            this.ScheduleView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ScheduleView_CellMouseDown);
            this.ScheduleView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ScheduleView_DragDrop);
            this.ScheduleView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ScheduleView_DragEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSchedule});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(134, 26);
            // 
            // editSchedule
            // 
            this.editSchedule.Name = "editSchedule";
            this.editSchedule.Size = new System.Drawing.Size(133, 22);
            this.editSchedule.Text = "Аудитория";
            this.editSchedule.Click += new System.EventHandler(this.editSchedule_Click);
            // 
            // MainEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 589);
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
            this.viewPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel controlsPanel;
        private Button showGroupLessons;
        private ComboBox groupList;
        private Panel viewPanel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem справочникиToolStripMenuItem;
        private ToolStripMenuItem аудиторииToolStripMenuItem;
        private ToolStripMenuItem дниСеместраToolStripMenuItem;
        private ToolStripMenuItem звонкиToolStripMenuItem;
        private ToolStripMenuItem студентыToolStripMenuItem;
        private ToolStripMenuItem группыToolStripMenuItem;
        private ToolStripMenuItem преподавателиToolStripMenuItem;
        private ToolStripMenuItem дисциплиныToolStripMenuItem;
        private DataGridView ScheduleView;
        private Button button1;
        private Button LoadToSite;
        private ToolStripMenuItem опцииToolStripMenuItem;
        private Button auditoriumKaput;
        private ToolStripMenuItem факультетыгруппыToolStripMenuItem;
        private ComboBox FacultyList;
        private ComboBox DOWList;
        private Button ActiveLessonsCount;
        private Button ManyGroups;
        private ToolStripMenuItem занятостьАудиторийToolStripMenuItem;
        private Button oneAuditorium;
        private Button auditoriums;
        private Button allChanges;
        private Button CreatePDF;
        private CheckBox weekFiltered;
        private ComboBox WeekFilter;
        private Button button2;
        private ComboBox WordFacultyFilter;
        private CheckBox WordOneFaculty;
        private Button WordExportButton;
        private Button WordCustom;
        private ComboBox WordExportWeekFilter;
        private CheckBox wordExportWeekFiltered;
        private Button BIGREDBUTTON;
        private Button WordSchool;
        private Button WordSchool2;
        private Button happyBirthday;
        private TextBox uploadPrefix;
        private Button WordWholeScheduleOneGroupOnePage;
        private Button OnePageGroupScheduleWordExport;
        private CheckBox cb90;
        private TextBox ToDBName;
        private Button DownloadRestore;
        private TextBox FromDBName;
        private Button BackupUpload;
        private Button startSchoolWordExport;
        private ToolStripMenuItem корпусаToolStripMenuItem;
        private ToolStripMenuItem анализToolStripMenuItem;
        private ToolStripMenuItem пожеланияToolStripMenuItem;
        private ToolStripMenuItem аудиторииДисциплинToolStripMenuItem;
        private ToolStripMenuItem нельзяСтавитьПоследнимУрокомToolStripMenuItem;
        private ToolStripMenuItem парыДисциплинНельзяСтавитьВОдинДеньToolStripMenuItem;
        private ToolStripMenuItem дисциплиныЛучшеСтавитьПо2УрокаToolStripMenuItem;
        private ToolStripMenuItem порядокПостановкиДисциплинВРасписаниеToolStripMenuItem;
        private CheckBox showProposedLessons;
        private Button analyseSchool;
        private Button analyse;
        private Button removeAllProposedLessons;
        private ToolStripMenuItem периодыГруппToolStripMenuItem;
        private ToolStripMenuItem корпусИПреимущественнаяАудиторияГруппыToolStripMenuItem;
        private ToolStripMenuItem дисциплиныСГарантированнойНаружнейАудиториейToolStripMenuItem;
        private ToolStripMenuItem сменыToolStripMenuItem;
        private CheckBox OnlyFutureDatesExportInWord;
        private Button button3;
        private ComboBox siteToUpload;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem заметкиКРасписаниюToolStripMenuItem;
        private ToolStripMenuItem названияДисциплинToolStripMenuItem;
        private ToolStripMenuItem экспортВWordToolStripMenuItem;
        private ToolStripMenuItem датыПоФизическойКультуреToolStripMenuItem;
        private ToolStripMenuItem bigRedButtonToolStripMenuItem;
        private ToolStripMenuItem удалитьРасписаниеТекущейГруппыToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem teachersToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel status;
        private ToolStripMenuItem процентЗанятийПоДатамToolStripMenuItem;
        private ToolStripMenuItem экспортДатЗачётовToolStripMenuItem;
        private ToolStripMenuItem проверитьПравильностьПоследовательностейВидовЗанятийЛПToolStripMenuItem;
        private ToolStripMenuItem экспортСпискаПреподавателейНа308ToolStripMenuItem;
        private ToolStripMenuItem последовательностьТиповЗанятийЛППоФакультетамToolStripMenuItem;
        private ToolStripMenuItem датыЗачётовToolStripMenuItem;
        private ToolStripMenuItem датыЗанятийПоМесяцамToolStripMenuItem;
        private ToolStripMenuItem спискиПреподавателейПоКорпусамToolStripMenuItem;
        private ToolStripMenuItem экспортАудиторийДисциплинToolStripMenuItem;
        private ToolStripMenuItem экспортБазыДанныхВТекстовыйФайлToolStripMenuItem;
        private ComboBox semester;
        private Button changeSemesterDb;
        private ToolStripMenuItem датыЗанятийФилологовToolStripMenuItem;
        private ToolStripMenuItem датыЗанятийЭкономистовмагистратураToolStripMenuItem;
        private ToolStripMenuItem датыЗанятийЮристовмагистратураToolStripMenuItem;
        private ToolStripMenuItem датыЗанятийМатематиковToolStripMenuItem;
        private ToolStripMenuItem backup10AAToolStripMenuItem;
        private ToolStripMenuItem restore10AAToolStripMenuItem;
        private ToolStripMenuItem вставитьПоследовательностиТиповЗанятияИзФайлаToolStripMenuItem;
        private ToolStripMenuItem exportMathDiscs100AAToolStripMenuItem;
        private ToolStripMenuItem датыЗанятийToolStripMenuItem;
        private ToolStripMenuItem расписаниеToolStripMenuItem;
        private ToolStripMenuItem датыЗанятийToolStripMenuItem1;
        private ToolStripMenuItem расписаниеToolStripMenuItem1;
        private ToolStripMenuItem датыЗанятийToolStripMenuItem2;
        private ToolStripMenuItem расписаниеToolStripMenuItem2;
        private ToolStripMenuItem датыЗанятийToolStripMenuItem3;
        private ToolStripMenuItem расписаниеToolStripMenuItem3;
        private ToolStripMenuItem расписаниеСессииToolStripMenuItem;
        private ToolStripMenuItem расписаниеСессииToolStripMenuItem1;
        private ToolStripMenuItem расписаниеСессииToolStripMenuItem2;
        private ToolStripMenuItem расписаниеСессииToolStripMenuItem3;
        private ToolStripMenuItem дисциплиныРасписанияToolStripMenuItem;
        private ToolStripMenuItem дисциплиныРасписанияToolStripMenuItem1;
        private ToolStripMenuItem дисциплиныРасписанияToolStripMenuItem2;
        private ToolStripMenuItem дисциплиныРасписанияToolStripMenuItem3;
        private ToolStripMenuItem файлаToolStripMenuItem;
        private ToolStripMenuItem файлаToolStripMenuItem1;
        private ToolStripMenuItem файлаToolStripMenuItem2;
        private ToolStripMenuItem файлаToolStripMenuItem3;
        private ToolStripMenuItem искусствоToolStripMenuItem;
        private ToolStripMenuItem файлаToolStripMenuItem4;
        private ToolStripMenuItem датыЗанятийToolStripMenuItem4;
        private ToolStripMenuItem расписаниеToolStripMenuItem4;
        private ToolStripMenuItem расписаниеСессииToolStripMenuItem4;
        private ToolStripMenuItem дисциплиныРасписаниеToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem горюшкинToolStripMenuItem;
        private ToolStripMenuItem файлаToolStripMenuItem5;
        private ToolStripMenuItem датыЗанятийToolStripMenuItem5;
        private ToolStripMenuItem расписаниеToolStripMenuItem5;
        private ToolStripMenuItem расписаниеСессииToolStripMenuItem5;
        private ToolStripMenuItem дисциплиныРасписанияToolStripMenuItem4;
        private ToolStripMenuItem поправитьАудиторииToolStripMenuItem;
        private ToolStripMenuItem поправитьАудиторииФакультетаИскусствToolStripMenuItem;
        private ToolStripMenuItem поправитьАудиторииСессииToolStripMenuItem;
        private ToolStripMenuItem убратьГруппыИностранныхЯзыковИзНазванийДисциплинToolStripMenuItem;
        private ToolStripMenuItem графикПроцессаToolStripMenuItem;
        private ToolStripMenuItem проверитьКоллизииПреподавателейToolStripMenuItem;
        private ToolStripMenuItem копироватьРасписаниеНаДеньНеделюToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem editSchedule;
        private ToolStripMenuItem расписаниеПереходовМеждуКорпусамиToolStripMenuItem;
        private Button ElevenTwelveWeek;
        private ToolStripMenuItem неточности811ToolStripMenuItem;
        private ToolStripMenuItem освободитьМестоВРасписанииToolStripMenuItem;
        private Button FontSmaller;
        private Button FontBigger;
        private Button week17;
        private Button week16;
        private ToolStripMenuItem googleCalendarToolStripMenuItem;
        private ToolStripMenuItem GoogleCalendarExport;
        private ToolStripMenuItem очиститьКаледнарьToolStripMenuItem;
        private ToolStripMenuItem restoreA0718OriginalDbsToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem датыЗанятийToolStripMenuItem6;
        private ToolStripMenuItem расписаниеToolStripMenuItem6;
        private ToolStripMenuItem расписаниеСессииToolStripMenuItem6;
        private ToolStripMenuItem дисциплиныРасписанияToolStripMenuItem5;
        private ToolStripMenuItem аудиторииДисциплинToolStripMenuItem1;
        private ToolStripMenuItem перевестиАудиторииВФорматООПToolStripMenuItem;
        private ToolStripMenuItem быстроДобавитьЗанятияToolStripMenuItem;
    }
}

