using System;
using System.Collections.Generic;

namespace DinoGame
{
    class Dinosaur : ICollidable
    {
        private readonly UI ui;

        private static readonly string[] displayText = new string[]
            {
                "               __",
                "              / _)",
                "     _.----._/ /",
                "    /         /",
                " __/ (  | (  |",
                "/__.-'|_|--|_|"
            };

        public bool Alive { get; set; } = true;

        private int jumpProgress = 0;
        private const int JUMP_LENGTH = 40;
        private const int JUMP_HEIGHT = 15;

        private int currentHeight = 0;
        private static readonly int defaultLeft = Console.WindowWidth / 4;


        public Dinosaur(UI ui) { this.ui = ui; }

        public void Jump()
        {
            if (jumpProgress > 0) return;

            jumpProgress = 1;
        }

        public void Step()
        {
            if (jumpProgress == 0) return;

            CleanUp();
            currentHeight = CalculateJumpHeight();
            Display();

            jumpProgress += 1;

            if (jumpProgress > JUMP_LENGTH) jumpProgress = 0;
        }

        public List<(int left, int top)> GenerateHitBox()
        {
            List<(int left, int top)> hitBox = new List<(int left, int top)>();

            for (int i = 0; i < displayText.Length; i++)
            {
                int left = defaultLeft;
                int top = ui.Horizon - 1 - i - currentHeight;

                foreach (char c in displayText[displayText.Length - i - 1])
                {
                    if (c != ' ') hitBox.Add((left, top));

                    left++;
                }
            }

            return hitBox;
        }

        private int CalculateJumpHeight()
        {
            return (int)Math.Floor(-4 * JUMP_HEIGHT / Math.Pow(JUMP_LENGTH, 2) * jumpProgress * (jumpProgress - JUMP_LENGTH));
        }

        public void Display()
        {
            for (int i = 0; i < displayText.Length; i++)
            {
                Console.SetCursorPosition(defaultLeft, ui.Horizon - 1 - i - currentHeight);

                foreach (char c in displayText[displayText.Length - i - 1])
                {
                    if (c == ' ') Console.CursorLeft++;
                    else Console.Write(c);
                }
            }
        }

        private void CleanUp()
        {
            for (int i = 0; i < displayText.Length; i++)
            {
                Console.SetCursorPosition(defaultLeft, ui.Horizon - 1 - i - currentHeight);

                foreach (char c in displayText[displayText.Length - i - 1])
                {
                    if (c == ' ') Console.CursorLeft++;
                    else Console.Write(' ');
                }
            }
        }
    }
}
