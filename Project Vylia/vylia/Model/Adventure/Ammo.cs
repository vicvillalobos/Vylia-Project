using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model.Adventure
{
    abstract class Ammo : Item
    {
        public override Type getItemType()
        {
            return Type.Ammo;
        }

        public abstract AmmoType getAmmoType();

        public enum AmmoType { Arrow, Bolt, Stone, Mana_Crystal }
    }
}
