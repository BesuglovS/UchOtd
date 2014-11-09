namespace UchOtd.Schedule.Forms
{
    partial class AllChanges
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
            this.eventDateFilter = new System.Windows.Forms.DateTimePicker();
            this.eventDateFiltering = new System.Windows.Forms.CheckBox();
            this.lessonDateFilter = new System.Windows.Forms.DateTimePicker();
            this.lessonDateFiltering = new System.Windows.Forms.CheckBox();
            this.UpdateView = new System.Windows.Forms.Button();
            this.teacherFiltering = new System.Windows.Forms.CheckBox();
            this.teacherFilter = new System.Windows.Forms.ComboBox();
            this.tfdFiltering = new System.Windows.Forms.CheckBox();
            this.tfdFilter = new System.Windows.Forms.ComboBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.view = new System.Windows.Forms.DataGridView();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.eventDateFilter);
            this.controlsPanel.Controls.Add(this.eventDateFiltering);
            this.controlsPanel.Controls.Add(this.lessonDateFilter);
            this.controlsPanel.Controls.Add(this.lessonDateFiltering);
            this.controlsPanel.Controls.Add(this.UpdateView);
            this.controlsPanel.Controls.Add(this.teacherFiltering);
            this.controlsPanel.Controls.Add(this.teacherFilter);
            this.controlsPanel.Controls.Add(this.tfdFiltering);
            this.controlsPanel.Controls.Add(this.tfdFilter);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(747, 103);
            this.controlsPanel.TabIndex = 0;
            // 
            // eventDateFilter
            // 
            this.eventDateFilter.Location = new System.Drawing.Point(181, 63);
            this.eventDateFilter.Name = "eventDateFilter";
            this.eventDateFilter.Size = new System.Drawing.Size(141, 20);
            this.eventDateFilter.TabIndex = 9;
            // 
            // eventDateFiltering
            // 
            this.eventDateFiltering.AutoSize = true;
            this.eventDateFiltering.Location = new System.Drawing.Point(12, 66);
            this.eventDateFiltering.Name = "eventDateFiltering";
            this.eventDateFiltering.Size = new System.Drawing.Size(166, 17);
            this.eventDateFiltering.TabIndex = 8;
            this.eventDateFiltering.Text = "Фильтр по дате изменения";
            this.eventDateFiltering.UseVisualStyleBackColor = true;
            // 
            // lessonDateFilter
            // 
            this.lessonDateFilter.Location = new System.Drawing.Point(506, 63);
            this.lessonDateFilter.Name = "lessonDateFilter";
            this.lessonDateFilter.Size = new System.Drawing.Size(141, 20);
            this.lessonDateFilter.TabIndex = 7;
            // 
            // lessonDateFiltering
            // 
            this.lessonDateFiltering.AutoSize = true;
            this.lessonDateFiltering.Location = new System.Drawing.Point(361, 66);
            this.lessonDateFiltering.Name = "lessonDateFiltering";
            this.lessonDateFiltering.Size = new System.Drawing.Size(139, 17);
            this.lessonDateFiltering.TabIndex = 6;
            this.lessonDateFiltering.Text = "Фильтр по дате урока";
            this.lessonDateFiltering.UseVisualStyleBackColor = true;
            // 
            // UpdateView
            // 
            this.UpdateView.Location = new System.Drawing.Point(653, 10);
            this.UpdateView.Name = "UpdateView";
            this.UpdateView.Size = new System.Drawing.Size(82, 73);
            this.UpdateView.TabIndex = 5;
            this.UpdateView.Text = "Обновить";
            this.UpdateView.UseVisualStyleBackColor = true;
            this.UpdateView.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // teacherFiltering
            // 
            this.teacherFiltering.AutoSize = true;
            this.teacherFiltering.Location = new System.Drawing.Point(12, 40);
            this.teacherFiltering.Name = "teacherFiltering";
            this.teacherFiltering.Size = new System.Drawing.Size(163, 17);
            this.teacherFiltering.TabIndex = 4;
            this.teacherFiltering.Text = "Фильтр по преподавателю";
            this.teacherFiltering.UseVisualStyleBackColor = true;
            // 
            // teacherFilter
            // 
            this.teacherFilter.FormattingEnabled = true;
            this.teacherFilter.Location = new System.Drawing.Point(181, 36);
            this.teacherFilter.Name = "teacherFilter";
            this.teacherFilter.Size = new System.Drawing.Size(466, 21);
            this.teacherFilter.TabIndex = 3;
            // 
            // tfdFiltering
            // 
            this.tfdFiltering.AutoSize = true;
            this.tfdFiltering.Location = new System.Drawing.Point(12, 14);
            this.tfdFiltering.Name = "tfdFiltering";
            this.tfdFiltering.Size = new System.Drawing.Size(105, 17);
            this.tfdFiltering.TabIndex = 2;
            this.tfdFiltering.Text = "Фильтр по TFD";
            this.tfdFiltering.UseVisualStyleBackColor = true;
            // 
            // tfdFilter
            // 
            this.tfdFilter.FormattingEnabled = true;
            this.tfdFilter.Location = new System.Drawing.Point(181, 10);
            this.tfdFilter.Name = "tfdFilter";
            this.tfdFilter.Size = new System.Drawing.Size(466, 21);
            this.tfdFilter.TabIndex = 1;
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.view);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 103);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(747, 465);
            this.viewPanel.TabIndex = 1;
            // 
            // view
            // 
            this.view.AllowUserToAddRows = false;
            this.view.AllowUserToDeleteRows = false;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.Location = new System.Drawing.Point(0, 0);
            this.view.Name = "view";
            this.view.ReadOnly = true;
            this.view.Size = new System.Drawing.Size(747, 465);
            this.view.TabIndex = 0;
            // 
            // AllChanges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 568);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "AllChanges";
            this.Text = "Все изменения";
            this.Load += new System.EventHandler(this.AllChanges_Load);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView view;
        private System.Windows.Forms.CheckBox teacherFiltering;
        private System.Windows.Forms.ComboBox teacherFilter;
        private System.Windows.Forms.CheckBox tfdFiltering;
        private System.Windows.Forms.ComboBox tfdFilter;
        private System.Windows.Forms.Button UpdateView;
        private System.Windows.Forms.DateTimePicker lessonDateFilter;
        private System.Windows.Forms.CheckBox lessonDateFiltering;
        private System.Windows.Forms.DateTimePicker eventDateFilter;
        private System.Windows.Forms.CheckBox eventDateFiltering;
    }
}