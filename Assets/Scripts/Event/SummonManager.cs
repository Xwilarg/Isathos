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
        private GameObject _yumena;

        [SerializeField]
        private Transform _summonPos;

        private bool _canSummon = true;
        private List<GameObject> _summons;

        private void Start()
        {
            _summons = new List<GameObject>
            {
                _yumena
            };
        }

        // We spoke with the last summon so we can go with a new one
        public void ClearSummon()
        {
            _canSummon = true;
        }

        public bool CanSummon()
            => _canSummon;

        public bool Summon()
        {
            if (_summons.Count == 0)
                return false;
            _canSummon = false;
            var rand = Random.Range(0, _summons.Count);
            var go = Instantiate(_summons[rand], _summonPos.transform.parent);
            go.transform.position = _summonPos.transform.position;
            _summons.RemoveAt(rand);
            return true;
        }
    }
}
