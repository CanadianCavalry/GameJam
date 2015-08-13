using System.Collections.Generic;

namespace GameJam
{
    class Player
    {
        public Area currentLocation;
        private List<Item> inventory;
        private Item mainHand;
        private Item offHand;
        private Item armor;
        private int armorRating;
        private bool alive;

        public Player()
        {
            inventory = new List<Item>();
            alive = true;
            mainHand = null;
            offHand = null;
            armor = null;
            armorRating = 0;
        }

        public void addItem(Item itemToAdd)
        {
            inventory.Add(itemToAdd);
        }

        public void removeItem(Item itemToRemove)
        {
            inventory.Remove(itemToRemove);
        }

        public string defend()
        {
            return "You take a defensive stance.";
        }

        public bool isAlive()
        {
            return alive;
        }

        internal void addToInventory(GameJam.Item item)
        {
            inventory.Add(item);
        }

        internal void removeFromInventory(GameJam.Item item)
        {
            inventory.Remove(item);
        }

        internal List<GameObject> getInventory()
        {
            List<GameObject> inventoryContents = new List<GameObject>(inventory);
            return inventoryContents;
        }
    }
}