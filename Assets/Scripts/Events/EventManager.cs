using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager S;

    private EtahniaDialogue _etahnia = new EtahniaDialogue();

    private void Awake()
    {
        S = this;
    }

    public void Clear()
    {
        _etahnia.Clear();
    }

    public void StartEvent(Character c, int id = -1)
    {
        IDialogueResult result;
        if (c == Character.ETAHNIA)
            result = _etahnia.GetDialogue(id);
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
            DialoguePopup.S.DisplayChoices(c, cDial.Choices);
        }
        else
            throw new InvalidOperationException("GetDialogue returned an unknown type for " + c.ToString());
    }

    // To keep the last expression when the other person is speaking
    private FacialExpression _speakerOneLastExpression = FacialExpression.NEUTRAL;
    private FacialExpression _speakerTwoLastExpression = FacialExpression.SMILE;
}