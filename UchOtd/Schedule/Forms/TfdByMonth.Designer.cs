namespace UchOtd.Schedule.Forms
{
    partial class TfdByMonth
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
            this.refresh = new System.Windows.Forms.Button();
            this.TeacherFIOFilter = new System.Windows.Forms.ComboBox();
            this.filteredByTeacherFIO = new System.Windows.Forms.CheckBox();
            this.DisciplineNameFilter = new System.Windows.Forms.TextBox();
            this.filteredByDisciplineName = new System.Windows.Forms.CheckBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.ByMonthView = new System.Windows.Forms.DataGridView();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ByMonthView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.refresh);
            this.controlsPanel.Controls.Add(this.TeacherFIOFilter);
            this.controlsPanel.Controls.Add(this.filteredByTeacherFIO);
            this.controlsPanel.Controls.Add(this.DisciplineNameFilter);
            this.controlsPanel.Controls.Add(this.filteredByDisciplineName);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(887, 85);
            this.controlsPanel.TabIndex = 0;
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(497, 9);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(75, 48);
            this.refresh.TabIndex = 4;
            this.refresh.Text = "Обновить";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // TeacherFIOFilter
            // 
            this.TeacherFIOFilter.FormattingEnabled = true;
            this.TeacherFIOFilter.Location = new System.Drawing.Point(152, 36);
            this.TeacherFIOFilter.Name = "TeacherFIOFilter";
            this.TeacherFIOFilter.Size = new System.Drawing.Size(339, 21);
            this.TeacherFIOFilter.TabIndex = 3;
            // 
            // filteredByTeacherFIO
            // 
            this.filteredByTeacherFIO.AutoSize = true;
            this.filteredByTeacherFIO.Location = new System.Drawing.Point(12, 38);
            this.filteredByTeacherFIO.Name = "filteredByTeacherFIO";
            this.filteredByTeacherFIO.Size = new System.Drawing.Size(133, 17);
            this.filteredByTeacherFIO.TabIndex = 2;
            this.filteredByTeacherFIO.Text = "ФИО преподавателя";
            this.filteredByTeacherFIO.UseVisualStyleBackColor = true;
            // 
            // DisciplineNameFilter
            // 
            this.DisciplineNameFilter.Location = new System.Drawing.Point(152, 9);
            this.DisciplineNameFilter.Name = "DisciplineNameFilter";
            this.DisciplineNameFilter.Size = new System.Drawing.Size(339, 20);
            this.DisciplineNameFilter.TabIndex = 1;
            // 
            // filteredByDisciplineName
            // 
            this.filteredByDisciplineName.AutoSize = true;
            this.filteredByDisciplineName.Location = new System.Drawing.Point(12, 12);
            this.filteredByDisciplineName.Name = "filteredByDisciplineName";
            this.filteredByDisciplineName.Size = new System.Drawing.Size(113, 17);
            this.filteredByDisciplineName.TabIndex = 0;
            this.filteredByDisciplineName.Text = "Имя дисциплины";
            this.filteredByDisciplineName.UseVisualStyleBackColor = true;
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.ByMonthView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 85);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(887, 521);
            this.viewPanel.TabIndex = 1;
            // 
            // ByMonthView
            // 
            this.ByMonthView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ByMonthView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ByMonthView.Location = new System.Drawing.Point(0, 0);
            this.ByMonthView.Name = "ByMonthView";
            this.ByMonthView.ReadOnly = true;
            this.ByMonthView.Size = new System.Drawing.Size(887, 521);
            this.ByMonthView.TabIndex = 0;
            // 
            // TfdByMonth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 606);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "TfdByMonth";
            this.Text = "Занятия по месяцам";
            this.Load += new System.EventHandler(this.TfdByMonth_Load);
            this.ResizeEnd += new System.EventHandler(this.TfdByMonth_ResizeEnd);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ByMonthView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.ComboBox TeacherFIOFilter;
        private System.Windows.Forms.CheckBox filteredByTeacherFIO;
        private System.Windows.Forms.TextBox DisciplineNameFilter;
        private System.Windows.Forms.CheckBox filteredByDisciplineName;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView ByMonthView;
    }
}