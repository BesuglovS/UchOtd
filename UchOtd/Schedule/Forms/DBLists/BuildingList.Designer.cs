using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.DBLists
{
    partial class BuildingList
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
            this.GroupListPanel = new System.Windows.Forms.Panel();
            this.BuildingsListView = new System.Windows.Forms.DataGridView();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.BuildingName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BuildingsListView)).BeginInit();
            this.LeftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupListPanel
            // 
            this.GroupListPanel.Controls.Add(this.BuildingsListView);
            this.GroupListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupListPanel.Location = new System.Drawing.Point(259, 0);
            this.GroupListPanel.Name = "GroupListPanel";
            this.GroupListPanel.Size = new System.Drawing.Size(298, 196);
            this.GroupListPanel.TabIndex = 21;
            // 
            // BuildingsListView
            // 
            this.BuildingsListView.AllowUserToAddRows = false;
            this.BuildingsListView.AllowUserToDeleteRows = false;
            this.BuildingsListView.AllowUserToResizeColumns = false;
            this.BuildingsListView.AllowUserToResizeRows = false;
            this.BuildingsListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BuildingsListView.ColumnHeadersVisible = false;
            this.BuildingsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BuildingsListView.Location = new System.Drawing.Point(0, 0);
            this.BuildingsListView.Name = "BuildingsListView";
            this.BuildingsListView.ReadOnly = true;
            this.BuildingsListView.RowHeadersVisible = false;
            this.BuildingsListView.Size = new System.Drawing.Size(298, 196);
            this.BuildingsListView.TabIndex = 2;
            this.BuildingsListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BuildingsListView_CellClick);
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.remove);
            this.LeftPanel.Controls.Add(this.update);
            this.LeftPanel.Controls.Add(this.add);
            this.LeftPanel.Controls.Add(this.BuildingName);
            this.LeftPanel.Controls.Add(this.label1);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(259, 196);
            this.LeftPanel.TabIndex = 20;
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(169, 60);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(69, 23);
            this.remove.TabIndex = 36;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(92, 60);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(71, 23);
            this.update.TabIndex = 35;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(15, 60);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(71, 23);
            this.add.TabIndex = 34;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // BuildingName
            // 
            this.BuildingName.Location = new System.Drawing.Point(14, 34);
            this.BuildingName.Name = "BuildingName";
            this.BuildingName.Size = new System.Drawing.Size(224, 20);
            this.BuildingName.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Название корпуса";
            // 
            // BuildingsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 196);
            this.Controls.Add(this.GroupListPanel);
            this.Controls.Add(this.LeftPanel);
            this.Name = "BuildingsList";
            this.Text = "Корпуса";
            this.Load += new System.EventHandler(this.BuildingsList_Load);
            this.GroupListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BuildingsListView)).EndInit();
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel GroupListPanel;
        private Panel LeftPanel;
        private DataGridView BuildingsListView;
        private Button remove;
        private Button update;
        private Button add;
        private TextBox BuildingName;
        private Label label1;

    }
}