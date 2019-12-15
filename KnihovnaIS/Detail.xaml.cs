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
using System.Windows.Shapes;
using DomainLayer;

namespace KnihovnaIS
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : Window
    {
        IBook book;
        public Detail(IBook book)
        {
            InitializeComponent();
            this.book = book;
            name.Content = book.Nazev;
            language.Content = book.Jazyk;
            genre.Content = book.Zanr;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            switch (book.DeleteBook())
            {
                case 0:
                    MessageBox.Show("Uspech");
                    this.Close();
                    break;
                case 1:
                    MessageBox.Show("Error, landed");
                    break;
                case 2:
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Odstranit rezervace?", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    { 
                        switch (book.DeleteWithReservations())
                        {
                            case 0:
                                MessageBox.Show("Uspech + rezervace");
                                this.Close();
                                break;
                            default:
                                MessageBox.Show("Chyba");
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
