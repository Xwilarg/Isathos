using Event.Dialogue.Result;
using Event.Trigger.EventType;
using Other;

namespace Event.Dialogue.Speak
{
    public class EranelDialogue : ADialogue
    {
        public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
        {
            var result = _current(e, lastChoiceId);
            IncreaseProgress();
            return result;
        }

        private IDialogueResult Intro(EventDiscussion e, int lastChoiceId)
        {
            Player.PlayerController.S.SetIsCinematic(true);
            if (_currProgress == 0) return new NormalDialogue(true, "A human? How did you find this place?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 1) return new NormalDialogue(false, "Etahnia sent me here, she told me you could help me.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 2) return new NormalDialogue(true, "This fool? What does she need help for?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 3)
            {
                if (InformationManager.S.HaveSealedPlanInfo && InformationManager.S.HaveBanInfo)
                    return new NormalDialogue(false, "She was banned to another plan and need me to help her to get out of there.", FacialExpression.NEUTRAL, _knownName);
                if (InformationManager.S.HaveSealedPlanInfo)
                    return new NormalDialogue(false, "She tried to explained me, she was banned from the celestials and need my help to get out of there.", FacialExpression.NEUTRAL, _knownName);
                if (InformationManager.S.HaveBanInfo)
                    return new NormalDialogue(false, "She tried to explained me, she is stuck is an alternative plan and need my help to get out of there.", FacialExpression.NEUTRAL, _knownName);
                return new NormalDialogue(false, "I didn't really understand, she is stuck in a white place or something and need me to help her to get out of there.", FacialExpression.NEUTRAL, _knownName);
            }
            if (_currProgress == 4) return new NormalDialogue(false, "To do that, she told me that she need me to become a god to help her escape and that I need to be acknowledged by Sae.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 5) return new NormalDialogue(true, "What nonsense, if anyone could become a god you and me wouldn't be speaking here.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 6)
            {
                if (InformationManager.S.HaveSealedPlanInfo && InformationManager.S.HaveBanInfo)
                    return new NormalDialogue(true, "How troublesome, the council finally decided to banish her... For grave crimes celestials are usually banned to sealed plans like this one.", FacialExpression.NEUTRAL, _knownName);
                if (InformationManager.S.HaveSealedPlanInfo || InformationManager.S.HaveBanInfo)
                    return new NormalDialogue(true, "I feel like you're missing some bit of information, from what you told me I guess she was banned to a sealed plan, that's what happens when celestials commit grave crimes.", FacialExpression.NEUTRAL, _knownName);
                return new NormalDialogue(true, "Looks like you don't know a lot, I guess she was banned to a sealed plan, that's what happens when celestials commit grave crimes.", FacialExpression.NEUTRAL, _knownName);

            }
            if (_currProgress == 7)
            {
                InformationManager.S.HaveBanInfo = true;
                InformationManager.S.HaveSealedPlanInfo = true;
                return new NormalDialogue(true, "I still have a debt towards Etahnia so I'll lend you a hand... Or at least I'll lend you my library.", FacialExpression.NEUTRAL, _knownName);
            }
            if (_currProgress == 8) return new NormalDialogue(true, "I would have like to help you but I made myself a cup of black tea with honey and I would hate for it to go cold.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 9) return new NormalDialogue(true, "My name is Eranel by the way, if you have any other question, feel free to ask my wife.", FacialExpression.NEUTRAL, _knownName);

            Player.PlayerController.S.SetIsCinematic(false);
            _knownName = "Eranel";
            EventManager.S.EnableEranelDoor();
            Tutorial.TutorialManager.S.IntroduceMeetEranel();
            e.gameObject.SetActive(false);

            return null;
        }

        public EranelDialogue() : base()
        {
            _current = Intro;
        }
    }
}