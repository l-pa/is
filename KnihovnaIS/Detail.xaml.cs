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
            name.Content = book.Name;
            language.Content = book.Language;
            genre.Content = book.Genre;
            publishYear.Content = book.PublishYear.Value.Year;
            state.Content = book.GetCondition();

            if (book.Lend.IsLanded())
            {
                landed.Content = "✔";
            }
            else
            {
                landed.Content = "❌";
            }
            var a = book.Reservation.GetBookReservation();
            if (a.Count > 0)
            {
                lastReservation.Content = a[a.Count - 1].DateOfReservation.ToString();
            } else
            {
                lastReservation.Content = "❌";
            }
            author.Content = book.Author;
            publishYear.Content = book.PublishYear;
            isbn.Content = book.Isbn;

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
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Chcete odstranit rezervace", "Odstranit rezervace?", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    { 
                        switch (book.DeleteWithReservations())
                        {
                            case 0:
                                MessageBox.Show("Kniha a rezervace odstraneny");
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
