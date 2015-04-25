﻿using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms
{
    partial class Auditoriums
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
            this.PlusTeacherFIO = new System.Windows.Forms.CheckBox();
            this.ExportInWord = new System.Windows.Forms.CheckBox();
            this.buildingList = new System.Windows.Forms.ComboBox();
            this.oneBuilding = new System.Windows.Forms.CheckBox();
            this.weekNumber = new System.Windows.Forms.NumericUpDown();
            this.oneWeek = new System.Windows.Forms.CheckBox();
            this.Sat = new System.Windows.Forms.Button();
            this.Fri = new System.Windows.Forms.Button();
            this.Thu = new System.Windows.Forms.Button();
            this.Wed = new System.Windows.Forms.Button();
            this.Tue = new System.Windows.Forms.Button();
            this.Mon = new System.Windows.Forms.Button();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.audView = new System.Windows.Forms.DataGridView();
            this.showProposed = new System.Windows.Forms.CheckBox();
            this.controlsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weekNumber)).BeginInit();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.audView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.showProposed);
            this.controlsPanel.Controls.Add(this.PlusTeacherFIO);
            this.controlsPanel.Controls.Add(this.ExportInWord);
            this.controlsPanel.Controls.Add(this.buildingList);
            this.controlsPanel.Controls.Add(this.oneBuilding);
            this.controlsPanel.Controls.Add(this.weekNumber);
            this.controlsPanel.Controls.Add(this.oneWeek);
            this.controlsPanel.Controls.Add(this.Sat);
            this.controlsPanel.Controls.Add(this.Fri);
            this.controlsPanel.Controls.Add(this.Thu);
            this.controlsPanel.Controls.Add(this.Wed);
            this.controlsPanel.Controls.Add(this.Tue);
            this.controlsPanel.Controls.Add(this.Mon);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(870, 76);
            this.controlsPanel.TabIndex = 0;
            // 
            // PlusTeacherFIO
            // 
            this.PlusTeacherFIO.AutoSize = true;
            this.PlusTeacherFIO.Location = new System.Drawing.Point(648, 40);
            this.PlusTeacherFIO.Name = "PlusTeacherFIO";
            this.PlusTeacherFIO.Size = new System.Drawing.Size(205, 17);
            this.PlusTeacherFIO.TabIndex = 11;
            this.PlusTeacherFIO.Text = "+ фамилии преподавателей в Word";
            this.PlusTeacherFIO.UseVisualStyleBackColor = true;
            // 
            // ExportInWord
            // 
            this.ExportInWord.AutoSize = true;
            this.ExportInWord.Location = new System.Drawing.Point(536, 41);
            this.ExportInWord.Name = "ExportInWord";
            this.ExportInWord.Size = new System.Drawing.Size(106, 17);
            this.ExportInWord.TabIndex = 10;
            this.ExportInWord.Text = "Экспорт в Word";
            this.ExportInWord.UseVisualStyleBackColor = true;
            // 
            // buildingList
            // 
            this.buildingList.FormattingEnabled = true;
            this.buildingList.Location = new System.Drawing.Point(273, 39);
            this.buildingList.Name = "buildingList";
            this.buildingList.Size = new System.Drawing.Size(236, 21);
            this.buildingList.TabIndex = 9;
            // 
            // oneBuilding
            // 
            this.oneBuilding.AutoSize = true;
            this.oneBuilding.Location = new System.Drawing.Point(177, 41);
            this.oneBuilding.Name = "oneBuilding";
            this.oneBuilding.Size = new System.Drawing.Size(90, 17);
            this.oneBuilding.TabIndex = 8;
            this.oneBuilding.Text = "Один корпус";
            this.oneBuilding.UseVisualStyleBackColor = true;
            // 
            // weekNumber
            // 
            this.weekNumber.Location = new System.Drawing.Point(109, 39);
            this.weekNumber.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.weekNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.weekNumber.Name = "weekNumber";
            this.weekNumber.Size = new System.Drawing.Size(45, 20);
            this.weekNumber.TabIndex = 7;
            this.weekNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // oneWeek
            // 
            this.oneWeek.AutoSize = true;
            this.oneWeek.Location = new System.Drawing.Point(12, 41);
            this.oneWeek.Name = "oneWeek";
            this.oneWeek.Size = new System.Drawing.Size(91, 17);
            this.oneWeek.TabIndex = 6;
            this.oneWeek.Text = "Одна неделя";
            this.oneWeek.UseVisualStyleBackColor = true;
            // 
            // Sat
            // 
            this.Sat.Location = new System.Drawing.Point(542, 12);
            this.Sat.Name = "Sat";
            this.Sat.Size = new System.Drawing.Size(100, 23);
            this.Sat.TabIndex = 5;
            this.Sat.Text = "Суббота";
            this.Sat.UseVisualStyleBackColor = true;
            this.Sat.Click += new System.EventHandler(this.Sat_Click);
            // 
            // Fri
            // 
            this.Fri.Location = new System.Drawing.Point(436, 12);
            this.Fri.Name = "Fri";
            this.Fri.Size = new System.Drawing.Size(100, 23);
            this.Fri.TabIndex = 4;
            this.Fri.Text = "Пятница";
            this.Fri.UseVisualStyleBackColor = true;
            this.Fri.Click += new System.EventHandler(this.Fri_Click);
            // 
            // Thu
            // 
            this.Thu.Location = new System.Drawing.Point(330, 12);
            this.Thu.Name = "Thu";
            this.Thu.Size = new System.Drawing.Size(100, 23);
            this.Thu.TabIndex = 3;
            this.Thu.Text = "Четверг";
            this.Thu.UseVisualStyleBackColor = true;
            this.Thu.Click += new System.EventHandler(this.Thu_Click);
            // 
            // Wed
            // 
            this.Wed.Location = new System.Drawing.Point(224, 12);
            this.Wed.Name = "Wed";
            this.Wed.Size = new System.Drawing.Size(100, 23);
            this.Wed.TabIndex = 2;
            this.Wed.Text = "Среда";
            this.Wed.UseVisualStyleBackColor = true;
            this.Wed.Click += new System.EventHandler(this.Wed_Click);
            // 
            // Tue
            // 
            this.Tue.Location = new System.Drawing.Point(118, 12);
            this.Tue.Name = "Tue";
            this.Tue.Size = new System.Drawing.Size(100, 23);
            this.Tue.TabIndex = 1;
            this.Tue.Text = "Вторник";
            this.Tue.UseVisualStyleBackColor = true;
            this.Tue.Click += new System.EventHandler(this.Tue_Click);
            // 
            // Mon
            // 
            this.Mon.Location = new System.Drawing.Point(12, 12);
            this.Mon.Name = "Mon";
            this.Mon.Size = new System.Drawing.Size(100, 23);
            this.Mon.TabIndex = 0;
            this.Mon.Text = "Понедельник";
            this.Mon.UseVisualStyleBackColor = true;
            this.Mon.Click += new System.EventHandler(this.Mon_Click);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.audView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 76);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(870, 395);
            this.viewPanel.TabIndex = 1;
            // 
            // audView
            // 
            this.audView.AllowUserToAddRows = false;
            this.audView.AllowUserToDeleteRows = false;
            this.audView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.audView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audView.Location = new System.Drawing.Point(0, 0);
            this.audView.Name = "audView";
            this.audView.ReadOnly = true;
            this.audView.RowHeadersWidth = 20;
            this.audView.Size = new System.Drawing.Size(870, 395);
            this.audView.TabIndex = 0;
            // 
            // showProposed
            // 
            this.showProposed.AutoSize = true;
            this.showProposed.Location = new System.Drawing.Point(648, 12);
            this.showProposed.Name = "showProposed";
            this.showProposed.Size = new System.Drawing.Size(211, 17);
            this.showProposed.TabIndex = 12;
            this.showProposed.Text = "Показывать предполагаемые уроки";
            this.showProposed.UseVisualStyleBackColor = true;
            // 
            // Auditoriums
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 471);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "Auditoriums";
            this.Text = "Аудитории";
            this.Load += new System.EventHandler(this.Auditoriums_Load);
            this.ResizeEnd += new System.EventHandler(this.Auditoriums_ResizeEnd);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weekNumber)).EndInit();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.audView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel controlsPanel;
        private Button Sat;
        private Button Fri;
        private Button Thu;
        private Button Wed;
        private Button Tue;
        private Button Mon;
        private Panel viewPanel;
        private DataGridView audView;
        private NumericUpDown weekNumber;
        private CheckBox oneWeek;
        private ComboBox buildingList;
        private CheckBox oneBuilding;
        private CheckBox ExportInWord;
        private CheckBox PlusTeacherFIO;
        private CheckBox showProposed;
    }
}