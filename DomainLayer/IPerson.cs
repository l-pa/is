using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public interface IPerson
    {
        string jmeno { get; set; }
        string prijmeni { get; set; }
        string adresa { get; set; }
        string email { get; set; }
    }
}
