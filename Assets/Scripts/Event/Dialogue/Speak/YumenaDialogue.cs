using Event.Dialogue.Result;
using Event.Trigger.EventType;
using Other;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Event.Dialogue.Speak
{
    public class YumenaDialogue : ADialogue
    {
        public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
        {
            var result = _current(e, lastChoiceId);
            _knownName = "Yumena";
            IncreaseProgress();
            return result;
        }

        private IDialogueResult Intro(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(true, "It's... It's okay Yumena, stay calm...", FacialExpression.NEUTRAL, _knownName);
            return null;
        }

        public YumenaDialogue() : base()
        {
            _current = Intro;
        }
    }
}