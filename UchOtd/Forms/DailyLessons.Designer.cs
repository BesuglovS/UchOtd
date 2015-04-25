using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Forms
{
    partial class DailyLessons
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
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.studentGroupsFiltered = new System.Windows.Forms.CheckBox();
            this.studentGroupList = new System.Windows.Forms.ListBox();
            this.refresh = new System.Windows.Forms.Button();
            this.facultyFilter = new System.Windows.Forms.ComboBox();
            this.facultyFiltered = new System.Windows.Forms.CheckBox();
            this.lessonsDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.loadingLabel = new System.Windows.Forms.Label();
            this.view = new System.Windows.Forms.DataGridView();
            this.showProposed = new System.Windows.Forms.CheckBox();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.showProposed);
            this.controlsPanel.Controls.Add(this.studentGroupsFiltered);
            this.controlsPanel.Controls.Add(this.studentGroupList);
            this.controlsPanel.Controls.Add(this.refresh);
            this.controlsPanel.Controls.Add(this.facultyFilter);
            this.controlsPanel.Controls.Add(this.facultyFiltered);
            this.controlsPanel.Controls.Add(this.lessonsDate);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(881, 108);
            this.controlsPanel.TabIndex = 0;
            // 
            // studentGroupsFiltered
            // 
            this.studentGroupsFiltered.AutoSize = true;
            this.studentGroupsFiltered.Location = new System.Drawing.Point(437, 5);
            this.studentGroupsFiltered.Name = "studentGroupsFiltered";
            this.studentGroupsFiltered.Size = new System.Drawing.Size(180, 17);
            this.studentGroupsFiltered.TabIndex = 10;
            this.studentGroupsFiltered.Text = "Фильтр по группам студентов";
            this.studentGroupsFiltered.UseVisualStyleBackColor = true;
            // 
            // studentGroupList
            // 
            this.studentGroupList.ColumnWidth = 35;
            this.studentGroupList.FormattingEnabled = true;
            this.studentGroupList.Location = new System.Drawing.Point(251, 3);
            this.studentGroupList.MultiColumn = true;
            this.studentGroupList.Name = "studentGroupList";
            this.studentGroupList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.studentGroupList.Size = new System.Drawing.Size(180, 95);
            this.studentGroupList.TabIndex = 9;
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(437, 51);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(180, 47);
            this.refresh.TabIndex = 8;
            this.refresh.Text = "Обновить";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // facultyFilter
            // 
            this.facultyFilter.FormattingEnabled = true;
            this.facultyFilter.Location = new System.Drawing.Point(12, 61);
            this.facultyFilter.Name = "facultyFilter";
            this.facultyFilter.Size = new System.Drawing.Size(204, 21);
            this.facultyFilter.TabIndex = 7;
            // 
            // facultyFiltered
            // 
            this.facultyFiltered.AutoSize = true;
            this.facultyFiltered.Location = new System.Drawing.Point(12, 38);
            this.facultyFiltered.Name = "facultyFiltered";
            this.facultyFiltered.Size = new System.Drawing.Size(142, 17);
            this.facultyFiltered.TabIndex = 6;
            this.facultyFiltered.Text = "Фильтр по факультету";
            this.facultyFiltered.UseVisualStyleBackColor = true;
            // 
            // lessonsDate
            // 
            this.lessonsDate.Location = new System.Drawing.Point(45, 12);
            this.lessonsDate.Name = "lessonsDate";
            this.lessonsDate.Size = new System.Drawing.Size(200, 20);
            this.lessonsDate.TabIndex = 5;
            this.lessonsDate.Value = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Дата";
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.loadingLabel);
            this.viewPanel.Controls.Add(this.view);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 108);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(881, 597);
            this.viewPanel.TabIndex = 1;
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.loadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadingLabel.Location = new System.Drawing.Point(12, 17);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(376, 74);
            this.loadingLabel.TabIndex = 4;
            this.loadingLabel.Text = "Загрузка ...";
            this.loadingLabel.Visible = false;
            // 
            // view
            // 
            this.view.AllowUserToAddRows = false;
            this.view.AllowUserToDeleteRows = false;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.ColumnHeadersVisible = false;
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.Location = new System.Drawing.Point(0, 0);
            this.view.Name = "view";
            this.view.ReadOnly = true;
            this.view.RowHeadersVisible = false;
            this.view.Size = new System.Drawing.Size(881, 597);
            this.view.TabIndex = 0;
            this.view.SelectionChanged += new System.EventHandler(this.view_SelectionChanged);
            // 
            // showProposed
            // 
            this.showProposed.AutoSize = true;
            this.showProposed.Location = new System.Drawing.Point(437, 28);
            this.showProposed.Name = "showProposed";
            this.showProposed.Size = new System.Drawing.Size(223, 17);
            this.showProposed.TabIndex = 11;
            this.showProposed.Text = "Показывать предполагаемые занятия";
            this.showProposed.UseVisualStyleBackColor = true;
            // 
            // DailyLessons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 705);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "DailyLessons";
            this.Text = "Расписание на день";
            this.Load += new System.EventHandler(this.DaylyLessons_Load);
            this.SizeChanged += new System.EventHandler(this.DailyLessons_ResizeEnd);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            this.viewPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel controlsPanel;
        private Panel viewPanel;
        private ComboBox facultyFilter;
        private CheckBox facultyFiltered;
        private DateTimePicker lessonsDate;
        private Label label1;
        private Button refresh;
        private DataGridView view;
        private Label loadingLabel;
        private ListBox studentGroupList;
        private CheckBox studentGroupsFiltered;
        private CheckBox showProposed;
    }
}