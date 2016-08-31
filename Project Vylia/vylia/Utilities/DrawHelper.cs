using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Utilities
{
    class DrawHelper
    {

        public static void drawRectangle(SpriteBatch graphics, Texture2D blankDot, Rectangle r, Color color)
        {
            graphics.Draw(blankDot, r, color);
        }
    }
}
