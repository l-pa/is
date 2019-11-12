using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DataLayer.DataTableGateway
{
    public class BookGateway
    {
        SQLDatabase sqlDatabase = new SQLDatabase();
        public BookGateway()
        {
            sqlDatabase.Connect();
        }
        public void find(int id) {
            Console.WriteLine(DatabaseTable.Query(sqlDatabase, "SELECT * from kniha where id = " + id, "kniha"));
        }
    }
}
