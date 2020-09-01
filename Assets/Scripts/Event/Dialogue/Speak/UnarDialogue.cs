using Event.Dialogue.Result;
using Event.Trigger.EventType;
using Other;

namespace Event.Dialogue.Speak
{
    public class UnarDialogue : ADialogue
    {
        public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
        {
            var result = _current(e, lastChoiceId);
            IncreaseProgress();
            return result;
        }

        private IDialogueResult IntroEnd(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(true, "...", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 1) return new NormalDialogue(false, "...", FacialExpression.NEUTRAL, "Me");

            return null;
        }

        public UnarDialogue() : base()
        {
            _current = IntroEnd;
        }
    }
}