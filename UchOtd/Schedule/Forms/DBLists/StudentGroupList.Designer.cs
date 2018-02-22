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
            this.addFromGroup = new System.Windows.Forms.Button();
            this.groupsList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StudentList = new System.Windows.Forms.ComboBox();
            this.removeStudentFrunGroup = new System.Windows.Forms.Button();
            this.addStudentToGroup = new System.Windows.Forms.Button();
            this.deletewithlessons = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.StudentGroupName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StudentListPanel = new System.Windows.Forms.Panel();
            this.StudentsInGroupListView = new System.Windows.Forms.DataGridView();
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
            this.LeftPanel.Size = new System.Drawing.Size(281, 586);
            this.LeftPanel.TabIndex = 16;
            // 
            // GroupListPanel
            // 
            this.GroupListPanel.Controls.Add(this.StudentGroupListView);
            this.GroupListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupListPanel.Location = new System.Drawing.Point(0, 318);
            this.GroupListPanel.Name = "GroupListPanel";
            this.GroupListPanel.Size = new System.Drawing.Size(281, 268);
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
            this.StudentGroupListView.Size = new System.Drawing.Size(281, 268);
            this.StudentGroupListView.TabIndex = 1;
            this.StudentGroupListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StudentGroupListView_CellClick);
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Controls.Add(this.addFromGroup);
            this.ControlsPanel.Controls.Add(this.groupsList);
            this.ControlsPanel.Controls.Add(this.label2);
            this.ControlsPanel.Controls.Add(this.StudentList);
            this.ControlsPanel.Controls.Add(this.removeStudentFrunGroup);
            this.ControlsPanel.Controls.Add(this.addStudentToGroup);
            this.ControlsPanel.Controls.Add(this.deletewithlessons);
            this.ControlsPanel.Controls.Add(this.remove);
            this.ControlsPanel.Controls.Add(this.update);
            this.ControlsPanel.Controls.Add(this.add);
            this.ControlsPanel.Controls.Add(this.StudentGroupName);
            this.ControlsPanel.Controls.Add(this.label1);
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ControlsPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(281, 318);
            this.ControlsPanel.TabIndex = 22;
            // 
            // addFromGroup
            // 
            this.addFromGroup.Location = new System.Drawing.Point(16, 254);
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
            this.groupsList.Location = new System.Drawing.Point(141, 269);
            this.groupsList.Name = "groupsList";
            this.groupsList.Size = new System.Drawing.Size(112, 21);
            this.groupsList.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Студент";
            // 
            // StudentList
            // 
            this.StudentList.FormattingEnabled = true;
            this.StudentList.Location = new System.Drawing.Point(16, 197);
            this.StudentList.Name = "StudentList";
            this.StudentList.Size = new System.Drawing.Size(237, 21);
            this.StudentList.TabIndex = 31;
            this.StudentList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StudentList_KeyPress);
            // 
            // removeStudentFrunGroup
            // 
            this.removeStudentFrunGroup.Location = new System.Drawing.Point(141, 224);
            this.removeStudentFrunGroup.Name = "removeStudentFrunGroup";
            this.removeStudentFrunGroup.Size = new System.Drawing.Size(112, 23);
            this.removeStudentFrunGroup.TabIndex = 30;
            this.removeStudentFrunGroup.Text = "Удалить";
            this.removeStudentFrunGroup.UseVisualStyleBackColor = true;
            this.removeStudentFrunGroup.Click += new System.EventHandler(this.removeStudentFrunGroup_Click);
            // 
            // addStudentToGroup
            // 
            this.addStudentToGroup.Location = new System.Drawing.Point(16, 224);
            this.addStudentToGroup.Name = "addStudentToGroup";
            this.addStudentToGroup.Size = new System.Drawing.Size(119, 23);
            this.addStudentToGroup.TabIndex = 28;
            this.addStudentToGroup.Text = "Добавить";
            this.addStudentToGroup.UseVisualStyleBackColor = true;
            this.addStudentToGroup.Click += new System.EventHandler(this.addStudentToGroup_Click);
            // 
            // deletewithlessons
            // 
            this.deletewithlessons.Location = new System.Drawing.Point(16, 89);
            this.deletewithlessons.Name = "deletewithlessons";
            this.deletewithlessons.Size = new System.Drawing.Size(237, 49);
            this.deletewithlessons.TabIndex = 27;
            this.deletewithlessons.Text = "Удалить вместе c дисциплинами и убрать студентов из группы";
            this.deletewithlessons.UseVisualStyleBackColor = true;
            this.deletewithlessons.Click += new System.EventHandler(this.deletewithlessons_Click);
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(178, 60);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 26;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(96, 60);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 25;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(15, 60);
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
            this.StudentListPanel.Size = new System.Drawing.Size(963, 586);
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
            this.StudentsInGroupListView.Size = new System.Drawing.Size(963, 586);
            this.StudentsInGroupListView.TabIndex = 2;
            // 
            // StudentGroupList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 586);
            this.Controls.Add(this.StudentListPanel);
            this.Controls.Add(this.LeftPanel);
            this.Name = "StudentGroupList";
            this.Text = "Группы студентов";
            this.Load += new System.EventHandler(this.StudentGroupListLoad);
            this.Resize += new System.EventHandler(this.StudentGroupList_Resize);
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
        private Button deletewithlessons;
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
    }
}