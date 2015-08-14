using System;
using System.Collections.Generic;

namespace GameJam
{

    class GameLogic
    {
        public GameState gameState;
        public Parser parser;
        private string playerResult;
        private string environmentResult;
        private bool turnPassed;
        private List<GameObject> foundObjects;
        private Dictionary<string, GameObject> executionParams;

        public GameLogic()
        {
            parser = new Parser();
            turnPassed = false;
            gameState = new GameState(new Player(), new Belisarius());
            foundObjects = new List<GameObject>();
            executionParams = new Dictionary<string, GameObject>();
        }

        public string processMessage(string playerInput)
        {
            Player player = gameState.player;

            if (player.isAlive() == false)
            {
                return "Game Over";
            }
            //initialize all the variables we need for the turn
            playerResult = string.Empty;
            foundObjects = new List<GameObject>();
            executionParams = new Dictionary<string, GameObject>();
            turnPassed = false;

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
            if (parserResult.ContainsKey("target"))
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

            playerResult = gameState.executeCommand(parserResult["command"], executionParams);
            environmentResult = gameState.turnPass();
            string output = playerResult + "\n\n" + environmentResult;
            return output;
        }

        internal string getIntro()
        {
            return gameState.getIntro();
        }
    }
}