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

    public abstract DialogueResult? GetDialogue();

    protected int _currProgress;
}
