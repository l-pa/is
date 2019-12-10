using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KnihovnaIS
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Page
    {
        DomainLayer.Book books = new DomainLayer.Book();

        public Search()
        {
            InitializeComponent();
        }

        private void lvBooks_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            List<DTO.Book> booksList = books.findBook(searchInput.Text);
            lvBooks.ItemsSource = booksList;
            if (booksList.Count == 0)
            {
                MessageBox.Show("No books");
            }
        }

        private void clickDetail_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Detail detail = new Detail((DTO.Book)btn.Tag);
            detail.Show();
        }
    }
}
