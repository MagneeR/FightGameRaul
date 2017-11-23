using Newtonsoft.Json;
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
        [JsonProperty("name")]//es para que coja la propiedad del json y la convierta a PlayerName. Importante que este encima
        public string PlayerName { get; set; }
        [JsonProperty("gender")]//es para que coja la propiedad del json y la convierta a PlayerGender. Importante que este encima
        public string PlayerGender { get; set; }
    }
}
