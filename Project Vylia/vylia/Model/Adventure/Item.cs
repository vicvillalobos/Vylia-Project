using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model.Adventure
{
    public abstract class Item
    {
        
        public enum Type { Consumable,Quest,Equipment,Weapon,Ammo,Misc }

        public abstract Type getItemType();

        public abstract int getID();
        public abstract String getName();
        public abstract String getDescription();
        public virtual Vector2 getIconCoords()
        {
            return new Vector2(0, 0);
        }

    }
}
