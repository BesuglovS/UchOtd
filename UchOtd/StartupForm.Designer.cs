namespace UchOtd
{
    partial class StartupForm
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
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.показатьОкноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеПреподавателяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.занятостьАудиторийAltAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.измененияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.trayIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIconMenu
            // 
            this.trayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.контингентToolStripMenuItem,
            this.заметкиToolStripMenuItem,
            this.телефоныToolStripMenuItem,
            this.toolStripMenuItem2,
            this.показатьОкноToolStripMenuItem,
            this.расписаниеПреподавателяToolStripMenuItem,
            this.занятостьАудиторийAltAToolStripMenuItem,
            this.измененияToolStripMenuItem,
            this.toolStripMenuItem3,
            this.openDBToolStripMenuItem,
            this.toolStripMenuItem1,
            this.выходToolStripMenuItem});
            this.trayIconMenu.Name = "trayIconMenu";
            this.trayIconMenu.Size = new System.Drawing.Size(266, 242);
            // 
            // контингентToolStripMenuItem
            // 
            this.контингентToolStripMenuItem.Image = global::UchOtd.Properties.Resources.people;
            this.контингентToolStripMenuItem.Name = "контингентToolStripMenuItem";
            this.контингентToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.контингентToolStripMenuItem.Text = "Контингент (Alt+S)";
            this.контингентToolStripMenuItem.Click += new System.EventHandler(this.контингентToolStripMenuItem_Click);
            // 
            // заметкиToolStripMenuItem
            // 
            this.заметкиToolStripMenuItem.Image = global::UchOtd.Properties.Resources.notes;
            this.заметкиToolStripMenuItem.Name = "заметкиToolStripMenuItem";
            this.заметкиToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.заметкиToolStripMenuItem.Text = "Заметки (Alt+N)";
            this.заметкиToolStripMenuItem.Click += new System.EventHandler(this.заметкиToolStripMenuItem_Click);
            // 
            // телефоныToolStripMenuItem
            // 
            this.телефоныToolStripMenuItem.Image = global::UchOtd.Properties.Resources.phone;
            this.телефоныToolStripMenuItem.Name = "телефоныToolStripMenuItem";
            this.телефоныToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.телефоныToolStripMenuItem.Text = "Телефоны (Alt+P)";
            this.телефоныToolStripMenuItem.Click += new System.EventHandler(this.телефоныToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(262, 6);
            // 
            // показатьОкноToolStripMenuItem
            // 
            this.показатьОкноToolStripMenuItem.Image = global::UchOtd.Properties.Resources.showWindow;
            this.показатьОкноToolStripMenuItem.Name = "показатьОкноToolStripMenuItem";
            this.показатьОкноToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.показатьОкноToolStripMenuItem.Text = "Расписание (Alt+R)";
            this.показатьОкноToolStripMenuItem.Click += new System.EventHandler(this.ПоказатьОкноToolStripMenuItemClick);
            // 
            // расписаниеПреподавателяToolStripMenuItem
            // 
            this.расписаниеПреподавателяToolStripMenuItem.Image = global::UchOtd.Properties.Resources.teacher;
            this.расписаниеПреподавателяToolStripMenuItem.Name = "расписаниеПреподавателяToolStripMenuItem";
            this.расписаниеПреподавателяToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.расписаниеПреподавателяToolStripMenuItem.Text = "Расписание преподавателя (Alt+T)";
            this.расписаниеПреподавателяToolStripMenuItem.Click += new System.EventHandler(this.РасписаниеПреподавателяToolStripMenuItemClick);
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
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Image = global::UchOtd.Properties.Resources.exit;
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.ВыходToolStripMenuItemClick);
            // 
            // openDBToolStripMenuItem
            // 
            this.openDBToolStripMenuItem.Image = global::UchOtd.Properties.Resources.ODBCKit;
            this.openDBToolStripMenuItem.Name = "openDBToolStripMenuItem";
            this.openDBToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.openDBToolStripMenuItem.Text = "Сменить базу данных";
            this.openDBToolStripMenuItem.Click += new System.EventHandler(this.openDBToolStripMenuItem_Click);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayIconMenu;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Учебный отдел";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(262, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(262, 6);
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 203);
            this.Name = "StartupForm";
            this.Text = "Form1";
            this.trayIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip trayIconMenu;
        private System.Windows.Forms.ToolStripMenuItem измененияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem расписаниеПреподавателяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem показатьОкноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ToolStripMenuItem контингентToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem занятостьАудиторийAltAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заметкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem телефоныToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}

