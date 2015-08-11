using System;
using Area;
using Player;

public class GameObject
{
    public string name;
    public string description;
    public int idNum;
    public List<string> keywords;

    public GameObject(string inName, string inDescription, int inIdNum, List<string> inKeywords)
    {
        name = inName;
        description = inDescription;
        idNum = inIdNum;
        keywords = inKeywords;
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
        return "It has no immediately obvious use.";
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

public class Link : GameObject
{
	private bool isAccessible;
	private string blockedDesc;
	private string travelDesc;
	private Area destination;
	
	public Link(Area inDestination, string inTravelDesc, string inBlockedDesc = "You can't go that way.")
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
		
		desc = travelDesc + "\n\n";
		player.currentLocation = destination;
		
		if (!player.currentLocation.isVisited())
		{
			player.currentLocation.markVisited();
			desc += player.currentLocation.lookAt();
		}
		return desc;
	}

    public void setDestination(Area area)
    {
        destination = area;
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

