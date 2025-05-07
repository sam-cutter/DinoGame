using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DinoGame
{
    class Game
    {
        private int score;

        private readonly UI ui;
        private readonly Dinosaur dinosaur;
        private readonly List<Cactus> cacti;

        private bool cheating;
        private readonly bool canCheat;

        public Game(bool canCheat)
        {
            score = 0;

            ui = new UI(3 * Console.WindowHeight / 4);
            dinosaur = new Dinosaur(ui);
            cacti = new List<Cactus>();

            for (int i = 0; i < 5; i++) cacti.Add(new Cactus(ui, -10 - 100 * i));

            cheating = false;
            this.canCheat = canCheat;
        }

        public int Play()
        {
            Random random = new Random();

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
                    if (cheating) dinosaur.Display();

                    if (Console.WindowWidth - cactus.DistanceFromRight < -20)
                    {
                        cactus.DistanceFromRight = cacti.Select(c => c.DistanceFromRight).Min() - random.Next(43, 60);

                        cactus.ScoreCounted = false;
                    }

                    cactus.Step(cheating ? 3 : 1);

                    if (!cheating && CollisionSystem.CheckCollisions(dinosaur, cactus) > 0)
                    {
                        dinosaur.Alive = false;
                        break;
                    }

                    if (cactus.GetRightmostLeft() < Console.WindowWidth / 4 && !cactus.ScoreCounted)
                    {
                        score++;
                        ui.UpdateScore(score);
                        cactus.ScoreCounted = true;
                    }
                }
            }

            return score;
        }

        private void AcceptUserInput()
        {
            while (dinosaur.Alive)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.C)
                {
                    if (canCheat) cheating = !cheating;
                }
                else dinosaur.Jump();
            }
        }
    }
}
