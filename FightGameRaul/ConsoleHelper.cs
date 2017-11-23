﻿using System;

namespace FightGameRaul
{
    public static class ConsoleHelper
    {
        public static void Write(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"{text}\n");
            Console.ResetColor();
        }
    }
}
