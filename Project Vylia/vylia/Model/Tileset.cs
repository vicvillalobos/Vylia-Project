﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static Project_Vylia.vylia.GameSettings;

namespace Project_Vylia.vylia.Model
{
    public class Tileset
    {
        private String name;
        private int imageWidth;
        private int imageHeight;
        private int tileSize;

        private String fileName;
        private Tile[] tileList;
        private Texture2D texture;

        public Tileset() { }

        public Tileset(String filename)
        {
            Tileset t = new Tileset();
            XMLManager<Tileset> xmlGameScreenManager = new XMLManager<Tileset>();
            xmlGameScreenManager.Type = this.GetType();
            t = xmlGameScreenManager.Load(filename);

            ScreenManager.Instance.addError(new Utilities.GameError("Tileset cargado."));

            this.name = t.Name;
            this.imageWidth = t.Width;
            this.imageHeight = t.Height;
            this.fileName = t.File;
            this.tileSize = t.TileSize;

            this.tileList = new Tile[imageWidth * imageHeight];

            for(int i = 1; i < imageWidth * imageHeight; i++)
            {
                Vector2 tex = new Vector2(
                    (i - 1) % imageWidth
                    ,
                    (int) Math.Floor((i - 1) / (float)imageWidth)
                    );
                tileList[i] = new Tile(i, tex, "AutoGenerated Tile", TileType.Ground);
            }

            foreach(Tile tile in t.Tiles)
            {
                this.tileList[tile.Id] = tile;
            }
            
        }

        public void DrawTile(SpriteBatch spr, int index, Vector2 pos)
        {
            if (index < tileList.Length && index > 0)
            {

                spr.Draw(this.texture, new Rectangle((int)pos.X, (int)pos.Y, (int)GAME_GRID_SIZE, (int)GAME_GRID_SIZE), new Rectangle((int)(tileList[index].TexturePosition.X * TileSize), (int)(tileList[index].TexturePosition.Y * TileSize), TileSize, TileSize), Color.White);
            }
        }

        public void LoadTexture(GameScreen gs)
        {
            texture = gs.Content.Load<Texture2D>(fileName);
        }

        public String Name { get { return name; } set { name = value; } }
        public int Width { get { return imageWidth; } set { imageWidth = value; } }
        public int Height { get { return imageHeight; } set { imageHeight = value; } }
        public int TileSize { get { return tileSize; } set { tileSize = value; } }
        public String File { get { return fileName; } set { fileName = value; } }
        public Tile[] Tiles { get { return tileList; } set { tileList = value; } }
    }
}
