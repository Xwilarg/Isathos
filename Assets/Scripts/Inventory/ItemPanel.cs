using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    class ItemPanel : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Text _name, _description, _actionButtonText;

        [SerializeField]
        private Button _actionButton;

        public void Init(Sprite icon, string name, string description)
        {
            _icon.sprite = icon;
            _name.text = name;
            _description.text = description;
            _actionButton.gameObject.SetActive(false);
        }
    }
}