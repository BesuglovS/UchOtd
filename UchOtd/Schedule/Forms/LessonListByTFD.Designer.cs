namespace UchOtd.Schedule.Forms
{
    partial class LessonListByTFD
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
            this.tfdBox = new System.Windows.Forms.ComboBox();
            this.viewPanel = new System.Windows.Forms.Panel();
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
            this.controlsPanel.Controls.Add(this.tfdBox);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(949, 46);
            this.controlsPanel.TabIndex = 0;
            // 
            // tfdBox
            // 
            this.tfdBox.FormattingEnabled = true;
            this.tfdBox.Location = new System.Drawing.Point(12, 12);
            this.tfdBox.Name = "tfdBox";
            this.tfdBox.Size = new System.Drawing.Size(649, 21);
            this.tfdBox.TabIndex = 0;
            this.tfdBox.SelectedIndexChanged += new System.EventHandler(this.tfdBox_SelectedIndexChanged);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.view);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 46);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(949, 534);
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
            this.view.RowHeadersVisible = false;
            this.view.Size = new System.Drawing.Size(949, 534);
            this.view.TabIndex = 0;
            // 
            // showProposed
            // 
            this.showProposed.AutoSize = true;
            this.showProposed.Location = new System.Drawing.Point(667, 14);
            this.showProposed.Name = "showProposed";
            this.showProposed.Size = new System.Drawing.Size(223, 17);
            this.showProposed.TabIndex = 1;
            this.showProposed.Text = "Показывать предполагаемые занятия";
            this.showProposed.UseVisualStyleBackColor = true;
            // 
            // LessonListByTFD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 580);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "LessonListByTFD";
            this.Text = "Список пар по TFD";
            this.Load += new System.EventHandler(this.LessonListByTFD_Load);
            this.ResizeEnd += new System.EventHandler(this.LessonListByTFD_ResizeEnd);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.ComboBox tfdBox;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView view;
        private System.Windows.Forms.CheckBox showProposed;
    }
}