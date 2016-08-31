using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model
{
    public class TilemapCollisionLayer
    {
        private int[] collisionTileIndexes;

        public TilemapCollisionLayer() { }

        public String TileMapArray;

        public int[] CollisionTiles
        {
            get { return collisionTileIndexes; }
            set { collisionTileIndexes = value; }
        }

    }
}
