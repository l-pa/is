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
         string Nazev { get; set; }
         string Autor { get; set; }
         string Jazyk { get; set; }
         DateTime? RokVydani { get; set; }
         string Isbn { get; set; }
         string Zanr { get; set; }
         string Vydavatel { get; set; }
         Condition Stav { get; set; }

         IReader Reader { get; set; }

         Land Land { get; set; }

         IReservation Reservation { get; set; }
         }
}
