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
            this.showProposed = new System.Windows.Forms.CheckBox();
            this.ExportInWordLandscape = new System.Windows.Forms.Button();
            this.ExportInWordPortrait = new System.Windows.Forms.Button();
            this.weekFilter = new System.Windows.Forms.ComboBox();
            this.refresh = new System.Windows.Forms.Button();
            this.weekFiltered = new System.Windows.Forms.CheckBox();
            this.teacherList = new System.Windows.Forms.ComboBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.scheduleView = new System.Windows.Forms.DataGridView();
            this.ExportAllTeachersInWord = new System.Windows.Forms.Button();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.ExportAllTeachersInWord);
            this.controlsPanel.Controls.Add(this.showProposed);
            this.controlsPanel.Controls.Add(this.ExportInWordLandscape);
            this.controlsPanel.Controls.Add(this.ExportInWordPortrait);
            this.controlsPanel.Controls.Add(this.weekFilter);
            this.controlsPanel.Controls.Add(this.refresh);
            this.controlsPanel.Controls.Add(this.weekFiltered);
            this.controlsPanel.Controls.Add(this.teacherList);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(1085, 56);
            this.controlsPanel.TabIndex = 2;
            // 
            // showProposed
            // 
            this.showProposed.AutoSize = true;
            this.showProposed.Location = new System.Drawing.Point(460, 30);
            this.showProposed.Name = "showProposed";
            this.showProposed.Size = new System.Drawing.Size(217, 17);
            this.showProposed.TabIndex = 39;
            this.showProposed.Text = "Показывать преполагаемые занятия";
            this.showProposed.UseVisualStyleBackColor = true;
            // 
            // ExportInWordLandscape
            // 
            this.ExportInWordLandscape.Location = new System.Drawing.Point(779, 10);
            this.ExportInWordLandscape.Name = "ExportInWordLandscape";
            this.ExportInWordLandscape.Size = new System.Drawing.Size(90, 35);
            this.ExportInWordLandscape.TabIndex = 38;
            this.ExportInWordLandscape.Text = "Word (ландшафтная)";
            this.ExportInWordLandscape.UseVisualStyleBackColor = true;
            this.ExportInWordLandscape.Click += new System.EventHandler(this.ExportInWordLandscape_Click);
            // 
            // ExportInWordPortrait
            // 
            this.ExportInWordPortrait.Location = new System.Drawing.Point(876, 10);
            this.ExportInWordPortrait.Name = "ExportInWordPortrait";
            this.ExportInWordPortrait.Size = new System.Drawing.Size(87, 35);
            this.ExportInWordPortrait.TabIndex = 37;
            this.ExportInWordPortrait.Text = "Word (портретная)";
            this.ExportInWordPortrait.UseVisualStyleBackColor = true;
            this.ExportInWordPortrait.Click += new System.EventHandler(this.WordExport_Click);
            // 
            // weekFilter
            // 
            this.weekFilter.FormattingEnabled = true;
            this.weekFilter.Location = new System.Drawing.Point(581, 6);
            this.weekFilter.Name = "weekFilter";
            this.weekFilter.Size = new System.Drawing.Size(60, 21);
            this.weekFilter.TabIndex = 36;
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(688, 7);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(85, 43);
            this.refresh.TabIndex = 2;
            this.refresh.Text = "Обновить";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // weekFiltered
            // 
            this.weekFiltered.AutoSize = true;
            this.weekFiltered.Location = new System.Drawing.Point(460, 7);
            this.weekFiltered.Name = "weekFiltered";
            this.weekFiltered.Size = new System.Drawing.Size(120, 17);
            this.weekFiltered.TabIndex = 1;
            this.weekFiltered.Text = "Фильтр по неделе";
            this.weekFiltered.UseVisualStyleBackColor = true;
            // 
            // teacherList
            // 
            this.teacherList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.teacherList.FormattingEnabled = true;
            this.teacherList.Location = new System.Drawing.Point(12, 18);
            this.teacherList.Name = "teacherList";
            this.teacherList.Size = new System.Drawing.Size(438, 21);
            this.teacherList.TabIndex = 0;
            this.teacherList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teacherList_KeyDown);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.scheduleView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 56);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(1085, 624);
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
            this.scheduleView.Size = new System.Drawing.Size(1085, 624);
            this.scheduleView.TabIndex = 0;
            this.scheduleView.TabStop = false;
            this.scheduleView.SelectionChanged += new System.EventHandler(this.ScheduleViewSelectionChanged);
            // 
            // ExportAllTeachersInWord
            // 
            this.ExportAllTeachersInWord.Location = new System.Drawing.Point(973, 10);
            this.ExportAllTeachersInWord.Name = "ExportAllTeachersInWord";
            this.ExportAllTeachersInWord.Size = new System.Drawing.Size(101, 35);
            this.ExportAllTeachersInWord.TabIndex = 40;
            this.ExportAllTeachersInWord.Text = "Word (все преподаватели)";
            this.ExportAllTeachersInWord.UseVisualStyleBackColor = true;
            this.ExportAllTeachersInWord.Click += new System.EventHandler(this.ExportAllTeachersInWord_Click);
            // 
            // TeacherSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 680);
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
        private System.Windows.Forms.Button ExportInWordPortrait;
        private System.Windows.Forms.Button ExportInWordLandscape;
        private System.Windows.Forms.CheckBox showProposed;
        private System.Windows.Forms.Button ExportAllTeachersInWord;
    }
}