using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class IdentityMap<T>
    {
        public Dictionary<T, DataTable> dictionary = new Dictionary<T, DataTable>();
    }
}
