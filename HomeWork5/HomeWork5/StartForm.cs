using HomeWork5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork5
{
    public partial class StartForm : Form
    {
        private DataSet _dataSet { get; set; }
        private DbDataAdapter _dataAdapter { get; set; }
        private DbCommandBuilder _dbCommandBuilder { get; set; }

        public StartForm()
        {
            InitializeComponent();
            ShowUsersList();
        }

        private void usersList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void usersList_MouseDoubleClick(object sender, EventArgs e)
        {

        }
        private void ShowUsersList()
        {
            usersList.Items.Clear();
            var providerName = ConfigurationManager.ConnectionStrings["UserConnectionString"].ProviderName;
            var connectionString = ConfigurationManager.ConnectionStrings["UserConnectionString"].ConnectionString;
            var factory = DbProviderFactories.GetFactory(providerName);

            using (var connection = factory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.ConnectionString = connectionString;
                command.CommandText = "select Login from Users";
                _dataSet = new DataSet("usersDataSet");
                _dataAdapter = factory.CreateDataAdapter();
                _dataAdapter.SelectCommand = command;
                connection.Open();
                _dataAdapter.Fill(_dataSet, "Users");                
            }
            //foreach(var row in _dataSet.Tables["Users"].Rows)
            //{
            //    if(Convert.ToInt32((row as DataRow).ItemArray[(row as DataRow).Table.Columns.IndexOf("IsAdmin")])==1)
            //        usersList.Items.Add("*"+(row as DataRow).ItemArray[(row as DataRow).Table.Columns.IndexOf("Login")]);
            //    else
            //        usersList.Items.Add((row as DataRow).ItemArray[(row as DataRow).Table.Columns.IndexOf("Login")]);
            //}
            var userTable = _dataSet.Tables["Users"];
            foreach(DataRow row in userTable.Rows)
            {
                if(Convert.ToBoolean(row.Table.Columns.IndexOf("IsAdmin"))==true)
                    usersList.Items.Add("*"+row.ItemArray[row.Table.Columns.IndexOf("Login")]);
                else
                    usersList.Items.Add(row.ItemArray[row.Table.Columns.IndexOf("Login")]);
            }
        }
    }
}
