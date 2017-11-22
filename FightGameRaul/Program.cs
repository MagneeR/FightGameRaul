using System;

namespace FightGame
{
    // otro cambio
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = '\u263A'.ToString();
            System.Console.WriteLine('\u263B');

            var game = new Game();
            game.Start();
        }
    }
}