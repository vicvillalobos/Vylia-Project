﻿using Microsoft.Xna.Framework;
using System;
using static Project_Vylia.vylia.GameSettings;

namespace Project_Vylia.vylia.Model
{
    public class GameObject
    {
        protected Vector2 position;
        protected Vector2 dimensions;
        protected Vector2 speed;
        protected Vector2 baseSpeed;

        public Type Type { get; set; }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 BaseSpeed
        {
            get { return baseSpeed; }
            set { baseSpeed = value; }
        }

        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        public Vector2 CenterPosition
        {
            get { return new Vector2(position.X + (float) Math.Floor(dimensions.X / 2f), position.Y + (float) Math.Floor(dimensions.Y / 2f)); }
            set { position = new Vector2(value.X - (float) Math.Floor(dimensions.X / 2f), value.Y - (float) Math.Floor(dimensions.Y / 2f)); }
        }

        public Vector2 CenterGridPosition
        {
            get { return new Vector2((float)Math.Floor(CenterPosition.X / GAME_GRID_SIZE), (float)Math.Floor(CenterPosition.Y / GAME_GRID_SIZE)); }
        }

        public Vector2 GridPosition
        {
            get { return new Vector2((float)Math.Floor(position.X / GAME_GRID_SIZE), (float)Math.Floor(position.Y / GAME_GRID_SIZE)); }
            set { position = new Vector2((value.X * GAME_GRID_SIZE), (value.Y * GAME_GRID_SIZE)); }
        }

        public Vector2 Speed { get { return speed; } set { speed = value; } }

        public bool IsVisible {
            get {
                return true;
            }
        }

        public GameObject()
        {
            baseSpeed = new Vector2(1, 1);
            speed = new Vector2(1, 1);
        }

        public void translate(Point p)
        {
            this.position.X += p.X;
            this.position.Y += p.Y;
            ScreenManager.Instance.addError(new Utilities.GameError("Moviendo "+this.GetType().ToString()+"!"));
        }

        public virtual void Update(GameTime gameTime, Tilemap map)
        {
            position.X += speed.X;
            position.Y += speed.Y;
        }

    }
}
