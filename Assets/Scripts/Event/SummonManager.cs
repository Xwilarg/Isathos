using Event.Trigger;
using Event.Trigger.EventType;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public class SummonManager : MonoBehaviour
    {
        public static SummonManager S { private set; get; }

        public void Awake()
        {
            S = this;
        }

        [SerializeField]
        private GameObject _yumena, _yumenaDoor;

        [SerializeField]
        private Transform _summonPos;

        [SerializeField]
        private GameObject _preventSummon, _gateExit;

        private bool _canSummon = true;
        private List<(GameObject, GameObject)> _summons;

        private void Start()
        {
            _summons = new List<(GameObject, GameObject)>
            {
                (_yumena, _yumenaDoor)
            };
        }

        // We spoke with the last summon so we can go with a new one
        public void ClearSummon()
        {
            _canSummon = true;
            _preventSummon.SetActive(false);
        }

        public bool CanSummon()
            => _canSummon;

        public bool Summon()
        {
            if (!Player.PlayerController.S.Inventory.ContainsItem(Inventory.ItemID.TRANSPORT_GEM))
                return false;
            _canSummon = false;
            var rand = Random.Range(0, _summons.Count);
            var go = Instantiate(_summons[rand].Item1, _summonPos.transform.parent);
            var doorGo = _summons[rand].Item2;

            // Speaking with the door now speak with the person inside
            doorGo.GetComponent<EventDoor>().enabled = false;
            doorGo.GetComponent<EventDiscussion>().enabled = true;
            doorGo.GetComponent<EventTrigger>().enabled = doorGo.GetComponent<EventDiscussion>();

            go.transform.position = _summonPos.transform.position;
            _summons.RemoveAt(rand);
            _preventSummon.SetActive(true);
            _gateExit.SetActive(false); // Make sure the gate to leave the player is closed
            return true;
        }
    }
}
