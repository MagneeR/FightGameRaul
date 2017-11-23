using System;
using System.Collections.Generic;
using System.Text;

namespace FightGameRaul
{
    public class StarWarsPeople
    {
        public List<Person> results { get; set; }
    }

    public class Person
    {
        public string name { get; set; }
        public string gender { get; set; }
    }
}
