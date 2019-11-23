using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using DTO;
using System.Data;

namespace DataLayer.Mapper
{
    public class BookMapper
    {
        public static SQLConnection sqlDatabase = new SQLConnection();
        IdentityMap<string> identityMap;
        public BookMapper()
        {
            sqlDatabase.Connect();
            identityMap = new IdentityMap<string>();
        }
        public List<DTO.Book> findBook(string query) {
            List<Book> books = new List<Book>();

            DataTable result = identityMap.identity(query);

            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Book book = new Book();
                book.id = (int)result.Rows[i].ItemArray[0];
                book.nazev = (string)result.Rows[i].ItemArray[1];
                book.autor = (string)result.Rows[i].ItemArray[2];
                book.jazyk = (string)result.Rows[i].ItemArray[3];
                book.rok_vydani = (DateTime)result.Rows[i].ItemArray[4];
                book.vydavatel = (string)result.Rows[i].ItemArray[5];
                book.ISBN = (string)result.Rows[i].ItemArray[6];
                book.zanr = (string)result.Rows[i].ItemArray[8];
                book.stav = null;
                books.Add(book);
            }

            return books;
        }
        public void insertBook(Book book)
        {
            
        }
        public void deleteBook(Book book)
        {

            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", book.id.ToString());
            DatabaseTable.Query(sqlDatabase, "DELETE FROM kniha WHERE id = @id", keyValues, "kniha");
        }
        public void updateBook(Book book)
        {

        }
    }
}
