public struct DialogueResult
{
    public DialogueResult(bool isSpeaking, string text, string nameOverride = null)
    {
        IsSpeaking = isSpeaking;
        Text = text;
        NameOverride = nameOverride;
    }

    public bool IsSpeaking;
    public string Text;
    public string NameOverride;
}