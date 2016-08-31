using Project_Vylia.vylia.Model.Adventure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Utilities
{
    class ItemDatabase
    {

        private List<Item> items;
        private List<Consumable> consumables;
        private List<Equipment> equipments;
        private List<QuestItem> questItems;
        private List<Weapon> weapons;
        private List<Ammo> ammos;

        public List<Item> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
            }
        }

        public List<Consumable> Consumables
        {
            get
            {
                return consumables;
            }

            set
            {
                consumables = value;
            }
        }

        public List<Equipment> Equipments
        {
            get
            {
                return equipments;
            }

            set
            {
                equipments = value;
            }
        }

        public List<QuestItem> QuestItems
        {
            get
            {
                return questItems;
            }

            set
            {
                questItems = value;
            }
        }

        public List<Weapon> Weapons
        {
            get
            {
                return weapons;
            }

            set
            {
                weapons = value;
            }
        }

        public List<Ammo> Ammos
        {
            get
            {
                return ammos;
            }

            set
            {
                ammos = value;
            }
        }

        public ItemDatabase()
        {
            // Load xml file with contents
            XMLManager<ItemDatabase> xmlGameScreenManager = new XMLManager<ItemDatabase>();
            xmlGameScreenManager.Type = this.GetType();
            ItemDatabase d = xmlGameScreenManager.Load("vylia/Load/Inventory/ItemDatabase.xml");

            this.ammos = d.ammos;
            this.consumables = d.consumables;
            this.equipments = d.equipments;
            this.items = d.items;
            this.questItems = d.questItems;
            this.weapons = d.weapons;

        }
    }
}
