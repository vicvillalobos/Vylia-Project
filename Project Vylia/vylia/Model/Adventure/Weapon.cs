using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model.Adventure
{
    public abstract class Weapon : Item
    {
        public enum WeaponType { Sword, Greatsword, WarAxe, BattleAxe, Club, Staff, Knuckles, Claws, Bow, Crossbow, Knife, Spear }

        public override Type getItemType()
        {
            return Item.Type.Weapon;
        }

        public abstract WeaponType getWeaponType();

        public abstract String getEffectsDescription();

        public abstract float getBaseAttackDamage();
        public abstract float getAttackDamageRate();

        public abstract float getBaseMagicDamage();
        public abstract float getMagicDamageRate();

        public abstract float getBaseParry();
        public abstract float getBaseMagicResistance();

        public abstract void executeParryEffect();

        public abstract void executeAttackEffect();

        public override Vector2 getIconCoords()
        {
            return new Vector2(1,0);
        }

    }
}
