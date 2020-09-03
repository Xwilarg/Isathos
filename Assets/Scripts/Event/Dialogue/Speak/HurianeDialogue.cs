using Event.Dialogue.Result;
using Event.Trigger.EventType;
using Other;

namespace Event.Dialogue.Speak
{
    public class HurianeDialogue : ADialogue
    {
        public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
        {
            var result = _current(e, lastChoiceId);
            IncreaseProgress();
            return result;
        }

        private IDialogueResult Intro(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0)
                return new NormalDialogue(true, "Wah a stranger in our house! Nice to meet you, I'm Huriane!", FacialExpression.SMILE, _knownName);
            if (_currProgress == 1)
            {
                _knownName = "Huriane";
                return new NormalDialogue(true, "I guess you are here from my husband? How can I help you?", FacialExpression.SMILE, _knownName);
            }
            if (_currProgress == 2)
                return new NormalDialogue(false, "A friend of your husband was sealed away and we are trying to free her.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 3)
                return new NormalDialogue(false, "To sum up I need to find information about the god Sae.", FacialExpression.NEUTRAL, _knownName);

            return null;
        }

        public HurianeDialogue() : base()
        {
            _current = Intro;
        }
    }
}