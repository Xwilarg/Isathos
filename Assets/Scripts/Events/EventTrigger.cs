using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField]
    private Character _me;

    public Character GetCharacter() => _me;
}
