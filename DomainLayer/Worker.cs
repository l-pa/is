using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Worker : IPerson
    {
        public int mzda;
        public string pozice;

        public string jmeno { get; set; }
        public string prijmeni { get; set; }
        public string adresa { get; set; }
        public string email { get; set; }
    }
}
