using System;
using System.Collections.Generic;
using System.Threading;

namespace DinoGame
{
    class Game
    {
        private int score;

        private readonly Dinosaur dinosaur;
        private readonly UI ui;
        private readonly List<Cactus> cacti;

        public Game()
        {
            score = 0;

            ui = new UI(3 * Console.WindowHeight / 4);
            dinosaur = new Dinosaur(ui);
            cacti = new List<Cactus>() { new Cactus(ui, -10), new Cactus(ui, -100), new Cactus(ui, -200), new Cactus(ui, -300), new Cactus(ui, -400), new Cactus(ui, -500) };
        }

        public int Play()
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

                    cactus.Step(score);

                    if (CollisionSystem.CheckCollisions(dinosaur, cactus) > 0)
                    {
                        dinosaur.Alive = false;
                        break;
                    }

                    if (cactus.GetRightmostLeft() == Console.WindowWidth / 4 - 1)
                    {
                        score++;
                        ui.UpdateScore(score);
                    }
                }

                Thread.Sleep(5);
            }

            return score;
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
}
