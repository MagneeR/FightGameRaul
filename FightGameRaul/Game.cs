
using System;
using System.Collections.Generic;


namespace FightGameRaul
{
    public class Game
    {
        public const int DefaultLives = 1;
        public const int DefaultPower = 10;

        public List<Player> Players { get; set; }

        private Random _random = new Random();

        public Game()
        {
            Players = new List<Player>
            {
                new Player
                {
                    Name= "Raúl",
                    Gender = Gender.Male,
                    Lives = DefaultLives,
                    Power = DefaultPower
                },
                new Player
                {
                    Name= "Rubén",
                    Gender = Gender.Male,
                    Lives = DefaultLives,
                    Power = DefaultPower
                },
                new Player
                {
                    Name= "Javier",
                    Gender = Gender.Male,
                    Lives = DefaultLives,
                    Power = DefaultPower
                }
            };

        }

        public void Start()
        {
            Console.WriteLine(@"___________.__       .__     __      ________                       
\_   _____/|__| ____ |  |___/  |_   /  _____/_____    _____   ____  
 |    __)  |  |/ ___\|  |  \   __\ /   \  ___\__  \  /     \_/ __ \ 
 |     \   |  / /_/  >   Y  \  |   \    \_\  \/ __ \|  Y Y  \  ___/ 
 \___  /   |__\___  /|___|  /__|    \______  (____  /__|_|  /\___  >
     \/      /_____/      \/               \/     \/      \/     \/ ");

            Menu();
        }

        private void Menu()
        {
            Console.WriteLine("\n     Choose one option: ");
            Console.WriteLine("-------------------------------");
            Console.WriteLine(" 1. Add Player ");
            Console.WriteLine(" 2. Status");
            Console.WriteLine(" 3. Fight! ");
            Console.WriteLine(" 4. Ranking");
            Console.WriteLine(" 5. Quit\n");
            ConsoleKeyInfo option = Console.ReadKey();

            switch(option.KeyChar)
            {
                case '1':
                    AddPlayer();
                    break;
                case '2':
                    Status();
                    break;
                case '3':
                    Fight();
                    break;
                case '4':
                    Ranking();
                    break;
                case '5':
                    Console.WriteLine("\n\n Do you want to quit the game? (y/n)");
                    var answer = Console.ReadKey();
                    if (answer.KeyChar == 'n')
                    {
                        Menu();
                    }
                    break;
                default:
                    Console.WriteLine("\nThe option you have chosen is invalid. Choose a valid option please. \n\n");
                    Menu();
                    break;

            }
        }

        public void AddPlayer()
        {
            string name = "";
            while (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                Console.WriteLine("\n\n Write player name (at least 3 characters and press Enter): \n");
                name = Console.ReadLine();
            }

            Gender? gender = null;
            while (gender == null)
            {
                Console.WriteLine("\n Choose gender: \n 1. Female\n 2. Male\n");
                var genderKey = Console.ReadKey();

                if (genderKey.KeyChar == '1')
                {
                    gender = Gender.Female;
                }
                else if (genderKey.KeyChar == '2')
                {
                    gender = Gender.Male;
                }
                else
                {
                    Console.WriteLine("\n\nInvalid gender. Introduce a valid gender.\n\n");
                }
            }

            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Gender = gender.Value,
                Name = name,
                Power = DefaultPower,
                Lives = DefaultLives
            };

            Players.Add(player);
            Console.WriteLine("\n\n Player added successfully!");
            player.Status();
            Console.ReadKey();
            Menu();
        }

        public void Fight()
        {//
            //hay mas de un jugador? no=error/si=seguir
            //elegir player aleatorio
            //elegir otro player aleatorio pero sin que se repita
            //quitamos power al player 2 (si power=0 || power <0 entonces se muere)
            //aumentamos gemas al player 1
            if (Players.Count == 0)
            {
                Console.WriteLine("\n There aren't players added yet");
            }
            else if (Players.Count < 2)
            {
                Console.WriteLine("\n There aren't enough players to fight");
            }
            else 
            {
                /*
                var playersCopy = new List <Player>(Players.ToArray()); //copia de la lista de players
                var indexPlayer1 = _random.Next(0, playersCopy.Count);
                var player1 = playersCopy[indexPlayer1];
                playersCopy.RemoveAt(indexPlayer1);
                var indexPlayer2 = _random.Next(0, playersCopy.Count);
                var player1 = playersCopy[indexPlayer2];
                //MUY REBUSCADO!!
                */
                var indexPlayer1 = _random.Next(0, Players.Count);
                int indexPlayer2 = 0;
                while (indexPlayer1 == indexPlayer2)
                {
                    indexPlayer2 = _random.Next(0, Players.Count);
                }
                var player1 = Players[indexPlayer1];
                var player2 = Players[indexPlayer2];
                var damageDone = _random.Next(1, 5);
                player2.Power -= damageDone;
                Console.WriteLine($"{player1.Name} has hitten {player2.Name} dealing {damageDone} damage to his Power.");
                if (player2.Power <= 0)
                {
                    player2.Lives--;
                    player1.Gems ++;
                    if (player2.Lives > 0)
                    {
                        Console.WriteLine($"{player2.Name} has lost a live. He still has {player2.Lives} lives.");
                    }
                    else
                    {
                        Console.WriteLine($"{player2.Name} is dead...");
                    }
                }
                Console.WriteLine("\n Press enter to fight again.");
                var key = Console.ReadKey();
            }
            
        }

        public void Ranking()
        {

        }

        public void Status()
        {
            if (Players.Count == 0)
            {
                Console.WriteLine("\n\nThere aren't players added yet.\nChoose the option Add Player first.");
                
            }
            else
            {
                foreach (var player in Players)
                {
                    player.Status();
                }
            }
            
            Console.ReadKey();
            Menu();
        }
              
    }
}
