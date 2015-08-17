using System.Collections.Generic;

namespace GameJam
{
    public class Player
    {
        public Area currentLocation;
        public List<Item> inventory;
        private int health;
        private int air;
        private const int maxAir = 4;
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

        public void breath()
        {
            bool submerged = currentLocation.isSubmerged();
            if (submerged == true)
            {
                reduceAir();
                return;
            }

            air++;

            if (air > maxAir)
            {
                air = maxAir;
            }
        }

        public void reduceAir()
        {
            air--;

            if (air < 0)
            {
                air = 0;
            }
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

            if ((air <= 0) && (currentLocation.isSubmerged()))
            {
                return false;
            }

            return true;
        }

        public Item getMainWeapon()
        {
            return mainHand;
        }

        public void takeDamage(int damage)
        {
            damage -= armorRating;
            health -= damage;

            if (health < 0)
            {
                health = 0;
            }
        }
    }
}