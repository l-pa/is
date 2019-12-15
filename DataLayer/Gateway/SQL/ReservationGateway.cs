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
        readonly SQLConnection _sqlDatabase = new SQLConnection();

        public List<DTO.Reservation> FindByReaderId(int id)
        {
            List<DTO.Reservation> reservations = new List<Reservation>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", id.ToString());
            var result = DatabaseTable.Query(_sqlDatabase, "SELECT * FROM rezervace WHERE ctenar_id = @id and datum_do > GETDATE()", keyValues, "rezervace"); // TODO
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Reservation reservation = new Reservation();
                reservation.id = (int)result.Rows[i].ItemArray[0];
                reservation.startOfReservation = (DateTime)result.Rows[i].ItemArray[1];
                reservation.endOfReservation = (DateTime)result.Rows[i].ItemArray[2];
                reservation.ctenar_id = (int)result.Rows[i].ItemArray[3];
                reservation.kniha_id = (int)result.Rows[i].ItemArray[4];
                reservation.dateOfReservation = DateTime.Now;
                reservations.Add(reservation);
            }
            return reservations;
        }


        public List<DTO.Reservation> FindByBookId(int id)
        {
            List<DTO.Reservation> reservations = new List<Reservation>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", id.ToString());
            var result = DatabaseTable.Query(_sqlDatabase, "SELECT * FROM rezervace WHERE kniha_id = @id and datum_do > GETDATE()", keyValues, "rezervace"); // TODO
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Reservation reservation = new Reservation();
                reservation.id = (int)result.Rows[i].ItemArray[0];
                reservation.startOfReservation = (DateTime)result.Rows[i].ItemArray[1];
                reservation.endOfReservation = (DateTime)result.Rows[i].ItemArray[2];
                reservation.ctenar_id = (int)result.Rows[i].ItemArray[3];
                reservation.kniha_id = (int)result.Rows[i].ItemArray[4];
                reservation.dateOfReservation = DateTime.Now;

                reservations.Add(reservation);
            }
            return reservations;
        }

        public List<DTO.Reservation> FindByBookIdReaderId(int id, int readerId)
        {
            List<DTO.Reservation> reservations = new List<Reservation>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", id.ToString());
            keyValues.Add("@ctenar", readerId.ToString());

            var result = DatabaseTable.Query(_sqlDatabase, "SELECT * FROM rezervace WHERE kniha_id = @id and datum_do > GETDATE() and ctenar_id = @ctenar", keyValues, "rezervace"); // TODO
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Reservation reservation = new Reservation();
                reservation.id = (int)result.Rows[i].ItemArray[0];
                reservation.startOfReservation = (DateTime)result.Rows[i].ItemArray[1];
                reservation.endOfReservation = (DateTime)result.Rows[i].ItemArray[2];
                reservation.ctenar_id = (int)result.Rows[i].ItemArray[3];
                reservation.kniha_id = (int)result.Rows[i].ItemArray[4];
                reservation.dateOfReservation = DateTime.Now;
                reservations.Add(reservation);
            }
            return reservations;
        }



        public List<DTO.Reservation> ReservedAtMoment(int bookId)
        {
            List<DTO.Reservation> reservations = new List<Reservation>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", bookId.ToString());
            var result = DatabaseTable.Query(_sqlDatabase, "select * from rezervace where kniha_id = @id and GETDATE() between datum_od and datum_do", keyValues, "rezervace");
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Reservation reservation = new Reservation();
                reservation.id = (int)result.Rows[i].ItemArray[0];
                reservation.startOfReservation = (DateTime)result.Rows[i].ItemArray[1];
                reservation.endOfReservation = (DateTime)result.Rows[i].ItemArray[2];
                reservation.ctenar_id = (int)result.Rows[i].ItemArray[3];
                reservation.kniha_id = (int)result.Rows[i].ItemArray[4];
                reservation.dateOfReservation = DateTime.Now;
                reservations.Add(reservation);
            }
            return reservations;
        }

        public List<DTO.Reservation> ReservedAtMomentBy(int bookId, int readerId)
        {
            List<DTO.Reservation> reservations = new List<Reservation>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@bookid", bookId.ToString());
            keyValues.Add("@readerId", readerId.ToString());

            var result = DatabaseTable.Query(_sqlDatabase, "select * from rezervace where kniha_id = @bookid and ctenar_id = @readerId and GETDATE() between datum_od and datum_do", keyValues, "rezervace");
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Reservation reservation = new Reservation();
                reservation.id = (int)result.Rows[i].ItemArray[0];
                reservation.startOfReservation = (DateTime)result.Rows[i].ItemArray[1];
                reservation.endOfReservation = (DateTime)result.Rows[i].ItemArray[2];
                reservation.ctenar_id = (int)result.Rows[i].ItemArray[3];
                reservation.kniha_id = (int)result.Rows[i].ItemArray[4];
                reservation.dateOfReservation = DateTime.Now;
                reservations.Add(reservation);
            }
            return reservations;
        }

        public void Insert(Reservation reservation)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@datum_od", reservation.startOfReservation.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            keyValues.Add("@datum_do", reservation.endOfReservation.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            keyValues.Add("@ctenar_id", reservation.ctenar_id.ToString());
            keyValues.Add("@kniha_id", reservation.kniha_id.ToString());

            DatabaseTable.NonQuery(_sqlDatabase, "insert into rezervace(datum_od, datum_do, ctenar_id, kniha_id) values(@datum_od, @datum_do, @ctenar_id, @kniha_id)", keyValues); // TODO
        }

        public void Delete (int bookId)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", bookId.ToString());
            DatabaseTable.NonQuery(_sqlDatabase, "delete from rezervace where kniha_id = @id", keyValues); // TODO
        }

        public List<Reservation> ReservationAfter(int bookId, DateTime endOfReservation, int days)
        {
            List<DTO.Reservation> reservations = new List<Reservation>();

            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@bookId", bookId.ToString());
            keyValues.Add("@dateTo", endOfReservation.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            keyValues.Add("@dateToS", endOfReservation.AddDays(days).ToString("yyyy-MM-dd HH:mm:ss.fff"));

            var result = DatabaseTable.Query(_sqlDatabase, "select * from rezervace where kniha_id = @bookId and datum_do > @dateTo and datum_od < @dateToS", keyValues, "rezervace"); // TODO
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Reservation reservation = new Reservation
                {
                    id = (int) result.Rows[i].ItemArray[0],
                    startOfReservation = (DateTime) result.Rows[i].ItemArray[1],
                    endOfReservation = (DateTime) result.Rows[i].ItemArray[2],
                    ctenar_id = (int) result.Rows[i].ItemArray[3],
                    kniha_id = (int) result.Rows[i].ItemArray[4],
                    dateOfReservation = DateTime.Now
                };
                reservations.Add(reservation);
            }

            return reservations;
        }
    }
}
