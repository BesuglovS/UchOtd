namespace Schedule.Forms.DBLists
{
    partial class FacultyList
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
            this.GroupsView = new System.Windows.Forms.DataGridView();
            this.GroupListPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupList = new System.Windows.Forms.ComboBox();
            this.removeStudentFrunGroup = new System.Windows.Forms.Button();
            this.FacultiesListView = new System.Windows.Forms.DataGridView();
            this.FacultyListPanel = new System.Windows.Forms.Panel();
            this.addGroupToFaculty = new System.Windows.Forms.Button();
            this.cascadeDelete = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.FacultyName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.SortingOrder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FacultyLetter = new System.Windows.Forms.TextBox();
            this.label768 = new System.Windows.Forms.Label();
            this.LeftPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.GroupsView)).BeginInit();
            this.GroupListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FacultiesListView)).BeginInit();
            this.FacultyListPanel.SuspendLayout();
            this.ControlsPanel.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupsView
            // 
            this.GroupsView.AllowUserToAddRows = false;
            this.GroupsView.AllowUserToDeleteRows = false;
            this.GroupsView.AllowUserToResizeColumns = false;
            this.GroupsView.AllowUserToResizeRows = false;
            this.GroupsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GroupsView.ColumnHeadersVisible = false;
            this.GroupsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupsView.Location = new System.Drawing.Point(0, 0);
            this.GroupsView.Name = "GroupsView";
            this.GroupsView.ReadOnly = true;
            this.GroupsView.RowHeadersVisible = false;
            this.GroupsView.Size = new System.Drawing.Size(274, 688);
            this.GroupsView.TabIndex = 2;
            // 
            // GroupListPanel
            // 
            this.GroupListPanel.Controls.Add(this.GroupsView);
            this.GroupListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupListPanel.Location = new System.Drawing.Point(309, 0);
            this.GroupListPanel.Name = "GroupListPanel";
            this.GroupListPanel.Size = new System.Drawing.Size(274, 688);
            this.GroupListPanel.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Группа";
            // 
            // GroupList
            // 
            this.GroupList.FormattingEnabled = true;
            this.GroupList.ItemHeight = 13;
            this.GroupList.Location = new System.Drawing.Point(16, 261);
            this.GroupList.Name = "GroupList";
            this.GroupList.Size = new System.Drawing.Size(287, 21);
            this.GroupList.TabIndex = 31;
            this.GroupList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GroupList_KeyPress);
            // 
            // removeStudentFrunGroup
            // 
            this.removeStudentFrunGroup.Location = new System.Drawing.Point(170, 288);
            this.removeStudentFrunGroup.Name = "removeStudentFrunGroup";
            this.removeStudentFrunGroup.Size = new System.Drawing.Size(133, 23);
            this.removeStudentFrunGroup.TabIndex = 9;
            this.removeStudentFrunGroup.Text = "Удалить";
            this.removeStudentFrunGroup.UseVisualStyleBackColor = true;
            this.removeStudentFrunGroup.Click += new System.EventHandler(this.removeStudentFrunGroup_Click);
            // 
            // FacultiesListView
            // 
            this.FacultiesListView.AllowUserToAddRows = false;
            this.FacultiesListView.AllowUserToDeleteRows = false;
            this.FacultiesListView.AllowUserToResizeColumns = false;
            this.FacultiesListView.AllowUserToResizeRows = false;
            this.FacultiesListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FacultiesListView.ColumnHeadersVisible = false;
            this.FacultiesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FacultiesListView.Location = new System.Drawing.Point(0, 0);
            this.FacultiesListView.Name = "FacultiesListView";
            this.FacultiesListView.ReadOnly = true;
            this.FacultiesListView.RowHeadersVisible = false;
            this.FacultiesListView.Size = new System.Drawing.Size(309, 355);
            this.FacultiesListView.TabIndex = 1;
            this.FacultiesListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FacultiesListViewCellClick);
            // 
            // FacultyListPanel
            // 
            this.FacultyListPanel.Controls.Add(this.FacultiesListView);
            this.FacultyListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FacultyListPanel.Location = new System.Drawing.Point(0, 333);
            this.FacultyListPanel.Name = "FacultyListPanel";
            this.FacultyListPanel.Size = new System.Drawing.Size(309, 355);
            this.FacultyListPanel.TabIndex = 23;
            // 
            // addGroupToFaculty
            // 
            this.addGroupToFaculty.Location = new System.Drawing.Point(16, 288);
            this.addGroupToFaculty.Name = "addGroupToFaculty";
            this.addGroupToFaculty.Size = new System.Drawing.Size(139, 23);
            this.addGroupToFaculty.TabIndex = 8;
            this.addGroupToFaculty.Text = "Добавить";
            this.addGroupToFaculty.UseVisualStyleBackColor = true;
            this.addGroupToFaculty.Click += new System.EventHandler(this.addGroupToFaculty_Click);
            // 
            // cascadeDelete
            // 
            this.cascadeDelete.Location = new System.Drawing.Point(16, 185);
            this.cascadeDelete.Name = "cascadeDelete";
            this.cascadeDelete.Size = new System.Drawing.Size(287, 49);
            this.cascadeDelete.TabIndex = 6;
            this.cascadeDelete.Text = "Удалить вместе с привязками к группам";
            this.cascadeDelete.UseVisualStyleBackColor = true;
            this.cascadeDelete.Click += new System.EventHandler(this.cascadeDelete_Click);
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(218, 156);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(85, 23);
            this.remove.TabIndex = 5;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(116, 156);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(85, 23);
            this.update.TabIndex = 4;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.UpdateClick);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(15, 156);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(85, 23);
            this.add.TabIndex = 3;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.AddClick);
            // 
            // FacultyName
            // 
            this.FacultyName.Location = new System.Drawing.Point(15, 34);
            this.FacultyName.Name = "FacultyName";
            this.FacultyName.Size = new System.Drawing.Size(288, 20);
            this.FacultyName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Название факультета";
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Controls.Add(this.SortingOrder);
            this.ControlsPanel.Controls.Add(this.label3);
            this.ControlsPanel.Controls.Add(this.FacultyLetter);
            this.ControlsPanel.Controls.Add(this.label768);
            this.ControlsPanel.Controls.Add(this.label2);
            this.ControlsPanel.Controls.Add(this.GroupList);
            this.ControlsPanel.Controls.Add(this.removeStudentFrunGroup);
            this.ControlsPanel.Controls.Add(this.addGroupToFaculty);
            this.ControlsPanel.Controls.Add(this.cascadeDelete);
            this.ControlsPanel.Controls.Add(this.remove);
            this.ControlsPanel.Controls.Add(this.update);
            this.ControlsPanel.Controls.Add(this.add);
            this.ControlsPanel.Controls.Add(this.FacultyName);
            this.ControlsPanel.Controls.Add(this.label1);
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ControlsPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(309, 333);
            this.ControlsPanel.TabIndex = 22;
            // 
            // SortingOrder
            // 
            this.SortingOrder.Location = new System.Drawing.Point(16, 130);
            this.SortingOrder.Name = "SortingOrder";
            this.SortingOrder.Size = new System.Drawing.Size(287, 20);
            this.SortingOrder.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Порядок сортировки";
            // 
            // FacultyLetter
            // 
            this.FacultyLetter.Location = new System.Drawing.Point(16, 82);
            this.FacultyLetter.Name = "FacultyLetter";
            this.FacultyLetter.Size = new System.Drawing.Size(287, 20);
            this.FacultyLetter.TabIndex = 1;
            // 
            // label768
            // 
            this.label768.AutoSize = true;
            this.label768.Location = new System.Drawing.Point(13, 57);
            this.label768.Name = "label768";
            this.label768.Size = new System.Drawing.Size(37, 13);
            this.label768.TabIndex = 33;
            this.label768.Text = "Буква";
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.FacultyListPanel);
            this.LeftPanel.Controls.Add(this.ControlsPanel);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(309, 688);
            this.LeftPanel.TabIndex = 18;
            // 
            // FacultyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 688);
            this.Controls.Add(this.GroupListPanel);
            this.Controls.Add(this.LeftPanel);
            this.Name = "FacultyList";
            this.Text = "Факультеты";
            this.Load += new System.EventHandler(this.FacultyListLoad);
            ((System.ComponentModel.ISupportInitialize)(this.GroupsView)).EndInit();
            this.GroupListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FacultiesListView)).EndInit();
            this.FacultyListPanel.ResumeLayout(false);
            this.ControlsPanel.ResumeLayout(false);
            this.ControlsPanel.PerformLayout();
            this.LeftPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GroupsView;
        private System.Windows.Forms.Panel GroupListPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox GroupList;
        private System.Windows.Forms.Button removeStudentFrunGroup;
        private System.Windows.Forms.DataGridView FacultiesListView;
        private System.Windows.Forms.Panel FacultyListPanel;
        private System.Windows.Forms.Button addGroupToFaculty;
        private System.Windows.Forms.Button cascadeDelete;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.TextBox FacultyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel ControlsPanel;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.TextBox SortingOrder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FacultyLetter;
        private System.Windows.Forms.Label label768;
    }
}