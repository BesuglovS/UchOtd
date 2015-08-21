using System;
using System.Windows.Forms;
using UchOtd.Properties;

namespace UchOtd.Forms
{
    public partial class OpenDb : Form
    {
        readonly StartupForm _startupForm;

        public OpenDb(StartupForm startupForm)
        {
            InitializeComponent();

            _startupForm = startupForm;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openDatabase_Click(object sender, EventArgs e)
        {
            if (sqlExpressDB.Checked)
            {
                var connectionString = "data source=tcp:" + StartupForm.CurrentServerName + ",1433; Database=" + SQLExpressDatabaseName.Text + 
                    ";User ID = " + Settings.Default.DBUserName +
                    ";Password = " + Settings.Default.DBPassword;

                _startupForm.Repo.SetConnectionString(connectionString);

                Close();
            }

            if (remoteDB.Checked)
            {
                var connectionString = "data source=tcp:" + remoteHost.Text +  "," + PortNumber.Text + 
                    ";Database=" + remoteDatabaseName.Text +
                    ";User ID = " + Settings.Default.DBUserName +
                    ";Password = " + Settings.Default.DBPassword;

                _startupForm.Repo.SetConnectionString(connectionString);

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
