namespace UchOtd.Schedule.Forms.DBLists
{
    partial class ScheduleNoteList
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
            this.ListPanel = new System.Windows.Forms.Panel();
            this.NotesView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NoteText = new System.Windows.Forms.TextBox();
            this.LessonsList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LateCount = new System.Windows.Forms.NumericUpDown();
            this.ListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NotesView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LateCount)).BeginInit();
            this.SuspendLayout();
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.NotesView);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ListPanel.Location = new System.Drawing.Point(0, 176);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(1124, 536);
            this.ListPanel.TabIndex = 11;
            // 
            // NotesView
            // 
            this.NotesView.AllowUserToAddRows = false;
            this.NotesView.AllowUserToDeleteRows = false;
            this.NotesView.AllowUserToResizeColumns = false;
            this.NotesView.AllowUserToResizeRows = false;
            this.NotesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NotesView.ColumnHeadersVisible = false;
            this.NotesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotesView.Location = new System.Drawing.Point(0, 0);
            this.NotesView.Name = "NotesView";
            this.NotesView.ReadOnly = true;
            this.NotesView.RowHeadersVisible = false;
            this.NotesView.Size = new System.Drawing.Size(1124, 536);
            this.NotesView.TabIndex = 0;
            this.NotesView.Click += new System.EventHandler(this.NotesView_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Урок";
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(1037, 130);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 16;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(1037, 101);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 15;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(1037, 72);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 14;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Текст";
            // 
            // NoteText
            // 
            this.NoteText.Location = new System.Drawing.Point(12, 65);
            this.NoteText.Multiline = true;
            this.NoteText.Name = "NoteText";
            this.NoteText.Size = new System.Drawing.Size(252, 105);
            this.NoteText.TabIndex = 21;
            // 
            // LessonsList
            // 
            this.LessonsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LessonsList.FormattingEnabled = true;
            this.LessonsList.Location = new System.Drawing.Point(15, 25);
            this.LessonsList.Name = "LessonsList";
            this.LessonsList.Size = new System.Drawing.Size(1097, 21);
            this.LessonsList.TabIndex = 22;
            this.LessonsList.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.LessonsList_Format);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Опоздание";
            // 
            // LateCount
            // 
            this.LateCount.Location = new System.Drawing.Point(270, 65);
            this.LateCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.LateCount.Name = "LateCount";
            this.LateCount.Size = new System.Drawing.Size(252, 20);
            this.LateCount.TabIndex = 25;
            // 
            // ScheduleNoteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 712);
            this.Controls.Add(this.LateCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LessonsList);
            this.Controls.Add(this.NoteText);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.update);
            this.Controls.Add(this.add);
            this.Controls.Add(this.label1);
            this.Name = "ScheduleNoteList";
            this.Text = "Заметки к расписанию";
            this.Load += new System.EventHandler(this.ScheduleNoteList_Load);
            this.ListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NotesView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LateCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ListPanel;
        private System.Windows.Forms.DataGridView NotesView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NoteText;
        private System.Windows.Forms.ComboBox LessonsList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown LateCount;
    }
}