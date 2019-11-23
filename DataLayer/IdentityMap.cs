using DataLayer.Mapper;
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
        private Dictionary<T, DataTable> identityMap = new Dictionary<T, DataTable>();

        public DataTable identity(T query)
        {
            Console.WriteLine(query);
            if (identityMap.ContainsKey(query))
            {
                return identityMap[query];
            } else
            {
                DataTable result = DatabaseTable.Query(BookMapper.sqlDatabase, "SELECT * FROM kniha WHERE nazev + autor + vydavatelstvi + isbn LIKE '%" + query + "%'", null, "kniha");
                identityMap[query] = result; 
                return result;
            }
        }
        
    }
}
