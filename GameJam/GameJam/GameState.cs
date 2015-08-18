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
            exposedRooms = new List<Area>();

            world = builder.buildWorld(this, player);
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

        private List<GameObject> findObjectInList(string keyword, List<GameObject> objectList)
        {
            List<GameObject> returnList = new List<GameObject>();

            foreach (GameObject gameObject in objectList)
            {
                if (gameObject.keywords.Contains(keyword))
                {
                    objectList.Add(gameObject);
                }
            }
            return returnList;
        }

        public string executeCommand(string command, Dictionary<string, GameObject> args)
        {
            turnPasses = true;

            if (args == null)
            {
                return executeCommandNoArgs(command);
            }
            if (args.Count == 0)
            {
                return executeCommandNoArgs(command);
            }

            GameObject target = args["target"];
            string result;

            switch (command)
            {
                case "look":
                case "examine":
                    turnPasses = false;
                    result = target.lookAt();
                    break;

                case "get":
                case "take":
                    result = target.pickUp(this, player);
                    break;

                case "go":
                case "move":
                case "walk":
                    result = target.travel(this);
                    break;

                case "swim":
                    result = target.swim(this);
                    break;

                case "drop":
                    result = target.drop(this, player);
                    break;

                case "use":
                    result = target.use(this, player);
                    break;

                case "talk":
                    result = target.talk(this);
                    break;

                case "open":
                    result = target.open(this);
                    break;

                case "close":
                    result = target.close(this);
                    break;

                case "equip":
                    result = target.equip(this, player);
                    break;

                case "attack":
                    result = target.attackPlayer(this, player);
                    break;

                case "eat":
                    result = target.eat(this, player);
                    break;

                case "drink":
                    result = target.drink(this, player);
                    break;

                case "read":
                    result = target.read(this);
                    break;

                default:
                    turnPasses = false;
                    result = "Command not found!";
                    break;
            }

            return result;
        }

        private string executeCommandNoArgs(string command)
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

        public void commandFailed()
        {
            turnPasses = false;
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
            int waterLevel = waterVolume * 10 / maxVolume;

            switch (waterLevel)
            {
                case 10:
                    break;

                case 9:
                case 8:
                case 7:
                    desc = "The room has almost completely flooded.";
                    break;

                case 6:
                case 5:
                    desc = "The water is getting pretty high.";
                    break;

                case 4:
                case 3:
                    desc = "The water is coming up to your knees.";
                    break;

                case 2:
                case 1:
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