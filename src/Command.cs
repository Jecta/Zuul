class Command
{
	public string CommandWord { get; }
	public string SecondWord { get; }
	public string ThirdWord { get; }

	public Command(string commandWord, string secondWord, string thirdWord)
	{
		CommandWord = commandWord;
		SecondWord = secondWord;
		ThirdWord = thirdWord;
	}

	public bool IsUnknown() => string.IsNullOrEmpty(CommandWord);

	public bool HasSecondWord() => !string.IsNullOrEmpty(SecondWord);

	public bool HasThirdWord() => !string.IsNullOrEmpty(ThirdWord);
}