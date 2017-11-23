
using FightGameRaul.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightGameRaul
{
    public class Game
    {
        public List<Player> Players { get; set; }

        private Random _random = new Random(DateTime.Now.Millisecond);

        public Game()
        {
            ConsoleHelper.Write(@"    ___________.__       .__     __      ________                       
    \_   _____/|__| ____ |  |___/  |_   /  _____/_____    _____   ____  
     |    __)  |  |/ ___\|  |  \   __\ /   \  ___\__  \  /     \_/ __ \ 
     |     \   |  / /_/  >   Y  \  |   \    \_\  \/ __ \|  Y Y  \  ___/ 
     \___  /   |__\___  /|___|  /__|    \______  (____  /__|_|  /\___  >
         \/      /_____/      \/               \/     \/      \/     \/ by Raúl", ConsoleColor.DarkMagenta);

            IPlayerService playerService = new ApiPlayerService();//new CustomPlayerService();
            Players = playerService.GetPlayers();
        }

        public void Start()
        {
            Menu();
            while (true)
            {
                ConsoleKeyInfo option = Console.ReadKey(true);
                if (option.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("\n Closing Game..");
                    Task.Run(async () => await Task.Delay(1500)).Wait();
                    break;
                }
                switch (option.KeyChar)
                {
                    case '0':
                        Menu();
                        break;
                    case '1':
                        AddPlayer();
                        break;

                    case '2':
                        Status();
                        break;

                    case '3':
                        Fight();
                        break;

                    case 'c':
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("\nSolo hay que escribir un número. ¿Es tan dificil?\n\n");
                        Menu();
                        break;
                }
            }
        }

        private void Menu()
        {
            Console.WriteLine("\n\n Choose an option:\n");
            Console.WriteLine(" 0. Show menu");
            Console.WriteLine(" 1. Add New Player");
            Console.WriteLine(" 2. Status");
            Console.WriteLine(" 3. Fight!");
            Console.WriteLine(" 4. Press c to clear the screen");
            Console.WriteLine(" 5. Exit");

            
        }

        public void AddPlayer()
        {
            string name = null;

            while (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                Console.WriteLine("\n\n Write player name (and press Enter):");
                name = Console.ReadLine();
            }

            Gender? gender = null;

            while (gender == null)
            {
                Console.WriteLine("\n Choose gender:\n 1. Female\n 2. Male");
                var genderKey = Console.ReadKey(true);

                if (genderKey.KeyChar == '1')
                {
                    gender = Gender.Female;
                }
                else if (genderKey.KeyChar == '2')
                {
                    gender = Gender.Male;
                }
            }

            var player = new Player
            {
                Id = ++GameModel.LastId,
                Gender = gender.Value,
                Name = name,
                Power = GameModel.DefaultPower,
                Lives = GameModel.DefaultLives
            };

            Players.Add(player);

            Console.WriteLine($"\n\n {player.Name} has been added.");

    
        }

        public void Fight()
        {
            var currentPlayers = Players
                .Where(x => x.Lives > 0)
                .ToList();

            // hay más de un jugador?
            if (currentPlayers.Count < 2)
            {
                ConsoleHelper.Write("\n There aren't enough players to fight :(", ConsoleColor.Red);
                return;
            }

            // elegir un player aleatoriamente
            var indexPlayer1 = _random.Next(0, currentPlayers.Count);
            var player1 = currentPlayers[indexPlayer1];

            // elegir el segundo player aleatoriamente pero que no se repita
            int indexPlayer2 = _random.Next(0, currentPlayers.Count); ;
            while (indexPlayer1 == indexPlayer2)
                indexPlayer2 = _random.Next(0, currentPlayers.Count);

            var player2 = currentPlayers[indexPlayer2];

            // quitamos power al player 2 (el nivel de daño será aleatorio entre 1 y 5)
            var damage = _random.Next(1, 5);
            player2.Power -= damage;

            ConsoleHelper.Write($" ==> {player1.Name} has hitten {player2.Name} with a force of {damage}.",
                ConsoleColor.Blue);

            if (player2.Power <= 0)
            {
                player2.Lives--;
                player2.Power = player2.Lives > 0 ? GameModel.DefaultPower : 0;

                if (player2.Lives > 0)
                {
                    ConsoleHelper.Write($" {player2.Name} has lost a LIFE!",
                        ConsoleColor.Yellow);
                }
                else
                {
                    player2.Gems = 0;
                    ConsoleHelper.Write($" {player2.Name} has been slain!",
                        ConsoleColor.Red);
                }

                player1.Gems++;

                ConsoleHelper.Write($" {player1.Name} has won a GEM! " +
                    $" Now {player1.Name} has {player1.Gems} gems.",
                    ConsoleColor.Green);

                // cada 3 gemas le damos una vida
                if (player1.Gems == 3)
                {
                    player1.Lives++;
                    player1.Gems = 0;

                    ConsoleHelper.Write($" {player1.Name} has won a LIFE!!",
                        ConsoleColor.Magenta);
                }

                // comprobar si hay ganador
                if (Players.Count(x => x.Lives > 0) == 1)
                {
                    Console.WriteLine("\n\n+============================================+");
                    Console.WriteLine("+============================================+");
                    Console.WriteLine("+============================================+");
                    ConsoleHelper.Write($"      {player1.Name} HAS WON!!", ConsoleColor.Cyan);
                    Console.WriteLine("+============================================+");
                    Console.WriteLine("+============================================+");
                    Console.WriteLine("+============================================+");
                }
            }
        }

        public void Status()
        {
            if (Players.Count == 0)
            {
                Console.WriteLine("\n There aren´t players.");
            }
            else
            {
                Console.WriteLine($"\n Name\t\t\t\t\t\tId\tLives\tPower\tGems\tGender");
                Console.WriteLine($"--------------------------------------------------------------------------------------------------");

                var ordered = Players
                    .OrderByDescending(x => x.Lives)
                    .ThenByDescending(x => x.Power)
                    .ThenByDescending(x => x.Gems);

                foreach (var player in ordered)
                {
                    var status = player.Status();
                    var color = player.Lives > 0 ? ConsoleColor.White : ConsoleColor.Red;
                    ConsoleHelper.Write(status, color);
                }
            }
        }
    }
}
