﻿using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.DBLists
{
    partial class StudentList
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
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.ParseIsertStudentList = new System.Windows.Forms.Button();
            this.ParseOneStudent = new System.Windows.Forms.Button();
            this.ParsePhoneMultiline = new System.Windows.Forms.Button();
            this.jsonExport = new System.Windows.Forms.Button();
            this.ImportStudentListFromJson = new System.Windows.Forms.Button();
            this.Filter = new System.Windows.Forms.Button();
            this.showAll = new System.Windows.Forms.Button();
            this.checkZachNumberDistinction = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.studentGroups = new System.Windows.Forms.ComboBox();
            this.studentCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.OrderList = new System.Windows.Forms.TextBox();
            this.PayForThis = new System.Windows.Forms.CheckBox();
            this.NFactor = new System.Windows.Forms.CheckBox();
            this.Expelled = new System.Windows.Forms.CheckBox();
            this.Starosta = new System.Windows.Forms.CheckBox();
            this.Phone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Address = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BirthDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.ZachNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.OBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.deletewithlessons = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.FBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.StudentListView = new System.Windows.Forms.DataGridView();
            this.importExpelled = new System.Windows.Forms.CheckBox();
            this.ControlsPanel.SuspendLayout();
            this.ListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StudentListView)).BeginInit();
            this.SuspendLayout();
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Controls.Add(this.importExpelled);
            this.ControlsPanel.Controls.Add(this.ParseIsertStudentList);
            this.ControlsPanel.Controls.Add(this.ParseOneStudent);
            this.ControlsPanel.Controls.Add(this.ParsePhoneMultiline);
            this.ControlsPanel.Controls.Add(this.jsonExport);
            this.ControlsPanel.Controls.Add(this.ImportStudentListFromJson);
            this.ControlsPanel.Controls.Add(this.Filter);
            this.ControlsPanel.Controls.Add(this.showAll);
            this.ControlsPanel.Controls.Add(this.checkZachNumberDistinction);
            this.ControlsPanel.Controls.Add(this.label9);
            this.ControlsPanel.Controls.Add(this.studentGroups);
            this.ControlsPanel.Controls.Add(this.studentCombo);
            this.ControlsPanel.Controls.Add(this.label8);
            this.ControlsPanel.Controls.Add(this.OrderList);
            this.ControlsPanel.Controls.Add(this.PayForThis);
            this.ControlsPanel.Controls.Add(this.NFactor);
            this.ControlsPanel.Controls.Add(this.Expelled);
            this.ControlsPanel.Controls.Add(this.Starosta);
            this.ControlsPanel.Controls.Add(this.Phone);
            this.ControlsPanel.Controls.Add(this.label7);
            this.ControlsPanel.Controls.Add(this.Address);
            this.ControlsPanel.Controls.Add(this.label6);
            this.ControlsPanel.Controls.Add(this.BirthDate);
            this.ControlsPanel.Controls.Add(this.label5);
            this.ControlsPanel.Controls.Add(this.ZachNumber);
            this.ControlsPanel.Controls.Add(this.label4);
            this.ControlsPanel.Controls.Add(this.OBox);
            this.ControlsPanel.Controls.Add(this.label3);
            this.ControlsPanel.Controls.Add(this.IBox);
            this.ControlsPanel.Controls.Add(this.label2);
            this.ControlsPanel.Controls.Add(this.deletewithlessons);
            this.ControlsPanel.Controls.Add(this.remove);
            this.ControlsPanel.Controls.Add(this.update);
            this.ControlsPanel.Controls.Add(this.add);
            this.ControlsPanel.Controls.Add(this.FBox);
            this.ControlsPanel.Controls.Add(this.label1);
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ControlsPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(230, 821);
            this.ControlsPanel.TabIndex = 10;
            // 
            // ParseIsertStudentList
            // 
            this.ParseIsertStudentList.Location = new System.Drawing.Point(116, 743);
            this.ParseIsertStudentList.Name = "ParseIsertStudentList";
            this.ParseIsertStudentList.Size = new System.Drawing.Size(104, 23);
            this.ParseIsertStudentList.TabIndex = 70;
            this.ParseIsertStudentList.Text = "Вставить всех";
            this.ParseIsertStudentList.UseVisualStyleBackColor = true;
            this.ParseIsertStudentList.Click += new System.EventHandler(this.ParseIsertStudentList_Click);
            // 
            // ParseOneStudent
            // 
            this.ParseOneStudent.Location = new System.Drawing.Point(16, 743);
            this.ParseOneStudent.Name = "ParseOneStudent";
            this.ParseOneStudent.Size = new System.Drawing.Size(94, 23);
            this.ParseOneStudent.TabIndex = 69;
            this.ParseOneStudent.Text = "Вставить 1";
            this.ParseOneStudent.UseVisualStyleBackColor = true;
            this.ParseOneStudent.Click += new System.EventHandler(this.ParseOneStudent_Click);
            // 
            // ParsePhoneMultiline
            // 
            this.ParsePhoneMultiline.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ParsePhoneMultiline.Location = new System.Drawing.Point(180, 315);
            this.ParsePhoneMultiline.Name = "ParsePhoneMultiline";
            this.ParsePhoneMultiline.Size = new System.Drawing.Size(36, 20);
            this.ParsePhoneMultiline.TabIndex = 68;
            this.ParsePhoneMultiline.Text = "Parse";
            this.ParsePhoneMultiline.UseVisualStyleBackColor = true;
            this.ParsePhoneMultiline.Click += new System.EventHandler(this.ParsePhoneMultiline_Click);
            // 
            // jsonExport
            // 
            this.jsonExport.Location = new System.Drawing.Point(14, 714);
            this.jsonExport.Name = "jsonExport";
            this.jsonExport.Size = new System.Drawing.Size(206, 23);
            this.jsonExport.TabIndex = 67;
            this.jsonExport.Text = "Экспортировать в JSON";
            this.jsonExport.UseVisualStyleBackColor = true;
            this.jsonExport.Click += new System.EventHandler(this.jsonExport_Click);
            // 
            // ImportStudentListFromJson
            // 
            this.ImportStudentListFromJson.Location = new System.Drawing.Point(14, 685);
            this.ImportStudentListFromJson.Name = "ImportStudentListFromJson";
            this.ImportStudentListFromJson.Size = new System.Drawing.Size(206, 23);
            this.ImportStudentListFromJson.TabIndex = 66;
            this.ImportStudentListFromJson.Text = "Импортировать из JSON";
            this.ImportStudentListFromJson.UseVisualStyleBackColor = true;
            this.ImportStudentListFromJson.Click += new System.EventHandler(this.ImportStudentListFromJson_Click);
            // 
            // Filter
            // 
            this.Filter.Location = new System.Drawing.Point(192, 23);
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(28, 23);
            this.Filter.TabIndex = 65;
            this.Filter.Text = "F";
            this.Filter.UseVisualStyleBackColor = true;
            this.Filter.Click += new System.EventHandler(this.Filter_Click);
            // 
            // showAll
            // 
            this.showAll.Location = new System.Drawing.Point(14, 656);
            this.showAll.Name = "showAll";
            this.showAll.Size = new System.Drawing.Size(206, 23);
            this.showAll.TabIndex = 64;
            this.showAll.Text = "Показать всех";
            this.showAll.UseVisualStyleBackColor = true;
            this.showAll.Click += new System.EventHandler(this.showAll_Click);
            // 
            // checkZachNumberDistinction
            // 
            this.checkZachNumberDistinction.AutoSize = true;
            this.checkZachNumberDistinction.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkZachNumberDistinction.Location = new System.Drawing.Point(15, 634);
            this.checkZachNumberDistinction.Name = "checkZachNumberDistinction";
            this.checkZachNumberDistinction.Size = new System.Drawing.Size(201, 16);
            this.checkZachNumberDistinction.TabIndex = 63;
            this.checkZachNumberDistinction.Text = "Проверять уникальность номера зачётки";
            this.checkZachNumberDistinction.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 610);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 62;
            this.label9.Text = "Группа";
            // 
            // studentGroups
            // 
            this.studentGroups.FormattingEnabled = true;
            this.studentGroups.Location = new System.Drawing.Point(86, 607);
            this.studentGroups.Name = "studentGroups";
            this.studentGroups.Size = new System.Drawing.Size(130, 21);
            this.studentGroups.TabIndex = 61;
            this.studentGroups.SelectedIndexChanged += new System.EventHandler(this.studentGroups_SelectedIndexChanged);
            // 
            // studentCombo
            // 
            this.studentCombo.FormattingEnabled = true;
            this.studentCombo.Location = new System.Drawing.Point(15, 580);
            this.studentCombo.Name = "studentCombo";
            this.studentCombo.Size = new System.Drawing.Size(205, 21);
            this.studentCombo.TabIndex = 60;
            this.studentCombo.SelectedIndexChanged += new System.EventHandler(this.studentCombo_SelectedIndexChanged);
            this.studentCombo.TextChanged += new System.EventHandler(this.studentCombo_TextChanged);
            this.studentCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.studentCombo_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 392);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 59;
            this.label8.Text = "Приказы";
            // 
            // OrderList
            // 
            this.OrderList.Location = new System.Drawing.Point(16, 408);
            this.OrderList.Multiline = true;
            this.OrderList.Name = "OrderList";
            this.OrderList.Size = new System.Drawing.Size(196, 88);
            this.OrderList.TabIndex = 48;
            // 
            // PayForThis
            // 
            this.PayForThis.AutoSize = true;
            this.PayForThis.Location = new System.Drawing.Point(98, 372);
            this.PayForThis.Name = "PayForThis";
            this.PayForThis.Size = new System.Drawing.Size(118, 17);
            this.PayForThis.TabIndex = 47;
            this.PayForThis.Text = "Платное обучение";
            this.PayForThis.UseVisualStyleBackColor = true;
            // 
            // NFactor
            // 
            this.NFactor.AutoSize = true;
            this.NFactor.Location = new System.Drawing.Point(98, 349);
            this.NFactor.Name = "NFactor";
            this.NFactor.Size = new System.Drawing.Size(76, 17);
            this.NFactor.TabIndex = 45;
            this.NFactor.Text = "Наяновец";
            this.NFactor.UseVisualStyleBackColor = true;
            // 
            // Expelled
            // 
            this.Expelled.AutoSize = true;
            this.Expelled.Location = new System.Drawing.Point(19, 372);
            this.Expelled.Name = "Expelled";
            this.Expelled.Size = new System.Drawing.Size(74, 17);
            this.Expelled.TabIndex = 44;
            this.Expelled.Text = "Отчислен";
            this.Expelled.UseVisualStyleBackColor = true;
            // 
            // Starosta
            // 
            this.Starosta.AutoSize = true;
            this.Starosta.Location = new System.Drawing.Point(19, 349);
            this.Starosta.Name = "Starosta";
            this.Starosta.Size = new System.Drawing.Size(73, 17);
            this.Starosta.TabIndex = 43;
            this.Starosta.Text = "Староста";
            this.Starosta.UseVisualStyleBackColor = true;
            // 
            // Phone
            // 
            this.Phone.Location = new System.Drawing.Point(15, 315);
            this.Phone.Name = "Phone";
            this.Phone.Size = new System.Drawing.Size(159, 20);
            this.Phone.TabIndex = 42;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 299);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Телефон";
            // 
            // Address
            // 
            this.Address.Location = new System.Drawing.Point(15, 220);
            this.Address.Multiline = true;
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(198, 76);
            this.Address.TabIndex = 41;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "Адрес";
            // 
            // BirthDate
            // 
            this.BirthDate.Location = new System.Drawing.Point(15, 181);
            this.BirthDate.Name = "BirthDate";
            this.BirthDate.Size = new System.Drawing.Size(197, 20);
            this.BirthDate.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Дата рождения";
            // 
            // ZachNumber
            // 
            this.ZachNumber.Location = new System.Drawing.Point(14, 142);
            this.ZachNumber.Name = "ZachNumber";
            this.ZachNumber.Size = new System.Drawing.Size(198, 20);
            this.ZachNumber.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "Номер зачётной книжки";
            // 
            // OBox
            // 
            this.OBox.Location = new System.Drawing.Point(15, 103);
            this.OBox.Name = "OBox";
            this.OBox.Size = new System.Drawing.Size(198, 20);
            this.OBox.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Отчество";
            // 
            // IBox
            // 
            this.IBox.Location = new System.Drawing.Point(15, 64);
            this.IBox.Name = "IBox";
            this.IBox.Size = new System.Drawing.Size(198, 20);
            this.IBox.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Имя";
            // 
            // deletewithlessons
            // 
            this.deletewithlessons.Location = new System.Drawing.Point(14, 531);
            this.deletewithlessons.Name = "deletewithlessons";
            this.deletewithlessons.Size = new System.Drawing.Size(206, 43);
            this.deletewithlessons.TabIndex = 52;
            this.deletewithlessons.Text = "Удалить вместе с членством в группах";
            this.deletewithlessons.UseVisualStyleBackColor = true;
            this.deletewithlessons.Click += new System.EventHandler(this.deletewithlessons_Click);
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(158, 502);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(62, 23);
            this.remove.TabIndex = 51;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(86, 502);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(66, 23);
            this.update.TabIndex = 50;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(14, 502);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(66, 23);
            this.add.TabIndex = 49;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // FBox
            // 
            this.FBox.Location = new System.Drawing.Point(15, 25);
            this.FBox.Name = "FBox";
            this.FBox.Size = new System.Drawing.Size(171, 20);
            this.FBox.TabIndex = 36;
            this.FBox.TextChanged += new System.EventHandler(this.FBox_TextChanged);
            this.FBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Фамилия";
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.StudentListView);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(230, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(968, 821);
            this.ListPanel.TabIndex = 11;
            // 
            // StudentListView
            // 
            this.StudentListView.AllowUserToAddRows = false;
            this.StudentListView.AllowUserToDeleteRows = false;
            this.StudentListView.AllowUserToResizeColumns = false;
            this.StudentListView.AllowUserToResizeRows = false;
            this.StudentListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StudentListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StudentListView.Location = new System.Drawing.Point(0, 0);
            this.StudentListView.Name = "StudentListView";
            this.StudentListView.ReadOnly = true;
            this.StudentListView.RowHeadersVisible = false;
            this.StudentListView.Size = new System.Drawing.Size(968, 821);
            this.StudentListView.TabIndex = 1;
            this.StudentListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StudentListView_CellClick);
            // 
            // importExpelled
            // 
            this.importExpelled.AutoSize = true;
            this.importExpelled.Location = new System.Drawing.Point(16, 774);
            this.importExpelled.Name = "importExpelled";
            this.importExpelled.Size = new System.Drawing.Size(94, 17);
            this.importExpelled.TabIndex = 71;
            this.importExpelled.Text = "Отчисленные";
            this.importExpelled.UseVisualStyleBackColor = true;
            // 
            // StudentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 821);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.ControlsPanel);
            this.Name = "StudentList";
            this.Text = "Студенты";
            this.Load += new System.EventHandler(this.StudentForm_Load);
            this.ControlsPanel.ResumeLayout(false);
            this.ControlsPanel.PerformLayout();
            this.ListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StudentListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel ControlsPanel;
        private Label label8;
        private TextBox OrderList;
        private CheckBox PayForThis;
        private CheckBox NFactor;
        private CheckBox Expelled;
        private CheckBox Starosta;
        private TextBox Phone;
        private Label label7;
        private TextBox Address;
        private Label label6;
        private DateTimePicker BirthDate;
        private Label label5;
        private TextBox ZachNumber;
        private Label label4;
        private TextBox OBox;
        private Label label3;
        private TextBox IBox;
        private Label label2;
        private Button deletewithlessons;
        private Button remove;
        private Button update;
        private Button add;
        private TextBox FBox;
        private Label label1;
        private Panel ListPanel;
        private DataGridView StudentListView;
        private ComboBox studentCombo;
        private Label label9;
        private ComboBox studentGroups;
        private CheckBox checkZachNumberDistinction;
        private Button showAll;
        private Button Filter;
        private Button ImportStudentListFromJson;
        private Button jsonExport;
        private Button ParsePhoneMultiline;
        private Button ParseIsertStudentList;
        private Button ParseOneStudent;
        private CheckBox importExpelled;
    }
}