using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Gateway;

namespace DomainLayer
{
    public class Person
    {
        PersonGateway PersonGateway = new PersonGateway();
        public Person(int userId)
        {
            var user = PersonGateway.FindReader(userId);
            Id = user.id;
            FirstName = user.jmeno;
            LastName = user.prijmeni;
            Address = user.adresa;
            Email = user.email;
            PhoneNumber = user.telefon;
        }

        public  int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public void Notify(string text)
        {
            System.Diagnostics.Debug.WriteLine(Email);
            System.Diagnostics.Debug.WriteLine(text);
        }
    }
}
