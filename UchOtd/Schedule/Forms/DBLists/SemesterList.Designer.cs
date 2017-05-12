namespace UchOtd.Schedule.Forms.DBLists
{
    partial class SemesterList
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
            this.GroupListPanel = new System.Windows.Forms.Panel();
            this.SemestersListView = new System.Windows.Forms.DataGridView();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.SemesterInYear = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.StartingYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.DisplayName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SemestersListView)).BeginInit();
            this.LeftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupListPanel
            // 
            this.GroupListPanel.Controls.Add(this.SemestersListView);
            this.GroupListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupListPanel.Location = new System.Drawing.Point(259, 0);
            this.GroupListPanel.Name = "GroupListPanel";
            this.GroupListPanel.Size = new System.Drawing.Size(311, 369);
            this.GroupListPanel.TabIndex = 23;
            // 
            // SemestersListView
            // 
            this.SemestersListView.AllowUserToAddRows = false;
            this.SemestersListView.AllowUserToDeleteRows = false;
            this.SemestersListView.AllowUserToResizeColumns = false;
            this.SemestersListView.AllowUserToResizeRows = false;
            this.SemestersListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SemestersListView.ColumnHeadersVisible = false;
            this.SemestersListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SemestersListView.Location = new System.Drawing.Point(0, 0);
            this.SemestersListView.Name = "SemestersListView";
            this.SemestersListView.ReadOnly = true;
            this.SemestersListView.RowHeadersVisible = false;
            this.SemestersListView.Size = new System.Drawing.Size(311, 369);
            this.SemestersListView.TabIndex = 2;
            this.SemestersListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SemestersListView_CellClick);
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.SemesterInYear);
            this.LeftPanel.Controls.Add(this.label3);
            this.LeftPanel.Controls.Add(this.StartingYear);
            this.LeftPanel.Controls.Add(this.label2);
            this.LeftPanel.Controls.Add(this.remove);
            this.LeftPanel.Controls.Add(this.update);
            this.LeftPanel.Controls.Add(this.add);
            this.LeftPanel.Controls.Add(this.DisplayName);
            this.LeftPanel.Controls.Add(this.label1);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(259, 369);
            this.LeftPanel.TabIndex = 22;
            // 
            // SemesterInYear
            // 
            this.SemesterInYear.Location = new System.Drawing.Point(14, 84);
            this.SemesterInYear.Name = "SemesterInYear";
            this.SemesterInYear.Size = new System.Drawing.Size(224, 20);
            this.SemesterInYear.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Номер семестра в учебном году";
            // 
            // StartingYear
            // 
            this.StartingYear.Location = new System.Drawing.Point(14, 34);
            this.StartingYear.Name = "StartingYear";
            this.StartingYear.Size = new System.Drawing.Size(224, 20);
            this.StartingYear.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Год начала учебного года";
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(169, 163);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(69, 23);
            this.remove.TabIndex = 36;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(92, 163);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(71, 23);
            this.update.TabIndex = 35;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(15, 163);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(71, 23);
            this.add.TabIndex = 34;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // DisplayName
            // 
            this.DisplayName.Location = new System.Drawing.Point(13, 134);
            this.DisplayName.Name = "DisplayName";
            this.DisplayName.Size = new System.Drawing.Size(224, 20);
            this.DisplayName.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Короткое имя";
            // 
            // SemesterList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 369);
            this.Controls.Add(this.GroupListPanel);
            this.Controls.Add(this.LeftPanel);
            this.Name = "SemesterList";
            this.Text = "Семестры расписания";
            this.Load += new System.EventHandler(this.SemesterList_Load);
            this.GroupListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SemestersListView)).EndInit();
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel GroupListPanel;
        private System.Windows.Forms.DataGridView SemestersListView;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.TextBox DisplayName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SemesterInYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox StartingYear;
        private System.Windows.Forms.Label label2;
    }
}