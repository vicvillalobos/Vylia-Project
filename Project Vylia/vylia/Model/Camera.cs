﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Project_Vylia.vylia.GameScreens;

namespace Project_Vylia.vylia.Model
{
    class Camera : GameObject
    {
        private GameObject target;
        private int deadzone = 1;

        public Camera(GameObject go)
        {
            dimensions = new Vector2(GameSettings.ScreenWidth,GameSettings.ScreenHeight);
            Target = go;
        }

        public GameObject Target { set { target = value; } }

        public override void Update(GameTime gameTime, Tilemap map)
        {
            //base.Update(gameTime, map);
            float difX;
            float difY;

            float centerX = CenterPosition.X;
            float centerY = CenterPosition.Y;

            if(target.Position.X <= centerX) // Target esta a la izquierda O Centro
            {
                difX = centerX - target.Position.X;
            }
            else if (target.Position.X == centerX)
            {
                difX = 0;
            }
            else // Target esta a la derecha
            {
                difX = target.Position.X - centerX;
            }

            if(target.Position.Y < centerY) // Target esta arriba?
            {
                difY = centerY - target.Position.Y;
            } else if(target.Position.Y == centerY)
            {
                difY = 0;
            }
            else // Target esta abajo?
            {
                difY = target.Position.Y - centerY;
            }

            if (difX > deadzone) {

                float chaseSpeed = target.BaseSpeed.X;

                //ScreenManager.Instance.addError(new Utilities.GameError("Moviendo camara en el eje X!"));
                if (target.Position.X < centerX)
                {
                    if(position.X > 0)
                        position.X -= chaseSpeed;
                }
                else
                {
                    if (position.X + dimensions.X < map.Width * GameSettings.GridSize)
                    position.X += chaseSpeed;
                }
            }

            if (difY > deadzone)
            {

                float chaseSpeed = target.BaseSpeed.Y;

                //ScreenManager.Instance.addError(new Utilities.GameError("Moviendo camara en el eje Y!"));
                if (target.Position.Y < centerY)
                {
                    if (position.Y > 0)
                    {
                        position.Y -= chaseSpeed;
                    }

                }
                else
                {
                    if(position.Y + dimensions.Y < map.Height * GameSettings.GridSize)
                    position.Y += chaseSpeed;
                }
            }

        }

    }
}