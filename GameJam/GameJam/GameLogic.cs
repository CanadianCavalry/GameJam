using System;
using System.Collections.Generic;

namespace GameJam
{

    class GameLogic
    {
        GameState gameState;
        Parser parser;
        string playerResult;
        string environmentResult;
        List<GameObject> foundObjects;
        IDictionary<string, GameObject> executionParams;

        public GameLogic()
        {
            parser = new Parser();
            gameState = new GameState(new Player());
        }

        public string processMessage(string playerInput)
        {
            Player player = gameState.player;

            if (player.isAlive() == true)
            {
                playerResult = null;
                foundObjects = null;
                executionParams = null;

                //If there's no input, go back and try again
                if (playerInput.Equals(string.Empty) == true)
                {
                    return string.Empty;
                }

                Dictionary<string, string> parserResult = parser.parseInput(playerInput);

                //If the parser returns false, then the command is not recognized
                if (parserResult == null)
                {
                    return "I don't understand that command.";
                }

                //If there's a target specified, attempt to retrieve it
                if (parserResult["target"])
                {
                    foundObjects = gameState.getLocalObject(parserResult["target"]);

                    if (foundObjects.Count == 0)
                    {
                        return "There is nothing like that here.";
                    }
                    else if (foundObjects.Count > 1)
                    {
                        return "You need to be more specific.";
                    }

                    executionParams["target"] = foundObjects[0];
                }

                playerResult = gameState.executeAction(parserResult["command"], executionParams);	//TODO
                environmentResult = gameState.turnPass();
                string output = playerResult + "\n\n" + environmentResult;
                return output;
            }

            return "Game Over";
        }
    }
}