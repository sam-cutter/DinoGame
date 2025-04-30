using System;
using System.Linq;

namespace DinoGame
{
    class Cactus
    {
        private readonly UI ui;

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

        public int DistanceFromRight { get; set; }

        public Cactus(UI ui, int distanceFromRight = -10)
        {
            DistanceFromRight = distanceFromRight;
            this.ui = ui;
        }

        public void Step()
        {
            CleanUp();
            DistanceFromRight++;
            Display();
        }

        public int GetRightmostLeft()
        {
            return Console.WindowWidth - DistanceFromRight + displayText.Select(line => line.Length).Max();
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

                    if (c != ' ') Console.Write(c);

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

                    if (c != ' ') Console.Write(' ');

                    left++;
                }
            }
        }

    }
}
