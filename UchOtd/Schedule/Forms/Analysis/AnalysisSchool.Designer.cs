using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.Analysis
{
    partial class AnalysisSchool
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
            this.logLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.start = new System.Windows.Forms.Button();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.messages = new System.Windows.Forms.RichTextBox();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.logLevel);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Controls.Add(this.cancel);
            this.controlsPanel.Controls.Add(this.start);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(920, 130);
            this.controlsPanel.TabIndex = 0;
            // 
            // logLevel
            // 
            this.logLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logLevel.FormattingEnabled = true;
            this.logLevel.Location = new System.Drawing.Point(127, 91);
            this.logLevel.Name = "logLevel";
            this.logLevel.Size = new System.Drawing.Size(341, 21);
            this.logLevel.TabIndex = 5;
            this.logLevel.SelectedIndexChanged += new System.EventHandler(this.logLevel_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Уровень логирования";
            // 
            // cancel
            // 
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel.Location = new System.Drawing.Point(474, 15);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(431, 70);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "ОТМЕНА";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // start
            // 
            this.start.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start.Location = new System.Drawing.Point(2, 15);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(466, 70);
            this.start.TabIndex = 2;
            this.start.Text = "ПОЕХАЛИ";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.messages);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 130);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(920, 591);
            this.viewPanel.TabIndex = 1;
            // 
            // messages
            // 
            this.messages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messages.Location = new System.Drawing.Point(0, 0);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(920, 591);
            this.messages.TabIndex = 0;
            this.messages.Text = "";
            // 
            // AnalysisSchool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 721);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "AnalysisSchool";
            this.Text = "Анализ (ШКОЛА)";
            this.Load += new System.EventHandler(this.AnalysisSchool_Load);
            this.Resize += new System.EventHandler(this.AnalysisSchool_Resize);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel controlsPanel;
        private Panel viewPanel;
        private Button cancel;
        private Button start;
        private RichTextBox messages;
        private ComboBox logLevel;
        private Label label1;
    }
}