namespace UchOtd.Schedule.Forms.DBLists
{
    partial class DisciplineNameList
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
            this.groupNameList = new System.Windows.Forms.ComboBox();
            this.reloadGroupList = new System.Windows.Forms.Button();
            this.DisciplinesList = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.refresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.groupnameFilter = new System.Windows.Forms.CheckBox();
            this.filter = new System.Windows.Forms.TextBox();
            this.DisciplineName = new System.Windows.Forms.TextBox();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.nameFilter = new System.Windows.Forms.CheckBox();
            this.DiscipineNameListView = new System.Windows.Forms.DataGridView();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.StudentGroupList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.filterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiscipineNameListView)).BeginInit();
            this.viewPanel.SuspendLayout();
            this.ListPanel.SuspendLayout();
            this.controlsPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupNameList
            // 
            this.groupNameList.FormattingEnabled = true;
            this.groupNameList.Location = new System.Drawing.Point(330, 29);
            this.groupNameList.Name = "groupNameList";
            this.groupNameList.Size = new System.Drawing.Size(100, 21);
            this.groupNameList.TabIndex = 5;
            // 
            // reloadGroupList
            // 
            this.reloadGroupList.Location = new System.Drawing.Point(6, 164);
            this.reloadGroupList.Name = "reloadGroupList";
            this.reloadGroupList.Size = new System.Drawing.Size(342, 23);
            this.reloadGroupList.TabIndex = 103;
            this.reloadGroupList.Text = "Перезагрузить список групп";
            this.reloadGroupList.UseVisualStyleBackColor = true;
            // 
            // DisciplinesList
            // 
            this.DisciplinesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DisciplinesList.FormattingEnabled = true;
            this.DisciplinesList.Location = new System.Drawing.Point(6, 63);
            this.DisciplinesList.Name = "DisciplinesList";
            this.DisciplinesList.Size = new System.Drawing.Size(343, 21);
            this.DisciplinesList.TabIndex = 101;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(316, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Фильтр по группе";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(97, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Фильтр по названию";
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(436, 12);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(112, 41);
            this.refresh.TabIndex = 6;
            this.refresh.Text = "Обновить";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Дисциплина";
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(238, 135);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(110, 23);
            this.remove.TabIndex = 31;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(122, 135);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(110, 23);
            this.update.TabIndex = 30;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(6, 135);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(110, 23);
            this.add.TabIndex = 6;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // groupnameFilter
            // 
            this.groupnameFilter.AutoSize = true;
            this.groupnameFilter.Location = new System.Drawing.Point(309, 32);
            this.groupnameFilter.Name = "groupnameFilter";
            this.groupnameFilter.Size = new System.Drawing.Size(15, 14);
            this.groupnameFilter.TabIndex = 4;
            this.groupnameFilter.UseVisualStyleBackColor = true;
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(34, 30);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(267, 20);
            this.filter.TabIndex = 0;
            // 
            // DisciplineName
            // 
            this.DisciplineName.Location = new System.Drawing.Point(6, 25);
            this.DisciplineName.Name = "DisciplineName";
            this.DisciplineName.Size = new System.Drawing.Size(343, 20);
            this.DisciplineName.TabIndex = 0;
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.label8);
            this.filterPanel.Controls.Add(this.label7);
            this.filterPanel.Controls.Add(this.refresh);
            this.filterPanel.Controls.Add(this.groupNameList);
            this.filterPanel.Controls.Add(this.groupnameFilter);
            this.filterPanel.Controls.Add(this.nameFilter);
            this.filterPanel.Controls.Add(this.filter);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(0, 0);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(610, 67);
            this.filterPanel.TabIndex = 1;
            // 
            // nameFilter
            // 
            this.nameFilter.AutoSize = true;
            this.nameFilter.Location = new System.Drawing.Point(13, 33);
            this.nameFilter.Name = "nameFilter";
            this.nameFilter.Size = new System.Drawing.Size(15, 14);
            this.nameFilter.TabIndex = 3;
            this.nameFilter.UseVisualStyleBackColor = true;
            // 
            // DiscipineNameListView
            // 
            this.DiscipineNameListView.AllowUserToAddRows = false;
            this.DiscipineNameListView.AllowUserToDeleteRows = false;
            this.DiscipineNameListView.AllowUserToResizeRows = false;
            this.DiscipineNameListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DiscipineNameListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiscipineNameListView.Location = new System.Drawing.Point(0, 0);
            this.DiscipineNameListView.Name = "DiscipineNameListView";
            this.DiscipineNameListView.ReadOnly = true;
            this.DiscipineNameListView.Size = new System.Drawing.Size(610, 602);
            this.DiscipineNameListView.TabIndex = 0;
            this.DiscipineNameListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DiscipineNameListView_CellClick);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.DiscipineNameListView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 67);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(610, 602);
            this.viewPanel.TabIndex = 2;
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.viewPanel);
            this.ListPanel.Controls.Add(this.filterPanel);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(355, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(610, 669);
            this.ListPanel.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Наименование дисциплины";
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.panel1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(355, 669);
            this.controlsPanel.TabIndex = 29;
            // 
            // StudentGroupList
            // 
            this.StudentGroupList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StudentGroupList.FormattingEnabled = true;
            this.StudentGroupList.Location = new System.Drawing.Point(6, 108);
            this.StudentGroupList.Name = "StudentGroupList";
            this.StudentGroupList.Size = new System.Drawing.Size(343, 21);
            this.StudentGroupList.TabIndex = 105;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 104;
            this.label3.Text = "Группа";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.StudentGroupList);
            this.panel1.Controls.Add(this.DisciplineName);
            this.panel1.Controls.Add(this.add);
            this.panel1.Controls.Add(this.reloadGroupList);
            this.panel1.Controls.Add(this.update);
            this.panel1.Controls.Add(this.DisciplinesList);
            this.panel1.Controls.Add(this.remove);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 203);
            this.panel1.TabIndex = 106;
            // 
            // DisciplineNameList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 669);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "DisciplineNameList";
            this.Text = "Названия дисциплин";
            this.Load += new System.EventHandler(this.DisciplineNameList_Load);
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiscipineNameListView)).EndInit();
            this.viewPanel.ResumeLayout(false);
            this.ListPanel.ResumeLayout(false);
            this.controlsPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox groupNameList;
        private System.Windows.Forms.Button reloadGroupList;
        private System.Windows.Forms.ComboBox DisciplinesList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.CheckBox groupnameFilter;
        private System.Windows.Forms.TextBox filter;
        private System.Windows.Forms.TextBox DisciplineName;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.CheckBox nameFilter;
        private System.Windows.Forms.DataGridView DiscipineNameListView;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.Panel ListPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox StudentGroupList;
    }
}