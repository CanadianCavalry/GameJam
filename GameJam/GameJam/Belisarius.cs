using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Belisarius : WorldBuilder
    {
        public override List<Area> buildWorld(Player player)
        {
            List<Area> world = new List<Area>();

            Area control = new Area("Control room", 
                "The main control stations sit atop a raised platform. A large, reinforced glass window provides a wide view of the front of the station.\nTo the north a hallway leads to Hub A.");
            world.Add(control);
            control.addFeature(new GameObject("Sonar station", new List<string>(new string[] {})));
            control.addFeature(new GameObject("A large, reinforced glass window provides a wide view of the front of the station.", new List<string>(new string[]{})));

            Area hubA = new Area("Hub A",
                "One of the main connector hubs.\nTo the north you can see the Moon pool. To the east lies Hub B. South is the main control room and to the west is the observation deck.");
            world.Add(hubA);

            Area moonPool = new Area("Moon pool",
                "In the center of the room is a large pool that leads outside the station. Surrounding the pool are four reinforced pillars. The chains of the overhead sub crane jingle softly.\nTo the south is Hub A. The bio labs are to the west.");
            world.Add(moonPool);
            setStartArea(player, moonPool);

            Area observationDeck = new Area("Observation deck",
                "Ladders lead down from the elevated walkway to the main observation deck. Multiple reinforced viewports line the west wall. A large, reinforced glass window covers the south wall of the deck.\nA twisting corridor leads north towards the bio labs. Hub A is to the east.");
            world.Add(observationDeck);

            Area bioLabA = new Area("Bio lab A",
                "");
            world.Add(bioLabA);

            Area bioLabB = new Area("Bio lab B",
                "");
            world.Add(bioLabB);

            Area bioLabC = new Area("Bio lab C",
                "");
            world.Add(bioLabC);

            Area hubB = new Area("Hub B",
                "");
            world.Add(hubB);

            Area cargoBay = new Area("Cargo bay",
                "");
            world.Add(cargoBay);

            Area galley = new Area("Galley",
                "");
            world.Add(galley);

            Area washroom = new Area("Washroom",
                "");
            world.Add(washroom);

            Area livingQuarters = new Area("Living quarters",
                "");
            world.Add(livingQuarters);

            Area connector = new Area("B-C Hub connector",
                "");
            world.Add(connector);

            Area hubC = new Area("Hub C",
                "");
            world.Add(hubC);


            string hubDescription = "A connector hub.";
            string[] hubAKeywords = { "north", "hub", "hub a", "huba", "north hub", "north hub a", "north huba" };
            string controlDescription = "The main control room.";
            string[] controlKeywords = {"south", "control", "control room", "main control", "main control room", "south control", "south control room" };
            linkAreas(control, hubDescription, hubAKeywords, hubA, controlDescription, controlKeywords, true);

            hubAKeywords = new string[] { "south", "hub", "hub a", "huba", "south hub", "south hub a", "south huba" };
            string moonPoolDescription = "The main submarine and equipment room.";
            string[] moonPoolKeywords = { "north", "moon pool", "sub room", "north moon pool", "north sub room" };
            linkAreas(hubA, hubDescription, hubAKeywords, moonPool, moonPoolDescription, moonPoolKeywords, true);

            hubAKeywords = new string[] { "east", "hub", "hub a", "huba", "east hub", "east hub a", "east huba" };
            string observationDeckDescription = "The observation deck.";
            string[] observationDeckKeywords = { "west", "observation", "observation deck", "west observation", "west deck", "west observation deck" };
            linkAreas(hubA, hubDescription, hubAKeywords, observationDeck, observationDeckDescription, observationDeckKeywords, true);

            observationDeckKeywords = new string[] { "south", "observation", "observation deck", "south observation", "south deck", "south observation deck" };
            string bioLabADescription = "Entrance to the Biology labs.";
            string[] bioLabAKeywords = { "north", "lab", "bio lab", "lab a", "bio lab a", "north lab", "north bio lab", "north bio lab a" };
            linkAreas(observationDeck, observationDeckDescription, observationDeckKeywords, bioLabA, bioLabADescription, bioLabAKeywords, true);

            return world;
        }
    }
}
