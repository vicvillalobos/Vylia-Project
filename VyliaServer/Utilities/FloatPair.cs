using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyliaServer.Utilities
{
    class FloatPair
    {
        public float X { get; set; }
        public float Y { get; set; }

        public FloatPair()
        {
            this.X = 0;
            this.Y = 0;
        }

        public FloatPair(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
