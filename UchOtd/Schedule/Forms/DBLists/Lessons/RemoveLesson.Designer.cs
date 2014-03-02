namespace Schedule.Forms.DBLists.Lessons
{
    partial class RemoveLesson
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
            this.topControls = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupName = new System.Windows.Forms.ComboBox();
            this.dayOfWeek = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ring = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lessons = new System.Windows.Forms.Panel();
            this.showLessons = new System.Windows.Forms.Button();
            this.removeAll = new System.Windows.Forms.Button();
            this.lessonsView = new System.Windows.Forms.DataGridView();
            this.topControls.SuspendLayout();
            this.lessons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lessonsView)).BeginInit();
            this.SuspendLayout();
            // 
            // topControls
            // 
            this.topControls.Controls.Add(this.removeAll);
            this.topControls.Controls.Add(this.showLessons);
            this.topControls.Controls.Add(this.ring);
            this.topControls.Controls.Add(this.label3);
            this.topControls.Controls.Add(this.dayOfWeek);
            this.topControls.Controls.Add(this.label2);
            this.topControls.Controls.Add(this.groupName);
            this.topControls.Controls.Add(this.label1);
            this.topControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.topControls.Location = new System.Drawing.Point(0, 0);
            this.topControls.Name = "topControls";
            this.topControls.Size = new System.Drawing.Size(683, 76);
            this.topControls.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Группа";
            // 
            // groupName
            // 
            this.groupName.FormattingEnabled = true;
            this.groupName.Location = new System.Drawing.Point(60, 11);
            this.groupName.Name = "groupName";
            this.groupName.Size = new System.Drawing.Size(140, 21);
            this.groupName.TabIndex = 1;
            // 
            // dayOfWeek
            // 
            this.dayOfWeek.FormattingEnabled = true;
            this.dayOfWeek.Location = new System.Drawing.Point(295, 11);
            this.dayOfWeek.Name = "dayOfWeek";
            this.dayOfWeek.Size = new System.Drawing.Size(140, 21);
            this.dayOfWeek.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "День недели";
            // 
            // ring
            // 
            this.ring.FormattingEnabled = true;
            this.ring.Location = new System.Drawing.Point(498, 11);
            this.ring.Name = "ring";
            this.ring.Size = new System.Drawing.Size(140, 21);
            this.ring.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(452, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Время";
            // 
            // lessons
            // 
            this.lessons.Controls.Add(this.lessonsView);
            this.lessons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lessons.Location = new System.Drawing.Point(0, 76);
            this.lessons.Name = "lessons";
            this.lessons.Size = new System.Drawing.Size(683, 402);
            this.lessons.TabIndex = 1;
            // 
            // showLessons
            // 
            this.showLessons.Location = new System.Drawing.Point(15, 38);
            this.showLessons.Name = "showLessons";
            this.showLessons.Size = new System.Drawing.Size(185, 23);
            this.showLessons.TabIndex = 6;
            this.showLessons.Text = "Показать все уроки";
            this.showLessons.UseVisualStyleBackColor = true;
            this.showLessons.Click += new System.EventHandler(this.showLessons_Click);
            // 
            // removeAll
            // 
            this.removeAll.Location = new System.Drawing.Point(206, 38);
            this.removeAll.Name = "removeAll";
            this.removeAll.Size = new System.Drawing.Size(229, 23);
            this.removeAll.TabIndex = 7;
            this.removeAll.Text = "Удалить все уроки";
            this.removeAll.UseVisualStyleBackColor = true;
            // 
            // lessonsView
            // 
            this.lessonsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lessonsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lessonsView.Location = new System.Drawing.Point(0, 0);
            this.lessonsView.Name = "lessonsView";
            this.lessonsView.Size = new System.Drawing.Size(683, 402);
            this.lessonsView.TabIndex = 0;
            // 
            // RemoveLesson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 478);
            this.Controls.Add(this.lessons);
            this.Controls.Add(this.topControls);
            this.Name = "RemoveLesson";
            this.Text = "Удалить урок(и)";
            this.Load += new System.EventHandler(this.RemoveLesson_Load);
            this.topControls.ResumeLayout(false);
            this.topControls.PerformLayout();
            this.lessons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lessonsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topControls;
        private System.Windows.Forms.Button removeAll;
        private System.Windows.Forms.Button showLessons;
        private System.Windows.Forms.ComboBox ring;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox dayOfWeek;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox groupName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel lessons;
        private System.Windows.Forms.DataGridView lessonsView;
    }
}