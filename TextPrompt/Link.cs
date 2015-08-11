using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Link : GameObject
    {
        private bool isAccessible;
        private string blockedDesc;
        private string travelDesc;
        private Area destination;
        private Link sibling;

        public Link()
            : base("", null)
        {
            isAccessible = true;
            travelDesc = "You open the door and step through.";
            blockedDesc = "You can't go that way.";
            description = "Default Description";
        }

        public Link(string inDescription, List<string> inKeywords, string inTravelDesc = "You open the door and step through.", string inBlockedDesc = "You can't go that way.")
            : base(inDescription, inKeywords)
        {
            isAccessible = true;
            travelDesc = inTravelDesc;
            blockedDesc = inBlockedDesc;
        }

        public string travel(Player player)
        {
            if (!isAccessible)
            {
                return blockedDesc;
            }

            string desc = travelDesc + "\n\n";
            player.currentLocation = destination;

            if (!player.currentLocation.isVisited())
            {
                player.currentLocation.markVisited();
                desc += player.currentLocation.lookAt();
            }
            return desc;
        }

        public void makeSibling(Link siblingLink)
        {
            sibling = siblingLink;
            siblingLink.sibling = this;
        }

        public void setDestination(Area area)
        {
            destination = area;
        }
    }
}
