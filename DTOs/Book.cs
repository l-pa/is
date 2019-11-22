using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Book
    {
        public int id { get; set; }
        public String nazev { get; set; }
        public String autor { get; set; }
        public String jazyk { get; set; }
        public DateTime? rok_vydani { get; set; }
        public String ISBN { get; set; }
        public String zanr { get; set; }
        public String vydavatel { get; set; }
        public Condition stav { get; set; }
    }
}
