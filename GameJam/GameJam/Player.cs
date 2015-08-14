using System.Collections.Generic;

namespace GameJam
{

    public class Player
    {
        public Area currentLocation;
        public List<Item> inventory;
        private int health;
        private int air;
        private Item mainHand;
        private Item offHand;
        private Item armor;
        private int armorRating;

        public Player()
        {
            inventory = new List<Item>();
            health = 10;
            air = 4;
            mainHand = null;
            offHand = null;
            armor = null;
            armorRating = 0;
        }

        public int getAir()
        {
            return air;
        }

        public void reduceAir()
        {
            air--;
        }

        public void addItem(Item itemToAdd)
        {
            inventory.Add(itemToAdd);
        }

        public void removeItem(Item itemToRemove)
        {
            inventory.Remove(itemToRemove);
        }

        public string equip(Item item)
        {
            mainHand = item;
            return "You equip the " + item.name + ".";
        }

        public string unequip()
        {
            mainHand = null;
            return "You free your hands.";
        }

        public string defend()
        {
            return "You take a defensive stance.";
        }

        public bool isAlive()
        {
            if (health <= 0)
            {
                return false;
            }
            else if ((air <= 0) && (currentLocation.isSubmerged()))
            {
                return false;
            }
            return true;
        }
    }
}