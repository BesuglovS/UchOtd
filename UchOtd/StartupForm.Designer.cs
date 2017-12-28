using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd
{
    partial class StartupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupForm));
            this.trayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.контингентToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заметкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.телефоныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.EditScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.расписаниеСессииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.показатьОкноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеНаДеньToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеПреподавателяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеПреподавателяAltTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокПарПоПреподавателюCtrlShiftTToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.часыПоПреподавателюAltShiftTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокПарПоДисциплинеAltLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.занятостьАудиторийAltAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.измененияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.openDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.uploadTimer = new System.Windows.Forms.Timer(this.components);
            this.trayIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIconMenu
            // 
            this.trayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.контингентToolStripMenuItem,
            this.заметкиToolStripMenuItem,
            this.телефоныToolStripMenuItem,
            this.toolStripMenuItem4,
            this.EditScheduleToolStripMenuItem,
            this.toolStripMenuItem2,
            this.расписаниеСессииToolStripMenuItem,
            this.toolStripMenuItem5,
            this.показатьОкноToolStripMenuItem,
            this.расписаниеНаДеньToolStripMenuItem,
            this.расписаниеПреподавателяToolStripMenuItem,
            this.списокПарПоДисциплинеAltLToolStripMenuItem,
            this.занятостьАудиторийAltAToolStripMenuItem,
            this.измененияToolStripMenuItem,
            this.toolStripMenuItem3,
            this.openDBToolStripMenuItem,
            this.toolStripMenuItem1,
            this.выходToolStripMenuItem});
            this.trayIconMenu.Name = "trayIconMenu";
            this.trayIconMenu.Size = new System.Drawing.Size(266, 320);
            // 
            // контингентToolStripMenuItem
            // 
            this.контингентToolStripMenuItem.Image = global::UchOtd.Properties.Resources.people;
            this.контингентToolStripMenuItem.Name = "контингентToolStripMenuItem";
            this.контингентToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.контингентToolStripMenuItem.Text = "Контингент (Alt+S)";
            this.контингентToolStripMenuItem.Click += new System.EventHandler(this.КонтингентToolStripMenuItem_Click);
            // 
            // заметкиToolStripMenuItem
            // 
            this.заметкиToolStripMenuItem.Image = global::UchOtd.Properties.Resources.notes;
            this.заметкиToolStripMenuItem.Name = "заметкиToolStripMenuItem";
            this.заметкиToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.заметкиToolStripMenuItem.Text = "Заметки (Alt+N)";
            this.заметкиToolStripMenuItem.Click += new System.EventHandler(this.ЗаметкиToolStripMenuItem_Click);
            // 
            // телефоныToolStripMenuItem
            // 
            this.телефоныToolStripMenuItem.Image = global::UchOtd.Properties.Resources.phone;
            this.телефоныToolStripMenuItem.Name = "телефоныToolStripMenuItem";
            this.телефоныToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.телефоныToolStripMenuItem.Text = "Телефоны (Alt+P)";
            this.телефоныToolStripMenuItem.Click += new System.EventHandler(this.ТелефоныToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(262, 6);
            // 
            // EditScheduleToolStripMenuItem
            // 
            this.EditScheduleToolStripMenuItem.Image = global::UchOtd.Properties.Resources.Edit;
            this.EditScheduleToolStripMenuItem.Name = "EditScheduleToolStripMenuItem";
            this.EditScheduleToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.EditScheduleToolStripMenuItem.Text = "Редактор расписания (Alt+R)";
            this.EditScheduleToolStripMenuItem.Click += new System.EventHandler(this.EditScheduleToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(262, 6);
            // 
            // расписаниеСессииToolStripMenuItem
            // 
            this.расписаниеСессииToolStripMenuItem.Image = global::UchOtd.Properties.Resources.bell;
            this.расписаниеСессииToolStripMenuItem.Name = "расписаниеСессииToolStripMenuItem";
            this.расписаниеСессииToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.расписаниеСессииToolStripMenuItem.Text = "Расписание сессии (Ctrl+Alt+S)";
            this.расписаниеСессииToolStripMenuItem.Click += new System.EventHandler(this.РасписаниеСессииToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(262, 6);
            // 
            // показатьОкноToolStripMenuItem
            // 
            this.показатьОкноToolStripMenuItem.Image = global::UchOtd.Properties.Resources.showWindow;
            this.показатьОкноToolStripMenuItem.Name = "показатьОкноToolStripMenuItem";
            this.показатьОкноToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.показатьОкноToolStripMenuItem.Text = "Просмотр расписания (Alt+V)";
            this.показатьОкноToolStripMenuItem.Click += new System.EventHandler(this.ПоказатьОкноToolStripMenuItemClick);
            // 
            // расписаниеНаДеньToolStripMenuItem
            // 
            this.расписаниеНаДеньToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("расписаниеНаДеньToolStripMenuItem.Image")));
            this.расписаниеНаДеньToolStripMenuItem.Name = "расписаниеНаДеньToolStripMenuItem";
            this.расписаниеНаДеньToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.расписаниеНаДеньToolStripMenuItem.Text = "Расписание на день";
            this.расписаниеНаДеньToolStripMenuItem.Click += new System.EventHandler(this.РасписаниеНаДеньToolStripMenuItem_Click);
            // 
            // расписаниеПреподавателяToolStripMenuItem
            // 
            this.расписаниеПреподавателяToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.расписаниеПреподавателяAltTToolStripMenuItem,
            this.списокПарПоПреподавателюCtrlShiftTToolStripMenuItem1,
            this.часыПоПреподавателюAltShiftTToolStripMenuItem});
            this.расписаниеПреподавателяToolStripMenuItem.Image = global::UchOtd.Properties.Resources.teacher;
            this.расписаниеПреподавателяToolStripMenuItem.Name = "расписаниеПреподавателяToolStripMenuItem";
            this.расписаниеПреподавателяToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.расписаниеПреподавателяToolStripMenuItem.Text = "Данные по преподавателю";
            // 
            // расписаниеПреподавателяAltTToolStripMenuItem
            // 
            this.расписаниеПреподавателяAltTToolStripMenuItem.Image = global::UchOtd.Properties.Resources.teacher;
            this.расписаниеПреподавателяAltTToolStripMenuItem.Name = "расписаниеПреподавателяAltTToolStripMenuItem";
            this.расписаниеПреподавателяAltTToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.расписаниеПреподавателяAltTToolStripMenuItem.Text = "Расписание преподавателя (Alt+T)";
            this.расписаниеПреподавателяAltTToolStripMenuItem.Click += new System.EventHandler(this.РасписаниеПреподавателяToolStripMenuItemClick);
            // 
            // списокПарПоПреподавателюCtrlShiftTToolStripMenuItem1
            // 
            this.списокПарПоПреподавателюCtrlShiftTToolStripMenuItem1.Image = global::UchOtd.Properties.Resources.teacher;
            this.списокПарПоПреподавателюCtrlShiftTToolStripMenuItem1.Name = "списокПарПоПреподавателюCtrlShiftTToolStripMenuItem1";
            this.списокПарПоПреподавателюCtrlShiftTToolStripMenuItem1.Size = new System.Drawing.Size(296, 22);
            this.списокПарПоПреподавателюCtrlShiftTToolStripMenuItem1.Text = "Часы преподавателя (Ctrl+Shift+T)";
            this.списокПарПоПреподавателюCtrlShiftTToolStripMenuItem1.Click += new System.EventHandler(this.СписокПарПоПреподавателюCtrlShiftTToolStripMenuItem1_Click);
            // 
            // часыПоПреподавателюAltShiftTToolStripMenuItem
            // 
            this.часыПоПреподавателюAltShiftTToolStripMenuItem.Image = global::UchOtd.Properties.Resources.teacher;
            this.часыПоПреподавателюAltShiftTToolStripMenuItem.Name = "часыПоПреподавателюAltShiftTToolStripMenuItem";
            this.часыПоПреподавателюAltShiftTToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.часыПоПреподавателюAltShiftTToolStripMenuItem.Text = "Список пар преподавателя (Alt+Shift+T)";
            this.часыПоПреподавателюAltShiftTToolStripMenuItem.Click += new System.EventHandler(this.ЧасыПоПреподавателюAltShiftTToolStripMenuItem_Click);
            // 
            // списокПарПоДисциплинеAltLToolStripMenuItem
            // 
            this.списокПарПоДисциплинеAltLToolStripMenuItem.Image = global::UchOtd.Properties.Resources.Book;
            this.списокПарПоДисциплинеAltLToolStripMenuItem.Name = "списокПарПоДисциплинеAltLToolStripMenuItem";
            this.списокПарПоДисциплинеAltLToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.списокПарПоДисциплинеAltLToolStripMenuItem.Text = "Список пар по дисциплине (Alt+L)";
            this.списокПарПоДисциплинеAltLToolStripMenuItem.Click += new System.EventHandler(this.СписокПарПоДисциплинеAltLToolStripMenuItem_Click);
            // 
            // занятостьАудиторийAltAToolStripMenuItem
            // 
            this.занятостьАудиторийAltAToolStripMenuItem.Image = global::UchOtd.Properties.Resources.AuditoriumChanged;
            this.занятостьАудиторийAltAToolStripMenuItem.Name = "занятостьАудиторийAltAToolStripMenuItem";
            this.занятостьАудиторийAltAToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.занятостьАудиторийAltAToolStripMenuItem.Text = "Занятость аудиторий (Alt+A)";
            this.занятостьАудиторийAltAToolStripMenuItem.Click += new System.EventHandler(this.ЗанятостьАудиторийAltAToolStripMenuItemClick);
            // 
            // измененияToolStripMenuItem
            // 
            this.измененияToolStripMenuItem.Image = global::UchOtd.Properties.Resources.diff;
            this.измененияToolStripMenuItem.Name = "измененияToolStripMenuItem";
            this.измененияToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.измененияToolStripMenuItem.Text = "Изменения расписания (Alt+C)";
            this.измененияToolStripMenuItem.Click += new System.EventHandler(this.ИзмененияToolStripMenuItemClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(262, 6);
            // 
            // openDBToolStripMenuItem
            // 
            this.openDBToolStripMenuItem.Image = global::UchOtd.Properties.Resources.ODBCKit;
            this.openDBToolStripMenuItem.Name = "openDBToolStripMenuItem";
            this.openDBToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.openDBToolStripMenuItem.Text = "Сменить базу данных";
            this.openDBToolStripMenuItem.Click += new System.EventHandler(this.OpenDBToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(262, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Image = global::UchOtd.Properties.Resources.exit;
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.ВыходToolStripMenuItemClick);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayIconMenu;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Учебный отдел";
            // 
            // uploadTimer
            // 
            this.uploadTimer.Interval = 1800000;
            this.uploadTimer.Tick += new System.EventHandler(this.UploadTimer_Tick);
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 203);
            this.Name = "StartupForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.StartupForm_Load);
            this.trayIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ContextMenuStrip trayIconMenu;
        private ToolStripMenuItem измененияToolStripMenuItem;
        private ToolStripMenuItem расписаниеПреподавателяToolStripMenuItem;
        private ToolStripMenuItem показатьОкноToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private NotifyIcon trayIcon;
        private ToolStripMenuItem контингентToolStripMenuItem;
        private ToolStripMenuItem занятостьАудиторийAltAToolStripMenuItem;
        private ToolStripMenuItem заметкиToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem телефоныToolStripMenuItem;
        private ToolStripMenuItem openDBToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem EditScheduleToolStripMenuItem;
        private ToolStripMenuItem часыПоПреподавателюAltShiftTToolStripMenuItem;
        private ToolStripMenuItem списокПарПоПреподавателюCtrlShiftTToolStripMenuItem1;
        private ToolStripMenuItem списокПарПоДисциплинеAltLToolStripMenuItem;
        private ToolStripMenuItem расписаниеПреподавателяAltTToolStripMenuItem;
        private ToolStripMenuItem расписаниеНаДеньToolStripMenuItem;
        private ToolStripMenuItem расписаниеСессииToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem5;
        private Timer uploadTimer;
    }
}

