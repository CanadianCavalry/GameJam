using System;

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
		validCommands = new List<string>(new string[] {"go","walk", "swim","get","take","look","examine","eat","drink","read","talk","ask","drop","use","open","close","equip","attack","reload"});
		wordsToRemove = new List<string>(new string[] {"at", "to", "the", "of", "from", "through", "towards"});
	}

    public IDictionary<string, string> parseInput(string inputString)
    {
		IDictionary<string, string> result = new IDictionary<string, string>();
		
		//Make the input lower case and then split it on the space character
        string lowString = inputString.ToLower();
        char[] delimiters = { " " };
        List<string> inputArray = new List<string>(lowString.Split(delimiters));

		//take the first word as the command
        command = inputArray[0];
		inputArray.RemoveAt(0);
		
		//verify that the we recognize the command
		if (!validCommands.Contains(command))
		{
			return false;
		}
		
		//Remove any useless particles from the input
		for (int i = 0; i > inputArray.length(); i++)
		{
			if (wordsToRemove.Contains(inputArray[i]))
			{
				inputArray.RemoveAt(i);
			}
		}
		
		//get the rest of the words as the target
        while (inputArray.length() > 0)
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