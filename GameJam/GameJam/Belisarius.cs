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

            introduction = "Standing in the main control room looking out the window you notice a large shape moving in the murk.\nSuddenly the dark shape darts towards the station. You are knocked off your feet and hear a loud crash and feel reverberations through the floor beneath you. As you get to your feet you notice the window is badly cracked and water has begun to pour into the room.";

            Area control = new Area("Control room", 
                "The main control stations sit atop a raised platform. A large, reinforced glass window provides a wide view of the front of the station.\nTo the north a hallway leads to Hub A.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(control);
            control.addFeature(new GameObject("Sonar station", new List<string>(new string[] {})));
            control.addFeature(new GameObject("A large, reinforced glass window provides a wide view of the front of the station.", new List<string>(new string[]{})));
            setStartArea(player, control);

            Area hubA = new Area("Hub A",
                "One of the main connector hubs.\nTo the north you can see the Moon pool. To the east lies Hub B. South is the main control room and to the west is the observation deck.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(hubA);

            Area moonPool = new Area("Moon pool",
                "In the center of the room is a large pool that leads outside the station. Surrounding the pool are four reinforced pillars. The chains of the overhead sub crane jingle softly.\nTo the south is Hub A. The bio labs are to the west.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(moonPool);

            Area observationDeck = new Area("Observation deck",
                "Ladders lead down from the elevated walkway to the main observation deck. Multiple reinforced viewports line the west wall. A large, reinforced glass window covers the south wall of the deck.\nA twisting corridor leads north towards the bio labs. Hub A is to the east.",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(observationDeck);

            Area bioLabA = new Area("Bio lab A",
                "",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(bioLabA);

            Area bioLabB = new Area("Bio lab B",
                "",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(bioLabB);

            Area bioLabC = new Area("Bio lab C",
                "",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(bioLabC);

            Area hubB = new Area("Hub B",
                "",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(hubB);

            Area cargoBay = new Area("Cargo bay",
                "",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(cargoBay);

            Area galley = new Area("Galley",
                "",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(galley);

            Area washroom = new Area("Washroom",
                "",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(washroom);

            Area livingQuarters = new Area("Living quarters",
                "",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(livingQuarters);

            Area connector = new Area("B-C Hub connector",
                "",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(connector);

            Area hubC = new Area("Hub C",
                "",
                defaultMaxWaterLevel, defaultFloodedDesc);
            world.Add(hubC);


            string hubADescription = "A connector hub.";
            string[] hubAKeywords = { "north", "hub", "hub a", "huba", "north hub", "north hub a", "north huba" };
            string controlDescription = "The main control room.";
            string[] controlKeywords = {"south", "control", "control room", "main control", "main control room", "south control", "south control room" };
            linkAreas(control, controlDescription, controlKeywords, hubA, hubADescription, hubAKeywords);

            hubAKeywords = new string[] { "south", "hub", "hub a", "huba", "south hub", "south hub a", "south huba" };
            string moonPoolDescription = "The main submarine and equipment room.";
            string[] moonPoolKeywords = { "north", "moon pool", "sub room", "north moon pool", "north sub room" };
            linkAreas(hubA, hubADescription, hubAKeywords, moonPool, moonPoolDescription, moonPoolKeywords);

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

            return world;
        }
    }
}
