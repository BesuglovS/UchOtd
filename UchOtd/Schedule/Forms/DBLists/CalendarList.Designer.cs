using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.DBLists
{
    partial class CalendarList
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
            this.CalendarListView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.calendarDate = new System.Windows.Forms.DateTimePicker();
            this.deletewithlessons = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.finishDate = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.calendarState = new System.Windows.Forms.ComboBox();
            this.transferDate = new System.Windows.Forms.DateTimePicker();
            this.transferDates = new System.Windows.Forms.Button();
            this.ListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalendarListView)).BeginInit();
            this.SuspendLayout();
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.CalendarListView);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ListPanel.Location = new System.Drawing.Point(254, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(269, 485);
            this.ListPanel.TabIndex = 0;
            // 
            // CalendarListView
            // 
            this.CalendarListView.AllowUserToAddRows = false;
            this.CalendarListView.AllowUserToDeleteRows = false;
            this.CalendarListView.AllowUserToResizeColumns = false;
            this.CalendarListView.AllowUserToResizeRows = false;
            this.CalendarListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CalendarListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalendarListView.Location = new System.Drawing.Point(0, 0);
            this.CalendarListView.Name = "CalendarListView";
            this.CalendarListView.ReadOnly = true;
            this.CalendarListView.RowHeadersVisible = false;
            this.CalendarListView.Size = new System.Drawing.Size(269, 485);
            this.CalendarListView.TabIndex = 0;
            this.CalendarListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CalendarListView_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Дата";
            // 
            // calendarDate
            // 
            this.calendarDate.Location = new System.Drawing.Point(21, 31);
            this.calendarDate.Name = "calendarDate";
            this.calendarDate.Size = new System.Drawing.Size(162, 20);
            this.calendarDate.TabIndex = 2;
            // 
            // deletewithlessons
            // 
            this.deletewithlessons.Location = new System.Drawing.Point(21, 144);
            this.deletewithlessons.Name = "deletewithlessons";
            this.deletewithlessons.Size = new System.Drawing.Size(171, 23);
            this.deletewithlessons.TabIndex = 10;
            this.deletewithlessons.Text = "Удалить вместе с занятиями";
            this.deletewithlessons.UseVisualStyleBackColor = true;
            this.deletewithlessons.Click += new System.EventHandler(this.deletewithlessons_Click);
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(172, 115);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(58, 23);
            this.remove.TabIndex = 9;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(97, 115);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(69, 23);
            this.update.TabIndex = 8;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(22, 115);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(69, 23);
            this.add.TabIndex = 7;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // startDate
            // 
            this.startDate.Location = new System.Drawing.Point(69, 225);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(162, 20);
            this.startDate.TabIndex = 11;
            this.startDate.Value = new System.DateTime(2013, 9, 2, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Начало";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Конец";
            // 
            // finishDate
            // 
            this.finishDate.Location = new System.Drawing.Point(69, 251);
            this.finishDate.Name = "finishDate";
            this.finishDate.Size = new System.Drawing.Size(162, 20);
            this.finishDate.TabIndex = 14;
            this.finishDate.Value = new System.DateTime(2013, 12, 31, 0, 0, 0, 0);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 196);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(210, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Добавить все даты из диапазона";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Статус";
            // 
            // calendarState
            // 
            this.calendarState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.calendarState.FormattingEnabled = true;
            this.calendarState.Location = new System.Drawing.Point(22, 72);
            this.calendarState.Name = "calendarState";
            this.calendarState.Size = new System.Drawing.Size(161, 21);
            this.calendarState.TabIndex = 18;
            // 
            // transferDate
            // 
            this.transferDate.Location = new System.Drawing.Point(69, 296);
            this.transferDate.Name = "transferDate";
            this.transferDate.Size = new System.Drawing.Size(162, 20);
            this.transferDate.TabIndex = 19;
            this.transferDate.Value = new System.DateTime(2013, 12, 31, 0, 0, 0, 0);
            // 
            // transferDates
            // 
            this.transferDates.Location = new System.Drawing.Point(16, 322);
            this.transferDates.Name = "transferDates";
            this.transferDates.Size = new System.Drawing.Size(215, 23);
            this.transferDates.TabIndex = 20;
            this.transferDates.Text = "Перенести все даты начиная с данной";
            this.transferDates.UseVisualStyleBackColor = true;
            this.transferDates.Click += new System.EventHandler(this.transferDates_Click);
            // 
            // CalendarList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 485);
            this.Controls.Add(this.transferDates);
            this.Controls.Add(this.transferDate);
            this.Controls.Add(this.calendarState);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.finishDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.startDate);
            this.Controls.Add(this.deletewithlessons);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.update);
            this.Controls.Add(this.add);
            this.Controls.Add(this.calendarDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalendarList";
            this.Text = "Даты семестра";
            this.Load += new System.EventHandler(this.CalendarList_Load);
            this.ListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CalendarListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel ListPanel;
        private DataGridView CalendarListView;
        private Label label1;
        private DateTimePicker calendarDate;
        private Button deletewithlessons;
        private Button remove;
        private Button update;
        private Button add;
        private DateTimePicker startDate;
        private Label label3;
        private Label label4;
        private DateTimePicker finishDate;
        private Button button1;
        private Label label2;
        private ComboBox calendarState;
        private DateTimePicker transferDate;
        private Button transferDates;
    }
}