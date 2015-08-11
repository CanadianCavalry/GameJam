using System.Collections.Generic;

namespace GameJam
{
    class Builder
    {
        public void buildSmallWorld()
    {
        //First you need to create an area to start with
        Area areaA = new Area("Living Room", "You find yourself in a small, cramped room packed with furniture. There is wooden door to the east leading out into the street");
        Area areaB = new Area("Street", "This dark, grungy cobblestone street has seen better days. Piles of trash lay heaped about and unidentified darks patches stain the stones.");
    
        //To connect two areas, we need the Areas themselves, and two links.
        //First create the list of keywords that can be used to refer to an object
        string[] keywordArr = {"east","door","east door","wood door","wooden door"};
        List<string> A1keywordList = new List<string>(keywordArr);

        keywordArr = new string[] {"west","door","west door","wood door","wooden door"};
        List<string> B1keywordList = new List<string>(keywordArr);

        //Then instantiate the links. Here we are using the default travel and blocked descriptions.
        Link linkA1 = new Link("A solid looking wooden door.", A1keywordList);
        Link linkB1 = new Link("A solid looking wooden door.", B1keywordList);

        //Now we need to connect the two rooms together. First we make the two links siblings. This is *IMPORTANT*.
        linkA1.makeSibling(linkB1);
        //These two statements connects areaA to areaB using linkA1.
        areaA.connect(linkA1, areaB);
        areaB.connect(linkB1, areaA);

        //The reason we need to link both ways, is that links can be made to be one way, or to behave in unusual ways (in keeping with the horror theme
        // if we so desire. Making a link a sibling tells the engine that they are different ends of the same link. That way they are kept in sync
        // so if one is locked, for example, the other will be locked as well.

        //Add some stuff to the room
        areaA.addFeature(new GameObject("A beat up looking dining table", new List<string>(new string[] {"table","dining table"})));

        //More verbose, for clarity. Here we are using every possible custom option. Most of these are optional. Check the class definition.
        Item cigarA = new Item(inDescription = "A stubby cigar that looks like its been around for a while",
                                inKeywords = new List<string>(new string[] {"cigar"}),
                                inName = "A Cigar", //This is what shows in your inventory
                                inSeenDesc = "A cigar is lying on the floor", //When you look at an area that the item is in
                                inPickupDesc = "You pick up the cigar and put it in your pocket.", //when you pick it up
                                inDropDesc = "You look at the cigar regretfully before tossing it to the ground.", //when you drop it
                                inInitSeenDesc = "Peeking out from under a newspaper on the table, you spy the end of a cigar.", //when you look at an area that the item is in, and you have not picked it up before
                                inInitPickupDesc = "Move the newspaper aside, you pick up the cigar.")  //When you pick it up for the FIRST time
        areaA.addItem(cigarA);
    }
    }
}