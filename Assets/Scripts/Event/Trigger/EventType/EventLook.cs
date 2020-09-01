using UnityEngine;

namespace Event.Trigger.EventType
{
    [RequireComponent(typeof(EventTrigger))]
    public class EventLook : MonoBehaviour, IEvent
    {
        private void Awake()
        {
            GetComponent<EventTrigger>().Event = this;
        }

        public Other.Zone Zone;

        public string ObjectId;
    }
}