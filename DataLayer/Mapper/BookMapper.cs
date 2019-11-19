using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using DTO;

namespace DataLayer.DataTableGateway
{
    public class BookGateway
    {
        SQLDatabase sqlDatabase = new SQLDatabase();
        public BookGateway()
        {
            sqlDatabase.Connect();
        }
        public void findBook(int id) {
            Console.WriteLine(DatabaseTable.Query(sqlDatabase, "SELECT * from kniha where id = " + id, "kniha"));
        }
        public void insertBook(Book book)
        {
        }
        public void deleteBook(Book book)
        {
        }
        public void updateBook(Book book)
        {
        }
    }
}
