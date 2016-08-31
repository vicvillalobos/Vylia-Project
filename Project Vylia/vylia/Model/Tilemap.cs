using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Vylia.vylia.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project_Vylia.vylia.Model
{
    public class Tilemap
    {

        private String name;
        private String version;
        private String slug;
        private int width;
        private int height;
        private Tileset tileset;
        private String tilesetStr;
        private int colFirstID;

        private TilemapLayer[] map;
        private Actor[] actorList;
        private TilemapCollisionLayer colLayer;

        public List<int> CollisionList;

        public TilemapLayer[] Map {
            get { return map; }
            set { map = value; }
        }

        public Actor[] ActorList
        {
            get { return actorList; }
            set { actorList = value; }
        }

        public TilemapCollisionLayer CollisionLayer
        {
            get { return colLayer; }
            set { colLayer = value; }
        }

        public int CollisionFirstID
        {
            get { return colFirstID; }
            set { colFirstID = value; }
        }

        public Tilemap()
        {

        }

        public void DrawTile(SpriteBatch spr, Vector2 cameraPosition, int layer, int i, int j)
        {
            int index = GetIndexFromCords((int)Math.Floor(i / GameSettings.GridSize), (int)Math.Floor(j / GameSettings.GridSize));
            if(index < map[layer].Tiles.Length && index >= 0)
                Tileset.DrawTile(spr, map[layer].Tiles[index],new Vector2(i - cameraPosition.X,j - cameraPosition.Y));
        }

        public void DrawCollisionTile(SpriteBatch spr, Vector2 cameraPosition, int layer, int i, int j, Texture2D blankDot)
        {
            int index = GetIndexFromCords((int)Math.Floor(i / GameSettings.GridSize), (int)Math.Floor(j / GameSettings.GridSize));
            if (index < CollisionLayer.CollisionTiles.Length && index >= 0)
            {
                float r, g, b, a;
                switch(CollisionLayer.CollisionTiles[index] - CollisionFirstID)
                {
                    default:
                        r = 0;
                        g = 0;
                        b = 0;
                        a = 0.1f;
                        break;
                    case (int)CollisionTiles.GROUND:
                        r = 0;
                        g = 0;
                        b = 255;
                        a = 0.2f;
                        break;
                    case (int)CollisionTiles.SOLID:
                        r = 255;
                        g = 0;
                        b = 0;
                        a = 0.2f;
                        break;

                }

                DrawHelper.drawRectangle(spr, blankDot, new Rectangle((int) (i - cameraPosition.X), (int)(j - cameraPosition.Y), (int)GameSettings.GridSize, (int)GameSettings.GridSize),new Color(r,g,b,a));
            }
        }

        public int GetIndexFromCords(int x, int y)
        {
            return y * this.width + x;
        }
        
        public int GetIndexFromCordsP(float x, float y)
        {
            int gy = (int)Math.Floor((y / GameSettings.GridSize));
            int gx = (int)Math.Floor((x / GameSettings.GridSize));

            return (int)( gy * this.width + gx );
        }

        public Vector2 GetCordsFromIndex(int index)
        {
            return new Vector2((int) (index % this.width),(int)(Math.Floor((float) index / this.width)));
        }

        public Vector2 GetCordsFromIndexP(int index)
        {
            return new Vector2((int)(index % this.width) * GameSettings.GridSize, (int)(Math.Floor((float)(index / this.width)) * GameSettings.GridSize));
        }

        public Tileset Tileset { get { return tileset; } set { tileset = value; } }

        public String TilesetFileName
        {
            get { return tilesetStr; }
            set {
                tileset = new Tileset(value);
                tilesetStr = value;
            }
        }

        public Tilemap(String filename)
        {
            // Load xml file with contents
            XMLManager<Tilemap> xmlGameScreenManager = new XMLManager<Tilemap>();
            xmlGameScreenManager.Type = this.GetType();
            Tilemap d = xmlGameScreenManager.Load(filename);

            this.CollisionList = new List<int>();
            this.width = d.Width;
            this.height = d.Height;
            this.name = d.Name;
            this.version = d.Version;
            this.slug = d.Slug;
            this.map = d.Map;
            this.colLayer = d.CollisionLayer;
            this.tileset = d.Tileset;
            this.actorList = d.ActorList;
            this.colFirstID = d.CollisionFirstID;

            foreach(TilemapLayer layer in map)
            {
                string[] split = layer.TileMapArray.Split(new char[1] { ',' });
                List<int> numbers = new List<int>();
                int parsed;

                foreach (string n in split)
                {
                    if (int.TryParse(n, out parsed))
                        numbers.Add(parsed);
                }
                layer.Tiles = numbers.ToArray();
            }

            // load col layer

            string[] splitx = colLayer.TileMapArray.Split(new char[1] { ',' });
            List<int> numbersx = new List<int>();
            int parsedx;

            foreach (string n in splitx)
            {
                if (int.TryParse(n, out parsedx))
                    numbersx.Add(parsedx);
            }
            colLayer.CollisionTiles = numbersx.ToArray();


            ScreenManager.Instance.addError(new Utilities.GameError("Capas del mapa cargadas."));

            if (actorList == null || actorList.Length <= 0) {
                Console.WriteLine("ActorList not found or empty.");
            }
            else
            {
                for (int i = 0; i < actorList.Length; i++)
                {
                    actorList[i] = actorList[i].LoadFromSource();
                    actorList[i].Initialize();
                    Console.WriteLine("Actor {0} Dimensions: {1}", actorList[i].Name, actorList[i].Dimensions);   
                }
            }

        }

        public bool checkCollision(Rectangle pos)
        {
            CollisionList.RemoveRange(0,CollisionList.Count);
            int[] index = new int[4];

            index[0] = GetIndexFromCordsP(pos.X, pos.Y);   // Izq. arriba
            index[1] = GetIndexFromCordsP(pos.X + pos.Width, pos.Y);   // Der. arriba
            index[2] = GetIndexFromCordsP(pos.X, pos.Y + pos.Height);   // Izq. abajo
            index[3] = GetIndexFromCordsP(pos.X + pos.Width, pos.Y + pos.Height);   // Der. abajo

            foreach (int i in index)
            {
                int colTile = colLayer.CollisionTiles[i] - CollisionFirstID;
                if (colTile == (int)CollisionTiles.SOLID || colTile == (int)CollisionTiles.WATER)
                {
                    CollisionList.Add(i);
                    return false;
                }
            }

            return true;

        }

        public string Name
        {
            get { return name; }
            set { name = value;}
        }

        public string Version
        {
            get
            {
                return version;
            }

            set
            {
                version = value;
            }
        }

        public string Slug
        {
            get
            {
                return slug;
            }

            set
            {
                slug = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        private enum CollisionTiles { GROUND,SOLID,WATER,STAIRS }
    }
}
