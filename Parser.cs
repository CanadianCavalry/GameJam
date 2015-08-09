using System;

public class Class1
{
    private string command;
    private string target;
    private string recipient;

	public Class1()
	{
        command = "";
        target = "";
        recipient = "";
	}

    public string parseInput(string inputString)
    {
        string lowString = inputString.ToLower();
        string[] inputArray = lowString.Split(new string[] { " " });

        if (inputArray.Length == 0)
        {
            return false;
        }

        command = inputArray[0];

        foreach (string word in inputArray)
        {
            string targetKey = target + word + " ";
        }

    }
}
