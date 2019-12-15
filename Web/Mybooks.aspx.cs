using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DomainLayer;

namespace Web
{
    public partial class MyBooks : Page
    {
        private List<IBook> myReservedBooks = new List<IBook>();
        private List<IBook> myLandedBooks = new List<IBook>();

        protected void Page_Load(object sender, EventArgs e)
        {
            myReservedBooks = new List<IBook>();
            System.Diagnostics.Debug.WriteLine("Moje knihy loaded");
            foreach (var reservation in new Reservation(new Reader()).GetReaderReservation())
            {
                myReservedBooks.Add(reservation.ReservatedBook);
                System.Diagnostics.Debug.WriteLine(reservation.ReservatedBook.Nazev);

            }

            List<IBook> distinctBook = myReservedBooks
                .GroupBy(p => p.Id)
                .Select(g => g.FirstOrDefault())
                .ToList();

            myReservedBooks = distinctBook;

            //foreach (var land in new Land(new Reader()).ReaderLands()) 
            //{
            //    myLandedBooks.Add(land.reservatedBook);
            //}

        }

        protected void myBookListView_OnLoad(object sender, EventArgs e)
        {
            myBookListView.DataSource = myReservedBooks;
            myBookListView.DataBind();
        }

        protected void detail_OnClick(object sender, EventArgs e)
        {
            ListViewItem item = (ListViewItem)(sender as Control).NamingContainer;

            Session["book"] = myReservedBooks[Convert.ToInt32(item.DataItemIndex)];
            Response.Redirect("~/Detail.aspx");
        }
    }
}