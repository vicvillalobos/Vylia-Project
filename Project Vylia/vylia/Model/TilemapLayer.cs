using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project_Vylia.vylia.Model
{
    public class TilemapLayer
    {
        private int[] tiles;

        private string name;

        [XmlAttribute("name")]
        public string Name { get { return name; } set { name = value; } }

        public TilemapLayer()
        {}

        public String TileMapArray;

        public int[] Tiles { get { return tiles; } set { tiles = value; } }
    }
}
