using UnityEngine;

namespace Event.Trigger.EventType
{
    [RequireComponent(typeof(EventTrigger))]
    public class EventDoor : MonoBehaviour, IEvent
    {
        private void Awake()
        {
            GetComponent<EventTrigger>().Event = this;
        }

        [Tooltip("If the door is locked, reason why it is")]
        public DoorFailureType FailureType;

        [Tooltip("Is the door a warp (instead of a normal door)")]
        public bool IsWarp;

        [Tooltip("Where the door is leading")]
        public GameObject Destination;

        [Tooltip("Phase that much be reached to use this object")]
        public Tutorial.Progression RequiredPhase;
    }

}