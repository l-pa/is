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
        public State stav { get; set; }

        private Reader reader;
        private Land land;

        private Reservation reservation;

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

            bookGateway = new BookGateway();
            reservation = new Reservation(this, reader);
            land = new Land(this);

        }

        public List<Book> findBook(String query)
        {
            List<Book> domainBooks = new List<Book>();
            var books = bookGateway.findBook(query);
           foreach (var book in books)
            {
                domainBooks.Add(new Book(book));
            }
            return domainBooks;
        }
        public void reservateBook(Reader reader, Reservation reservation)
        {
            // Datetime from user input
            //if (bookGateway.checkBookReservation(new DateTime(2000, 2, 15), new DateTime(2002, 12, 24)))
            //{
            //    // ReservateMapper
            //} else
            //{
            //    // Show nearest available reservation / error

            //}
        }

        public void extendReservation(Reader reader, Reservation reservation)
        {
            //if (!bookGateway.checkBookReservation(reservation.endOfReservation, reservation.endOfReservation.AddDays(7)))
            //{
                
            //}
            //else
            //{
            //    // If same book exists and doesnt have reservation show dialog
            //}
        }

        public int deleteWithReservations()
        {
            reservation.deleteBookReservations();
            return deleteBook();
        }

        public int deleteBook()
        {
            if (land.isLanded())
            {
                return 1;
            } else if (reservation.isReservated())
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
