namespace UchOtd.Forms
{
    partial class TeachersAddLessons
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.teachers = new System.Windows.Forms.DataGridView();
            this.refresh = new System.Windows.Forms.Button();
            this.controlsPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teachers)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.refresh);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(707, 79);
            this.controlsPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.teachers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 491);
            this.panel1.TabIndex = 1;
            // 
            // teachers
            // 
            this.teachers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.teachers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teachers.Location = new System.Drawing.Point(0, 0);
            this.teachers.Name = "teachers";
            this.teachers.Size = new System.Drawing.Size(707, 491);
            this.teachers.TabIndex = 0;
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(12, 12);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(683, 51);
            this.refresh.TabIndex = 0;
            this.refresh.Text = "Обновить";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // TeachersAddLessons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 570);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.controlsPanel);
            this.Name = "TeachersAddLessons";
            this.Text = "Преподаватели, которым необходимо дополнить расписание";
            this.controlsPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teachers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView teachers;
        private System.Windows.Forms.Button refresh;
    }
}