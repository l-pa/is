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
        private IBook book;
        private List<IReservation> reservations;
        private List<DateTime> dateTimes = new List<DateTime>();

        private int alternativeBookId;

        private IBook nextReservedBook;

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void reservateButton_Click(object sender, EventArgs e)
        {
            showCalendar.Visible = true;
            reservateButton.Visible = false;
        }

        protected void reservateButtonExtend_Click(object sender, EventArgs e)
        {
            successMessage.Visible = false;
            errorMessage.Visible = false;
            promptMessage.Visible = false;

            var tmp = book.Reservation.ExtendBookReservation(7);
            
            Session["tmp"] = tmp;

            System.Diagnostics.Debug.WriteLine(tmp + " tmp");

            switch (tmp)
            {
                case 0:
                    successMessage.Visible = true;
                    successMessageText.Text = "Rezervace " + book.Name + " uspesne prodlouzena.";
                    break;
                case -1:
                    errorMessage.Visible = true;
                    errorMessageText.Text = "Rezervace " + book.Name + " neuspech.";
                    break;
                default:
                    promptMessage.Visible = true;
                    promptMessageText.Text = "Je mozno rezervovat nasledujici knihu " + book.Name + " chcete ji rezervovat?";
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
            book = (IBook) Session["book"];
            if (book != null)
            {
                reservations = new List<IReservation>();
                reservations = book.Reservation.GetBookReservation();

                title.InnerText = book.Name;

                autor.InnerText = book.Author;
                nazev.InnerText = book.Name;
                stav.InnerText = book.GetCondition();
                isbn.InnerText = book.Isbn;
                zanr.InnerText = book.Genre;
                jazyk.InnerText = book.Language;

                if (book.Lend.IsLanded())
                {
                    vypujcena.InnerText = "✅";
                }
                else
                {
                    vypujcena.InnerText = "❌";
                }

                if (book.Reservation.IsReserved())
                {
                    rezervovana.InnerText = "✅";
                }
                else
                {
                    rezervovana.InnerText = "❌";
                }

                if (book.Reservation.IsReservedByReader())
                {
                    reservateButton.Text = "Prodlouzit rezervaci";
                    reservateButton.Click -= new EventHandler(reservateButton_Click); // remove Button1_Click
                    reservateButton.Click += new EventHandler(reservateButtonExtend_Click); // add    Button2_Click
                }

                try
                {
                    posledniRezervace.InnerText = book.Reservation.GetBookReservation()[0].DateOfReservation.ToString();
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
            if (dateTimes.Count > 2)
            {
                dateTimes.Clear();
            }

            if (e.Day.IsSelected == true)
            {
                dateTimes.Add(e.Day.Date);
            }
            Session["SelectedDates"] = dateTimes;

            foreach (var reservation in reservations)
            {
                if (e.Day.Date >= reservation.StartOfReservation && e.Day.Date <= reservation.EndOfReservation && reservation.Reader.Id == 1)
                {
                    e.Cell.BackColor = Color.Aqua;
                } else if (e.Day.Date >= reservation.StartOfReservation && e.Day.Date <= reservation.EndOfReservation)
                {
                    e.Cell.BackColor = Color.AliceBlue;
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

        }

        protected void confirmReservation_OnClick(object sender, EventArgs e)
        {
            successMessage.Visible = false;
            errorMessage.Visible = false;
            promptMessage.Visible = false;
            newDateDiv.Visible = false;
            reservateButton.Visible = true;


            dateTimes = (List<DateTime>) Session["SelectedDates"];
            System.Diagnostics.Debug.WriteLine(dateTimes.Count.ToString());

            if (dateTimes.Count == 2)
            {
                var res = book.Reservation.MakeReservation(dateTimes[0], dateTimes[1]);
                Session["newDate"] = res;

                if (res == null)
                {
                    reservateButton.Text = "Prodlouzit rezervaci";
                    reservateButton.Click -= new EventHandler(reservateButton_Click); // remove Button1_Click
                    reservateButton.Click += new EventHandler(reservateButtonExtend_Click); // add    Button2_Click

                    showCalendar.Visible = false;

                    successMessage.Visible = true;
                    successMessageText.Text = "Kniha " + book.Name + " byla rezervovana od " + dateTimes[0].Date + " do " + dateTimes[1].Date;

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
                    reservateButton.Visible = false;

                    System.Diagnostics.Debug.WriteLine("Reservation error");
                    System.Diagnostics.Debug.WriteLine("Reservation " + res[0].Date);
                    System.Diagnostics.Debug.WriteLine("Reservation " + res[1].Date);

                }
            }
        }

        protected void extendNextBook_OnClick(object sender, EventArgs e)
        {
            IBook b = book.FindBook((int)Session["tmp"]);
            switch (b.Reservation.ExtendBookReservation(7))
            {
                case 0:
                    successMessageText.Text = "Rezervace knihy " + book.Name + " byla prodlouzena";
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
            newDateDiv.Visible = false;
            successMessage.Visible = false;
            errorMessage.Visible = false;
            promptMessage.Visible = false;

            extendNextBook.Visible = false;
            cancelExtendNextBook.Visible = false;

            errorMessageText.Visible = true;
            errorMessageText.Text = "Rezervace zrusena";
            errorMessage.Visible = true;
        }

        protected void reservateNewDate_OnClick(object sender, EventArgs e)
        {
            successMessage.Visible = false;
            errorMessage.Visible = false;
            promptMessage.Visible = false;

            var newDates = (List<DateTime>)Session["newDate"];
            var res = book.Reservation.MakeReservation(newDates[0], newDates[1]);

            if (res == null)
            {
                reservateButton.Text = "Prodlouzit rezervaci";
                reservateButton.Click -= new EventHandler(reservateButton_Click); // remove Button1_Click
                reservateButton.Click += new EventHandler(reservateButtonExtend_Click); // add    Button2_Click

                showCalendar.Visible = false;

                System.Diagnostics.Debug.WriteLine("Reservation successful");
                successMessage.Visible = true;
                successMessageText.Text = "Kniha " + book.Name + " byla rezervovana od " + newDates[0].Date + " do " + newDates[1].Date;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Reservation error");
                errorMessageText.Visible = true;
                errorMessageText.Text = "Rezervace zrusena";
                errorMessage.Visible = true;
            }
        }
    }
}