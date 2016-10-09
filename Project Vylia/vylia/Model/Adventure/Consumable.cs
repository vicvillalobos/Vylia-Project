using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model.Adventure
{
    abstract class Consumable : Item
    {
        public override Item.Type getItemType()
        {
            return Item.Type.Consumable;
        }

        public abstract ConsumableType getConsumableType();

        public enum ConsumableType { HP_Potion, MP_Potion, STA_Potion, Effect_Potion, HP_Poison, MP_Poison, STA_Poison }

        public abstract void activeEffect();

        public abstract void onConsume();

        public abstract void onDrop();

        public abstract void onBattleConsume();

    }
}
