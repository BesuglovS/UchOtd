namespace UchOtd.Schedule.Forms.DBLists
{
    partial class AuditoriumEventsList
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
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.finishDate = new System.Windows.Forms.DateTimePicker();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.useDataSet = new System.Windows.Forms.CheckBox();
            this.eventDate = new System.Windows.Forms.DateTimePicker();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.eventAuditorium = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.eventTime = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.eventName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.eventsView = new System.Windows.Forms.DataGridView();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.showAll = new System.Windows.Forms.Button();
            this.eventsDate = new System.Windows.Forms.DateTimePicker();
            this.filter = new System.Windows.Forms.Button();
            this.filterBox = new System.Windows.Forms.CheckBox();
            this.ControlsPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventsView)).BeginInit();
            this.filterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Controls.Add(this.label5);
            this.ControlsPanel.Controls.Add(this.finishDate);
            this.ControlsPanel.Controls.Add(this.startDate);
            this.ControlsPanel.Controls.Add(this.useDataSet);
            this.ControlsPanel.Controls.Add(this.eventDate);
            this.ControlsPanel.Controls.Add(this.remove);
            this.ControlsPanel.Controls.Add(this.update);
            this.ControlsPanel.Controls.Add(this.add);
            this.ControlsPanel.Controls.Add(this.eventAuditorium);
            this.ControlsPanel.Controls.Add(this.label4);
            this.ControlsPanel.Controls.Add(this.eventTime);
            this.ControlsPanel.Controls.Add(this.label3);
            this.ControlsPanel.Controls.Add(this.label2);
            this.ControlsPanel.Controls.Add(this.eventName);
            this.ControlsPanel.Controls.Add(this.label1);
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ControlsPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(234, 632);
            this.ControlsPanel.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 277);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Только один день недели";
            // 
            // finishDate
            // 
            this.finishDate.Location = new System.Drawing.Point(16, 319);
            this.finishDate.Name = "finishDate";
            this.finishDate.Size = new System.Drawing.Size(206, 20);
            this.finishDate.TabIndex = 14;
            // 
            // startDate
            // 
            this.startDate.Location = new System.Drawing.Point(16, 293);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(206, 20);
            this.startDate.TabIndex = 13;
            // 
            // useDataSet
            // 
            this.useDataSet.AutoSize = true;
            this.useDataSet.Location = new System.Drawing.Point(16, 257);
            this.useDataSet.Name = "useDataSet";
            this.useDataSet.Size = new System.Drawing.Size(147, 17);
            this.useDataSet.TabIndex = 12;
            this.useDataSet.Text = "Использовать эти даты";
            this.useDataSet.UseVisualStyleBackColor = true;
            // 
            // eventDate
            // 
            this.eventDate.Location = new System.Drawing.Point(15, 64);
            this.eventDate.Name = "eventDate";
            this.eventDate.Size = new System.Drawing.Size(206, 20);
            this.eventDate.TabIndex = 11;
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(16, 228);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 10;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(16, 199);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 9;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(15, 170);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 8;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // eventAuditorium
            // 
            this.eventAuditorium.FormattingEnabled = true;
            this.eventAuditorium.Location = new System.Drawing.Point(15, 143);
            this.eventAuditorium.Name = "eventAuditorium";
            this.eventAuditorium.Size = new System.Drawing.Size(206, 21);
            this.eventAuditorium.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Аудитория";
            // 
            // eventTime
            // 
            this.eventTime.FormattingEnabled = true;
            this.eventTime.Location = new System.Drawing.Point(15, 103);
            this.eventTime.Name = "eventTime";
            this.eventTime.Size = new System.Drawing.Size(206, 21);
            this.eventTime.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Время события";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дата события";
            // 
            // eventName
            // 
            this.eventName.Location = new System.Drawing.Point(15, 25);
            this.eventName.Name = "eventName";
            this.eventName.Size = new System.Drawing.Size(206, 20);
            this.eventName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя события";
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.viewPanel);
            this.rightPanel.Controls.Add(this.filterPanel);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(234, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(737, 632);
            this.rightPanel.TabIndex = 1;
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.eventsView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 100);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(737, 532);
            this.viewPanel.TabIndex = 1;
            // 
            // eventsView
            // 
            this.eventsView.AllowUserToAddRows = false;
            this.eventsView.AllowUserToDeleteRows = false;
            this.eventsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventsView.Location = new System.Drawing.Point(0, 0);
            this.eventsView.Name = "eventsView";
            this.eventsView.ReadOnly = true;
            this.eventsView.Size = new System.Drawing.Size(737, 532);
            this.eventsView.TabIndex = 0;
            this.eventsView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.eventsView_CellClick);
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.filterBox);
            this.filterPanel.Controls.Add(this.showAll);
            this.filterPanel.Controls.Add(this.eventsDate);
            this.filterPanel.Controls.Add(this.filter);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(0, 0);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(737, 100);
            this.filterPanel.TabIndex = 0;
            // 
            // showAll
            // 
            this.showAll.Location = new System.Drawing.Point(619, 15);
            this.showAll.Name = "showAll";
            this.showAll.Size = new System.Drawing.Size(106, 72);
            this.showAll.TabIndex = 2;
            this.showAll.Text = "Показать все";
            this.showAll.UseVisualStyleBackColor = true;
            this.showAll.Click += new System.EventHandler(this.showAll_Click);
            // 
            // eventsDate
            // 
            this.eventsDate.Location = new System.Drawing.Point(20, 13);
            this.eventsDate.Name = "eventsDate";
            this.eventsDate.Size = new System.Drawing.Size(161, 20);
            this.eventsDate.TabIndex = 1;
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(187, 9);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(105, 32);
            this.filter.TabIndex = 0;
            this.filter.Text = "Отфильтровать";
            this.filter.UseVisualStyleBackColor = true;
            this.filter.Click += new System.EventHandler(this.filter_Click);
            // 
            // filterBox
            // 
            this.filterBox.AutoSize = true;
            this.filterBox.Location = new System.Drawing.Point(26, 37);
            this.filterBox.Name = "filterBox";
            this.filterBox.Size = new System.Drawing.Size(95, 17);
            this.filterBox.TabIndex = 3;
            this.filterBox.Text = "Фильтровать";
            this.filterBox.UseVisualStyleBackColor = true;
            // 
            // AuditoriumEventsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 632);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.ControlsPanel);
            this.Name = "AuditoriumEventsList";
            this.Text = "Занятость аудиторий";
            this.Load += new System.EventHandler(this.AuditoriumEventsList_Load);
            this.ControlsPanel.ResumeLayout(false);
            this.ControlsPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventsView)).EndInit();
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ControlsPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox eventName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView eventsView;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.ComboBox eventTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox eventAuditorium;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button filter;
        private System.Windows.Forms.DateTimePicker eventDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker finishDate;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.CheckBox useDataSet;
        private System.Windows.Forms.DateTimePicker eventsDate;
        private System.Windows.Forms.Button showAll;
        private System.Windows.Forms.CheckBox filterBox;
    }
}