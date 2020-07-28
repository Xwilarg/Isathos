using System;

public abstract class ADialogue : AEventMessage
{
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

    private int _relation;
    protected Func<EventDiscussion, int, IDialogueResult> _current;
}
