using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Gateway;

namespace DomainLayer
{
    public class Land
    {
        LandGateway landGateway = new LandGateway();

        public DateTime datumVypujcky;
        public DateTime datumNavratu;
        public bool potvrzeniONavratu;
        public Book reservatedBook;

        public Land(Book book)
        {
            reservatedBook = book;
        }

        public bool isLanded()
        {
            if (landGateway.isLanded(reservatedBook.id).Count > 0) {
                return true;
            } else
            {
                return false;
            }
        }

    }
}
