namespace UchOtd.Schedule.Forms
{
    partial class ZachDates
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
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.dateFiltered = new System.Windows.Forms.CheckBox();
            this.groupFiltered = new System.Windows.Forms.CheckBox();
            this.filterDate = new System.Windows.Forms.DateTimePicker();
            this.refresh = new System.Windows.Forms.Button();
            this.groupList = new System.Windows.Forms.ComboBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.ZachDatesView = new System.Windows.Forms.DataGridView();
            this.teacherFiltered = new System.Windows.Forms.CheckBox();
            this.teachersList = new System.Windows.Forms.ComboBox();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZachDatesView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.teachersList);
            this.controlsPanel.Controls.Add(this.teacherFiltered);
            this.controlsPanel.Controls.Add(this.dateFiltered);
            this.controlsPanel.Controls.Add(this.groupFiltered);
            this.controlsPanel.Controls.Add(this.filterDate);
            this.controlsPanel.Controls.Add(this.refresh);
            this.controlsPanel.Controls.Add(this.groupList);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(846, 98);
            this.controlsPanel.TabIndex = 0;
            // 
            // dateFiltered
            // 
            this.dateFiltered.AutoSize = true;
            this.dateFiltered.Location = new System.Drawing.Point(258, 27);
            this.dateFiltered.Name = "dateFiltered";
            this.dateFiltered.Size = new System.Drawing.Size(15, 14);
            this.dateFiltered.TabIndex = 5;
            this.dateFiltered.UseVisualStyleBackColor = true;
            // 
            // groupFiltered
            // 
            this.groupFiltered.AutoSize = true;
            this.groupFiltered.Location = new System.Drawing.Point(13, 24);
            this.groupFiltered.Name = "groupFiltered";
            this.groupFiltered.Size = new System.Drawing.Size(61, 17);
            this.groupFiltered.TabIndex = 4;
            this.groupFiltered.Text = "Группа";
            this.groupFiltered.UseVisualStyleBackColor = true;
            // 
            // filterDate
            // 
            this.filterDate.Location = new System.Drawing.Point(279, 23);
            this.filterDate.Name = "filterDate";
            this.filterDate.Size = new System.Drawing.Size(148, 20);
            this.filterDate.TabIndex = 3;
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(433, 20);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(75, 23);
            this.refresh.TabIndex = 2;
            this.refresh.Text = "Обновить";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // groupList
            // 
            this.groupList.FormattingEnabled = true;
            this.groupList.Location = new System.Drawing.Point(80, 22);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(156, 21);
            this.groupList.TabIndex = 1;
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.statusStrip);
            this.viewPanel.Controls.Add(this.ZachDatesView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 98);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(846, 532);
            this.viewPanel.TabIndex = 1;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip.Location = new System.Drawing.Point(0, 510);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(846, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 17);
            // 
            // ZachDatesView
            // 
            this.ZachDatesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ZachDatesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZachDatesView.Location = new System.Drawing.Point(0, 0);
            this.ZachDatesView.Name = "ZachDatesView";
            this.ZachDatesView.Size = new System.Drawing.Size(846, 532);
            this.ZachDatesView.TabIndex = 0;
            // 
            // teacherFiltered
            // 
            this.teacherFiltered.AutoSize = true;
            this.teacherFiltered.Location = new System.Drawing.Point(13, 60);
            this.teacherFiltered.Name = "teacherFiltered";
            this.teacherFiltered.Size = new System.Drawing.Size(105, 17);
            this.teacherFiltered.TabIndex = 6;
            this.teacherFiltered.Text = "Преподаватель";
            this.teacherFiltered.UseVisualStyleBackColor = true;
            // 
            // teachersList
            // 
            this.teachersList.FormattingEnabled = true;
            this.teachersList.Location = new System.Drawing.Point(124, 56);
            this.teachersList.Name = "teachersList";
            this.teachersList.Size = new System.Drawing.Size(384, 21);
            this.teachersList.TabIndex = 7;
            // 
            // ZachDates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 630);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "ZachDates";
            this.Text = "Даты зачётов";
            this.Load += new System.EventHandler(this.ZachDates_Load);
            this.ResizeEnd += new System.EventHandler(this.ZachDates_ResizeEnd);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            this.viewPanel.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZachDatesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.ComboBox groupList;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView ZachDatesView;
        private System.Windows.Forms.CheckBox dateFiltered;
        private System.Windows.Forms.CheckBox groupFiltered;
        private System.Windows.Forms.DateTimePicker filterDate;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ComboBox teachersList;
        private System.Windows.Forms.CheckBox teacherFiltered;
    }
}