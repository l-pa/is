using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public interface IBook
    {
        List<IBook> FindBook(String query);

        IBook FindBook(int id);

        IBook FindAlternativeBook();

        int DeleteWithReservations();

        string GetCondition();

        int DeleteBook();

         int Id { get; set; }
         string Name { get; set; }
         string Author { get; set; }
         string Language { get; set; }
         DateTime? PublishYear { get; set; }
         string Isbn { get; set; }
         string Genre { get; set; }
         string Publisher { get; set; }
         Condition Condition { get; set; }

         Lend Lend { get; set; }

         IReservation Reservation { get; set; }
         }
}
