using System;
using GameState;
using GameObject;
using Area;
using Player;
using GUI;
using Locater;

public class Main() 
{
	GameState gameState;
	Parser parser;
	GUI gui;
	string playerResult;
	string environmentResult;
	List<GameObject> foundObjects;
	IDictionary<string, GameObject> executionParams;

    public Main()
    {
        parser = new Parser();
        gui = new GUI();
    }

    public void runGame()
    {
        while (true)
        {
	        gameState = new GameState(new Player());
	
	        while (player.isAlive())
	        {
		        playerResult = null;
		        foundObjects = null;
		        executionParams = null;
			
		        string playerInput = GUI.getInput();	//TODO
			
		        //If there's no input, go back and try again
		        if (!playerInput)
		        {
			        continue;
		        }
			
		        IDictionary<string, string> parserResult = parser.parseInput(playerInput);
			
		        //If the parser returns false, then the command is not recognized
		        if (!parserResult)
		        {
			        GUI.displayText("I don't understand that command.";
			        continue;
		        }
			
		        //If there's a target specified, attempt to retrieve it
		        if (parserResult["target"])
		        {
			        foundObjects = gameState.getLocalObject(parserResult["target"]);
			
			        if (foundObjects.Count() == 0)
			        {
				        GUI.displayText("There is nothing like that here.");
				        continue;
			        }
			        else if (foundObjects.Count() > 1)
			        {
				        GUI.displayText("You need to be more specific.");
				        continue;
			        }
				
			        executionParams["target"] = foundObjects[0];
		        }
			
		        playerResult = gameState.executeAction(parserResult["command"], executionParams);	//TODO
		        environmentResult = gameState.turnPass();
		        GUI.displayText(playerResult + "\n\n" + environmentResult);
	        }
	
        GUI.displayText("Game Over");
        }
    }
}