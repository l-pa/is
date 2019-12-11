using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Gateway;

namespace DomainLayer
{
    public class Reader : Person
    {
        ReaderGateway readerGateway = new ReaderGateway();
        DTO.Reader reader;

        public Reader(int id) : base ("","","","")
        {
            readerGateway.findReader();
        }

        public DateTime zalozeniUctu;
    }
}
