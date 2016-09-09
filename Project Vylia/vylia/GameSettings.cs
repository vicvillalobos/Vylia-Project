using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia
{
    class GameSettings
    {
        
        public const int GAME_SCREEN_WIDTH = 800;
        
        public const int GAME_SCREEN_HEIGHT = 450;
        
        public const bool GAME_FULL_SCREEN = false;

        //public const float GridSize = 0.04f * ScreenWidth;
        
        public const float GAME_GRID_SIZE = 32;

        public const float GAME_BASE_SPEED = GAME_GRID_SIZE / 32;
        
        public static bool GAME_DEBUG_MODE = false;
    }
}
