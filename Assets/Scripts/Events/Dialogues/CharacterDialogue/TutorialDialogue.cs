using System;
using System.Collections.Generic;
using System.Linq;

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
        if (_current != null)
            return _current(e, lastChoiceId);

        if (_currProgress == 0) return new NormalDialogue(Character.SALENAE, true, "He is back!", FacialExpression.SMILE, "???");
        if (_currProgress == 1) return new NormalDialogue(Character.ANAEL, true, "Are we done here then?", FacialExpression.NEUTRAL, "???");
        if (_currProgress == 2) return new NormalDialogue(Character.MC, false, "(I have no idea what they are speaking about...)", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 3) return new NormalDialogue(Character.ANAEL, true, "So? Did you kill the celestian?", FacialExpression.NEUTRAL, "???");
        if (_currProgress == 4) return new NormalDialogue(Character.MC, false, "(He must be speaking about " + (EventManager.S.GetEtahnia().IsNameKnown() ? "Etahnia" : "the girl from before") + "." +
            " He looks like he would be ready to cut me down at any second, I should think carefully of my answer.)", FacialExpression.NEUTRAL, "Me");

        if (lastChoiceId == -1) return new ChoiceDialogue(_introChoice.Select(x => x.Value).ToArray());

        return AskQuestion(_introChoice, e, lastChoiceId);
    }

    private IDialogueResult DisplayTmpDialog(bool isLying)
    {
        if (tmp == false)
        {
            if (_currProgress == 0)
            {
                if (isLying)
                    return new NormalDialogue(Character.EXPL_GOD, true, "Oh my, what a little liar.", FacialExpression.SMILE, "???");
                else
                    return new NormalDialogue(Character.EXPL_GOD, true, "Oh my, how bold.", FacialExpression.SMILE, "???");
            }
            if (_currProgress == 1) return new NormalDialogue(Character.EXPL_GOD, true, "However this route isn't ready yet.", FacialExpression.NEUTRAL, "???");
            if (_currProgress == 2) return new NormalDialogue(Character.EXPL_GOD, true, "Come again later.", FacialExpression.SMILE, "???");
        }
        else
        {
            if (_currProgress == 0) return new NormalDialogue(Character.EXPL_GOD, true, "My, aren't you a troublesome one?", FacialExpression.NEUTRAL, "???");
            if (_currProgress == 1) return new NormalDialogue(Character.EXPL_GOD, true, "This may seems unfair but I can't let you go this way too.", FacialExpression.NEUTRAL, "???");
            if (_currProgress == 2) return new NormalDialogue(Character.EXPL_GOD, true, "You shouldn't have too much difficulty choosing now.", FacialExpression.SMILE, "???");
        }
        return null;
    }

    private IDialogueResult IntroKillEtahniaConfirm(EventDiscussion e, int lastChoiceId)
    {
        var val = DisplayTmpDialog(true);
        if (val != null) return val;
        _current = null;
        _currProgress = 0;
        _introChoice.Remove(IntroKillEtahniaConfirm);
        tmp = true;
        return IntroKillEtahnia(e, lastChoiceId);
    }

    private IDialogueResult IntroKillEtahniaNotYet(EventDiscussion e, int lastChoiceId)
    {
        if (_currProgress == 0) return new NormalDialogue(Character.MC, false, "It was a bit harder than expected so I'm not done yet.", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 1) return new NormalDialogue(Character.ANAEL, true, "We don't have all day so you better hurry to go back inside.", FacialExpression.NEUTRAL, "???");
        if (_currProgress == 2) return new NormalDialogue(Character.SALENAE, true, "What Anael means is that keeping the portal open is rather tiresome so it's better not to take too much time.", FacialExpression.SMILE, "???");
        if (_currProgress == 3) return new NormalDialogue(Character.MC, false, "(Whatever my decision end up being, I should go back in that portal.)", FacialExpression.NEUTRAL, "Me");
        return null;
    }

    private IDialogueResult IntroKillEtahniaRefuse(EventDiscussion e, int lastChoiceId)
    {
        var val = DisplayTmpDialog(false);
        if (val != null) return val;
        _current = null;
        _currProgress = 0;
        _introChoice.Remove(IntroKillEtahniaRefuse);
        tmp = true;
        return IntroKillEtahnia(e, lastChoiceId);
    }

    private Dictionary<Func<EventDiscussion, int, IDialogueResult>, string> _introChoice;

    private bool tmp = false;

    public TutorialDialogue() : base()
    {
        _introChoice = new Dictionary<Func<EventDiscussion, int, IDialogueResult>, string>();
        _introChoice.Add(IntroKillEtahniaConfirm, "Yes, she is dead.");
        _introChoice.Add(IntroKillEtahniaNotYet, "No, not yet.");
        _introChoice.Add(IntroKillEtahniaRefuse, "I changed my mind.");
    }
}