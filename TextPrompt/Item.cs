using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Item : GameObject
    {
        public string name;
        public string seenDesc;
        public string pickupDesc;
        public string dropDesc;
        public string initSeenDesc;
        public string initPickupDesc;
        public string inaccessibleDesc;
        public bool accessible;
        public bool firstSeen;
        public bool firstTaken;

        public Item() : base("", null)
        {
            name = "default";
            seenDesc = "default";
            pickupDesc = "default";
            dropDesc = "default";
            initSeenDesc = "default";
            initPickupDesc = "default";
            inaccessibleDesc = "default";
            accessible = true;
            firstSeen = true;
            firstTaken = true;
        }

        public Item(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inPickupDesc = "Got it.", string inDropDesc = "Dropped.")
            : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            pickupDesc = inPickupDesc;
            dropDesc = inDropDesc;
            initSeenDesc = inSeenDesc;
            initPickupDesc = inPickupDesc;
            inaccessibleDesc = null;
            accessible = true;
            firstSeen = true;
            firstTaken = true;
            description = inDescription;
            keywords = inKeywords;
        }

        public Item(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inPickupDesc = "Got it.", string inDropDesc = "Dropped.", string inInitSeenDesc, string inInitPickupDesc) : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            pickupDesc = inPickupDesc;
            dropDesc = inDropDesc;
            initSeenDesc = inInitSeenDesc;
            initPickupDesc = inInitPickupDesc;
            inaccessibleDesc = null;
            accessible = true;
            firstSeen = true;
            firstTaken = true;
            description = inDescription;
            keywords = inKeywords;
        }

        public virtual string pickUp(Player player)
        {
            if (accessible)
            {
                player.inventory.Add(this);
            }
            else
            {
                return inaccessibleDesc;
            }

            if (firstTaken)
            {
                firstTaken = false;
                return initPickupDesc;
            }
            else
            {
                return pickupDesc;
            }
        }

        public virtual string drop(Player player)
        {
            player.inventory.Remove(this);
            return dropDesc;
        }

        public virtual void makeAccessible()
        {
            accessible = true;
        }

        public virtual void makeInaccessible(string inInaccessibleDesc)
        {
            accessible = false;
            inaccessibleDesc = inInaccessibleDesc;
        }
    }
}
