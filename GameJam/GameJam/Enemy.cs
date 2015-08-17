using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    public class Enemy : GameObject
    {
        private string name;
        public string seenDesc;
        public string initSeenDesc;
        public string talkResponse;
        public bool firstSeen;
        public Area currentLocation;
        private int damage;
        private string damageType;
        public Behaviour behaviour;
        private Dictionary<string, int> vulnerabilities;

        public Enemy(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, int inDamage = 0, string inDamageType = Item.other, string demeanor = Demeanor.indifferent, int inStrength = 5)
            : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inSeenDesc;
            currentLocation = null;
            talkResponse = string.Empty;
            behaviour = new Behaviour(demeanor, inStrength);
            vulnerabilities = new Dictionary<string, int>();
        }

        public Enemy(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inInitSeenDesc, int inDamage = 0, string inDamageType = Item.other, string demeanor = Demeanor.indifferent, int inStrength = 5)
            : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inInitSeenDesc;
            currentLocation = null;
            talkResponse = string.Empty;
            behaviour = new Behaviour(demeanor, inStrength);
            vulnerabilities = new Dictionary<string, int>();
        }

        public Enemy(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inInitSeenDesc, int inDamage, string inDamageType, Behaviour inBehaviour, Dictionary<string, int> inVulnerabilities)
            : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inInitSeenDesc;
            currentLocation = null;
            damage = inDamage;
            damageType = inDamageType;
            behaviour = inBehaviour;
            vulnerabilities = inVulnerabilities;
        }

        public override GameObject getClone()
        {
            Enemy clone = new Enemy(description, keywords, name, seenDesc, initSeenDesc, damage, damageType, behaviour, vulnerabilities);
            return clone;
        }

        public void addVulnerability(string vulnerabilityToAdd, int value)
        {
            bool alreadyHasVulnerability = vulnerabilities.ContainsKey(vulnerabilityToAdd);
            if (alreadyHasVulnerability == true)
            {
                vulnerabilities[vulnerabilityToAdd] = value;
                return;
            }

            vulnerabilities.Add(vulnerabilityToAdd, value);
        }

        public void removeVulnerability(string vulnerabilityToRemove)
        {
            vulnerabilities.Remove(vulnerabilityToRemove);
        }

        private int addTypeBonusesToDamage(int damage, List<string> damageTypes)
        {
            foreach (string vulnerability in vulnerabilities.Keys)
            {
                foreach (string damageType in damageTypes)
                {
                    bool weakToType = vulnerability.Equals(damageType);
                    if (weakToType == true)
                    {
                        int damageBonus = vulnerabilities[vulnerability];
                        damage += damageBonus;
                    }
                }
            }
            return damage;
        }

        public override string lookAt()
        {
            return description;
        }

        public override string attack(Player player)
        {
            Item weapon = player.getMainWeapon();
            int damage = weapon.getDamage();
            List<string> damageTypes = weapon.getDamageTypes();
            damage = addTypeBonusesToDamage(damage, damageTypes);

            string response = behaviour.getResponse(player, name, Stimulus.attack, damage);
            return response;
        }

        public override string attackPlayer(Player player)
        {
            string response = behaviour.getResponse(player, name, Stimulus.approach, 0);

            // ToDo Have Enemy attack Player

            return response;
        }

        public string threaten(Player player)
        {
            Item weapon = player.getMainWeapon();
            int damage = weapon.getDamage();
            List<string> damageTypes = weapon.getDamageTypes();
            damage = addTypeBonusesToDamage(damage, damageTypes);
            damage /= 2;
            if (damage < 0)
            {
                damage = 0;
            }

            string response = behaviour.getResponse(player, name, Stimulus.threat, damage);
            return response;
        }

        public override string talk()
        {
            string response = behaviour.getResponse(null, name, Stimulus.sound, 0);
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

        private string behave(string name, string action, int damage, List<Tuple<int, string>> behaviour)
        {
            string response = String.Format("You {0} the {1}", action.ToLower(), name);

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

                    response += " " + Condition.wounded.ToLower() + " it and";

                    int lethalThreshold = behaviour[0].Item1;
                    if (damage > lethalThreshold)
                    {
                        currentCondition = Condition.deceased;

                        response += " " + Condition.deceased.ToLower();

                        return response;
                    }

                    continue;
                }

                if (condition.Equals(Condition.pained))
                {
                    continue;
                }

                currentCondition = condition;

                if (condition.Equals(Condition.passive))
                {
                    response += " " + Condition.passive.ToLower();
                    return response;
                }

                response += " " + currentCondition.ToLower() + " it.";
            }

            return response;
        }

        private string respondToStimulus(string name, string action, int damage)
        {
            string response = string.Empty;

            switch (currentCondition)
            {
                case Condition.passive:
                    response = behave(name, action, damage, passiveBehaviour);
                    break;

                case Condition.aggravated:
                    response = behave(name, action, damage, aggravatedBehaviour);
                    break;

                case Condition.enraged:
                    response = behave(name, action, damage, enragedBehaviour);
                    break;

                case Condition.frightened:
                    response = behave(name, action, damage, frightenedBehaviour);
                    break;
            }

            return response;
        }

        private string attackResponse(Player player)
        {
            string response = "It ";
            switch (currentCondition)
            {
                case Condition.aggravated:
                    response += "lashes out at you.";
                    break;

                case Condition.enraged:
                    response += "savagely attacks you.";
                    break;

                case Condition.frightened:
                    response += "keeps its distance from you.";
                    break;

                default:
                    response += "just seems to ignore you though.";
                    break;
            }

            return response;
        }

        public string getResponse(Player player, string name, string stimulus, int damage)
        {
            string response = string.Empty;

            switch (stimulus)
            {
                case Stimulus.approach:
                    response = respondToStimulus(name, Stimulus.approach, 0);
                    response += " " + attackResponse(player);
                    break;

                case Stimulus.sound:
                    response = respondToStimulus(name, Stimulus.sound, 0);
                    break;

                case Stimulus.threat:
                    response = respondToStimulus(name, Stimulus.threat, damage);
                    break;

                case Stimulus.attack:
                    response = respondToStimulus(name, Stimulus.attack, damage);
                    break;
            }

            return response;
        }
    }

    public static class Demeanor
    {
        public const string aggressive = "Aggresive";
        public const string curious = "Curious";
        public const string indifferent = "Indifferent";
        public const string apprehensive = "Apprehensive";
        public const string reclusive = "Reclusive";
    }

    public static class Stimulus
    {
        public const string approach = "Approach";
        public const string sound = "Startle";
        public const string threat = "Threaten";
        public const string attack = "Attack";
    }

    public static class Condition
    {
        public const string aggravated = "Aggravate";
        public const string enraged = "Enrage";
        public const string passive = "Don't seem to affect it.";
        public const string pained = "Injure";
        public const string incapacitated = "Incapacitate";
        public const string frightened = "Frighten";
        public const string wounded = "Wounding";
        public const string deceased = "It dies.";
    }
}
