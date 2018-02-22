using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
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

            //var serverList = SqlInfo();

            //remoteHost.Items.Clear();
            //foreach (var server in serverList)
            //{
            //    remoteHost.Items.Add(server);
            //}

            //if (serverList.Count == 0)
            //{
            //    remoteHost.Items.Add("Сервер не найден");
            //}

            //remoteHost.SelectedIndex = 0;
        }

        //public static List<string> GetComputers()
        //{
        //    List<string> computerNames = new List<string>();

        //    using (DirectoryEntry entry = new DirectoryEntry("LDAP://local.nayanova.edu"))
        //    {
        //        using (DirectorySearcher mySearcher = new DirectorySearcher(entry))
        //        {
        //            mySearcher.Filter = ("(objectClass=computer)");

        //            // No size limit, reads all objects
        //            mySearcher.SizeLimit = 0;

        //            // Read data in pages of 250 objects. Make sure this value is below the limit configured in your AD domain (if there is a limit)
        //            mySearcher.PageSize = 250;

        //            // Let searcher know which properties are going to be used, and only load those
        //            mySearcher.PropertiesToLoad.Add("name");

        //            foreach (SearchResult resEnt in mySearcher.FindAll())
        //            {
        //                // Note: Properties can contain multiple values.
        //                if (resEnt.Properties["name"].Count > 0)
        //                {
        //                    string computerName = (string)resEnt.Properties["name"][0];
        //                    computerNames.Add(computerName);
        //                }
        //            }
        //        }
        //    }

        //    return computerNames.OrderBy(cn => cn).ToList();
        //}

        //public static List<string> SqlInfo()
        //{
        //    SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
        //    DataTable table = instance.GetDataSources();
        //    return ServerList(table);
        //}

        //private static List<string> ServerList(DataTable table)
        //{
        //    var result = new List<string>();
        //    foreach (DataRow row in table.Rows)
        //    {
        //        foreach (DataColumn dataColumn in table.Columns)
        //        {
        //            if (dataColumn.ColumnName == "ServerName")
        //            {
        //                result.Add((string)row[dataColumn]);
        //            }
        //        }
        //    }

        //    return result;
        //}

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openDatabase_Click(object sender, EventArgs e)
        {
            if (sqlExpressDB.Checked)
            {
                // Repo = new ScheduleRepository("data source=tcp:" + CurrentServerName + ",1433;Database=" + DefaultDbName + "; User ID=sa;Password=ghjuhfvvf;multipleactiveresultsets=True");
                var connectionString = "data source=tcp:" + StartupForm.CurrentServerName + ",1433; Database=" + SQLExpressDatabaseName.Text +
                    "; User ID=sa;Password=ghjuhfvvf; multipleactiveresultsets=True";

                _startupForm.Repo.SetConnectionString(connectionString);

                Close();
            }

            if (remoteDB.Checked)
            {
                // Integrated Security = SSPI;
                var connectionString = "data source=tcp:" + remoteHost.Text +  "," + PortNumber.Text + 
                    ";Database=" + remoteDatabaseName.Text +
                    "; User ID=sa;Password=ghjuhfvvf; multipleactiveresultsets=True";

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

        private void remoteHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (remoteHost.Text == "Сервер не найден")
            //{
            //    return;
            //}

            //var serverName = remoteHost.Text;

            //var dbNamesList = new List<string>();

            //using (var con = new SqlConnection("Data Source=" + serverName + "; Integrated Security=True;"))
            //{
            //    try
            //    {
            //        con.Open();
            //        DataTable databases = con.GetSchema("Databases");
            //        foreach (DataRow database in databases.Rows)
            //        {
            //            String databaseName = database.Field<String>("database_name");
            //            dbNamesList.Add(databaseName);
            //            short dbID = database.Field<short>("dbid");
            //            DateTime creationDate = database.Field<DateTime>("create_date");
            //        }
            //    }
            //    catch (Exception exception)
            //    {
                    
            //    }
            //}

            //dbNamesList = dbNamesList.OrderBy(n => n).ToList();

            //remoteDatabaseName.Items.Clear();
            //foreach (var server in dbNamesList)
            //{
            //    remoteDatabaseName.Items.Add(server);
            //}

            //if (remoteDatabaseName.Items.Count == 0)
            //{
            //    remoteDatabaseName.Items.Add("Сервер не найден");
            //}

            //remoteDatabaseName.SelectedIndex = 0;
        }
    }
}
