using System.Collections.Generic;

namespace FightGame
{
    public interface IPlayerService
    {
        List<Player> GetPlayers();

        Player GetPlayerById(int id);

        Player AddPlayer(Player player);

        Player UpdatePlayer(Player player);

        void Delete(int id);
    }
}
