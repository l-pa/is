﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public interface IReader
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Address { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        DateTime AccountCreatedTime { get; set; }
        void Notify(string text);

    }
}
