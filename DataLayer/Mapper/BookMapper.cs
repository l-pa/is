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
        private static SQLConnection sqlDatabase = new SQLConnection();
        public BookMapper()
        {
            sqlDatabase.Connect();
        }
        public List<DTO.Book> findBook(string query) {
            List<Book> books = new List<Book>();
            
            DataTable result = DatabaseTable.Query(sqlDatabase, "SELECT * FROM kniha WHERE nazev + autor + vydavatelstvi + isbn LIKE '%" + query + "%'",null, "kniha");
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
        }
        public void updateBook(Book book)
        {
        }
    }
}
