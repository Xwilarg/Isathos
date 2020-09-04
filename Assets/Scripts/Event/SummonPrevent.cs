using Event.Trigger.EventType;
using Other;
using Player;
using UnityEngine;

namespace Event
{
    public class SummonPrevent : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            EventManager.S.DisplayDiscution(null, -1, Character.SUMMON);
        }
    }
}
