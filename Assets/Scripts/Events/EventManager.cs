using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager S;

    [SerializeField]
    private Reaction _reaction;

    private EtahniaDialogue _etahnia = new EtahniaDialogue();

    private void Awake()
    {
        S = this;
    }

    public void Clear()
    {
        _etahnia.Clear();
    }

    public void DisplayReaction(EventDiscussion e, ReactionType react)
    {
        GameObject go = null;
        if (react == ReactionType.RELATION_UP)
            go = _reaction.relationUp;

        if (go == null)
            throw new ArgumentException("Invalid reaction " + react.ToString());

        Instantiate(go, e.transform.position + (Vector3)(Vector2.one * .2f), Quaternion.identity);
    }

    public void StartEvent(EventTrigger e)
    {
        if (e.Event is EventDiscussion eDisc)
        {
            StartDiscussion(eDisc, -1);
        }
    }

    public void StartDiscussion(EventDiscussion e, int id = -1)
    {
        Character c = e.GetCharacter();

        IDialogueResult result;
        if (c == Character.ETAHNIA)
            result = _etahnia.GetDialogue(e, id);
        else
            throw new ArgumentException("Invalid character " + c.ToString());
        if (result == null)
        {
            Clear();
            DialoguePopup.S.Close();
            return;
        }
        if (result is NormalDialogue nDial)
        {
            if (nDial.IsSpeaking)
                _speakerTwoLastExpression = nDial.Expression;
            else
                _speakerOneLastExpression = nDial.Expression;
            DialoguePopup.S.Display(Character.MC, c, _speakerOneLastExpression, _speakerTwoLastExpression, nDial.Text, !nDial.IsSpeaking, nDial.NameOverride);
        }
        else if (result is ChoiceDialogue cDial)
        {
            DialoguePopup.S.DisplayChoices(e, cDial.Choices);
        }
        else
            throw new InvalidOperationException("GetDialogue returned an unknown type for " + c.ToString());
    }

    // To keep the last expression when the other person is speaking
    private FacialExpression _speakerOneLastExpression = FacialExpression.NEUTRAL;
    private FacialExpression _speakerTwoLastExpression = FacialExpression.SMILE;
}