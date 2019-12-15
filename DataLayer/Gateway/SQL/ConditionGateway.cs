using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DataLayer.Gateway
{
    public class ConditionGateway
    {
        static SQLConnection sqlDatabase = new SQLConnection();

        public Condition FindByConditionId(int id)
        {
        Condition condition = new Condition();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", id.ToString());
            var result = DatabaseTable.Query(sqlDatabase, "SELECT * FROM stav WHERE stav_id = @id", keyValues, "stav"); // TODO
            for (int i = 0; i < result.Rows.Count; i++)
            {
                condition.id_stav = (int)result.Rows[i].ItemArray[0];
                condition.stav = (string)result.Rows[i].ItemArray[1];
                condition.stav_popis = (string)result.Rows[i].ItemArray[2];
            }
            return condition;
        }

    }

}
