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
        public Behaviour behaviour;

        public Enemy(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string demeanor = Demeanor.indifferent, int inStrength = 5)
            : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inSeenDesc;
            currentLocation = null;
            talkResponse = string.Empty;
            behaviour = new Behaviour(demeanor, inStrength);
        }

        public Enemy(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inInitSeenDesc, string demeanor = Demeanor.indifferent, int inStrength = 5)
            : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inInitSeenDesc;
            currentLocation = null;
            talkResponse = string.Empty;
            behaviour = new Behaviour(demeanor, inStrength);
        }

        public Enemy(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inInitSeenDesc, Behaviour inBehaviour)
            : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inInitSeenDesc;
            currentLocation = null;
            behaviour = inBehaviour;
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
            string response = behaviour.getResponse(Stimulus.attack);
            return response;
        }

        public override string attack(Player player)
        {
            string response = "";

            return response;
        }

        public string threaten()
        {
            string response = behaviour.getResponse(Stimulus.threat);
            return response;
        }

        public override string talk()
        {
            string response = behaviour.getResponse(Stimulus.sound);
            return response;
        }
    }

    public class Behaviour
    {
        public string demeanor;
        public string currentCondition;
        private int strengthModifier = 5;
        private List<Tuple<int, string>> passiveBehaviour;
        private List<Tuple<int, string>> frightenedBehaviour;
        private List<Tuple<int, string>> aggravatedBehaviour;
        private List<Tuple<int, string>> enragedBehaviour;
        public int wounds;

        public Behaviour(string inDemeanor, int strength)
        {
            demeanor = inDemeanor;
            currentCondition = Condition.passive;
            wounds = 0;

            passiveBehaviour = new List<Tuple<int, string>>();
            aggravatedBehaviour = new List<Tuple<int, string>>();
            enragedBehaviour = new List<Tuple<int, string>>();
            frightenedBehaviour = new List<Tuple<int, string>>();

            switch (demeanor)
            {
                case Demeanor.aggressive:
                    setAggressiveBehaviour(strength);
                    break;

                case Demeanor.curious:
                    setCuriousBehaviour(strength);
                    break;

                case Demeanor.indifferent:
                    setIndifferentBehaviour(strength);
                    break;

                case Demeanor.apprehensive:
                    setApprehensiveBehaviour(strength);
                    break;

                case Demeanor.reclusive:
                    setReclusiveBehaviour(strength);
                    break;
            }
        }

        private void setIndifferentBehaviour(int strength)
        {
            addResponse(Condition.passive, 18, strength, Condition.deceased);
            addResponse(Condition.passive, 10, strength, Condition.wounded);
            addResponse(Condition.passive, 12, strength, Condition.incapacitated);
            addResponse(Condition.passive, 5, strength, Condition.pained);
            addResponse(Condition.passive, 10, strength, Condition.frightened);
            addResponse(Condition.passive, 6, strength, Condition.enraged);
            addResponse(Condition.passive, 3, strength, Condition.aggravated);

            addResponse(Condition.aggravated, 18, strength, Condition.deceased);
            addResponse(Condition.aggravated, 10, strength, Condition.wounded);
            addResponse(Condition.aggravated, 14, strength, Condition.incapacitated);
            addResponse(Condition.aggravated, 6, strength, Condition.pained);
            addResponse(Condition.aggravated, 12, strength, Condition.frightened);
            addResponse(Condition.aggravated, 4, strength, Condition.enraged);

            addResponse(Condition.enraged, 18, strength, Condition.deceased);
            addResponse(Condition.enraged, 10, strength, Condition.wounded);
            addResponse(Condition.enraged, 15, strength, Condition.incapacitated);
            addResponse(Condition.enraged, 8, strength, Condition.pained);
            addResponse(Condition.enraged, 14, strength, Condition.frightened);

            addResponse(Condition.frightened, 18, strength, Condition.deceased);
            addResponse(Condition.frightened, 10, strength, Condition.wounded);
            addResponse(Condition.frightened, 14, strength, Condition.incapacitated);
            addResponse(Condition.frightened, 5, strength, Condition.pained);
            addResponse(Condition.frightened, 8, strength, Condition.enraged);
        }

        private void setCuriousBehaviour(int strength)
        {
            addResponse(Condition.passive, 18, strength, Condition.deceased);
            addResponse(Condition.passive, 10, strength, Condition.wounded);
            addResponse(Condition.passive, 12, strength, Condition.incapacitated);
            addResponse(Condition.passive, 5, strength, Condition.pained);
            addResponse(Condition.passive, 10, strength, Condition.frightened);
            addResponse(Condition.passive, 6, strength, Condition.enraged);
            addResponse(Condition.passive, 3, strength, Condition.aggravated);

            addResponse(Condition.aggravated, 18, strength, Condition.deceased);
            addResponse(Condition.aggravated, 10, strength, Condition.wounded);
            addResponse(Condition.aggravated, 14, strength, Condition.incapacitated);
            addResponse(Condition.aggravated, 6, strength, Condition.pained);
            addResponse(Condition.aggravated, 12, strength, Condition.frightened);
            addResponse(Condition.aggravated, 4, strength, Condition.enraged);

            addResponse(Condition.enraged, 18, strength, Condition.deceased);
            addResponse(Condition.enraged, 10, strength, Condition.wounded);
            addResponse(Condition.enraged, 15, strength, Condition.incapacitated);
            addResponse(Condition.enraged, 8, strength, Condition.pained);
            addResponse(Condition.enraged, 14, strength, Condition.frightened);

            addResponse(Condition.frightened, 18, strength, Condition.deceased);
            addResponse(Condition.frightened, 10, strength, Condition.wounded);
            addResponse(Condition.frightened, 14, strength, Condition.incapacitated);
            addResponse(Condition.frightened, 5, strength, Condition.pained);
            addResponse(Condition.frightened, 8, strength, Condition.enraged);
        }

        private void setAggressiveBehaviour(int strength)
        {
            addResponse(Condition.passive, 18, strength, Condition.deceased);
            addResponse(Condition.passive, 10, strength, Condition.wounded);
            addResponse(Condition.passive, 12, strength, Condition.incapacitated);
            addResponse(Condition.passive, 5, strength, Condition.pained);
            addResponse(Condition.passive, 10, strength, Condition.frightened);
            addResponse(Condition.passive, 6, strength, Condition.enraged);
            addResponse(Condition.passive, 3, strength, Condition.aggravated);

            addResponse(Condition.aggravated, 18, strength, Condition.deceased);
            addResponse(Condition.aggravated, 10, strength, Condition.wounded);
            addResponse(Condition.aggravated, 14, strength, Condition.incapacitated);
            addResponse(Condition.aggravated, 6, strength, Condition.pained);
            addResponse(Condition.aggravated, 12, strength, Condition.frightened);
            addResponse(Condition.aggravated, 4, strength, Condition.enraged);

            addResponse(Condition.enraged, 18, strength, Condition.deceased);
            addResponse(Condition.enraged, 10, strength, Condition.wounded);
            addResponse(Condition.enraged, 15, strength, Condition.incapacitated);
            addResponse(Condition.enraged, 8, strength, Condition.pained);
            addResponse(Condition.enraged, 14, strength, Condition.frightened);

            addResponse(Condition.frightened, 18, strength, Condition.deceased);
            addResponse(Condition.frightened, 10, strength, Condition.wounded);
            addResponse(Condition.frightened, 14, strength, Condition.incapacitated);
            addResponse(Condition.frightened, 5, strength, Condition.pained);
            addResponse(Condition.frightened, 8, strength, Condition.enraged);
        }

        private void setApprehensiveBehaviour(int strength)
        {
            addResponse(Condition.passive, 18, strength, Condition.deceased);
            addResponse(Condition.passive, 10, strength, Condition.wounded);
            addResponse(Condition.passive, 12, strength, Condition.incapacitated);
            addResponse(Condition.passive, 5, strength, Condition.pained);
            addResponse(Condition.passive, 10, strength, Condition.frightened);
            addResponse(Condition.passive, 6, strength, Condition.enraged);
            addResponse(Condition.passive, 3, strength, Condition.aggravated);

            addResponse(Condition.aggravated, 18, strength, Condition.deceased);
            addResponse(Condition.aggravated, 10, strength, Condition.wounded);
            addResponse(Condition.aggravated, 14, strength, Condition.incapacitated);
            addResponse(Condition.aggravated, 6, strength, Condition.pained);
            addResponse(Condition.aggravated, 12, strength, Condition.frightened);
            addResponse(Condition.aggravated, 4, strength, Condition.enraged);

            addResponse(Condition.enraged, 18, strength, Condition.deceased);
            addResponse(Condition.enraged, 10, strength, Condition.wounded);
            addResponse(Condition.enraged, 15, strength, Condition.incapacitated);
            addResponse(Condition.enraged, 8, strength, Condition.pained);
            addResponse(Condition.enraged, 14, strength, Condition.frightened);

            addResponse(Condition.frightened, 18, strength, Condition.deceased);
            addResponse(Condition.frightened, 10, strength, Condition.wounded);
            addResponse(Condition.frightened, 14, strength, Condition.incapacitated);
            addResponse(Condition.frightened, 5, strength, Condition.pained);
            addResponse(Condition.frightened, 8, strength, Condition.enraged);
        }

        private void setReclusiveBehaviour(int strength)
        {
            addResponse(Condition.passive, 18, strength, Condition.deceased);
            addResponse(Condition.passive, 10, strength, Condition.wounded);
            addResponse(Condition.passive, 12, strength, Condition.incapacitated);
            addResponse(Condition.passive, 5, strength, Condition.pained);
            addResponse(Condition.passive, 10, strength, Condition.frightened);
            addResponse(Condition.passive, 6, strength, Condition.enraged);
            addResponse(Condition.passive, 3, strength, Condition.aggravated);

            addResponse(Condition.aggravated, 18, strength, Condition.deceased);
            addResponse(Condition.aggravated, 10, strength, Condition.wounded);
            addResponse(Condition.aggravated, 14, strength, Condition.incapacitated);
            addResponse(Condition.aggravated, 6, strength, Condition.pained);
            addResponse(Condition.aggravated, 12, strength, Condition.frightened);
            addResponse(Condition.aggravated, 4, strength, Condition.enraged);

            addResponse(Condition.enraged, 18, strength, Condition.deceased);
            addResponse(Condition.enraged, 10, strength, Condition.wounded);
            addResponse(Condition.enraged, 15, strength, Condition.incapacitated);
            addResponse(Condition.enraged, 8, strength, Condition.pained);
            addResponse(Condition.enraged, 14, strength, Condition.frightened);

            addResponse(Condition.frightened, 18, strength, Condition.deceased);
            addResponse(Condition.frightened, 10, strength, Condition.wounded);
            addResponse(Condition.frightened, 14, strength, Condition.incapacitated);
            addResponse(Condition.frightened, 5, strength, Condition.pained);
            addResponse(Condition.frightened, 8, strength, Condition.enraged);
        }

        private void addResponse(string conditionalBehaviour, int threshold, int strength, string endCondition)
        {
            int modifiedThreshold = threshold * strength / strengthModifier;
            if (modifiedThreshold < 0)
            {
                modifiedThreshold = 0;
            }

            switch (conditionalBehaviour)
            {
                case Condition.passive:
                    passiveBehaviour.Add(new Tuple<int, string>(modifiedThreshold, endCondition));
                    break;

                case Condition.aggravated:
                    aggravatedBehaviour.Add(new Tuple<int, string>(modifiedThreshold, endCondition));
                    break;

                case Condition.enraged:
                    enragedBehaviour.Add(new Tuple<int, string>(modifiedThreshold, endCondition));
                    break;

                case Condition.frightened:
                    frightenedBehaviour.Add(new Tuple<int, string>(modifiedThreshold, endCondition));
                    break;
            }
        }

        private string behave(int damage, List<Tuple<int, string>> behaviour)
        {
            string response = "You";

            damage += 2 * wounds;
            foreach (Tuple<int, string> conditionPair in behaviour)
            {
                int threshold = conditionPair.Item1;
                string condition = conditionPair.Item2;

                if (damage < threshold)
                {
                    continue;
                }

                if (condition.Equals(Condition.wounded))
                {
                    wounds++;
                    damage += 2;

                    response += " wound the creature";

                    int lethalThreshold = behaviour[0].Item1;
                    if (damage > lethalThreshold)
                    {
                        currentCondition = Condition.deceased;

                        response += ", killing it.";

                        return response;
                    }

                    response += ", and";

                    continue;
                }

                if (condition.Equals(Condition.pained))
                {
                    continue;
                }

                currentCondition = condition;

                if (condition.Equals(Condition.passive))
                {
                    response += " don't seem to affect it.";
                    return response;
                }

                response += " " + currentCondition + " it.";
            }

            return response;
        }

        private string respondToAttack(int damage)
        {
            string response = string.Empty;

            switch (currentCondition)
            {
                case Condition.passive:
                    response = behave(damage, passiveBehaviour);
                    break;

                case Condition.aggravated:
                    response = behave(damage, aggravatedBehaviour);
                    break;

                case Condition.enraged:
                    response = behave(damage, enragedBehaviour);
                    break;

                case Condition.frightened:
                    response = behave(damage, frightenedBehaviour);
                    break;
            }

            return response;
        }

        public string getResponse(string stimulus)
        {
            string response = string.Empty;

            switch (stimulus)
            {
                case Stimulus.approach:
                    break;

                case Stimulus.sound:
                    break;

                case Stimulus.threat:
                    break;

                case Stimulus.attack:
                    break;
            }

            return response;
        }
    }

    public static class Demeanor
    {
        public static const string aggressive = "Aggresive";
        public static const string curious = "Curious";
        public static const string indifferent = "Indifferent";
        public static const string apprehensive = "Apprehensive";
        public static const string reclusive = "Reclusive";
    }

    public static class Stimulus
    {
        public static const string approach = "Approach";
        public static const string sound = "Sound";
        public static const string threat = "Threat";
        public static const string attack = "Attack";
    }

    public class Condition
    {
        public static const string aggravated = "Aggravate";
        public static const string enraged = "Enrage";
        public static const string passive = "Passive";
        public static const string pained = "Injure";
        public static const string incapacitated = "Incapacitat";
        public static const string frightened = "Frighten";
        public static const string wounded = "Wound";
        public static const string deceased = "Deceased";
    }
}
