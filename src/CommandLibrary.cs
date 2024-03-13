class CommandLibrary
{
	private readonly HashSet<string> validCommands;

	public CommandLibrary()
	{
		validCommands = new HashSet<string> { "help", "go", "quit", "look", "status", "show", "status", "take", "drop", "use", "attack" };
	}

	public bool IsValidCommandWord(string instring)
	{
		return validCommands.Contains(instring);
	}

	public string GetCommandsString()
	{
		return string.Join(", ", validCommands);
	}
}