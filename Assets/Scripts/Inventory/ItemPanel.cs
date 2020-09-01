using System;
using UnityEngine;
using UnityEngine.Events;
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

        public void Init(Sprite icon, string name, string description, Action callback)
        {
            _icon.sprite = icon;
            _name.text = name;
            _description.text = description;
            if (callback == null)
                _actionButton.gameObject.SetActive(false);
            else
            {
                _actionButtonText.text = "Give";
                _actionButton.onClick.AddListener(new UnityAction(callback));
                _actionButton.gameObject.SetActive(true);
            }
        }
    }
}