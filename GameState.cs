using System;
using Player;
using GameObject;

public class GameState
{
	public Player player;

    public GameState(inPlayer)
    {
		player = inPlayer;
    }
	
	public List<GameObject> getLocalObject(string keyword)
	{
		List<GameObject> objectList = new List<GameObject>();
		
		foreach (GameObject gameObject in player.inventory)
		{
			if (gameObject.Contains(keyword))
			{
				resultList.Add(gameObject);
			}
		}
		
		foreach (GameObject gameObject in player.currentLocation.itemsContained)
		{
			if (gameObject.Contains(keyword))
			{
				resultList.Add(gameObject);
			}
		}
		
		foreach (GameObject gameObject in player.currentLocation.links)
		{
			if (gameObject.Contains(keyword))
			{
				resultList.Add(gameObject);
			}
		}
		
		foreach (GameObject gameObject in player.currentLocation.npcs)
		{
			if (gameObject.Contains(keyword))
			{
				resultList.Add(gameObject);
			}
		}
		
		foreach (GameObject gameObject in player.currentLocation.groundItems)
		{
			if (gameObject.Contains(keyword))
			{
				resultList.Add(gameObject);
			}
		}
				
		foreach (GameObject gameObject in player.currentLocation.features)
		{
			if (gameObject.Contains(keyword))
			{
				resultList.Add(gameObject);
			}
		}
		
		return objectList;
	}

	public string executeCommend(string command, IDictionary<string, GameObject> args)
	{
		if (!args)
		{
			switch(command)
			{
				case "look" :
					return player.currentLocation.lookAt();
				case "wait" :
					return "You wait."
				case "defend" :
					return player.defend();
			}
		}
		else if (args["target"])
		{
			gameObject target = args["target"];
			
			switch(command)
			{
				case "look" :
				case "examine" :
					return target.lookAt();
				case "get" :
				case "take" :
					return target.get();
				case "go" :
				case "walk" :
					return target.travel(player);
				case "drop" :
					return target.drop();
				case "use" :
					return target.use();
				case "talk" :
					return target.talk();
				case "open" :
					return target.open();
				case "close" :
					return target.close();
				case "equip" :
					return target.equip();
				case "attack" :
					return target.attack();
				case "eat" :
					return target.eat();
				case "drink" :
					return target.drink();
				case "read" :
					return target.read();
			}
		}
	}
}