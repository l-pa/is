using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class DatabaseTable
    {
        public static DataTable Query(SQLDatabase database, string sqlString, string table)
        {
            return Query(database, sqlString, null, table);
        }

        public static DataTable Query(SQLDatabase database, string sqlString, Dictionary<string, string> arguments, string table)
        {
            DataTable records;

            database.Connect();
            using (var command = PrepareCommand(database, sqlString, arguments))
            {
                using (var adapter = new SqlDataAdapter(command))
                {
                    using (var ds = new DataSet())
                    {
                        adapter.Fill(ds, table);
                        records = ds.Tables[table];
                    }
                }
            }
            database.Close();

            return records;
        }

        public static int NonQuery(SQLDatabase database, string sqlString)
        {
            return NonQuery(database, sqlString, null);
        }

        public static int NonQuery(SQLDatabase database, string sqlString, Dictionary<string, string> args)
        {
            int rowNumber;

            database.Connect();
            using (var command = PrepareCommand(database, sqlString, args))
            {
                rowNumber = command.ExecuteNonQuery();
            }
            database.Close();

            return rowNumber;
        }

        public static object Scalar(SQLDatabase database, string sqlString)
        {
            return Scalar(database, sqlString, null);
        }
        public static object Scalar(SQLDatabase database, string sqlString, Dictionary<string, string> args)
        {
            object result;

            database.Connect();
            using (var command = PrepareCommand(database, sqlString, args))
            {
                result = command.ExecuteScalar();
            }
            database.Close();

            return result;
        }

        private static SqlCommand PrepareCommand(SQLDatabase database, string sqlString, Dictionary<string, string> args)
        {
            var command = new SqlCommand(sqlString, database.connection);
            if (args == null) return command;

            foreach (var item in args)
            {
                command.Parameters.AddWithValue(item.Key, (object)item.Value ?? DBNull.Value);
            }

            return command;
        }
    }
}
