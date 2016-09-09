using Microsoft.Xna.Framework;
using Project_Vylia.vylia.GameScreens;
using Project_Vylia.vylia.Utilities;
using System;
using static Project_Vylia.vylia.GameSettings;

namespace Project_Vylia.vylia.Model.Entities
{
    public class Warp : Entity
    {
        private bool executed = false;
        private String map;
        private Vector2 pos;
        private String source;
        private long StartMs = 0;
        private long ExecuteMs = 0;
        private long EndMs = 0;

        public String DestinationMap { get { return map; } set { map = value; } }

        public Vector2 DestinationPosition { get { return pos; } set { pos = value; } }

        public String Source { get { return source; } set { source = value; } }

        public Warp () : base()
        {
            this.Type = this.GetType();
            dimensions.X = GAME_GRID_SIZE;
            dimensions.Y = GAME_GRID_SIZE;
        }

        public Warp(String m, Vector2 p) : base()
        {
            this.map = m;
            this.pos = p;
        }
        
        public Warp LoadFromSource()
        {
            XMLManager<Warp> xmlGameScreenManager = new XMLManager<Warp>();
            xmlGameScreenManager.Type = this.Type;
            return xmlGameScreenManager.Load(this.Source);
        }

        public void TouchEventHandler(Player p, LocalMap lm)
        {
            base.TouchEventHandler(p);

            p.Speed = new Vector2(0, 0);
            p.Moving = 0;

            long now = TimeHelper.getActualMiliseconds();
            if (StartMs == 0)
            {
                lm.fadeOut(300);
                StartMs = now;

                ExecuteMs = StartMs + 300;

                EndMs = ExecuteMs + 300;
            }

            // move player to destination map and position
            if (ExecuteMs != 0 && now > ExecuteMs)
            {
                Console.WriteLine(now);
                lm.UnloadContent();
                lm.InitialMap = this.DestinationMap;
                p.GridPosition = this.DestinationPosition;
                lm.Initialize();
                lm.LoadContent();
            }

        }

        public void checkEffect(Player p, LocalMap lm)
        {

            Rectangle pos = p.CollisionBox;
            Rectangle wpos = new Rectangle((int) Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);

            
            if (GridHelper.CollisionDetect(pos,wpos))
            {
                 TouchEventHandler(p,lm);
            }
        }
    }
}
