using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Person
    {
        public Person(string jmeno, string prijmeni, string adresa, string email)
        {
            this.jmeno = jmeno;
            this.prijmeni = prijmeni;
            this.adresa = adresa;
            this.email = email;

        }
        string jmeno { get; set; }
        string prijmeni { get; set; }
        string adresa { get; set; }
        string email { get; set; }

    }
}
