using System;
using Area;
using Player;

public class GameObject
{
    public string description;
    public int idNum;
    public List<string> keywords;
	
    public GameObject(string inDescription, List<string> inKeywords)
    {
        description = inDescription;
        keywords = inKeywords;
    }

    public void setIdNum(int inIdNum)
    {
        idNum = inIdNum;
    }

    public void setKeywords(List<string> inKeywords)
    {
        keywords = inKeywords;
    }

	public virtual string travel()
	{
		"That doesn't make sense.";
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
	private string swimDesc;
	private string floodDesc;
	private bool firstUse;
	private Area destination;
    private Link sibling;

    public Link()
    {
		description = "default";
		keywords = new List<string>();
        isAccessible = true;
        travelDesc = "You open the door and step through.";
        blockedDesc = "You can't go that way.";
		swimDesc = "You swim over to the door and force it open.";
		floodDesc = "As you force the door open, water begins pouring through into the next room.";
		firstUse = true;
    }

	public Link(string inDescription, List<string> inKeywords, string inTravelDesc = "You open the door and climb through.", string inBlockedDesc = "You can't go that way.", string inSwimDesc = "You swim over to the door and force it open.", string inFloodDesc = "As you force the door open, water begins pouring through into the next room.")
	{
		description = inDescription;
		keywords = inKeywords;
		isAccessible = true;
		travelDesc = inTravelDesc;
		blockedDesc = inBlockedDesc;
		swimDesc = inSwimDesc;
		floodDesc = inFloodDesc;
		isAccessible = true;
		firstUse = true;
	}
	
	public string travel(GameState state)
	{
		if (!isAccessible)
		{
			return blockedDesc;
		}
		
		string desc = travelDesc + "\n\n";
		state.player.currentLocation = destination;
		
		if (firstUse)
		{
			firstUse = false;
			desc += floodDesc;
			state.exposedRooms.Add(destination);
		}
		
		if (!state.player.currentLocation.isVisited())
		{
			state.player.currentLocation.markVisited();
			desc += state.player.currentLocation.lookAt();
		}
		return desc;
	}

	public string swim(GameState state)
	{
		return travel(state);
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
	private List<Item> itemsContained;
	private bool accessible;
	private bool open;
	private string blockedDesc;
	private string openDesc;
	private string closeDesc;
	
	public Container(string inDescription, List<string> inKeywords, string inOpenDesc, string inCloseDesc, string inBlockedDesc = "")
	{
		itemsContained = new List<Item>();
		accessible = true;
		open = false;
		openDesc = inOpenDesc;
		closeDesc = inCloseDesc;
		blockedDesc = inBlockedDesc;
		description = inDescription;
		keywords = inKeywords;
	}
	
	public addItem(itemToAdd)
	{
		itemsContained.Add(itemToAdd);
	}
	
	public removeItem(itemToRemove)
	{
		itemsContained.Remove(itemToRemove);
	}
	
	public string lookAt()
	{
		string desc = description;
		if (open)
		{
			desc += " It's open.";
			if (itemsContained.Count() != 0)
			{
				desc += " Inside you see:"
				foreach (Item item in itemsContained)
				{
					desc += "\n" + item.name;
				}
			}
		}
		else
		{
			desc += " It's closed."
		}
		return desc;
	}
	
	public string open()
	{
		if (!accessible)
		{
			return blockedDesc;
		}
		else if (open)
		{
			return "It's already open.";
		}
		else
		{
			open = true;
			string desc = openDesc;
			if (itemsContained.Count() != 0)
			{
				desc += " Inside you see:"
				foreach (Item item in itemsContained)
				{
					desc += "\n" + item.name;
				}
			}
			else
			{
				desc += " There's nothing inside.";
			}
		}
	}
	
	public string close()
	{
		if (!open)
		{
			return "It's already closed.";
		}
		else
		{
			open = false;
			return closeDesc;
		}
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
    {
        name = "default";
        seenDesc = "default";
        pickupDesc = "default";
        dropDesc = "default";
        initSeenDesc = "default";
        initPickupDesc = "default";
        inaccessibleDesc = "default";
        acessible = true;
        firstSeen = true;
        firstTaken = true;
    }

    public Item(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inPickupDesc = "Got it.", string inDropDesc = "Dropped.", string inInitSeenDesc = inSeenDesc, string inInitPickupDesc = inPickupDesc) 
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
        player.inventory.Remove(this);
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