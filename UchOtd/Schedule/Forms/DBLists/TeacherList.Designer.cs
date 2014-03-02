namespace Schedule.Forms.DBLists
{
    partial class TeacherList
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
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.TeacherListPanel = new System.Windows.Forms.Panel();
            this.TeacherListView = new System.Windows.Forms.DataGridView();
            this.TeacherControlsPanel = new System.Windows.Forms.Panel();
            this.teacherPhone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.deletewithlessons = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.teacherFIO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TFDPanel = new System.Windows.Forms.Panel();
            this.TFDListPanel = new System.Windows.Forms.Panel();
            this.TeacherTFDPanel = new System.Windows.Forms.Panel();
            this.TeacherTFDListPanel = new System.Windows.Forms.Panel();
            this.TFDListView = new System.Windows.Forms.DataGridView();
            this.TeacherTFDTitle = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.TFDSelectPanel = new System.Windows.Forms.Panel();
            this.AllDiscListPanel = new System.Windows.Forms.Panel();
            this.freeDiscPanel = new System.Windows.Forms.Panel();
            this.AllDisciplinesList = new System.Windows.Forms.DataGridView();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.filterButton = new System.Windows.Forms.Button();
            this.filter = new System.Windows.Forms.TextBox();
            this.AllDiscTitle = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.TFDControlsPanel = new System.Windows.Forms.Panel();
            this.removeWithLessons = new System.Windows.Forms.Button();
            this.removeTFD = new System.Windows.Forms.Button();
            this.addTFD = new System.Windows.Forms.Button();
            this.LeftPanel.SuspendLayout();
            this.TeacherListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TeacherListView)).BeginInit();
            this.TeacherControlsPanel.SuspendLayout();
            this.TFDPanel.SuspendLayout();
            this.TFDListPanel.SuspendLayout();
            this.TeacherTFDPanel.SuspendLayout();
            this.TeacherTFDListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TFDListView)).BeginInit();
            this.TeacherTFDTitle.SuspendLayout();
            this.TFDSelectPanel.SuspendLayout();
            this.AllDiscListPanel.SuspendLayout();
            this.freeDiscPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AllDisciplinesList)).BeginInit();
            this.filterPanel.SuspendLayout();
            this.AllDiscTitle.SuspendLayout();
            this.TFDControlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.TeacherListPanel);
            this.LeftPanel.Controls.Add(this.TeacherControlsPanel);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(260, 695);
            this.LeftPanel.TabIndex = 25;
            // 
            // TeacherListPanel
            // 
            this.TeacherListPanel.Controls.Add(this.TeacherListView);
            this.TeacherListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TeacherListPanel.Location = new System.Drawing.Point(0, 272);
            this.TeacherListPanel.Name = "TeacherListPanel";
            this.TeacherListPanel.Size = new System.Drawing.Size(260, 423);
            this.TeacherListPanel.TabIndex = 1;
            // 
            // TeacherListView
            // 
            this.TeacherListView.AllowUserToAddRows = false;
            this.TeacherListView.AllowUserToDeleteRows = false;
            this.TeacherListView.AllowUserToResizeColumns = false;
            this.TeacherListView.AllowUserToResizeRows = false;
            this.TeacherListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TeacherListView.ColumnHeadersVisible = false;
            this.TeacherListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TeacherListView.Location = new System.Drawing.Point(0, 0);
            this.TeacherListView.Name = "TeacherListView";
            this.TeacherListView.ReadOnly = true;
            this.TeacherListView.RowHeadersVisible = false;
            this.TeacherListView.Size = new System.Drawing.Size(260, 423);
            this.TeacherListView.TabIndex = 1;
            this.TeacherListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TeacherListView_CellClick);
            // 
            // TeacherControlsPanel
            // 
            this.TeacherControlsPanel.Controls.Add(this.teacherPhone);
            this.TeacherControlsPanel.Controls.Add(this.label2);
            this.TeacherControlsPanel.Controls.Add(this.deletewithlessons);
            this.TeacherControlsPanel.Controls.Add(this.remove);
            this.TeacherControlsPanel.Controls.Add(this.update);
            this.TeacherControlsPanel.Controls.Add(this.add);
            this.TeacherControlsPanel.Controls.Add(this.teacherFIO);
            this.TeacherControlsPanel.Controls.Add(this.label1);
            this.TeacherControlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TeacherControlsPanel.Location = new System.Drawing.Point(0, 0);
            this.TeacherControlsPanel.Name = "TeacherControlsPanel";
            this.TeacherControlsPanel.Size = new System.Drawing.Size(260, 272);
            this.TeacherControlsPanel.TabIndex = 0;
            // 
            // teacherPhone
            // 
            this.teacherPhone.Location = new System.Drawing.Point(15, 64);
            this.teacherPhone.Name = "teacherPhone";
            this.teacherPhone.Size = new System.Drawing.Size(198, 20);
            this.teacherPhone.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Телефон";
            // 
            // deletewithlessons
            // 
            this.deletewithlessons.Location = new System.Drawing.Point(16, 177);
            this.deletewithlessons.Name = "deletewithlessons";
            this.deletewithlessons.Size = new System.Drawing.Size(197, 49);
            this.deletewithlessons.TabIndex = 5;
            this.deletewithlessons.Text = "Удалить вместе c назначениями на дисциплины";
            this.deletewithlessons.UseVisualStyleBackColor = true;
            this.deletewithlessons.Click += new System.EventHandler(this.deletewithlessons_Click);
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(16, 148);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 4;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(16, 119);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 3;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(15, 90);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 2;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // teacherFIO
            // 
            this.teacherFIO.Location = new System.Drawing.Point(15, 25);
            this.teacherFIO.Name = "teacherFIO";
            this.teacherFIO.Size = new System.Drawing.Size(198, 20);
            this.teacherFIO.TabIndex = 0;
            this.teacherFIO.TextChanged += new System.EventHandler(this.TeacherFIOTextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "ФИО";
            // 
            // TFDPanel
            // 
            this.TFDPanel.Controls.Add(this.TFDListPanel);
            this.TFDPanel.Controls.Add(this.TFDControlsPanel);
            this.TFDPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TFDPanel.Location = new System.Drawing.Point(260, 0);
            this.TFDPanel.Name = "TFDPanel";
            this.TFDPanel.Size = new System.Drawing.Size(945, 695);
            this.TFDPanel.TabIndex = 26;
            // 
            // TFDListPanel
            // 
            this.TFDListPanel.Controls.Add(this.TeacherTFDPanel);
            this.TFDListPanel.Controls.Add(this.TFDSelectPanel);
            this.TFDListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TFDListPanel.Location = new System.Drawing.Point(0, 61);
            this.TFDListPanel.Name = "TFDListPanel";
            this.TFDListPanel.Size = new System.Drawing.Size(945, 634);
            this.TFDListPanel.TabIndex = 1;
            // 
            // TeacherTFDPanel
            // 
            this.TeacherTFDPanel.Controls.Add(this.TeacherTFDListPanel);
            this.TeacherTFDPanel.Controls.Add(this.TeacherTFDTitle);
            this.TeacherTFDPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TeacherTFDPanel.Location = new System.Drawing.Point(0, 398);
            this.TeacherTFDPanel.Name = "TeacherTFDPanel";
            this.TeacherTFDPanel.Size = new System.Drawing.Size(945, 236);
            this.TeacherTFDPanel.TabIndex = 1;
            // 
            // TeacherTFDListPanel
            // 
            this.TeacherTFDListPanel.Controls.Add(this.TFDListView);
            this.TeacherTFDListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TeacherTFDListPanel.Location = new System.Drawing.Point(0, 54);
            this.TeacherTFDListPanel.Name = "TeacherTFDListPanel";
            this.TeacherTFDListPanel.Size = new System.Drawing.Size(945, 182);
            this.TeacherTFDListPanel.TabIndex = 1;
            // 
            // TFDListView
            // 
            this.TFDListView.AllowUserToAddRows = false;
            this.TFDListView.AllowUserToDeleteRows = false;
            this.TFDListView.AllowUserToResizeColumns = false;
            this.TFDListView.AllowUserToResizeRows = false;
            this.TFDListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TFDListView.ColumnHeadersVisible = false;
            this.TFDListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TFDListView.Location = new System.Drawing.Point(0, 0);
            this.TFDListView.Name = "TFDListView";
            this.TFDListView.ReadOnly = true;
            this.TFDListView.RowHeadersVisible = false;
            this.TFDListView.Size = new System.Drawing.Size(945, 182);
            this.TFDListView.TabIndex = 4;
            // 
            // TeacherTFDTitle
            // 
            this.TeacherTFDTitle.Controls.Add(this.label4);
            this.TeacherTFDTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.TeacherTFDTitle.Location = new System.Drawing.Point(0, 0);
            this.TeacherTFDTitle.Name = "TeacherTFDTitle";
            this.TeacherTFDTitle.Size = new System.Drawing.Size(945, 54);
            this.TeacherTFDTitle.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(6, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(373, 31);
            this.label4.TabIndex = 1;
            this.label4.Text = "Дисциплины преподавателя";
            // 
            // TFDSelectPanel
            // 
            this.TFDSelectPanel.Controls.Add(this.AllDiscListPanel);
            this.TFDSelectPanel.Controls.Add(this.AllDiscTitle);
            this.TFDSelectPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TFDSelectPanel.Location = new System.Drawing.Point(0, 0);
            this.TFDSelectPanel.Name = "TFDSelectPanel";
            this.TFDSelectPanel.Size = new System.Drawing.Size(945, 398);
            this.TFDSelectPanel.TabIndex = 0;
            // 
            // AllDiscListPanel
            // 
            this.AllDiscListPanel.Controls.Add(this.freeDiscPanel);
            this.AllDiscListPanel.Controls.Add(this.filterPanel);
            this.AllDiscListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllDiscListPanel.Location = new System.Drawing.Point(0, 46);
            this.AllDiscListPanel.Name = "AllDiscListPanel";
            this.AllDiscListPanel.Size = new System.Drawing.Size(945, 352);
            this.AllDiscListPanel.TabIndex = 1;
            // 
            // freeDiscPanel
            // 
            this.freeDiscPanel.Controls.Add(this.AllDisciplinesList);
            this.freeDiscPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.freeDiscPanel.Location = new System.Drawing.Point(0, 50);
            this.freeDiscPanel.Name = "freeDiscPanel";
            this.freeDiscPanel.Size = new System.Drawing.Size(945, 302);
            this.freeDiscPanel.TabIndex = 8;
            // 
            // AllDisciplinesList
            // 
            this.AllDisciplinesList.AllowUserToAddRows = false;
            this.AllDisciplinesList.AllowUserToDeleteRows = false;
            this.AllDisciplinesList.AllowUserToResizeColumns = false;
            this.AllDisciplinesList.AllowUserToResizeRows = false;
            this.AllDisciplinesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AllDisciplinesList.ColumnHeadersVisible = false;
            this.AllDisciplinesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllDisciplinesList.Location = new System.Drawing.Point(0, 0);
            this.AllDisciplinesList.Name = "AllDisciplinesList";
            this.AllDisciplinesList.ReadOnly = true;
            this.AllDisciplinesList.RowHeadersVisible = false;
            this.AllDisciplinesList.Size = new System.Drawing.Size(945, 302);
            this.AllDisciplinesList.TabIndex = 5;
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.filterButton);
            this.filterPanel.Controls.Add(this.filter);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(0, 0);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(945, 50);
            this.filterPanel.TabIndex = 7;
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(551, 12);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(125, 23);
            this.filterButton.TabIndex = 1;
            this.filterButton.Text = "Отфильтровать";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.FilterButtonClick);
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(12, 15);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(533, 20);
            this.filter.TabIndex = 0;
            this.filter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterKeyPress);
            // 
            // AllDiscTitle
            // 
            this.AllDiscTitle.Controls.Add(this.label3);
            this.AllDiscTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.AllDiscTitle.Location = new System.Drawing.Point(0, 0);
            this.AllDiscTitle.Name = "AllDiscTitle";
            this.AllDiscTitle.Size = new System.Drawing.Size(945, 46);
            this.AllDiscTitle.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(393, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "Список свободных дисциплин";
            // 
            // TFDControlsPanel
            // 
            this.TFDControlsPanel.Controls.Add(this.removeWithLessons);
            this.TFDControlsPanel.Controls.Add(this.removeTFD);
            this.TFDControlsPanel.Controls.Add(this.addTFD);
            this.TFDControlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TFDControlsPanel.Location = new System.Drawing.Point(0, 0);
            this.TFDControlsPanel.Name = "TFDControlsPanel";
            this.TFDControlsPanel.Size = new System.Drawing.Size(945, 61);
            this.TFDControlsPanel.TabIndex = 0;
            // 
            // removeWithLessons
            // 
            this.removeWithLessons.Location = new System.Drawing.Point(220, 3);
            this.removeWithLessons.Name = "removeWithLessons";
            this.removeWithLessons.Size = new System.Drawing.Size(258, 23);
            this.removeWithLessons.TabIndex = 31;
            this.removeWithLessons.Text = "Удалить вместе с занятиями по дисциплине";
            this.removeWithLessons.UseVisualStyleBackColor = true;
            this.removeWithLessons.Click += new System.EventHandler(this.removeWithLessons_Click);
            // 
            // removeTFD
            // 
            this.removeTFD.Location = new System.Drawing.Point(113, 3);
            this.removeTFD.Name = "removeTFD";
            this.removeTFD.Size = new System.Drawing.Size(101, 23);
            this.removeTFD.TabIndex = 30;
            this.removeTFD.Text = "Удалить";
            this.removeTFD.UseVisualStyleBackColor = true;
            this.removeTFD.Click += new System.EventHandler(this.removeTFD_Click);
            // 
            // addTFD
            // 
            this.addTFD.Location = new System.Drawing.Point(6, 3);
            this.addTFD.Name = "addTFD";
            this.addTFD.Size = new System.Drawing.Size(101, 23);
            this.addTFD.TabIndex = 28;
            this.addTFD.Text = "Добавить";
            this.addTFD.UseVisualStyleBackColor = true;
            this.addTFD.Click += new System.EventHandler(this.addTFD_Click);
            // 
            // TeacherList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 695);
            this.Controls.Add(this.TFDPanel);
            this.Controls.Add(this.LeftPanel);
            this.Name = "TeacherList";
            this.Text = "Преподаватели";
            this.Load += new System.EventHandler(this.TeacherList_Load);
            this.LeftPanel.ResumeLayout(false);
            this.TeacherListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TeacherListView)).EndInit();
            this.TeacherControlsPanel.ResumeLayout(false);
            this.TeacherControlsPanel.PerformLayout();
            this.TFDPanel.ResumeLayout(false);
            this.TFDListPanel.ResumeLayout(false);
            this.TeacherTFDPanel.ResumeLayout(false);
            this.TeacherTFDListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TFDListView)).EndInit();
            this.TeacherTFDTitle.ResumeLayout(false);
            this.TeacherTFDTitle.PerformLayout();
            this.TFDSelectPanel.ResumeLayout(false);
            this.AllDiscListPanel.ResumeLayout(false);
            this.freeDiscPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AllDisciplinesList)).EndInit();
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.AllDiscTitle.ResumeLayout(false);
            this.AllDiscTitle.PerformLayout();
            this.TFDControlsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel TeacherControlsPanel;
        private System.Windows.Forms.TextBox teacherPhone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button deletewithlessons;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.TextBox teacherFIO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel TeacherListPanel;
        private System.Windows.Forms.DataGridView TeacherListView;
        private System.Windows.Forms.Panel TFDPanel;
        private System.Windows.Forms.Panel TFDListPanel;
        private System.Windows.Forms.Panel TFDControlsPanel;
        private System.Windows.Forms.Button removeTFD;
        private System.Windows.Forms.Button addTFD;
        private System.Windows.Forms.Panel TeacherTFDPanel;
        private System.Windows.Forms.Panel TFDSelectPanel;
        private System.Windows.Forms.Panel TeacherTFDListPanel;
        private System.Windows.Forms.DataGridView TFDListView;
        private System.Windows.Forms.Panel TeacherTFDTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel AllDiscListPanel;
        private System.Windows.Forms.DataGridView AllDisciplinesList;
        private System.Windows.Forms.Panel AllDiscTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button removeWithLessons;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.TextBox filter;
        private System.Windows.Forms.Panel freeDiscPanel;
    }
}