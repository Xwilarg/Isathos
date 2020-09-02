using UnityEngine;
using UnityEngine.UI;

namespace Event
{
    public class ComputerManager : MonoBehaviour
    {
        public static ComputerManager S { private set; get; }

        public void Awake()
        {
            S = this;
        }

        [SerializeField]
        private GameObject _hurianeComputer;

        [SerializeField]
        private InputField _hurianePasswordField;

        [SerializeField]
        private Text _hurianLoginErrorMessage;

        public void StartComputer()
        {
            _hurianeComputer.SetActive(true);
            Player.PlayerController.S.SetIsCinematic(true);
        }

        public void CheckPassword()
        {
            _hurianLoginErrorMessage.gameObject.SetActive(true);
        }
    }
}
