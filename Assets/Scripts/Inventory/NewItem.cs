using UnityEngine;
using UnityEngine.UI;

public class NewItem : MonoBehaviour
{
    [SerializeField]
    private TextMesh _name;

    [SerializeField]
    private SpriteRenderer _icon;

    public void Init(string name, Sprite icon)
    {
        _name.text = name;
        _icon.sprite = icon;
    }
}