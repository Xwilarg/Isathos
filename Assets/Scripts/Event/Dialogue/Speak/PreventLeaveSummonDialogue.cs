using Event.Dialogue.Result;
using Event.Trigger.EventType;
using Other;

namespace Event.Dialogue.Speak
{
    public class PreventLeaveSummonDialogue : ADialogue
    {
        public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
        {
            var result = _current(e, lastChoiceId);
            IncreaseProgress();
            return result;
        }

        private IDialogueResult IntroEnd(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(Character.NARRATOR, true, "I should speak with the new girl first...", FacialExpression.SMILE, _knownName);

            return null;
        }

        public PreventLeaveSummonDialogue() : base()
        {
            _current = IntroEnd;
        }
    }
}