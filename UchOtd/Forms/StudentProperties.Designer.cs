using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Forms
{
    partial class StudentProperties
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
            this.ExpelledBox = new System.Windows.Forms.CheckBox();
            this.PaidLearningBox = new System.Windows.Forms.CheckBox();
            this.FromSchoolBox = new System.Windows.Forms.CheckBox();
            this.StarostaBox = new System.Windows.Forms.CheckBox();
            this.OrdersBox = new System.Windows.Forms.TextBox();
            this.OrdersLabel = new System.Windows.Forms.Label();
            this.PhoneBox = new System.Windows.Forms.TextBox();
            this.PhoneLabel = new System.Windows.Forms.Label();
            this.AddressBox = new System.Windows.Forms.TextBox();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.BirthDateBox = new System.Windows.Forms.DateTimePicker();
            this.BirthDateLabel = new System.Windows.Forms.Label();
            this.IdNumBox = new System.Windows.Forms.TextBox();
            this.IdNumLabel2 = new System.Windows.Forms.Label();
            this.IdNumLabel1 = new System.Windows.Forms.Label();
            this.PatronymicBox = new System.Windows.Forms.TextBox();
            this.PatronymicLabel = new System.Windows.Forms.Label();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.FamilyBox = new System.Windows.Forms.TextBox();
            this.FamilyLabel = new System.Windows.Forms.Label();
            this.StudentGroupLabel = new System.Windows.Forms.Label();
            this.StudentGroupBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ExpelledBox
            // 
            this.ExpelledBox.AutoSize = true;
            this.ExpelledBox.Location = new System.Drawing.Point(155, 209);
            this.ExpelledBox.Name = "ExpelledBox";
            this.ExpelledBox.Size = new System.Drawing.Size(74, 17);
            this.ExpelledBox.TabIndex = 76;
            this.ExpelledBox.Text = "Отчислен";
            this.ExpelledBox.UseVisualStyleBackColor = true;
            this.ExpelledBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // PaidLearningBox
            // 
            this.PaidLearningBox.AutoSize = true;
            this.PaidLearningBox.Location = new System.Drawing.Point(15, 209);
            this.PaidLearningBox.Name = "PaidLearningBox";
            this.PaidLearningBox.Size = new System.Drawing.Size(118, 17);
            this.PaidLearningBox.TabIndex = 75;
            this.PaidLearningBox.Text = "Платное обучение";
            this.PaidLearningBox.UseVisualStyleBackColor = true;
            this.PaidLearningBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // FromSchoolBox
            // 
            this.FromSchoolBox.AutoSize = true;
            this.FromSchoolBox.Location = new System.Drawing.Point(155, 184);
            this.FromSchoolBox.Name = "FromSchoolBox";
            this.FromSchoolBox.Size = new System.Drawing.Size(76, 17);
            this.FromSchoolBox.TabIndex = 74;
            this.FromSchoolBox.Text = "Наяновец";
            this.FromSchoolBox.UseVisualStyleBackColor = true;
            this.FromSchoolBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // StarostaBox
            // 
            this.StarostaBox.AutoSize = true;
            this.StarostaBox.Location = new System.Drawing.Point(15, 184);
            this.StarostaBox.Name = "StarostaBox";
            this.StarostaBox.Size = new System.Drawing.Size(73, 17);
            this.StarostaBox.TabIndex = 73;
            this.StarostaBox.Text = "Староста";
            this.StarostaBox.UseVisualStyleBackColor = true;
            this.StarostaBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // OrdersBox
            // 
            this.OrdersBox.Location = new System.Drawing.Point(111, 369);
            this.OrdersBox.MaxLength = 300;
            this.OrdersBox.Multiline = true;
            this.OrdersBox.Name = "OrdersBox";
            this.OrdersBox.Size = new System.Drawing.Size(192, 61);
            this.OrdersBox.TabIndex = 63;
            this.OrdersBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // OrdersLabel
            // 
            this.OrdersLabel.AutoSize = true;
            this.OrdersLabel.Location = new System.Drawing.Point(12, 391);
            this.OrdersLabel.Name = "OrdersLabel";
            this.OrdersLabel.Size = new System.Drawing.Size(53, 13);
            this.OrdersLabel.TabIndex = 71;
            this.OrdersLabel.Text = "Приказы";
            // 
            // PhoneBox
            // 
            this.PhoneBox.Location = new System.Drawing.Point(111, 302);
            this.PhoneBox.MaxLength = 200;
            this.PhoneBox.Multiline = true;
            this.PhoneBox.Name = "PhoneBox";
            this.PhoneBox.Size = new System.Drawing.Size(192, 61);
            this.PhoneBox.TabIndex = 62;
            this.PhoneBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // PhoneLabel
            // 
            this.PhoneLabel.AutoSize = true;
            this.PhoneLabel.Location = new System.Drawing.Point(12, 323);
            this.PhoneLabel.Name = "PhoneLabel";
            this.PhoneLabel.Size = new System.Drawing.Size(60, 13);
            this.PhoneLabel.TabIndex = 70;
            this.PhoneLabel.Text = "Телефоны";
            // 
            // AddressBox
            // 
            this.AddressBox.Location = new System.Drawing.Point(111, 235);
            this.AddressBox.MaxLength = 300;
            this.AddressBox.Multiline = true;
            this.AddressBox.Name = "AddressBox";
            this.AddressBox.Size = new System.Drawing.Size(192, 61);
            this.AddressBox.TabIndex = 61;
            this.AddressBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Location = new System.Drawing.Point(12, 255);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(38, 13);
            this.AddressLabel.TabIndex = 69;
            this.AddressLabel.Text = "Адрес";
            // 
            // BirthDateBox
            // 
            this.BirthDateBox.Location = new System.Drawing.Point(111, 152);
            this.BirthDateBox.Name = "BirthDateBox";
            this.BirthDateBox.Size = new System.Drawing.Size(192, 20);
            this.BirthDateBox.TabIndex = 59;
            this.BirthDateBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // BirthDateLabel
            // 
            this.BirthDateLabel.AutoSize = true;
            this.BirthDateLabel.Location = new System.Drawing.Point(12, 156);
            this.BirthDateLabel.Name = "BirthDateLabel";
            this.BirthDateLabel.Size = new System.Drawing.Size(86, 13);
            this.BirthDateLabel.TabIndex = 68;
            this.BirthDateLabel.Text = "Дата рождения";
            // 
            // IdNumBox
            // 
            this.IdNumBox.Location = new System.Drawing.Point(111, 123);
            this.IdNumBox.MaxLength = 6;
            this.IdNumBox.Name = "IdNumBox";
            this.IdNumBox.Size = new System.Drawing.Size(192, 20);
            this.IdNumBox.TabIndex = 58;
            this.IdNumBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // IdNumLabel2
            // 
            this.IdNumLabel2.AutoSize = true;
            this.IdNumLabel2.Location = new System.Drawing.Point(12, 126);
            this.IdNumLabel2.Name = "IdNumLabel2";
            this.IdNumLabel2.Size = new System.Drawing.Size(94, 13);
            this.IdNumLabel2.TabIndex = 67;
            this.IdNumLabel2.Text = "зачётной книжки";
            // 
            // IdNumLabel1
            // 
            this.IdNumLabel1.AutoSize = true;
            this.IdNumLabel1.Location = new System.Drawing.Point(12, 113);
            this.IdNumLabel1.Name = "IdNumLabel1";
            this.IdNumLabel1.Size = new System.Drawing.Size(41, 13);
            this.IdNumLabel1.TabIndex = 66;
            this.IdNumLabel1.Text = "Номер";
            // 
            // PatronymicBox
            // 
            this.PatronymicBox.Location = new System.Drawing.Point(74, 58);
            this.PatronymicBox.MaxLength = 50;
            this.PatronymicBox.Name = "PatronymicBox";
            this.PatronymicBox.Size = new System.Drawing.Size(229, 20);
            this.PatronymicBox.TabIndex = 55;
            this.PatronymicBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // PatronymicLabel
            // 
            this.PatronymicLabel.AutoSize = true;
            this.PatronymicLabel.Location = new System.Drawing.Point(12, 61);
            this.PatronymicLabel.Name = "PatronymicLabel";
            this.PatronymicLabel.Size = new System.Drawing.Size(54, 13);
            this.PatronymicLabel.TabIndex = 60;
            this.PatronymicLabel.Text = "Отчество";
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(74, 32);
            this.NameBox.MaxLength = 50;
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(229, 20);
            this.NameBox.TabIndex = 53;
            this.NameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 35);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(29, 13);
            this.NameLabel.TabIndex = 57;
            this.NameLabel.Text = "Имя";
            // 
            // FamilyBox
            // 
            this.FamilyBox.Location = new System.Drawing.Point(74, 6);
            this.FamilyBox.MaxLength = 50;
            this.FamilyBox.Name = "FamilyBox";
            this.FamilyBox.Size = new System.Drawing.Size(229, 20);
            this.FamilyBox.TabIndex = 52;
            this.FamilyBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // FamilyLabel
            // 
            this.FamilyLabel.AutoSize = true;
            this.FamilyLabel.Location = new System.Drawing.Point(12, 9);
            this.FamilyLabel.Name = "FamilyLabel";
            this.FamilyLabel.Size = new System.Drawing.Size(56, 13);
            this.FamilyLabel.TabIndex = 54;
            this.FamilyLabel.Text = "Фамилия";
            // 
            // StudentGroupLabel
            // 
            this.StudentGroupLabel.AutoSize = true;
            this.StudentGroupLabel.Location = new System.Drawing.Point(12, 93);
            this.StudentGroupLabel.Name = "StudentGroupLabel";
            this.StudentGroupLabel.Size = new System.Drawing.Size(44, 13);
            this.StudentGroupLabel.TabIndex = 72;
            this.StudentGroupLabel.Text = "Группы";
            // 
            // StudentGroupBox
            // 
            this.StudentGroupBox.Location = new System.Drawing.Point(74, 90);
            this.StudentGroupBox.MaxLength = 5;
            this.StudentGroupBox.Name = "StudentGroupBox";
            this.StudentGroupBox.ReadOnly = true;
            this.StudentGroupBox.Size = new System.Drawing.Size(229, 20);
            this.StudentGroupBox.TabIndex = 56;
            this.StudentGroupBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FamilyBox_KeyDown);
            // 
            // StudentProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 447);
            this.Controls.Add(this.ExpelledBox);
            this.Controls.Add(this.PaidLearningBox);
            this.Controls.Add(this.FromSchoolBox);
            this.Controls.Add(this.StarostaBox);
            this.Controls.Add(this.StudentGroupBox);
            this.Controls.Add(this.StudentGroupLabel);
            this.Controls.Add(this.OrdersBox);
            this.Controls.Add(this.OrdersLabel);
            this.Controls.Add(this.PhoneBox);
            this.Controls.Add(this.PhoneLabel);
            this.Controls.Add(this.AddressBox);
            this.Controls.Add(this.AddressLabel);
            this.Controls.Add(this.BirthDateBox);
            this.Controls.Add(this.BirthDateLabel);
            this.Controls.Add(this.IdNumBox);
            this.Controls.Add(this.IdNumLabel2);
            this.Controls.Add(this.IdNumLabel1);
            this.Controls.Add(this.PatronymicBox);
            this.Controls.Add(this.PatronymicLabel);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.FamilyBox);
            this.Controls.Add(this.FamilyLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StudentProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Данные студента";
            this.Load += new System.EventHandler(this.StudentPropertiesLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox ExpelledBox;
        private CheckBox PaidLearningBox;
        private CheckBox FromSchoolBox;
        private CheckBox StarostaBox;
        private TextBox OrdersBox;
        private Label OrdersLabel;
        private TextBox PhoneBox;
        private Label PhoneLabel;
        private TextBox AddressBox;
        private Label AddressLabel;
        private DateTimePicker BirthDateBox;
        private Label BirthDateLabel;
        internal TextBox IdNumBox;
        private Label IdNumLabel2;
        private Label IdNumLabel1;
        private TextBox PatronymicBox;
        private Label PatronymicLabel;
        private TextBox NameBox;
        private Label NameLabel;
        private TextBox FamilyBox;
        private Label FamilyLabel;
        private Label StudentGroupLabel;
        internal TextBox StudentGroupBox;
    }
}