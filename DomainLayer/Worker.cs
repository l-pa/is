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
        readonly PersonGateway _workerGateway = new PersonGateway();
        public Worker() : base(1)
        {
            var worker = _workerGateway.FindWorker(1);
            Salary = worker.mzda;
            Position = worker.pozice;
        }
        public int Salary { get; set; }
        public string Position { get; set; }

    }
}
