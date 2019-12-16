using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Gateway;

namespace DomainLayer
{
    public class Reservation : IReservation
    {
        public int Id { get; set; }
        public DateTime DateOfReservation { get; set; }
        public DateTime StartOfReservation { get; set; }
        public DateTime EndOfReservation { get; set; }
        public IBook ReservatedBook { get; set; }
    public IReader Reader { get; set; }

        private ReservationGateway reservationGateway = new ReservationGateway();

        public Reservation(IReader reader)
        {
            Reader = reader;
        }
        public Reservation(IBook book, IReader reader)
        {
            ReservatedBook = book;
            Reader = reader;
        }

        public Reservation(IBook book)
        {
            ReservatedBook = book;
            Reader = new Reader(1);
        }

        public Reservation(DTO.Reservation res, IBook reservatedBook)
        {
            Id = 1;
            StartOfReservation = res.startOfReservation;
            EndOfReservation = res.endOfReservation;
            DateOfReservation = res.dateOfReservation;
            Reader = new Reader(res.ctenar_id);
            ReservatedBook = reservatedBook;
        }

        public List<IReservation> GetBookReservation()
        {
            List<IReservation> reservations = new List<IReservation>();
            foreach (DTO.Reservation reservation in reservationGateway.FindByBookId(ReservatedBook.Id))
            {
                reservations.Add(new Reservation(reservation, ReservatedBook));
            }
            return reservations;
        }

        public List<IReservation> GetReaderReservation()
        {
            List<IReservation> reservations = new List<IReservation>();
            Book book = new Book();
            foreach (DTO.Reservation reservation in reservationGateway.FindByReaderId(Reader.Id))
            {
                reservations.Add(new Reservation(reservation, book.FindBook(reservation.kniha_id)));
            }
            return reservations;
        }



        public bool IsReserved()
        {
            if (reservationGateway.ReservedAtMoment(ReservatedBook.Id).Count > 0)
            {
                return true;
            } else
            {
                return false;
            }    
        }

        public bool IsReservedByReader()
        {
            if (reservationGateway.FindByBookIdReaderId(ReservatedBook.Id, Reader.Id).Count > 0)
            {
                return true;
            }
            return false;
            }

        public void DeleteBookReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            foreach(var res in reservationGateway.FindByBookId(ReservatedBook.Id))
            {
                reservations.Add(new Reservation(res, ReservatedBook));
            }

            foreach(var reader in reservations)
            {
                reader.Reader.Notify("Rezervace knihy " + reader.ReservatedBook.Id + " byla zrusena");
            }
            reservationGateway.Delete(ReservatedBook.Id);
        }

        public int ExtendBookReservation(int days)
        {
            try
            {
                var res = reservationGateway.FindByBookIdReaderId(ReservatedBook.Id, 1);
                EndOfReservation = res[res.Count - 1].endOfReservation;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                EndOfReservation = DateTime.Today;
            }

            System.Diagnostics.Debug.WriteLine(EndOfReservation.Date);

            if (reservationGateway.ReservationAfter(ReservatedBook.Id, EndOfReservation, days).Count > 0)
            {
                var alternativeBook = ReservatedBook.FindAlternativeBook();
                if (alternativeBook == null)
                {
                    return -1;
                }
                return alternativeBook.Id;
            }

            DTO.Reservation reservation = new DTO.Reservation
            {
                ctenar_id = Reader.Id,
                endOfReservation = EndOfReservation.AddDays(days),
                kniha_id = ReservatedBook.Id,
                startOfReservation = EndOfReservation
            };

            reservationGateway.Insert(reservation);
            return 0;
        }

        public List<DateTime> MakeReservation(DateTime from, DateTime to)
        {
            List<IReservation> reservations = GetBookReservation();

            List<DateTime> daysBetween = new List<DateTime>();

            for (DateTime date = from; date <= to; date = date.AddDays(1))
            {
                daysBetween.Add(date);
            }


            foreach (var reservation in reservations)
            {
                foreach (DateTime selectedDays in daysBetween)
                {
                    if (reservation.StartOfReservation <= selectedDays && reservation.EndOfReservation >= selectedDays)
                    {
                        return FindFreeReservationDate(from, to);
                    }
                }
            }

            DTO.Reservation dtoReservation = new DTO.Reservation();
            dtoReservation.ctenar_id = Reader.Id;
            dtoReservation.kniha_id = ReservatedBook.Id;
            dtoReservation.startOfReservation = from;
            dtoReservation.endOfReservation = to;
            reservationGateway.Insert(dtoReservation);
            Reader.Notify("Kniha " + ReservatedBook.Nazev + " byla rezervovana od " + from.Date + " do " + to.Date);
            return null;
        }

        public List<DateTime> FindFreeReservationDate(DateTime startData, DateTime endDate)
        {
            List<IReservation> reservations = GetBookReservation();

            List<DateTime> dates = new List<DateTime>();

            foreach (var reservation in reservations)
            {
                for (DateTime date = reservation.StartOfReservation; date <= reservation.EndOfReservation; date = date.AddDays(1))
                {
                    dates.Add(date);
                }
            }

            var start = startData;
            var end = endDate;

            var range = Enumerable.Range(0, (int)(end - start).TotalDays + 1)
                .Select(i => start.AddDays(i));

            var missing = range.Except(dates);
            DateTime firstFree = new DateTime();

            List<DateTime> days = new List<DateTime>();
            foreach (var dateTime in missing)
            {
                days.Add(dateTime);
            }

            try
            {
                firstFree = days[0];
            }
            catch (Exception)
            {
                throw;
            }
            
            for (int i = 1; i < days.Count; i++)
            {
                if (firstFree.AddDays(i) == days[i])
                {
                    endDate = days[i];
                }
            }
            List<DateTime> res = new List<DateTime>();
            res.Add(days[0]);
            res.Add(endDate);

            return res;
        }

    }
}
