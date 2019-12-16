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
        private List<IBook> pageList = new List<IBook>();
        private List<IBook> booksList;
        private int index = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            booksList = books.FindBook(searchInput.Value);
            System.Diagnostics.Debug.WriteLine("Search lodead");
        }

        public void updateList()
        {
            errorMessage.Visible = false;
            if (booksList.Count == 0)
            {
                errorMessage.Visible = true;
                errorMessageText.Text = "Nenalezeny zadne knihy";
            } else
            {
                try
                {
                    pageList = booksList.GetRange(index * 10, (index + 1) * 10);
                }
                catch (Exception)
                {
                    pageList = booksList.GetRange((booksList.Count / 10) * 10, booksList.Count % 10);
                }
                listView.DataSource = pageList;
                listView.DataBind();
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

        protected void nextPage_Click(object sender, EventArgs e)
        {
            if (index < Math.Ceiling(booksList.Count / 10d))
            {
                index += 1;
                updateList();
            }
        }

        protected void prevPage_Click(object sender, EventArgs e)
        {
            if (index > 1)
            {
                index -= 1;
                updateList();
            }
        }
    }
}