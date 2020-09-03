using UnityEngine;

namespace Phone
{
    public class PhoneManager : MonoBehaviour
    {
        public static PhoneManager S { private set; get; }

        public void Awake()
        {
            S = this;
        }

        [SerializeField]
        private GameObject _phone;

        public void TogglePhone()
        {
            _phone.SetActive(!_phone.activeInHierarchy);
        }
    }
}
