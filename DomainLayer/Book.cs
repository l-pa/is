using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Gateway;

namespace DomainLayer
{
    public class Book : IBook
    {
        public int Id { get; set; }
        public string Nazev { get; set; }
        public string Autor { get; set; }
        public string Jazyk { get; set; }
        public DateTime? RokVydani { get; set; }
        public string Isbn { get; set; }
        public string Zanr { get; set; }
        public string Vydavatel { get; set; }
        public Condition Stav { get; set; }

        private int conditionId;


        public Land Land { get; set; }

        public IReservation Reservation { get; set; }

        BookGateway bookGateway;

        public Book()
        {
            bookGateway = new BookGateway();
        }
        public Book(DTO.Book book)
        {
            Id = book.id;
            Nazev = book.nazev;
            Autor = book.autor;
            Jazyk = book.jazyk;
            RokVydani = book.rok_vydani;
            Isbn = book.ISBN;
            Zanr = book.zanr;
            Vydavatel = book.vydavatel;
            Stav = null;
            conditionId = book.stav;
            bookGateway = new BookGateway();
            Reservation = new Reservation(this);
            Land = new Land(this);

        }

        public List<IBook> FindBook(String query)
        {
            List<IBook> domainBooks = new List<IBook>();
            var books = bookGateway.findBooks(query);
           foreach (var book in books)
            {
                domainBooks.Add(new Book(book));
            }
            return domainBooks;
        }

        public IBook FindBook(int id)
        {
            List<Book> domainBooks = new List<Book>();
            var book = bookGateway.findBookById(id);
            return new Book(book);
        }

        public IBook FindAlternativeBook()
        {
            var book = bookGateway.FindBookAlternative(Nazev, Id);
            if (book == null)
            {
                return null;
            }
            return new Book(book);
        }


        public int DeleteWithReservations()
        {
            Reservation.DeleteBookReservations();
            return DeleteBook();
        }

        public string GetCondition()
        {
            return new Condition(conditionId).stav;
        }

        public int DeleteBook()
        {
            if (Land.IsLanded())
            {
                return 1;
            } else if (Reservation.IsReserved())
            {
                return 2;
            }
            else
            {
                bookGateway.deleteBook(Id);
                return 0;
            }
        }
    }
}
