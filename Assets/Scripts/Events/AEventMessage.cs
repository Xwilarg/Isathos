public abstract class AEventMessage
{
    public AEventMessage()
    {
        _currProgress = 0;
    }

    public void Clear()
    {
        _currProgress = 0;
    }

    public void IncreaseProgress()
    {
        _currProgress++;
    }

    protected int _currProgress;
}