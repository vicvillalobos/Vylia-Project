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
    }
}
