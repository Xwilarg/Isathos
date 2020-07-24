public abstract class ADialogue
{
    public ADialogue()
    {
        _currProgress = 0;
    }

    public void Clear()
    {
        _currProgress = 0;
    }

    /// <summary>
    /// Get the next dialogue
    /// </summary>
    /// <param name="lastChoiceId">If the last dialogue was a choice, id the of the answer (0 to 3), else -1</param>
    public abstract IDialogueResult GetDialogue(int lastChoiceId);

    protected int _currProgress;
}
