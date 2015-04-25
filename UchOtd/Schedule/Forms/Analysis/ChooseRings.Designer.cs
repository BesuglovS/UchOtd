using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.Analysis
{
    partial class ChooseRings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RingsList = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Cancel = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.MolRings = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 358);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(133, 101);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.RingsList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(133, 358);
            this.panel2.TabIndex = 1;
            // 
            // RingsList
            // 
            this.RingsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RingsList.FormattingEnabled = true;
            this.RingsList.Location = new System.Drawing.Point(0, 0);
            this.RingsList.Name = "RingsList";
            this.RingsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.RingsList.Size = new System.Drawing.Size(133, 358);
            this.RingsList.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.MolRings);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(133, 42);
            this.panel3.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.Cancel);
            this.panel4.Controls.Add(this.OK);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 42);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(133, 59);
            this.panel4.TabIndex = 1;
            // 
            // Cancel
            // 
            this.Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cancel.Location = new System.Drawing.Point(64, 0);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(69, 59);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "Отмена";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // OK
            // 
            this.OK.Dock = System.Windows.Forms.DockStyle.Left;
            this.OK.Location = new System.Drawing.Point(0, 0);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(64, 59);
            this.OK.TabIndex = 2;
            this.OK.Text = "Выбрать";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // MolRings
            // 
            this.MolRings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MolRings.Location = new System.Drawing.Point(0, 0);
            this.MolRings.Name = "MolRings";
            this.MolRings.Size = new System.Drawing.Size(133, 42);
            this.MolRings.TabIndex = 0;
            this.MolRings.Text = "Обычные звонки (80)";
            this.MolRings.UseVisualStyleBackColor = true;
            this.MolRings.Click += new System.EventHandler(this.MolRings_Click);
            // 
            // ChooseRings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(133, 459);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ChooseRings";
            this.Text = "Выбор звонков";
            this.Load += new System.EventHandler(this.ChooseRings_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private ListBox RingsList;
        private Panel panel4;
        private Button Cancel;
        private Button OK;
        private Panel panel3;
        private Button MolRings;

    }
}