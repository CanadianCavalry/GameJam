using System;
using System.Collections.Generic;

namespace GameJam
{
    class Area
    {
        private string name;
        private string description;
        private bool visited;
        public List<GameObject> features;
        public List<Container> containers;
        public List<Item> itemsContained;
        public List<Item> groundItems;
        public List<NPC> npcs;
        public List<Enemy> enemies;
        public List<Link> links;

        public Area()
        {
            name = "Default Name";
            description = "Default Description";
            visited = false;
        }

        public Area(string inName, string inDescription)
        {
            name = inName;
            description = inDescription;
            visited = false;
            features = new List<GameObject>();
            containers = new List<Container>();
            itemsContained = new List<Item>();
            groundItems = new List<Item>();
            npcs = new List<NPC>();
            enemies = new List<Enemy>();
            links = new List<Link>();
        }

        public bool isVisited()
        {
            return visited;
        }

        public void markVisited()
        {
            visited = true;
        }

        public string lookAt()
	{
		string desc = name + "\n" + description;
		if (itemsContained.Count > 0)
		{
			foreach (GameObject item in groundItems)
			{
				if (item.firstSeen)
				{
					desc += " " + item.initSeenDesc;
					item.firstSeen = false;
				}
				else
				{
					desc += " " + item.seenDesc;
				}
			}
		}
		
		if (groundItems.Count > 0)
		{
			foreach (GameObject item in groundItems)
			{
				desc += " " + item.seenDesc;
			}
		}
		
		if (npcs.Count > 0)
		{
			for (NPC npc in npcs)
			{
				if (npc.firstSeen)
				{
					desc += " " + npc.initSeenDesc;
					npc.firstSeen = false;
				}
				else
				{
					desc += " " + npc.seenDesc;
				}
			}
		}
		
		return desc;
	}

        public void addItem(GameObject itemToAdd)
        {
            itemsContained.Add(itemToAdd);
        }

        public void addItemToGround(GameObject itemToAdd)
        {
            groundItems.Add(itemToAdd);
        }

        public void removeItem(GameObject itemToRemove)
	{
		if (itemsContained.Contains(itemToRemove))
		{	
			itemsContained.Remove(itemToRemove);
		}
		else if (groundItems.Contains(itemToRemove)
		{
			groundItems.Remove(itemToRemove);
		}
		else
		{
			
		}
	}

        public void connect(Link link, Area destination)
        {
            link.setDestination(destination);
            links.Add(link);
        }

        public void addFeature(GameObject feature)
        {
            features.Add(feature);
        }

        public void removeFeature(GameObject feature)
        {
            features.Remove(feature);
        }

        public void addContainer(Container container)
        {
            containers.Add(container);
        }

        public void removeContainer(Container container)
        {
            containers.Remove(container);
        }

        public void addNPC(NPC npcToAdd)
        {
            npcs.Add(npcToAdd);
            npcToAdd.currentLocation = this;
        }

        public void removeNPC(NPC npcToRemove)
        {
            npcs.Remove(npcToRemove);
            npcToRemove.currentLocation = null;
        }
    }
}