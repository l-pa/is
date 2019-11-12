using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataLayer.Mapper
{
    public class ReaderMapper
    {
        private XmlDocument xmlDocument = new XmlDocument();

        public ReaderMapper()
        {
            xmlDocument.Load("uzivatele.xml");
        }
        public Objects.Reader find(int id)
        {
            Objects.Reader reader = new Objects.Reader();

            var node = xmlDocument.DocumentElement.SelectSingleNode("/uzivatele/citetele/citatel[@id=1]");
            reader.jmeno = node.ChildNodes[0].ToString();
            reader.prijmeni = node.ChildNodes[1].ToString();
            reader.adresa = node.ChildNodes[2].ToString();

            Console.WriteLine();

            return reader;
        }
    }
}
