using System;

namespace DinoGame
{
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
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            for (int top = Horizon; top < Console.WindowHeight; top++)
            {
                for (int left = 0; left < Console.WindowWidth; left++)
                {
                    Console.SetCursorPosition(left, top);
                    Console.Write('█');
                }
            }

            Console.ResetColor();

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

            Console.Write(score);
        }
    }
}
