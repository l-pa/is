using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Gateway;

namespace DomainLayer
{
    public class Reader : Person, IReader
    {
        PersonGateway _readerGateway = new PersonGateway();
        
        public DateTime AccountCreatedTime { get; set; }
        public Reader() : base(1)
        {
            AccountCreatedTime = _readerGateway.FindReader(1).datum_zalozeni_uctu;
        }

    }
}
