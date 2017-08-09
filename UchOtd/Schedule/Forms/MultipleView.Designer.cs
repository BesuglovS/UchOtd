using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms
{
    partial class MultipleView
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
            this.semesterList = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.showProposed = new System.Windows.Forms.CheckBox();
            this.update = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.group5 = new System.Windows.Forms.ComboBox();
            this.group4 = new System.Windows.Forms.ComboBox();
            this.group3 = new System.Windows.Forms.ComboBox();
            this.group2 = new System.Windows.Forms.ComboBox();
            this.group1 = new System.Windows.Forms.ComboBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.view = new System.Windows.Forms.DataGridView();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.semesterList);
            this.controlsPanel.Controls.Add(this.label6);
            this.controlsPanel.Controls.Add(this.showProposed);
            this.controlsPanel.Controls.Add(this.update);
            this.controlsPanel.Controls.Add(this.label5);
            this.controlsPanel.Controls.Add(this.label4);
            this.controlsPanel.Controls.Add(this.label3);
            this.controlsPanel.Controls.Add(this.label2);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Controls.Add(this.group5);
            this.controlsPanel.Controls.Add(this.group4);
            this.controlsPanel.Controls.Add(this.group3);
            this.controlsPanel.Controls.Add(this.group2);
            this.controlsPanel.Controls.Add(this.group1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(1217, 64);
            this.controlsPanel.TabIndex = 0;
            // 
            // semesterList
            // 
            this.semesterList.FormattingEnabled = true;
            this.semesterList.Location = new System.Drawing.Point(1007, 21);
            this.semesterList.Name = "semesterList";
            this.semesterList.Size = new System.Drawing.Size(121, 21);
            this.semesterList.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(950, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Семестр";
            // 
            // showProposed
            // 
            this.showProposed.AutoSize = true;
            this.showProposed.Location = new System.Drawing.Point(735, 23);
            this.showProposed.Name = "showProposed";
            this.showProposed.Size = new System.Drawing.Size(205, 17);
            this.showProposed.TabIndex = 11;
            this.showProposed.Text = "Показывать преполагаемые уроки";
            this.showProposed.UseVisualStyleBackColor = true;
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(654, 8);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 45);
            this.update.TabIndex = 10;
            this.update.Text = "Update";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(539, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Группа 5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(416, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Группа 4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(286, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Группа 3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(160, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Группа 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(34, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Группа 1";
            // 
            // group5
            // 
            this.group5.FormattingEnabled = true;
            this.group5.Location = new System.Drawing.Point(520, 12);
            this.group5.Name = "group5";
            this.group5.Size = new System.Drawing.Size(121, 21);
            this.group5.TabIndex = 4;
            // 
            // group4
            // 
            this.group4.FormattingEnabled = true;
            this.group4.Location = new System.Drawing.Point(393, 12);
            this.group4.Name = "group4";
            this.group4.Size = new System.Drawing.Size(121, 21);
            this.group4.TabIndex = 3;
            // 
            // group3
            // 
            this.group3.FormattingEnabled = true;
            this.group3.Location = new System.Drawing.Point(266, 12);
            this.group3.Name = "group3";
            this.group3.Size = new System.Drawing.Size(121, 21);
            this.group3.TabIndex = 2;
            // 
            // group2
            // 
            this.group2.FormattingEnabled = true;
            this.group2.Location = new System.Drawing.Point(139, 12);
            this.group2.Name = "group2";
            this.group2.Size = new System.Drawing.Size(121, 21);
            this.group2.TabIndex = 1;
            // 
            // group1
            // 
            this.group1.FormattingEnabled = true;
            this.group1.Location = new System.Drawing.Point(12, 12);
            this.group1.Name = "group1";
            this.group1.Size = new System.Drawing.Size(121, 21);
            this.group1.TabIndex = 0;
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.view);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 64);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(1217, 635);
            this.viewPanel.TabIndex = 1;
            // 
            // view
            // 
            this.view.AllowUserToAddRows = false;
            this.view.AllowUserToDeleteRows = false;
            this.view.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.Location = new System.Drawing.Point(0, 0);
            this.view.Name = "view";
            this.view.ReadOnly = true;
            this.view.RowHeadersVisible = false;
            this.view.Size = new System.Drawing.Size(1217, 635);
            this.view.TabIndex = 0;
            // 
            // MultipleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1217, 699);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "MultipleView";
            this.Text = "MultipleView";
            this.Load += new System.EventHandler(this.MultipleView_Load);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel controlsPanel;
        private Button update;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox group5;
        private ComboBox group4;
        private ComboBox group3;
        private ComboBox group2;
        private ComboBox group1;
        private Panel viewPanel;
        private DataGridView view;
        private CheckBox showProposed;
        private ComboBox semesterList;
        private Label label6;
    }
}