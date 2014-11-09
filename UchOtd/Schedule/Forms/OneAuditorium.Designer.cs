namespace UchOtd.Schedule.Forms
{
    partial class OneAuditorium
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
            this.auditoriumList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.controlsPanel.Controls.Add(this.auditoriumList);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(611, 44);
            this.controlsPanel.TabIndex = 0;
            // 
            // auditoriumList
            // 
            this.auditoriumList.FormattingEnabled = true;
            this.auditoriumList.Location = new System.Drawing.Point(78, 9);
            this.auditoriumList.Name = "auditoriumList";
            this.auditoriumList.Size = new System.Drawing.Size(168, 21);
            this.auditoriumList.TabIndex = 1;
            this.auditoriumList.SelectedIndexChanged += new System.EventHandler(this.auditoriumList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Аудитория";
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.view);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 44);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(611, 439);
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
            this.view.Size = new System.Drawing.Size(611, 439);
            this.view.TabIndex = 0;
            // 
            // showProposed
            // 
            this.showProposed.AutoSize = true;
            this.showProposed.Location = new System.Drawing.Point(252, 11);
            this.showProposed.Name = "showProposed";
            this.showProposed.Size = new System.Drawing.Size(217, 17);
            this.showProposed.TabIndex = 2;
            this.showProposed.Text = "Показывать преполагаемые занятия";
            this.showProposed.UseVisualStyleBackColor = true;
            // 
            // OneAuditorium
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 483);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "OneAuditorium";
            this.Text = "Занятость аудитории";
            this.Load += new System.EventHandler(this.OneAuditorium_Load);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.ComboBox auditoriumList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView view;
        private System.Windows.Forms.CheckBox showProposed;
    }
}