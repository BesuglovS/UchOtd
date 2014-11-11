﻿using System;
using System.Windows.Forms;

namespace UchOtd.Forms
{
    public partial class OpenDB : Form
    {
        readonly StartupForm startupForm;

        public OpenDB(StartupForm StartupForm)
        {
            InitializeComponent();

            startupForm = StartupForm;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openDatabase_Click(object sender, EventArgs e)
        {
            if (sqlExpressDB.Checked)
            {
                var connectionString = "data source=tcp:127.0.0.1,1433; Database=" + SQLExpressDatabaseName.Text + 
                    ";User ID = " + Properties.Settings.Default.DBUserName +
                    ";Password = " + Properties.Settings.Default.DBPassword;

                startupForm._repo.ConnectionString = connectionString;

                Close();
            }

            if (remoteDB.Checked)
            {
                var connectionString = "data source=tcp:" + remoteHost.Text +  "," + PortNumber.Text + 
                    ";Database=" + remoteDatabaseName.Text +
                    ";User ID = " + Properties.Settings.Default.DBUserName +
                    ";Password = " + Properties.Settings.Default.DBPassword;

                startupForm._repo.ConnectionString = connectionString;

                Close();
            }
        }

        private void SQLExpressDatabaseName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                openDatabase.PerformClick();
            }
        }

        private void OpenDB_Load(object sender, EventArgs e)
        {
            SQLExpressDatabaseName.Focus();
        }

        private void SQLExpressDatabaseName_Enter(object sender, EventArgs e)
        {
            sqlExpressDB.Checked = true;
        }

        private void DatabaseFilename_Enter(object sender, EventArgs e)
        {
            fileDatabase.Checked = true;
        }

        private void remoteHost_Enter(object sender, EventArgs e)
        {
            remoteDB.Checked = true;
        }

        private void PortNumber_Enter(object sender, EventArgs e)
        {
            remoteDB.Checked = true;
        }

        private void remoteDatabaseName_Enter(object sender, EventArgs e)
        {
            remoteDB.Checked = true;
        }
    }
}
