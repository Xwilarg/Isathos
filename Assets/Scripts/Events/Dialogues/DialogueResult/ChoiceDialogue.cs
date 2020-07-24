public struct ChoiceDialogue : IDialogueResult
{
    public ChoiceDialogue(params string[] choices)
    {
        Choices = choices;
    }

    public string[] Choices;
}