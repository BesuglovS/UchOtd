using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.NUDS
{
    partial class ScheduleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleForm));
            this.teacherSchedule = new System.Windows.Forms.Button();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.changes = new System.Windows.Forms.Button();
            this.tomorrow = new System.Windows.Forms.Button();
            this.today = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.groupList = new System.Windows.Forms.ComboBox();
            this.scheduleView = new System.Windows.Forms.DataGridView();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.loadingLabel = new System.Windows.Forms.Label();
            this.showProposed = new System.Windows.Forms.CheckBox();
            this.controlsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleView)).BeginInit();
            this.viewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // teacherSchedule
            // 
            this.teacherSchedule.Image = global::UchOtd.Properties.Resources.teacher;
            this.teacherSchedule.Location = new System.Drawing.Point(15, 44);
            this.teacherSchedule.Name = "teacherSchedule";
            this.teacherSchedule.Size = new System.Drawing.Size(32, 32);
            this.teacherSchedule.TabIndex = 10;
            this.teacherSchedule.UseVisualStyleBackColor = true;
            this.teacherSchedule.Click += new System.EventHandler(this.TeacherScheduleClick);
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.showProposed);
            this.controlsPanel.Controls.Add(this.teacherSchedule);
            this.controlsPanel.Controls.Add(this.changes);
            this.controlsPanel.Controls.Add(this.tomorrow);
            this.controlsPanel.Controls.Add(this.today);
            this.controlsPanel.Controls.Add(this.label2);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Controls.Add(this.datePicker);
            this.controlsPanel.Controls.Add(this.groupList);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(402, 81);
            this.controlsPanel.TabIndex = 3;
            // 
            // changes
            // 
            this.changes.Image = ((System.Drawing.Image)(resources.GetObject("changes.Image")));
            this.changes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.changes.Location = new System.Drawing.Point(60, 46);
            this.changes.Name = "changes";
            this.changes.Size = new System.Drawing.Size(109, 29);
            this.changes.TabIndex = 8;
            this.changes.Text = "        Изменения";
            this.changes.UseVisualStyleBackColor = true;
            this.changes.Click += new System.EventHandler(this.ChangesClick);
            // 
            // tomorrow
            // 
            this.tomorrow.Location = new System.Drawing.Point(286, 46);
            this.tomorrow.Name = "tomorrow";
            this.tomorrow.Size = new System.Drawing.Size(103, 29);
            this.tomorrow.TabIndex = 5;
            this.tomorrow.Text = "Завтра";
            this.tomorrow.UseVisualStyleBackColor = true;
            this.tomorrow.Click += new System.EventHandler(this.TomorrowClick);
            // 
            // today
            // 
            this.today.Location = new System.Drawing.Point(180, 46);
            this.today.Name = "today";
            this.today.Size = new System.Drawing.Size(100, 29);
            this.today.TabIndex = 4;
            this.today.Text = "Сегодня";
            this.today.UseVisualStyleBackColor = true;
            this.today.Click += new System.EventHandler(this.TodayClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Дата";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Группа";
            // 
            // datePicker
            // 
            this.datePicker.Location = new System.Drawing.Point(216, 5);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(173, 20);
            this.datePicker.TabIndex = 1;
            this.datePicker.Value = new System.DateTime(2013, 9, 2, 0, 0, 0, 0);
            this.datePicker.ValueChanged += new System.EventHandler(this.DatePickerValueChanged);
            // 
            // groupList
            // 
            this.groupList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.groupList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.groupList.FormattingEnabled = true;
            this.groupList.Location = new System.Drawing.Point(60, 14);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(109, 21);
            this.groupList.TabIndex = 0;
            this.groupList.SelectedIndexChanged += new System.EventHandler(this.GroupListSelectedIndexChanged);
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
            this.scheduleView.Size = new System.Drawing.Size(402, 330);
            this.scheduleView.TabIndex = 0;
            this.scheduleView.SelectionChanged += new System.EventHandler(this.ScheduleViewSelectionChanged);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.loadingLabel);
            this.viewPanel.Controls.Add(this.scheduleView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 81);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(402, 330);
            this.viewPanel.TabIndex = 4;
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.loadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadingLabel.Location = new System.Drawing.Point(12, 13);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(376, 74);
            this.loadingLabel.TabIndex = 1;
            this.loadingLabel.Text = "Загрузка ...";
            this.loadingLabel.Visible = false;
            // 
            // showProposed
            // 
            this.showProposed.AutoSize = true;
            this.showProposed.Location = new System.Drawing.Point(198, 26);
            this.showProposed.Name = "showProposed";
            this.showProposed.Size = new System.Drawing.Size(173, 17);
            this.showProposed.TabIndex = 11;
            this.showProposed.Text = "Показывать преполагаемые";
            this.showProposed.UseVisualStyleBackColor = true;
            // 
            // ScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 411);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "ScheduleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Расписание";
            this.Load += new System.EventHandler(this.ScheduleFormLoad);
            this.Resize += new System.EventHandler(this.ScheduleFormResize);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleView)).EndInit();
            this.viewPanel.ResumeLayout(false);
            this.viewPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button teacherSchedule;
        private Panel controlsPanel;
        private Button changes;
        private Button tomorrow;
        private Button today;
        private Label label2;
        private Label label1;
        private DateTimePicker datePicker;
        private ComboBox groupList;
        private DataGridView scheduleView;
        private Panel viewPanel;
        private Label loadingLabel;
        private CheckBox showProposed;
    }
}