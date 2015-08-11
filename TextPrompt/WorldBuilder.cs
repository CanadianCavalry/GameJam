using System.Collections.Generic;
using Area;
using GameObject;

public class WorldBuilder
{
    public void buildSmallWorld()
    {
        //First you need to create an area to start with
        Area areaA = new Area("Gates", "You are standing on a street corner. To the west a large oriental archway stands over the road welcoming people. Beyond the archway lies a street market. To the south the street continues. The street also continues off to the north but you have no interest going that way.");
        //Add some stuff to the room
        areaA.addFeature(new GameObject("A large oriental archway made of red painted wood and covered with terra cotta tiles. Carved marble creatures stand guard at the base of the arch's pillars. ", new List<string>(new string[] {"archway","oriental archway", "large archway", "large oriental archway"})));

        //More verbose, for clarity. Here we are using every possible custom option. Most of these are optional. Check the class definition.
        Item yourCar = new Item(inDescription = "Your car. It's seen better days, but overall it's not in terrible shape.",
                                inKeywords = new List<string>(new string[] { "car" }),
                                inName = "A Car", //This is what shows in your inventory
                                inSeenDesc = "Your car sits parked on the other side of the road.", //When you look at an area that the item is in
                                inPickupDesc = "Haha sure would be awesome if you could pick up a car.", //when you pick it up
                                inDropDesc = "You try to set the car down as gently as you can.", //when you drop it
                                inInitSeenDesc = "Your car sits parked on the other side of the road, right where you left it.", //when you look at an area that the item is in, and you have not picked it up before
                                inInitPickupDesc = "Yeah right. You can't pick up a car dummy! Who do you think you are?" //When you pick it up for the FIRST time
                                );
        areaA.addItem(yourCar);
	
        Area areaB = new Area("Market", "You find yourself in a large street market. Awnings cover the walkways where merchants offer a multitude of wares. To the east is a large archway. To the west the street continues on.");
	
	    Area areaC = new Area("Street", "You are standing along the side of the main street. To the north is Xing Fu's chinese restaurant. To the east is the street market. To the south is a small alley. The street continues off to the west.");
	
	    Area areaD = new Area("Restaurant", "");
	
	    Area areaE = new Area("Alley", "");
	
	    Area areaF = new Area("Street", "As you walk along the side of the street you notice a tea shop to the south.");
	
	    Area areaG = new Area("Tea shop", "A bell rings as you open the door. The owner greets you as you enter the tea shop. You notice a vast assortment of teapots, teacups and other related goods.");
	
	    Area areaH = new Area("Street", "You are at the end of the street. To the north sits a large Buddist temple, partly hidden behind stands of bamboo.");
	
	    Area areaI = new Area("Courtyard", "Standing before the entrance to the temple you are surrounded by coi ponds, bamboo stands and other greenery. You can hear the faint trickle of water pouring into the ponds. If you go north you will enter the temple. To the south lies the street.");
	
	    Area areaJ = new Area("Temple", "");
	
	    Area areaK = new Area("Temple", "");
	
	    Area areaL = new Area("Temple", "");
	
	    Area areaM = new Area("Temple garden", "");
	
	    Area areaN = new Area("Alley", "");
	
	    Area areaO = new Area("Secret garden", "");
	
	    Area areaP = new Area("Alley", "");
	
	    Area areaQ = new Area("Street", "");
	
	
	    string firstDescription = "A busy street market.";
	    string[] firstKeywords = {"west", "archway", "west archway", "market", "street market"};
	    string secondDescription = "An empty street.";
	    string[] secondKeywords = {"east", "archway", "east archway"};
	    linkAreas(areaA, firstDescription, firstKeywords, areaB, secondDescription, secondKeywords, true);
	
	    secondDescription = firstDescription;
	    firstDescription = "A street.";
	    firstKeywords = new string[]{"west", "street"};
	    secondKeywords = new string[]{"east", "market", "street market"};
	    linkAreas(areaB, firstDescription, firstKeywords, areaC, secondDescription, secondKeywords, true);
	
	    secondDescription = firstDescription;
	    firstDescription = "Xing Fu's chinese restaurant.";
	    firstKeywords = new string[]{"north", "xing fu's", "restaurant", "xing fu's restaurant", "chinese restaurant", "xing fu's chinese restaurant"};
	    secondKeywords = new string[]{"south", "street", "exit"};
	    linkAreas(areaC, firstDescription, firstKeywords, areaD, secondDescription, secondKeywords, true);
    }

    public void linkAreas(Area firstArea, string firstDescription, string[] firstKeywords, Area secondArea, string secondDescription, string[] secondKeywords, bool makeSiblings)
    {
        //To connect two areas, we need the Areas themselves, and two links.
        //First create the list of keywords that can be used to refer to an object
        List<string> firstKeywordList = new List<string>(firstKeywords);
        List<string> secondKeywordList = new List<string>(secondKeywords);

        //Then instantiate the links. Here we are using the default travel and blocked descriptions.
        Link firstLink = new Link(firstDescription, firstKeywordList);
        Link secondLink = new Link(secondDescription, secondKeywordList);

        //Now we need to connect the two rooms together. First we make the two links siblings. This is *IMPORTANT*.
        if (makeSiblings == true)
        {
            firstLink.makeSibling(secondLink);
        }

        //These two statements connects firstArea to secondArea using firstLink.
        firstArea.connect(firstLink, secondArea);
        secondArea.connect(secondLink, firstArea);

        //The reason we need to link both ways, is that links can be made to be one way, or to behave in unusual ways (in keeping with the horror theme
        // if we so desire. Making a link a sibling tells the engine that they are different ends of the same link. That way they are kept in sync
        // so if one is locked, for example, the other will be locked as well.
    }
}