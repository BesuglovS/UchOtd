namespace UchOtd.Schedule.Forms.DBLists
{
    partial class RingList
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
            this.ListPanel = new System.Windows.Forms.Panel();
            this.RingListView = new System.Windows.Forms.DataGridView();
            this.forceDeleteWithReplace = new System.Windows.Forms.Button();
            this.deletewithlessons = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RingTime = new System.Windows.Forms.DateTimePicker();
            this.newRing = new System.Windows.Forms.DateTimePicker();
            this.ListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RingListView)).BeginInit();
            this.SuspendLayout();
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.RingListView);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ListPanel.Location = new System.Drawing.Point(228, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(274, 466);
            this.ListPanel.TabIndex = 9;
            // 
            // RingListView
            // 
            this.RingListView.AllowUserToAddRows = false;
            this.RingListView.AllowUserToDeleteRows = false;
            this.RingListView.AllowUserToResizeColumns = false;
            this.RingListView.AllowUserToResizeRows = false;
            this.RingListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RingListView.ColumnHeadersVisible = false;
            this.RingListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RingListView.Location = new System.Drawing.Point(0, 0);
            this.RingListView.Name = "RingListView";
            this.RingListView.ReadOnly = true;
            this.RingListView.RowHeadersVisible = false;
            this.RingListView.Size = new System.Drawing.Size(274, 466);
            this.RingListView.TabIndex = 0;
            this.RingListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RingListView_CellClick);
            // 
            // forceDeleteWithReplace
            // 
            this.forceDeleteWithReplace.Location = new System.Drawing.Point(10, 176);
            this.forceDeleteWithReplace.Name = "forceDeleteWithReplace";
            this.forceDeleteWithReplace.Size = new System.Drawing.Size(197, 23);
            this.forceDeleteWithReplace.TabIndex = 16;
            this.forceDeleteWithReplace.Text = "Удалить перенеся занятия";
            this.forceDeleteWithReplace.UseVisualStyleBackColor = true;
            this.forceDeleteWithReplace.Click += new System.EventHandler(this.forceDeleteWithReplace_Click);
            // 
            // deletewithlessons
            // 
            this.deletewithlessons.Location = new System.Drawing.Point(10, 147);
            this.deletewithlessons.Name = "deletewithlessons";
            this.deletewithlessons.Size = new System.Drawing.Size(197, 23);
            this.deletewithlessons.TabIndex = 15;
            this.deletewithlessons.Text = "Удалить вместе с занятиями";
            this.deletewithlessons.UseVisualStyleBackColor = true;
            this.deletewithlessons.Click += new System.EventHandler(this.deletewithlessons_Click);
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(10, 118);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 14;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(10, 89);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 13;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(9, 60);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 12;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Начало пары";
            // 
            // RingTime
            // 
            this.RingTime.CustomFormat = "H:mm";
            this.RingTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.RingTime.Location = new System.Drawing.Point(14, 31);
            this.RingTime.Name = "RingTime";
            this.RingTime.Size = new System.Drawing.Size(200, 20);
            this.RingTime.TabIndex = 18;
            // 
            // newRing
            // 
            this.newRing.CustomFormat = "H:mm";
            this.newRing.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.newRing.Location = new System.Drawing.Point(10, 205);
            this.newRing.Name = "newRing";
            this.newRing.Size = new System.Drawing.Size(197, 20);
            this.newRing.TabIndex = 19;
            // 
            // RingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 466);
            this.Controls.Add(this.newRing);
            this.Controls.Add(this.RingTime);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.forceDeleteWithReplace);
            this.Controls.Add(this.deletewithlessons);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.update);
            this.Controls.Add(this.add);
            this.Controls.Add(this.label1);
            this.Name = "RingForm";
            this.Text = "Звонки";
            this.Load += new System.EventHandler(this.RingForm_Load);
            this.ListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RingListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ListPanel;
        private System.Windows.Forms.DataGridView RingListView;
        private System.Windows.Forms.Button forceDeleteWithReplace;
        private System.Windows.Forms.Button deletewithlessons;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker RingTime;
        private System.Windows.Forms.DateTimePicker newRing;
    }
}