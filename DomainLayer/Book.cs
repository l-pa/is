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
        public string Name { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public DateTime? PublishYear { get; set; }
        public string Isbn { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public Condition Condition { get; set; }

        private int conditionId;


        public Lend Lend { get; set; }

        public IReservation Reservation { get; set; }

        BookGateway bookGateway;

        public Book()
        {
            bookGateway = new BookGateway();
        }
        public Book(DTO.Book book)
        {
            Id = book.id;
            Name = book.nazev;
            Author = book.autor;
            Language = book.jazyk;
            PublishYear = book.rok_vydani;
            Isbn = book.ISBN;
            Genre = book.zanr;
            Publisher = book.vydavatel;
            Condition = null;
            conditionId = book.stav;
            bookGateway = new BookGateway();
            Reservation = new Reservation(this);
            Lend = new Lend(this);

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
            var book = bookGateway.FindBookAlternative(Name, Id);
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
            if (Lend.IsLanded())
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
