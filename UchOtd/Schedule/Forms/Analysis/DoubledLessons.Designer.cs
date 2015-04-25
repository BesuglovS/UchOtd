using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.Analysis
{
    partial class DoubledLessons
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
            this.labelsPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Exclude = new System.Windows.Forms.Button();
            this.Include = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.allDiscsList = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.NotTheLastOneLessonDiscsList = new System.Windows.Forms.ListBox();
            this.labelsPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelsPanel
            // 
            this.labelsPanel.Controls.Add(this.label2);
            this.labelsPanel.Controls.Add(this.label1);
            this.labelsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelsPanel.Location = new System.Drawing.Point(0, 0);
            this.labelsPanel.Name = "labelsPanel";
            this.labelsPanel.Size = new System.Drawing.Size(852, 69);
            this.labelsPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(465, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(371, 44);
            this.label2.TabIndex = 1;
            this.label2.Text = "Лучше по 2 занятия";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(22, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(348, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Общий список";
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.panel3);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Controls.Add(this.panel2);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 69);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(852, 648);
            this.MainPanel.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Exclude);
            this.panel3.Controls.Add(this.Include);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(400, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(52, 648);
            this.panel3.TabIndex = 5;
            // 
            // Exclude
            // 
            this.Exclude.Location = new System.Drawing.Point(6, 238);
            this.Exclude.Name = "Exclude";
            this.Exclude.Size = new System.Drawing.Size(40, 36);
            this.Exclude.TabIndex = 1;
            this.Exclude.Text = "<<";
            this.Exclude.UseVisualStyleBackColor = true;
            this.Exclude.Click += new System.EventHandler(this.Exclude_Click);
            // 
            // Include
            // 
            this.Include.Location = new System.Drawing.Point(6, 196);
            this.Include.Name = "Include";
            this.Include.Size = new System.Drawing.Size(40, 36);
            this.Include.TabIndex = 0;
            this.Include.Text = ">>";
            this.Include.UseVisualStyleBackColor = true;
            this.Include.Click += new System.EventHandler(this.Include_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.allDiscsList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 648);
            this.panel1.TabIndex = 3;
            // 
            // allDiscsList
            // 
            this.allDiscsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allDiscsList.FormattingEnabled = true;
            this.allDiscsList.Location = new System.Drawing.Point(0, 0);
            this.allDiscsList.Name = "allDiscsList";
            this.allDiscsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.allDiscsList.Size = new System.Drawing.Size(400, 648);
            this.allDiscsList.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.NotTheLastOneLessonDiscsList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(452, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 648);
            this.panel2.TabIndex = 4;
            // 
            // NotTheLastOneLessonDiscsList
            // 
            this.NotTheLastOneLessonDiscsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotTheLastOneLessonDiscsList.FormattingEnabled = true;
            this.NotTheLastOneLessonDiscsList.Location = new System.Drawing.Point(0, 0);
            this.NotTheLastOneLessonDiscsList.Name = "NotTheLastOneLessonDiscsList";
            this.NotTheLastOneLessonDiscsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.NotTheLastOneLessonDiscsList.Size = new System.Drawing.Size(400, 648);
            this.NotTheLastOneLessonDiscsList.TabIndex = 0;
            // 
            // DoubledLessons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 717);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.labelsPanel);
            this.Name = "DoubledLessons";
            this.Text = "Дисциплины, которые лучше ставить по 2 урока";
            this.Load += new System.EventHandler(this.LastLesson_Load);
            this.labelsPanel.ResumeLayout(false);
            this.labelsPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel labelsPanel;
        private Label label2;
        private Label label1;
        private Panel MainPanel;
        private Panel panel3;
        private Button Exclude;
        private Button Include;
        private Panel panel1;
        private ListBox allDiscsList;
        private Panel panel2;
        private ListBox NotTheLastOneLessonDiscsList;

    }
}