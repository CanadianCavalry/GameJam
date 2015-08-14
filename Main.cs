using System;
using GameState;
using GameObject;
using Area;
using Player;
using GUI;
using Locater;

public class Main() 
{
	public GameState gameState;
	public Parser parser;
	public GUI gui;
	public string playerResult;
	public string environmentResult;
	public bool turnPassed;
	List<GameObject> foundObjects;
	IDictionary<string, GameObject> executionParams;

    public Main()
    {
		gameState = new GameState();
		turnPassed = false;
        parser = new Parser();
        gui = new GUI();
		foundObjects = new List<GameObject>();
		executionParams = new IDictionary<string, GameObject>();
    }

    public void runGame()
    {
        while (true)
        {
	        gameState = new GameState(new Player());
	
	        while (player.isAlive())
	        {
				//initialize all the variables we need for the turn
		        playerResult = null;
		        foundObjects = null;
		        executionParams = null;
				turnPassed = false;
			
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
			
		        playerResult = gameState.executeAction(parserResult["command"], executionParams);
		        environmentResult = gameState.turnPass();
		        GUI.displayText(playerResult + "\n\n" + environmentResult);

	        }
	
			GUI.displayText("Game Over");
        }
    }
}