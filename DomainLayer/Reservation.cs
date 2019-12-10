using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
