using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DataLayer.Gateway
{
    public class ReservationGateway
    {
        SQLConnection sqlDatabase = new SQLConnection();

        public void addReservation(Reservation reservation)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", reservation.id.ToString());
            keyValues.Add("@od", reservation.startOfReservation.ToString());
            keyValues.Add("@do", reservation.endOfReservation.ToString());
            keyValues.Add("@co", reservation.reservatedBook.id.ToString());
            keyValues.Add("@kdy", reservation.dateOfReservation.ToString());

            var query = DatabaseTable.Query(sqlDatabase, "INSERT INTO rezervace values ( id = @id AND ", keyValues, "rezervace"); // TODO
        }

        public bool isReservatedAtMomement(Book book)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", book.id.ToString());
           var query = DatabaseTable.Query(sqlDatabase, "SELECT * FROM rezervace WHERE id = @id AND ", keyValues, "rezervace"); // TODO
            if (query.Rows.Count > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }


        public DateTime nextFreeReservation(Book book)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", book.id.ToString());
            var query = DatabaseTable.Query(sqlDatabase, "SELECT * FROM rezervace WHERE id = @id AND ", keyValues, "rezervace"); // TODO
            return DateTime.Now;
        }
    }
}
