using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DataLayer.Mapper;

namespace DomainLayer
{
    public class Book
    {
        BookMapper bookMapper = new BookMapper();
        public List<DTO.Book> findBook(String query)
        {
           return bookMapper.findBook(query);
        }
        public void reservateBook(Book book, Reader reader)
        {
            
        }

        public void extendReservation(Book book, Reader reader)
        {

        }

        public void deleteBook(Book book)
        {

        }
    }
}
