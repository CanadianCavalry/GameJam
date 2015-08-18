using System.Collections.Generic;

namespace GameJam
{
    public class GameState
    {
        public Player player;
        public List<Area> world;
        public List<Area> exposedRooms;
        private string introduction;
        private bool turnPasses;

        public GameState(Player inPlayer, WorldBuilder builder)
        {
            player = inPlayer;
            world = builder.buildWorld(player);
            exposedRooms = new List<Area>();
            introduction = builder.getIntro();
            turnPasses = false;
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

            return objectList;
        }

        public string executeCommand(string command, Dictionary<string, GameObject> args)
        {
            turnPasses = true;

            if (args == null)
            {
                switch (command)
                {
                    case "l":
                    case "look":
                        turnPasses = false;
                        return player.currentLocation.lookAt();
                    case "wait":
                        return "You wait.";
                    case "defend":
                        return player.defend();
                }
                turnPasses = false;
                return "Command not found.";
            }
            if (args.Count == 0)
            {
                switch (command)
                {
                    case "l":
                    case "look":
                        turnPasses = false;
                        return player.currentLocation.lookAt();
                    case "wait":
                        return "You wait.";
                    case "defend":
                        return player.defend();
                }
                turnPasses = false;
                return "Command not found.";
            }
            else
            {
                GameObject target = args["target"];
                string result;

                switch (command)
                {
                    case "look":
                    case "examine":
                        turnPasses = false;
                        result = target.lookAt();
                        return result;
                    case "get":
                    case "take":
                        result = target.pickUp(player);
                        return result;
                    case "go":
                    case "move":
                    case "walk":
                        result = target.travel(this);
                        return result;
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
                        return target.attackPlayer(player);
                    case "eat":
                        return target.eat(player);
                    case "drink":
                        return target.drink(player);
                    case "read":
                        return target.read();
                }

                turnPasses = false;
                return "Command not found!";
            }
        }

        public string turnPass()
        {
            string desc = "";
            if (turnPasses == false)
            {
                return desc;
            }

            foreach (Area room in exposedRooms)
            {
                bool roomFlooded = room.increaseWaterLevel();
            }
            //if (!desc.Equals(string.Empty))
            //{
            //    desc += " ";
            //}
            desc += describeWaterLevel(player.currentLocation);

            desc += player.breathe();

            turnPasses = false;

            return desc;
        }

        private string describeWaterLevel(Area currentArea)
        {
            string desc = string.Empty;
            bool completelyFlooded = currentArea.isSubmerged();
            if (completelyFlooded == true)
            {
                desc = "The rushing water completely fills the area.";
                return desc;
            }

            int waterVolume = currentArea.getWaterLevel();
            int maxVolume = currentArea.getMaxWaterLevel();
            int waterLevel = maxVolume / waterVolume;

            switch (waterLevel)
            {
                case 1:
                    break;

                case 2:
                    desc = "The room has almost completely flooded.";
                    break;

                case 3:
                    desc = "The water is getting pretty high.";
                    break;

                case 4:
                    desc = "The water is coming up to your knees.";
                    break;

                case 5:
                case 6:
                case 7:
                case 8:
                    desc = "You can feel water soaking into your shoes.";
                    break;

                default:
                    break;
            }

            return desc;
        }

        public string getIntro()
        {
            return introduction;
        }
    }
}