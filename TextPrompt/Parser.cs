using System;
using System.Collections.Generic;

public class Parser
{
    private string command;
    private string target;
    private string recipient;
	private List<string> wordsToRemove;
	private List<string> validCommands;

	public Parser()
	{
        command = "";
        target = "";
        recipient = "";
		validCommands = new List<string>(new string[] {"go","walk","get","take","look","examine","eat","drink","read","talk","ask","drop","use","open","close","equip","attack","reload"});
		wordsToRemove = new List<string>(new string[] {"at", "to", "the", "of", "from", "through", "towards"});
	}

    public Dictionary<string, string> parseInput(string inputString)
    {
		Dictionary<string, string> result = new Dictionary<string, string>();
		
		//Make the input lower case and then split it on the space character
        string lowString = inputString.ToLower();
        char[] delimiters = { ' ' };
        List<string> inputArray = new List<string>(lowString.Split(delimiters));

		//take the first word as the command
        command = inputArray[0];
		inputArray.RemoveAt(0);
		
		//verify that the we recognize the command
		if (!validCommands.Contains(command))
		{
			return null;
		}
		
		//Remove any useless particles from the input
		for (int i = 0; i > inputArray.Count; i++)
		{
			if (wordsToRemove.Contains(inputArray[i]))
			{
				inputArray.RemoveAt(i);
			}
		}
		
		//get the rest of the words as the target
        while (inputArray.Count > 0)
        {
            target += inputArray[0] + " ";
			inputArray.RemoveAt(0);
        }
		
		result["command"] = command;
		if (target != "")
		{
			result["target"] = target;
		}
		
		return result;
    }
}