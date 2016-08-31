using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model.Adventure
{
    class ActorRace
    {
        private Type type;

        public enum Type
        {
            Nara,High_Elf,Beast,
            Human,Elf,
            Human_Elf,
            High_Human, Divine_Elf, Dark_Creature,
            Beastling, Slime, Archangel, Devil
        }

        public ActorRace(Type type)
        {
            this.type = type;
        }
    }
}
