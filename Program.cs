using System;

namespace DinoGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.CursorVisible = false;

                    Console.WriteLine("Welcome to the dinosaur game.");

                    Leaderboard leaderboard = new Leaderboard("leaderboard.bin");
                    leaderboard.Display();

                    Console.Write("\nEnter your name: ");
                    Console.CursorVisible = true;
                    string name = Console.ReadLine().Trim().ToUpper();
                    Console.CursorVisible = false;

                    Console.WriteLine($"Good luck, {name}. Press any key to continue.");

                    Console.ReadKey();

                    int score = new Game().Play();

                    leaderboard.Entry(name, score);

                    Console.ReadKey();
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong. Press any key to continue.");

                    Console.ReadKey();
                }
            }
        }
    }
}
