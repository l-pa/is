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
        public DateTime AccountCreatedTime { get; set; }
        public Reader(int id) : base(id)
        {

        }

    }
}
