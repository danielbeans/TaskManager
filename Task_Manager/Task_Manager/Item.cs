using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager
{
    interface IItem
    {
        string Name { get; set; }
        string Description { get; set; }
        DateTime DateAdded { get; }
        int ItemID { get; }
    }
    class Item
    {
        private static int s_currentID = 0;

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; }
        public int ItemID { get; }

        public Item()
        {
            DateAdded = DateTime.Now;
            ItemID = s_currentID;
            s_currentID++;
        }

        public Item(string name, string description) : this()
        {
            Name = name;
            Description = description;
        }

        public Item(Item item)
        {
            Name = item.Name;
            Description = item.Description;
            DateAdded = item.DateAdded;
            ItemID = item.ItemID;
        }
    }
}
