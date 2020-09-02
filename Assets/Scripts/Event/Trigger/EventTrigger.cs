using UnityEngine;

namespace Event.Trigger
{
    public class EventTrigger : MonoBehaviour
    {
        public IEvent Event { set; get; }

        private void Start()
        {
            foreach (var sr in GetComponentsInChildren<SpriteRenderer>())
                sr.sortingOrder = -(int)(transform.position.y * 100f);
        }
    }
}
