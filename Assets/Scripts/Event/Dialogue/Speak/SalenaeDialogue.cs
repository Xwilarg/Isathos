using Event.Dialogue.Result;
using Event.Trigger.EventType;
using Inventory;
using Other;

namespace Event.Dialogue.Speak
{
    public class SalenaeDialogue : ADialogue
    {
        public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
        {
            var result = _current(e, lastChoiceId);
            IncreaseProgress();
            return result;
        }

        private IDialogueResult IntroEnd(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(true, "I wish we could help you more but we are not strong enough to go with you.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 1) return new NormalDialogue(true, "Oh but I can lend you this instead, may it protect you.", FacialExpression.SMILE, _knownName);
            _current = IntroEnd2;
            _currProgress = 0;
            EventManager.S.DisplayNewItem(e, ItemID.SALENAE_RING);
            return IntroEnd2(e, lastChoiceId);
        }

        private IDialogueResult IntroEnd2(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(true, "My mom gave it to me when I was young, she told me it watch over one's soul.", FacialExpression.SMILE, _knownName);
            if (_currProgress == 1) return new NormalDialogue(true, "Please don't loose it, it's really important for me.", FacialExpression.NEUTRAL, _knownName);
            _current = IntroEnd3;
            return null;
        }

        private IDialogueResult IntroEnd3(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(true, "Hopefully all of this will end soon, when that will be the case, let's eat something the both of us.", FacialExpression.SMILE, _knownName);
            return null;
        }

        public SalenaeDialogue() : base()
        {
            _current = IntroEnd;
        }
    }
}