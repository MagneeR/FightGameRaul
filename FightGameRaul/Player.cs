

using System;

namespace FightGameRaul
{/// <summary>
/// /
/// </summary>
    public enum Gender
    {
        Male,
        Female
    }
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Lives { get; set; }
        public int Power { get; set; }
        public int Gems { get; set; } 
        public Gender Gender { get; set; }
        
        public Player()
        {
            //comentario
            
        }

        public void Status()
        {
            Console.WriteLine($"\n\n {Name}\n");
            Console.WriteLine($"****************\n");
            Console.WriteLine($" Id: {Id}\n");            
            Console.WriteLine($" Lives {Lives}\n");
            Console.WriteLine($" Power: {Power}\n");
            Console.WriteLine($" Gems: {Gems}\n");
            /*
            var genderDisplay = (Gender == Gender.Male) //si esto se cumple
                ? "Hombre" //genderDisplay valdra esto,
                : "Mujer"; //si no valdra esto
            Console.WriteLine($" Sexo: {genderDisplay}\n\n");
            */
            Console.WriteLine($" Gender: {Gender}\n\n");

        }

        public void Train()
        {

        }
    }
}
