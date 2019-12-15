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
        
        public void delete (string columnName, int id)
        {
            foreach (KeyValuePair<T, DataTable> entry in dictionary)
            {
                for (int i = dictionary[entry.Key].Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = entry.Value.Rows[i];
                    if (dr[columnName].ToString() == id.ToString())
                    {
                        dr.Delete();
                    }
                    dr.AcceptChanges();
                }
            }
        }

        public DataRow Search(string columnName, T value)
        {
            foreach (KeyValuePair<T, DataTable> entry in dictionary)
            {
                for (int i = dictionary[entry.Key].Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = entry.Value.Rows[i];
                    if (dr[columnName].ToString() == value.ToString())
                    {
                        return dr;
                    }
                }
            }
            return null;
        }
    }
}
