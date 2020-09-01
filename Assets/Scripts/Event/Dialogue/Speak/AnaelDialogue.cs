using Event.Dialogue.Result;
using Event.Trigger.EventType;
using Other;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Event.Dialogue.Speak
{
    public class AnaelDialogue : ADialogue
    {
        public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
        {
            var result = _current(e, lastChoiceId);
            _knownName = "Anael";
            IncreaseProgress();
            return result;
        }

        private IDialogueResult Intro(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(true, "So what was the issue that prevent you to do your task?", FacialExpression.NEUTRAL, _knownName);

            if (lastChoiceId == -1) return new ChoiceDialogue(_introChoice.Select(x => x.Value).ToArray());

            return AskQuestion(_introChoice, e, lastChoiceId);
        }
        private IDialogueResult IntroDidntFindHer(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(false, "I wasn't able to find her, there is only white as far as the eye can see.", FacialExpression.NEUTRAL, "Me");
            if (_currProgress == 1) return new NormalDialogue(true, "I didn't expect that part to be easy but you will have to find a way.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 2) return new NormalDialogue(true, "Since there is no one but her, just use localization spells.", FacialExpression.NEUTRAL, _knownName);

            _current = IntroEnd;
            return null;
        }
        private IDialogueResult IntroTooStrong(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(false, "She was too strong, I couldn't fight her.", FacialExpression.NEUTRAL, "Me");
            if (_currProgress == 1) return new NormalDialogue(true, "You don't really looks hurt for someone who fought, looks more like you didn't even tried.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 2) return new NormalDialogue(true, "I don't think you would want me to tell you what happens if you fail to follow our agreement. So go back and get the job done.", FacialExpression.NEUTRAL, _knownName);

            DecreaseRelation(e);

            _current = IntroEnd;
            return null;
        }
        private IDialogueResult IntroEnd(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(true, "If have time to speak you would better use that time to do your job.", FacialExpression.NEUTRAL, _knownName);
            return null;
        }

        private Dictionary<Func<EventDiscussion, int, IDialogueResult>, string> _introChoice;
        public AnaelDialogue() : base()
        {
            _introChoice = new Dictionary<Func<EventDiscussion, int, IDialogueResult>, string>();
            _introChoice.Add(IntroDidntFindHer, "I didn't find her");
            _introChoice.Add(IntroTooStrong, "She was too strong");

            _current = Intro;
        }
    }
}