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

		string word1 = words.ElementAtOrDefault(0);
		string word2 = words.ElementAtOrDefault(1);
		string word3 = words.ElementAtOrDefault(2);

		return commandLibrary.IsValidCommandWord(word1) ? new Command(word1, word2, word3) : new Command(null, null, null);
	}

	public void PrintValidCommands()
	{
		Console.WriteLine("List of commands:");
		Console.WriteLine(commandLibrary.GetCommandsString());
	}
}