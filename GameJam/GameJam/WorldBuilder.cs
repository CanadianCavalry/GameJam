using System.Collections.Generic;

namespace GameJam
{
    public class WorldBuilder
    {
        protected string introduction;

        public WorldBuilder()
        {
            introduction = string.Empty;
        }

        public virtual List<Area> buildWorld(Player player)
        {
            List<Area> world = new List<Area>();
            return world;
        }

        public void setStartArea(Player player, Area toStartIn)
        {
            player.currentLocation = toStartIn;
            toStartIn.isVisited();
            toStartIn.increaseWaterLevel();
        }

        public virtual string getIntro()
        {
            return string.Empty;
        }

        public void linkAreas(Area origin, string originDescription, string[] originKeywords, Area destination, string destinationDescription, string[] destinationKeywords)
        {
            linkAreas(origin, originDescription, originKeywords, destination, destinationDescription, destinationKeywords, true);
        }

        public void linkAreas(Area origin, string originDescription, string[] originKeywords, Area destination, string destinationDescription, string[] destinationKeywords, bool makeSiblings)
        {
            //To connect two areas, we need the Areas themselves, and two links.
            //First create the list of keywords that can be used to refer to an object
            List<string> destinationKeywordList = new List<string>(destinationKeywords);
            List<string> originKeywordList = new List<string>(originKeywords);

            //Then instantiate the links. Here we are using the default travel and blocked descriptions.
            Link firstLink = new Link(destinationDescription, destinationKeywordList);
            Link secondLink = new Link(originDescription, originKeywordList);

            //Now we need to connect the two rooms together. First we make the two links siblings. This is *IMPORTANT*.
            if (makeSiblings == true)
            {
                firstLink.makeSibling(secondLink);
            }

            //These two statements connects firstArea to secondArea using firstLink.
            origin.connect(firstLink, destination);
            destination.connect(secondLink, origin);

            //The reason we need to link both ways, is that links can be made to be one way, or to behave in unusual ways (in keeping with the horror theme
            // if we so desire. Making a link a sibling tells the engine that they are different ends of the same link. That way they are kept in sync
            // so if one is locked, for example, the other will be locked as well.
        }
    }
}