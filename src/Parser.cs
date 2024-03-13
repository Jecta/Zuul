class Parser
{
	private readonly CommandLibrary commandLibrary;

	public Parser()
	{
		commandLibrary = new CommandLibrary();
	}

	public Command GetCommand()
	{
		Console.Write("> ");

		string[] words = Console.ReadLine().Split(' ');
		string word1 = words.Length > 0 ? words[0] : null;
		string word2 = words.Length > 1 ? words[1] : null;
		string word3 = words.Length > 2 ? words[2] : null;

		return commandLibrary.IsValidCommandWord(word1) ? new Command(word1, word2, word3) : new Command(null, null, null);
	}

	public void PrintValidCommands()
	{
		Console.WriteLine("List of commands:");
		Console.WriteLine(commandLibrary.GetCommandsString());
	}
}