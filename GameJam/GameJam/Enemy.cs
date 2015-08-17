using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Enemy : GameObject
    {
        private string name;
        public string seenDesc;
        public string initSeenDesc;
        public string talkResponse;
        public bool firstSeen;
        public Area currentLocation;
        public string behaviour;
        public int wounds;

        public Enemy(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inBehaviour = Behaviour.passive) : base(inDescription, inKeywords) {
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inSeenDesc;
            currentLocation = null;
            talkResponse = string.Empty;
            behaviour = inBehaviour;
            wounds = 0;
        }

        public Enemy(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inInitSeenDesc, string inBehaviour = Behaviour.passive)
            : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inInitSeenDesc;
            currentLocation = null;
            talkResponse = string.Empty;
            behaviour = inBehaviour;
            wounds = 0;
        }

        public override GameObject getClone()
        {
            Enemy clone = new Enemy(description, keywords, name, seenDesc, initSeenDesc, behaviour);
            return clone;
        }

        public override string lookAt()
        {
            return description;
        }

        public override string attack()
        {
            string response = "";

            return response;
        }

        public override string attack(Player player)
        {
            string response = "";

            return response;
        }

        public string threaten()
        {
            string response = "";

            return response;
        }

        public string threaten(Player player)
        {
            string response = "";

            return response;
        }

        public override string talk()
        {
            return talkResponse;
        }

        public void setTalkResponse(string inTalkResponse)
        {
            talkResponse = inTalkResponse;
        }
    }

    public static class Behaviour
    {
        public static const string aggravated = "Aggravated";
        public static const string enraged = "Enraged";
        public static const string passive = "Passive";
        public static const string pained = "Pained";
        public static const string incapacitated = "Incapacitated";
        public static const string frightened = "Frightened";
        public static const string wounded = "Wounded";
    }
}
