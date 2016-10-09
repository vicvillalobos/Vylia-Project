using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Project_Vylia.vylia.Model.Adventure.Items.Weapons
{
    class WoodenSword : Sword
    {

        public override int getID()
        {
            return 1001;
        }

        public override Vector2 getIconCoords()
        {
            return base.getIconCoords();
        }

        public override void executeAttackEffect()
        {
            throw new NotImplementedException();
        }
        
        public override void executeParryEffect()
        {
            throw new NotImplementedException();
        }

        public override float getAttackDamageRate()
        {
            return 0.4f;
        }

        public override float getBaseAttackDamage()
        {
            return 10;
        }

        public override float getBaseMagicDamage()
        {
            return 0;
        }

        public override float getBaseMagicResistance()
        {
            return 0;
        }

        public override float getBaseParry()
        {
            return 0.1f;
        }

        public override string getDescription()
        {
            return "A wooden stick with the shape of a sword. Sort of.";
        }

        public override string getEffectsDescription()
        {
            return "";
        }

        public override float getMagicDamageRate()
        {
            return 0;
        }

        public override string getName()
        {
            return "Wooden Sword";
        }
    }
}
