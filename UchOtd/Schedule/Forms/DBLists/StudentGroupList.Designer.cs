using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.DBLists
{
    partial class StudentGroupList
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
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.GroupListPanel = new System.Windows.Forms.Panel();
            this.StudentGroupListView = new System.Windows.Forms.DataGridView();
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.PeriodEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.PeriodStart = new System.Windows.Forms.DateTimePicker();
            this.refresh = new System.Windows.Forms.Button();
            this.semesterFiltered = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.semesterList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.addFromGroup = new System.Windows.Forms.Button();
            this.groupsList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StudentList = new System.Windows.Forms.ComboBox();
            this.removeStudentFrunGroup = new System.Windows.Forms.Button();
            this.addStudentToGroup = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.StudentGroupName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StudentListPanel = new System.Windows.Forms.Panel();
            this.StudentsInGroupListView = new System.Windows.Forms.DataGridView();
            this.updateSig = new System.Windows.Forms.Button();
            this.LeftPanel.SuspendLayout();
            this.GroupListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StudentGroupListView)).BeginInit();
            this.ControlsPanel.SuspendLayout();
            this.StudentListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StudentsInGroupListView)).BeginInit();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.GroupListPanel);
            this.LeftPanel.Controls.Add(this.ControlsPanel);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(281, 710);
            this.LeftPanel.TabIndex = 16;
            // 
            // GroupListPanel
            // 
            this.GroupListPanel.Controls.Add(this.StudentGroupListView);
            this.GroupListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupListPanel.Location = new System.Drawing.Point(0, 454);
            this.GroupListPanel.Name = "GroupListPanel";
            this.GroupListPanel.Size = new System.Drawing.Size(281, 256);
            this.GroupListPanel.TabIndex = 23;
            // 
            // StudentGroupListView
            // 
            this.StudentGroupListView.AllowUserToAddRows = false;
            this.StudentGroupListView.AllowUserToDeleteRows = false;
            this.StudentGroupListView.AllowUserToResizeColumns = false;
            this.StudentGroupListView.AllowUserToResizeRows = false;
            this.StudentGroupListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StudentGroupListView.ColumnHeadersVisible = false;
            this.StudentGroupListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StudentGroupListView.Location = new System.Drawing.Point(0, 0);
            this.StudentGroupListView.Name = "StudentGroupListView";
            this.StudentGroupListView.ReadOnly = true;
            this.StudentGroupListView.RowHeadersVisible = false;
            this.StudentGroupListView.Size = new System.Drawing.Size(281, 256);
            this.StudentGroupListView.TabIndex = 1;
            this.StudentGroupListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StudentGroupListView_CellClick);
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Controls.Add(this.updateSig);
            this.ControlsPanel.Controls.Add(this.label5);
            this.ControlsPanel.Controls.Add(this.PeriodEnd);
            this.ControlsPanel.Controls.Add(this.label4);
            this.ControlsPanel.Controls.Add(this.PeriodStart);
            this.ControlsPanel.Controls.Add(this.refresh);
            this.ControlsPanel.Controls.Add(this.semesterFiltered);
            this.ControlsPanel.Controls.Add(this.comboBox1);
            this.ControlsPanel.Controls.Add(this.semesterList);
            this.ControlsPanel.Controls.Add(this.label3);
            this.ControlsPanel.Controls.Add(this.addFromGroup);
            this.ControlsPanel.Controls.Add(this.groupsList);
            this.ControlsPanel.Controls.Add(this.label2);
            this.ControlsPanel.Controls.Add(this.StudentList);
            this.ControlsPanel.Controls.Add(this.removeStudentFrunGroup);
            this.ControlsPanel.Controls.Add(this.addStudentToGroup);
            this.ControlsPanel.Controls.Add(this.remove);
            this.ControlsPanel.Controls.Add(this.update);
            this.ControlsPanel.Controls.Add(this.add);
            this.ControlsPanel.Controls.Add(this.StudentGroupName);
            this.ControlsPanel.Controls.Add(this.label1);
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ControlsPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(281, 454);
            this.ControlsPanel.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 339);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "Конец периода";
            // 
            // PeriodEnd
            // 
            this.PeriodEnd.Location = new System.Drawing.Point(16, 355);
            this.PeriodEnd.Name = "PeriodEnd";
            this.PeriodEnd.Size = new System.Drawing.Size(200, 20);
            this.PeriodEnd.TabIndex = 43;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Начало периода";
            // 
            // PeriodStart
            // 
            this.PeriodStart.Location = new System.Drawing.Point(16, 310);
            this.PeriodStart.Name = "PeriodStart";
            this.PeriodStart.Size = new System.Drawing.Size(200, 20);
            this.PeriodStart.TabIndex = 41;
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(153, 389);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(100, 23);
            this.refresh.TabIndex = 40;
            this.refresh.Text = "Обновить";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // semesterFiltered
            // 
            this.semesterFiltered.AutoSize = true;
            this.semesterFiltered.Location = new System.Drawing.Point(15, 393);
            this.semesterFiltered.Name = "semesterFiltered";
            this.semesterFiltered.Size = new System.Drawing.Size(132, 17);
            this.semesterFiltered.TabIndex = 39;
            this.semesterFiltered.Text = "Фильтр по семестру";
            this.semesterFiltered.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(16, 416);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(238, 21);
            this.comboBox1.TabIndex = 38;
            // 
            // semesterList
            // 
            this.semesterList.FormattingEnabled = true;
            this.semesterList.Location = new System.Drawing.Point(15, 82);
            this.semesterList.Name = "semesterList";
            this.semesterList.Size = new System.Drawing.Size(238, 21);
            this.semesterList.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Семестр";
            // 
            // addFromGroup
            // 
            this.addFromGroup.Location = new System.Drawing.Point(16, 233);
            this.addFromGroup.Name = "addFromGroup";
            this.addFromGroup.Size = new System.Drawing.Size(119, 49);
            this.addFromGroup.TabIndex = 34;
            this.addFromGroup.Text = "Добавить всех из группы";
            this.addFromGroup.UseVisualStyleBackColor = true;
            this.addFromGroup.Click += new System.EventHandler(this.addFromGroup_Click);
            // 
            // groupsList
            // 
            this.groupsList.FormattingEnabled = true;
            this.groupsList.Location = new System.Drawing.Point(141, 248);
            this.groupsList.Name = "groupsList";
            this.groupsList.Size = new System.Drawing.Size(112, 21);
            this.groupsList.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Студент";
            // 
            // StudentList
            // 
            this.StudentList.FormattingEnabled = true;
            this.StudentList.Location = new System.Drawing.Point(16, 176);
            this.StudentList.Name = "StudentList";
            this.StudentList.Size = new System.Drawing.Size(237, 21);
            this.StudentList.TabIndex = 31;
            this.StudentList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StudentList_KeyPress);
            // 
            // removeStudentFrunGroup
            // 
            this.removeStudentFrunGroup.Location = new System.Drawing.Point(178, 203);
            this.removeStudentFrunGroup.Name = "removeStudentFrunGroup";
            this.removeStudentFrunGroup.Size = new System.Drawing.Size(75, 23);
            this.removeStudentFrunGroup.TabIndex = 30;
            this.removeStudentFrunGroup.Text = "Удалить";
            this.removeStudentFrunGroup.UseVisualStyleBackColor = true;
            this.removeStudentFrunGroup.Click += new System.EventHandler(this.removeStudentFrunGroup_Click);
            // 
            // addStudentToGroup
            // 
            this.addStudentToGroup.Location = new System.Drawing.Point(16, 203);
            this.addStudentToGroup.Name = "addStudentToGroup";
            this.addStudentToGroup.Size = new System.Drawing.Size(74, 23);
            this.addStudentToGroup.TabIndex = 28;
            this.addStudentToGroup.Text = "Добавить";
            this.addStudentToGroup.UseVisualStyleBackColor = true;
            this.addStudentToGroup.Click += new System.EventHandler(this.addStudentToGroup_Click);
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(178, 115);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 26;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(96, 115);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 25;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(15, 115);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 24;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // StudentGroupName
            // 
            this.StudentGroupName.Location = new System.Drawing.Point(15, 34);
            this.StudentGroupName.Name = "StudentGroupName";
            this.StudentGroupName.Size = new System.Drawing.Size(238, 20);
            this.StudentGroupName.TabIndex = 23;
            this.StudentGroupName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StudentGroupName_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Название группы";
            // 
            // StudentListPanel
            // 
            this.StudentListPanel.Controls.Add(this.StudentsInGroupListView);
            this.StudentListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StudentListPanel.Location = new System.Drawing.Point(281, 0);
            this.StudentListPanel.Name = "StudentListPanel";
            this.StudentListPanel.Size = new System.Drawing.Size(963, 710);
            this.StudentListPanel.TabIndex = 17;
            // 
            // StudentsInGroupListView
            // 
            this.StudentsInGroupListView.AllowUserToAddRows = false;
            this.StudentsInGroupListView.AllowUserToDeleteRows = false;
            this.StudentsInGroupListView.AllowUserToResizeColumns = false;
            this.StudentsInGroupListView.AllowUserToResizeRows = false;
            this.StudentsInGroupListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StudentsInGroupListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StudentsInGroupListView.Location = new System.Drawing.Point(0, 0);
            this.StudentsInGroupListView.Name = "StudentsInGroupListView";
            this.StudentsInGroupListView.ReadOnly = true;
            this.StudentsInGroupListView.RowHeadersVisible = false;
            this.StudentsInGroupListView.Size = new System.Drawing.Size(963, 710);
            this.StudentsInGroupListView.TabIndex = 2;
            this.StudentsInGroupListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StudentsInGroupListView_CellClick);
            // 
            // updateSig
            // 
            this.updateSig.Location = new System.Drawing.Point(96, 203);
            this.updateSig.Name = "updateSig";
            this.updateSig.Size = new System.Drawing.Size(75, 23);
            this.updateSig.TabIndex = 45;
            this.updateSig.Text = "Изменить";
            this.updateSig.UseVisualStyleBackColor = true;
            this.updateSig.Click += new System.EventHandler(this.updateSig_Click);
            // 
            // StudentGroupList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 710);
            this.Controls.Add(this.StudentListPanel);
            this.Controls.Add(this.LeftPanel);
            this.Name = "StudentGroupList";
            this.Text = "Группы студентов";
            this.Load += new System.EventHandler(this.StudentGroupListLoad);
            this.LeftPanel.ResumeLayout(false);
            this.GroupListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StudentGroupListView)).EndInit();
            this.ControlsPanel.ResumeLayout(false);
            this.ControlsPanel.PerformLayout();
            this.StudentListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StudentsInGroupListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel LeftPanel;
        private Panel GroupListPanel;
        private Panel ControlsPanel;
        private Button remove;
        private Button update;
        private Button add;
        private TextBox StudentGroupName;
        private Label label1;
        private DataGridView StudentGroupListView;
        private Label label2;
        private ComboBox StudentList;
        private Button removeStudentFrunGroup;
        private Button addStudentToGroup;
        private Panel StudentListPanel;
        private DataGridView StudentsInGroupListView;
        private Button addFromGroup;
        private ComboBox groupsList;
        private ComboBox semesterList;
        private Label label3;
        private Button refresh;
        private CheckBox semesterFiltered;
        private ComboBox comboBox1;
        private Label label5;
        private DateTimePicker PeriodEnd;
        private Label label4;
        private DateTimePicker PeriodStart;
        private Button updateSig;
    }
}