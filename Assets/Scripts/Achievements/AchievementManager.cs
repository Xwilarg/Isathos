using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager S { private set; get; }

    [SerializeField]
    private GameObject _gameObject;

    public void Awake()
    {
        S = this;
    }

    public void DisplayAchievement(AchievementID id)
    {
        var go = Instantiate(_gameObject, transform);
    }
}
