using Other;
using UnityEngine;

namespace Event.Trigger.EventType
{
    [RequireComponent(typeof(EventTrigger))]
    public class EventDiscussion : MonoBehaviour, IEvent
    {
        private void Awake()
        {
            GetComponent<EventTrigger>().Event = this;
        }

        [SerializeField]
        private Character _me;

        public Character GetCharacter() => _me;
    }
}