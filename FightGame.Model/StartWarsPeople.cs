using Newtonsoft.Json;
using System.Collections.Generic;

namespace FightGame
{
    public class StartWarsPeople
    {
        [JsonProperty("results")]
        public List<Person> Results { get; set; }
    }

    public class Person
    {
        [JsonProperty("name")]
        public string PlayerName { get; set; }

        [JsonProperty("gender")]
        public string PlayerGender { get; set; }
    }
}
