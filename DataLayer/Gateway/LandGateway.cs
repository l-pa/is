using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DataLayer.Gateway
{
    public class LandGateway
    {
        SQLConnection sqlDatabase = new SQLConnection();

        public bool isLandedAtMomement(Book book)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", book.id.ToString());
            var query = DatabaseTable.Query(sqlDatabase, "SELECT * FROM vypujcky WHERE id = @id AND ", keyValues, "rezervace"); // TODO
            if (query.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
