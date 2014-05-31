namespace UchOtd.Forms.Session
{
    partial class Session
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
            this.examsView = new System.Windows.Forms.DataGridView();
            this.Auditoriums = new System.Windows.Forms.Button();
            this.RemoveSyncWithSchedule = new System.Windows.Forms.Button();
            this.AddExamsFromSchedule = new System.Windows.Forms.Button();
            this.TeacherSchedule = new System.Windows.Forms.Button();
            this.TeacherList = new System.Windows.Forms.ComboBox();
            this.UpdateView = new System.Windows.Forms.Button();
            this.WordExport = new System.Windows.Forms.Button();
            this.upload = new System.Windows.Forms.Button();
            this.showAll = new System.Windows.Forms.Button();
            this.BigRedButton = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.controlsPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.examsView)).BeginInit();
            this.viewPanel.SuspendLayout();
            this.controlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // examsView
            // 
            this.examsView.AllowUserToAddRows = false;
            this.examsView.AllowUserToDeleteRows = false;
            this.examsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.examsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.examsView.Location = new System.Drawing.Point(0, 0);
            this.examsView.Name = "examsView";
            this.examsView.ReadOnly = true;
            this.examsView.RowHeadersVisible = false;
            this.examsView.Size = new System.Drawing.Size(763, 308);
            this.examsView.TabIndex = 0;
            this.examsView.DoubleClick += new System.EventHandler(this.examsView_DoubleClick);
            // 
            // Auditoriums
            // 
            this.Auditoriums.Location = new System.Drawing.Point(15, 72);
            this.Auditoriums.Name = "Auditoriums";
            this.Auditoriums.Size = new System.Drawing.Size(161, 23);
            this.Auditoriums.TabIndex = 13;
            this.Auditoriums.Text = "Аудитории";
            this.Auditoriums.UseVisualStyleBackColor = true;
            this.Auditoriums.Click += new System.EventHandler(this.Auditoriums_Click);
            // 
            // RemoveSyncWithSchedule
            // 
            this.RemoveSyncWithSchedule.Location = new System.Drawing.Point(537, 71);
            this.RemoveSyncWithSchedule.Name = "RemoveSyncWithSchedule";
            this.RemoveSyncWithSchedule.Size = new System.Drawing.Size(217, 23);
            this.RemoveSyncWithSchedule.TabIndex = 12;
            this.RemoveSyncWithSchedule.Text = "Удалить экзамены по расписанию";
            this.RemoveSyncWithSchedule.UseVisualStyleBackColor = true;
            this.RemoveSyncWithSchedule.Click += new System.EventHandler(this.RemoveSyncWithSchedule_Click);
            // 
            // AddExamsFromSchedule
            // 
            this.AddExamsFromSchedule.Location = new System.Drawing.Point(294, 71);
            this.AddExamsFromSchedule.Name = "AddExamsFromSchedule";
            this.AddExamsFromSchedule.Size = new System.Drawing.Size(237, 23);
            this.AddExamsFromSchedule.TabIndex = 11;
            this.AddExamsFromSchedule.Text = "Добавить экзамены из расписания";
            this.AddExamsFromSchedule.UseVisualStyleBackColor = true;
            this.AddExamsFromSchedule.Click += new System.EventHandler(this.AddExamsFromSchedule_Click);
            // 
            // TeacherSchedule
            // 
            this.TeacherSchedule.Location = new System.Drawing.Point(384, 42);
            this.TeacherSchedule.Name = "TeacherSchedule";
            this.TeacherSchedule.Size = new System.Drawing.Size(293, 23);
            this.TeacherSchedule.TabIndex = 10;
            this.TeacherSchedule.Text = "GO";
            this.TeacherSchedule.UseVisualStyleBackColor = true;
            this.TeacherSchedule.Click += new System.EventHandler(this.TeacherSchedule_Click);
            // 
            // TeacherList
            // 
            this.TeacherList.FormattingEnabled = true;
            this.TeacherList.Location = new System.Drawing.Point(384, 16);
            this.TeacherList.Name = "TeacherList";
            this.TeacherList.Size = new System.Drawing.Size(293, 21);
            this.TeacherList.TabIndex = 9;
            // 
            // UpdateView
            // 
            this.UpdateView.Location = new System.Drawing.Point(182, 16);
            this.UpdateView.Name = "UpdateView";
            this.UpdateView.Size = new System.Drawing.Size(33, 50);
            this.UpdateView.TabIndex = 8;
            this.UpdateView.Text = "GO";
            this.UpdateView.UseVisualStyleBackColor = true;
            this.UpdateView.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // WordExport
            // 
            this.WordExport.Location = new System.Drawing.Point(301, 16);
            this.WordExport.Name = "WordExport";
            this.WordExport.Size = new System.Drawing.Size(77, 47);
            this.WordExport.TabIndex = 5;
            this.WordExport.Text = "Экспорт в Word";
            this.WordExport.UseVisualStyleBackColor = true;
            this.WordExport.Click += new System.EventHandler(this.WordExport_Click);
            // 
            // upload
            // 
            this.upload.Location = new System.Drawing.Point(221, 16);
            this.upload.Name = "upload";
            this.upload.Size = new System.Drawing.Size(74, 49);
            this.upload.TabIndex = 4;
            this.upload.Text = "Загрузить на сайт";
            this.upload.UseVisualStyleBackColor = true;
            this.upload.Click += new System.EventHandler(this.UploadClick);
            // 
            // showAll
            // 
            this.showAll.Location = new System.Drawing.Point(15, 43);
            this.showAll.Name = "showAll";
            this.showAll.Size = new System.Drawing.Size(161, 23);
            this.showAll.TabIndex = 3;
            this.showAll.Text = "Показать все";
            this.showAll.UseVisualStyleBackColor = true;
            this.showAll.Click += new System.EventHandler(this.showAll_Click);
            // 
            // BigRedButton
            // 
            this.BigRedButton.Location = new System.Drawing.Point(683, 16);
            this.BigRedButton.Name = "BigRedButton";
            this.BigRedButton.Size = new System.Drawing.Size(71, 52);
            this.BigRedButton.TabIndex = 2;
            this.BigRedButton.Text = "Big Red Button";
            this.BigRedButton.UseVisualStyleBackColor = true;
            this.BigRedButton.Click += new System.EventHandler(this.BigRedButton_Click);
            // 
            // groupBox
            // 
            this.groupBox.FormattingEnabled = true;
            this.groupBox.Location = new System.Drawing.Point(60, 16);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(116, 21);
            this.groupBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Группа";
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.examsView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 110);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(763, 308);
            this.viewPanel.TabIndex = 3;
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.Auditoriums);
            this.controlsPanel.Controls.Add(this.RemoveSyncWithSchedule);
            this.controlsPanel.Controls.Add(this.AddExamsFromSchedule);
            this.controlsPanel.Controls.Add(this.TeacherSchedule);
            this.controlsPanel.Controls.Add(this.TeacherList);
            this.controlsPanel.Controls.Add(this.UpdateView);
            this.controlsPanel.Controls.Add(this.WordExport);
            this.controlsPanel.Controls.Add(this.upload);
            this.controlsPanel.Controls.Add(this.showAll);
            this.controlsPanel.Controls.Add(this.BigRedButton);
            this.controlsPanel.Controls.Add(this.groupBox);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(763, 110);
            this.controlsPanel.TabIndex = 2;
            // 
            // Session
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 418);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "Session";
            this.Text = "Session";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.examsView)).EndInit();
            this.viewPanel.ResumeLayout(false);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView examsView;
        private System.Windows.Forms.Button Auditoriums;
        private System.Windows.Forms.Button RemoveSyncWithSchedule;
        private System.Windows.Forms.Button AddExamsFromSchedule;
        private System.Windows.Forms.Button TeacherSchedule;
        private System.Windows.Forms.ComboBox TeacherList;
        private System.Windows.Forms.Button UpdateView;
        private System.Windows.Forms.Button WordExport;
        private System.Windows.Forms.Button upload;
        private System.Windows.Forms.Button showAll;
        private System.Windows.Forms.Button BigRedButton;
        private System.Windows.Forms.ComboBox groupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.Panel controlsPanel;
    }
}