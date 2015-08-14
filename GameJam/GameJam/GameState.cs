using System.Collections.Generic;

namespace GameJam
{
    public class GameState
    {
        public Player player;
        public List<Area> world;
        public List<Area> exposedRooms;

        public GameState(Player inPlayer, WorldBuilder builder)
        {
            player = inPlayer;
            world = builder.buildWorld(player);
            exposedRooms = new List<Area>();
        }

        public void exposeRoom(Area areaToAdd)
        {
            exposedRooms.Add(areaToAdd);
        }

        public List<GameObject> getLocalObject(string keyword)
        {
            List<GameObject> objectList = new List<GameObject>();

            foreach (GameObject gameObject in player.inventory)
            {
                if (gameObject.keywords.Contains(keyword))
                {
                    objectList.Add(gameObject);
                }
            }

            foreach (GameObject gameObject in player.currentLocation.itemsContained)
            {
                if (gameObject.keywords.Contains(keyword))
                {
                    objectList.Add(gameObject);
                }
            }

            foreach (GameObject gameObject in player.currentLocation.links)
            {
                if (gameObject.keywords.Contains(keyword))
                {
                    objectList.Add(gameObject);
                }
            }

            foreach (GameObject gameObject in player.currentLocation.npcs)
            {
                if (gameObject.keywords.Contains(keyword))
                {
                    objectList.Add(gameObject);
                }
            }

            foreach (GameObject gameObject in player.currentLocation.features)
            {
                if (gameObject.keywords.Contains(keyword))
                {
                    objectList.Add(gameObject);
                }
            }

            foreach (GameObject gameObject in player.currentLocation.containers)
            {
                if (gameObject.keywords.Contains(keyword))
                {
                    objectList.Add(gameObject);
                    if (gameObject.isOpen == false)
                    {
                        continue;
                    }

                    foreach (GameObject heldObject in gameObject.itemsContained)
                    {
                        if (heldObject.keywords.Contains(keyword))
                        {
                            objectList.Add(heldObject);
                        }
                    }
                }
            }

            foreach (GameObject gameObject in player.currentLocation.groundItems)
            {
                if (gameObject.keywords.Contains(keyword))
                {
                    objectList.Add(gameObject);
                }
            }

            foreach (GameObject gameObject in player.currentLocation.features)
            {
                if (gameObject.keywords.Contains(keyword))
                {
                    objectList.Add(gameObject);
                }
            }

            return objectList;
        }

        public string executeCommand(string command, Dictionary<string, GameObject> args)
        {
            if (args == null)
            {
                switch (command)
                {
                    case "l":
                    case "look":
                        return player.currentLocation.lookAt();
                    case "wait":
                        return "You wait.";
                    case "defend":
                        return player.defend();
                }
                return "Command not found.";
            }
            if (args.Count == 0)
            {
                switch (command)
                {
                    case "l":
                    case "look":
                        return player.currentLocation.lookAt();
                    case "wait":
                        return "You wait.";
                    case "defend":
                        return player.defend();
                }
                return "Command not found.";
            }
            else
            {
                GameObject target = args["target"];

                switch (command)
                {
                    case "look":
                    case "examine":
                        return target.lookAt();
                    case "get":
                    case "take":
                        return target.pickUp(player);
                    case "go":
                    case "walk":
                        return target.travel(this);
                    case "swim":
                        return target.swim(this);
                    case "drop":
                        return target.drop(player);
                    case "use":
                        return target.use(player);
                    case "talk":
                        return target.talk();
                    case "open":
                        return target.open();
                    case "close":
                        return target.close();
                    case "equip":
                        return target.equip(player);
                    case "attack":
                        return target.attack(player);
                    case "eat":
                        return target.eat(player);
                    case "drink":
                        return target.drink(player);
                    case "read":
                        return target.read();
                }
                return "Command not found!";
            }
        }

        public string turnPass()
        {
            if (true)
            {
                string desc = "";

                if (player.currentLocation.isSubmerged())
                {
                    player.reduceAir();
                }

                foreach (Area room in exposedRooms)
                {
                    if (room.increaseWaterLevel())
                    {
                        desc += "The rushing water completely fills the area.";
                    }
                }
                return desc;
            }
        }
    }
}