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

        public int id { get; set; }
        public string jmeno { get; set; }
        public string prijmeni { get; set; }
        public string adresa { get; set; }
        public string telefon { get; set; }
        public DateTime datum_zalozeni_uctu { get; set; }
        public Reader(int id) : base ("","","","")
        {
            
        }

        public DateTime zalozeniUctu;
    }
}
