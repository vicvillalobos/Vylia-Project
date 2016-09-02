using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Utilities
{
    class GridHelper
    {
        public static Vector2 PixelToGridPosition(Vector2 pixel)
        {
            return new Vector2((float)Math.Floor(pixel.X / GameSettings.GridSize), (float)Math.Floor(pixel.Y / GameSettings.GridSize));
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
