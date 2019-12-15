using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Gateway;

namespace DomainLayer
{
    public class Book
    {
        public int id { get; set; }
        public string nazev { get; set; }
        public string autor { get; set; }
        public string jazyk { get; set; }
        public DateTime? rok_vydani { get; set; }
        public string ISBN { get; set; }
        public string zanr { get; set; }
        public string vydavatel { get; set; }
        public Condition stav { get; set; }

        private int conditionId;

        public Reader reader = new Reader();

        public Land land;

        public Reservation reservation;

        BookGateway bookGateway;
        public Book()
        {
            bookGateway = new BookGateway();
        }
        public Book(DTO.Book book)
        {
            id = book.id;
            nazev = book.nazev;
            autor = book.autor;
            jazyk = book.jazyk;
            rok_vydani = book.rok_vydani;
            ISBN = book.ISBN;
            zanr = book.zanr;
            vydavatel = book.vydavatel;
            stav = null;
            conditionId = book.stav;
            bookGateway = new BookGateway();
            reservation = new Reservation(this, reader);
            land = new Land(this);

        }

        public List<Book> FindBook(String query)
        {
            List<Book> domainBooks = new List<Book>();
            var books = bookGateway.findBooks(query);
           foreach (var book in books)
            {
                domainBooks.Add(new Book(book));
            }
            return domainBooks;
        }

        public Book FindBook(int id)
        {
            List<Book> domainBooks = new List<Book>();
            var book = bookGateway.findBookById(id);
            return new Book(book);
        }

        public Book FindAlternativeBook()
        {
            var book = bookGateway.FindBookAlternative(nazev, id);
            if (book == null)
            {
                return null;
            }
            return new Book(book);
        }


        public int DeleteWithReservations()
        {
            reservation.DeleteBookReservations();
            return DeleteBook();
        }

        public string GetCondition()
        {
            return new Condition(conditionId).stav;
        }

        public int DeleteBook()
        {
            if (land.IsLanded())
            {
                return 1;
            } else if (reservation.IsReserved())
            {
                return 2;
            }
            else
            {
                bookGateway.deleteBook(id);
                return 0;
            }
        }
    }
}
