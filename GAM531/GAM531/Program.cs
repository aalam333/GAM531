using System;

namespace GAM531
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game()) 
            {
                game.Run();
            }
        }
    }
} 