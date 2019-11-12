using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DataLayer
{
    public class SQLDatabase
    {
        public SqlConnection connection { get; }
        private const string connectionString = "Server=den1.mssql8.gear.host;Database=visproject;User Id=visproject;Password=Visvis*;";

        public SQLDatabase()
        {
            connection = new SqlConnection(connectionString);
        }

        public void Connect()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
