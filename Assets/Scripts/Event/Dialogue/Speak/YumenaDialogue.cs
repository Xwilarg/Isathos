using Event.Dialogue.Result;
using Event.Trigger.EventType;
using Other;

namespace Event.Dialogue.Speak
{
    public class YumenaDialogue : ADialogue
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
            {
                _knownName = "Yumena";
                return new NormalDialogue(true, "Wh- What is happening? It's... It's okay Yumena, stays calm...", FacialExpression.NEUTRAL, _knownName);
            }
            if (_currProgress == 1) return new NormalDialogue(false, "Are you okay?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 2) return new NormalDialogue(true, "I'm... Just not good with sudden situations... I need to sit down a bit...", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 3) return new NormalDialogue(Character.HURIANE, true, "I'll guide her to her room so she can rest a bit.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 4) return new NormalDialogue(false, "T-thanks...", FacialExpression.NEUTRAL, _knownName);
            e.gameObject.SetActive(false);
            InformationManager.S.DidSummonYumena = true;
            return null;
        }

        public YumenaDialogue() : base()
        {
            _current = Intro;
        }
    }
}