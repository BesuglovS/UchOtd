namespace UchOtd.Forms
{
    partial class TeacherSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeacherSchedule));
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.currentWeek = new System.Windows.Forms.CheckBox();
            this.teacherList = new System.Windows.Forms.ComboBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.scheduleView = new System.Windows.Forms.DataGridView();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.currentWeek);
            this.controlsPanel.Controls.Add(this.teacherList);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(718, 50);
            this.controlsPanel.TabIndex = 2;
            // 
            // currentWeek
            // 
            this.currentWeek.AutoSize = true;
            this.currentWeek.Location = new System.Drawing.Point(466, 14);
            this.currentWeek.Name = "currentWeek";
            this.currentWeek.Size = new System.Drawing.Size(110, 17);
            this.currentWeek.TabIndex = 1;
            this.currentWeek.Text = "Текущая неделя";
            this.currentWeek.UseVisualStyleBackColor = true;
            this.currentWeek.CheckedChanged += new System.EventHandler(this.CurrentWeekCheckedChanged);
            // 
            // teacherList
            // 
            this.teacherList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.teacherList.FormattingEnabled = true;
            this.teacherList.Location = new System.Drawing.Point(12, 12);
            this.teacherList.Name = "teacherList";
            this.teacherList.Size = new System.Drawing.Size(438, 21);
            this.teacherList.TabIndex = 0;
            this.teacherList.SelectedIndexChanged += new System.EventHandler(this.TeacherListSelectedIndexChanged);
            this.teacherList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teacherList_KeyDown);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.scheduleView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 50);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(718, 630);
            this.viewPanel.TabIndex = 4;
            // 
            // scheduleView
            // 
            this.scheduleView.AllowUserToAddRows = false;
            this.scheduleView.AllowUserToDeleteRows = false;
            this.scheduleView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.scheduleView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scheduleView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduleView.Location = new System.Drawing.Point(0, 0);
            this.scheduleView.Name = "scheduleView";
            this.scheduleView.ReadOnly = true;
            this.scheduleView.Size = new System.Drawing.Size(718, 630);
            this.scheduleView.TabIndex = 0;
            this.scheduleView.TabStop = false;
            this.scheduleView.SelectionChanged += new System.EventHandler(this.ScheduleViewSelectionChanged);
            // 
            // TeacherSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 680);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(478, 600);
            this.Name = "TeacherSchedule";
            this.Text = "Расписание преподавателя";
            this.Load += new System.EventHandler(this.TeacherScheduleLoad);
            this.Resize += new System.EventHandler(this.TeacherScheduleResize);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scheduleView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.CheckBox currentWeek;
        private System.Windows.Forms.ComboBox teacherList;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView scheduleView;
    }
}