namespace UchOtd.Schedule.Forms.Analysis
{
    partial class GroupPeriods
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
            this.Group = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.refresh = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.groupnameFilter = new System.Windows.Forms.CheckBox();
            this.filter = new System.Windows.Forms.TextBox();
            this.add = new System.Windows.Forms.Button();
            this.zeroHours = new System.Windows.Forms.Button();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.orderByGroupname = new System.Windows.Forms.CheckBox();
            this.discnameFilter = new System.Windows.Forms.CheckBox();
            this.PeriodsListView = new System.Windows.Forms.DataGridView();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.PeriodName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.endOfPeriod = new System.Windows.Forms.DateTimePicker();
            this.startOfPeriod = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.filterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodsListView)).BeginInit();
            this.viewPanel.SuspendLayout();
            this.ListPanel.SuspendLayout();
            this.controlsPanel.SuspendLayout();
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
            this.reloadGroupList.Location = new System.Drawing.Point(88, 213);
            this.reloadGroupList.Name = "reloadGroupList";
            this.reloadGroupList.Size = new System.Drawing.Size(115, 81);
            this.reloadGroupList.TabIndex = 103;
            this.reloadGroupList.Text = "Перезагрузить список групп";
            this.reloadGroupList.UseVisualStyleBackColor = true;
            // 
            // Group
            // 
            this.Group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Group.FormattingEnabled = true;
            this.Group.Location = new System.Drawing.Point(8, 70);
            this.Group.Name = "Group";
            this.Group.Size = new System.Drawing.Size(200, 21);
            this.Group.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Группа";
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
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(8, 271);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 31;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(8, 242);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 30;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
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
            // add
            // 
            this.add.Location = new System.Drawing.Point(7, 213);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 6;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // zeroHours
            // 
            this.zeroHours.Location = new System.Drawing.Point(763, 29);
            this.zeroHours.Name = "zeroHours";
            this.zeroHours.Size = new System.Drawing.Size(179, 23);
            this.zeroHours.TabIndex = 13;
            this.zeroHours.Text = "0 часов в расписании";
            this.zeroHours.UseVisualStyleBackColor = true;
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.zeroHours);
            this.filterPanel.Controls.Add(this.orderByGroupname);
            this.filterPanel.Controls.Add(this.label8);
            this.filterPanel.Controls.Add(this.label7);
            this.filterPanel.Controls.Add(this.refresh);
            this.filterPanel.Controls.Add(this.groupNameList);
            this.filterPanel.Controls.Add(this.groupnameFilter);
            this.filterPanel.Controls.Add(this.discnameFilter);
            this.filterPanel.Controls.Add(this.filter);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(0, 0);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(596, 67);
            this.filterPanel.TabIndex = 1;
            // 
            // orderByGroupname
            // 
            this.orderByGroupname.AutoSize = true;
            this.orderByGroupname.Location = new System.Drawing.Point(763, 7);
            this.orderByGroupname.Name = "orderByGroupname";
            this.orderByGroupname.Size = new System.Drawing.Size(179, 17);
            this.orderByGroupname.TabIndex = 12;
            this.orderByGroupname.Text = "сортировать по имени группы";
            this.orderByGroupname.UseVisualStyleBackColor = true;
            // 
            // discnameFilter
            // 
            this.discnameFilter.AutoSize = true;
            this.discnameFilter.Location = new System.Drawing.Point(13, 33);
            this.discnameFilter.Name = "discnameFilter";
            this.discnameFilter.Size = new System.Drawing.Size(15, 14);
            this.discnameFilter.TabIndex = 3;
            this.discnameFilter.UseVisualStyleBackColor = true;
            // 
            // PeriodsListView
            // 
            this.PeriodsListView.AllowUserToAddRows = false;
            this.PeriodsListView.AllowUserToDeleteRows = false;
            this.PeriodsListView.AllowUserToResizeColumns = false;
            this.PeriodsListView.AllowUserToResizeRows = false;
            this.PeriodsListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PeriodsListView.ColumnHeadersVisible = false;
            this.PeriodsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PeriodsListView.Location = new System.Drawing.Point(0, 0);
            this.PeriodsListView.Name = "PeriodsListView";
            this.PeriodsListView.ReadOnly = true;
            this.PeriodsListView.RowHeadersVisible = false;
            this.PeriodsListView.Size = new System.Drawing.Size(596, 592);
            this.PeriodsListView.TabIndex = 0;
            this.PeriodsListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PeriodsListView_CellClick);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.PeriodsListView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 67);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(596, 592);
            this.viewPanel.TabIndex = 2;
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.viewPanel);
            this.ListPanel.Controls.Add(this.filterPanel);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(233, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(596, 659);
            this.ListPanel.TabIndex = 30;
            // 
            // PeriodName
            // 
            this.PeriodName.Location = new System.Drawing.Point(6, 25);
            this.PeriodName.Name = "PeriodName";
            this.PeriodName.Size = new System.Drawing.Size(202, 20);
            this.PeriodName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Наименование периода";
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.endOfPeriod);
            this.controlsPanel.Controls.Add(this.startOfPeriod);
            this.controlsPanel.Controls.Add(this.label3);
            this.controlsPanel.Controls.Add(this.label2);
            this.controlsPanel.Controls.Add(this.reloadGroupList);
            this.controlsPanel.Controls.Add(this.Group);
            this.controlsPanel.Controls.Add(this.label6);
            this.controlsPanel.Controls.Add(this.remove);
            this.controlsPanel.Controls.Add(this.update);
            this.controlsPanel.Controls.Add(this.add);
            this.controlsPanel.Controls.Add(this.PeriodName);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(233, 659);
            this.controlsPanel.TabIndex = 29;
            // 
            // endOfPeriod
            // 
            this.endOfPeriod.Location = new System.Drawing.Point(8, 177);
            this.endOfPeriod.Name = "endOfPeriod";
            this.endOfPeriod.Size = new System.Drawing.Size(200, 20);
            this.endOfPeriod.TabIndex = 107;
            // 
            // startOfPeriod
            // 
            this.startOfPeriod.Location = new System.Drawing.Point(8, 122);
            this.startOfPeriod.Name = "startOfPeriod";
            this.startOfPeriod.Size = new System.Drawing.Size(200, 20);
            this.startOfPeriod.TabIndex = 106;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 105;
            this.label3.Text = "Конец периода";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 104;
            this.label2.Text = "Начало периода";
            // 
            // GroupPeriods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 659);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "GroupPeriods";
            this.Text = "Периоды групп";
            this.Load += new System.EventHandler(this.GroupPeriods_Load);
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodsListView)).EndInit();
            this.viewPanel.ResumeLayout(false);
            this.ListPanel.ResumeLayout(false);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox groupNameList;
        private System.Windows.Forms.Button reloadGroupList;
        private System.Windows.Forms.ComboBox Group;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.CheckBox groupnameFilter;
        private System.Windows.Forms.TextBox filter;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button zeroHours;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.CheckBox orderByGroupname;
        private System.Windows.Forms.CheckBox discnameFilter;
        private System.Windows.Forms.DataGridView PeriodsListView;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.Panel ListPanel;
        private System.Windows.Forms.TextBox PeriodName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.DateTimePicker endOfPeriod;
        private System.Windows.Forms.DateTimePicker startOfPeriod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}