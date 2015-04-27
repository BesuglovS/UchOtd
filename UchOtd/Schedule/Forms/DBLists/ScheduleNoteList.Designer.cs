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
            this.panel1 = new System.Windows.Forms.Panel();
            this.LongGoneText = new System.Windows.Forms.Button();
            this.LateText = new System.Windows.Forms.Button();
            this.notesDateFilter = new System.Windows.Forms.DateTimePicker();
            this.isNotesDateFiltered = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DateFilter = new System.Windows.Forms.DateTimePicker();
            this.isDateFilter = new System.Windows.Forms.CheckBox();
            this.filterLessons = new System.Windows.Forms.Button();
            this.TeacherFilter = new System.Windows.Forms.ComboBox();
            this.IsTeacherFilter = new System.Windows.Forms.CheckBox();
            this.IsLesson = new System.Windows.Forms.CheckBox();
            this.totalLate = new System.Windows.Forms.Label();
            this.totalLateLabel = new System.Windows.Forms.Label();
            this.upload = new System.Windows.Forms.Button();
            this.ShowAll = new System.Windows.Forms.Button();
            this.FilterNotes = new System.Windows.Forms.Button();
            this.FilterText = new System.Windows.Forms.TextBox();
            this.LateCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.LessonsList = new System.Windows.Forms.ComboBox();
            this.NoteText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.NotesView = new System.Windows.Forms.DataGridView();
            this.dateSync = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LateCount)).BeginInit();
            this.ListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NotesView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dateSync);
            this.panel1.Controls.Add(this.LongGoneText);
            this.panel1.Controls.Add(this.LateText);
            this.panel1.Controls.Add(this.notesDateFilter);
            this.panel1.Controls.Add(this.isNotesDateFiltered);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.IsLesson);
            this.panel1.Controls.Add(this.totalLate);
            this.panel1.Controls.Add(this.totalLateLabel);
            this.panel1.Controls.Add(this.upload);
            this.panel1.Controls.Add(this.ShowAll);
            this.panel1.Controls.Add(this.FilterNotes);
            this.panel1.Controls.Add(this.FilterText);
            this.panel1.Controls.Add(this.LateCount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.LessonsList);
            this.panel1.Controls.Add(this.NoteText);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.remove);
            this.panel1.Controls.Add(this.update);
            this.panel1.Controls.Add(this.add);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1124, 285);
            this.panel1.TabIndex = 12;
            // 
            // LongGoneText
            // 
            this.LongGoneText.Location = new System.Drawing.Point(125, 154);
            this.LongGoneText.Name = "LongGoneText";
            this.LongGoneText.Size = new System.Drawing.Size(138, 23);
            this.LongGoneText.TabIndex = 63;
            this.LongGoneText.Text = "Завершение раньше";
            this.LongGoneText.UseVisualStyleBackColor = true;
            this.LongGoneText.Click += new System.EventHandler(this.LongGoneText_Click);
            // 
            // LateText
            // 
            this.LateText.Location = new System.Drawing.Point(12, 154);
            this.LateText.Name = "LateText";
            this.LateText.Size = new System.Drawing.Size(107, 23);
            this.LateText.TabIndex = 62;
            this.LateText.Text = "Опоздание";
            this.LateText.UseVisualStyleBackColor = true;
            this.LateText.Click += new System.EventHandler(this.LateText_Click);
            // 
            // notesDateFilter
            // 
            this.notesDateFilter.Location = new System.Drawing.Point(905, 240);
            this.notesDateFilter.Name = "notesDateFilter";
            this.notesDateFilter.Size = new System.Drawing.Size(200, 20);
            this.notesDateFilter.TabIndex = 61;
            this.notesDateFilter.ValueChanged += new System.EventHandler(this.DateFilter_ValueChanged);
            // 
            // isNotesDateFiltered
            // 
            this.isNotesDateFiltered.AutoSize = true;
            this.isNotesDateFiltered.Location = new System.Drawing.Point(707, 243);
            this.isNotesDateFiltered.Name = "isNotesDateFiltered";
            this.isNotesDateFiltered.Size = new System.Drawing.Size(168, 17);
            this.isNotesDateFiltered.TabIndex = 60;
            this.isNotesDateFiltered.Text = "Фильтровать по дате урока";
            this.isNotesDateFiltered.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DateFilter);
            this.groupBox1.Controls.Add(this.isDateFilter);
            this.groupBox1.Controls.Add(this.filterLessons);
            this.groupBox1.Controls.Add(this.TeacherFilter);
            this.groupBox1.Controls.Add(this.IsTeacherFilter);
            this.groupBox1.Location = new System.Drawing.Point(437, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 119);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтры списка уроков";
            // 
            // DateFilter
            // 
            this.DateFilter.Location = new System.Drawing.Point(206, 70);
            this.DateFilter.Name = "DateFilter";
            this.DateFilter.Size = new System.Drawing.Size(200, 20);
            this.DateFilter.TabIndex = 59;
            this.DateFilter.ValueChanged += new System.EventHandler(this.DateFilter_ValueChanged);
            // 
            // isDateFilter
            // 
            this.isDateFilter.AutoSize = true;
            this.isDateFilter.Location = new System.Drawing.Point(8, 73);
            this.isDateFilter.Name = "isDateFilter";
            this.isDateFilter.Size = new System.Drawing.Size(168, 17);
            this.isDateFilter.TabIndex = 58;
            this.isDateFilter.Text = "Фильтровать по дате урока";
            this.isDateFilter.UseVisualStyleBackColor = true;
            // 
            // filterLessons
            // 
            this.filterLessons.Location = new System.Drawing.Point(558, 29);
            this.filterLessons.Name = "filterLessons";
            this.filterLessons.Size = new System.Drawing.Size(108, 61);
            this.filterLessons.TabIndex = 57;
            this.filterLessons.Text = "Фильтровать";
            this.filterLessons.UseVisualStyleBackColor = true;
            this.filterLessons.Click += new System.EventHandler(this.filterLessons_Click);
            // 
            // TeacherFilter
            // 
            this.TeacherFilter.FormattingEnabled = true;
            this.TeacherFilter.Location = new System.Drawing.Point(206, 29);
            this.TeacherFilter.Name = "TeacherFilter";
            this.TeacherFilter.Size = new System.Drawing.Size(346, 21);
            this.TeacherFilter.TabIndex = 56;
            // 
            // IsTeacherFilter
            // 
            this.IsTeacherFilter.AutoSize = true;
            this.IsTeacherFilter.Location = new System.Drawing.Point(8, 29);
            this.IsTeacherFilter.Name = "IsTeacherFilter";
            this.IsTeacherFilter.Size = new System.Drawing.Size(192, 17);
            this.IsTeacherFilter.TabIndex = 55;
            this.IsTeacherFilter.Text = "Фильтровать по преподавателю";
            this.IsTeacherFilter.UseVisualStyleBackColor = true;
            // 
            // IsLesson
            // 
            this.IsLesson.AutoSize = true;
            this.IsLesson.Checked = true;
            this.IsLesson.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsLesson.Location = new System.Drawing.Point(11, 28);
            this.IsLesson.Name = "IsLesson";
            this.IsLesson.Size = new System.Drawing.Size(107, 17);
            this.IsLesson.TabIndex = 48;
            this.IsLesson.Text = "Учитывать урок";
            this.IsLesson.UseVisualStyleBackColor = true;
            // 
            // totalLate
            // 
            this.totalLate.AutoSize = true;
            this.totalLate.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalLate.Location = new System.Drawing.Point(348, 216);
            this.totalLate.Name = "totalLate";
            this.totalLate.Size = new System.Drawing.Size(0, 51);
            this.totalLate.TabIndex = 47;
            this.totalLate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalLateLabel
            // 
            this.totalLateLabel.AutoSize = true;
            this.totalLateLabel.Location = new System.Drawing.Point(446, 185);
            this.totalLateLabel.Name = "totalLateLabel";
            this.totalLateLabel.Size = new System.Drawing.Size(94, 13);
            this.totalLateLabel.TabIndex = 46;
            this.totalLateLabel.Text = "Итого опазданий";
            // 
            // upload
            // 
            this.upload.Location = new System.Drawing.Point(11, 243);
            this.upload.Name = "upload";
            this.upload.Size = new System.Drawing.Size(227, 23);
            this.upload.TabIndex = 45;
            this.upload.Text = "Загрузить на сайт";
            this.upload.UseVisualStyleBackColor = true;
            this.upload.Click += new System.EventHandler(this.upload_Click);
            // 
            // ShowAll
            // 
            this.ShowAll.Location = new System.Drawing.Point(125, 211);
            this.ShowAll.Name = "ShowAll";
            this.ShowAll.Size = new System.Drawing.Size(113, 23);
            this.ShowAll.TabIndex = 44;
            this.ShowAll.Text = "Показать все";
            this.ShowAll.UseVisualStyleBackColor = true;
            this.ShowAll.Click += new System.EventHandler(this.ShowAll_Click);
            // 
            // FilterNotes
            // 
            this.FilterNotes.Location = new System.Drawing.Point(11, 211);
            this.FilterNotes.Name = "FilterNotes";
            this.FilterNotes.Size = new System.Drawing.Size(108, 23);
            this.FilterNotes.TabIndex = 43;
            this.FilterNotes.Text = "Фильтровать";
            this.FilterNotes.UseVisualStyleBackColor = true;
            this.FilterNotes.Click += new System.EventHandler(this.FilterNotes_Click);
            // 
            // FilterText
            // 
            this.FilterText.Location = new System.Drawing.Point(11, 185);
            this.FilterText.Name = "FilterText";
            this.FilterText.Size = new System.Drawing.Size(227, 20);
            this.FilterText.TabIndex = 42;
            // 
            // LateCount
            // 
            this.LateCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 63F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LateCount.Location = new System.Drawing.Point(269, 74);
            this.LateCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.LateCount.Name = "LateCount";
            this.LateCount.Size = new System.Drawing.Size(161, 103);
            this.LateCount.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Опоздание";
            // 
            // LessonsList
            // 
            this.LessonsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LessonsList.FormattingEnabled = true;
            this.LessonsList.Location = new System.Drawing.Point(134, 28);
            this.LessonsList.Name = "LessonsList";
            this.LessonsList.Size = new System.Drawing.Size(975, 21);
            this.LessonsList.TabIndex = 39;
            this.LessonsList.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.LessonsList_Format);
            // 
            // NoteText
            // 
            this.NoteText.Location = new System.Drawing.Point(11, 74);
            this.NoteText.Multiline = true;
            this.NoteText.Name = "NoteText";
            this.NoteText.Size = new System.Drawing.Size(252, 75);
            this.NoteText.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Урок";
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(244, 243);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 36;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(244, 214);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 35;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(244, 185);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 34;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Текст";
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.NotesView);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(0, 285);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(1124, 427);
            this.ListPanel.TabIndex = 13;
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
            this.NotesView.Size = new System.Drawing.Size(1124, 427);
            this.NotesView.TabIndex = 0;
            this.NotesView.Click += new System.EventHandler(this.NotesView_Click);
            // 
            // dateSync
            // 
            this.dateSync.AutoSize = true;
            this.dateSync.Location = new System.Drawing.Point(708, 217);
            this.dateSync.Name = "dateSync";
            this.dateSync.Size = new System.Drawing.Size(149, 17);
            this.dateSync.TabIndex = 64;
            this.dateSync.Text = "Синхронизировать даты";
            this.dateSync.UseVisualStyleBackColor = true;
            // 
            // ScheduleNoteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 712);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.panel1);
            this.Name = "ScheduleNoteList";
            this.Text = "Заметки к расписанию";
            this.Load += new System.EventHandler(this.ScheduleNoteList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LateCount)).EndInit();
            this.ListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NotesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox IsLesson;
        private System.Windows.Forms.Label totalLate;
        private System.Windows.Forms.Label totalLateLabel;
        private System.Windows.Forms.Button upload;
        private System.Windows.Forms.Button ShowAll;
        private System.Windows.Forms.Button FilterNotes;
        private System.Windows.Forms.TextBox FilterText;
        private System.Windows.Forms.NumericUpDown LateCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox LessonsList;
        private System.Windows.Forms.TextBox NoteText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel ListPanel;
        private System.Windows.Forms.DataGridView NotesView;
        private System.Windows.Forms.DateTimePicker notesDateFilter;
        private System.Windows.Forms.CheckBox isNotesDateFiltered;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker DateFilter;
        private System.Windows.Forms.CheckBox isDateFilter;
        private System.Windows.Forms.Button filterLessons;
        private System.Windows.Forms.ComboBox TeacherFilter;
        private System.Windows.Forms.CheckBox IsTeacherFilter;
        private System.Windows.Forms.Button LongGoneText;
        private System.Windows.Forms.Button LateText;
        private System.Windows.Forms.CheckBox dateSync;

    }
}