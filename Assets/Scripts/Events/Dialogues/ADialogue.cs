using System;
using System.Collections.Generic;
using System.Linq;

public abstract class ADialogue : AEventMessage
{
    public ADialogue() : base()
    {
        _current = null;
    }

    /// <summary>
    /// Get the next dialogue
    /// </summary>
    /// <param name="lastChoiceId">If the last dialogue was a choice, id the of the answer (0 to 3), else -1</param>
    public abstract IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId);

    protected void IncreaseRelation(EventDiscussion e)
    {
        _relation++;
        EventManager.S.DisplayReaction(e, ReactionType.RELATION_UP);
    }
    protected IDialogueResult AskQuestion(Dictionary<Func<EventDiscussion, int, IDialogueResult>, string> choices, EventDiscussion e, int choiceId)
    {
        _currProgress = 0;
        _current = choices.ElementAt(choiceId).Key;
        return _current(e, choiceId);
    }

    private int _relation;
    private string _knownName = "???";
    protected Func<EventDiscussion, int, IDialogueResult> _current;

    public bool IsNameKnown()
        => _knownName != "???";
}
