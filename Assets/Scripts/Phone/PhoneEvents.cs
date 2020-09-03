using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Phone
{
    public class PhoneEvents : MonoBehaviour
    {
        [SerializeField]
        private Text _phoneNumber;

        [SerializeField]
        private Transform _callingMenu, _mainMenu;

        public void AddNumber(string nb)
        {
            if (_phoneNumber.text.Length < 11)
            {
                if (_phoneNumber.text.Length == 3 || _phoneNumber.text.Length == 7)
                    _phoneNumber.text += "-";
                _phoneNumber.text += nb;
            }
        }

        public void RemoveNumber()
        {
            if (_phoneNumber.text.Length > 0)
            {
                if (_phoneNumber.text.Length == 4 || _phoneNumber.text.Length == 8)
                    _phoneNumber.text = _phoneNumber.text.Substring(0, _phoneNumber.text.Length - 1);
                _phoneNumber.text = _phoneNumber.text.Substring(0, _phoneNumber.text.Length - 1);
            }
        }

        public void Call()
        {
            _callingMenu.SetAsLastSibling();
            StartCoroutine(CallToMenu());
        }

        private IEnumerator CallToMenu()
        {
            yield return new WaitForSeconds(2f);
            _mainMenu.SetAsLastSibling();
        }
    }
}
