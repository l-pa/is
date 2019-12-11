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
            var query = DatabaseTable.Query(sqlDatabase, "INSERT INTO rezervace values ( id = @id AND ", keyValues, "rezervace"); // TODO
        }

        public List<DTO.Reservation> findById(Book book)
        {
            List<DTO.Reservation> reservations = new List<Reservation>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", book.id.ToString());
            var result = DatabaseTable.Query(sqlDatabase, "SELECT * FROM rezervace WHERE id = @id AND ", keyValues, "rezervace"); // TODO
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Reservation reservation = new Reservation();
                reservation.id = (int)result.Rows[i].ItemArray[0];
                reservation.startOfReservation = (DateTime)result.Rows[i].ItemArray[1];
                reservation.endOfReservation = (DateTime)result.Rows[i].ItemArray[2];
                reservation.ctenar_id = (int)result.Rows[i].ItemArray[3];
                reservation.kniha_id = (int)result.Rows[i].ItemArray[4];
                reservations.Add(reservation);
            }
            return reservations;
        }

        public List<DTO.Reservation> isReservated(int id)
        {
            List<DTO.Reservation> reservations = new List<Reservation>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", id.ToString());
            var result = DatabaseTable.Query(sqlDatabase, "select * from vypujcka where kniha_id = @id and GETDATE() between datum_od and datum_do", keyValues, "rezervace");
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Reservation reservation = new Reservation();
                reservation.id = (int)result.Rows[i].ItemArray[0];
                reservation.startOfReservation = (DateTime)result.Rows[i].ItemArray[1];
                reservation.endOfReservation = (DateTime)result.Rows[i].ItemArray[2];
                reservation.ctenar_id = (int)result.Rows[i].ItemArray[3];
                reservation.kniha_id = (int)result.Rows[i].ItemArray[4];
                reservations.Add(reservation);
            }
            return reservations;
        }

    public void insert(Reservation reservation)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@datum_od", reservation.startOfReservation.ToString());
            keyValues.Add("@datum_do", reservation.endOfReservation.ToString());
            keyValues.Add("@ctenar_id", reservation.ctenar_id.ToString());
            keyValues.Add("@kniha_id", reservation.kniha_id.ToString());

            DatabaseTable.NonQuery(sqlDatabase, "insert into rezervace(datum_od, datum_do, ctenar_id, kniha_id) values(@datum_od, @datum_do, @ctenar_id, @kniha_id)", keyValues); // TODO
        }

        public void delete (int bookId)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", bookId.ToString());
            DatabaseTable.NonQuery(sqlDatabase, "delete from rezervace where kniha_id = @id", keyValues); // TODO
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
