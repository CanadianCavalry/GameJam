using System.Collections.Generic;

namespace GameJam
{
    class GameObject
    {
        public string description;
        public int idNum;
        public List<string> keywords;
        public bool isOpen { get; protected set; }
        public List<Item> itemsContained { get; protected set; }

        public GameObject(string inDescription, List<string> inKeywords)
        {
            description = inDescription;
            keywords = inKeywords;
            isOpen = false;
            itemsContained = new List<Item>();
        }

        public void setIdNum(int inIdNum)
        {
            idNum = inIdNum;
        }

        public void setKeywords(List<string> inKeywords)
        {
            keywords = inKeywords;
        }

        public virtual string lookAt()
        {
            return description;
        }

        public virtual string pickUp()
        {
            return "You can't pick that up.";
        }

        public virtual string drop()
        {
            return "You're not holding that.";
        }

        public virtual string use()
        {
            return "It has no immediately obvious use.";
        }

        public virtual string useOn()
        {
            return "You don't know how to use it with that.";
        }

        public virtual string openObject()
        {
            return "That isn't something you can open.";
        }

        public virtual string closeObject()
        {
            return "That isn't something you can close.";
        }

        public virtual string equip()
        {
            return "That's not something you can equip.";
        }

        public virtual string attack()
        {
            return "You're not attacking that...";
        }

        public virtual string reload()
        {
            return "That isn't a weapon.";
        }

        public virtual string eat()
        {
            return "you can't eat that.";
        }

        public virtual string drink()
        {
            return "you can't drink that.";
        }

        public virtual string read()
        {
            return "There's nothing to read.";
        }
    }

    class Link : GameObject
    {
        private bool isAccessible;
        private string blockedDesc;
        private string travelDesc;
        private Area destination;
        private Link sibling;

        public Link()
            : base("", null)
        {
            isAccessible = true;
            travelDesc = "You open the door and step through.";
            blockedDesc = "You can't go that way.";
            description = "Default Description";
        }

        public Link(string inDescription, List<string> inKeywords, string inTravelDesc = "You open the door and step through.", string inBlockedDesc = "You can't go that way.")
            : base(inDescription, inKeywords)
        {
            isAccessible = true;
            travelDesc = inTravelDesc;
            blockedDesc = inBlockedDesc;
        }

        public string travel(Player player)
        {
            if (!isAccessible)
            {
                return blockedDesc;
            }

            string desc = travelDesc + "\n\n";
            player.currentLocation = destination;

            if (!player.currentLocation.isVisited())
            {
                player.currentLocation.markVisited();
                desc += player.currentLocation.lookAt();
            }
            return desc;
        }

        public void makeSibling(Link siblingLink)
        {
            sibling = siblingLink;
            siblingLink.sibling = this;
        }

        public void setDestination(Area area)
        {
            destination = area;
        }
    }

    public class Container : GameObject
    {
        private bool accessible;
        private string blockedDesc;
        private string openDesc;
        private string closeDesc;

        public Container(string inDescription, List<string> inKeywords, string inOpenDesc, string inCloseDesc, string inBlockedDesc = "")
            : base(inDescription, inKeywords)
        {
            itemsContained = new List<Item>();
            accessible = true;
            openDesc = inOpenDesc;
            closeDesc = inCloseDesc;
            blockedDesc = inBlockedDesc;
            description = inDescription;
            keywords = inKeywords;
        }

        public void addItem(Item itemToAdd)
        {
            itemsContained.Add(itemToAdd);
        }

        public void removeItem(Item itemToRemove)
        {
            itemsContained.Remove(itemToRemove);
        }

        public string lookAt()
        {
            string desc = description;
            if (isOpen == true)
            {
                desc += " It's open.";
                if (itemsContained.Count != 0)
                {
                    desc += " Inside you see:";
                    foreach (Item item in itemsContained)
                    {
                        desc += "\n" + item.name;
                    }
                }
                return desc;
            }

            desc += " It's closed.";
            return desc;
        }

        public string open()
        {
            if (!accessible)
            {
                return blockedDesc;
            }
            if (isOpen)
            {
                return "It's already open.";
            }

            isOpen = true;
            string desc = openDesc;
            if (itemsContained.Count != 0)
            {
                desc += " Inside you see:";
                foreach (Item item in itemsContained)
                {
                    desc += "\n" + item.name;
                }
                return desc;
            }

            desc += " There's nothing inside.";
            return desc;
        }

        public string close()
        {
            if (!isOpen)
            {
                return "It's already closed.";
            }

            isOpen = false;
            return closeDesc;
        }

        public void makeInaccessible(string inBlockedDesc)
        {
            accessible = false;
            blockedDesc = inBlockedDesc;
        }

        public void makeAccessible()
        {
            accessible = true;
        }
    }

    public class Item : GameObject
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

        public Item()
            : base("", null)
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

        public Item(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inPickupDesc = "Got it.", string inDropDesc = "Dropped.", string inInitSeenDesc, string inInitPickupDesc)
            : base(inDescription, inKeywords)
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
                player.addToInventory(this);
                player.currentLocation.removeItem(this);
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
            player.removeFromInventory(this);
            player.currentLocation.groundItems.Add(this);
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