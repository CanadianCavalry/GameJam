using System;
using System.Collections.Generic;

namespace GameJam
{
    class Area
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
            return description;
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
            else
            {
                groundItems.Remove(itemToRemove);
            }
        }

        public void connect(Link link, Area destination)
        {
            link.setDestination(destination);
            links.Add(link);
        }

        public void addFeature(Feature feature)
        {
            features.Add(feature);
        }
    }
}