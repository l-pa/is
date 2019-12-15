using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DomainLayer;

namespace Web
{
    public partial class Detail : Page
    {
        private Book book;
        private List<Reservation> reservations;
        private List<DateTime> dateTimes = new List<DateTime>();

        private int alternativeBookId;

        private Book nextReservedBook;

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void reservateButton_Click(object sender, EventArgs e)
        {
            showCalendar.Visible = true;
        }

        protected void reservateButtonExtend_Click(object sender, EventArgs e)
        {
            successMessage.Visible = false;
            errorMessage.Visible = false;
            promptMessage.Visible = false;

            var tmp = book.reservation.ExtendBookReservation(7);
            
            Session["tmp"] = tmp;

            System.Diagnostics.Debug.WriteLine(tmp + " tmp");

            switch (tmp)
            {
                case 0:
                    successMessage.Visible = true;
                    successMessageText.Text = "Rezervace " + book.nazev + " uspesne prodlouzena.";
                    break;
                case -1:
                    errorMessage.Visible = true;
                    errorMessageText.Text = "Rezervace " + book.nazev + " neuspech.";
                    break;
                default:
                    promptMessage.Visible = true;
                    promptMessageText.Text = "Je mozno rezervovat nasledujici knihu " + book.nazev + " chcete ji rezervovat?";
                    extendNextBook.Visible = true;
                    cancelExtendNextBook.Visible = true;
                    alternativeBookId = tmp;
                    break;
            }
        }

        protected void canceButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Search.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            book = (Book) Session["book"];
            if (book != null)
            {
                reservations = new List<Reservation>();
                reservations = book.reservation.GetBookReservation();

                title.InnerText = book.nazev;

                autor.InnerText = book.autor;
                nazev.InnerText = book.nazev;
                stav.InnerText = book.GetCondition();
                isbn.InnerText = book.ISBN;
                zanr.InnerText = book.zanr;
                jazyk.InnerText = book.jazyk;

                if (book.land.IsLanded())
                {
                    vypujcena.InnerText = "❌";
                }
                else
                {
                    vypujcena.InnerText = "✅";
                }

                if (book.reservation.IsReserved())
                {
                    rezervovana.InnerText = "❌";
                }
                else
                {
                    rezervovana.InnerText = "✅";

                }

                if (book.reservation.IsReservedByReader())
                {
                    reservateButton.Text = "Prodlouzit rezervaci";
                    reservateButton.Click -= new EventHandler(reservateButton_Click); // remove Button1_Click
                    reservateButton.Click += new EventHandler(reservateButtonExtend_Click); // add    Button2_Click
                }

                try
                {
                    posledniRezervace.InnerText = book.reservation.GetBookReservation()[0].DateOfReservation.ToString();
                }
                catch (Exception exception)
                {
                    posledniRezervace.InnerText = "❌";

                    Console.WriteLine(exception);
                }
            }
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        protected void calendar_SelectionChanged(object sender, EventArgs e)
        {
            if (Session["SelectedDates"] != null)
            {
                List<DateTime> newList = (List<DateTime>)Session["SelectedDates"];
                foreach (DateTime dt in newList)
                {
                    calendar.SelectedDates.Add(dt);
                }
                dateTimes.Clear();
            }
            // print it or whatever
        }

        protected void calendar_OnLoad(object sender, EventArgs e)
        {

        }

        protected void calendar_OnDayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsSelected == true)
            {
                dateTimes.Add(e.Day.Date);
            }
            Session["SelectedDates"] = dateTimes;

            foreach (var reservation in reservations)
            {
                if (e.Day.Date >= reservation.StartOfReservation && e.Day.Date <= reservation.EndOfReservation)
                {
                    e.Cell.BackColor = Color.Aqua;

                }
            }
            if (e.Day.Date < DateTime.Today)
            {
                e.Day.IsSelectable = false;
            }

            if (dateTimes.Count == 2)
            {
                if (e.Day.Date >= dateTimes[0] && e.Day.Date <= dateTimes[1])
                {

                }
            }

            if (dateTimes.Count > 2)
            {
                dateTimes.Clear();
            }

        }

        protected void confirmReservation_OnClick(object sender, EventArgs e)
        {
            dateTimes = (List<DateTime>) Session["SelectedDates"];
            System.Diagnostics.Debug.WriteLine(dateTimes.Count.ToString());

            if (dateTimes.Count == 2)
            {
                var res = book.reservation.MakeReservation(dateTimes[0], dateTimes[1]);
                Session["newDate"] = res;

                if (res == null)
                {
                    System.Diagnostics.Debug.WriteLine("Reservation successful");
                }
                else
                {
                    newDateDiv.Visible = true;
                    newDate.Text =
                        "Rezervace ve zvolenem datu neni k dispozici, ale je mozne rezervovat knihu v nasledujicim datu " +
                        res[0].Date + " - " + res[1].Date;
                    reservateNewDate.Visible = true;
                    cancelReservation.Visible = true;
                    reservateButton.Visible = true;

                    System.Diagnostics.Debug.WriteLine("Reservation error");
                    System.Diagnostics.Debug.WriteLine("Reservation " + res[0].Date);
                    System.Diagnostics.Debug.WriteLine("Reservation " + res[1].Date);

                }
            }
        }

        protected void extendNextBook_OnClick(object sender, EventArgs e)
        {
            Book b = book.FindBook((int)Session["tmp"]);
            switch (b.reservation.ExtendBookReservation(7))
            {
                case 0:
                    successMessageText.Text = "Prodlouzena rezervace";
                    successMessage.Visible = true;
                    extendNextBook.Visible = false;
                    promptMessage.Visible = false;
                    break;

                case -1:
                    errorMessage.Visible = true;
                    errorMessageText.Text = "Knihu nelze prodlouzit";
                    extendNextBook.Visible = false;
                    promptMessage.Visible = false;

                    break;

            }
        }

        protected void cancelExtendNextBook_OnClick(object sender, EventArgs e)
        {
            promptMessage.Visible = false;
            extendNextBook.Visible = false;
            cancelExtendNextBook.Visible = false;

            errorMessageText.Visible = true;
            errorMessageText.Text = "Nelze rezervovat";
            errorMessage.Visible = true;
        }

        protected void reservateNewDate_OnClick(object sender, EventArgs e)
        {
            var newDates = (List<DateTime>)Session["newDate"];
            var res = book.reservation.MakeReservation(newDates[0], newDates[1]);

            if (res == null)
            {
                System.Diagnostics.Debug.WriteLine("Reservation successful");
                successMessage.Visible = true;
                successMessageText.Text = "Kniha rezervovana";
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Reservation error");
                errorMessageText.Visible = true;
                errorMessageText.Text = "Nelze rezervovat";
                errorMessage.Visible = true;
            }
        }
    }
}