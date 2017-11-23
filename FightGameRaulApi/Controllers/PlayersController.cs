using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FightGameRaul;

namespace FightGameRaulApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        // GET api/players
        [HttpGet]
        public IEnumerable<Player> Get()
        {
            var playerService = new CustomPlayerService();
            return playerService.GetPlayers();
        }

        // GET api/players/5
        [HttpGet("{id}")]
        public Player Get(int id)
        {
            var playerService = new CustomPlayerService();
            var players = playerService.GetPlayers();
            var player = players.FirstOrDefault(x => x.Id == id);
            return player;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
