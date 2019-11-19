using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DTO;

namespace DataLayer.Mapper
{
    public class ReaderMapper
    {
        private XmlDocument xmlDocument = new XmlDocument();

        public ReaderMapper()
        {
            xmlDocument.Load("uzivatele.xml");
        }
        public Reader findReader(int id)
        {
            Reader reader = new Reader();

            var node = xmlDocument.DocumentElement.SelectSingleNode("/uzivatele/citatele/citatel[@id="+id+"]");
            reader.jmeno = node.ChildNodes[0].ToString();
            reader.prijmeni = node.ChildNodes[1].ToString();
            reader.adresa = node.ChildNodes[2].ToString();
            return reader;
        }
        public void deleteReader(Reader reader)
        {
            
        }
        public void insertReader(Reader reader)
        {

        }
        public void updateReader(Reader reader)
        {

        }

    }
}
