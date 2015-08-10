using System;
using GameObject;
using NPC;
using Enemy;

public class Area
{
	private string name;
	private string description;
	private bool visited;
	public List<Feature> features;
	public List<Item> itemsContained;
	public List<Item> groundItems;
	public List<NPC> npcs;
	public List<Enemy> enemy;
	public List<Link> links;

    public Area(string inName, inDescription)
    {
		name = inName;
		description = inDescription;
		visited = false;
    }
	
	public bool isVisited()
	{
		return visited;
	}
	
	public void markVisited()
	{
		visited = true;
	}
	
	public void addItem(GameObject itemToAdd)
	{
		itemsContained.Add(itemToAdd);
	}
	
	public string lookAt()
	{
		return description;
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
		else
		{
			groundItems.Remove(itemToRemove);
		}
	}
}