using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model
{
    public class Tile
    {
        private int id;
        private Vector2 texCoords;
        private String name;
        private TileType type;

        public Tile()
        {

        }

        public Tile(int id, Vector2 texCoords, String name, TileType type) {
            this.id = id;
            this.texCoords = texCoords;
            this.name = name;
            this.type = type;
        }

        public int Id { get { return id; } set { id = value; } }
        public Vector2 TexturePosition { get { return texCoords; } set { texCoords = value; } }
        public String Name { get { return name; } set { name = value; } }
        public TileType Type { get { return type; } }
        public String TileTypeName {
            set {
                TileType result;
                if (Enum.TryParse(value,out result))
                {
                    type = result;
                } else
                {
                    type = TileType.Void;
                }
            }
            get
            {
                return type.ToString();
            }
        }


    }
}
