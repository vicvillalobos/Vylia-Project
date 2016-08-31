using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model.Entities
{
    class Warp : Entity
    {
        private String map;
        private Vector2 pos;

        public String DestinationMap { get { return map; } set { map = value; } }

        public Vector2 DestinationPosition { get { return pos; } set { pos = value; } }

        public Warp () : base()
        {}

        public Warp(String m, Vector2 p) : base()
        {
            this.map = m;
            this.pos = p;
        }

        public override void TouchEventHandler(Entity e)
        {
            base.TouchEventHandler(e);

            // move player to destination map and position
        }
    }
}
