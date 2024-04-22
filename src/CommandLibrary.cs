class CommandLibrary
{
	private readonly HashSet<string> validCommands = new HashSet<string> { "help", "go", "quit", "look", "status", "take", "drop", "use", "attack" };

	public bool IsValidCommandWord(string instring) => validCommands.Contains(instring);

	public string GetCommandsString() => string.Join(", ", validCommands);
}