using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.DBLists.Lessons
{
    partial class AddLesson
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
            this.BuildingsPanel = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ProposedLesson = new System.Windows.Forms.CheckBox();
            this.filterRings = new System.Windows.Forms.CheckBox();
            this.deselectBuilding = new System.Windows.Forms.Button();
            this.proposedIncluded = new System.Windows.Forms.CheckBox();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Execute
            // 
            this.Execute.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Execute.Location = new System.Drawing.Point(6, 270);
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
            this.Cancel.Location = new System.Drawing.Point(357, 268);
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
            this.isActive.Location = new System.Drawing.Point(6, 222);
            this.isActive.Name = "isActive";
            this.isActive.Size = new System.Drawing.Size(180, 17);
            this.isActive.TabIndex = 25;
            this.isActive.Text = "Активный урок (в расписании)";
            this.isActive.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Дисциплина в карточке учебных поручений";
            // 
            // teacherForDisciplineBox
            // 
            this.teacherForDisciplineBox.FormattingEnabled = true;
            this.teacherForDisciplineBox.Location = new System.Drawing.Point(6, 33);
            this.teacherForDisciplineBox.Name = "teacherForDisciplineBox";
            this.teacherForDisciplineBox.Size = new System.Drawing.Size(574, 21);
            this.teacherForDisciplineBox.TabIndex = 0;
            this.teacherForDisciplineBox.SelectedIndexChanged += new System.EventHandler(this.teacherForDisciplineBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Время занятия";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "День недели";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(354, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Недели занятий";
            // 
            // lessonWeeks
            // 
            this.lessonWeeks.Location = new System.Drawing.Point(357, 83);
            this.lessonWeeks.Name = "lessonWeeks";
            this.lessonWeeks.Size = new System.Drawing.Size(223, 20);
            this.lessonWeeks.TabIndex = 3;
            this.lessonWeeks.TextChanged += new System.EventHandler(this.lessonWeeks_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Public comment";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 371);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Hidden comment";
            // 
            // publicComment
            // 
            this.publicComment.FormattingEnabled = true;
            this.publicComment.Location = new System.Drawing.Point(118, 337);
            this.publicComment.Name = "publicComment";
            this.publicComment.Size = new System.Drawing.Size(466, 21);
            this.publicComment.TabIndex = 7;
            // 
            // hiddenComment
            // 
            this.hiddenComment.FormattingEnabled = true;
            this.hiddenComment.Location = new System.Drawing.Point(118, 368);
            this.hiddenComment.Name = "hiddenComment";
            this.hiddenComment.Size = new System.Drawing.Size(466, 21);
            this.hiddenComment.TabIndex = 8;
            // 
            // auditoriums
            // 
            this.auditoriums.Location = new System.Drawing.Point(357, 109);
            this.auditoriums.Multiline = true;
            this.auditoriums.Name = "auditoriums";
            this.auditoriums.Size = new System.Drawing.Size(223, 118);
            this.auditoriums.TabIndex = 4;
            this.auditoriums.Text = "Ауд. ";
            // 
            // showAuds
            // 
            this.showAuds.Location = new System.Drawing.Point(357, 233);
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
            this.audList.Location = new System.Drawing.Point(225, 83);
            this.audList.Name = "audList";
            this.audList.Size = new System.Drawing.Size(126, 225);
            this.audList.TabIndex = 27;
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(225, 57);
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
            this.ringsListBox.Location = new System.Drawing.Point(138, 83);
            this.ringsListBox.Name = "ringsListBox";
            this.ringsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ringsListBox.Size = new System.Drawing.Size(81, 134);
            this.ringsListBox.TabIndex = 1;
            // 
            // DayOfWeekListBox
            // 
            this.DayOfWeekListBox.FormattingEnabled = true;
            this.DayOfWeekListBox.Location = new System.Drawing.Point(6, 83);
            this.DayOfWeekListBox.Name = "DayOfWeekListBox";
            this.DayOfWeekListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.DayOfWeekListBox.Size = new System.Drawing.Size(126, 134);
            this.DayOfWeekListBox.TabIndex = 29;
            // 
            // BuildingsPanel
            // 
            this.BuildingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.BuildingsPanel.Location = new System.Drawing.Point(0, 0);
            this.BuildingsPanel.Name = "BuildingsPanel";
            this.BuildingsPanel.Size = new System.Drawing.Size(598, 56);
            this.BuildingsPanel.TabIndex = 30;
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.proposedIncluded);
            this.MainPanel.Controls.Add(this.ProposedLesson);
            this.MainPanel.Controls.Add(this.filterRings);
            this.MainPanel.Controls.Add(this.deselectBuilding);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.Execute);
            this.MainPanel.Controls.Add(this.DayOfWeekListBox);
            this.MainPanel.Controls.Add(this.Cancel);
            this.MainPanel.Controls.Add(this.ringsListBox);
            this.MainPanel.Controls.Add(this.isActive);
            this.MainPanel.Controls.Add(this.reset);
            this.MainPanel.Controls.Add(this.teacherForDisciplineBox);
            this.MainPanel.Controls.Add(this.audList);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.showAuds);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.auditoriums);
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.hiddenComment);
            this.MainPanel.Controls.Add(this.lessonWeeks);
            this.MainPanel.Controls.Add(this.publicComment);
            this.MainPanel.Controls.Add(this.label5);
            this.MainPanel.Controls.Add(this.label6);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 56);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(598, 396);
            this.MainPanel.TabIndex = 31;
            // 
            // ProposedLesson
            // 
            this.ProposedLesson.AutoSize = true;
            this.ProposedLesson.Location = new System.Drawing.Point(6, 245);
            this.ProposedLesson.Name = "ProposedLesson";
            this.ProposedLesson.Size = new System.Drawing.Size(186, 17);
            this.ProposedLesson.TabIndex = 32;
            this.ProposedLesson.Text = "Предполагаемый урок (анализ)";
            this.ProposedLesson.UseVisualStyleBackColor = true;
            // 
            // filterRings
            // 
            this.filterRings.AutoSize = true;
            this.filterRings.Location = new System.Drawing.Point(236, 6);
            this.filterRings.Name = "filterRings";
            this.filterRings.Size = new System.Drawing.Size(134, 17);
            this.filterRings.TabIndex = 31;
            this.filterRings.Text = "Фильтровать звонки";
            this.filterRings.UseVisualStyleBackColor = true;
            this.filterRings.CheckedChanged += new System.EventHandler(this.filterRings_CheckedChanged);
            // 
            // deselectBuilding
            // 
            this.deselectBuilding.Location = new System.Drawing.Point(417, 3);
            this.deselectBuilding.Name = "deselectBuilding";
            this.deselectBuilding.Size = new System.Drawing.Size(163, 20);
            this.deselectBuilding.TabIndex = 30;
            this.deselectBuilding.Text = "Снять выделение корпуса";
            this.deselectBuilding.UseVisualStyleBackColor = true;
            this.deselectBuilding.Click += new System.EventHandler(this.deselectBuilding_Click);
            // 
            // proposedIncluded
            // 
            this.proposedIncluded.AutoSize = true;
            this.proposedIncluded.Checked = true;
            this.proposedIncluded.CheckState = System.Windows.Forms.CheckState.Checked;
            this.proposedIncluded.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.proposedIncluded.Location = new System.Drawing.Point(225, 314);
            this.proposedIncluded.Name = "proposedIncluded";
            this.proposedIncluded.Size = new System.Drawing.Size(117, 14);
            this.proposedIncluded.TabIndex = 33;
            this.proposedIncluded.Text = "Учитывая преполагаемые";
            this.proposedIncluded.UseVisualStyleBackColor = true;
            this.proposedIncluded.CheckedChanged += new System.EventHandler(this.proposedIncluded_CheckedChanged);
            // 
            // AddLesson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 452);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.BuildingsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLesson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить урок(и)";
            this.Load += new System.EventHandler(this.AddLesson_Load);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button Execute;
        private Button Cancel;
        private CheckBox isActive;
        private Label label1;
        private ComboBox teacherForDisciplineBox;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox lessonWeeks;
        private Label label5;
        private Label label6;
        private ComboBox publicComment;
        private ComboBox hiddenComment;
        private TextBox auditoriums;
        private Button showAuds;
        private ListBox audList;
        private Button reset;
        private ListBox ringsListBox;
        private ListBox DayOfWeekListBox;
        private Panel BuildingsPanel;
        private Panel MainPanel;
        private Button deselectBuilding;
        private CheckBox filterRings;
        private CheckBox ProposedLesson;
        private CheckBox proposedIncluded;
    }
}