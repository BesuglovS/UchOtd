using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Forms
{
    partial class TeacherSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeacherSchedule));
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.OnlyFutureDatesExportInWord = new System.Windows.Forms.CheckBox();
            this.ExportAllTeachersInWord = new System.Windows.Forms.Button();
            this.showProposed = new System.Windows.Forms.CheckBox();
            this.ExportInWordLandscape = new System.Windows.Forms.Button();
            this.ExportInWordPortrait = new System.Windows.Forms.Button();
            this.weekFilter = new System.Windows.Forms.ComboBox();
            this.refresh = new System.Windows.Forms.Button();
            this.weekFiltered = new System.Windows.Forms.CheckBox();
            this.teacherList = new System.Windows.Forms.ComboBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.scheduleView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.semesterList = new System.Windows.Forms.ComboBox();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.semesterList);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Controls.Add(this.OnlyFutureDatesExportInWord);
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
            this.controlsPanel.Size = new System.Drawing.Size(1085, 77);
            this.controlsPanel.TabIndex = 2;
            // 
            // OnlyFutureDatesExportInWord
            // 
            this.OnlyFutureDatesExportInWord.AutoSize = true;
            this.OnlyFutureDatesExportInWord.Location = new System.Drawing.Point(460, 53);
            this.OnlyFutureDatesExportInWord.Name = "OnlyFutureDatesExportInWord";
            this.OnlyFutureDatesExportInWord.Size = new System.Drawing.Size(155, 17);
            this.OnlyFutureDatesExportInWord.TabIndex = 68;
            this.OnlyFutureDatesExportInWord.Text = "только последующие дни";
            this.OnlyFutureDatesExportInWord.UseVisualStyleBackColor = true;
            // 
            // ExportAllTeachersInWord
            // 
            this.ExportAllTeachersInWord.Location = new System.Drawing.Point(973, 10);
            this.ExportAllTeachersInWord.Name = "ExportAllTeachersInWord";
            this.ExportAllTeachersInWord.Size = new System.Drawing.Size(101, 61);
            this.ExportAllTeachersInWord.TabIndex = 40;
            this.ExportAllTeachersInWord.Text = "Word (все преподаватели)";
            this.ExportAllTeachersInWord.UseVisualStyleBackColor = true;
            this.ExportAllTeachersInWord.Click += new System.EventHandler(this.ExportAllTeachersInWord_Click);
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
            this.ExportInWordLandscape.Size = new System.Drawing.Size(90, 61);
            this.ExportInWordLandscape.TabIndex = 38;
            this.ExportInWordLandscape.Text = "Word (ландшафтная)";
            this.ExportInWordLandscape.UseVisualStyleBackColor = true;
            this.ExportInWordLandscape.Click += new System.EventHandler(this.ExportInWordLandscape_Click);
            // 
            // ExportInWordPortrait
            // 
            this.ExportInWordPortrait.Location = new System.Drawing.Point(876, 10);
            this.ExportInWordPortrait.Name = "ExportInWordPortrait";
            this.ExportInWordPortrait.Size = new System.Drawing.Size(87, 61);
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
            this.refresh.Size = new System.Drawing.Size(85, 64);
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
            this.teacherList.Location = new System.Drawing.Point(12, 15);
            this.teacherList.Name = "teacherList";
            this.teacherList.Size = new System.Drawing.Size(438, 21);
            this.teacherList.TabIndex = 0;
            this.teacherList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teacherList_KeyDown);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.scheduleView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 77);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(1085, 603);
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
            this.scheduleView.Size = new System.Drawing.Size(1085, 603);
            this.scheduleView.TabIndex = 0;
            this.scheduleView.TabStop = false;
            this.scheduleView.SelectionChanged += new System.EventHandler(this.ScheduleViewSelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Семестр";
            // 
            // semesterList
            // 
            this.semesterList.FormattingEnabled = true;
            this.semesterList.Location = new System.Drawing.Point(69, 45);
            this.semesterList.Name = "semesterList";
            this.semesterList.Size = new System.Drawing.Size(137, 21);
            this.semesterList.TabIndex = 70;
            // 
            // TeacherSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 680);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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

        private Panel controlsPanel;
        private ComboBox teacherList;
        private Panel viewPanel;
        private DataGridView scheduleView;
        private Button refresh;
        private CheckBox weekFiltered;
        private ComboBox weekFilter;
        private Button ExportInWordPortrait;
        private Button ExportInWordLandscape;
        private CheckBox showProposed;
        private Button ExportAllTeachersInWord;
        private CheckBox OnlyFutureDatesExportInWord;
        private ComboBox semesterList;
        private Label label1;
    }
}