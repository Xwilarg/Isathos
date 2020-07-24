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

    public void Display(Character speakerOne, Character speakerTwo, string text, bool isCharacterOneSpeaking, string speakerNameOverride)
    {
        _imageSpeakerOne.sprite = GetSprite(speakerOne);
        _imageSpeakerTwo.sprite = GetSprite(speakerTwo);
        _imageSpeakerOne.color = isCharacterOneSpeaking ? Color.white : new Color(0f, 0f, 0f, .5f);
        _imageSpeakerTwo.color = isCharacterOneSpeaking ? new Color(0f, 0f, 0f, .5f) : Color.white;
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

    private Sprite GetSprite(Character c)
    {
        if (c == Character.MC) return _sprites.MC;
        if (c == Character.ETAHNIA) return _sprites.Etahnia;
        return _sprites.Empty;
    }
}
