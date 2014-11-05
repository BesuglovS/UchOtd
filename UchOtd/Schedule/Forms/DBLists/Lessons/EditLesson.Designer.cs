namespace UchOtd.Schedule.Forms.DBLists.Lessons
{
    partial class EditLesson
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
            this.removeLessons = new System.Windows.Forms.Button();
            this.saveChanges = new System.Windows.Forms.Button();
            this.nextTFD = new System.Windows.Forms.Button();
            this.prevTFD = new System.Windows.Forms.Button();
            this.tfdIndex = new System.Windows.Forms.TextBox();
            this.lessonsPanel = new System.Windows.Forms.Panel();
            this.proposedLessons = new System.Windows.Forms.CheckBox();
            this.tfd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.auditoriums = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lessonWeeks = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.acceptLessons = new System.Windows.Forms.Button();
            this.controlsPanel.SuspendLayout();
            this.lessonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.removeLessons);
            this.controlsPanel.Controls.Add(this.saveChanges);
            this.controlsPanel.Controls.Add(this.nextTFD);
            this.controlsPanel.Controls.Add(this.prevTFD);
            this.controlsPanel.Controls.Add(this.tfdIndex);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(608, 49);
            this.controlsPanel.TabIndex = 0;
            // 
            // removeLessons
            // 
            this.removeLessons.Location = new System.Drawing.Point(280, 9);
            this.removeLessons.Name = "removeLessons";
            this.removeLessons.Size = new System.Drawing.Size(107, 23);
            this.removeLessons.TabIndex = 4;
            this.removeLessons.Text = "Удалить уроки";
            this.removeLessons.UseVisualStyleBackColor = true;
            this.removeLessons.Click += new System.EventHandler(this.RemoveLessonsClick);
            // 
            // saveChanges
            // 
            this.saveChanges.Location = new System.Drawing.Point(492, 10);
            this.saveChanges.Name = "saveChanges";
            this.saveChanges.Size = new System.Drawing.Size(104, 23);
            this.saveChanges.TabIndex = 3;
            this.saveChanges.Text = "Сохранить";
            this.saveChanges.UseVisualStyleBackColor = true;
            this.saveChanges.Click += new System.EventHandler(this.SaveChangesClick);
            // 
            // nextTFD
            // 
            this.nextTFD.Location = new System.Drawing.Point(199, 10);
            this.nextTFD.Name = "nextTFD";
            this.nextTFD.Size = new System.Drawing.Size(75, 23);
            this.nextTFD.TabIndex = 2;
            this.nextTFD.Text = "След.";
            this.nextTFD.UseVisualStyleBackColor = true;
            this.nextTFD.Click += new System.EventHandler(this.NextTFDClick);
            // 
            // prevTFD
            // 
            this.prevTFD.Location = new System.Drawing.Point(12, 10);
            this.prevTFD.Name = "prevTFD";
            this.prevTFD.Size = new System.Drawing.Size(75, 23);
            this.prevTFD.TabIndex = 1;
            this.prevTFD.Text = "Пред.";
            this.prevTFD.UseVisualStyleBackColor = true;
            this.prevTFD.Click += new System.EventHandler(this.PrevTFDClick);
            // 
            // tfdIndex
            // 
            this.tfdIndex.Location = new System.Drawing.Point(93, 12);
            this.tfdIndex.Name = "tfdIndex";
            this.tfdIndex.Size = new System.Drawing.Size(100, 20);
            this.tfdIndex.TabIndex = 0;
            // 
            // lessonsPanel
            // 
            this.lessonsPanel.Controls.Add(this.acceptLessons);
            this.lessonsPanel.Controls.Add(this.proposedLessons);
            this.lessonsPanel.Controls.Add(this.tfd);
            this.lessonsPanel.Controls.Add(this.label2);
            this.lessonsPanel.Controls.Add(this.auditoriums);
            this.lessonsPanel.Controls.Add(this.label1);
            this.lessonsPanel.Controls.Add(this.lessonWeeks);
            this.lessonsPanel.Controls.Add(this.label4);
            this.lessonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lessonsPanel.Location = new System.Drawing.Point(0, 49);
            this.lessonsPanel.Name = "lessonsPanel";
            this.lessonsPanel.Size = new System.Drawing.Size(608, 214);
            this.lessonsPanel.TabIndex = 1;
            // 
            // proposedLessons
            // 
            this.proposedLessons.AutoCheck = false;
            this.proposedLessons.AutoSize = true;
            this.proposedLessons.Location = new System.Drawing.Point(268, 14);
            this.proposedLessons.Name = "proposedLessons";
            this.proposedLessons.Size = new System.Drawing.Size(146, 17);
            this.proposedLessons.TabIndex = 18;
            this.proposedLessons.Text = "Неутверждённые уроки";
            this.proposedLessons.UseVisualStyleBackColor = true;
            // 
            // tfd
            // 
            this.tfd.Location = new System.Drawing.Point(12, 40);
            this.tfd.Name = "tfd";
            this.tfd.Size = new System.Drawing.Size(577, 20);
            this.tfd.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Аудитории";
            // 
            // auditoriums
            // 
            this.auditoriums.Location = new System.Drawing.Point(342, 77);
            this.auditoriums.Multiline = true;
            this.auditoriums.Name = "auditoriums";
            this.auditoriums.Size = new System.Drawing.Size(247, 114);
            this.auditoriums.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Дисциплина в карточке учебных поручений";
            // 
            // lessonWeeks
            // 
            this.lessonWeeks.Location = new System.Drawing.Point(107, 77);
            this.lessonWeeks.Name = "lessonWeeks";
            this.lessonWeeks.Size = new System.Drawing.Size(121, 20);
            this.lessonWeeks.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Недели занятий";
            // 
            // acceptLessons
            // 
            this.acceptLessons.Location = new System.Drawing.Point(420, 10);
            this.acceptLessons.Name = "acceptLessons";
            this.acceptLessons.Size = new System.Drawing.Size(75, 23);
            this.acceptLessons.TabIndex = 19;
            this.acceptLessons.Text = "Утвердить";
            this.acceptLessons.UseVisualStyleBackColor = true;
            this.acceptLessons.Click += new System.EventHandler(this.acceptLessons_Click);
            // 
            // EditLesson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 263);
            this.Controls.Add(this.lessonsPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "EditLesson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit lessons";
            this.Load += new System.EventHandler(this.EditLesson_Load);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.lessonsPanel.ResumeLayout(false);
            this.lessonsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Button nextTFD;
        private System.Windows.Forms.Button prevTFD;
        private System.Windows.Forms.TextBox tfdIndex;
        private System.Windows.Forms.Panel lessonsPanel;
        private System.Windows.Forms.Button saveChanges;
        private System.Windows.Forms.TextBox tfd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox auditoriums;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lessonWeeks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button removeLessons;
        private System.Windows.Forms.CheckBox proposedLessons;
        private System.Windows.Forms.Button acceptLessons;
    }
}