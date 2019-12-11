using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Worker : Person
    {
        public Worker() : base("","", "", "")
        {
            
        }
        public int mzda;
        public string pozice;

    }
}
