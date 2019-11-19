using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Book
    {
        public int id;
        public String nazev;
        public String autor;
        public String jazyk;
        public DateTime? rok_vydani;
        public String ISBN;
        public String vydavatel;
        public Condition stav;
    }
}
