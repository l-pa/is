using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Gateway;

namespace DomainLayer
{
    public class Reservation
    {
        public int id;
        public DateTime dateOfReservation;
        public DateTime startOfReservation;
        public DateTime endOfReservation;
        public Book reservatedBook;
        public Reader reader;

        ReservationGateway reservationGateway = new ReservationGateway();
        public Reservation(Book book, Reader reader)
        {
            reservatedBook = book;
            this.reader = reader;
        }

        public bool isReservated()
        {
            if (reservationGateway.dateReservations(reservatedBook.id).Count > 0)
            {
                return true;
            } else
            {
                return false;
            }    
        }

        public void deleteBookReservations()
        {
            reservationGateway.delete(reservatedBook.id);
        }

        public void makeReservation(DateTime from, DateTime to)
        {
            DTO.Reservation reservation = new DTO.Reservation();
            reservation.ctenar_id = reader.id;
            reservation.kniha_id = reservatedBook.id;
            reservation.startOfReservation = from;
            reservation.endOfReservation = to;
            reservationGateway.insert(reservation);
        }


    }
}
