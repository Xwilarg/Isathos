using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryPopup : MonoBehaviour
    {
        public static InventoryPopup S { private set; get; }

        [SerializeField]
        private ItemPanel _itemPrefab;

        [SerializeField]
        private GameObject _inventoryPopup;

        private List<GameObject> _instanciatedItems;

        public void Awake()
        {
            S = this;
        }

        public void Start()
        {
            _instanciatedItems = new List<GameObject>();
        }

        /// <summary>
        /// Open the inventory if closed and close it if opened
        /// </summary>
        public void ToggleInventory()
        {
            _inventoryPopup.SetActive(!_inventoryPopup.activeInHierarchy);
            if (_inventoryPopup.activeInHierarchy)
                OpenInventory(null);
            else
                CloseInventory();
        }

        /// <summary>
        /// Force open/refresh the inventory (do nothing if it's already open)
        /// </summary>
        public void ForceOpenInventory(Action<ItemID> callback)
        {
            CloseInventory();
            OpenInventory(callback);
            _inventoryPopup.SetActive(true);
        }

        public void ForceCloseInventory()
        {
            CloseInventory();
            _inventoryPopup.SetActive(false);
        }

        /// <summary>
        /// Opent the player inventory
        /// </summary>
        /// <param name="callback">Button to display next to each item. If null, nothing is displayed</param>
        private void OpenInventory(Action<ItemID> callback)
        {
            foreach (var item in Player.PlayerController.S.Inventory.GetItems())
            {
                GameObject go = Instantiate(_itemPrefab.gameObject, _inventoryPopup.transform);
                var rTransform = (RectTransform)go.transform;
                rTransform.anchoredPosition = new Vector2(0f, -30 - (_instanciatedItems.Count * 60f));
                _instanciatedItems.Add(go);

                var fItem = ItemManager.S.GetItem(item.Key);
                go.GetComponent<ItemPanel>().Init(fItem.GetIcon(), fItem.GetName(), fItem.GetDescription(), callback == null ? (Action)null : () => { callback(item.Key); });
            }
            if (callback != null) // This happens when we display the menu the give items to Etahnia
            {
                GameObject go = Instantiate(_itemPrefab.gameObject, _inventoryPopup.transform);
                var rTransform = (RectTransform)go.transform;
                rTransform.anchoredPosition = new Vector2(0f, -30 - (_instanciatedItems.Count * 60f));
                _instanciatedItems.Add(go);
                go.GetComponentInChildren<Button>().onClick.AddListener(new UnityAction(() => { callback((ItemID)(-1)); }));
            }
        }

        private void CloseInventory()
        {
            foreach (GameObject go in _instanciatedItems)
                Destroy(go);
            _instanciatedItems.RemoveAll(x => true);
        }
    }
}