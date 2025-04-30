using System;
using System.Collections.Generic;
using System.Threading;

namespace DinoGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the dinosaur game. Press any key to continue.");
            Console.ReadKey();

            Game game = new Game();
            game.Start();

            Console.ReadKey();
        }
    }

    class Game
    {
        private int score;

        private readonly Dinosaur dinosaur;
        private readonly UI ui;
        private List<Cactus> cacti;

        public Game()
        {
            score = 0;

            ui = new UI(3 * Console.WindowHeight / 4);
            dinosaur = new Dinosaur(ui);
            cacti = new List<Cactus>() { new Cactus(ui, -10), new Cactus(ui, -100), new Cactus(ui, -200), new Cactus(ui, -300) };
        }

        public void Start()
        {
            ui.Skeleton();
            ui.UpdateScore(0);
            dinosaur.Display();

            Thread userInputThread = new Thread(AcceptUserInput);
            userInputThread.Start();

            while (dinosaur.Alive)
            {
                dinosaur.Step();

                foreach (Cactus cactus in cacti)
                {
                    if (Console.WindowWidth - cactus.DistanceFromRight < -20) cactus.DistanceFromRight = -100;

                    cactus.Step();

                    if (cactus.GetRightmostLeft() == Console.WindowWidth / 4 - 1)
                    {
                        score++;
                        ui.UpdateScore(score);
                    }
                }

                Thread.Sleep(10);
            }
        }

        private void AcceptUserInput()
        {
            while (dinosaur.Alive)
            {
                Console.ReadKey();

                dinosaur.Jump();
            }
        }
    }

    class UI
    {
        public int Horizon { get; }

        public UI(int horizon)
        {
            Horizon = horizon;
        }

        public void Skeleton()
        {
            Console.CursorVisible = false;
            Console.Clear();

            DisplayGround();
            DisplayScoreLabel();
        }

        private void DisplayGround()
        {
            for (int top = Horizon; top < Console.WindowHeight; top++)
            {
                for (int left = 0; left < Console.WindowWidth; left++)
                {
                    Console.SetCursorPosition(left, top);
                    Console.Write('█');
                }
            }

            Console.CursorTop = 0;
        }

        private void DisplayScoreLabel()
        {
            Console.SetCursorPosition(3, 1);

            Console.Write("SCORE:");

            Console.CursorTop = 0;
        }

        public void UpdateScore(int score)
        {
            Console.SetCursorPosition(3, 1);
            Console.CursorLeft += "SCORE: ".Length;

            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));

            Console.SetCursorPosition(3, 1);
            Console.CursorLeft += "SCORE: ".Length;

            Console.Write(score);
        }
    }
}
