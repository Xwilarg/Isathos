namespace Event.Look
{
    public abstract class ALook : AEventMessage
    {
        public abstract string GetText(string name);
    }
}