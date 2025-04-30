using System;
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
        private bool dinosaurControllable;
        private bool dinosaurAlive;
        private int jumpProgress;
        private static readonly int jumpLength = 20;
        private static readonly int jumpHeight = 10;

        private readonly UI ui;

        public Game()
        {
            score = 0;
            dinosaurAlive = true;
            dinosaurControllable = true;
            jumpProgress = 0;

            ui = new UI(3 * Console.WindowHeight / 4);
            dinosaur = new Dinosaur();
        }

        public void Start()
        {
            ui.Skeleton();
            ui.UpdateScore(0);
            dinosaur.Display(ui);

            new Cactus(20).Display(ui);

            Thread userInputThread = new Thread(AcceptUserInput);
            userInputThread.Start();

            while (dinosaurAlive)
            {
                HandleJumping();

                Thread.Sleep(10);
            }
        }

        private void AcceptUserInput()
        {
            while (dinosaurAlive)
            {
                Console.ReadKey();

                if (dinosaurControllable)
                {
                    jumpProgress = 1;
                    dinosaurControllable = false;
                }
            }
        }

        private void HandleJumping()
        {
            if (jumpProgress > 0)
            {
                dinosaur.CleanUp(ui);
                dinosaur.UpdateJumpHeight(CalculateJumpHeight());
                dinosaur.Display(ui);

                jumpProgress += 1;

                if (jumpProgress > jumpLength) { jumpProgress = 0; dinosaurControllable = true; }
            }
        }

        private int CalculateJumpHeight()
        {
            return (int)Math.Floor(-4 * jumpHeight / Math.Pow(jumpLength, 2) * jumpProgress * (jumpProgress - jumpLength));
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

    class Dinosaur
    {
        private static readonly string[] displayText = new string[]
            {
                "               __",
                "              / _)",
                "     _.----._/ /",
                "    /         /",
                " __/ (  | (  |",
                "/__.-'|_|--|_|"
            };

        private int jumpHeight;

        public Dinosaur() { jumpHeight = 0; }

        public void UpdateJumpHeight(int jumpHeight) { this.jumpHeight = jumpHeight; }

        public void Display(UI ui)
        {
            for (int i = 0; i < displayText.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, ui.Horizon - 1 - i - jumpHeight);

                foreach (char c in displayText[displayText.Length - i - 1])
                {
                    if (c == ' ') Console.CursorLeft++;
                    else Console.Write(c);
                }
            }
        }

        public void CleanUp(UI ui)
        {
            for (int i = 0; i < displayText.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, ui.Horizon - 1 - i - jumpHeight);

                foreach (char c in displayText[displayText.Length - i - 1])
                {
                    if (c == ' ') Console.CursorLeft++;
                    else Console.Write(' ');
                }
            }
        }
    }

    class Cactus
    {
        private static readonly string[] displayText = new string[]
        {
            "    ,*-.",
            "    |  |",
            ",.  |  |",
            "| |_|  | ,.",
            "`---.  |_| |",
            "    |  .--`",
            "    |  |",
            "    |  |"
        };

        private int distanceFromRight;

        public Cactus(int distanceFromRight)
        {
            this.distanceFromRight = distanceFromRight;
        }

        public void Display(UI ui)
        {
            for (int i = 0; i < displayText.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth - distanceFromRight, ui.Horizon - 1 - i);

                foreach (char c in displayText[displayText.Length - i - 1])
                {
                    if (c == ' ') Console.CursorLeft++;
                    else Console.Write(c);
                }
            }
        }
    }
}
