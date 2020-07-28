using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager S;

    [SerializeField]
    private Reaction _reaction;

    private EtahniaDialogue _etahnia = new EtahniaDialogue(); public EtahniaDialogue GetEtahnia() => _etahnia;
    private TutorialLook _tutorial = new TutorialLook();
    private TutorialDialogue _tutorialD = new TutorialDialogue();

    private void Awake()
    {
        S = this;
    }

    public void Clear()
    {
        _etahnia.Clear();
        _tutorial.Clear();
        _tutorialD.Clear();
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

    public void StartEvent(EventTrigger e, Transform player)
    {
        if (e.Event is EventDiscussion eDisc)
        {
            StartDiscussion(eDisc, -1);
        }
        else if (e.Event is EventDoor eDoor)
        {
            if (eDoor.RequiredPhase > TutorialManager.S.GetProgression()) // We can't use that because we didn't go far enough in the tutorial
            {
                var result = _tutorial.GetText();
                if (result == null)
                {
                    Clear();
                    DialoguePopup.S.Close();
                    return;
                }
                StartPopup(result);
            }
            else if (eDoor.FailureType == DoorFailureType.NONE)
            {
                player.position = eDoor.Destination.transform.position;
                eDoor.transform.parent.gameObject.SetActive(false);
                eDoor.Destination.transform.parent.gameObject.SetActive(true);
                switch (TutorialManager.S.GetProgression())
                {
                    case TutorialProgression.ETAHNIA_INTRO: // The player go back to the human world for the first time
                        PlayerController.S.SetCanMove(false);
                        Clear();
                        StartTutorialDiscution();
                        break;
                }
            }
        }
        else
            throw new ArgumentException("Invalid event " + e.name);
    }

    private void StartPopup(string text)
    {
        DialoguePopup.S.Display(Character.MC, Character.NONE, FacialExpression.NEUTRAL, FacialExpression.NEUTRAL, text, true, "Me");
    }

    public void StartTutorialDiscution(int id = -1)
    {
        IDialogueResult result;
        result = _tutorialD.GetDialogue(null, id);
        if (result == null)
        {
            PlayerController.S.SetCanMove(true);
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
            DialoguePopup.S.Display(Character.MC, nDial.Speaker, _speakerOneLastExpression, _speakerTwoLastExpression, nDial.Text, !nDial.IsSpeaking, nDial.NameOverride);
        }
        else if (result is ChoiceDialogue cDial)
        {
            DialoguePopup.S.DisplayChoices(null, cDial.Choices);
        }
    }

    public void StartDiscussion(EventDiscussion e, int id = -1)
    {
        // Tutorial choice callback
        if (e == null)
        {
            StartTutorialDiscution(id);
            return;
        }

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