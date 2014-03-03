namespace Schedule.Forms
{
    partial class teacherHours
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
            this.teachersList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.view = new System.Windows.Forms.DataGridView();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.teachersList);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(618, 49);
            this.controlsPanel.TabIndex = 0;
            // 
            // teachersList
            // 
            this.teachersList.FormattingEnabled = true;
            this.teachersList.Location = new System.Drawing.Point(104, 12);
            this.teachersList.Name = "teachersList";
            this.teachersList.Size = new System.Drawing.Size(502, 21);
            this.teachersList.TabIndex = 1;
            this.teachersList.SelectedIndexChanged += new System.EventHandler(this.teachersList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Преподаватель";
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.view);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 49);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(618, 455);
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
            this.view.Size = new System.Drawing.Size(618, 455);
            this.view.TabIndex = 0;
            this.view.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.view_CellFormatting);
            // 
            // teacherHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 504);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "teacherHours";
            this.Text = "Часы по преподавателю";
            this.Load += new System.EventHandler(this.teacherHours_Load);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.ComboBox teachersList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView view;
    }
}