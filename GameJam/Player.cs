using System;
using GameObject;
using Area;

public class Player
{
	public Area currentLocation;
    private List<Item> inventory;
    private Item mainHand;
    private Item offHand;
    private Item armor;
    private int armorRating;
	private bool alive;

    public Player()
    {
        inventory = List();
		isAlive = true;
    }

    public void addItem(Item itemToAdd)
    {
        inventory.Add(itemToAdd);
    }

    public void removeItem(Item itemToRemove)
    {
        inventory.Remove(itemToRemove);
    }
	
	public string defend()
	{
		return "You take a defensive stance.";
	}
	
	public bool isAlive()
	{
		return alive;
	}
}