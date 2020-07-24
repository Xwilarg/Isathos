using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePopup : MonoBehaviour
{
    public static DialoguePopup S;

    [SerializeField]
    private DialogueSprites _sprites;

    [SerializeField]
    private GameObject _popup;

    [SerializeField]
    private Image _imageSpeakerOne, _imageSpeakerTwo;

    [SerializeField]
    private Text _textName, _textContent;

    private void Awake()
    {
        S = this;
    }

    public void Display(Character speakerOne, Character speakerTwo, FacialExpression speakerOneExpression, FacialExpression speakerTwoExpression, string text, bool isCharacterOneSpeaking, string speakerNameOverride)
    {
        _imageSpeakerOne.sprite = GetSprite(speakerOne, speakerOneExpression);
        _imageSpeakerTwo.sprite = GetSprite(speakerTwo, speakerTwoExpression);

        // We reduce the alpha of the sprite of the character who isn't speaking by 0.5
        _imageSpeakerOne.color = isCharacterOneSpeaking ? Color.white : new Color(0f, 0f, 0f, .5f);
        _imageSpeakerTwo.color = isCharacterOneSpeaking ? new Color(0f, 0f, 0f, .5f) : Color.white;

        // Name is value of enum to lower with first character upper
        string name = (speakerNameOverride == null ? (isCharacterOneSpeaking ? speakerOne : speakerTwo).ToString().ToLower() : speakerNameOverride);
        name = char.ToUpper(name[0]) + string.Join("", name.Skip(1));
        _textName.text = name;

        _textContent.text = text;
        _popup.SetActive(true);
    }

    public void Close()
    {
        _popup.SetActive(false);
    }

    /// <summary>
    /// Get character sprite from a character and its facial expression
    /// </summary>
    private Sprite GetSprite(Character c, FacialExpression fe)
    {
        DialogueSpritesCharacter? charac = null;
        if (c == Character.MC) charac  = _sprites.MC;
        if (c == Character.ETAHNIA) charac = _sprites.Etahnia;
        if (charac == null)
            return _sprites.Empty;
        if (fe == FacialExpression.NEUTRAL)
            return charac.Value.Neutral;
        if (fe == FacialExpression.SMILE)
            return charac.Value.Smile;
        throw new ArgumentException("Invalid expression " + fe.ToString());
    }
}
