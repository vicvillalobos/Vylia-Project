using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model.Adventure
{
    class ActorClass
    {
        private Type type;

        public enum Type {
            Civilian, Soldier,
            Warrior, Archer, Alchemist, Merchant, Swordsman, Assassin, Wizard, Sage, Lancer, Priest, Templar,
            Devil, Animal
        }

        public ActorClass(Type type)
        {
            this.type = type;
        }

    }
}
