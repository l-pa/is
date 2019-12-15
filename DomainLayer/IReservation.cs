using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public interface IReservation
    {
        int Id { get; set; }
        DateTime DateOfReservation { get; set; }
        DateTime StartOfReservation { get; set; }
        DateTime EndOfReservation { get; set; }
        IBook ReservatedBook { get; set; }
        IReader Reader { get; set; }
        void DeleteBookReservations();

        bool IsReserved();
        List<DateTime> MakeReservation(DateTime from, DateTime to);

        int ExtendBookReservation(int days);

        bool IsReservedByReader();

        List<IReservation> GetReaderReservation();

        List<IReservation> GetBookReservation();


    }
}
