using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DomainLayer;

namespace Web
{
    public partial class Search : Page
    {
        IBook books = new Book();
        private List<IBook> booksList;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Search lodead");
        }

        public void updateList()
        {
            booksList = books.FindBook(searchInput.Value);
            listView.DataSource = booksList;
            listView.DataBind();
            if (booksList.Count == 0)
            {
                // Alert
            }
        }

        private void detailClosed(object sender, EventArgs e)
        {
            updateList();
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            updateList();
        }

        protected void detail_Click(object sender, EventArgs e)
        {
            //Get the listviewitem from button        
            ListViewItem item = (ListViewItem)(sender as Control).NamingContainer;
            updateList();
            Session["book"] = booksList[Convert.ToInt32(item.DataItemIndex)];
            Response.Redirect("~/Detail.aspx");
        }
    }
}