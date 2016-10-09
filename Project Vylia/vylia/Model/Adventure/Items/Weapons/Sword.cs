using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model.Adventure.Items.Weapons
{
    abstract class Sword : Weapon
    {
        public override WeaponType getWeaponType()
        {
            return WeaponType.Sword;
        }
    }
}
