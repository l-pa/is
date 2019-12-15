using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DataLayer.Properties;
using DTO;

namespace DataLayer.Gateway
{
    public class PersonGateway
    {
        private readonly XmlDocument _xmlDocument = new XmlDocument();

        public PersonGateway()
        {
            _xmlDocument.LoadXml( Resources.uzivatele );
        }
        public Reader FindReader(int id)
        {
            Reader reader = new Reader();
            reader.id = id;
            var node = _xmlDocument.DocumentElement.SelectSingleNode("/uzivatele/citatele//citatel[@id=" + id + "]");
            reader.jmeno = node.ChildNodes[0].InnerText;
            reader.prijmeni = node.ChildNodes[1].InnerText;
            reader.adresa = node.ChildNodes[2].InnerText;
            reader.telefon = node.ChildNodes[3].InnerText;
            reader.datum_zalozeni_uctu = DateTime.Parse(node.ChildNodes[5].InnerText);
            reader.email = node.ChildNodes[4].InnerText;
            return reader;
        }

        public Worker FindWorker(int id)
        {
            Worker worker = new Worker();
            worker.id = id;
            var node = _xmlDocument.DocumentElement.SelectSingleNode("/uzivatele/pracovnici//pracovnik[@id=" + id + "]");
            worker.jmeno = node.ChildNodes[0].Value;
            worker.prijmeni = node.ChildNodes[1].Value;
            worker.adresa = node.ChildNodes[2].Value;
            worker.telefon = node.ChildNodes[3].Value;
            worker.pozice = node.ChildNodes[4].Value;
            worker.mzda = Convert.ToInt32(node.ChildNodes[5].Value);
            return worker;
        }

    }
}
