using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model.Adventure
{
    abstract class QuestItem : Item
    {
        public override Item.Type getItemType()
        {
            return Type.Quest;
        }

        public abstract QuestItemType getQuestItemType();

        public enum QuestItemType { Active, Material }
    }
}
