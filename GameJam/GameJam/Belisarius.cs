using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Belisarius : WorldBuilder
    {
        public override string getIntro()
        {
            return introduction;
        }

        public override List<Area> buildWorld(Player player)
        {
            List<Area> world = new List<Area>();

            int defaultMaxWaterLevel = 10;
            string defaultFloodedDesc = "";

            //Features
            GameObject sonar = new GameObject("A Sonar console pings quietly.", new List<string>(new string[] { "sonar", "sonar console" }));
            GameObject gertrude = new GameObject("The Gertrude underwater telephone sits plugged into one of the consoles.", new List<string>(new string[] { "gertrude", "uqc", "phone", "telephone", "underwater phone", "underwater telephone", "gertrude phone" }));
            GameObject viewingWindow = new GameObject("A large, reinforced glass window provides a wide view of what lies beyond the station.", new List<string>(new string[] { "window" }));


            //Items
            Item drySuit = new Item("A waterproof, full body suit meant to be filled slightly with air.", new List<string>(new string[] { "suit", "dry suit", "dive suit", "dry dive suit" }), "Dry suit", "A dry suit hangs from a rack.");
            Item hazmatSuit = new Item("A heavy duty full body suit to protect against fire and other hazardous materials.", new List<string>(new string[] { "suit", "hazmat suit", "hazardous materials suit" }), "HazMat suit", "A hazardous materials suit lies folded up.");
            Item diveGear = new Item("A dive helmet, fins and buoyancy control device used in deep sea diving.", new List<string>(new string[] { "gear", "dive gear", "helmet", "fins", "bcd" }), "Dive gear", "A BCD and dive helmet lay together. Searching around a bit you also find a pair of fins.");
            Item scubaTank = new Item("A tank filled with compressed air for deep sea diving.", new List<string>(new string[] { "tank", "dive tank", "scuba tank" }), "Scuba tank", "There is a Scuba tank sitting upright.");
            Item speargun = new Item("A spring loaded weapon capable of firing a barbed spear.", new List<string>(new string[] { "speargun", "gun" }), "Speargun", "You notice a speargun.");
            Item wrench = new Item("A heavy adjustable wrench used to repair pipes.", new List<string>(new string[] { "wrench", "tool" }), "Wrench", "You find a plumbing wrench.");
            Item screwdriver = new Item("A handsized tool used to tighten and loosen bolts and screws.", new List<string>(new string[] { "screwdriver", "tool" }), "Screwdriver", "You find a screwdriver.");
            Item hammer = new Item("It's a hammer. A handsized tool used for hammering. You knew that...", new List<string>(new string[] { "hammer", "tool" }), "Hammer", "You spot a hammer.");
            Item flashlight = new Item("A portable, handsized electic light.", new List<string>(new string[] { "light", "flashlight" }), "Flashlight", "You see a flashlight. Hopefully the batteries are charged.");
            Item glowstick = new Item("A small plastic tube that provides colourful illumination once cracked.", new List<string>(new string[] { "glowstick" }), "Glowstick", "You notice a glowstick. Electronic music starts going through your head for some reason.");
            Item divingKnife = new Item("A large knife used by divers.", new List<string>(new string[] { "knife", "dive knife" }), "Dive knife", "You spot a dive knife in its sheath.");
            Item extinguisher = new Item("A medium sized fire extinguisher that dispenses foam fire retardent.", new List<string>(new string[] { "extinguisher", "fire extinguisher" }), "Fire extinguisher", "You notice a fire extinguisher attached to the wall.");
            Item weldingTorch = new Item("A hyperbaric welding torch used for underwater welding.", new List<string>(new string[] { "torch", "welder", "welding torch", "underwater torch" }), "Welding torch", "You come across a welding torch and its connected fuel tank.");
            Item weldingMask = new Item("A full face mask with a small window of tinted glass to look through.", new List<string>(new string[] { "mask", "welding mask", "welder's mask" }), "Welding mask", "You spot a welder's mask.");
            Item steelRod = new Item("An arm length piece of steel rebar.", new List<string>(new string[] { "rod", "steel rod", "metal rod", "rebar" }), "Steel rod", "You come across a broken piece of steel rebar that could be used as a tool or a weapon.");
            Item steelPipe = new Item("A short piece of steel pipe.", new List<string>(new string[] { "pipe", "steel pipe", "metal pipe" }), "Steel pipe", "You see a short section of metal pipe that could be used as a club.");
            Item medkit = new Item("A case filled with medical supplies such as bandages, gauss and some pharmaceuticals.", new List<string>(new string[] { "kit", "medkit", "med kit" }), "Medkit", "A medical kit sits closed.");
            Item defibrillator = new Item("A small electronic device used to restart a person's heart.", new List<string>(new string[] { "aed", "defibrillator" }), "Defibrillator", "An automatic external defibrillator lays packaged up neatly.");
            Item adrenaline = new Item("A syringe filled with the powerful stimulant epinephrine.", new List<string>(new string[] { "adrenaline", "syringe" }), "Adrenaline shot", "A syringe of adrenaline sits in its packaging.");

            introduction = "Standing in the main control room looking out the window you notice a large shape moving in the murk.\nSuddenly the dark shape darts towards the station. You are knocked off your feet and hear a loud crash and feel reverberations through the floor beneath you. As you get to your feet you notice the window is badly cracked and water has begun to pour into the room.";

            //Areas
            Area control = new Area("Control room", 
                "The main control stations sit atop a raised platform. A large, reinforced glass window provides a wide view of the front of the station.\nTo the north a hallway leads to Hub A.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(control);
            control.addFeature(sonar.getClone());
            control.addFeature(viewingWindow.getClone());
            control.addFeature(gertrude.getClone());
            control.addItemToGround((Item)extinguisher.getClone());
            setStartArea(player, control);

            Area hubA = new Area("Hub A",
                "One of the main connector hubs.\nTo the north you can see the Moon pool. To the east lies Hub B. South is the main control room and to the west is the observation deck.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(hubA);

            Area moonPool = new Area("Moon pool",
                "In the center of the room is a large pool that leads outside the station. Surrounding the pool are four reinforced pillars. The chains of the overhead sub crane jingle softly.\nEast is Hub C. To the south is Hub A. The bio labs are to the west.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(moonPool);

            Area observationDeck = new Area("Observation deck",
                "Ladders lead down from the elevated walkway to the main observation deck. Multiple reinforced viewports line the west wall. A large, reinforced glass window covers the south wall of the deck.\nA twisting corridor leads north towards the bio labs. Hub A is to the east.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(observationDeck);

            Area bioLabA = new Area("Bio lab A",
                "\nThe Moon pool is east of you. South is a winding connector that takes you to the Observation deck. To the west is the specimen study area and the rest of the lab. ",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(bioLabA);

            Area bioLabB = new Area("Bio lab B",
                "Counters run along the north and west walls.\nThe lab entrance is to the east. South you can see the Dive prep area.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(bioLabB);

            Area bioLabC = new Area("Bio lab C",
                "\nLooking north you can see into the specimen lab. The airlock is to the south.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(bioLabC);

            Area hubB = new Area("Hub B",
                "A pair of ladders hang down on either side of a small raised platform just above head height.\nNorth are the ladders leading up to the connector bridging Hub B and Hub C. To the east is the galley. To the South is the adjoining cargo bay and to the west is Hub A.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(hubB);

            Area cargoBay = new Area("Cargo bay",
                "\nTo the north is Hub B. The airlock used for resupply is built into the south wall.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(cargoBay);

            Area galley = new Area("Galley",
                "A small fold out table extends from the east wall. A few chairs sit surrounding the table. In the southeast corner is a small sink. In the northeast corner is a simple stove.\nThe living quarters are to the north, washrooms to the south. Hub B is to the west.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(galley);

            Area washroom = new Area("Washroom",
                "Shower stalls line the south wall. Two toilet stalls are squeezed into the northwest corner. A pair of sinks line the east wall with mirrors bolted to the wall above them.\nGoing north will take you back to the galley.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(washroom);

            Area livingQuarters = new Area("Living quarters",
                "The room is separated into two sections with a thick curtain dividing them. The entrance area has two long cushioned seats running the length of the east and west walls. Beyond the curtain are stacks of sleeping bunks used by the crew.\nThe galley is to the south.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(livingQuarters);

            Area connector = new Area("B-C Hub connector",
                "The connector gradually slopes up and then back down.\nNorth is Hub C. South is Hub B.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(connector);

            Area hubC = new Area("Hub C",
                "The ceiling in this room is higher than any of the others. Shelves are built into the eastern wall. A pair of ladders lead down to the western lower level. Additional shelving can be found on the lower level behind the ladders.\nThe Hub B-C connector is south. The Moon pool is to the west.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(hubC);

            //Links
            string hubADescription = "A connector hub.";
            string[] hubAKeywords = { "north", "hub", "hub a", "huba", "north hub", "north hub a", "north huba" };
            string controlDescription = "The main control room.";
            string[] controlKeywords = {"south", "control", "control room", "main control", "main control room", "south control", "south control room" };
            linkAreas(control, controlDescription, controlKeywords, hubA, hubADescription, hubAKeywords);

            hubAKeywords = new string[] { "south", "hub", "hub a", "huba", "south hub", "south hub a", "south huba" };
            string moonPoolDescription = "The main submarine and equipment room.";
            string[] moonPoolKeywords = { "north", "moon pool", "sub room", "north moon pool", "north sub room" };
            linkAreas(hubA, hubADescription, hubAKeywords, moonPool, moonPoolDescription, moonPoolKeywords);

            hubAKeywords = new string[] { "west", "hub a", "huba", "west hub", "west hub a", "west huba" };
            string hubBDescription = "A connector hub.";
            string[] hubBKeywords = { "east", "hub b", "hubb", "east hub", "east hub b", "east hubb" };
            linkAreas(hubA, hubADescription, hubAKeywords, hubB, hubBDescription, hubBKeywords);

            hubAKeywords = new string[] { "east", "hub", "hub a", "huba", "east hub", "east hub a", "east huba" };
            string observationDeckDescription = "The observation deck.";
            string[] observationDeckKeywords = { "west", "observation", "observation deck", "west observation", "west deck", "west observation deck" };
            linkAreas(hubA, hubADescription, hubAKeywords, observationDeck, observationDeckDescription, observationDeckKeywords);

            observationDeckKeywords = new string[] { "south", "observation", "observation deck", "south observation", "south deck", "south observation deck" };
            string bioLabADescription = "Entrance to the Biology labs.";
            string[] bioLabAKeywords = { "north", "lab", "bio lab", "lab a", "bio lab a", "north lab", "north bio lab", "north bio lab a" };
            linkAreas(observationDeck, observationDeckDescription, observationDeckKeywords, bioLabA, bioLabADescription, bioLabAKeywords);

            bioLabAKeywords = new string[] { "west", "lab", "bio lab", "lab a", "bio lab a", "west lab", "west bio lab", "west bio lab a" };
            moonPoolKeywords = new string[] { "east", "moon pool", "sub room", "east moon pool", "east sub room" };
            linkAreas(bioLabA, bioLabADescription, bioLabAKeywords, moonPool, moonPoolDescription, moonPoolKeywords);

            bioLabAKeywords = new string[] { "east", "lab a", "bio lab a", "east lab", "east bio lab", "east bio lab a" };
            string bioLabBDescription = "Specimen study area of the Biology labs.";
            string[] bioLabBKeywords = { "west", "lab b", "bio lab b", "west lab", "west bio lab", "west bio lab b" };
            linkAreas(bioLabA, bioLabADescription, bioLabAKeywords, bioLabB, bioLabBDescription, bioLabBKeywords);

            bioLabBKeywords = new string[] { "north", "lab b", "bio lab b", "north lab", "north bio lab", "north bio lab b" };
            string bioLabCDescription = "Dive prep area and gear storage.";
            string[] bioLabCKeywords = { "south", "lab c", "bio lab c", "south lab", "south bio lab", "south bio lab c" };
            linkAreas(bioLabB, bioLabBDescription, bioLabBKeywords, bioLabC, bioLabCDescription, bioLabCKeywords);

            hubBKeywords = new string[] { "south", "hub", "hub b", "hubb", "south hub", "south hub b", "south hubb" };
            string connectorDescription = "A long connecting corridor.";
            string[] connectorKeywords = { "north", "connector", "corridor", "hub connector", "hub corridor", "north connector", "north corridor", "north hub connector" };
            linkAreas(hubB, hubBDescription, hubBKeywords, connector, connectorDescription, connectorKeywords);

            hubBKeywords = new string[] { "west", "hub", "hub b", "hubb", "west hub", "west hub b", "west hubb" };
            string galleyDescription = "The galley.";
            string[] galleyKeywords = { "east", "galley", "east galley" };
            linkAreas(hubB, hubBDescription, hubBKeywords, galley, galleyDescription, galleyKeywords);

            hubBKeywords = new string[] { "north", "hub", "hub b", "hubb", "north hub", "north hub b", "north hubb" };
            string cargoBayDescription = "The cargo bay.";
            string[] cargoBayKeywords = { "south", "cargo bay", "south cargo bay" };
            linkAreas(hubB, hubBDescription, hubBKeywords, cargoBay, cargoBayDescription, cargoBayKeywords);

            connectorKeywords = new string[] { "south", "connector", "corridor", "hub connector", "hub corridor", "south connector", "south corridor", "south hub connector" };
            string hubCDescription = "A connector hub and storage room.";
            string[] hubCKeywords = { "north", "hub", "hub c", "hubc", "north hub", "north hub c", "north hubc" };
            linkAreas(connector, connectorDescription, connectorKeywords, hubC, hubCDescription, hubCKeywords);

            moonPoolKeywords = new string[] { "west", "moon pool", "sub room", "west moon pool", "west sub room" };
            hubCKeywords = new string[] { "east", "hub", "hub c", "hubc", "east hub", "east hub c", "east hubc" };
            linkAreas(moonPool, moonPoolDescription, moonPoolKeywords, hubC, hubCDescription, hubCKeywords);

            galleyKeywords = new string[] { "south", "galley", "south galley" };
            string livingQuartersDescription = "The living quarters.";
            string[] livingQuartersKeywords = new string[] { "north", "livingquarters", "living quarters", "north livingquarters", "north living quarters" };
            linkAreas(galley, galleyDescription, galleyKeywords, livingQuarters, livingQuartersDescription, livingQuartersKeywords);

            galleyKeywords = new string[] { "north", "galley", "north galley" };
            string washroomDescription = "The washroom.";
            string[] washroomKeywords = { "south", "washroom", "showers", "toilet", "toilets", "head", "south washroom" };
            linkAreas(galley, galleyDescription, galleyKeywords, washroom, washroomDescription, washroomKeywords);

            return world;
        }
    }
}
