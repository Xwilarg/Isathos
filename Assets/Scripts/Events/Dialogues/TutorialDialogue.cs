using System;

public class TutorialDialogue : ADialogue
{
    public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
    {
        IDialogueResult res = null;
        if (TutorialManager.S.GetProgression() == TutorialProgression.ETAHNIA_INTRO)
            res = IntroKillEtahnia(e, lastChoiceId);
        else
            throw new ArgumentException("Invalid tutorial state " + TutorialManager.S.GetProgression());
        IncreaseProgress();
        return res;
    }

    private IDialogueResult IntroKillEtahnia(EventDiscussion e, int lastChoiceId)
    {
        if (_currProgress == 0) return new NormalDialogue(Character.LEANA, true, "Hey, he is back!", FacialExpression.SMILE, "???");
        if (_currProgress == 1) return new NormalDialogue(Character.ANAEL, true, "So how did that went?", FacialExpression.SMILE, "???");

        return null;
    }
}