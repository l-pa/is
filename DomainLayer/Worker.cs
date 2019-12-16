using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Gateway;

namespace DomainLayer
{
    public class Worker : Person
    {
        public Worker(int id) : base(id)
        {
            base.FindWorker();
        }
        public int Salary { get; set; }
        public string Position { get; set; }

    }
}
