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

    public void StartEvent(Character c)
    {
        DialogueResult? result;
        if (c == Character.ETAHNIA)
            result = _etahnia.GetDialogue();
        else
            throw new ArgumentException("Invalid character " + c.ToString());
        if (!result.HasValue)
        {
            Clear();
            DialoguePopup.S.Close();
            return;
        }
        DialoguePopup.S.Display(Character.MC, c, result.Value.Text, !result.Value.IsSpeaking, result.Value.NameOverride);
    }
}