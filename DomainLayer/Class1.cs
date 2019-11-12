using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace DomainLayer
{
    public class Class1
    {
        public void test()
        {
            DataLayer.TableGateway.ReaderGateway.find(1);
        }
    }
}
