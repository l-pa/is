using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Gateway;

namespace DomainLayer
{
    public class Reservation
    {
        public int Id = 1;
        public DateTime DateOfReservation;
        public DateTime StartOfReservation;
        public DateTime EndOfReservation;
        public Book ReservatedBook;
        public Reader Reader;

        private ReservationGateway reservationGateway = new ReservationGateway();

        public Reservation(Reader reader)
        {
            Reader = reader;
        }
        public Reservation(Book book, Reader reader)
        {
            ReservatedBook = book;
            Reader = reader;
        }

        public Reservation(DTO.Reservation res, Book reservatedBook)
        {
            Id = res.id;
            StartOfReservation = res.startOfReservation;
            EndOfReservation = res.endOfReservation;
            DateOfReservation = res.dateOfReservation;
            Reader = new Reader();
            ReservatedBook = reservatedBook;
        }

        public List<Reservation> GetBookReservation()
        {
            List<Reservation> reservations = new List<Reservation>();
            foreach (DTO.Reservation reservation in reservationGateway.FindByBookId(ReservatedBook.id))
            {
                reservations.Add(new Reservation(reservation, ReservatedBook));
            }
            return reservations;
        }

        public List<Reservation> GetReaderReservation()
        {
            List<Reservation> reservations = new List<Reservation>();
            Book book = new Book();
            foreach (DTO.Reservation reservation in reservationGateway.FindByReaderId(Reader.Id))
            {
                reservations.Add(new Reservation(reservation, book.FindBook(reservation.kniha_id)));
            }
            return reservations;
        }



        public bool IsReserved()
        {
            if (reservationGateway.ReservedAtMoment(ReservatedBook.id).Count > 0)
            {
                return true;
            } else
            {
                return false;
            }    
        }

        public bool IsReservedByReader()
        {
            if (reservationGateway.FindByBookIdReaderId(ReservatedBook.id, Reader.Id).Count > 0)
            {
                return true;
            }
            return false;
            }

        public void DeleteBookReservations()
        {
            reservationGateway.Delete(ReservatedBook.id);
        }

        public int ExtendBookReservation(int days)
        {
            EndOfReservation = reservationGateway.FindByBookIdReaderId(ReservatedBook.id, 1)[0].endOfReservation;

            System.Diagnostics.Debug.WriteLine(EndOfReservation.Date);

            if (reservationGateway.ReservationAfter(ReservatedBook.id, EndOfReservation, days).Count > 0)
            {
                var alternativeBook = ReservatedBook.FindAlternativeBook();
                if (alternativeBook == null)
                {
                    return -1;
                }
                return alternativeBook.id;
            }

            DTO.Reservation reservation = new DTO.Reservation
            {
                ctenar_id = Reader.Id,
                endOfReservation = EndOfReservation.AddDays(days),
                kniha_id = ReservatedBook.id,
                startOfReservation = EndOfReservation
            };

            reservationGateway.Insert(reservation);
            return 0;
        }

        public List<DateTime> MakeReservation(DateTime from, DateTime to)
        {
            List<Reservation> reservations = GetBookReservation();

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
            dtoReservation.kniha_id = ReservatedBook.id;
            dtoReservation.startOfReservation = from;
            dtoReservation.endOfReservation = to;
            reservationGateway.Insert(dtoReservation);
            ReservatedBook.reader.Notify("Kniha rezervovana");
            return null;
        }

        public List<DateTime> FindFreeReservationDate(DateTime startData, DateTime endDate)
        {
            List<Reservation> reservations = GetBookReservation();

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
            DateTime endFree = new DateTime();

            List<DateTime> days = new List<DateTime>();
            foreach (var dateTime in missing)
            {
                days.Add(dateTime);
            }
            
            firstFree = days[0];

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
