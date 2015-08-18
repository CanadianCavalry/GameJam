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
        private Item gear;
        private Dictionary<string, int> vulnerabilities;

        public Player()
        {
            inventory = new List<Item>();
            health = 10;
            air = 4;
            mainHand = null;
            gear = null;
        }

        public int getAir()
        {
            return air;
        }

        public string breathe()
        {
            string desc = string.Empty;
            bool submerged = currentLocation.isSubmerged();
            if (submerged == true)
            {
                reduceAir();
                desc = describeAirLeft();
                return desc;
            }

            air++;

            if (air > maxAir)
            {
                air = maxAir;
            }

            return desc;
        }

        public void reduceAir()
        {
            air--;

            if (air < 0)
            {
                air = 0;
            }
        }

        public string describeAirLeft()
        {
            string desc = "\n";
            switch (air)
            {
                case 0:
                    desc += "You can't hold it anymore. Water fills your lungs and everything goes dark.";
                    break;

                case 1:
                    desc += "Your lungs burn! You need air now!";
                    break;

                //case 2:
                //    desc += "Your lungs are starting to burn. You need to come up for air soon.";
                //    break;

                case 2:
                    desc += "You continue to hold your breath.";
                    break;

                default:
                    desc += "You take a deep breath as you are plunged into the icy water.";
                    break;
            }

            return desc;
        }

        public void addVulnerability(string vulnerabilityToAdd, int value)
        {
            bool alreadyHasVulnerability = vulnerabilities.ContainsKey(vulnerabilityToAdd);
            if (alreadyHasVulnerability == true)
            {
                vulnerabilities[vulnerabilityToAdd] = value;
                return;
            }

            vulnerabilities.Add(vulnerabilityToAdd, value);
        }

        public void removeVulnerability(string vulnerabilityToRemove)
        {
            vulnerabilities.Remove(vulnerabilityToRemove);
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

        public void takeDamage(int damage, string damageType)
        {
            damage = addTypeBonusesToDamage(damage, new List<string>(new string[] { damageType }));

            if (damage <= 0)
            {
                return;
            }

            health -= damage;

            if (health < 0)
            {
                health = 0;
            }
        }

        private int addTypeBonusesToDamage(int damage, List<string> damageTypes)
        {
            foreach (string vulnerability in vulnerabilities.Keys)
            {
                foreach (string damageType in damageTypes)
                {
                    bool weakToType = vulnerability.Equals(damageType);
                    if (weakToType == true)
                    {
                        int damageBonus = vulnerabilities[vulnerability];
                        damage += damageBonus;
                    }
                }
            }
            return damage;
        }
    }
}