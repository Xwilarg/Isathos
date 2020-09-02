using Event.Dialogue.Result;
using Event.Trigger.EventType;
using Inventory;
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
            if (_currProgress == 0) return new NormalDialogue(true, "A human? How did you find this place?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 1) return new NormalDialogue(false, "Etahnia sent me here, she told me you could help me.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 2) return new NormalDialogue(true, "This fool? What do you need help for?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 3) return new NormalDialogue(false, "I'm not sure to really understand myself, she told me that she need me to become a god to help her escape and that I need to be acknowledged by Sae.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 4) return new NormalDialogue(true, "What nonsence, if anyone could become a god you and me wouldn't be speaking here.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 5) return new NormalDialogue(true, "However I still have a debt towards Etahnia so I'll lend you a hand... Or at least I'll lend you my library.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 6) return new NormalDialogue(true, "I would have like to help you but I made myself a cup of black tea with honey and I would hate for it to be cold.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 7) return new NormalDialogue(true, "If you have any other question, feel free to ask my wife.", FacialExpression.NEUTRAL, _knownName);

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