using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia
{
    class GameSettings
    {

        public const int ScreenWidth = 800;
        public const int ScreenHeight = 450;

        public const bool FullScreen = false;

        //public const float GridSize = 0.04f * ScreenWidth;
        public const float GridSize = 32;

        public const float GameBaseSpeed = GridSize / 32;

        public static bool DebugMode = false;
    }
}
