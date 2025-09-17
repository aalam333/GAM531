using System;           // Import basic system functionalities like Console, Math, etc.
using GameEngine;     // Import the WindowEngine namespace, which contains Game class and other related classes


namespace WindowEngine
{
    // Entry point of the application
    class Program
    {
        // Main method: the starting point of every C# console application
        static void Main(string[] args)
        {
            Game game = new TestGame("Assignment 1", 800, 600);
            game.Run();
        }
    }
}