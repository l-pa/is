using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Gateway;
using Microsoft.Win32.SafeHandles;

namespace DomainLayer
{
    public class Land
    {
        readonly LandGateway _landGateway = new LandGateway();
        
        public int Id;
        public DateTime DatumVypujcky;
        public DateTime DatumNavratu;
        public bool PotvrzeniONavratu;
        public IBook ReservatedBook;
        public IReader Reader;
        public Condition State;

        public Land(IReader reader)
        {
            this.Reader = reader;
        }

        public Land(IBook book)
        {
            ReservatedBook = book;
        }

        public Land(IBook book, DTO.Land land)
        {
            Id = land.id;
            DatumVypujcky = land.startLand;
            DatumNavratu = land.endLand;
            PotvrzeniONavratu = land.returned;
            ReservatedBook = book;
        }

        public List<Land> bookLands()
        {
            List<Land> lands = new List<Land>();
            foreach (DTO.Land l in _landGateway.findByBookId(ReservatedBook.Id))
            {
                lands.Add(new Land(ReservatedBook, l));
            }
            return lands;
        }

        public List<Land> ReaderLands()
        {
            List<Land> lands = new List<Land>();
            Book book = new Book();
            foreach (DTO.Land l in _landGateway.findByReaderId(Reader.Id))
            {
                lands.Add(new Land(book.FindBook(l.book_id), l));
            }
            return lands;
        }



        public bool IsLanded()
        {
            if (_landGateway.IsLanded(ReservatedBook.Id).Count > 0) {
                return true;
            }
            return false;
        }

    }
}
