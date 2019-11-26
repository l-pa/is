using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DataLayer.Mapper;

namespace DomainLayer
{
    public class Book
    {
        BookMapper bookMapper = new BookMapper();
        public List<DTO.Book> findBook(String query)
        {
           return bookMapper.findBook(query);
        }
        public void reservateBook(Book book, Reader reader)
        {
            // Datetime from user input
            if (bookMapper.checkBookReservation(new DateTime(2000, 2, 15), new DateTime(2002, 12, 24)))
            {
                // ReservateMapper
            } else
            {
                // Show nearest available reservation / error

            }
        }

        public void extendReservation(Book book, Reader reader, Reservation reservation)
        {
            if (bookMapper.checkBookReservation(reservation.endOfReservation, reservation.endOfReservation.AddDays(7)))
            {
                // Add new reservation
            }
            else
            {
                // If same book exists and doesnt have reservation show dialog
            }
        }

        public void deleteBook(Book book)
        {
            // Check for reservation / land
        }
    }
}
