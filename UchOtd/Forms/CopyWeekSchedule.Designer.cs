namespace UchOtd.Forms
{
    partial class CopyWeekSchedule
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
            this.groupList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fromWeek = new System.Windows.Forms.TextBox();
            this.toWeek = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Go = new System.Windows.Forms.Button();
            this.facultyList = new System.Windows.Forms.ComboBox();
            this.copyGroup = new System.Windows.Forms.RadioButton();
            this.copyFaculty = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.status = new System.Windows.Forms.Label();
            this.deleteWeekSchedule = new System.Windows.Forms.Button();
            this.FacultyScheduleFromGroup = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupList
            // 
            this.groupList.FormattingEnabled = true;
            this.groupList.Location = new System.Drawing.Point(101, 12);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(287, 21);
            this.groupList.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Копировать из недели";
            // 
            // fromWeek
            // 
            this.fromWeek.Location = new System.Drawing.Point(139, 83);
            this.fromWeek.Name = "fromWeek";
            this.fromWeek.Size = new System.Drawing.Size(37, 20);
            this.fromWeek.TabIndex = 3;
            // 
            // toWeek
            // 
            this.toWeek.Location = new System.Drawing.Point(351, 83);
            this.toWeek.Name = "toWeek";
            this.toWeek.Size = new System.Drawing.Size(37, 20);
            this.toWeek.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(228, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Копировать в неделю";
            // 
            // Go
            // 
            this.Go.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Go.Location = new System.Drawing.Point(12, 119);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(376, 59);
            this.Go.TabIndex = 6;
            this.Go.Text = "Копировать";
            this.Go.UseVisualStyleBackColor = true;
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // facultyList
            // 
            this.facultyList.FormattingEnabled = true;
            this.facultyList.Location = new System.Drawing.Point(101, 43);
            this.facultyList.Name = "facultyList";
            this.facultyList.Size = new System.Drawing.Size(287, 21);
            this.facultyList.TabIndex = 8;
            // 
            // copyGroup
            // 
            this.copyGroup.AutoSize = true;
            this.copyGroup.Location = new System.Drawing.Point(10, 16);
            this.copyGroup.Name = "copyGroup";
            this.copyGroup.Size = new System.Drawing.Size(60, 17);
            this.copyGroup.TabIndex = 9;
            this.copyGroup.TabStop = true;
            this.copyGroup.Text = "Группа";
            this.copyGroup.UseVisualStyleBackColor = true;
            // 
            // copyFaculty
            // 
            this.copyFaculty.AutoSize = true;
            this.copyFaculty.Location = new System.Drawing.Point(10, 44);
            this.copyFaculty.Name = "copyFaculty";
            this.copyFaculty.Size = new System.Drawing.Size(81, 17);
            this.copyFaculty.TabIndex = 10;
            this.copyFaculty.TabStop = true;
            this.copyFaculty.Text = "Факультет";
            this.copyFaculty.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.status);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 372);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 35);
            this.panel1.TabIndex = 12;
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(7, 10);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(37, 13);
            this.status.TabIndex = 12;
            this.status.Text = "..........";
            // 
            // deleteWeekSchedule
            // 
            this.deleteWeekSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteWeekSchedule.Location = new System.Drawing.Point(12, 184);
            this.deleteWeekSchedule.Name = "deleteWeekSchedule";
            this.deleteWeekSchedule.Size = new System.Drawing.Size(376, 59);
            this.deleteWeekSchedule.TabIndex = 13;
            this.deleteWeekSchedule.Text = "УДАЛИТЬ";
            this.deleteWeekSchedule.UseVisualStyleBackColor = true;
            this.deleteWeekSchedule.Click += new System.EventHandler(this.deleteWeekSchedule_Click);
            // 
            // FacultyScheduleFromGroup
            // 
            this.FacultyScheduleFromGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FacultyScheduleFromGroup.Location = new System.Drawing.Point(12, 249);
            this.FacultyScheduleFromGroup.Name = "FacultyScheduleFromGroup";
            this.FacultyScheduleFromGroup.Size = new System.Drawing.Size(376, 111);
            this.FacultyScheduleFromGroup.TabIndex = 14;
            this.FacultyScheduleFromGroup.Text = "Сформировать расписание факультета из группы";
            this.FacultyScheduleFromGroup.UseVisualStyleBackColor = true;
            this.FacultyScheduleFromGroup.Click += new System.EventHandler(this.FacultyScheduleFromGroup_Click);
            // 
            // CopyWeekSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 407);
            this.Controls.Add(this.FacultyScheduleFromGroup);
            this.Controls.Add(this.deleteWeekSchedule);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.copyFaculty);
            this.Controls.Add(this.copyGroup);
            this.Controls.Add(this.facultyList);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.toWeek);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fromWeek);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupList);
            this.Name = "CopyWeekSchedule";
            this.Text = "Копировать расписание  на день / неделю";
            this.Load += new System.EventHandler(this.CopyWeekSchedule_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox groupList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fromWeek;
        private System.Windows.Forms.TextBox toWeek;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Go;
        private System.Windows.Forms.ComboBox facultyList;
        private System.Windows.Forms.RadioButton copyGroup;
        private System.Windows.Forms.RadioButton copyFaculty;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button deleteWeekSchedule;
        private System.Windows.Forms.Button FacultyScheduleFromGroup;
    }
}