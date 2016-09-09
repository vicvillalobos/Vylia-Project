using Microsoft.Xna.Framework;
using System;
using static Project_Vylia.vylia.GameSettings;

namespace Project_Vylia.vylia.Utilities
{
    class GridHelper
    {
        public static Vector2 PixelToGridPosition(Vector2 pixel)
        {
            return new Vector2((float)Math.Floor(pixel.X / GAME_GRID_SIZE), (float)Math.Floor(pixel.Y / GAME_GRID_SIZE));
        }

        public static bool CollisionDetect(Rectangle rect1, Rectangle rect2)
        {

            return (rect1.X < rect2.X + rect2.Width &&
           rect1.X + rect1.Width > rect2.X &&
           rect1.Y < rect2.Y + rect2.Height &&
           rect1.Height + rect1.Y > rect2.Y) ;

        }
    }
}
