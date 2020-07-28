public class NachiDialogue : ADialogue
{
    public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
    {
        var result = _current(e, lastChoiceId);
        IncreaseProgress();
        return result;
    }

    private IDialogueResult IntroEnd(EventDiscussion e, int lastChoiceId)
    {
        if (_currProgress == 0) return new NormalDialogue(true, "Don't worry, we will keep this portal open until you come back!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 1) return new NormalDialogue(false, "Thanks.", FacialExpression.NEUTRAL, "Me");

        return null;
    }

    public NachiDialogue() : base()
    {
        _current = IntroEnd;
    }
}