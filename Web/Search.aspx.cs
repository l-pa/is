using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DomainLayer;

namespace Web
{
    public partial class Contact : Page
    {
        Book books = new Book();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void updateList()
        {
            List<Book> booksList = books.findBook(searchInput.Value);
            listView.DataSource = booksList;
            listView.DataBind();
            if (booksList.Count == 0)
            {
                
            }
        }

        private void clickDetail_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            //Detail detail = new Detail((Book)btn.Tag);
            //detail.Show();

            //detail.Closed += detailClosed;
        }

        private void detailClosed(object sender, EventArgs e)
        {
            updateList();
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            updateList();
        }
    }
}