using System;

namespace FightGameRaul
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Lives { get; set; }
        public int Power { get; set; }
        public int Gems { get; set; }
        public Gender Gender { get; set; }

        public Player()
        {
            // comentario
            // otro cambio
        }

        public string Status()
        {
            /*var genderDisplay = (Gender == Gender.Male)
                ? "Hombre"
                : "Mujer";*/

            return $" {Name.PadRight(25)}\t\t\t{Id}\t{Lives}\t{Power}\t{Gems}\t{Gender}";
        }

    }
}
