using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model.Entities
{
    public class Player : Actor
    {

        public Player()
        {
            baseSpeed = new Vector2(WALKSPEED, WALKSPEED);
            GridPosition = new Vector2(11,10);
            dimensions = new Vector2(32, 48);
            textureURL = "sprites/f_actor2";
            name = "Player";
            money = 2000;
            nonPlayerActor = false;
            Initialize();
        }
        
        public override void GetInput(GamePadState frameInput, GamePadState pastInput)
        {
            base.GetInput(frameInput, pastInput);

            if (frameInput.ThumbSticks.Left.Length() > 0)
            {
                moving = MOVING;
            }
            else
            {
                moving = IDLE;
            }

            if (Math.Abs(frameInput.ThumbSticks.Left.X) > Math.Abs(frameInput.ThumbSticks.Left.Y))
            {
                speed.Y = 0;
                if (frameInput.ThumbSticks.Left.X < 0)
                {
                    facing = POSITION_LEFT;
                    Move(new Vector2(frameInput.ThumbSticks.Left.X, 0));
                }
                else if (frameInput.ThumbSticks.Left.X > 0)
                {
                    facing = POSITION_RIGHT;
                    Move(new Vector2(frameInput.ThumbSticks.Left.X,0));
                }
                else
                    speed.X = 0;


                ANIMATION_CYCLE_MS = (int)(Math.Abs(2 - Math.Abs(frameInput.ThumbSticks.Left.X)) * (frameInput.IsButtonDown(Buttons.A) ? 400 : 800));
            }
            else
            {
                speed.X = 0;
                if (frameInput.ThumbSticks.Left.Y < 0)
                {
                    facing = POSITION_DOWN;
                    Move(new Vector2(0, -1 * frameInput.ThumbSticks.Left.Y));
                }
                else if (frameInput.ThumbSticks.Left.Y > 0)
                {
                    facing = POSITION_UP;
                    Move(new Vector2(0, -1 * frameInput.ThumbSticks.Left.Y));
                }
                else
                    speed.Y = 0;

                ANIMATION_CYCLE_MS = (int)(Math.Abs(2 - Math.Abs(frameInput.ThumbSticks.Left.Y)) * (frameInput.IsButtonDown(Buttons.A) ? 400 : 800));
            }

            if (frameInput.IsButtonDown(Buttons.DPadLeft))
            {
                facing = POSITION_LEFT;
                Move(new Vector2(-1,0));
                ANIMATION_CYCLE_MS = 800;
                moving = MOVING;
            }
            else if (frameInput.IsButtonDown(Buttons.DPadUp))
            {
                facing = POSITION_UP;
                Move(new Vector2(0, -1));
                ANIMATION_CYCLE_MS = 800;
                moving = MOVING;
            }
            else if(frameInput.IsButtonDown(Buttons.DPadRight))
            {
                facing = POSITION_RIGHT;
                Move(new Vector2(1,0));
                ANIMATION_CYCLE_MS = 800;
                moving = MOVING;
            }
            else if(frameInput.IsButtonDown(Buttons.DPadDown))
            {
                facing = POSITION_DOWN;
                Move(new Vector2(0, 1));
                ANIMATION_CYCLE_MS = 800;
                moving = MOVING;
            }

            if (ANIMATION_CYCLE_MS <= 0)
                ANIMATION_CYCLE_MS = 800;

        }

        public override void GetInput(KeyboardState frameInput, KeyboardState pastInput)
        {
            base.GetInput(frameInput, pastInput);


            Speed = Vector2.Zero;
            moving = MOVING;

            if (frameInput.IsKeyDown(Keys.OemPlus) && !pastInput.IsKeyDown(Keys.OemPlus))
            {
                this.money += 200;
            }

            if (frameInput.IsKeyDown(Keys.OemMinus) && !pastInput.IsKeyDown(Keys.OemMinus))
            {
                this.money -= 200;
                if (money < 0)
                    money = 0;
            }

            if (!pastInput.IsKeyDown(Keys.X) && frameInput.IsKeyDown(Keys.X))
            {
                Run();
            }

            if (pastInput.IsKeyDown(Keys.X) && !frameInput.IsKeyDown(Keys.X))
            {
                Walk();
            }

            if (frameInput.IsKeyDown(Keys.Left))
            {
                facing = POSITION_LEFT;
                Move(new Vector2(-1, 0));
            }
            else
                if (frameInput.IsKeyDown(Keys.Up))
            {
                facing = POSITION_UP;
                Move(new Vector2(0,-1));
            }
            else
                if (frameInput.IsKeyDown(Keys.Right))
            {
                facing = POSITION_RIGHT;
                Move(new Vector2(1,0));
            }
            else
            if (frameInput.IsKeyDown(Keys.Down))
            {
                facing = POSITION_DOWN;
                Move(new Vector2(0, 1));
            } else
            {
                moving = IDLE;
            }

        }

        public override void Update(GameTime gameTime, Tilemap map)
        {
            base.Update(gameTime,map);
            
        }

        public bool IsActionValid(Rectangle actorHitbox)
        {
            Vector2 actionPoint = getActionPoint();

            return (
                actionPoint.X > actorHitbox.X && actionPoint.X < actorHitbox.X + actorHitbox.Width &&
                actionPoint.Y > actorHitbox.Y && actionPoint.Y < actorHitbox.Y + actorHitbox.Height
                ) ;
            
        }

        public Vector2 getActionPoint()
        {
            switch (facing)
            {
                case POSITION_LEFT:
                    return new Vector2(this.Position.X - (Dimensions.X / 2), this.Position.Y + (this.Dimensions.Y / 2));
                case POSITION_UP:
                    return new Vector2(this.Position.X + (Dimensions.X / 2), this.Position.Y - (this.Dimensions.Y / 2));
                case POSITION_RIGHT:
                    return new Vector2(this.Position.X + (Dimensions.X * 1.5f), this.Position.Y + (this.Dimensions.Y / 2));
                default:
                case POSITION_DOWN:
                    return new Vector2(this.Position.X + (Dimensions.X / 2), this.Position.Y + (this.Dimensions.Y * 1.5f));
            }
        }
        
        public int ANIMATION_CYCLE_MS = 800;
    }
}
