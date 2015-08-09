using System;

public class GameObject
{
    public string name;
    public string description;
    public int idNum;
    public List<string> keyWords;

    public GameObject(string inName, string inDescription, int inIdNum, List<string> inKeyWords)
    {
        name = inName;
        description = inDescription;
        idNum = inIdNum;
        keyWords = inKeyWords;
    }

    public void setIdNum(int inIdNum)
    {
        idNum = inIdNum;
    }

    public virtual string lookAt()
    {
        return description;
    }

    public virtual string pickUp()
    {
        return "I can't pick that up.";
    }

    public virtual string drop()
    {
        return "I'm not holding that.";
    }

    public virtual string use()
    {
        return "It has no immediatly obvious use.";
    }

    public virtual string useOn()
    {
        return "It has no immediatly obvious use.";
    }

    public virtual string openObject()
    {
        return "That isn't something I can open.";
    }

    public virtual string closeObject()
    {
        return "That isn't something I can close.";
    }

    public virtual string equip()
    {
        return "That's not something I can equip.";
    }

    public virtual string reload()
    {
        return "That isn't a weapon.";
    }

    public virtual string eat()
    {
        return "I can't eat that.";
    }

    public virtual string drink()
    {
        return "I can't drink that.";
    }

    public virtual string read()
    {
        return "There's nothing to read.";
    }
}

public class Item : GameObject
{
    public string seenDesc;
    public string pickupDesc;
    public string dropDesc;
    public string initSeenDesc;
    public string initPickupDesc;
    public string inaccessibleDesc;
    public bool accessible;
    public bool firstSeen;
    public bool firstTaken;

    public Item(string inSeenDesc, string inPickupDesc = "Got it.", string inDropDesc = "Dropped.", string inInitSeenDesc = inSeenDesc, string inInitPickupDesc = inPickupDesc) 
    {
        string seenDesc = inSeenDesc;
        string pickupDesc = inPickupDesc;
        string dropDesc = inDropDesc;
        string initSeenDesc = inInitSeenDesc;
        string initPickupDesc = inInitPickupDesc;
        string inaccessibleDesc = null;
        bool accessible = true;
        bool firstSeen = true;
        bool firstTaken = true;
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
            return pickipDesc;
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

