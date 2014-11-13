namespace UchOtd.Forms
{
    partial class OpenDb
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
            this.ChoosePanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.remoteDatabaseName = new System.Windows.Forms.TextBox();
            this.remoteHost = new System.Windows.Forms.TextBox();
            this.remoteDB = new System.Windows.Forms.RadioButton();
            this.DatabaseFilename = new System.Windows.Forms.TextBox();
            this.fileDatabase = new System.Windows.Forms.RadioButton();
            this.SQLExpressDatabaseName = new System.Windows.Forms.TextBox();
            this.sqlExpressDB = new System.Windows.Forms.RadioButton();
            this.actionPanel = new System.Windows.Forms.Panel();
            this.Cancel = new System.Windows.Forms.Button();
            this.openDatabase = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.PortNumber = new System.Windows.Forms.TextBox();
            this.ChoosePanel.SuspendLayout();
            this.actionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChoosePanel
            // 
            this.ChoosePanel.Controls.Add(this.PortNumber);
            this.ChoosePanel.Controls.Add(this.label3);
            this.ChoosePanel.Controls.Add(this.label2);
            this.ChoosePanel.Controls.Add(this.label1);
            this.ChoosePanel.Controls.Add(this.remoteDatabaseName);
            this.ChoosePanel.Controls.Add(this.remoteHost);
            this.ChoosePanel.Controls.Add(this.remoteDB);
            this.ChoosePanel.Controls.Add(this.DatabaseFilename);
            this.ChoosePanel.Controls.Add(this.fileDatabase);
            this.ChoosePanel.Controls.Add(this.SQLExpressDatabaseName);
            this.ChoosePanel.Controls.Add(this.sqlExpressDB);
            this.ChoosePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChoosePanel.Location = new System.Drawing.Point(0, 0);
            this.ChoosePanel.Name = "ChoosePanel";
            this.ChoosePanel.Size = new System.Drawing.Size(655, 122);
            this.ChoosePanel.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Название базы данных";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Имя сервера";
            // 
            // remoteDatabaseName
            // 
            this.remoteDatabaseName.Location = new System.Drawing.Point(359, 91);
            this.remoteDatabaseName.Name = "remoteDatabaseName";
            this.remoteDatabaseName.Size = new System.Drawing.Size(280, 20);
            this.remoteDatabaseName.TabIndex = 6;
            this.remoteDatabaseName.Enter += new System.EventHandler(this.remoteDatabaseName_Enter);
            // 
            // remoteHost
            // 
            this.remoteHost.Location = new System.Drawing.Point(359, 65);
            this.remoteHost.Name = "remoteHost";
            this.remoteHost.Size = new System.Drawing.Size(129, 20);
            this.remoteHost.TabIndex = 5;
            this.remoteHost.Enter += new System.EventHandler(this.remoteHost_Enter);
            // 
            // remoteDB
            // 
            this.remoteDB.AutoSize = true;
            this.remoteDB.Location = new System.Drawing.Point(12, 66);
            this.remoteDB.Name = "remoteDB";
            this.remoteDB.Size = new System.Drawing.Size(209, 17);
            this.remoteDB.TabIndex = 4;
            this.remoteDB.TabStop = true;
            this.remoteDB.Text = "Удалённая база данных SQLExpress";
            this.remoteDB.UseVisualStyleBackColor = true;
            // 
            // DatabaseFilename
            // 
            this.DatabaseFilename.Location = new System.Drawing.Point(227, 37);
            this.DatabaseFilename.Name = "DatabaseFilename";
            this.DatabaseFilename.Size = new System.Drawing.Size(412, 20);
            this.DatabaseFilename.TabIndex = 3;
            this.DatabaseFilename.Enter += new System.EventHandler(this.DatabaseFilename_Enter);
            // 
            // fileDatabase
            // 
            this.fileDatabase.AutoSize = true;
            this.fileDatabase.Location = new System.Drawing.Point(12, 38);
            this.fileDatabase.Name = "fileDatabase";
            this.fileDatabase.Size = new System.Drawing.Size(123, 17);
            this.fileDatabase.TabIndex = 2;
            this.fileDatabase.TabStop = true;
            this.fileDatabase.Text = "Файл базы данных";
            this.fileDatabase.UseVisualStyleBackColor = true;
            // 
            // SQLExpressDatabaseName
            // 
            this.SQLExpressDatabaseName.Location = new System.Drawing.Point(227, 11);
            this.SQLExpressDatabaseName.Name = "SQLExpressDatabaseName";
            this.SQLExpressDatabaseName.Size = new System.Drawing.Size(412, 20);
            this.SQLExpressDatabaseName.TabIndex = 1;
            this.SQLExpressDatabaseName.Enter += new System.EventHandler(this.SQLExpressDatabaseName_Enter);
            this.SQLExpressDatabaseName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SQLExpressDatabaseName_KeyPress);
            // 
            // sqlExpressDB
            // 
            this.sqlExpressDB.AutoSize = true;
            this.sqlExpressDB.Location = new System.Drawing.Point(12, 12);
            this.sqlExpressDB.Name = "sqlExpressDB";
            this.sqlExpressDB.Size = new System.Drawing.Size(209, 17);
            this.sqlExpressDB.TabIndex = 0;
            this.sqlExpressDB.TabStop = true;
            this.sqlExpressDB.Text = "Локальная база данных SQLExpress";
            this.sqlExpressDB.UseVisualStyleBackColor = true;
            // 
            // actionPanel
            // 
            this.actionPanel.Controls.Add(this.Cancel);
            this.actionPanel.Controls.Add(this.openDatabase);
            this.actionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionPanel.Location = new System.Drawing.Point(0, 122);
            this.actionPanel.Name = "actionPanel";
            this.actionPanel.Size = new System.Drawing.Size(655, 48);
            this.actionPanel.TabIndex = 4;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(359, 11);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(280, 23);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Отмена";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // openDatabase
            // 
            this.openDatabase.Location = new System.Drawing.Point(12, 11);
            this.openDatabase.Name = "openDatabase";
            this.openDatabase.Size = new System.Drawing.Size(341, 23);
            this.openDatabase.TabIndex = 0;
            this.openDatabase.Text = "Открыть";
            this.openDatabase.UseVisualStyleBackColor = true;
            this.openDatabase.Click += new System.EventHandler(this.openDatabase_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(494, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Порт";
            // 
            // PortNumber
            // 
            this.PortNumber.Location = new System.Drawing.Point(532, 63);
            this.PortNumber.Name = "PortNumber";
            this.PortNumber.Size = new System.Drawing.Size(100, 20);
            this.PortNumber.TabIndex = 10;
            this.PortNumber.Text = "1433";
            this.PortNumber.Enter += new System.EventHandler(this.PortNumber_Enter);
            // 
            // OpenDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 170);
            this.Controls.Add(this.actionPanel);
            this.Controls.Add(this.ChoosePanel);
            this.Name = "OpenDb";
            this.Text = "Выберите базу данных";
            this.Load += new System.EventHandler(this.OpenDB_Load);
            this.ChoosePanel.ResumeLayout(false);
            this.ChoosePanel.PerformLayout();
            this.actionPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ChoosePanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox remoteDatabaseName;
        private System.Windows.Forms.TextBox remoteHost;
        private System.Windows.Forms.RadioButton remoteDB;
        private System.Windows.Forms.TextBox DatabaseFilename;
        private System.Windows.Forms.RadioButton fileDatabase;
        private System.Windows.Forms.TextBox SQLExpressDatabaseName;
        private System.Windows.Forms.RadioButton sqlExpressDB;
        private System.Windows.Forms.Panel actionPanel;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button openDatabase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PortNumber;
    }
}