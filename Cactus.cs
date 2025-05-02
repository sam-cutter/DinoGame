using System;
using System.Collections.Generic;
using System.Linq;

namespace DinoGame
{
    class Cactus : ICollidable
    {
        private static int nextCactusNumber = 0;
        private readonly UI ui;


        private static readonly string[][] DISPLAY_TEXTS = new string[][]
        {
            new string[]
            {
                "    ,*-.",
                "    |  |",
                ",.  |  |",
                "| |_|  | ,.",
                "`---.  |_| |",
                "    |  .--`",
                "    |  |",
                "    |  |"
            },
            new string[]
            {
                "  _  _",
                " | || | _",
                "-| || || |",
                " | || || |-",
                "  \\_  || |",
                "    |  _/",
                "   -| | \\",
                "    |_|-"
            }
        };

        private readonly string[] displayText;

        public int DistanceFromRight { get; set; }

        public Cactus(UI ui, int distanceFromRight = -10)
        {
            displayText = DISPLAY_TEXTS[nextCactusNumber++ % DISPLAY_TEXTS.Length];
            DistanceFromRight = distanceFromRight;
            this.ui = ui;
        }

        public void Step(int score)
        {
            CleanUp();
            DistanceFromRight += 1;
            Display();
        }

        public int GetRightmostLeft()
        {
            return Console.WindowWidth - DistanceFromRight + displayText.Select(line => line.Length).Max();
        }

        public List<(int left, int top)> GenerateHitBox()
        {
            List<(int left, int top)> hitBox = new List<(int left, int top)>();

            for (int i = 0; i < displayText.Length; i++)
            {
                int left = Console.WindowWidth - DistanceFromRight;
                int top = ui.Horizon - 1 - i;

                foreach (char c in displayText[displayText.Length - i - 1])
                {
                    if (c != ' ') hitBox.Add((left, top));
                    left++;
                }
            }

            return hitBox;
        }

        public void Display()
        {
            for (int i = 0; i < displayText.Length; i++)
            {
                int left = Console.WindowWidth - DistanceFromRight;

                foreach (char c in displayText[displayText.Length - i - 1])
                {
                    if (left < 0) { left++; continue; }
                    if (left >= Console.WindowWidth) { break; }

                    Console.SetCursorPosition(left, ui.Horizon - 1 - i);

                    if (c != ' ')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(c);
                        Console.ResetColor();
                    }

                    left++;
                }
            }
        }


        public void CleanUp()
        {
            for (int i = 0; i < displayText.Length; i++)
            {
                int left = Console.WindowWidth - DistanceFromRight;

                foreach (char c in displayText[displayText.Length - i - 1])
                {
                    if (left < 0) { left++; continue; }
                    if (left >= Console.WindowWidth) { break; }

                    Console.SetCursorPosition(left, ui.Horizon - 1 - i);

                    if (c != ' ') {
                        Console.ResetColor();
                        Console.Write(' ');
                    }

                    left++;
                }
            }
        }

    }
}
