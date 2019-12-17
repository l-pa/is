using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Gateway;
using Microsoft.Win32.SafeHandles;

namespace DomainLayer
{
    public class Lend
    {
        readonly LandGateway _landGateway = new LandGateway();
        
        public int Id;
        public DateTime DatumVypujcky;
        public DateTime DatumNavratu;
        public bool PotvrzeniONavratu;
        public IBook ReservatedBook;
        public IReader Reader;
        public Condition State;

        public Lend(IReader reader)
        {
            this.Reader = reader;
        }

        public Lend(IBook book)
        {
            ReservatedBook = book;
        }

        public Lend(IBook book, DTO.Land land)
        {
            Id = land.id;
            DatumVypujcky = land.startLand;
            DatumNavratu = land.endLand;
            PotvrzeniONavratu = land.returned;
            ReservatedBook = book;
        }

        public List<Lend> bookLands()
        {
            List<Lend> lands = new List<Lend>();
            foreach (DTO.Land l in _landGateway.findByBookId(ReservatedBook.Id))
            {
                lands.Add(new Lend(ReservatedBook, l));
            }
            return lands;
        }

        public List<Lend> ReaderLands()
        {
            List<Lend> lands = new List<Lend>();
            Book book = new Book();
            foreach (DTO.Land l in _landGateway.findByReaderId(Reader.Id))
            {
                lands.Add(new Lend(book.FindBook(l.book_id), l));
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
