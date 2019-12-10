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
        public int id;
        public String nazev;
        public String autor;
        public String jazyk;
        public DateTime? rok_vydani;
        public String ISBN;
        public String zanr;
        public String vydavatel;
        public State stav;

        BookGateway bookGateway = new BookGateway();

        public List<DTO.Book> findBook(String query)
        {
           return bookGateway.findBook(query);
        }
        public void reservateBook(Book book, Reader reader)
        {
            // Datetime from user input
            if (bookGateway.checkBookReservation(new DateTime(2000, 2, 15), new DateTime(2002, 12, 24)))
            {
                // ReservateMapper
            } else
            {
                // Show nearest available reservation / error

            }
        }

        public void extendReservation(Book book, Reader reader, Reservation reservation)
        {
            if (!bookGateway.checkBookReservation(reservation.endOfReservation, reservation.endOfReservation.AddDays(7)))
            {
                
            }
            else
            {
                // If same book exists and doesnt have reservation show dialog
            }
        }

        public void deleteBook(Book book)
        {
            DTO.Book book1 = new DTO.Book();
            book1.nazev = nazev;
            bookGateway.deleteBook(book1);
        }

        public int checkDeleteBook(DTO.Book book)
        {
            if (land.isLandedAtMomement(book))
            {
                return 1;
            } else if (reservation.isReservatedAtMomement(book))
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
    }
}
