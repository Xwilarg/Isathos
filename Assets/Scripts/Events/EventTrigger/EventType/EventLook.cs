using UnityEngine;

[RequireComponent(typeof(EventTrigger))]
public class EventLook : MonoBehaviour, IEvent
{
    private void Awake()
    {
        GetComponent<EventTrigger>().Event = this;
    }

    public Zone Zone;

    public string ObjectId;
}
