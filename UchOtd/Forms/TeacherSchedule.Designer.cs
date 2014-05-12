﻿namespace UchOtd.Forms
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
            this.refresh = new System.Windows.Forms.Button();
            this.teacherList = new System.Windows.Forms.ComboBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.scheduleView = new System.Windows.Forms.DataGridView();
            this.weekFiltered = new System.Windows.Forms.CheckBox();
            this.weekFilter = new System.Windows.Forms.ComboBox();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.weekFilter);
            this.controlsPanel.Controls.Add(this.refresh);
            this.controlsPanel.Controls.Add(this.weekFiltered);
            this.controlsPanel.Controls.Add(this.teacherList);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(750, 50);
            this.controlsPanel.TabIndex = 2;
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(658, 10);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(85, 23);
            this.refresh.TabIndex = 2;
            this.refresh.Text = "Обновить";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
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
            this.viewPanel.Size = new System.Drawing.Size(750, 630);
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
            this.scheduleView.Size = new System.Drawing.Size(750, 630);
            this.scheduleView.TabIndex = 0;
            this.scheduleView.TabStop = false;
            this.scheduleView.SelectionChanged += new System.EventHandler(this.ScheduleViewSelectionChanged);
            // 
            // weekFiltered
            // 
            this.weekFiltered.AutoSize = true;
            this.weekFiltered.Location = new System.Drawing.Point(466, 14);
            this.weekFiltered.Name = "weekFiltered";
            this.weekFiltered.Size = new System.Drawing.Size(120, 17);
            this.weekFiltered.TabIndex = 1;
            this.weekFiltered.Text = "Фильтр по неделе";
            this.weekFiltered.UseVisualStyleBackColor = true;
            // 
            // weekFilter
            // 
            this.weekFilter.FormattingEnabled = true;
            this.weekFilter.Location = new System.Drawing.Point(592, 10);
            this.weekFilter.Name = "weekFilter";
            this.weekFilter.Size = new System.Drawing.Size(60, 21);
            this.weekFilter.TabIndex = 36;
            // 
            // TeacherSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 680);
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
        private System.Windows.Forms.ComboBox teacherList;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView scheduleView;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.CheckBox weekFiltered;
        private System.Windows.Forms.ComboBox weekFilter;
    }
}