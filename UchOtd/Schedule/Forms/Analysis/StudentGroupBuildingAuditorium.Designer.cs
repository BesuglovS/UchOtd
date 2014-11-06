namespace UchOtd.Schedule.Forms.Analysis
{
    partial class StudentGroupBuildingAuditorium
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
            this.reloadGroupList = new System.Windows.Forms.Button();
            this.group = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.itemsListView = new System.Windows.Forms.DataGridView();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.building = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.auditorium = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.itemsListView)).BeginInit();
            this.viewPanel.SuspendLayout();
            this.ListPanel.SuspendLayout();
            this.controlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // reloadGroupList
            // 
            this.reloadGroupList.Location = new System.Drawing.Point(88, 162);
            this.reloadGroupList.Name = "reloadGroupList";
            this.reloadGroupList.Size = new System.Drawing.Size(115, 81);
            this.reloadGroupList.TabIndex = 103;
            this.reloadGroupList.Text = "Перезагрузить список групп";
            this.reloadGroupList.UseVisualStyleBackColor = true;
            // 
            // group
            // 
            this.group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.group.FormattingEnabled = true;
            this.group.Location = new System.Drawing.Point(6, 28);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(197, 21);
            this.group.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Группа";
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(8, 220);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 31;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(8, 191);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 30;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(7, 162);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 6;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // itemsListView
            // 
            this.itemsListView.AllowUserToAddRows = false;
            this.itemsListView.AllowUserToDeleteRows = false;
            this.itemsListView.AllowUserToResizeColumns = false;
            this.itemsListView.AllowUserToResizeRows = false;
            this.itemsListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemsListView.Location = new System.Drawing.Point(0, 0);
            this.itemsListView.Name = "itemsListView";
            this.itemsListView.ReadOnly = true;
            this.itemsListView.RowHeadersVisible = false;
            this.itemsListView.Size = new System.Drawing.Size(415, 702);
            this.itemsListView.TabIndex = 0;
            this.itemsListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.itemsListView_CellClick);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.itemsListView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 0);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(415, 702);
            this.viewPanel.TabIndex = 2;
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.viewPanel);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(218, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(415, 702);
            this.ListPanel.TabIndex = 30;
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.auditorium);
            this.controlsPanel.Controls.Add(this.label2);
            this.controlsPanel.Controls.Add(this.building);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Controls.Add(this.reloadGroupList);
            this.controlsPanel.Controls.Add(this.group);
            this.controlsPanel.Controls.Add(this.label6);
            this.controlsPanel.Controls.Add(this.remove);
            this.controlsPanel.Controls.Add(this.update);
            this.controlsPanel.Controls.Add(this.add);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(218, 702);
            this.controlsPanel.TabIndex = 29;
            // 
            // building
            // 
            this.building.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.building.FormattingEnabled = true;
            this.building.Location = new System.Drawing.Point(8, 70);
            this.building.Name = "building";
            this.building.Size = new System.Drawing.Size(197, 21);
            this.building.TabIndex = 104;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 105;
            this.label1.Text = "Корпус";
            // 
            // auditorium
            // 
            this.auditorium.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.auditorium.FormattingEnabled = true;
            this.auditorium.Location = new System.Drawing.Point(9, 114);
            this.auditorium.Name = "auditorium";
            this.auditorium.Size = new System.Drawing.Size(197, 21);
            this.auditorium.TabIndex = 106;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 13);
            this.label2.TabIndex = 107;
            this.label2.Text = "Преимущественная аудитория";
            // 
            // StudentGroupBuildingAuditorium
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 702);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "StudentGroupBuildingAuditorium";
            this.Text = "Корпус и преимущественная аудитория для группы";
            this.Load += new System.EventHandler(this.StudentGroupBuildingAuditorium_Load);
            ((System.ComponentModel.ISupportInitialize)(this.itemsListView)).EndInit();
            this.viewPanel.ResumeLayout(false);
            this.ListPanel.ResumeLayout(false);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button reloadGroupList;
        private System.Windows.Forms.ComboBox group;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.DataGridView itemsListView;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.Panel ListPanel;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.ComboBox auditorium;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox building;
        private System.Windows.Forms.Label label1;
    }
}