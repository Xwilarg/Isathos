using UnityEngine;
using UnityEngine.UI;

public class AchievementPopup : MonoBehaviour
{

    [SerializeField]
    private Text _name, _description;

    public void Init(string name, string description)
    {
        _name.text = name;
        _description.text = description;
    }
}
