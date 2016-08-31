using Project_Vylia.vylia.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia
{
    class ItemManager
    {
        private static ItemManager instance;

        private ItemDatabase database;

        public static ItemManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ItemManager();

                return instance;
            }
        }

        public ItemDatabase Inventory
        {
            get { return database; }
        }

        private ItemManager()
        {
            // Load Item db
            database = new ItemDatabase();
               
        }

    }
}
