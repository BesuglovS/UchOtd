using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UchOtd.Forms
{
    public partial class OpenDB : Form
    {
        StartupForm startupForm;

        public OpenDB(StartupForm StartupForm)
        {
            InitializeComponent();

            this.startupForm = StartupForm;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openDatabase_Click(object sender, EventArgs e)
        {
            if (sqlExpressDB.Checked)
            {
                var connectionString = "data source=tcp:127.0.0.1,1433; Database=" + SQLExpressDatabaseName.Text + ";User ID = sa;Password = ghjuhfvvf";

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
    }
}
