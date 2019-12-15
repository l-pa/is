using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Gateway;

namespace DomainLayer
{
    public class Condition
    {
        public int id;
        public string stav;
        public string popisStavu;


        ConditionGateway ConditionGateway = new ConditionGateway();

        public Condition(int id)
        {
            var condition = ConditionGateway.FindByConditionId(id);
            id = condition.id_stav;
            stav = condition.stav;
            popisStavu = condition.stav_popis;
        }
    }
}
