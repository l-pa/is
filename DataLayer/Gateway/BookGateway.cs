using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using DTO;
using System.Data;

namespace DataLayer.Gateway
{
    public class BookGateway
    {
        SQLConnection sqlDatabase = new SQLConnection();
        IdentityMap<string> identityMap;
        public BookGateway()
        {
            sqlDatabase.Connect();
            identityMap = new IdentityMap<string>();
        }
        public List<DTO.Book> findBook(string query) {
            List<Book> books = new List<Book>();
            
            if (!identityMap.dictionary.ContainsKey(query))
            {
                DataTable queryResult = DatabaseTable.Query(sqlDatabase, "SELECT * FROM kniha WHERE nazev + autor + vydavatelstvi + isbn LIKE '%" + query + "%'", null, "kniha");
                identityMap.dictionary[query] = queryResult;
            }
            DataTable result = identityMap.dictionary[query];

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

        public DataTable checkBookReservation(DateTime start, DateTime end)
        {
            Dictionary<string, string> dates = new Dictionary<string, string>();
            dates.Add("%od", start.Year + "" + start.Month + "" + start.Day);
            dates.Add("%do", end.Year + "" + end.Month + "" + end.Day);
            return DatabaseTable.Query(sqlDatabase, "SELECT * FROM rezervace WHERE %od >= datum_od AND datum_do <= %do ", dates, "rezervace");
        }
        public void insertBook(Book book)
        {
            
        }
        public void deleteBook(int id)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", id.ToString());
            DatabaseTable.Query(sqlDatabase, "DELETE FROM kniha WHERE id = @id", keyValues, "kniha");
        }
        public void updateBook(Book book)
        {

        }
    }
}
