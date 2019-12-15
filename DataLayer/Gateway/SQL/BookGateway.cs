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
        private static SQLConnection sqlDatabase; //  = new SQLConnection();
        private static IdentityMap<string> identityMap;
        public BookGateway()
        {
            if (sqlDatabase == null)
            {
                sqlDatabase = new SQLConnection();
                sqlDatabase.Connect();
            }
            if (identityMap == null)
            {
            identityMap = new IdentityMap<string>();
            }
        }

        public List<DTO.Book> findBooks(string query) {
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
                book.stav = (int)result.Rows[i].ItemArray[7];
                books.Add(book);
            }

            return books;
        }

        public Book findBookById(int id)
        {

            Book book = new Book();

            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", id.ToString());

            DataRow result;

            if (identityMap.Search("kniha_id", id.ToString()) != null)
            {
                result = identityMap.Search("kniha_id", id.ToString());
            }
            else
            {
                result = DatabaseTable.Query(sqlDatabase, "SELECT * FROM kniha WHERE kniha_id = @id", keyValues, "kniha").Rows[0];
            }

            book.id = (int)result.ItemArray[0];
                book.nazev = (string)result.ItemArray[1];
                book.autor = (string)result.ItemArray[2];
                book.jazyk = (string)result.ItemArray[3];
                book.rok_vydani = (DateTime)result.ItemArray[4];
                book.vydavatel = (string)result.ItemArray[5];
                book.ISBN = (string)result.ItemArray[6];
                book.zanr = (string)result.ItemArray[8];
                book.stav = (int)result.ItemArray[7];
            return book;
        }

        public Book FindBookAlternative(string bookName, int bookId)
        {
            Book book = new Book();

            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@bookid", bookName);
            keyValues.Add("@id", bookId.ToString());

            DataTable result;

            result = DatabaseTable.Query(sqlDatabase, "select *  from kniha where nazev = @bookid and kniha_id != @id", keyValues, "kniha");
            if (result.Rows.Count == 0)
            {
                return null;
            }

            book.id = (int)result.Rows[0].ItemArray[0];
            book.nazev = (string)result.Rows[0].ItemArray[1];
            book.autor = (string)result.Rows[0].ItemArray[2];
            book.jazyk = (string)result.Rows[0].ItemArray[3];
            book.rok_vydani = (DateTime)result.Rows[0].ItemArray[4];
            book.vydavatel = (string)result.Rows[0].ItemArray[5];
            book.ISBN = (string)result.Rows[0].ItemArray[6];
            book.zanr = (string)result.Rows[0].ItemArray[8];
            book.stav = (int)result.Rows[0].ItemArray[7];
            return book;
        }

        public DataTable checkBookReservation(DateTime start, DateTime end)
        {
            Dictionary<string, string> dates = new Dictionary<string, string>();
            dates.Add("%od", start.Year + "" + start.Month + "" + start.Day);
            dates.Add("%do", end.Year + "" + end.Month + "" + end.Day);
            return DatabaseTable.Query(sqlDatabase, "SELECT * FROM rezervace WHERE %od >= datum_od AND datum_do <= %do ", dates, "rezervace");
        }

        public void deleteBook(int id)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", id.ToString());
            DatabaseTable.Query(sqlDatabase, "DELETE FROM kniha WHERE kniha_id = @id", keyValues, "kniha");
            identityMap.delete("kniha_id", id);
        }
    }
}
