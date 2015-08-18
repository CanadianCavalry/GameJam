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
        public string threatDesc;
        public bool firstSeen;
        public Area currentLocation;
        private int damage;
        private string damageType;
        public Behaviour behaviour;
        private bool waterLocked;
        private Dictionary<string, int> vulnerabilities;

        public Enemy(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inThreatenDesc, int inDamage = 0, string inDamageType = Item.other, string demeanor = Demeanor.indifferent, int inStrength = 5, bool inWaterLocked = true)
            : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inSeenDesc;
            threatDesc = inThreatenDesc;
            currentLocation = null;
            talkResponse = string.Empty;
            behaviour = new Behaviour(demeanor, inStrength);
            vulnerabilities = new Dictionary<string, int>();
            waterLocked = inWaterLocked;
        }

        public Enemy(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inInitSeenDesc, string inThreatenDesc, int inDamage = 0, string inDamageType = Item.other, string demeanor = Demeanor.indifferent, int inStrength = 5, bool inWaterLocked = true)
            : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inInitSeenDesc;
            threatDesc = inThreatenDesc;
            currentLocation = null;
            talkResponse = string.Empty;
            behaviour = new Behaviour(demeanor, inStrength);
            vulnerabilities = new Dictionary<string, int>();
            waterLocked = inWaterLocked;
        }

        public Enemy(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inInitSeenDesc, string inThreatenDesc, int inDamage, string inDamageType, Behaviour inBehaviour, Dictionary<string, int> inVulnerabilities, bool inWaterLocked)
            : base(inDescription, inKeywords)
        {
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inInitSeenDesc;
            threatDesc = inThreatenDesc;
            currentLocation = null;
            damage = inDamage;
            damageType = inDamageType;
            behaviour = inBehaviour;
            vulnerabilities = inVulnerabilities;
            waterLocked = inWaterLocked;
        }

        public override GameObject getClone()
        {
            Enemy clone = new Enemy(description, keywords, name, seenDesc, initSeenDesc, threatDesc, damage, damageType, behaviour, vulnerabilities, waterLocked);
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

        public override string attack(GameState state, Player player)
        {
            Item weapon = player.getMainWeapon();
            int damage = weapon.getDamage();
            List<string> damageTypes = weapon.getDamageTypes();
            damage = addTypeBonusesToDamage(damage, damageTypes);

            string response = behaviour.getResponse(player, name, Stimulus.attack, damage);
            return response;
        }

        public void moveAround(Player player)
        {
            Area playerRoom = player.currentLocation;
            Random random = new Random();
            int randomIndex;
            Area randomRoom;
            List<Area> adjacentRooms;

            string currentMood = behaviour.currentMood;
            switch (currentMood)
            {
                case Mood.enraged:
                    foreach (Link connector in currentLocation.links)
                    {
                        Area adjacentRoom = connector.getDestination();

                        bool playerIsAdjacent = playerRoom.Equals(adjacentRoom);
                        if (playerIsAdjacent == false)
                        {
                            continue;
                        }

                        if (waterLocked == true)
                        {
                            bool roomHasWater = playerRoom.getIsFlooding();
                            if (roomHasWater == false)
                            {
                                return;
                            }
                        }

                        currentLocation.removeEnemy(this);
                        currentLocation = playerRoom;
                        playerRoom.addEnemy(this);
                    }
                    break;

                case Mood.aggravated:
                    adjacentRooms = new List<Area>();
                    foreach (Link connector in currentLocation.links)
                    {
                        Area adjacentRoom = connector.getDestination();

                        if (waterLocked == true)
                        {
                            bool roomHasWater = playerRoom.getIsFlooding();
                            if (roomHasWater == false)
                            {
                                return;
                            }
                        }

                        adjacentRooms.Add(adjacentRoom);
                    }
                    if (adjacentRooms.Count == 0)
                    {
                        return;
                    }
                    for (int i = 0; i < adjacentRooms.Count; i++)
                    {
                        adjacentRooms.Add(playerRoom);
                    }

                    randomIndex = random.Next(0, adjacentRooms.Count);
                    randomRoom = adjacentRooms[randomIndex];

                    currentLocation.removeEnemy(this);
                    currentLocation = randomRoom;
                    randomRoom.addEnemy(this);
                    break;

                case Mood.frightened:
                    List<Area> emptyRooms = new List<Area>();
                    foreach (Link connector in currentLocation.links)
                    {
                        Area adjacentRoom = connector.getDestination();

                        bool playerIsAdjacent = playerRoom.Equals(adjacentRoom);
                        if (playerIsAdjacent == true)
                        {
                            continue;
                        }

                        if (waterLocked == true)
                        {
                            bool roomHasWater = adjacentRoom.getIsFlooding();
                            if (roomHasWater == false)
                            {
                                continue;
                            }
                        }

                        emptyRooms.Add(adjacentRoom);
                    }
                    if (emptyRooms.Count == 0)
                    {
                        return;
                    }

                    randomIndex = random.Next(0, emptyRooms.Count);
                    randomRoom = emptyRooms[randomIndex];

                    currentLocation.removeEnemy(this);
                    currentLocation = randomRoom;
                    randomRoom.addEnemy(this);
                    break;

                case Mood.calm:
                    adjacentRooms = new List<Area>();
                    foreach (Link connector in currentLocation.links)
                    {
                        Area adjacentRoom = connector.getDestination();

                        if (waterLocked == true)
                        {
                            bool roomHasWater = adjacentRoom.getIsFlooding();
                            if (roomHasWater == false)
                            {
                                continue;
                            }
                        }

                        adjacentRooms.Add(adjacentRoom);
                    }
                    if (adjacentRooms.Count == 0)
                    {
                        return;
                    }
                    for (int i = 0; i < adjacentRooms.Count; i++)
                    {
                        adjacentRooms.Add(currentLocation);
                    }

                    randomIndex = random.Next(0, adjacentRooms.Count);
                    randomRoom = adjacentRooms[randomIndex];
                    bool alreadyInRoom = randomRoom.Equals(currentLocation);
                    if (alreadyInRoom == true)
                    {
                        return;
                    }

                    currentLocation.removeEnemy(this);
                    currentLocation = randomRoom;
                    randomRoom.addEnemy(this);
                    break;
            }
        }

        public override string attackPlayer(GameState state, Player player)
        {
            string response = behaviour.getResponse(player, name, Stimulus.approach, 0);

            Random random = new Random();
            int randomModifier = random.Next(GameState.damageRandomModifier) + GameState.damageRandomMin;
            int modifiedDamage = damage + randomModifier;
            if (modifiedDamage < 0)
            {
                modifiedDamage = 0;
            }

            player.takeDamage(modifiedDamage, damageType);

            return response;
        }

        public string threaten(GameState state, Player player)
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

        public override string talk(GameState state)
        {
            string response = behaviour.getResponse(null, name, Stimulus.sound, 0);
            return response;
        }
    }

    public class Behaviour
    {
        public string demeanor;
        public string currentMood;
        private int strengthModifier = 5;
        private List<Tuple<int, string>> passiveBehaviour;
        private List<Tuple<int, string>> frightenedBehaviour;
        private List<Tuple<int, string>> aggravatedBehaviour;
        private List<Tuple<int, string>> enragedBehaviour;
        public int wounds;

        public Behaviour(string inDemeanor, int strength)
        {
            demeanor = inDemeanor;
            currentMood = Mood.calm;
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
            addResponse(Mood.calm, 18, strength, Mood.deceased);
            addResponse(Mood.calm, 10, strength, Mood.wounded);
            addResponse(Mood.calm, 12, strength, Mood.incapacitated);
            addResponse(Mood.calm, 5, strength, Mood.pained);
            addResponse(Mood.calm, 10, strength, Mood.frightened);
            addResponse(Mood.calm, 6, strength, Mood.enraged);
            addResponse(Mood.calm, 0, strength, Mood.aggravated);

            addResponse(Mood.aggravated, 18, strength, Mood.deceased);
            addResponse(Mood.aggravated, 10, strength, Mood.wounded);
            addResponse(Mood.aggravated, 14, strength, Mood.incapacitated);
            addResponse(Mood.aggravated, 6, strength, Mood.pained);
            addResponse(Mood.aggravated, 12, strength, Mood.frightened);
            addResponse(Mood.aggravated, 4, strength, Mood.enraged);
            addResponse(Mood.aggravated, 0, strength, Mood.calm);

            addResponse(Mood.enraged, 18, strength, Mood.deceased);
            addResponse(Mood.enraged, 10, strength, Mood.wounded);
            addResponse(Mood.enraged, 15, strength, Mood.incapacitated);
            addResponse(Mood.enraged, 8, strength, Mood.pained);
            addResponse(Mood.enraged, 14, strength, Mood.frightened);
            addResponse(Mood.enraged, 0, strength, Mood.aggravated);

            addResponse(Mood.frightened, 18, strength, Mood.deceased);
            addResponse(Mood.frightened, 10, strength, Mood.wounded);
            addResponse(Mood.frightened, 14, strength, Mood.incapacitated);
            addResponse(Mood.frightened, 5, strength, Mood.pained);
            addResponse(Mood.frightened, 8, strength, Mood.enraged);
            addResponse(Mood.frightened, 0, strength, Mood.calm);
        }

        private void setCuriousBehaviour(int strength)
        {
            addResponse(Mood.calm, 18, strength, Mood.deceased);
            addResponse(Mood.calm, 10, strength, Mood.wounded);
            addResponse(Mood.calm, 12, strength, Mood.incapacitated);
            addResponse(Mood.calm, 5, strength, Mood.pained);
            addResponse(Mood.calm, 10, strength, Mood.frightened);
            addResponse(Mood.calm, 6, strength, Mood.enraged);
            addResponse(Mood.calm, 3, strength, Mood.aggravated);
            addResponse(Mood.calm, 0, strength, Mood.calm);

            addResponse(Mood.aggravated, 18, strength, Mood.deceased);
            addResponse(Mood.aggravated, 10, strength, Mood.wounded);
            addResponse(Mood.aggravated, 14, strength, Mood.incapacitated);
            addResponse(Mood.aggravated, 6, strength, Mood.pained);
            addResponse(Mood.aggravated, 12, strength, Mood.frightened);
            addResponse(Mood.aggravated, 4, strength, Mood.enraged);
            addResponse(Mood.aggravated, 0, strength, Mood.calm);

            addResponse(Mood.enraged, 18, strength, Mood.deceased);
            addResponse(Mood.enraged, 10, strength, Mood.wounded);
            addResponse(Mood.enraged, 15, strength, Mood.incapacitated);
            addResponse(Mood.enraged, 8, strength, Mood.pained);
            addResponse(Mood.enraged, 14, strength, Mood.frightened);
            addResponse(Mood.enraged, 0, strength, Mood.aggravated);

            addResponse(Mood.frightened, 18, strength, Mood.deceased);
            addResponse(Mood.frightened, 10, strength, Mood.wounded);
            addResponse(Mood.frightened, 14, strength, Mood.incapacitated);
            addResponse(Mood.frightened, 5, strength, Mood.pained);
            addResponse(Mood.frightened, 8, strength, Mood.enraged);
            addResponse(Mood.frightened, 0, strength, Mood.calm);
        }

        private void setAggressiveBehaviour(int strength)
        {
            addResponse(Mood.calm, 18, strength, Mood.deceased);
            addResponse(Mood.calm, 10, strength, Mood.wounded);
            addResponse(Mood.calm, 12, strength, Mood.incapacitated);
            addResponse(Mood.calm, 5, strength, Mood.pained);
            addResponse(Mood.calm, 10, strength, Mood.frightened);
            addResponse(Mood.calm, 6, strength, Mood.enraged);
            addResponse(Mood.calm, 3, strength, Mood.aggravated);

            addResponse(Mood.aggravated, 18, strength, Mood.deceased);
            addResponse(Mood.aggravated, 10, strength, Mood.wounded);
            addResponse(Mood.aggravated, 14, strength, Mood.incapacitated);
            addResponse(Mood.aggravated, 6, strength, Mood.pained);
            addResponse(Mood.aggravated, 12, strength, Mood.frightened);
            addResponse(Mood.aggravated, 4, strength, Mood.enraged);
            addResponse(Mood.aggravated, 0, strength, Mood.calm);

            addResponse(Mood.enraged, 18, strength, Mood.deceased);
            addResponse(Mood.enraged, 10, strength, Mood.wounded);
            addResponse(Mood.enraged, 15, strength, Mood.incapacitated);
            addResponse(Mood.enraged, 8, strength, Mood.pained);
            addResponse(Mood.enraged, 14, strength, Mood.frightened);
            addResponse(Mood.aggravated, 0, strength, Mood.aggravated);

            addResponse(Mood.frightened, 18, strength, Mood.deceased);
            addResponse(Mood.frightened, 10, strength, Mood.wounded);
            addResponse(Mood.frightened, 14, strength, Mood.incapacitated);
            addResponse(Mood.frightened, 5, strength, Mood.pained);
            addResponse(Mood.frightened, 8, strength, Mood.enraged);
            addResponse(Mood.frightened, 0, strength, Mood.calm);
        }

        private void setApprehensiveBehaviour(int strength)
        {
            addResponse(Mood.calm, 18, strength, Mood.deceased);
            addResponse(Mood.calm, 10, strength, Mood.wounded);
            addResponse(Mood.calm, 12, strength, Mood.incapacitated);
            addResponse(Mood.calm, 5, strength, Mood.pained);
            addResponse(Mood.calm, 10, strength, Mood.frightened);
            addResponse(Mood.calm, 6, strength, Mood.enraged);
            addResponse(Mood.calm, 3, strength, Mood.aggravated);
            addResponse(Mood.calm, 0, strength, Mood.calm);

            addResponse(Mood.aggravated, 18, strength, Mood.deceased);
            addResponse(Mood.aggravated, 10, strength, Mood.wounded);
            addResponse(Mood.aggravated, 14, strength, Mood.incapacitated);
            addResponse(Mood.aggravated, 6, strength, Mood.pained);
            addResponse(Mood.aggravated, 12, strength, Mood.frightened);
            addResponse(Mood.aggravated, 4, strength, Mood.enraged);
            addResponse(Mood.aggravated, 0, strength, Mood.calm);

            addResponse(Mood.enraged, 18, strength, Mood.deceased);
            addResponse(Mood.enraged, 10, strength, Mood.wounded);
            addResponse(Mood.enraged, 15, strength, Mood.incapacitated);
            addResponse(Mood.enraged, 8, strength, Mood.pained);
            addResponse(Mood.enraged, 14, strength, Mood.frightened);
            addResponse(Mood.enraged, 0, strength, Mood.aggravated);

            addResponse(Mood.frightened, 18, strength, Mood.deceased);
            addResponse(Mood.frightened, 10, strength, Mood.wounded);
            addResponse(Mood.frightened, 14, strength, Mood.incapacitated);
            addResponse(Mood.frightened, 5, strength, Mood.pained);
            addResponse(Mood.frightened, 8, strength, Mood.enraged);
            addResponse(Mood.frightened, 0, strength, Mood.calm);
        }

        private void setReclusiveBehaviour(int strength)
        {
            addResponse(Mood.calm, 18, strength, Mood.deceased);
            addResponse(Mood.calm, 10, strength, Mood.wounded);
            addResponse(Mood.calm, 12, strength, Mood.incapacitated);
            addResponse(Mood.calm, 5, strength, Mood.pained);
            addResponse(Mood.calm, 10, strength, Mood.frightened);
            addResponse(Mood.calm, 6, strength, Mood.enraged);
            addResponse(Mood.calm, 3, strength, Mood.aggravated);
            addResponse(Mood.calm, 0, strength, Mood.calm);

            addResponse(Mood.aggravated, 18, strength, Mood.deceased);
            addResponse(Mood.aggravated, 10, strength, Mood.wounded);
            addResponse(Mood.aggravated, 14, strength, Mood.incapacitated);
            addResponse(Mood.aggravated, 6, strength, Mood.pained);
            addResponse(Mood.aggravated, 12, strength, Mood.frightened);
            addResponse(Mood.aggravated, 4, strength, Mood.enraged);
            addResponse(Mood.aggravated, 0, strength, Mood.aggravated);

            addResponse(Mood.enraged, 18, strength, Mood.deceased);
            addResponse(Mood.enraged, 10, strength, Mood.wounded);
            addResponse(Mood.enraged, 15, strength, Mood.incapacitated);
            addResponse(Mood.enraged, 8, strength, Mood.pained);
            addResponse(Mood.enraged, 14, strength, Mood.frightened);
            addResponse(Mood.enraged, 3, strength, Mood.aggravated);
            addResponse(Mood.enraged, 0, strength, Mood.calm);

            addResponse(Mood.frightened, 18, strength, Mood.deceased);
            addResponse(Mood.frightened, 10, strength, Mood.wounded);
            addResponse(Mood.frightened, 14, strength, Mood.incapacitated);
            addResponse(Mood.frightened, 5, strength, Mood.pained);
            addResponse(Mood.frightened, 8, strength, Mood.enraged);
            addResponse(Mood.frightened, 0, strength, Mood.calm);
        }

        private void addResponse(string moodBehaviour, int threshold, int strength, string endCondition)
        {
            int modifiedThreshold = threshold * strength / strengthModifier;
            if (modifiedThreshold < 0)
            {
                modifiedThreshold = 0;
            }

            switch (moodBehaviour)
            {
                case Mood.calm:
                    passiveBehaviour.Add(new Tuple<int, string>(modifiedThreshold, endCondition));
                    break;

                case Mood.aggravated:
                    aggravatedBehaviour.Add(new Tuple<int, string>(modifiedThreshold, endCondition));
                    break;

                case Mood.enraged:
                    enragedBehaviour.Add(new Tuple<int, string>(modifiedThreshold, endCondition));
                    break;

                case Mood.frightened:
                    frightenedBehaviour.Add(new Tuple<int, string>(modifiedThreshold, endCondition));
                    break;
            }
        }

        private string behave(string name, string action, int damage, List<Tuple<int, string>> behaviour)
        {
            string response = String.Format("You {0} the {1}", action.ToLower(), name);

            damage += 2 * wounds;

            Random random = new Random();

            foreach (Tuple<int, string> conditionPair in behaviour)
            {
                int threshold = conditionPair.Item1;
                string condition = conditionPair.Item2;

                int randomModifier = random.Next(GameState.damageRandomModifier) + GameState.damageRandomMin;
                damage += randomModifier;
                if (damage < 0)
                {
                    damage = 0;
                }

                if (damage < threshold)
                {
                    continue;
                }

                if (condition.Equals(Mood.wounded))
                {
                    wounds++;
                    damage += 2;

                    response += " " + Mood.wounded.ToLower() + " it and";

                    int lethalThreshold = behaviour[0].Item1;
                    if (damage > lethalThreshold)
                    {
                        currentMood = Mood.deceased;

                        response += " " + Mood.deceased.ToLower();

                        return response;
                    }

                    continue;
                }

                if (condition.Equals(Mood.pained))
                {
                    continue;
                }

                currentMood = condition;

                if (condition.Equals(Mood.calm))
                {
                    response += " " + Mood.calm.ToLower();
                    return response;
                }

                response += " " + currentMood.ToLower() + " it.";
            }

            return response;
        }

        private string respondToStimulus(string name, string action, int damage)
        {
            string response = string.Empty;

            switch (currentMood)
            {
                case Mood.calm:
                    response = behave(name, action, damage, passiveBehaviour);
                    break;

                case Mood.aggravated:
                    response = behave(name, action, damage, aggravatedBehaviour);
                    break;

                case Mood.enraged:
                    response = behave(name, action, damage, enragedBehaviour);
                    break;

                case Mood.frightened:
                    response = behave(name, action, damage, frightenedBehaviour);
                    break;
            }

            return response;
        }

        private string attackResponse(Player player)
        {
            string response = "It ";
            switch (currentMood)
            {
                case Mood.aggravated:
                    response += "lashes out at you.";
                    break;

                case Mood.enraged:
                    response += "savagely attacks you.";
                    break;

                case Mood.frightened:
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

    public static class Mood
    {
        public const string aggravated = "Aggravate";
        public const string enraged = "Enrage";
        public const string calm = "Don't seem to affect it.";
        public const string pained = "Injure";
        public const string incapacitated = "Incapacitate";
        public const string frightened = "Frighten";
        public const string wounded = "Wounding";
        public const string deceased = "It dies.";
    }
}
