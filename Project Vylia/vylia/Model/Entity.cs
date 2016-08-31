using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_Vylia.vylia.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model
{
    public class Entity : GameObject
    {
        public Entity() : base()
        {
        }

        private int HealthPoints;
        private int MaxHealth;

        private int StaminaPoints;
        private int MaxStamina;

        private int ManaPoints;
        private int MaxMana;

        public virtual void ActionEventHandler(Entity e) { }

        public virtual void TouchEventHandler(Entity e) { }

        public virtual void GetInput(KeyboardState frameInput, KeyboardState pastInput) { }

        public virtual void GetInput(GamePadState frameInput, GamePadState pastInput) { }

        public override void Update(GameTime gt, Tilemap map) {
            base.Update(gt,map);

        }

        public virtual void Draw(SpriteBatch spr) { }
    }
}
