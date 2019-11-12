using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Objects
{
    public class Reservation
    {
        public int id;
        public DateTime dateOfReservation;
        public DateTime startOfReservation;
        public DateTime endOfReservation;
        public Book reservatedBook;
    }
}
