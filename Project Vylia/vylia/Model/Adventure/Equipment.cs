using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model.Adventure
{
    abstract class Equipment : Item
    {
        public override Type getItemType()
        {
            return Type.Equipment;
        }

        public abstract EquipmentType getEquipmentType();

        public enum EquipmentType { Head_Top, Head_Mid, Head_Low, Armor, Hands, Feet, Earring, Ring, Spirit }
    }
}
