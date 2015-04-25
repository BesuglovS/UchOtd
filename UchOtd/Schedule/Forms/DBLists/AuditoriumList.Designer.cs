using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.DBLists
{
    partial class AuditoriumList
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
            this.ListPanel = new System.Windows.Forms.Panel();
            this.AuditoriumListView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.AudName = new System.Windows.Forms.TextBox();
            this.add = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.deletewithlessons = new System.Windows.Forms.Button();
            this.forceDeleteWithReplace = new System.Windows.Forms.Button();
            this.newAuditorium = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BuildingsList = new System.Windows.Forms.ComboBox();
            this.ListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AuditoriumListView)).BeginInit();
            this.SuspendLayout();
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.AuditoriumListView);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ListPanel.Location = new System.Drawing.Point(289, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(444, 620);
            this.ListPanel.TabIndex = 0;
            // 
            // AuditoriumListView
            // 
            this.AuditoriumListView.AllowUserToAddRows = false;
            this.AuditoriumListView.AllowUserToDeleteRows = false;
            this.AuditoriumListView.AllowUserToResizeColumns = false;
            this.AuditoriumListView.AllowUserToResizeRows = false;
            this.AuditoriumListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AuditoriumListView.ColumnHeadersVisible = false;
            this.AuditoriumListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AuditoriumListView.Location = new System.Drawing.Point(0, 0);
            this.AuditoriumListView.Name = "AuditoriumListView";
            this.AuditoriumListView.ReadOnly = true;
            this.AuditoriumListView.RowHeadersVisible = false;
            this.AuditoriumListView.Size = new System.Drawing.Size(444, 620);
            this.AuditoriumListView.TabIndex = 0;
            this.AuditoriumListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AuditoriumListView_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Аудитория";
            // 
            // AudName
            // 
            this.AudName.Location = new System.Drawing.Point(15, 34);
            this.AudName.Name = "AudName";
            this.AudName.Size = new System.Drawing.Size(198, 20);
            this.AudName.TabIndex = 2;
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(15, 111);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 3;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(16, 140);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 4;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.save_Click);
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(16, 169);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 5;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // deletewithlessons
            // 
            this.deletewithlessons.Location = new System.Drawing.Point(16, 198);
            this.deletewithlessons.Name = "deletewithlessons";
            this.deletewithlessons.Size = new System.Drawing.Size(197, 23);
            this.deletewithlessons.TabIndex = 6;
            this.deletewithlessons.Text = "Удалить вместе с занятиями";
            this.deletewithlessons.UseVisualStyleBackColor = true;
            this.deletewithlessons.Click += new System.EventHandler(this.deletewithlessons_Click);
            // 
            // forceDeleteWithReplace
            // 
            this.forceDeleteWithReplace.Location = new System.Drawing.Point(16, 227);
            this.forceDeleteWithReplace.Name = "forceDeleteWithReplace";
            this.forceDeleteWithReplace.Size = new System.Drawing.Size(197, 23);
            this.forceDeleteWithReplace.TabIndex = 7;
            this.forceDeleteWithReplace.Text = "Удалить перенеся занятия";
            this.forceDeleteWithReplace.UseVisualStyleBackColor = true;
            this.forceDeleteWithReplace.Click += new System.EventHandler(this.forceDeleteWithReplace_Click);
            // 
            // newAuditorium
            // 
            this.newAuditorium.Location = new System.Drawing.Point(16, 256);
            this.newAuditorium.Name = "newAuditorium";
            this.newAuditorium.Size = new System.Drawing.Size(197, 20);
            this.newAuditorium.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Корпус";
            // 
            // BuildingsList
            // 
            this.BuildingsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BuildingsList.FormattingEnabled = true;
            this.BuildingsList.Location = new System.Drawing.Point(14, 81);
            this.BuildingsList.Name = "BuildingsList";
            this.BuildingsList.Size = new System.Drawing.Size(199, 21);
            this.BuildingsList.TabIndex = 10;
            // 
            // AuditoriumList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 620);
            this.Controls.Add(this.BuildingsList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newAuditorium);
            this.Controls.Add(this.forceDeleteWithReplace);
            this.Controls.Add(this.deletewithlessons);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.update);
            this.Controls.Add(this.add);
            this.Controls.Add(this.AudName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuditoriumList";
            this.ShowInTaskbar = false;
            this.Text = "Аудитории";
            this.Load += new System.EventHandler(this.AuditoriumList_Load);
            this.ListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AuditoriumListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel ListPanel;
        private DataGridView AuditoriumListView;
        private Label label1;
        private TextBox AudName;
        private Button add;
        private Button update;
        private Button remove;
        private Button deletewithlessons;
        private Button forceDeleteWithReplace;
        private TextBox newAuditorium;
        private Label label2;
        private ComboBox BuildingsList;
    }
}