﻿using System;
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

        public override List<Area> buildWorld(GameState state, Player player)
        {
            List<Area> world = new List<Area>();

            //Features
            GameObject sonar = new GameObject("The Sonar console pings quietly.", new List<string>(new string[] { "sonar", "sonar console" }), "The active sonar console shows a large mass slowly moving about beyond the station.");
            GameObject gertrude = new GameObject("The Gertrude underwater telephone sits plugged into one of the consoles.", new List<string>(new string[] { "gertrude", "uqc", "phone", "telephone", "underwater phone", "underwater telephone", "gertrude phone" }), "An underwater telephone for communicating with the surface.");
            GameObject viewPort = new GameObject("Multiple small bubble viewports line the west wall.", new List<string>(new string[] { "viewports", "bubble viewports", "bubble windows", "windows" }), "Looking out through one of the viewports you can't make anything out from the murky darkness.");
            GameObject viewingWindow = new GameObject(" A large, reinforced glass window covers the south wall of the deck.", new List<string>(new string[] { "large window", "window" }), "Looking out into the gloom you think you can see something moving through the water but it's too dark to tell.");
            GameObject brokenWindow = new GameObject("A large, reinforced glass window provides a wide view of what lies beyond the station. It has been badly cracked and is now leaking heavily.", new List<string>(new string[] { "broken window", "large window", "window" }), "Through the cracks and rushing water you're unable to make anything out.");
            GameObject overheadGantryCrane = new GameObject("The chains of the overhead sub crane jingle softly.", new List<string>(new string[] { "crane" }), "An overhead gantry crane for lifting the minisub in and out of the moon pool. The controls are on the nearby pillar.");
            GameObject craneControls = new GameObject("A set of controls on a console are attached to one of the pillars.", new List<string>(new string[] { "crane controls", "console", "controls" }), "A set of joysticks allow for the operation of the overhead crane.");
            GameObject miniSub = new GameObject("The minisub sits next to the pool. It appears to be damaged.", new List<string>(new string[] { "mini sub", "minisub", "sub" }), "A three man minisub used for long distance excursions away from the station. It can also be used to return to the surface in case of emergencies. However there is a small tear in the metal side.");
            GameObject bilgePump = new GameObject("There is a bilge pump located in the corner.", new List<string>(new string[] { "pump", "bilge pump", "water pump" }), "A water pump to deal with flooding in case of an emergency. However it isn't working for some reason.");
            GameObject divingAirCompressor = new GameObject("The compressor fills the southwest corner of the room.", new List<string>(new string[] { "compressor", "air compressor" }), "An air compressor for refilling diving tanks.");
            GameObject microscope = new GameObject("A microscope sits on the counter.", new List<string>(new string[] { "microscope", "scope" }), "A high powered microscope for examining biological specimens.");
            GameObject airlock = new GameObject("An airlock is built into the south wall.", new List<string>(new string[] { "airlock" }), "A door sized airlock leading outside.");
            GameObject galleyTable = new GameObject("A small fold down table extends westward from the counter.", new List<string>(new string[] { "table", "foldout table" }), "A few dishes and some of the crews' personal belongings lay scatterd across the table.");
            GameObject chairs = new GameObject("A few chairs sit surrounding the table.", new List<string>(new string[] { "chairs" }), "Simple chairs with padded seats.");
            GameObject sink = new GameObject("In the southeast corner is a small sink.", new List<string>(new string[] { "sink" }), "The sink is filled with dirty dishes.");
            GameObject stove = new GameObject("In the northeast corner is the stove.", new List<string>(new string[] { "stove" }), "A simple, electric stove with two burners.");
            GameObject couch = new GameObject("Couches are attached to the west and east walls of the common area.", new List<string>(new string[] { "couch", "seat", "cushioned seat" }), "Long padded seats with back cushions running the length of the wall.");
            GameObject bunks = new GameObject("There are several bunk beds used by the crew in the sleeping area.", new List<string>(new string[] { "bed", "beds", "bunk", "bunks", "bunkbed", "bunkbeds" }), "Narrow bunk beds fill most of the available space in the sleeping area.");
            GameObject livingQuartersTable = new GameObject("A long table bolted to the floor sits in front of one of the couches.", new List<string>(new string[] { "table" }), "A beatup deck of cards and a few old magazines litter the tabletop.");
            GameObject toilets = new GameObject("Two toilet stalls are squeezed into the southwest corner next to the sinks.", new List<string>(new string[] { "toilets", "toilet stalls" }), "The stalls are narrow and each has a stainless steel toilet filled with dark blue liquid.");
            GameObject showers = new GameObject("Shower stalls line the east wall.", new List<string>(new string[] { "showers", "shower stalls" }), "Three small shower stalls cover the entire eastern wall.");
            GameObject washroomSinks = new GameObject("A pair of sinks and mirrors line the west wall next to the washroom entrance.", new List<string>(new string[] { "sinks", "mirrors" }), "Both the sinks and mirrors are small and bolted to the wall. The sinks like everything else are stainless steel.");

            //Containers
            Container cabinet = new Container("A small cabinet is set into the wall.", new List<string>(new string[] { "cabinet" }), "You open the cabinet and look inside.", "You shut the cabinet door and hear it click shut.", "You try to open the door but it's jammed shut.");
            Container storageLocker = new Container("A metal storage locker is attached to the wall.", new List<string>(new string[] { "locker", "storage locker", "metal locker", "metal storage locker" }), "You open the locker door and look inside.", "You shut the locker door shut.", "It's locked.");
            Container cupboard = new Container("A cupboard is built under the counter.", new List<string>(new string[] { "cupboard" }), "You open the cupboard.", "You push the cupboard shut.", "You go to open the cupboard but it won't budge.");
            Container fridge = new Container("A medium sized refrigeration unit sits in the northwest corner.", new List<string>(new string[] { "fridge", "cooler", "refrigerator" }), "You lift the top lid of the fridge and look inside.", "You pull the lid down and you hear it seal shut.", "You try to lift the top lid of the fridge but it's stuck.");
            Container shelves = new Container("There are several storage shelves.", new List<string>(new string[] { "shelf" }), "You look to see what's on the shelves.", "You turn away from the shelves.");
            Container specimenJar = new Container("A small clear jar for storing biological speciments sits on the counter.", new List<string>(new string[] { "jar", "specimen jar" }), "You lift the lid off the top and reach inside.", "You take your hand out and replace the lid.", "The lid is stuck. You can't get it open.");
            Container specimenTank = new Container("In the southeast corner sits a large glass tank. It is filled with water and is used for storing live speciments.", new List<string>(new string[] { "aquarium", "water tank", "specimen tank" }), "You lift up the lid and peer inside.", "You set the lid back down.", "You can't get the lid open.");
            Container trunk = new Container("A wide boxy trunk for storing personal belongings sits at the foot of your bunk.", new List<string>(new string[] { "trunk", "box" }), "You lift up the lid of the trunk and look inside.", "You close the lid.", "The trunk is locked.");

            //Items
            Item drySuit = new Item("A waterproof, full body suit meant to be filled slightly with air.", new List<string>(new string[] { "suit", "dry suit", "dive suit", "dry dive suit" }), "Dry suit", "A dry suit hangs from a rack.", Item.other);
            Item hazmatSuit = new Item("A heavy duty full body suit to protect against fire and other hazardous materials.", new List<string>(new string[] { "suit", "hazmat suit", "hazardous materials suit" }), "HazMat suit", "A hazardous materials suit lies folded up.", Item.other);
            Item diveGear = new Item("A dive helmet, fins and buoyancy control device used in deep sea diving.", new List<string>(new string[] { "gear", "dive gear", "helmet", "fins", "bcd" }), "Dive gear", "A BCD and dive helmet lay together. Searching around a bit you also find a pair of fins.", Item.blunt);
            Item scubaTank = new Item("A tank filled with compressed air for deep sea diving.", new List<string>(new string[] { "tank", "dive tank", "scuba tank" }), "Scuba tank", "There is a Scuba tank sitting upright.", Item.blunt, 9);
            Item speargun = new Item("A spring loaded weapon capable of firing a barbed spear.", new List<string>(new string[] { "speargun", "gun" }), "Speargun", "You notice a speargun.", Item.sharp, 15);
            Item wrench = new Item("A heavy adjustable wrench used to repair pipes.", new List<string>(new string[] { "wrench", "tool" }), "Wrench", "You find a plumbing wrench.", Item.blunt, 8);
            Item screwdriver = new Item("A handsized tool used to tighten and loosen bolts and screws.", new List<string>(new string[] { "screwdriver", "tool" }), "Screwdriver", "You find a screwdriver.", Item.sharp, 3);
            Item hammer = new Item("It's a hammer. A handsized tool used for hammering. You knew that...", new List<string>(new string[] { "hammer", "tool" }), "Hammer", "You spot a hammer.", Item.blunt, 5);
            Item flashlight = new Item("A portable, handsized electic light.", new List<string>(new string[] { "light", "flashlight" }), "Flashlight", "You see a flashlight. Hopefully the batteries are charged.", Item.blunt, Item.light, 2);
            Item glowstick = new Item("A small plastic tube that provides colourful illumination once cracked.", new List<string>(new string[] { "glowstick" }), "Glowstick", "You notice a glowstick. Electronic music starts going through your head for some reason.", Item.blunt);
            Item divingKnife = new Item("A large knife used by divers.", new List<string>(new string[] { "knife", "dive knife" }), "Dive knife", "You spot a dive knife in its sheath.", Item.sharp, 5);
            Item extinguisher = new Item("A medium sized fire extinguisher that dispenses foam fire retardent.", new List<string>(new string[] { "extinguisher", "fire extinguisher" }), "Fire extinguisher", "You notice a fire extinguisher attached to the wall.", Item.blunt, 7);
            Item weldingTorch = new Item("A hyperbaric welding torch used for underwater welding.", new List<string>(new string[] { "torch", "welder", "welding torch", "underwater torch" }), "Welding torch", "You come across a welding torch and its connected fuel tank.", Item.heat, Item.light, 10);
            Item weldingMask = new Item("A full face mask with a small window of tinted glass to look through.", new List<string>(new string[] { "mask", "welding mask", "welder's mask" }), "Welding mask", "You spot a welder's mask.", Item.other);
            Item steelRod = new Item("An arm length piece of steel rebar.", new List<string>(new string[] { "rod", "steel rod", "metal rod", "rebar" }), "Steel rod", "You come across a broken piece of steel rebar that could be used as a tool or a weapon.", Item.blunt, Item.sharp, 6);
            Item steelPipe = new Item("A short piece of steel pipe.", new List<string>(new string[] { "pipe", "steel pipe", "metal pipe" }), "Steel pipe", "You see a short section of metal pipe that could be used as a club.", Item.blunt, 7);
            Item medkit = new Item("A case filled with medical supplies such as bandages, gauss and some pharmaceuticals.", new List<string>(new string[] { "kit", "medkit", "med kit" }), "Medkit", "A medical kit sits closed.", Item.other);
            Item defibrillator = new Item("A small electronic device used to restart a person's heart.", new List<string>(new string[] { "aed", "defibrillator" }), "Defibrillator", "An automatic external defibrillator lays packaged up neatly.", Item.shock, 2);
            Item adrenaline = new Item("A syringe filled with the powerful stimulant epinephrine.", new List<string>(new string[] { "adrenaline", "syringe" }), "Adrenaline shot", "A syringe of adrenaline sits in its packaging.", Item.sharp, 1);

            //Enemies
            Enemy giantIsopod = new Enemy("A nearly half meter long crustacean similar in appearance to a pill bug.", new List<string>(new string[] { "isopod", "giant isopod" }), "Giant Isopod", "You see a giant isopod scuttling about.", "The isopod waves it's antennae and front legs at you.", 2, Item.sharp, Demeanor.indifferent, inStrength: 3, inWaterLocked: false);
            Enemy bobbitWorm = new Enemy("A three meter long predatory worm. A set of curved claws extend from its head, capable of cutting clean through its prey.", new List<string>(new string[] { "bobbit worm", "polychaete worm", "worm" }), "Polychaete Bobbit worm", "You are shocked to see a carniverous Bobbit worm crawling around on the floor.", "The worm darts at you menacingly, brandishing it's claws.", 8, Item.sharp, Demeanor.aggressive, inStrength: 4, inWaterLocked:false);
            Enemy dunkleosteus = new Enemy("A ten meter long armoured bony fish sports a cutting jaw of solid bone.", new List<string>(new string[] { "dunkleosteus", "bony fish", "giant armoured fish", "giant fish", "armoured fish", "fish" }), "Dunkleosteus", "You are terrified to see a giant armoured fish swimming about in the room.", "The Dunkleosteus snaps its massive armoured jaws at you.", 12, Item.sharp, Demeanor.aggressive, inStrength: 10);
            Enemy lionsManeJellyfish = new Enemy("A jellyfish with a bell over two meters wide and thin, hairlike tentacles over thirty meters long.", new List<string>(new string[] { "lion's mane", "jellyfish", "lion's mane jellyfish" }), "Lion's Mane jellyfish", "A giant jellyfish slides about slowly.", "The jellyfish's giant bell pulsates slowly.", 6, Item.shock, Demeanor.indifferent, inStrength: 1);
            Enemy gulperEel = new Enemy("Also known as the black swallower. This two meter long eel-like fish is pitch black and is capable of opening its maw wide enough to consume prey even larger than itself.", new List<string>(new string[] { "gulper", "gulper eel", "black eel", "eel", "black swallower" }), "Gulper eel", "A creepy looking black eel is swimming about.", "The eel opens its massive mouth and just stares at you.", 4, Item.sharp, Demeanor.curious, inStrength: 3);
            Enemy lampreys = new Enemy("A swarm of parasitic worm-like fish that burrow through their victims' flesh to feed on their blood.", new List<string>(new string[] { "lampreys", "parasites" }), "Lampreys", "You notice several worm-like parasites swimming about in the water.", "The swarm swims about more frantically.", 1, Item.sharp, Demeanor.curious, inStrength: 1);

            //Areas
            int defaultMaxWaterLevel = 12;
            string defaultFloodedDesc = "";

            Area control = new Area("Control room",
                "The main control stations sit atop a raised platform.", "To the north a hallway leads to Hub A.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(control);
            control.addFeature(sonar.getClone());
            control.addFeature(brokenWindow.getClone());
            control.addFeature(gertrude.getClone());
            control.addItemToGround((Item)extinguisher.getClone());

            Area hubA = new Area("Hub A",
                "One of the main connector hubs.",
                "To the north is the Moon pool. To the east lies Hub B. South is the main control room and to the west is the observation deck.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(hubA);
            hubA.addFeature(bilgePump.getClone());

            Area moonPool = new Area("Moon pool",
                "In the center of the room is a large pool that leads outside the station. Surrounding the pool are four reinforced pillars.",
                "East is Hub C. To the south is Hub A. The bio labs are to the west.",
                defaultMaxWaterLevel + 9, defaultFloodedDesc);
            world.Add(moonPool);
            moonPool.addFeature(overheadGantryCrane.getClone());
            moonPool.addFeature(bilgePump.getClone());
            moonPool.addFeature(craneControls.getClone());
            moonPool.addFeature(miniSub.getClone());
            moonPool.addContainer((Container)cabinet.getClone());
            moonPool.addEnemy((Enemy)giantIsopod.getClone());

            Area observationDeck = new Area("Observation deck",
                "Ladders lead down from the elevated walkway to the main observation deck.",
                "A twisting corridor leads north towards the bio labs. Hub A is to the east.",
                defaultMaxWaterLevel + 6, defaultFloodedDesc);
            world.Add(observationDeck);
            observationDeck.addFeature(viewingWindow.getClone());
            observationDeck.addFeature(viewPort.getClone());
            observationDeck.addFeature(bilgePump.getClone());

            Area bioLabA = new Area("Bio lab A",
                "A counter runs along the north wall.",
                "The Moon pool is east of you. South is a winding connector that takes you to the Observation deck. To the west is the specimen study area and the rest of the lab. ",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(bioLabA);
            bioLabA.addFeature(microscope.getClone());
            bioLabA.addContainer((Container)cabinet.getClone());

            Area bioLabB = new Area("Bio lab B",
                "Counters run along the north and west walls.",
                "The lab entrance is to the east. South you can see the Dive prep area.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(bioLabB);
            bioLabB.addFeature(microscope.getClone());
            bioLabB.addContainer((Container)cabinet.getClone());
            bioLabB.addContainer((Container)specimenJar.getClone());
            bioLabB.addContainer((Container)specimenTank.getClone());

            Area bioLabC = new Area("Bio lab C",
                "This room is used by the divers for stoarge.",
                "Looking north you can see into the specimen lab. The airlock is to the south.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(bioLabC);
            bioLabC.addFeature(divingAirCompressor.getClone());
            bioLabC.addFeature(bilgePump.getClone());
            bioLabC.addFeature(airlock.getClone());
            bioLabC.addContainer((Container)storageLocker.getClone());
            bioLabC.addContainer((Container)shelves.getClone());

            Area hubB = new Area("Hub B",
                "A pair of ladders hang down on either side of a small raised platform just above head height.",
                "North are the ladders leading up to the connector bridging Hub B and Hub C. To the east is the galley. To the South is the adjoining cargo bay and to the west is Hub A.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(hubB);
            hubB.addContainer((Container)shelves.getClone());
            hubB.addEnemy((Enemy)giantIsopod.getClone());

            Area cargoBay = new Area("Cargo bay",
                "The resupply sub unloads into this cargo bay.",
                "To the north is Hub B.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(cargoBay);
            cargoBay.addFeature(bilgePump.getClone());
            cargoBay.addFeature(airlock.getClone());
            cargoBay.addContainer((Container)shelves.getClone());

            Area galley = new Area("Galley",
                "A few dirty dishes sit on the counter.",
                "The living quarters are to the north, washrooms to the south. Hub B is to the west.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(galley);
            galley.addContainer((Container)cupboard.getClone());
            galley.addContainer((Container)fridge.getClone());
            galley.addFeature(galleyTable.getClone());
            galley.addFeature(chairs.getClone());
            galley.addFeature(sink.getClone());
            galley.addFeature(stove.getClone());
            galley.addItemToGround((Item)extinguisher.getClone());

            Area washroom = new Area("Washroom",
                "This isn't really the time to be going to the washroom.",
                "Going north will take you back to the galley.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(washroom);
            washroom.addFeature(showers.getClone());
            washroom.addFeature(washroomSinks.getClone());
            washroom.addFeature(toilets.getClone());

            Area livingQuarters = new Area("Living quarters",
                "The room is separated into two sections with a thick curtain dividing them. The outer area is a common area, behind the curtain is the sleeping area used by the crew.",
                "The galley is to the south.",
                defaultMaxWaterLevel + 6, defaultFloodedDesc);
            world.Add(livingQuarters);
            livingQuarters.addFeature(couch.getClone());
            livingQuarters.addFeature(livingQuartersTable.getClone());
            livingQuarters.addFeature(bilgePump.getClone());
            livingQuarters.addFeature(bunks.getClone());
            livingQuarters.addContainer((Container)trunk.getClone());

            Area connector = new Area("B-C Hub connector",
                "The connector gradually slopes up and then back down.",
                "North is Hub C. South is Hub B.",
                defaultMaxWaterLevel - 6, defaultFloodedDesc);
            world.Add(connector);

            Area hubC = new Area("Hub C",
                "The ceiling in this room is higher than any of the others. Shelves are built into the eastern wall. A pair of ladders lead down to the western lower level. Additional shelving can be found on the lower level behind the ladders.",
                "The Hub B-C connector is south. The Moon pool is to the west.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(hubC);
            hubC.addContainer((Container)shelves.getClone());

            //Links
            string hubADescription = "A connector hub.";
            string[] hubAKeywords = { "north", "hub", "hub a", "huba", "north hub", "north hub a", "north huba" };
            string controlDescription = "The main control room.";
            string[] controlKeywords = { "south", "control", "control room", "main control", "main control room", "south control", "south control room" };
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

            //Set start conditions
            setStartArea(player, livingQuarters);
            floodArea(state, control);
            //introduction = "Standing in the main control room looking out the window you notice a large shape moving in the murk.\nSuddenly the dark shape darts towards the station. You are knocked off your feet and hear a loud crash and feel reverberations through the floor beneath you. As you get to your feet you notice the window is badly cracked and water has begun to pour into the room.";
            introduction = "You are awakened violently as you are knocked out of your bed. The metal walls around you still reverberate from the impact of whatever rudely awakened you. Several klaxons begin to sound loudly. The hull has been breached!";

            return world;
        }
    }
}
