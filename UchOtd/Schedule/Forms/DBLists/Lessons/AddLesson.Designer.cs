﻿namespace Schedule.Forms.DBLists.Lessons
{
    partial class AddLesson
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
            this.Execute = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.isActive = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.teacherForDisciplineBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lessonWeeks = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.publicComment = new System.Windows.Forms.ComboBox();
            this.hiddenComment = new System.Windows.Forms.ComboBox();
            this.auditoriums = new System.Windows.Forms.TextBox();
            this.showAuds = new System.Windows.Forms.Button();
            this.audList = new System.Windows.Forms.ListBox();
            this.reset = new System.Windows.Forms.Button();
            this.ringsListBox = new System.Windows.Forms.ListBox();
            this.DayOfWeekListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Execute
            // 
            this.Execute.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Execute.Location = new System.Drawing.Point(15, 246);
            this.Execute.Name = "Execute";
            this.Execute.Size = new System.Drawing.Size(213, 61);
            this.Execute.TabIndex = 5;
            this.Execute.Text = "Добавить";
            this.Execute.UseVisualStyleBackColor = true;
            this.Execute.Click += new System.EventHandler(this.Execute_Click);
            // 
            // Cancel
            // 
            this.Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Cancel.Location = new System.Drawing.Point(366, 244);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(227, 61);
            this.Cancel.TabIndex = 6;
            this.Cancel.Text = "Отмена";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // isActive
            // 
            this.isActive.AutoSize = true;
            this.isActive.Checked = true;
            this.isActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isActive.Location = new System.Drawing.Point(15, 223);
            this.isActive.Name = "isActive";
            this.isActive.Size = new System.Drawing.Size(180, 17);
            this.isActive.TabIndex = 25;
            this.isActive.Text = "Активный урок (в расписании)";
            this.isActive.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Дисциплина в карточке учебных поручений";
            // 
            // teacherForDisciplineBox
            // 
            this.teacherForDisciplineBox.FormattingEnabled = true;
            this.teacherForDisciplineBox.Location = new System.Drawing.Point(15, 34);
            this.teacherForDisciplineBox.Name = "teacherForDisciplineBox";
            this.teacherForDisciplineBox.Size = new System.Drawing.Size(574, 21);
            this.teacherForDisciplineBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Время занятия";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "День недели";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(363, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Недели занятий";
            // 
            // lessonWeeks
            // 
            this.lessonWeeks.Location = new System.Drawing.Point(366, 84);
            this.lessonWeeks.Name = "lessonWeeks";
            this.lessonWeeks.Size = new System.Drawing.Size(223, 20);
            this.lessonWeeks.TabIndex = 3;
            this.lessonWeeks.TextChanged += new System.EventHandler(this.lessonWeeks_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 316);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Public comment";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 347);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Hidden comment";
            // 
            // publicComment
            // 
            this.publicComment.FormattingEnabled = true;
            this.publicComment.Location = new System.Drawing.Point(127, 313);
            this.publicComment.Name = "publicComment";
            this.publicComment.Size = new System.Drawing.Size(466, 21);
            this.publicComment.TabIndex = 7;
            // 
            // hiddenComment
            // 
            this.hiddenComment.FormattingEnabled = true;
            this.hiddenComment.Location = new System.Drawing.Point(127, 344);
            this.hiddenComment.Name = "hiddenComment";
            this.hiddenComment.Size = new System.Drawing.Size(466, 21);
            this.hiddenComment.TabIndex = 8;
            // 
            // auditoriums
            // 
            this.auditoriums.Location = new System.Drawing.Point(366, 110);
            this.auditoriums.Multiline = true;
            this.auditoriums.Name = "auditoriums";
            this.auditoriums.Size = new System.Drawing.Size(223, 90);
            this.auditoriums.TabIndex = 4;
            this.auditoriums.Text = "Ауд. ";
            // 
            // showAuds
            // 
            this.showAuds.Location = new System.Drawing.Point(366, 209);
            this.showAuds.Name = "showAuds";
            this.showAuds.Size = new System.Drawing.Size(223, 23);
            this.showAuds.TabIndex = 26;
            this.showAuds.Text = "Аудитории";
            this.showAuds.UseVisualStyleBackColor = true;
            this.showAuds.Click += new System.EventHandler(this.showAuds_Click);
            // 
            // audList
            // 
            this.audList.FormattingEnabled = true;
            this.audList.Location = new System.Drawing.Point(234, 84);
            this.audList.Name = "audList";
            this.audList.Size = new System.Drawing.Size(126, 225);
            this.audList.TabIndex = 27;
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(234, 58);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(126, 23);
            this.reset.TabIndex = 28;
            this.reset.Text = "Сбросить";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // ringsListBox
            // 
            this.ringsListBox.FormattingEnabled = true;
            this.ringsListBox.Location = new System.Drawing.Point(15, 83);
            this.ringsListBox.Name = "ringsListBox";
            this.ringsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ringsListBox.Size = new System.Drawing.Size(121, 134);
            this.ringsListBox.TabIndex = 1;
            // 
            // DayOfWeekListBox
            // 
            this.DayOfWeekListBox.FormattingEnabled = true;
            this.DayOfWeekListBox.Location = new System.Drawing.Point(142, 83);
            this.DayOfWeekListBox.Name = "DayOfWeekListBox";
            this.DayOfWeekListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.DayOfWeekListBox.Size = new System.Drawing.Size(86, 134);
            this.DayOfWeekListBox.TabIndex = 29;
            // 
            // AddLesson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 374);
            this.Controls.Add(this.DayOfWeekListBox);
            this.Controls.Add(this.ringsListBox);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.audList);
            this.Controls.Add(this.showAuds);
            this.Controls.Add(this.auditoriums);
            this.Controls.Add(this.hiddenComment);
            this.Controls.Add(this.publicComment);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lessonWeeks);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.teacherForDisciplineBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.isActive);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Execute);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLesson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить урок(и)";
            this.Load += new System.EventHandler(this.AddLesson_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Execute;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.CheckBox isActive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox teacherForDisciplineBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lessonWeeks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox publicComment;
        private System.Windows.Forms.ComboBox hiddenComment;
        private System.Windows.Forms.TextBox auditoriums;
        private System.Windows.Forms.Button showAuds;
        private System.Windows.Forms.ListBox audList;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.ListBox ringsListBox;
        private System.Windows.Forms.ListBox DayOfWeekListBox;
    }
}