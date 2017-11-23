
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FightGameRaul
{
    //vamos a crear una interface
    public interface IPlayerService //por convencion empieza por I
    {
        //quiero que me de una lista de jugadores
        List<Player> GetPlayers();
    }
    public class ApiPlayerService : IPlayerService
    {
        private const string ApiUrl = "https://swapi.co/api/people/";
        public List<Player> GetPlayers()
        {
            var httpClient = new HttpClient();
            Task <string> task = httpClient.GetStringAsync(ApiUrl);

            //Forma 1 de recuperar valor
            /*Task.Run(async () =>
            {
               string result = await task;
            });*/

            //Forma 2 de recuperar valor
            string result = task.Result;
            //vamos a deserializar el json de la api de star wars = coger lo que se recibe y convertirlo en objetos de C#
            //esto lo haremos con una libreria 
            StarWarsPeople people = JsonConvert.DeserializeObject<StarWarsPeople>(result);

            //forma 1: convertir List<Person> en nuestro List<Player>
            /* var players = new List<Player>();
             foreach (var person in people.results)
             {
                 players.Add(new Player {
                     Id = ++Game.LastId,
                     Name = person.name,
                     Gender = person.gender == "male" ? Gender.Male : Gender.Female,//si person.gender == "male", entonces Gender.Male ; si no Gender.Female
                     Lives = Game.DefaultLives,
                     Power = Game.DefaultPower
                 });
             }*/
            //forma 2: convertir List<Person> en nuestro List<Player> con LINQ
            var players = people.results.Select(person => new Player {
                Id = ++Game.LastId,
                Name = person.PlayerName,
                Gender = person.PlayerGender == "male" ? Gender.Male : Gender.Female,//si person.gender == "male", entonces Gender.Male ; si no Gender.Female
                Lives = Game.DefaultLives,
                Power = Game.DefaultPower
            });
            return players.ToList();
            
        }
    }
    public class CustomPlayerService : IPlayerService
    {
        public List <Player> GetPlayers()
        {
            return new List<Player>
            {
                new Player
                {
                    Id = ++Game.LastId,
                    Name = "Alberto",
                    Gender = Gender.Male,
                    Lives = Game.DefaultLives,
                    Power = Game.DefaultPower
                },
                new Player
                {
                    Id = ++Game.LastId,
                    Name = "Mary",
                    Gender = Gender.Female,
                    Lives = Game.DefaultLives,
                    Power = Game.DefaultPower
                },
                new Player
                {
                    Id = ++Game.LastId,
                    Name = "Juan",
                    Gender = Gender.Male,
                    Lives = Game.DefaultLives,
                    Power = Game.DefaultPower
                },
                new Player
                {
                    Id = ++Game.LastId,
                    Name = "Thor",
                    Gender = Gender.Male,
                    Lives = Game.DefaultLives,
                    Power = Game.DefaultPower
                },
            };
        }
    }
}
