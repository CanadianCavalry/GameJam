using System;
using System.Collections.Generic;

namespace GameJam
{
    public class Area
    {
        private string name;
        private string description;
        private string floodDesc;
        private bool visited;
        private bool submerged;
        private int waterLevel;
        private int maxWaterLevel;
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

        public Area(string inName, string inDescription, int inMaxWaterLevel, string inFloodDesc)
        {
            name = inName;
            description = inDescription;
            maxWaterLevel = inMaxWaterLevel;
            floodDesc = inFloodDesc;
            waterLevel = 0;
            submerged = false;
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

        public bool isSubmerged()
        {
            return submerged;
        }

        public int getWaterLevel()
        {
            return waterLevel;
        }

        public bool increaseWaterLevel()
        {
            if (!submerged)
            {
                waterLevel++;
                if (waterLevel >= maxWaterLevel)
                {
                    submerged = true;
                    return true;
                }
            }
            return false;
        }

        public string lookAt()
        {
            //Start with the name
            string desc = name + "\n";

            //Add the areas base description
            if (isSubmerged())
            {
                desc += floodDesc;
            }
            else
            {
                desc += description;
            }

            //Add the seenDescription of any features
            if (features.Count > 0)
            {
                foreach (GameObject feature in features)
                {
                    desc += " " + feature.seenDesc;
                }
            }

            //Add the seen description of any NPCs
            if (npcs.Count > 0)
            {
                foreach (NPC npc in npcs)
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

            //Add the seen descriptions of all items in the room
            if (itemsContained.Count > 0)
            {
                foreach (Item item in itemsContained)
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
                foreach (Item item in groundItems)
                {
                    desc += " " + item.seenDesc;
                }
            }

            
            }

            return desc;
        }

        public void addItem(Item itemToAdd)
        {
            itemsContained.Add(itemToAdd);
        }

        public void addItemToGround(Item itemToAdd)
        {
            groundItems.Add(itemToAdd);
        }

        public void removeItem(Item itemToRemove)
        {
            if (itemsContained.Contains(itemToRemove))
            {
                itemsContained.Remove(itemToRemove);
            }
            else if (groundItems.Contains(itemToRemove))
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

        public void addEnemy(Enemy enemyToAdd)
        {
            enemies.Add(enemyToAdd);
            enemyToAdd.currentLocation = this;
        }

        public void removeEnemy(Enemy enemyToRemove)
        {
            enemies.Remove(enemyToRemove);
            enemyToRemove.currentLocation = null;
        }
    }
}