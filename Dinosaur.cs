using System;
using System.Threading;

namespace DinoGame
{
    class Dinosaur
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

        public bool Alive { get; } = true;

        private int jumpProgress = 0;
        private const int JUMP_LENGTH = 40;
        private const int JUMP_HEIGHT = 15;

        private int currentHeight = 0;


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

        private int CalculateJumpHeight()
        {
            return (int)Math.Floor(-4 * JUMP_HEIGHT / Math.Pow(JUMP_LENGTH, 2) * jumpProgress * (jumpProgress - JUMP_LENGTH));
        }

        public void Display()
        {
            for (int i = 0; i < displayText.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, ui.Horizon - 1 - i - currentHeight);

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
                Console.SetCursorPosition(Console.WindowWidth / 4, ui.Horizon - 1 - i - currentHeight);

                foreach (char c in displayText[displayText.Length - i - 1])
                {
                    if (c == ' ') Console.CursorLeft++;
                    else Console.Write(' ');
                }
            }
        }
    }
}
