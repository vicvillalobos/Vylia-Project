using Microsoft.Xna.Framework;
using static Project_Vylia.vylia.GameSettings;

namespace Project_Vylia.vylia.Model
{
    class Camera : GameObject
    {
        private GameObject target;
        private int deadzone = 1;

        public Camera(GameObject go, Tilemap map)
        {
            dimensions = new Vector2(GAME_SCREEN_WIDTH,GAME_SCREEN_HEIGHT);
            Target = go;
            setPositionByTarget(map);
        }

        public GameObject Target { set { target = value; } }

        public void setPositionByTarget(Tilemap map)
        {
            if (this.target.Position.X <= (this.Dimensions.X / 2))
            {
                this.position.X = 0;

            } else if(this.target.Position.X >= (map.Width * GAME_GRID_SIZE) - (this.Dimensions.X / 2))
            {
                this.position.X = (map.Width * GAME_GRID_SIZE) - this.Dimensions.X;
            } else
            {
                this.position.X = this.target.CenterPosition.X - (this.Dimensions.X / 2);
            }


            if (this.target.Position.Y <= (this.Dimensions.Y / 2))
            {
                this.position.Y = 0;

            }
            else if (this.target.Position.Y >= (map.Height * GAME_GRID_SIZE) - (this.Dimensions.Y / 2))
            {
                this.position.Y = (map.Height * GAME_GRID_SIZE) - this.Dimensions.Y;
            }
            else
            {
                this.position.Y = this.target.Position.Y - (this.Dimensions.Y / 2);
            }

        }

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
                    if (position.X + dimensions.X < map.Width * GAME_GRID_SIZE)
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
                    if(position.Y + dimensions.Y < map.Height * GAME_GRID_SIZE)
                    position.Y += chaseSpeed;
                }
            }

        }
        public Vector2 adjustVector2(Vector2 r)
        {
            r.X -= (int)Position.X;
            r.Y -= (int)Position.Y;
            return r;
        }

        public Rectangle adjustRectangle(Rectangle r)
        {
            r.X -= (int) Position.X;
            r.Y -= (int) Position.Y;
            return r;
        }

    }
}
