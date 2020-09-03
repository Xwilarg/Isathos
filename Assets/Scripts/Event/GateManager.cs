using UnityEngine;

namespace Event
{
    public class GateManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _gateEtahnia;

        public void EnableGateEtahnia()
        {
            _gateEtahnia.SetActive(true);
        }

        public static GateManager S { private set; get; }

        public void Awake()
        {
            S = this;
        }
    }
}
