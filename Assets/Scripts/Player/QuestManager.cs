using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager S;

    [SerializeField]
    private Text _questText;

    private void Awake()
    {
        S = this;
    }

    public void UpdateQuestDescription(string content)
    {
        _questText.text = content;
    }
}