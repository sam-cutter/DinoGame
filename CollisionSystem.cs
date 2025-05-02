using System;
using System.Collections.Generic;

namespace DinoGame
{
    public interface ICollidable
    {
        List<(int left, int top)> GenerateHitBox();
    }

    class CollisionSystem
    {
        public static int CheckCollisions(ICollidable first, ICollidable second)
        {
            int collisions = 0;

            foreach ((int, int) coordinate in first.GenerateHitBox())
            {
                foreach ((int, int) otherCoordinate in second.GenerateHitBox())
                {
                    if (coordinate == otherCoordinate)
                    {
                        Console.SetCursorPosition(coordinate.Item1, coordinate.Item2);
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write('*');
                        Console.ResetColor();

                        collisions++;
                    }
                }
            }

            return collisions;
        }
    }
}
