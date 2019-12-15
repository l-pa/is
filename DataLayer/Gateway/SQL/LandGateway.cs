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

        public List<DTO.Land> findByBookId(int bookId)
        {
            List<DTO.Land> lands = new List<Land>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", bookId.ToString());
            var result = DatabaseTable.Query(sqlDatabase, "SELECT * FROM vypujcka WHERE kniha_id = @id", keyValues, "vypujcka"); // TODO
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Land land = new Land();
                land.id = (int)result.Rows[i].ItemArray[0];
                land.startLand = (DateTime)result.Rows[i].ItemArray[1];
                land.endLand = (DateTime)result.Rows[i].ItemArray[2];
                land.returned = (bool)result.Rows[i].ItemArray[3];
                land.reader_id = (int)result.Rows[i].ItemArray[4];
                land.book_id = (int)result.Rows[i].ItemArray[5];
                land.state_id = (int)result.Rows[i].ItemArray[6];
                lands.Add(land);
            }
            return lands;
        }

        public List<DTO.Land> findByReaderId(int bookId)
        {
            List<DTO.Land> lands = new List<Land>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", bookId.ToString());
            var result = DatabaseTable.Query(sqlDatabase, "SELECT * FROM vypujcka WHERE ctenar_id = @id", keyValues, "vypujcka"); // TODO
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Land land = new Land();
                land.id = (int)result.Rows[i].ItemArray[0];
                land.startLand = (DateTime)result.Rows[i].ItemArray[1];
                land.endLand = (DateTime)result.Rows[i].ItemArray[2];
                land.returned = (bool)result.Rows[i].ItemArray[3];
                land.reader_id = (int)result.Rows[i].ItemArray[4];
                land.book_id = (int)result.Rows[i].ItemArray[5];
                land.state_id = (int)result.Rows[i].ItemArray[6];
                lands.Add(land);
            }
            return lands;
        }

        public List<DTO.Land> IsLanded(int id)
        {
            List<DTO.Land> lands = new List<Land>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@id", id.ToString());
            var result = DatabaseTable.Query(sqlDatabase, "select * from vypujcka where kniha_id = @id and GETDATE() between datum_od and datum_do", keyValues, "vypujcka"); // TODO
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DTO.Land land = new Land();
                land.id = (int)result.Rows[i].ItemArray[0];
                land.startLand = (DateTime)result.Rows[i].ItemArray[1];
                land.endLand = (DateTime)result.Rows[i].ItemArray[2];
                land.returned = (bool)result.Rows[i].ItemArray[3];
                land.reader_id = (int)result.Rows[i].ItemArray[4];
                land.book_id = (int)result.Rows[i].ItemArray[5];
                land.state_id = (int)result.Rows[i].ItemArray[6];
                lands.Add(land);
            }
            return lands;
        }

        public void insert(Land land)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@datum_od", land.startLand.ToString());
            keyValues.Add("@datum_do", land.endLand.ToString());
            keyValues.Add("@potvrzeni_o_navratu", Convert.ToInt32(land.returned).ToString());
            keyValues.Add("@ctenar_id", land.reader_id.ToString());
            keyValues.Add("@kniha_id", land.book_id.ToString());
            keyValues.Add("@stav_id", land.state_id.ToString());

            DatabaseTable.NonQuery(sqlDatabase, "insert into vypujcka (datum_od, datum_do, potvrzeni_o_navratu, ctenar_id, kniha_id, stav_id) values(@datum_od, @datum_do, @potvrzeni_o_navratu, @ctenar_id, @kniha_id, @stav_id)", keyValues); // TODO
        }
    }
}
