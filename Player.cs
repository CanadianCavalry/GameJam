using System;
using GameObject;

public class Player
{
    public List<Item> inventory;
    public Item mainHand;
    public Item offHand;
    public Item armor;
    public int armorRating;

    public Player()
    {
        inventory = List();
    }

    public void addItem(Item itemToAdd)
    {
        inventory.Add(itemToAdd);
    }

    public void removeItem(Item itemToRemove)
    {
        inventory.Remove(itemToRemove);
    }
}
